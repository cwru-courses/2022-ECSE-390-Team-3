using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNoRot : MonoBehaviour
{
    private Controller2DNoRot controller;

    Vector2 input;
    Vector2 velocity;

    void Start()
    {
        controller = GetComponentInChildren<Controller2DNoRot>();
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal") * 7.5f;
        input.y = Input.GetAxisRaw("Vertical") * 7.5f;

        velocity = input;
    }

    private void LateUpdate()
    {
        controller.Move(velocity * Time.deltaTime);
    }
}
