using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propose : MonoBehaviour
{
    public GameEndHandler gameEndHandler;
    public GameObject confirmationScreen;
    public void CheckDays()
    {
        if (Resources.dayCounter < 30)
        {
            //confirmation screen
            confirmationScreen.gameObject.SetActive(true);
        }
        else
        {
            gameEndHandler.gameObject.SetActive(true);
            gameEndHandler.ResolveGame();
        }
    }
}
