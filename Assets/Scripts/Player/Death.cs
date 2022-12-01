using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    GameManager GM;
    public AudioSource deathSound;
    Camera cam;
    private float deathPauseTime = 0.125f;


    private void Start()
    {
        cam = Camera.main;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>(); ;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard") || collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(freezeOnDeath());
        }
    }

    // Freezes the camera on death
    IEnumerator freezeOnDeath() {

        CameraBehavior CB = cam.GetComponent<CameraBehavior>();
        CB.stopCam = true;

        Time.timeScale = 0f;

        // time for another super hacky hack
        Player player = GetComponentInParent<Player>();
        player.SetColorWhite();

        yield return new WaitForSecondsRealtime(deathPauseTime);
        Time.timeScale = 1f;

        player.ResetColor();
        GM.PlayerDeath();
        SpeedrunStats.playerDeath();
        deathSound.PlayOneShot(deathSound.clip);

        // CB.ToggleScreenLock(true);
    }
}
