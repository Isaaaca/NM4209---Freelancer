using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent EndDayEvent;
    public UnityEvent StartDayEvent;

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
    public int rentAmt;
    
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

    [Header("Base Nap")]
    [SerializeField]
    private float napSanity;
    [SerializeField]
    private float napTime;

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

    public void StartDay()
    {
        StartDayEvent.Invoke();
        dayTimeLeft = dayDuration;
        inputField.Select();
        isDay = true;
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
        if (money > coffeeMoney)
        {
            IncreaseSanity(coffeeSanity);
            DecreaseMoney(coffeeMoney);
            DecreaseTime(coffeeTime);
        }
    }
    public void OnNap()
    {
        if (dayTimeLeft > napTime)
        {
            IncreaseSanity(napSanity);
            DecreaseTime(napTime);
        }
        else
        {
            IncreaseSanity(dayTimeLeft / napTime * napSanity);
            DecreaseTime(dayTimeLeft);
        }
    }

    public void PayRent()
    {
        DecreaseMoney(rentAmt);
    }
    // Start is called before the first frame update
    void Start()
    {

        if (EndDayEvent == null)
            EndDayEvent = new UnityEvent();
        if (StartDayEvent == null)
            StartDayEvent = new UnityEvent();

        EndDayEvent.AddListener(OnEndDay);
        clock.Initialize(dayDuration, dayDuration);
        sanity = startSanity;
        sanityBar.Initialize(startSanity, 100);
        money = startMoney;
        cashDisplay.text = "$" + money.ToString();
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


    void OnEndDay()
    {
        print("dayended");
        isDay = false;
        if (dayTimeLeft > 0)
        {
            //show dialogue for day ending due to sanity 
        }
    }


    private void DecreaseSanity(float amt)
    {
        sanity = Mathf.Clamp(sanity-amt, 0f, 100f);
        sanityBar.Set(sanity);
    }
    private void IncreaseSanity(float amt)
    {
        sanity = Mathf.Clamp(sanity + amt, 0f, 100f);
        sanityBar.Set(sanity);
    }

    private void DecreaseTime(float amt)
    {
        dayTimeLeft = Mathf.Clamp(dayTimeLeft - amt, 0f, 100f);
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
