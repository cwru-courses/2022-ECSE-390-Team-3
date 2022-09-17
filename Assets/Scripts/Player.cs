using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Controller2D controller;

    [SerializeField]
    [Tooltip("max degrees rotated per second")]
    float rotSpd = 720;

    Vector2 input;
    Vector2 move;
    
    void Start()
    {
        controller = GetComponentInChildren<Controller2D>();
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal") * 7.5f;
        input.y = Input.GetAxisRaw("Vertical") * 7.5f;
        RotateToMouse();
    }

    void RotateToMouse()
    {
        float cameraToScreenRatio = (float)Camera.main.pixelHeight / (float)Screen.height;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition * cameraToScreenRatio);
        mousePos.z = 0f;

        Vector3 lineOfSight = mousePos - transform.position;

        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, lineOfSight);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotSpd * Time.deltaTime);

        move = Quaternion.AngleAxis(transform.eulerAngles.z, Vector3.back) * input;
    }

    private void LateUpdate()
    {
        controller.Move(move * Time.deltaTime);
    }
}