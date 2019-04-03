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
    public float cashReward;
    public float cashPenalty;
    public float rentAmt;
    public float rentIncrease;

    [Header("Starting Amounts")]
    [SerializeField]
    private float dayDuration;
    [SerializeField]
    private float startMoney;
    [SerializeField]
    private float startSanity;
    [SerializeField]
    private float nightSanity;

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
    private CashText cashDisplay;
    [SerializeField]
    private Text dayDisplay;

    [SerializeField]
    InputField inputField;
    private float dayTimeLeft;
    private float lastRightWordTime =0;
    private float spentSanity = 0;
    private int day =0;

    private bool hitBackspace = false;
    private bool isDay;

    public void StartDay()
    {
        day++;
        if (day % 5 == 0)
            rentAmt += rentIncrease;
        StartDayEvent.Invoke();
        dayTimeLeft = dayDuration;
        inputField.Select();
        dayDisplay.text = "Day " + day.ToString();
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
        PayRent();
        Resources.IncreaseSanity(nightSanity);
    }


    public void OnCorrectWord(int wordLen)
    {
        if (drainSainityPerWord)
            Resources.DecreaseSanity(rightWordSanityDrain);

        float pay = cashReward;
        if (Upgrades.hasUpgrade("rightfulPay"))
        {
            int multiplier =Mathf.Max(1, wordLen / 4);
            pay *= multiplier;
            if (Upgrades.hasUpgrade("longerWords")&& wordLen>=8)
            {
                if (Upgrades.hasUpgrade("onlyLongWords") && !hitBackspace)
                    pay *= 2;
                else
                    pay *= 1.5f;
            }
            else if(Upgrades.hasUpgrade("shorterWords") && Upgrades.hasUpgrade("onlyShortWords"))
            {
                print(Time.time - lastRightWordTime);
                if (Time.time - lastRightWordTime < 2)
                {
                    pay *= 1.4f;
                }

            }
        }
        Resources.IncreaseMoney(pay);
        hitBackspace = false;
        lastRightWordTime = Time.time;

        if (Upgrades.hasUpgrade("positiveMindset"))
        {
            Resources.IncreaseSanity(spentSanity * 0.5f);

        }
        spentSanity = 0;

    }
    public void OnWrongWord()
    {
        if (drainSainityPerWord)
            Resources.DecreaseSanity(wrongWordSanityDrain);
        Resources.DecreaseMoney(cashPenalty);
        hitBackspace = false;
        spentSanity = 0;
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
        cashDisplay.Refresh();
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

            if (Resources.sanity <= 0)
            {
                Resources.sanity = 0;
                EndDayEvent.Invoke();
            }

            if (drainSanityOnKeypress && Input.inputString != "")
            {
                Resources.DecreaseSanity(sanityDrainPerKey);
                spentSanity += sanityDrainPerKey;
            }

            if (Input.GetKey(KeyCode.Backspace))
            {
                hitBackspace = true;
            }
        }
       
        

        //Update UI
        sanityBar.Set(Resources.sanity);
        cashDisplay.Refresh();


    }


  
    private void DecreaseTime(float amt)
    {
        dayTimeLeft = Mathf.Clamp(dayTimeLeft - amt, 0f, 100f);

    }
}