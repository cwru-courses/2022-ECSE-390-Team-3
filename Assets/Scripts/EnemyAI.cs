using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyAI : MonoBehaviour
{

    //waypoints
    public List<Transform> points;
    //next id index
    public int nextID = 0;
    //the value of changing for ID
    int change = 1;
    public float speed = 2;

    private void Reset()
    {
        Init();
    }


    void Init()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;

        //create a root and set it as the enemy's parent
        GameObject root = new GameObject(name + "_root");
        root.transform.position = transform.position;
        transform.SetParent(root.transform);

        //create waypoints object and make it child of the root
        GameObject waypoints = new GameObject(name + "_WayPoints");
        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position = root.transform.position;

        //create two waypoints (basic two)
        GameObject p1 = new GameObject("Point1");
        p1.transform.SetParent(waypoints.transform);
        p1.transform.position = waypoints.transform.position;
        GameObject p2 = new GameObject("Point2");
        p2.transform.SetParent(waypoints.transform);
        p2.transform.position = waypoints.transform.position;

        points = new List<Transform>();
        points.Add(p1.transform);
        points.Add(p2.transform);
    }

    // Update is called once per frame
    void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        Transform goalPoint = points[nextID];
        if(goalPoint.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, goalPoint.position) < 0.2f)
        {
            if(nextID == points.Count - 1)
            {
                change = -1;
            }
            if(nextID == 0)
            {
                change = 1;
            }
            nextID += change;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("Triggered");
            PlayerDies();
        }
    }

    private void PlayerDies()
    {
        RestartLevel();
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
