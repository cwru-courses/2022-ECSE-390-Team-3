using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class GreenEnemyAI : MonoBehaviour
{

    //waypoints
    public List<Transform> points;
    //next id index
    public int nextID = 0;
    public AudioSource audioSource;
    //the value of changing for ID
    int change = 1;
    public float speed = 2;
    public float maxVolume = 5f;

    private bool frozen = false;

    GameObject target;

    float distance;

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

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (frozen) return;
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        Transform goalPoint = points[nextID];
        if (goalPoint.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, goalPoint.position) < 0.2f)
        {
            playAudio();
            if (nextID == points.Count - 1)
            {
                change = -1;
            }
            if (nextID == 0)
            {
                change = 1;
            }
            nextID += change;
        }
    }
    public void SetFreeze(bool _frozen)
    {
        frozen = _frozen;
    }

    public void playAudio()
    {
        distance = Vector3.Distance(target.transform.position, transform.position);
        audioSource.PlayOneShot(audioSource.clip, maxVolume * (2 / distance));
    }
}
