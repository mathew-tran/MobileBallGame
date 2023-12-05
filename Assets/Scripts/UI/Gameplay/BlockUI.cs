using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockUI : MonoBehaviour
{
    public GameManager managerReference;
    public Slider slider;
    public void Start()
    {
        managerReference.OnEnemyRemove += OnEnemyRemove;
    }

    public void OnEnemyRemove()
    {
        StopCoroutine(SlowUpdateBar());
        StartCoroutine(SlowUpdateBar());
        
    }
    public IEnumerator SlowUpdateBar()
    {
        var oldValue = slider.value;
        var newValue = 1 - (float)managerReference.blockAmount / (float)managerReference.maxBlockAmount;

        var step = 0.0f;
        var rate = .1f;
        while(step != 1.0f)
        {
            slider.value = Mathf.Lerp(oldValue, newValue, step);
            step += rate;
            yield return null;
        }
        
    }
    
}
