using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoggedInElement : MonoBehaviour
{
    void OnEnable()
    {
        if(!GooglePlayController.IsAuthenticated())
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
