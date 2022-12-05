using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBloodCell : MonoBehaviour
{
    public Animator anim;
    public Transform[] patrolPoints;
    private bool bonked;
    private int pointIndex = 0;
    private GameObject gameManager;
    public SpriteRenderer sr;
    public Material flash;
    public Material spriteDefault;
    public GameObject door;
    public GameObject bossDripPrefab;
    public GameObject[] organs;
    Camera cam;
    private float bossHitPauseTime = 0.125f;
    private CameraBehavior CB;
    public bool isFinal;
    public AudioManager AM;


    // Start is called before the first frame update
    void Start()
    { 
        AM = FindObjectOfType<AudioManager>();
        cam = Camera.main;
        CB = cam.GetComponent<CameraBehavior>();
        gameManager = GameObject.Find("GameManager");
        //pointIndex = patrolPoints.Length - 2;
    }

    // Update is called once per frame

    void Update()
    {

        if (bonked && anim.GetCurrentAnimatorStateInfo(0).IsName("wbc swim"))
        {
            swim();
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("white blood cell idle"))
        {
            transform.eulerAngles = new Vector3(0, 0, pointIndex * 90);
        }

        //jank
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("wbc bonk") && patrolPoints.Length - 1 != pointIndex)
        {
            transform.eulerAngles = new Vector3(0, 0, (pointIndex * 90) - 90);
        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player" && anim.GetCurrentAnimatorStateInfo(0).IsName("white blood cell idle"))
        {
            if(AM != null){
                AM.Play("bonk");
            }
            harmed();

            if (patrolPoints.Length - 1 == pointIndex)
            {
                dead();
            }
            else
            {
                if (isFinal)
                {
                    StartCoroutine(rotateDelay(0.7f));
                    
                }
                else
                {
                    Debug.Log("rgrg");
                    CB.stopCam = true;
                    //unfreezeCamera(3f);

                }

                if (pointIndex < patrolPoints.Length - 1) pointIndex++;
                bonked = true;

            }

            if (pointIndex == 1) FindObjectOfType<Player>().bonkedBoss(new Vector2(0, 200));
            else if (pointIndex == 2) FindObjectOfType<Player>().bonkedBoss(new Vector2(200, 0));
            else if (pointIndex == 3) FindObjectOfType<Player>().bonkedBoss(new Vector2(0, -200));
            else if (pointIndex == 4) FindObjectOfType<Player>().bonkedBoss(new Vector2(-200, 0));
            else if (pointIndex == 5) FindObjectOfType<Player>().bonkedBoss(new Vector2(0, -200));
            collision.gameObject.GetComponentInParent<Player>();

        }

    }

    void harmed()
    {
        sr.material = flash;
        StartCoroutine(flashDelay(0.2f));
        anim.SetBool("bonked", true);
        anim.SetBool("unbonk", false);
    }

    void dead()
    {
        CB.stopCam = true;
        anim.SetBool("dead", true);
        StartCoroutine(bossDrip(2.75f));
        StartCoroutine(unfreezeCamera(5f));
        gameManager.GetComponent<GameManager>().OnKeyGet(null, door);
    }

    void swim()
    {
        Vector3 vectorToTarget = patrolPoints[pointIndex].position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 20);

        float currentAnim = anim.GetCurrentAnimatorStateInfo(0).normalizedTime % anim.GetCurrentAnimatorStateInfo(0).length;

        if (currentAnim > 0f && currentAnim <= 0.3f)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[pointIndex].position, 5 * Time.deltaTime);

        }
        else if (currentAnim > 0.3f && currentAnim <= 0.615f)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[pointIndex].position, 25 * Time.deltaTime);
        }
        else if (currentAnim > 0.615f && currentAnim <= 0.8f)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[pointIndex].position, 18 * Time.deltaTime);
        }
        else
        {

            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[pointIndex].position, 15 * Time.deltaTime);

        }



        if (Vector2.Distance(transform.position, patrolPoints[pointIndex].position) < .002f)
        {
            anim.SetBool("unbonk", true);
            anim.SetBool("bonked", false);

        }

    }

    IEnumerator flashDelay(float time)
    {
        yield return new WaitForSeconds(time);
        sr.material = spriteDefault;

    }

    IEnumerator rotateDelay(float time)
    {
        yield return new WaitForSeconds(time);
        gameManager.GetComponent<GameManager>().Rotate90();

    }

    IEnumerator bossDrip(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(bossDripPrefab, new Vector3(this.transform.position.x, this.transform.position.y - 5, this.transform.position.z), Quaternion.identity);
        GetComponent<Renderer>().enabled = false;
        Instantiate(organs[0], this.transform.position, Quaternion.identity);
        Instantiate(organs[1], this.transform.position, Quaternion.identity);
        Instantiate(organs[2], this.transform.position, Quaternion.identity);
      
    }

    IEnumerator unfreezeCamera(float time)
    {
        yield return new WaitForSeconds(time);
        CB.stopCam = false;
    
    }
    }

