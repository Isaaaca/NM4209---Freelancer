using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextDay : MonoBehaviour
{
    public GameManager gm;
    public GameObject confirmationScreen;
    [SerializeField]
    private Button button;

    public void CheckLoseCondition()
    {
        if (Resources.money < 0)
        {
            //confirmation screen
            confirmationScreen.gameObject.SetActive(true);
        }
        else
        {
            gm.StartDay();
        }
    }

    public void OnEndDay()
    {
        if (Resources.dayCounter >= 30)
            button.interactable = false;
    }
}
