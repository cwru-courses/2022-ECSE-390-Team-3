using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Player player;
    Controller2D controller;
    Camera cam;
    EnemyManager EM;

    List<Wind> winds;
    Vector2 windVelocity;
    Vector2 currVelocity;

    bool umbrellaOpen;
    Vector2 glideVelocity;

    Vector2 waveImpulse;

    bool respawning = false;
    AudioManager AM;

    bool rotating = false;

    bool frozen = false;

    void Start()
    {
        AM = FindObjectOfType<AudioManager>();
        if(AM != null) {
            AM.Stop("briansTheme");
            AM.Play("puzzlingTheme");
        }
        EM = FindObjectOfType<EnemyManager>();
        cam = Camera.main;
        player = GameObject.Find("Player").GetComponent<Player>();       
        controller = player.GetComponentInChildren<Controller2D>();
        winds = new List<Wind>();
    }

    void Update()
    {
        if (respawning || frozen) return;

        if(winds.Count != 0)
        {
            Vector2 targetVelocity = Vector2.zero;

            foreach (Wind wind in winds)
            {
                wind.SetCurrVelocity(Vector2.SmoothDamp(wind.GetCurrVelocity(), wind.GetVelocity(), ref wind.GetRefVelocity(), 0.2f));

                targetVelocity += wind.GetCurrVelocity();
            }

            windVelocity = Vector2.SmoothDamp(windVelocity, targetVelocity, ref currVelocity, 0.2f);
        }
        else
        {
            windVelocity = Vector2.SmoothDamp(windVelocity, Vector2.zero, ref currVelocity, 0.05f);
        }

        Debug.DrawRay(player.transform.position, windVelocity.normalized * 10f, Color.white);

        player.ApplyWind(windVelocity.normalized, windVelocity.magnitude);

        if(umbrellaOpen)
        {
            player.ApplyUmbrella(glideVelocity);
        }
        else
        {
            player.ApplyUmbrella(Vector2.zero);
        }

        // if(Input.GetKeyDown(KeyCode.P))
        // {
        //     StartCoroutine(RotateWorldClockwise90());
        // }
    }

    public void Rotate90()
    {
        StartCoroutine(RotateWorldClockwise90());
    }

    public void AddWind(Wind wind)
    {
        winds.Add(wind);
    }

    public void RemoveWind(Wind wind)
    {
        wind.SetCurrVelocity(Vector2.zero);
        winds.Remove(wind);
    }

    public void SetUmbrellaStatus(bool _umbrellaOpen)
    {
        umbrellaOpen = _umbrellaOpen;
    }

    public void SetUmbrellaVelocity(Vector2 _glideVelocity)
    {
        glideVelocity = _glideVelocity;
    }

    public bool UmbrellaOpen()
    {
        return umbrellaOpen;
    }

    public void Latch()
    {
        if (respawning) return;
        player.Latch();
        cam.GetComponent<CameraBehavior>().ZoomOut();
        //SetFreeze(true);
    }

    public void Unlatch()
    {
        if (respawning) return;
        player.Unlatch();
        cam.GetComponent<CameraBehavior>().Unzoom();
    }

    public void Shake(Vector2 dir)
    {
        cam.GetComponent<CameraBehavior>().Shake(dir);
    }

    public void PlayerDeath()
    {
        StartCoroutine(Death(0.5f));
    }

    public void SetPlayerSpawn(Vector3 point)
    {
        player.SetSpawnPoint(point);
    }

    public void SetFreeze(bool frozen)
    {
        player.enabled = !frozen;
        controller.enabled = !frozen;
    }

    public Vector2 GetCurrentWindDirection()
    {
        return winds.Last().GetVelocity().normalized;
    }

    public List<Wind> GetWinds()
    {
        return winds;
    }

    public void OnKeyGet(GameObject key, GameObject door)
    {
        StartCoroutine(WTFDoICallThis(key, door));
    }

    private void PrintWinds()
    {
        string windNames = "winds: ";

        foreach(Wind wind in winds)
        {
            windNames = windNames + wind.name + " ";
        }

        Debug.Log(windNames);
    }

    IEnumerator RotateWorldClockwise90()
    {
        if (rotating) yield break;

        rotating = true;
        // lock world entities
        // then

        float currentRotation = Mathf.Round(cam.transform.rotation.eulerAngles.z);
        float targetRotation = currentRotation + 90f;


        while(currentRotation < targetRotation)
        {
            cam.transform.Rotate(0f, 0f, 90f * Time.deltaTime);
            currentRotation += 90f * Time.deltaTime;
            yield return null;
        }
        cam.transform.eulerAngles = new Vector3(0f, 0f, targetRotation);
        rotating = false;

        //cam.transform.GetComponent<CameraBehavior>().Rotate();
        yield return null;
    }

    IEnumerator WTFDoICallThis(GameObject key, GameObject door)
    {
        // yeah no we're not refactoring everything we're going to pass a shit ton of references

        Destroy(key);

        // fuck this is ugly
        player.enabled = false;
        if (EM != null) EM.SetFreeze(true);

        CameraBehavior CB = cam.GetComponent<CameraBehavior>();

        // fuck why did i think requiring the camera to use transform was a good idea
        CB.ToggleScreenLock(false);
        CB.SetTarget(door.transform);

        while(((Vector2)(cam.transform.position - door.transform.position)).magnitude > 0.05f) yield return null;

        // this is how long the camera stares at the door
        yield return new WaitForSeconds(1f);

        // here is where you can play a door animation or something
        door.GetComponentInChildren<SpriteRenderer>().enabled = false;

        // this is how long the camera stares at the place where there is no more door
        yield return new WaitForSeconds(2f);

        CB.SetTarget(player.GetComponentInParent<Transform>());

        while (((Vector2)(cam.transform.position - player.transform.position)).magnitude > 0.05f) yield return null;

        CB.ToggleScreenLock(true);

        player.enabled = true;
        if (EM != null) EM.SetFreeze(false);

        Destroy(door);

        yield return null;
    }

    IEnumerator Death(float respawnTime)
    {
        CameraBehavior CB = cam.GetComponent<CameraBehavior>();

        respawning = true;

        // Freezes the camera on death
        player.DisableRenderer();
        SetFreeze(true);

        // is this a lazy hack? yes
        GameObject corpse = Instantiate(Resources.Load("Corpse", typeof(GameObject)), player.GetComponentInParent<Transform>().position, GameObject.Find("Pivot").transform.rotation) as GameObject;
        corpse.GetComponent<Corpse>().Scatter(respawnTime);

        yield return new WaitForSeconds(respawnTime);
        SetFreeze(false);
        CB.stopCam = false;

        player.EnableRenderer();
        player.Respawn();
        respawning = false;
        Unlatch();
        player.GetComponentInParent<Transform>().GetComponentInChildren<Animator>().SetBool("latchOn", false);
        windVelocity = Vector2.zero;
        currVelocity = Vector2.zero;
        yield return null;
    }

    public Transform GetPlayer()
    {
        return player.GetComponent<Transform>();
    }
}
