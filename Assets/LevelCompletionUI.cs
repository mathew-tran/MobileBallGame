using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompletionUI : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        var progress = LevelSelectUI.LoadLevelProgression();

        var maxProgress = LevelManager._instance.levelDatas.Count;
        var percent = (((float)progress.levelProgression - 1) / maxProgress) * 100;
        text.text = $"{percent}% Complete !!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
