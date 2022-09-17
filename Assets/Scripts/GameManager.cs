using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    Controller2D controller;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PushPlayer(Vector2 dir, float force)
    {
        // this is really really bad and is really only here for proof of concept
        // 1. it completely resets the player's movement vector, causing jank
        // 2. it has to be done this way because it's called constantly while the player is inside an active zone
        // 3. this is really bad
        //
        // using the true wave implementation would work much nicer with the character controller,
        // ie.,
        // upon the umbrella opening inside of a wave,
        // have rigidbody on umbrella, wave.script has OnCollisionEnter
        // check position inside wave collider and apply one-time push to player
        // based on umbrella angle (todo) and position inside wave
        // to prevent players from just holding umbrella right in front of approaching wave,
        // maybe add a timer to when it's "valid" or just allow players to do so
        // we'll figure it out
        controller.Move(dir * force * Time.deltaTime);
    }
}
