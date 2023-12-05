using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeUI : MonoBehaviour
{
    public Player playerReference;
    public Slider slider;

    public void Start()
    {
        playerReference.OnChargeChange += OnValueChange;
    }
    public void OnValueChange()
    {
        slider.value = playerReference.charge / playerReference.maxCharge;
    }
}
