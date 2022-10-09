using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircleRenderer : MonoBehaviour
{
    [SerializeField]
    Transform target;

    LineRenderer LR;
    [SerializeField]
    float radius;
    
    void Start()
    {
        LR = GetComponent<LineRenderer>();
    }
    
    void Update()
    {
        DrawCircle(125, radius);
    }

    void DrawCircle(int steps, float radius)
    {
        LR.positionCount = steps + 1;

        for(int i = 0; i <= steps; i++)
        {
            float arc = (float)i / steps;

            float arcRadians = arc * 2 * Mathf.PI;

            float xScale = Mathf.Cos(arcRadians);
            float yScale = Mathf.Sin(arcRadians);

            float x = xScale * radius;
            float y = yScale * radius;

            Vector3 pos = new Vector3(x, y, 0);

            LR.SetPosition(i, pos + target.position);
        }
    }
}
