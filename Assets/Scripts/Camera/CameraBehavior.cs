using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField]
    Transform player;
    Vector2 currentPosition;
    Vector2 targetPosition;

    void Update()
    {
        targetPosition = Vector2.SmoothDamp(transform.position, player.position, ref currentPosition, 0.2f);
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(targetPosition.x, targetPosition.y, -10.0f);
    }
}
