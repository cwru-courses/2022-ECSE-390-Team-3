using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Controller2D controller;
    private List<SpriteRenderer> sprites;

    GameManager GM;
    Latch latch;
    RotateToCursor RTC;

    [SerializeField]
    Animator playerAnim;

    // Remember v = a * t^2; gravity magnitude gets multiplied by deltaTime twice
    // Normal platformers force your Y to zero when grounded. This is a bit tricky with our current setup.
    // We can talk about different implementations but for now I'll be doing it like this.
    [SerializeField]
    [Tooltip("acceleration downwards due to gravity, stored as positive")]
    float gravityConstant = 2f;
    [SerializeField]
    [Tooltip("terminal falling due to gravity, should be low")]
    float terminalVelocity = 4f;
    [SerializeField]
    GameObject soundSources;

    [SerializeField]
    [Tooltip("min distance between cursor and player for rotation")]
    float minRadius;

    Vector3 cursorDir;

    Vector2 gravity;
    Vector2 waveImpulse;
    Vector2 currImpulse;
    // It's wind because current is... well, too similar to current. And flow is lame.
    Vector2 windVelocity;
    Vector2 dampedWindVelocity;
    Vector2 windVelocityRef;
    Vector2 currVelocity;
    Vector2 umbrVelocity;

    [SerializeField]
    float unlatchImpulse = 10f;
    Vector2 latchImpulse;
    Vector2 latchImpulseRef;

    // The final vector that gets passed to the controller
    Vector2 velocity;

    Vector3 spawnPoint;

    bool latched;
    bool latchJumping;
    float latchJumpDuration = 0.25f;
    float latchJumpTimer;

    private bool frozen = false;

    private Transform pivot;
    
    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        latch = GetComponentInChildren<Latch>();
        RTC = GetComponentInChildren<RotateToCursor>();

        controller = GetComponent<Controller2D>();
        pivot = GameObject.Find("Pivot").transform;

        sprites = new List<SpriteRenderer>();
        sprites.Add(GetComponent<SpriteRenderer>());

        foreach (Transform child in transform)
        {
            if (child.GetComponentInChildren<SpriteRenderer>() == null) continue;
            sprites.Add(child.GetComponentInChildren<SpriteRenderer>());
        }

        spawnPoint = transform.position;
    }

    void Update()
    {
        if (frozen) return;

        if (GM.UmbrellaOpen()) gravity.y -= (gravityConstant * Time.deltaTime);
        else gravity.y = (controller.IsGrounded() || latchJumping) ? 0f : gravity.y - (gravityConstant * Time.deltaTime);

        gravity.y = Mathf.Max(-terminalVelocity, gravity.y);

        waveImpulse = Vector2.SmoothDamp(waveImpulse, Vector2.zero, ref currImpulse, 0.5f);
        latchImpulse = Vector2.SmoothDamp(latchImpulse, Vector2.zero, ref latchImpulseRef, 0.25f);

        if (latchJumpTimer <= latchJumpDuration)
        {
            latchJumpTimer += Time.deltaTime;
            dampedWindVelocity = Vector2.zero;
        }
        else latchJumping = false;

        // test parameters
        // windvelocity == 1, damptime = 0
        // windvelocity == 20, damptime = 0.3
        // damptime = 0.3/20
        dampedWindVelocity = Vector2.SmoothDamp(dampedWindVelocity, windVelocity, ref windVelocityRef, 0.015f * windVelocity.magnitude);

        velocity = (latchJumping) ? latchImpulse + waveImpulse: gravity + waveImpulse + latchImpulse + dampedWindVelocity + umbrVelocity;

        // Debug.DrawRay(transform.position, gravity, Color.green);
        // Debug.DrawRay(transform.position, windVelocity, Color.blue);
        // Debug.DrawRay(transform.position, umbrVelocity, Color.magenta);
        // Debug.DrawRay(transform.position, latchImpulse * 2f, Color.yellow);
        // Debug.DrawRay(transform.position, velocity, Color.red);
    }

    public void ApplyImpulse(Vector2 _impulse)
    {
        waveImpulse = _impulse;
        GM.Shake(_impulse);
    }

    public void ApplyWind(Vector2 dir, float magnitude)
    {
        windVelocity = dir * magnitude;
    }

    public void ApplyUmbrella(Vector2 _velocity)
    {
        umbrVelocity = _velocity;
    }

    public void Latch() { 
        latched = true; 
        AudioSource latchSound = soundSources.transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        latchSound.PlayOneShot(latchSound.clip);
    }

    public void Unlatch() {
        latched = false;
        ApplyLatchImpulse();
        AudioSource latchSound = soundSources.transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        latchSound.PlayOneShot(latchSound.clip);
    }

    private void ApplyLatchImpulse()
    {
        latchJumpTimer = 0f;
        latchJumping = true;
        latchImpulse = Quaternion.AngleAxis(pivot.eulerAngles.z, Vector3.forward) * Vector2.up * unlatchImpulse;
        // Debug.DrawRay(transform.position, latchImpulse, Color.blue, 2f);

    }

    public void SetGravity(Vector2 _gravity)
    {
        gravity = _gravity;
    }

    // Handles character rotation and rotates final velocity vector
    /*
    void RotateToCursor()
    {
        float cameraToScreenRatio = (float)Camera.main.pixelHeight / (float)Screen.height;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition * cameraToScreenRatio);
        mousePos.z = 0f;

        cursorDir = mousePos - transform.position;

        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, cursorDir);

        bool rotate = (mousePos - transform.position).magnitude > minRadius;

        if (rotate) transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotSpd * Time.deltaTime);

        velocity = Quaternion.AngleAxis(transform.eulerAngles.z, Vector3.back) * velocity;
    }
    */

    public void EnableRenderer()
    {
        foreach (SpriteRenderer SR in sprites)
        {
            SR.enabled = true;
        }
    }

    public void DisableRenderer()
    {
        foreach(SpriteRenderer SR in sprites)
        {
            SR.enabled = false;
        }
    }

    // another hacky hack
    // with more find hacks because why not
    public void SetColorWhite()
    {
        foreach(SpriteRenderer SR in sprites)
        {
            SR.material.shader = Shader.Find("GUI/Text Shader");
        }
    }

    public void ResetColor()
    {
        foreach(SpriteRenderer SR in sprites)
        {
            SR.material.shader = Shader.Find("Sprites/Default");
        }
    }

    public void Respawn()
    {
        latched = false;
        latch.Reset();
        ResetVelocities();
        transform.position = spawnPoint;
    }

    private void ResetVelocities()
    {
        velocity =
        gravity =
        waveImpulse =
        latchImpulse = 
        latchImpulseRef =
        currImpulse = 
        windVelocity =
        dampedWindVelocity =
        windVelocityRef =
        umbrVelocity = Vector2.zero;
        latchJumping = false;
    }

    public void ResetGravity()
    {
        gravity = Vector2.zero;
    }

    public float GetGravity()
    {
        return gravityConstant;
    }

    public float GetCurrentGravity()
    {
        return Mathf.Abs(gravity.y);
    }

    public void SetSpawnPoint(Vector3 point)
    {
        spawnPoint = point;
    }

    public void SetFreeze(bool _frozen)
    {
        frozen = _frozen;
        latch.enabled = _frozen;
        RTC.enabled = _frozen;
    }

    public void bonkedBoss(Vector2 bounce)
    {
        controller.Move(bounce * Time.deltaTime);
    }

    private void LateUpdate()
    {
        if (latched) ResetVelocities();

        controller.Move(velocity * Time.deltaTime);
        Debug.DrawRay(transform.position, velocity * 0.25f, Color.green);
    }
}