using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathFind;

public class ShipControllScript : MonoBehaviour
{
    MapScript mapScript;

    int posX;
    int posY;
    int tarX;
    int tarY;

    List<Point> wayToTarget;
    public float nextWayPointX;
    public float nextWayPointY;
    public bool nextWayPoint = false;

    // Start is called before the first frame update
    void Start()
    {
        mapScript = GameObject.Find("MapController").GetComponent<MapScript>();
        wayToTarget = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                tarX = hit.collider.gameObject.GetComponent<TileScript>().x;
                tarY = hit.collider.gameObject.GetComponent<TileScript>().y;

                Debug.Log("MousePos x: " + tarX + " y: " + tarY);

                wayToTarget = mapScript.Astar(posX, posY, tarX, tarY);
            }
        }

        if (wayToTarget != null)
        {
            if (wayToTarget.Count == 0)
            {
                wayToTarget = null;
                nextWayPoint = false;
            }
            else if (nextWayPoint == false)
            {
                nextWayPointX = wayToTarget[0].x;
                nextWayPointY = wayToTarget[0].y;
                nextWayPoint = true;
            }
            else if (Vector3.Distance(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(nextWayPointX, nextWayPointY, 0)) <= 0.01f)
            {
                posX = wayToTarget[0].x;
                posY = wayToTarget[0].y;
                wayToTarget.RemoveAt(0);
                nextWayPoint = false;
            }
            else if (Vector3.Distance(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(nextWayPointX, nextWayPointY, 0)) >= 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(nextWayPointX, nextWayPointY, 0), Time.deltaTime);
            }
        }
    }

}
