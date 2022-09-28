using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingText : MonoBehaviour
{
    public Text storyText;
    public Text continueText;
    private string blinking;
    private string line;
    public string[] lines;
    public float textSpeed;
    private float timer;
    private float blink;
    private bool notStarted = true;
    private int flip = -1;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        storyText.text = "";
        flip = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(line == lines[index])
            {
                if(index == lines.Length-1)
                {
                    SceneController.LoadSpecificScene("Tutorial_L1");
                }    
                else
                {
                    NextLine();
                }
            }
            else
            {
                StopAllCoroutines();
                line = lines[index];
            }
        }

        timer += Time.deltaTime;

        storyText.text = line + blinking;

        if (timer > 3 && notStarted)
        {
            beginCutscene();
            notStarted = false;
        }

        blink += Time.deltaTime;
        if (blink > 0.75 && flip == 0)
        {
            blinking = "_";
            blink = 0;
            flip = 1;

            continueText.gameObject.SetActive(false);
        }
        else if (blink > 0.75 && flip == 1)
        {
            blinking = "  ";
            blink = 0;
            flip = 0;
            
            if (timer > 3)
            {
                continueText.gameObject.SetActive(true);
            }
        }
    }

    void beginCutscene()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            line += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            line = string.Empty;
            StartCoroutine(TypeLine());
        }
    }
}
