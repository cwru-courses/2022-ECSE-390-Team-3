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
    List<Vector2> windVelocities;
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
                wind.SetCurrVelocity(Vector2.SmoothDamp(wind.GetCurrVelocity(), wind.GetVelocity(), ref wind.GetRefVelocity(), 0.2f));

                targetVelocity += wind.GetCurrVelocity();
            }

            windVelocity = Vector2.SmoothDamp(windVelocity, targetVelocity, ref currVelocity, 0.2f);
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
        wind.SetCurrVelocity(Vector2.zero);
        winds.Remove(wind);
    }
}
