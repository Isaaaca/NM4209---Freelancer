using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent EndDayEvent;

    [Header("Sanity Drains")]
    public bool drainSainityPerWord;
    public float rightWordSanityDrain;
    public float wrongWordSanityDrain;

    public bool drainSanityOnKeypress;
    public float sanityDrainPerKey;

    public bool drainSanityOverTime;
    public float drainRate;

    [Header("Money flow")]
    public int cashReward;
    public int cashPenalty;
    
    [Header("Starting Amounts")]
    [SerializeField]
    private float dayDuration;
    [SerializeField]
    private int startMoney;
    [SerializeField]
    private float startSanity; 

    [Header("Base Coffee")]
    [SerializeField]
    private float coffeeSanity;
    [SerializeField]
    private int coffeeMoney;
    [SerializeField]
    private float coffeeTime;

    [Header("UI Elements")]
    [SerializeField]
    private PercentDisplay clock;
    [SerializeField]
    private PercentDisplay sanityBar;
    [SerializeField]
    private Text cashDisplay;

    [SerializeField]
    InputField inputField;
    private float dayTimeLeft;
    private float sanity;
    private int money;

    [SerializeField]
    private bool isDay;

    // Start is called before the first frame update
    void Start()
    {

        if (EndDayEvent == null)
            EndDayEvent = new UnityEvent();

        EndDayEvent.AddListener(OnEndDay);
        clock.Initialize(dayDuration, dayDuration);
        sanity = startSanity;
        sanityBar.Initialize(startSanity, 100);
        money = startMoney;
        StartDay();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDay)
        {
            DecreaseTime(Time.deltaTime);
            if (dayTimeLeft <= 0) EndDayEvent.Invoke();

            if (drainSanityOverTime)
            {
                DecreaseSanity(Time.deltaTime*drainRate);
            }
        }
        if (sanity <= 0)
        {
            sanity = 0;
            EndDayEvent.Invoke();
        }
        if(drainSanityOnKeypress && Input.anyKeyDown)
        {
            DecreaseSanity(sanityDrainPerKey);
        }
    }

    void StartDay()
    {
        dayTimeLeft = dayDuration;
        inputField.Select();
        isDay = true;
    }

    void OnEndDay()
    {
        print("dayended");
        isDay = false;
        if (dayTimeLeft > 0)
        {
            //show dialogue for day ending due to sanity 
        }
    }

    public void OnCorrectWord()
    {
        if(drainSainityPerWord)
            DecreaseSanity(rightWordSanityDrain);

        IncreaseMoney(cashReward);
    }
    public void OnWrongWord()
    {
        if (drainSainityPerWord)
            DecreaseSanity(wrongWordSanityDrain);
        DecreaseMoney(cashPenalty);
    }

    public void OnCoffee()
    {
        IncreaseSanity(coffeeSanity);
        DecreaseMoney(coffeeMoney);
        DecreaseTime(coffeeTime);
    }

    private void DecreaseSanity(float amt)
    {
        sanity -= amt;
        sanityBar.Set(sanity);
    }
    private void IncreaseSanity(float amt)
    {
        sanity += amt;
        sanityBar.Set(sanity);
    }

    private void DecreaseTime(float amt)
    {
        dayTimeLeft -= amt;
        clock.Set(dayTimeLeft);
    }

    private void IncreaseMoney(int amt)
    {
        money += amt;
        cashDisplay.text = "$" + money.ToString();
    }
    private void DecreaseMoney(int amt)
    {
        money -= amt;
        cashDisplay.text = "$" + money.ToString();
    }
}
