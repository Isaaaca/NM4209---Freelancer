using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEndHandler : MonoBehaviour
{
    [SerializeField]
    GameObject bankruptEnd;
    [SerializeField]
    GameObject badEnd;
    [SerializeField]
    GameObject goodEnd;
    [SerializeField]
    GameObject bestEnd;
    public UnityEvent EndGameEvent;
    public void ResolveGame()
    {
        EndGameEvent.Invoke();
        float affection = Resources.affection;
        if (Resources.money < 0)
        {
            //Bankrupcy ending
            bankruptEnd.SetActive(true);
        }
        else if (affection < 0.5)
        {
            //rejection ending
            badEnd.SetActive(true);
        }
        else if (affection < 0.75)
        {
            //Good Ending
            goodEnd.SetActive(true);
        }
        else
        {
            //Best Ending
            bestEnd.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (EndGameEvent == null)
            EndGameEvent = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
