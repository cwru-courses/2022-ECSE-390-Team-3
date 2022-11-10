using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{

    public GameObject door;
    private AudioManager AM;

    // Start is called before the first frame update
    void Start()
    {
        AM = FindObjectOfType<AudioManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("get the key");
            if(AM != null) {
                AM.Play("doorOpen");
            }
            Destroy(this.gameObject);
            Destroy(door);
        }
    }
}
