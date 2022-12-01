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
            float percentDone = timer / duration;
            pieces[i].position += (Vector3)directions[i] * 12f * (Mathf.Pow(percentDone - 1, 2) * 0.9f + 0.1f) * Time.deltaTime;
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
            pieces[i].transform.Rotate(0, 0, Random.Range(0f, 360f));
            directions[i] = (pieces[i].position - transform.position).normalized;
        }

        duration = time;
        timer = 0f;
    }
}
