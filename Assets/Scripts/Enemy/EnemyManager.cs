using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<Transform> enemies;

    void Start()
    {
        enemies = new List<Transform>();

        foreach(Transform enemy in transform)
        {
            enemies.Add(enemy);
        }
    }

    public void SetFreeze(bool frozen)
    {
        foreach(Transform enemy in enemies)
        {
            if(enemy.GetComponentInChildren<EnemyAI>() != null) enemy.GetComponentInChildren<EnemyAI>().SetFreeze(frozen);
            if (enemy.GetComponentInChildren<EnemyProjectile>() != null) enemy.GetComponentInChildren<EnemyProjectile>().SetFreeze(frozen);
        }
    }

    public void Reset()
    {
        // something
    }
}
