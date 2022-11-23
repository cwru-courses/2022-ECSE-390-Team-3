using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpse : MonoBehaviour
{
    Transform[] pieces;
    Vector2[] directions;
    private float duration;
    private float timer;

    void Update()
    {
        if (timer >= duration)
        {
            Destroy(this.gameObject);
        }

        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].position += (Vector3)directions[i] * 3f * Time.deltaTime;
        }

        timer += Time.deltaTime;
    }

    public void Scatter(float time)
    {
        pieces = new Transform[transform.childCount];
        directions = new Vector2[pieces.Length];

        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i] = transform.GetChild(i);
            directions[i] = (pieces[i].position - transform.position).normalized;
        }

        duration = time;
        timer = 0f;
    }
}
