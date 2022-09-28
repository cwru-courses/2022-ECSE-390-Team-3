using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Wave : MonoBehaviour
{
    BoxCollider2D box;

    private float speed, maxImpulse, distance;
    private Vector2 start, end;
    private Vector2 trailingEdge, leadingEdge;

    void Update()
    {
        bool finish = ((Vector2)transform.position - start).magnitude >= distance;
        if (finish) transform.position = start;

        //Debug.DrawRay(transform.position, dir, Color.blue);
        //Debug.DrawLine(start, end, Color.yellow);

        Quaternion rotation = Quaternion.AngleAxis(transform.eulerAngles.z, Vector3.forward);
        trailingEdge = box.bounds.center + (rotation * Vector2.left * box.size.x / 2);
        leadingEdge = box.bounds.center + (rotation * Vector2.right * box.size.x / 2);
        Debug.DrawLine(trailingEdge, leadingEdge, Color.red);
    }

    void LateUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public void SetDimensions(float width, float height)
    {
        box = GetComponent<BoxCollider2D>();
        box.size = new Vector2(width, height);
    }

    public void SetRotation(float angle)
    {
        transform.localRotation = Quaternion.AngleAxis(angle, transform.forward);
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    public void SetMaxImpulse(float _maxImpulse)
    {
        maxImpulse = _maxImpulse;
    }

    public void SetPath(Vector2 _start, Vector2 _end)
    {
        start = _start;
        end = _end;

        distance = (end - start).magnitude;

        transform.position = _start;
    }

    public float GetMaxImpulse()
    {
        return maxImpulse;
    }

    public Vector2 GetDirection()
    {
        return (end - start).normalized;
    }

    public float GetPositionPercentage(Vector2 pos)
    {
        Vector2 projectedPos = Vector3.Project((pos - trailingEdge), (leadingEdge - trailingEdge)) + (Vector3)trailingEdge;
        return (projectedPos - trailingEdge).magnitude / (leadingEdge - trailingEdge).magnitude;
    }
}
