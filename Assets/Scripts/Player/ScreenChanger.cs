using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenChanger : MonoBehaviour
{
    [SerializeField]
    GameManager GM;

    Vector3 respawnPoint;
    Vector3 lastRespawnPoint;

    BoxCollider2D enteredBox;
    BoxCollider2D exitedBox;
    BoxCollider2D lastEnteredBox;

    private void Start()
    {
        
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

            if (collision.gameObject.transform.childCount == 0) return;
            respawnPoint = collision.gameObject.transform.GetChild(0).transform.position;
            GM.SetPlayerSpawn(respawnPoint);
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Screen"))
        {
            exitedBox = collision.gameObject.GetComponent<BoxCollider2D>();

            if(lastEnteredBox != exitedBox)
            {
                CameraBehavior cam = Camera.main.GetComponent<CameraBehavior>();
                cam.UpdateScreenBounds(lastEnteredBox);
                cam.UpdateScreen();

                enteredBox = lastEnteredBox;
                lastEnteredBox = exitedBox;
            }
        }
    }
}