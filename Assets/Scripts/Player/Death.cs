using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    GameManager GM;
    public AudioSource deathSound;
    Camera cam;
    private float deathTime = 0.175f;


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
        yield return new WaitForSecondsRealtime(deathTime);
        Time.timeScale = 1f;
        GM.PlayerDeath();
        SpeedrunStats.playerDeath();
        deathSound.PlayOneShot(deathSound.clip);

        // CB.ToggleScreenLock(true);
    }
}
