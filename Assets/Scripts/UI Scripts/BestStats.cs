using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestStats : MonoBehaviour
{
    public Text stats;
    public string level;
    private int bestTutorial1Deaths = 99;

    bestTutorial1Deaths = SpeedrunStats.getDeaths();
}
