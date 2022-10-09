using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Player player;
    Controller2D controller;

    List<Wind> winds;
    Vector2 windVelocity;
    Vector2 currVelocity;

    bool umbrellaOpen;
    Vector2 glideVelocity;

    Vector2 waveImpulse;

    bool respawning;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        controller = player.GetComponentInChildren<Controller2D>();
        winds = new List<Wind>();
    }

    void Update()
    {
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
        Camera.main.GetComponent<CameraBehavior>().ZoomOut();
        //SetFreeze(true);
    }

    public void Unlatch()
    {
        if (respawning) return;
        player.Unlatch();
        Camera.main.GetComponent<CameraBehavior>().Unzoom();
    }

    public void PlayerDeath()
    {
        StartCoroutine(Death(0.5f));
    }

    public void SetPlayerSpawn(Vector3 point)
    {
        player.SetSpawnPoint(point);
    }

    private void SetFreeze(bool frozen)
    {
        player.enabled = !frozen;
        controller.enabled = !frozen;
    }

    public Vector2 GetCurrentWindDirection()
    {
        return winds.Last().GetVelocity().normalized;
    }

    IEnumerator Death(float respawnTime)
    {
        respawning = true;

        player.DisableRenderer();
        SetFreeze(true);
        yield return new WaitForSeconds(respawnTime);
        SetFreeze(false);

        player.EnableRenderer();
        player.Respawn();
        respawning = false;
        yield return null;
    }

    public Transform GetPlayer()
    {
        return player.GetComponent<Transform>();
    }
}
