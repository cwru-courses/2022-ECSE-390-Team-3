using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedrunStats : MonoBehaviour
{
    public Text timeCounter;
    public Text deathsCounter;
    public Text stats;
    public Text levelLabel;
    public Text timeLabel;
    public Text deathsLabel;
    public Text finalStats;
    public Text finalTime;
    public Text finalDeaths;
    public string currentLevel;
    private static string levelHelper;
    private static bool statsOpen = false;
    private static bool restarted = false;

    private static TimeSpan timePlaying;
    private static bool timerGoing = false;

    private static float elapsedTime = 0f;

    private static TimeSpan TutorialL1Time;
    private static TimeSpan TutorialL2Time;
    private static TimeSpan TutorialL3Time;
    private static TimeSpan TutorialL4Time;
    private static TimeSpan BloodstreamL1Time;
    private static TimeSpan BloodstreamL2Time;
    private static TimeSpan BloodstreamL3Time;
    private static TimeSpan BloodstreamL4Time;
    private static TimeSpan PuzzleL1Time;
    private static TimeSpan PuzzleL2Time;
    private static TimeSpan totalTime;

    private static int TutorialL1Deaths = 0;
    private static int TutorialL2Deaths = 0;
    private static int TutorialL3Deaths = 0;
    private static int TutorialL4Deaths = 0;
    private static int BloodstreamL1Deaths = 0;
    private static int BloodstreamL2Deaths = 0;
    private static int BloodstreamL3Deaths = 0;
    private static int BloodstreamL4Deaths = 0;
    private static int PuzzleL1Deaths = 0;
    private static int PuzzleL2Deaths = 0;
    private static int totalDeaths = 0;

    private void Start()
    {
        levelHelper = currentLevel;
        timerGoing = true;
        StartCoroutine(updateTimer());
        elapsedTime = 0;

        if (!restarted)
        {
            switch (levelHelper)
            {
                case "Tutorial_L1":
                    break;
                case "Tutorial_L2":
                    TutorialL1Time = timePlaying;
                    break;
                case "Tutorial_L3":
                    TutorialL2Time = timePlaying - TutorialL1Time;
                    break;
                case "Tutorial_L4":
                    TutorialL3Time = timePlaying - TutorialL2Time - TutorialL1Time;
                    break;
                case "Bloodstream_L1":
                    TutorialL4Time = timePlaying - TutorialL3Time - TutorialL2Time - TutorialL1Time;
                    break;
                case "Bloodstream_L2":
                    BloodstreamL1Time = timePlaying - TutorialL4Time - TutorialL3Time - TutorialL2Time - TutorialL1Time;
                    break;
                case "Bloodstream_L3":
                    BloodstreamL2Time = timePlaying - BloodstreamL1Time - TutorialL4Time - TutorialL3Time - TutorialL2Time - TutorialL1Time;
                    break;
                case "Bloodstream_L4":
                    BloodstreamL3Time = timePlaying - BloodstreamL2Time - BloodstreamL1Time - TutorialL4Time - TutorialL3Time - TutorialL2Time - TutorialL1Time;
                    break;
                case "Puzzle_L1":
                    BloodstreamL4Time = timePlaying - BloodstreamL3Time - BloodstreamL2Time - BloodstreamL1Time - TutorialL4Time - TutorialL3Time - TutorialL2Time - TutorialL1Time;
                    break;
                case "Puzzle_L2":
                    PuzzleL1Time = timePlaying - BloodstreamL4Time - BloodstreamL3Time - BloodstreamL2Time - BloodstreamL1Time - TutorialL4Time - TutorialL3Time - TutorialL2Time - TutorialL1Time;
                    break;
                case "Finale":
                    PuzzleL2Time = timePlaying - PuzzleL1Time - BloodstreamL4Time - BloodstreamL3Time - BloodstreamL2Time - BloodstreamL1Time - TutorialL4Time - TutorialL3Time - TutorialL2Time - TutorialL1Time;
                    totalTime = timePlaying;
                    break;
                default:
                    break;
            }
        }
        else
        {
            restarted = false;
        }

        if (stats != null)
        {
            stats.text =
                "Tutorial_L1:    " + TutorialL1Time.ToString("mm':'ss'.'ff") + "    " + TutorialL1Deaths + "\n" +
                "Tutorial_L2:    " + TutorialL2Time.ToString("mm':'ss'.'ff") + "    " + TutorialL2Deaths + "\n" +
                "Tutorial_L3:    " + TutorialL3Time.ToString("mm':'ss'.'ff") + "    " + TutorialL3Deaths + "\n" +
                "Tutorial_L4:    " + TutorialL4Time.ToString("mm':'ss'.'ff") + "    " + TutorialL4Deaths + "\n" +
                "Bloodstream_L1: " + BloodstreamL1Time.ToString("mm':'ss'.'ff") + "    " + BloodstreamL1Deaths + "\n" +
                "Bloodstream_L2: " + BloodstreamL2Time.ToString("mm':'ss'.'ff") + "    " + BloodstreamL2Deaths + "\n" +
                "Bloodstream_L3: " + BloodstreamL3Time.ToString("mm':'ss'.'ff") + "    " + BloodstreamL3Deaths + "\n" +
                "Bloodstream_L4: " + BloodstreamL4Time.ToString("mm':'ss'.'ff") + "    " + BloodstreamL4Deaths + "\n" +
                "Puzzle_L1:      " + PuzzleL1Time.ToString("mm':'ss'.'ff") + "    " + PuzzleL1Deaths + "\n" +
                "Puzzle_L2:      " + PuzzleL2Time.ToString("mm':'ss'.'ff") + "    " + PuzzleL2Deaths + "\n";
        }
        if (finalStats != null)
        {
            finalStats.text =
                "Tutorial_L1:    " + TutorialL1Time.ToString("mm':'ss'.'ff") + "    " + TutorialL1Deaths + "\n" +
                "Tutorial_L2:    " + TutorialL2Time.ToString("mm':'ss'.'ff") + "    " + TutorialL2Deaths + "\n" +
                "Tutorial_L3:    " + TutorialL3Time.ToString("mm':'ss'.'ff") + "    " + TutorialL3Deaths + "\n" +
                "Tutorial_L4:    " + TutorialL4Time.ToString("mm':'ss'.'ff") + "    " + TutorialL4Deaths + "\n" +
                "Bloodstream_L1: " + BloodstreamL1Time.ToString("mm':'ss'.'ff") + "    " + BloodstreamL1Deaths + "\n" +
                "Bloodstream_L2: " + BloodstreamL2Time.ToString("mm':'ss'.'ff") + "    " + BloodstreamL2Deaths + "\n" +
                "Bloodstream_L3: " + BloodstreamL3Time.ToString("mm':'ss'.'ff") + "    " + BloodstreamL3Deaths + "\n" +
                "Bloodstream_L4: " + BloodstreamL4Time.ToString("mm':'ss'.'ff") + "    " + BloodstreamL4Deaths + "\n" +
                "Puzzle_L1:      " + PuzzleL1Time.ToString("mm':'ss'.'ff") + "    " + PuzzleL1Deaths + "\n" +
                "Puzzle_L2:      " + PuzzleL2Time.ToString("mm':'ss'.'ff") + "    " + PuzzleL2Deaths + "\n" +
                "-----------------------------" + "\n" +
                "Total:          " + totalTime.ToString("mm':'ss'.'ff") + "    " + totalDeaths;
        }
        if (finalTime != null)
        {
            finalTime.text = "Total Playtime" + "\n" + totalTime.ToString("mm':'ss'.'ff");
        }
        if (finalDeaths != null)
        {
            finalDeaths.text = "Total Deaths" +  "\n" + totalDeaths;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && statsOpen == false)
        {
            statsOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && statsOpen == true)
        {
            statsOpen = false;
        }

        if(statsOpen == true)
        {
            openStats();
        }
        else if (statsOpen == false)
        {
            closeStats();
        }
    }

    private void openStats()
    {
        timeCounter.enabled = true;
        deathsCounter.enabled = true;
        levelLabel.enabled = true;
        timeLabel.enabled = true;
        deathsLabel.enabled = true;
        stats.enabled = true;
    }
    private void closeStats()
    {
        timeCounter.enabled = false;
        deathsCounter.enabled = false;
        levelLabel.enabled = false;
        timeLabel.enabled = false;
        deathsLabel.enabled = false;
        stats.enabled = false;
    }

    private IEnumerator updateTimer()
    {
        while (timerGoing)
        {
            if (timeCounter != null)
            {
                elapsedTime += Time.deltaTime;
                timePlaying = TutorialL1Time + TutorialL2Time + TutorialL3Time + TutorialL4Time + BloodstreamL1Time + BloodstreamL2Time + BloodstreamL3Time + BloodstreamL4Time + PuzzleL1Time + PuzzleL2Time + TimeSpan.FromSeconds(elapsedTime);
                string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
                timeCounter.text = timePlayingStr;
                deathsCounter.text = "Deaths: " + totalDeaths.ToString();
            }

            yield return null;
        }
    }

    public static void playerDeath()
    {
        totalDeaths++;
        switch (levelHelper)
        {
            case "Tutorial_L1":
                TutorialL1Deaths++;
                break;
            case "Tutorial_L2":
                TutorialL2Deaths++;
                break;
            case "Tutorial_L3":
                TutorialL3Deaths++;
                break;
            case "Tutorial_L4":
                TutorialL4Deaths++;
                break;
            case "Bloodstream_L1":
                BloodstreamL1Deaths++;
                break;
            case "Bloodstream_L2":
                BloodstreamL2Deaths++;
                break;
            case "Bloodstream_L3":
                BloodstreamL3Deaths++;
                break;
            case "Bloodstream_L4":
                BloodstreamL4Deaths++;
                break;
            case "Puzzle_L1":
                PuzzleL1Deaths++;
                break;
            case "Puzzle_L2":
                PuzzleL2Deaths++;
                break;
            case "Finale":
                //totalDeaths = TutorialL1Deaths + TutorialL2Deaths + TutorialL3Deaths + TutorialL4Deaths + BloodstreamL1Deaths + BloodstreamL2Deaths + BloodstreamL3Deaths + BloodstreamL4Deaths + PuzzleL1Deaths + PuzzleL2Deaths;
                break;
            default:
                break;
        }
    }

    public static void resetLevel()
    {
        restarted = true;
        switch (levelHelper)
        {
            case "Tutorial_L1":
                TutorialL1Time = TimeSpan.Zero;
                TutorialL1Deaths = 0;
                break;
            case "Tutorial_L2":
                TutorialL2Time = TimeSpan.Zero;
                TutorialL2Deaths = 0;
                break;
            case "Tutorial_L3":
                TutorialL3Time = TimeSpan.Zero;
                TutorialL3Deaths = 0;
                break;
            case "Tutorial_L4":
                TutorialL4Time = TimeSpan.Zero;
                TutorialL4Deaths = 0;
                break;
            case "Bloodstream_L1":
                BloodstreamL1Time = TimeSpan.Zero;
                BloodstreamL1Deaths = 0;
                break;
            case "Bloodstream_L2":
                BloodstreamL2Time = TimeSpan.Zero;
                BloodstreamL2Deaths = 0;
                break;
            case "Bloodstream_L3":
                BloodstreamL3Time = TimeSpan.Zero;
                BloodstreamL3Deaths = 0;
                break;
            case "Bloodstream_L4":
                BloodstreamL4Time = TimeSpan.Zero;
                BloodstreamL4Deaths = 0;
                break;
            case "Puzzle_L1":
                PuzzleL1Time = TimeSpan.Zero;
                PuzzleL1Deaths = 0;
                break;
            case "Puzzle_L2":
                PuzzleL2Time = TimeSpan.Zero;
                PuzzleL2Deaths = 0;
                break;
            default:
                break;
        }
        totalDeaths = TutorialL1Deaths + TutorialL2Deaths + TutorialL3Deaths + TutorialL4Deaths + BloodstreamL1Deaths + BloodstreamL2Deaths + BloodstreamL3Deaths + BloodstreamL4Deaths + PuzzleL1Deaths + PuzzleL2Deaths;
    }
}
