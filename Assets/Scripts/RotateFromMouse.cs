using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFromMouse : MonoBehaviour
{
    [SerializeField]
    [Tooltip("max degrees rotated per second")]
    float rotSpd = 720;

    private Quaternion rotation;

    void Update()
    {
        float cameraToScreenRatio = (float)Camera.main.pixelHeight / (float)Screen.height;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition * cameraToScreenRatio);
        mousePos.z = 0f;

        Vector3 lineOfSight = mousePos - transform.position;

        rotation = Quaternion.LookRotation(Vector3.forward, lineOfSight);      
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotSpd * Time.deltaTime);
    }
}
