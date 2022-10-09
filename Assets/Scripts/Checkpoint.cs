using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameManager GM;
    Transform spawnPoint;

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnPoint = gameObject.transform.GetChild(0).transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GM.SetPlayerSpawn(spawnPoint.position);
        }
    }
}
