using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class GameData
{
    public string selectedLevel;
}

public class GameManager : MonoBehaviour
{
    public enum State
    {
        WaitToStart,
        Win,
        InProgress,
        Lose
    }

    public float time = 0;
    public float maxTime = 15.0f;
    public float bestTime = 21.23f;

    public float realTime = 0.0f;

    public delegate void OnTimeChangeEvent();
    public event OnTimeChangeEvent OnTimeChange;

    public delegate void OnGameEndEvent();
    public event OnGameEndEvent OnGameEnd;

    public delegate void OnEnemyRemoveEvent();
    public event OnEnemyRemoveEvent OnEnemyRemove;

    public State currentState;

    public int blockAmount = 0;
    public int maxBlockAmount = 0;

    public GameObject winUI;
    public GameObject loseUI;
    public GameObject generalUI;
    public GameObject personalBestUI;
    public GameObject nextLevelUI;
    public GameObject levelNameUI;
    public bool didPass = false;
    public Level level;
    public string selectedLevelName;

    public LevelData data;

    public AudioClip winClip;
    public AudioClip loseClip;


    public static void SetLevel(GameData data)
    {
        PlayerPrefs.SetString($"SelectedLevel", data.selectedLevel);
        PlayerPrefs.Save();
    }
    public static Level LoadLevel()
    {
        GameData savedData= new GameData();
        savedData.selectedLevel = PlayerPrefs.GetString($"SelectedLevel");
        var level = Instantiate(Resources.Load<GameObject>(savedData.selectedLevel));
        return level.GetComponentInChildren<Level>();
    }
    public static void SaveLevel(LevelData data, Level level)
    {
        PlayerPrefs.SetFloat($"BestTime{level.levelName}", data.bestTime);
        PlayerPrefs.Save();
    }
    
    public static LevelData LoadData(Level level)
    {
        LevelData data = new LevelData();
        data.bestTime = PlayerPrefs.GetFloat($"BestTime{level.levelName}");
        if(data.bestTime == 0)
        {
            data.bestTime = 999.0f;
        }
        return data;
    }

    public void Awake()
    {
        level = LoadLevel();

        AudioManager._instance.PlaySong(level.music.clip);
        levelNameUI.GetComponent<Text>().text = level.levelName;

        data = LoadData(level);

        if(data.bestTime == 0)
        {
            LevelData newData = new LevelData();
            SaveLevel(newData, level);
        }
        bestTime = data.bestTime;
    }

    public void Start()
    {
        time = maxTime;

        currentState = State.WaitToStart;

        StartCoroutine(StartGame());

        AudioManager._instance.Attach();
          
        
    }

    public IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2);
        
        OnTimeChange();
        blockAmount = GameObject.FindObjectsOfType(typeof(Brick)).Length;
        maxBlockAmount = blockAmount;
        currentState = State.InProgress;
    }
    public void Update()
    {        
        if (currentState == State.InProgress)
        {
            realTime += Time.deltaTime;
            time -= Time.deltaTime;
            OnTimeChange();            
            
            if(time <= 0.0f)
            {
                currentState = State.Lose;
                AudioManager._instance.PlaySFX(loseClip);
                loseUI.SetActive(true);
                generalUI.SetActive(true);
                OnGameEnd();               
            }
        }
        
    }

    public void AddTime(float addedTime)
    {        
        time += addedTime;
        if(time >= maxTime)
        {
            time = maxTime;
        }
        OnTimeChange();

    }
    public void RemoveBlock()
    {        
        StopCoroutine(CheckBlocks());
        StartCoroutine(CheckBlocks());        
    }

    public IEnumerator CheckBlocks()
    {
        yield return new WaitForSeconds(.5f);
        blockAmount = GameObject.FindObjectsOfType(typeof(Brick)).Length;
        if (blockAmount == 0 && time - 0.5f >= 0f)
        {
            currentState = State.Win;
            winUI.SetActive(true);
            generalUI.SetActive(true);

            LevelProgression progression = LevelSelectUI.LoadLevelProgression();

            if(progression.levelProgression == level.level)
            {
                progression.levelProgression += 1;
                LevelSelectUI.SaveLevelProgression(progression);
            }

            LevelManager._instance.index = LevelManager._instance.levelDatas.FindIndex(x => x.levelName == level.levelName);
            if (LevelManager._instance.index < LevelManager._instance.levelDatas.Count - 1)
            {
                var nextLevel = LevelManager._instance.levelDatas[LevelManager._instance.index + 1];  
                nextLevelUI.SetActive(true);
                nextLevelUI.GetComponentInChildren<LevelPlayButton>().levelResource = $"Scenes/{nextLevel.world}/{nextLevel.levelName}";
            }

            long milliseconds = 0;
            milliseconds = (long)(realTime * 1000);
            GooglePlayController.PostToLeaderboard(milliseconds, level.boardId);
            if (realTime < bestTime)
            {
                bestTime = realTime;
                personalBestUI.SetActive(true);               
                didPass = true;
                data.bestTime = bestTime;
                SaveLevel(data, level);

            }
            else
            {
                AudioManager._instance.PlaySFX(winClip);
            }
            OnGameEnd();

        }
        OnEnemyRemove();
    }
}
