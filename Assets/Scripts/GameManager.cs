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

    [SerializeField]
    private bool isDay;

    public void StartDay()
    {
        StartDayEvent.Invoke();
        dayTimeLeft = dayDuration;
        inputField.Select();
        isDay = true;
    }

    public void OnCorrectWord(int wordLen)
    {
        if (drainSainityPerWord)
            Resources.DecreaseSanity(rightWordSanityDrain);
        if (Upgrades.hasUpgrade("rightfulPay"))
        {
            int multiplier =Mathf.Max(1, wordLen / 4);
            Resources.IncreaseMoney(cashReward * multiplier);
        }
        else Resources.IncreaseMoney(cashReward);
    }
    public void OnWrongWord()
    {
        if (drainSainityPerWord)
            Resources.DecreaseSanity(wrongWordSanityDrain);
        Resources.DecreaseMoney(cashPenalty);
    }

    public void OnCoffee()
    {
        if (Resources.money > coffeeMoney)
        {
            Resources.IncreaseSanity(coffeeSanity);
            Resources.DecreaseMoney(coffeeMoney);
            DecreaseTime(coffeeTime);
        }
    }
    public void OnNap()
    {
        if (dayTimeLeft > napTime)
        {
            Resources.IncreaseSanity(napSanity);
            DecreaseTime(napTime);
        }
        else
        {
            Resources.IncreaseSanity(dayTimeLeft / napTime * napSanity);
            DecreaseTime(dayTimeLeft);
        }
    }

    public void PayRent()
    {
        Resources.DecreaseMoney(rentAmt);
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
        Resources.sanity = startSanity;
        sanityBar.Initialize(startSanity, 100);
        Resources.money = startMoney;
        cashDisplay.text = "$" + Resources.money.ToString();
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
                Resources.DecreaseSanity(Time.deltaTime * drainRate);
            }
            clock.Set(dayTimeLeft);
        }
        if (Resources.sanity <= 0)
        {
            Resources.sanity = 0;
            EndDayEvent.Invoke();
        }
        if (drainSanityOnKeypress && Input.inputString != "")
        {
            Resources.DecreaseSanity(sanityDrainPerKey);
        }

        //Update UI
        sanityBar.Set(Resources.sanity);
        cashDisplay.text = "$" + Resources.money.ToString();

    }


    void OnEndDay()
    {
        print("dayended");
        isDay = false;
        if (dayTimeLeft > 0)
        {
            //show dialogue for day ending due to sanity 
        }
        Resources.IncreaseSanity(60f);
    }

    private void DecreaseTime(float amt)
    {
        dayTimeLeft = Mathf.Clamp(dayTimeLeft - amt, 0f, 100f);

    }
}