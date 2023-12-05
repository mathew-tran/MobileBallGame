using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimeUI : MonoBehaviour
{
    public GameManager ManagerReference;
    public Slider slider;
    public Text currentTimeText;
    public Text bestTimeText;
    public static string format = "{0:0.##}";
    // Start is called before the first frame update
    void Start()
    {
        ManagerReference.OnTimeChange += UpdateTimeUI;
        ManagerReference.OnGameEnd += UpdateEndTimeUI;
        bestTimeText.text = "BEST: " + String.Format(format, ManagerReference.bestTime) + "s";
    }

    public void UpdateTimeUI()
    {
        slider.value = ManagerReference.time / ManagerReference.maxTime;
        currentTimeText.text = String.Format(format, ManagerReference.realTime) +"s";        
    }

    public void UpdateEndTimeUI()
    {               
        if(ManagerReference.didPass)
        {
            bestTimeText.color = Color.yellow;
        }
        currentTimeText.text = String.Format(format, ManagerReference.realTime) + "s";
        bestTimeText.text = "BEST: " + String.Format(format, ManagerReference.bestTime) + "s";        
    }



   
}
