using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenChanger : MonoBehaviour
{
    GameManager GM;

    Vector3 respawnPoint;
    Vector3 lastRespawnPoint;

    BoxCollider2D enteredBox;
    BoxCollider2D exitedBox;
    BoxCollider2D lastEnteredBox;

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        CameraBehavior cam = Camera.main.GetComponent<CameraBehavior>();
        if (GM.transform.GetChild(2).GetChild(0).GetComponentInChildren<BoxCollider2D>() != null) cam.UpdateScreenBounds(GM.transform.GetChild(2).GetChild(0).GetComponentInChildren<BoxCollider2D>());
        cam.UpdateScreen();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Screen"))
        {
            lastEnteredBox = enteredBox;
            enteredBox = collision.gameObject.GetComponent<BoxCollider2D>();

            CameraBehavior cam = Camera.main.GetComponent<CameraBehavior>();
            cam.UpdateScreenBounds(enteredBox);
            cam.UpdateScreen();

            // if (collision.gameObject.transform.childCount == 0) return;
            // respawnPoint = collision.gameObject.transform.GetChild(0).transform.position;
            // GM.SetPlayerSpawn(respawnPoint);
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Screen"))
        {
            exitedBox = collision.gameObject.GetComponent<BoxCollider2D>();

            if(lastEnteredBox != exitedBox)
            {
                if (lastEnteredBox == null) return;
                CameraBehavior cam = Camera.main.GetComponent<CameraBehavior>();
                cam.UpdateScreenBounds(lastEnteredBox);
                cam.UpdateScreen();

                enteredBox = lastEnteredBox;
                lastEnteredBox = exitedBox;
            }
        }
    }
}
