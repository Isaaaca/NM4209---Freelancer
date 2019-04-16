using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenuNav : MonoBehaviour
{
    [SerializeField]
    private GameObject[] screens;

    private int currActive = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    public void NextScreen()
    {
        screens[currActive].SetActive(false);
        currActive = (currActive + 1)%screens.Length;
        screens[currActive].SetActive(true);

    }

    public void PrevScreen()
    {
        screens[currActive].SetActive(false);
        currActive = (currActive + screens.Length -1) %screens.Length;
        screens[currActive].SetActive(true);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
