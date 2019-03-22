using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float dayDuration;
    [SerializeField]
    private float startMoney;
    [SerializeField]
    private float startSanity;

    [SerializeField]
    private PercentDisplay clock;

    [SerializeField]
    private float dayTimeLeft;
    private float sanity;
    private int money;

    private bool isDay;

    // Start is called before the first frame update
    void Start()
    {

        clock.Initialize(dayDuration, dayDuration);
        StartDay();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDay)
        {
            dayTimeLeft -= Time.deltaTime;
            clock.Set(dayTimeLeft);
            if (dayTimeLeft <= 0) isDay = false;
        }
    }

    void StartDay()
    {
        dayTimeLeft = dayDuration;
        isDay = true;
    }
}
