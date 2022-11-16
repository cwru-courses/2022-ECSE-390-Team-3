using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{

    public GameObject door;
    private AudioManager AM;
    private GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        AM = FindObjectOfType<AudioManager>();
        GM = FindObjectOfType<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("get the key");
            if(AM != null) {
                AM.Play("doorOpen");
            }

            GM.OnKeyGet(this.gameObject, door);
        }
    }
}
