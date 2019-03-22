using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    [SerializeField] int mapWidth = 0;
    [SerializeField] int mapHeight = 0;

    float[,] knotMap;
    int[,] islandMap;

    [Space]
    [SerializeField] [Tooltip("Big number is small possibility")] int islandPossibility = 0;
    [SerializeField] int maxIslandSize = 0;
    [SerializeField] int minIslandSize = 0;

    [Space]
    [SerializeField] GameObject tile = null;

    // Start is called before the first frame update
    void Start()
    {
        knotMap = new float[mapWidth,mapHeight];
        islandMap = new int[mapWidth,mapHeight];

        generateKnots();
        generateIslands();
        readIslandsgeneration();
        drawMap();
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
                if (islandMap[x,y] == 1)
                {
                    //Bei der Tile Erstellung muss die x, y Position und der Array für die Map übergeben werden
                    // nur diese garantiert das wir eine RuleTile bauen können die sich automatisch anpasst.
                    GameObject go = Instantiate<GameObject>(tile);
                    go.transform.position = new Vector3(x, y, 0);
                    go.GetComponent<SmartTile>().x = x;
                    go.GetComponent<SmartTile>().y = y;
                    go.GetComponent<SmartTile>().islandMap = islandMap;
                }
            }
        }
    }
}
