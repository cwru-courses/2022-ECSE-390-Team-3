using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBloodCell : MonoBehaviour
// TODO: implement Lunge(), IsPlayerLatched(), probably fix the if else block in Update() to use coroutines or something 
{
    SpriteRenderer sr;
    private float Delay;
    private Color startingColor;
    private Color c;
    BoxCollider2D box;
    public bool isLatchedTest = true;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        startingColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlayerLatched()){
            ComeIntoBeing();
            if(box.enabled){
                Lunge();
            }
        }
        else{
            if(box.enabled){
                FadeIntoTheAbyss();
            }
        }
        
    }

    // checks if the player is latched; true = player is latched
    bool IsPlayerLatched()
    {
        // check if player is latched
        return isLatchedTest; // for testing purposes
    }

    // makes the sprite renderer go from clear to its starting color over a period of time
    void ComeIntoBeing(){
        Delay += Time.deltaTime;
        sr.color = Color.Lerp(Color.clear, startingColor, Delay * 0.5f);
        if(sr.color == startingColor){
            box.enabled = true;
        }
    }

    // makes the sprite renderer go from clear to its starting color over a period of time
    void FadeIntoTheAbyss(){
        Delay += Time.deltaTime;
        sr.color = Color.Lerp(startingColor, Color.clear, Delay * 0.5f);
        if(sr.color == Color.clear){
            box.enabled = false;
        }
    }

    // lunges at the player
    void Lunge(){
     Debug.Log("I am lunging at you now");
    }


}
