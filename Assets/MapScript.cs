using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    [SerializeField] int mapWidth = 0;
    [SerializeField] int mapHeight = 0;

    float[,] knotMap;
    float[,] islandMap;
    float[,] navMesh;
    PathFind.Grid pathfindGrid;

    [Space]
    [SerializeField] [Tooltip("Big number is small possibility")] int islandPossibility = 0;
    [SerializeField] int maxIslandSize = 0;
    [SerializeField] int minIslandSize = 0;

    [Space]
    [SerializeField] GameObject tile = null;
    [SerializeField] GameObject water = null;

    // Start is called before the first frame update
    void Start()
    {
        knotMap = new float[mapWidth,mapHeight];
        islandMap = new float[mapWidth, mapHeight];
        navMesh = new float[mapWidth,mapHeight];

        generateKnots();
        generateIslands();
        readIslandsgeneration();
        drawMap();
        generateNavMesh();

        pathfindGrid = new PathFind.Grid(mapWidth, mapHeight, navMesh);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void generateKnots()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                float rngK = Random.Range(0, islandPossibility);
                if (rngK <= 1)
                {
                    float rngH = Random.Range(minIslandSize, maxIslandSize);
                    knotMap[x, y] = rngH;
                }
            }
        }
    }

    private void generateIslands()
    {
        bool smoothable = false;
        do
        {
            smoothable = false;
            for (int x = 1; x < mapWidth - 1; x++)
            {
                for (int y = 1; y < mapHeight - 1; y++)
                {
                    if (knotMap[x, y] > 1)
                    {
                        smoothable = true;

                        if (knotMap[x-1, y-1] <= 0.1f)
                        {
                            float rngH = Random.Range(knotMap[x, y] / 2, knotMap[x, y]);
                            knotMap[x-1, y-1] = rngH;
                        }

                        if (knotMap[x, y-1] <= 0.1f)
                        {
                            float rngH = Random.Range(knotMap[x, y] / 2, knotMap[x, y]);
                            knotMap[x, y-1] = rngH;
                        }

                        if (knotMap[x-1, y] <= 0.1f)
                        {
                            float rngH = Random.Range(knotMap[x, y] / 2, knotMap[x, y]);
                            knotMap[x-1, y] = rngH;
                        }

                        if (knotMap[x+1, y] <= 0.1f)
                        {
                            float rngH = Random.Range(knotMap[x, y] / 2, knotMap[x, y]);
                            knotMap[x+1, y] = rngH;
                        }

                        if (knotMap[x, y+1] <= 0.1f)
                        {
                            float rngH = Random.Range(knotMap[x, y] / 2, knotMap[x, y]);
                            knotMap[x, y+1] = rngH;
                        }

                        if (knotMap[x+1, y+1] <= 0.1f)
                        {
                            float rngH = Random.Range(knotMap[x, y] / 2, knotMap[x, y]);
                            knotMap[x+1, y+1] = rngH;
                        }

                        if (knotMap[x-1, y+1] <= 0.1f)
                        {
                            float rngH = Random.Range(knotMap[x, y] / 2, knotMap[x, y]);
                            knotMap[x-1, y+1] = rngH;
                        }

                        if (knotMap[x+1, y-1] <= 0.1f)
                        {
                            float rngH = Random.Range(knotMap[x, y] / 2, knotMap[x, y]);
                            knotMap[x+1, y-1] = rngH;
                        }

                        knotMap[x, y] = 1;
                    }
                }
            }
        } while (smoothable);
    }

    private void readIslandsgeneration()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (knotMap[x, y] >= 1)
                {
                    islandMap[x, y] = 1;
                }
            }
        }
    }

    private void drawMap()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                GameObject go = Instantiate<GameObject>(water);
                go.transform.position = new Vector3(x, y, 0);
                go.GetComponent<TileScript>().x = x;
                go.GetComponent<TileScript>().y = y;

                if (islandMap[x,y] > 0.99)
                {
                    go = Instantiate<GameObject>(tile);
                    go.transform.position = new Vector3(x, y, 0);
                    go.GetComponent<TileScript>().x = x;
                    go.GetComponent<TileScript>().y = y;
                    go.GetComponent<SmartTile>().x = x;
                    go.GetComponent<SmartTile>().y = y;
                    go.GetComponent<SmartTile>().islandMap = islandMap;
                }
            }
        }
    }

    private void generateNavMesh()
    {
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (islandMap[x,y] >= 0.99f)
                {
                    navMesh[x, y] = 0;
                }
                else
                {
                    navMesh[x, y] = 1;
                }
            }
        }
    }

    public List<PathFind.Point> Astar(int startX, int startY, int targetX, int targetY)
    {

        PathFind.Point _from = new PathFind.Point(startX, startY);
        PathFind.Point _to = new PathFind.Point(targetX, targetY);

        return PathFind.Pathfinding.FindPath(pathfindGrid, _from, _to);
    }
}
