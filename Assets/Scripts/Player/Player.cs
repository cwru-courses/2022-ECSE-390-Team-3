using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Controller2D controller;

    [SerializeField]
    [Tooltip("max degrees rotated per second")]
    float rotSpd = 720;

    // Remember v = a * t^2; gravity magnitude gets multiplied by deltaTime twice
    // Normal platformers force your Y to zero when grounded. This is a bit tricky with our current setup.
    // We can talk about different implementations but for now I'll be doing it like this.
    [SerializeField]
    [Tooltip("acceleration downwards due to gravity, stored as positive")]
    float gravityConstant = 2f;
    [SerializeField]
    [Tooltip("reduces the effect of gravity when in upwards winds")]
    float gravityModifier = 0.25f;
    float gravityMod;
    [SerializeField]
    [Tooltip("terminal falling due to gravity, should be low")]
    float terminalVelocity = 4f;

    // Temporary flag system
    bool gravityOn;
    [SerializeField]
    [Tooltip("enable/disable full WASD movement")]
    bool inputOn;

    Vector3 cursorDir;

    // We'll independently calculate a number of movement vectors
    // which we'll pass into velocity at the end.
    // This includes gravity, which really doesn't need to be one
    // but we're doing it for consistency.
    // When modifying vectors, we're interpreting them as axis-aligned
    // and they will be rotated at the end to account for player rotation.

    // Input is purely for debug movement
    Vector2 input;

    Vector2 gravity;
    Vector2 waveImpulse;
    Vector2 currImpulse;
    // It's wind because current is... well, too similar to current. And flow is lame.
    Vector2 windVelocity;
    Vector2 currVelocity;
    Vector2 umbrVelocity;

    // The final vector that gets passed to the controller
    Vector2 velocity;
    
    void Start()
    {
        controller = GetComponentInChildren<Controller2D>();
        gravityOn = !inputOn;
    }

    void Update()
    {
        if(inputOn)
        {
            input.x = Input.GetAxisRaw("Horizontal") * 7.5f;
            input.y = Input.GetAxisRaw("Vertical") * 7.5f;
            if (input == Vector2.zero) gravityOn = true;
            else gravityOn = false;
        }

        if(gravityOn)
        {
            gravity.y = (controller.IsColliding()) ? gravityConstant : gravity.y - (gravityConstant * Time.deltaTime);
            gravity.y = Mathf.Max(-terminalVelocity, Mathf.Min(0f, gravity.y));
            gravity.y *= gravityMod;
        }

        waveImpulse = Vector2.SmoothDamp(waveImpulse, Vector2.zero, ref currImpulse, 0.5f);

        if (windVelocity.y >= 0.05 && gravity.y < 0) gravityMod = gravityModifier;
        else gravityMod = 1;

        // Pass all calculated vectors to velocity
        velocity = input + gravity + waveImpulse + windVelocity + umbrVelocity;

        // Perform rotation
        RotateToCursor();
    }

    // Pass a normalized vector for direction,
    // and a float for the magnitude.
    // It will be necessary to ensure the vector waveImpulse is only passed to velocity once
    // no reason to put this in separate method im just in a software craftsmanship mood today
    // the idea is to handle it from somewhere else
    public void ApplyImpulse(Vector2 _impulse)
    {
        waveImpulse = _impulse;
    }

    // Pass a normalized vector for direction,
    // and a float for the magnitude.
    public void ApplyWind(Vector2 dir, float magnitude)
    {
        windVelocity = dir * magnitude;
    }

    public void ApplyUmbrella(Vector2 _velocity)
    {
        umbrVelocity = _velocity;
    }

    public void SetGravityOn(bool what)
    {
        gravityOn = what;
    }

    // Handles character rotation and rotates final velocity vector
    void RotateToCursor()
    {
        float cameraToScreenRatio = (float)Camera.main.pixelHeight / (float)Screen.height;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition * cameraToScreenRatio);
        mousePos.z = 0f;

        cursorDir = mousePos - transform.position;

        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, cursorDir);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotSpd * Time.deltaTime);

        velocity = Quaternion.AngleAxis(transform.eulerAngles.z, Vector3.back) * velocity;
    }

    private void LateUpdate()
    {
        controller.Move(velocity * Time.deltaTime);
    }
}