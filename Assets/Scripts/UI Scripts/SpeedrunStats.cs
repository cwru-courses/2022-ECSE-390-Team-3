using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpeedrunStats : MonoBehaviour
{
    #region Level Name Texts
    private Text Tutorial_L1Name;
    private Text Tutorial_L2Name;
    private Text Tutorial_L3Name;
    private Text Tutorial_L4Name;
    private Text Bloodstream_L1Name;
    private Text Bloodstream_L2Name;
    private Text Puzzle_L1Name;
    private Text Bloodstream_L3Name;
    private Text Bloodstream_L4Name;
    private Text Puzzle_L2Name;
    private Text Boss_LevelName;
    #endregion
    #region Level Time Texts
    private Text Tutorial_L1Time;
    private Text Tutorial_L2Time;
    private Text Tutorial_L3Time;
    private Text Tutorial_L4Time;
    private Text Bloodstream_L1Time;
    private Text Bloodstream_L2Time;
    private Text Puzzle_L1Time;
    private Text Bloodstream_L3Time;
    private Text Bloodstream_L4Time;
    private Text Puzzle_L2Time;
    private Text Boss_LevelTime;
    #endregion
    #region Level Deaths Texts
    private Text Tutorial_L1Deaths;
    private Text Tutorial_L2Deaths;
    private Text Tutorial_L3Deaths;
    private Text Tutorial_L4Deaths;
    private Text Bloodstream_L1Deaths;
    private Text Bloodstream_L2Deaths;
    private Text Puzzle_L1Deaths;
    private Text Bloodstream_L3Deaths;
    private Text Bloodstream_L4Deaths;
    private Text Puzzle_L2Deaths;
    private Text Boss_LevelDeaths;
    #endregion
    #region Level Time And Death Texts
    private Text levelTimeName;
    private Text levelTimeTime;
    private Text levelDeathsName;
    private Text levelDeathsDeaths;
    #endregion

    public Text finalTime;
    public Text finalDeaths;
    public string currentLevel;
    private static string levelHelper;
    private static bool statsOpen = false;
    private static bool restarted = false;
    [HideInInspector] public static bool notFullStats = false;

    private static TimeSpan timePlaying;
    private static bool timerGoing = false;

    private static float elapsedTime = 0f;
    #region Time Variables
    private static TimeSpan TutorialL1Time;
    private static TimeSpan TutorialL2Time;
    private static TimeSpan TutorialL3Time;
    private static TimeSpan TutorialL4Time;
    private static TimeSpan BloodstreamL1Time;
    private static TimeSpan BloodstreamL2Time;
    private static TimeSpan PuzzleL1Time;
    private static TimeSpan BloodstreamL3Time;
    private static TimeSpan BloodstreamL4Time;
    private static TimeSpan PuzzleL2Time;
    private static TimeSpan BossLevelTime;
    private static TimeSpan totalTime;
    #endregion
    #region Death Variables
    private static int TutorialL1Deaths = 0;
    private static int TutorialL2Deaths = 0;
    private static int TutorialL3Deaths = 0;
    private static int TutorialL4Deaths = 0;
    private static int BloodstreamL1Deaths = 0;
    private static int BloodstreamL2Deaths = 0;
    private static int PuzzleL1Deaths = 0;
    private static int BloodstreamL3Deaths = 0;
    private static int BloodstreamL4Deaths = 0;
    private static int PuzzleL2Deaths = 0;
    private static int BossLevelDeaths = 0;
    private static int totalDeaths = 0;
    #endregion

    private void Start()
    {
        #region Level Name Texts
        Tutorial_L1Name = GameObject.Find("Tutorial_L1 name").GetComponent<Text>();
        Tutorial_L2Name = GameObject.Find("Tutorial_L2 name").GetComponent<Text>();
        Tutorial_L3Name = GameObject.Find("Tutorial_L3 name").GetComponent<Text>();
        Tutorial_L4Name = GameObject.Find("Tutorial_L4 name").GetComponent<Text>();
        Bloodstream_L1Name = GameObject.Find("Bloodstream_L1 name").GetComponent<Text>();
        Bloodstream_L2Name = GameObject.Find("Bloodstream_L2 name").GetComponent<Text>();
        Puzzle_L1Name = GameObject.Find("Puzzle_L1 name").GetComponent<Text>();
        Bloodstream_L3Name = GameObject.Find("Bloodstream_L3 name").GetComponent<Text>();
        Bloodstream_L4Name = GameObject.Find("Bloodstream_L4 name").GetComponent<Text>();
        Puzzle_L2Name = GameObject.Find("Puzzle_L2 name").GetComponent<Text>();
        Boss_LevelName = GameObject.Find("Boss Level name").GetComponent<Text>();
        #endregion
        #region Level Time Texts
        Tutorial_L1Time = GameObject.Find("Tutorial_L1 time").GetComponent<Text>();
        Tutorial_L2Time = GameObject.Find("Tutorial_L2 time").GetComponent<Text>();
        Tutorial_L3Time = GameObject.Find("Tutorial_L3 time").GetComponent<Text>();
        Tutorial_L4Time = GameObject.Find("Tutorial_L4 time").GetComponent<Text>();
        Bloodstream_L1Time = GameObject.Find("Bloodstream_L1 time").GetComponent<Text>();
        Bloodstream_L2Time = GameObject.Find("Bloodstream_L2 time").GetComponent<Text>();
        Puzzle_L1Time = GameObject.Find("Puzzle_L1 time").GetComponent<Text>();
        Bloodstream_L3Time = GameObject.Find("Bloodstream_L3 time").GetComponent<Text>();
        Bloodstream_L4Time = GameObject.Find("Bloodstream_L4 time").GetComponent<Text>();
        Puzzle_L2Time = GameObject.Find("Puzzle_L2 time").GetComponent<Text>();
        Boss_LevelTime = GameObject.Find("Boss Level time").GetComponent<Text>();
        #endregion
        #region Level Deaths Texts
        Tutorial_L1Deaths = GameObject.Find("Tutorial_L1 deaths").GetComponent<Text>();
        Tutorial_L2Deaths = GameObject.Find("Tutorial_L2 deaths").GetComponent<Text>();
        Tutorial_L3Deaths = GameObject.Find("Tutorial_L3 deaths").GetComponent<Text>();
        Tutorial_L4Deaths = GameObject.Find("Tutorial_L4 deaths").GetComponent<Text>();
        Bloodstream_L1Deaths = GameObject.Find("Bloodstream_L1 deaths").GetComponent<Text>();
        Bloodstream_L2Deaths = GameObject.Find("Bloodstream_L2 deaths").GetComponent<Text>();
        Puzzle_L1Deaths = GameObject.Find("Puzzle_L1 deaths").GetComponent<Text>();
        Bloodstream_L3Deaths = GameObject.Find("Bloodstream_L3 deaths").GetComponent<Text>();
        Bloodstream_L4Deaths = GameObject.Find("Bloodstream_L4 deaths").GetComponent<Text>();
        Puzzle_L2Deaths = GameObject.Find("Puzzle_L2 deaths").GetComponent<Text>();
        Boss_LevelDeaths = GameObject.Find("Boss Level deaths").GetComponent<Text>();
        #endregion
        #region Level Time And Death Texts
        levelTimeName = GameObject.Find("Timer Text").GetComponent<Text>();
        levelTimeTime = GameObject.Find("Time").GetComponent<Text>();
        levelDeathsName = GameObject.Find("Death Text").GetComponent<Text>();
        levelDeathsDeaths = GameObject.Find("Deaths Counter").GetComponent<Text>();
        #endregion

        if (TutorialL1Time == TimeSpan.Zero ||
            TutorialL2Time == TimeSpan.Zero ||
            TutorialL3Time == TimeSpan.Zero ||
            TutorialL4Time == TimeSpan.Zero ||
            BloodstreamL1Time == TimeSpan.Zero ||
            BloodstreamL2Time == TimeSpan.Zero ||
            PuzzleL1Time == TimeSpan.Zero ||
            BloodstreamL3Time == TimeSpan.Zero ||
            BloodstreamL4Time == TimeSpan.Zero ||
            PuzzleL2Time == TimeSpan.Zero ||
            BossLevelTime == TimeSpan.Zero
            )
        {
            notFullStats = true;
        }
        else
        {
            notFullStats = false;
        }

        levelHelper = currentLevel;
        timerGoing = true;
        StartCoroutine(updateTimer());
        elapsedTime = 0;

        if (!restarted)
        {
            switch (levelHelper)
            {
                case "Tutorial_L1":
                    resetAll();
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
                case "Puzzle_L1":
                    BloodstreamL2Time = timePlaying - BloodstreamL1Time - TutorialL4Time - TutorialL3Time - TutorialL2Time - TutorialL1Time;
                    break;
                case "Bloodstream_L3":
                    PuzzleL1Time = timePlaying - BloodstreamL2Time - BloodstreamL1Time - TutorialL4Time - TutorialL3Time - TutorialL2Time - TutorialL1Time;
                    break;
                case "Bloodstream_L4":
                    BloodstreamL3Time = timePlaying - PuzzleL1Time - BloodstreamL2Time - BloodstreamL1Time - TutorialL4Time - TutorialL3Time - TutorialL2Time - TutorialL1Time;
                    break;
                case "Puzzle_L2":
                    BloodstreamL4Time = timePlaying - PuzzleL1Time - BloodstreamL3Time - BloodstreamL2Time - BloodstreamL1Time - TutorialL4Time - TutorialL3Time - TutorialL2Time - TutorialL1Time;
                    break;
                case "Boss Level":
                    PuzzleL2Time = timePlaying - PuzzleL1Time - BloodstreamL4Time - BloodstreamL3Time - BloodstreamL2Time - BloodstreamL1Time - TutorialL4Time - TutorialL3Time - TutorialL2Time - TutorialL1Time;
                    break;
                case "Outro Video":
                    BossLevelTime = timePlaying - PuzzleL2Time - PuzzleL1Time - BloodstreamL4Time - BloodstreamL3Time - BloodstreamL2Time - BloodstreamL1Time - TutorialL4Time - TutorialL3Time - TutorialL2Time - TutorialL1Time;
                    totalTime = timePlaying;
                    break;
            }
        }
        else
        {
            restarted = false;
        }


        if (finalTime != null)
        {
            finalTime.text = "Total Playtime" + "\n" + totalTime.ToString("mm':'ss'.'ff");
        }
        if (finalDeaths != null)
        {
            finalDeaths.text = "Total Deaths" + "\n" + totalDeaths;
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

        if (statsOpen == true)
        {
            openStats();
        }
        else if (statsOpen == false)
        {
            closeStats();
        }

        if (Tutorial_L1Time != null) Tutorial_L1Time.text = TutorialL1Time.ToString("mm':'ss'.'ff");
        if (Tutorial_L2Time != null) Tutorial_L2Time.text = TutorialL2Time.ToString("mm':'ss'.'ff");
        if (Tutorial_L3Time != null) Tutorial_L3Time.text = TutorialL3Time.ToString("mm':'ss'.'ff");
        if (Tutorial_L4Time != null) Tutorial_L4Time.text = TutorialL4Time.ToString("mm':'ss'.'ff");
        if (Bloodstream_L1Time != null) Bloodstream_L1Time.text = BloodstreamL1Time.ToString("mm':'ss'.'ff");
        if (Bloodstream_L2Time != null) Bloodstream_L2Time.text = BloodstreamL2Time.ToString("mm':'ss'.'ff");
        if (Puzzle_L1Time != null) Puzzle_L1Time.text = PuzzleL1Time.ToString("mm':'ss'.'ff");
        if (Bloodstream_L3Time != null) Bloodstream_L3Time.text = BloodstreamL3Time.ToString("mm':'ss'.'ff");
        if (Bloodstream_L4Time != null) Bloodstream_L4Time.text = BloodstreamL4Time.ToString("mm':'ss'.'ff");
        if (Puzzle_L2Time != null) Puzzle_L2Time.text = PuzzleL2Time.ToString("mm':'ss'.'ff");
        if (Boss_LevelTime != null) Boss_LevelTime.text = BossLevelTime.ToString("mm':'ss'.'ff");

        if (Tutorial_L1Deaths != null) Tutorial_L1Deaths.text = "" + TutorialL1Deaths;
        if (Tutorial_L2Deaths != null) Tutorial_L2Deaths.text = "" + TutorialL2Deaths;
        if (Tutorial_L3Deaths != null) Tutorial_L3Deaths.text = "" + TutorialL3Deaths;
        if (Tutorial_L4Deaths != null) Tutorial_L4Deaths.text = "" + TutorialL4Deaths;
        if (Bloodstream_L1Deaths != null) Bloodstream_L1Deaths.text = "" + BloodstreamL1Deaths;
        if (Bloodstream_L2Deaths != null) Bloodstream_L2Deaths.text = "" + BloodstreamL2Deaths;
        if (Puzzle_L1Deaths != null) Puzzle_L1Deaths.text = "" + PuzzleL1Deaths;
        if (Bloodstream_L3Deaths != null) Bloodstream_L3Deaths.text = "" + BloodstreamL3Deaths;
        if (Bloodstream_L4Deaths != null) Bloodstream_L4Deaths.text = "" + BloodstreamL4Deaths;
        if (Puzzle_L2Deaths != null) Puzzle_L2Deaths.text = "" + PuzzleL2Deaths;
        if (Boss_LevelDeaths != null) Boss_LevelDeaths.text = "" + BossLevelDeaths;
    }

    private void openStats()
    {
        #region enable names
        if (Tutorial_L1Name != null) Tutorial_L1Name.enabled = true;
        if (Tutorial_L2Name != null) Tutorial_L2Name.enabled = true;
        if (Tutorial_L3Name != null) Tutorial_L3Name.enabled = true;
        if (Tutorial_L4Name != null) Tutorial_L4Name.enabled = true;
        if (Bloodstream_L1Name != null) Bloodstream_L1Name.enabled = true;
        if (Bloodstream_L2Name != null) Bloodstream_L2Name.enabled = true;
        if (Puzzle_L1Name != null) Puzzle_L1Name.enabled = true;
        if (Bloodstream_L3Name != null) Bloodstream_L3Name.enabled = true;
        if (Bloodstream_L4Name != null) Bloodstream_L4Name.enabled = true;
        if (Puzzle_L2Name != null) Puzzle_L2Name.enabled = true;
        if (Boss_LevelName != null) Boss_LevelName.enabled = true;
        #endregion
        #region enable times
        if (Tutorial_L1Time != null) Tutorial_L1Time.enabled = true;
        if (Tutorial_L2Time != null) Tutorial_L2Time.enabled = true;
        if (Tutorial_L3Time != null) Tutorial_L3Time.enabled = true;
        if (Tutorial_L4Time != null) Tutorial_L4Time.enabled = true;
        if (Bloodstream_L1Time != null) Bloodstream_L1Time.enabled = true;
        if (Bloodstream_L2Time != null) Bloodstream_L2Time.enabled = true;
        if (Puzzle_L1Time != null) Puzzle_L1Time.enabled = true;
        if (Bloodstream_L3Time != null) Bloodstream_L3Time.enabled = true;
        if (Bloodstream_L4Time != null) Bloodstream_L4Time.enabled = true;
        if (Puzzle_L2Time != null) Puzzle_L2Time.enabled = true;
        if (Boss_LevelTime != null) Boss_LevelTime.enabled = true;
        #endregion
        #region enable deaths
        if (Tutorial_L1Deaths != null) Tutorial_L1Deaths.enabled = true;
        if (Tutorial_L2Deaths != null) Tutorial_L2Deaths.enabled = true;
        if (Tutorial_L3Deaths != null) Tutorial_L3Deaths.enabled = true;
        if (Tutorial_L4Deaths != null) Tutorial_L4Deaths.enabled = true;
        if (Bloodstream_L1Deaths != null) Bloodstream_L1Deaths.enabled = true;
        if (Bloodstream_L2Deaths != null) Bloodstream_L2Deaths.enabled = true;
        if (Puzzle_L1Deaths != null) Puzzle_L1Deaths.enabled = true;
        if (Bloodstream_L3Deaths != null) Bloodstream_L3Deaths.enabled = true;
        if (Bloodstream_L4Deaths != null) Bloodstream_L4Deaths.enabled = true;
        if (Puzzle_L2Deaths != null) Puzzle_L2Deaths.enabled = true;
        if (Boss_LevelDeaths != null) Boss_LevelDeaths.enabled = true;
        #endregion
        #region enable level stats
        if (levelTimeName != null) levelTimeName.enabled = true;
        if (levelTimeTime != null) levelTimeTime.enabled = true;
        if (levelDeathsName != null) levelDeathsName.enabled = true;
        if (levelDeathsDeaths != null) levelDeathsDeaths.enabled = true;
        #endregion
    }
    private void closeStats()
    {
        #region disable names
        if (Tutorial_L1Name != null) Tutorial_L1Name.enabled = false;
        if (Tutorial_L2Name != null) Tutorial_L2Name.enabled = false;
        if (Tutorial_L3Name != null) Tutorial_L3Name.enabled = false;
        if (Tutorial_L4Name != null) Tutorial_L4Name.enabled = false;
        if (Bloodstream_L1Name != null) Bloodstream_L1Name.enabled = false;
        if (Bloodstream_L2Name != null) Bloodstream_L2Name.enabled = false;
        if (Puzzle_L1Name != null) Puzzle_L1Name.enabled = false;
        if (Bloodstream_L3Name != null) Bloodstream_L3Name.enabled = false;
        if (Bloodstream_L4Name != null) Bloodstream_L4Name.enabled = false;
        if (Puzzle_L2Name != null) Puzzle_L2Name.enabled = false;
        if (Boss_LevelName != null) Boss_LevelName.enabled = false;
        #endregion
        #region  disable times
        if (Tutorial_L1Time != null) Tutorial_L1Time.enabled = false;
        if (Tutorial_L2Time != null) Tutorial_L2Time.enabled = false;
        if (Tutorial_L3Time != null) Tutorial_L3Time.enabled = false;
        if (Tutorial_L4Time != null) Tutorial_L4Time.enabled = false;
        if (Bloodstream_L1Time != null) Bloodstream_L1Time.enabled = false;
        if (Bloodstream_L2Time != null) Bloodstream_L2Time.enabled = false;
        if (Puzzle_L1Time != null) Puzzle_L1Time.enabled = false;
        if (Bloodstream_L3Time != null) Bloodstream_L3Time.enabled = false;
        if (Bloodstream_L4Time != null) Bloodstream_L4Time.enabled = false;
        if (Puzzle_L2Time != null) Puzzle_L2Time.enabled = false;
        if (Boss_LevelTime != null) Boss_LevelTime.enabled = false;
        #endregion
        #region  disable deaths
        if (Tutorial_L1Deaths != null) Tutorial_L1Deaths.enabled = false;
        if (Tutorial_L2Deaths != null) Tutorial_L2Deaths.enabled = false;
        if (Tutorial_L3Deaths != null) Tutorial_L3Deaths.enabled = false;
        if (Tutorial_L4Deaths != null) Tutorial_L4Deaths.enabled = false;
        if (Bloodstream_L1Deaths != null) Bloodstream_L1Deaths.enabled = false;
        if (Bloodstream_L2Deaths != null) Bloodstream_L2Deaths.enabled = false;
        if (Puzzle_L1Deaths != null) Puzzle_L1Deaths.enabled = false;
        if (Bloodstream_L3Deaths != null) Bloodstream_L3Deaths.enabled = false;
        if (Bloodstream_L4Deaths != null) Bloodstream_L4Deaths.enabled = false;
        if (Puzzle_L2Deaths != null) Puzzle_L2Deaths.enabled = false;
        if (Boss_LevelDeaths != null) Boss_LevelDeaths.enabled = false;
        #endregion
        #region disable level stats
        if (levelTimeName != null) levelTimeName.enabled = false;
        if (levelTimeTime != null) levelTimeTime.enabled = false;
        if (levelDeathsName != null) levelDeathsName.enabled = false;
        if (levelDeathsDeaths != null) levelDeathsDeaths.enabled = false;
        #endregion
    }

    private IEnumerator updateTimer()
    {
        while (timerGoing)
        {
            if (levelDeathsDeaths != null)
            {
                elapsedTime += Time.deltaTime;
                timePlaying = TutorialL1Time + TutorialL2Time + TutorialL3Time + TutorialL4Time + BloodstreamL1Time + BloodstreamL2Time + BloodstreamL3Time + BloodstreamL4Time + PuzzleL1Time + PuzzleL2Time + TimeSpan.FromSeconds(elapsedTime);
                string timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
                if(levelTimeTime != null) levelTimeTime.text = timePlayingStr;
                if(levelDeathsDeaths != null) levelDeathsDeaths.text = totalDeaths.ToString();
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
            case "Boss_Level":
                BossLevelDeaths++;
                break;
            case "Finale":
                //totalDeaths = TutorialL1Deaths + TutorialL2Deaths + TutorialL3Deaths + TutorialL4Deaths + BloodstreamL1Deaths + BloodstreamL2Deaths + BloodstreamL3Deaths + BloodstreamL4Deaths + PuzzleL1Deaths + PuzzleL2Deaths;
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
                //TutorialL1Deaths = 0;
                break;
            case "Tutorial_L2":
                TutorialL2Time = TimeSpan.Zero;
                //TutorialL2Deaths = 0;
                break;
            case "Tutorial_L3":
                TutorialL3Time = TimeSpan.Zero;
                //TutorialL3Deaths = 0;
                break;
            case "Tutorial_L4":
                TutorialL4Time = TimeSpan.Zero;
                //TutorialL4Deaths = 0;
                break;
            case "Bloodstream_L1":
                BloodstreamL1Time = TimeSpan.Zero;
                //BloodstreamL1Deaths = 0;
                break;
            case "Bloodstream_L2":
                BloodstreamL2Time = TimeSpan.Zero;
                //BloodstreamL2Deaths = 0;
                break;
            case "Bloodstream_L3":
                BloodstreamL3Time = TimeSpan.Zero;
                //BloodstreamL3Deaths = 0;
                break;
            case "Bloodstream_L4":
                BloodstreamL4Time = TimeSpan.Zero;
                //BloodstreamL4Deaths = 0;
                break;
            case "Puzzle_L1":
                PuzzleL1Time = TimeSpan.Zero;
                //PuzzleL1Deaths = 0;
                break;
            case "Puzzle_L2":
                PuzzleL2Time = TimeSpan.Zero;
                //PuzzleL2Deaths = 0;
                break;
            case "Boss Level":
                BossLevelTime = TimeSpan.Zero;
                break;
            default:
                break;
        }
        totalDeaths = TutorialL1Deaths + TutorialL2Deaths + TutorialL3Deaths + TutorialL4Deaths + BloodstreamL1Deaths + BloodstreamL2Deaths + BloodstreamL3Deaths + BloodstreamL4Deaths + PuzzleL1Deaths + PuzzleL2Deaths + BossLevelDeaths;
    }

    public static void resetAll()
    {
        totalDeaths = 0;
        TutorialL1Time = TimeSpan.Zero;
        TutorialL1Deaths = 0;
        TutorialL2Time = TimeSpan.Zero;
        TutorialL2Deaths = 0;
        TutorialL3Time = TimeSpan.Zero;
        TutorialL3Deaths = 0;
        TutorialL4Time = TimeSpan.Zero;
        TutorialL4Deaths = 0;
        BloodstreamL1Time = TimeSpan.Zero;
        BloodstreamL1Deaths = 0;
        BloodstreamL2Time = TimeSpan.Zero;
        BloodstreamL2Deaths = 0;
        BloodstreamL3Time = TimeSpan.Zero;
        BloodstreamL3Deaths = 0;
        BloodstreamL4Time = TimeSpan.Zero;
        BloodstreamL4Deaths = 0;
        PuzzleL1Time = TimeSpan.Zero;
        PuzzleL1Deaths = 0;
        PuzzleL2Time = TimeSpan.Zero;
        PuzzleL2Deaths = 0;
        BossLevelTime = TimeSpan.Zero;
        BossLevelDeaths = 0;
    }
}
