using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    Controller2D controller;

    List<Wind> winds;
    Vector2 windVelocity;
    Vector2 currVelocity;

    void Start()
    {
        winds = new List<Wind>();
    }

    void Update()
    {
        if(winds.Count != 0)
        {
            Vector2 targetVelocity = Vector2.zero;

            foreach (Wind wind in winds)
            {
                targetVelocity += wind.GetVelocity();
            }

            windVelocity = Vector2.SmoothDamp(windVelocity, targetVelocity, ref currVelocity, 0.1f);
        }
        else
        {
            windVelocity = Vector2.SmoothDamp(windVelocity, Vector2.zero, ref currVelocity, 0.25f);
        }

        Debug.DrawRay(Vector3.zero, windVelocity.normalized * 10f);

        player.ApplyWind(windVelocity.normalized, windVelocity.magnitude);
    }

    public void AddWind(Wind wind)
    {
        winds.Add(wind);
    }

    public void RemoveWind(Wind wind)
    {
        winds.Remove(wind);
    }
}
