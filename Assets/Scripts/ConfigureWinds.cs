using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigureWinds : MonoBehaviour
{
    GameManager GM;
    List<Wind> winds;
    void Start()
    {
        GM = GetComponentInParent<GameManager>();

        foreach(Transform child in transform)
        {
            if (child.GetComponentInParent<Wind>() == null) continue;

            child.GetComponentInParent<Wind>().SetGameManager(GM);
        }
    }
}
