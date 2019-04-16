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
    public float startRentAmt;
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
    private GameObject pauseScreen;
    [SerializeField]
    private SummaryPanel summaryPanel;

    [SerializeField]
    InputField inputField;
    private float dayTimeLeft;

    [SerializeField]
    private AudioClip[] typingSounds;
    [SerializeField]
    private AudioSource audioPlayer;
    
    private float lastRightWordTime =0;
    private float spentSanity = 0;

    private bool hitBackspace = false;
    private bool isDay;
    private bool paused = false;

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
        Resources.rentAmt = startRentAmt;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDay && !paused)
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

            if (Input.inputString != "")
            {
                audioPlayer.PlayOneShot(typingSounds[Random.Range(0, typingSounds.Length)]);
                if (drainSanityOnKeypress)
                {
                    Resources.DecreaseSanity(sanityDrainPerKey);
                    spentSanity += sanityDrainPerKey;
                }
            }

            if (Input.GetKey(KeyCode.Backspace))
            {
                hitBackspace = true;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                OnNap();
            }
            else if (Input.GetKeyDown(KeyCode.RightShift))
            {
                OnCoffee();
            }
        }



        //Update UI
        sanityBar.Set(Resources.sanity);
        cashDisplay.Refresh();


    }

    public void StartDay()
    {
        Resources.dayCounter++;
        StartDayEvent.Invoke();
        dayTimeLeft = dayDuration;
        inputField.Select();
        dayDisplay.text = "Day " + Resources.dayCounter.ToString()+"/30";
        isDay = true;
        Resources.daysEarnings = 0;
        Resources.daysBonus = 0;
        Resources.daysPenalty = 0;
        Resources.coffeeExpenditure = 0;
        Resources.numRightWords = 0;
        Resources.numWrongWords = 0;
    }
    void OnEndDay()
    {
        isDay = false;
        PayRent();
        System.Text.StringBuilder builder = new System.Text.StringBuilder();
        if (dayTimeLeft > 0)
        {
            //show dialogue for day ending due to sanity 
            builder.Append("You got fed up with work and wasted the rest of the day.\n");
        }
        else if (Resources.dayCounter >= 30)
        {
            builder.Append("Tomorrow's the big day! Time to prepare as much as possible.\n");
        }
        else
        {
            builder.Append("Yet another day passes.\n");
        }
        if (Resources.dayCounter % 5 == 0)
        {
            builder.Append("Landlady increased the rent. Again.\n");
            Resources.rentAmt += rentIncrease;
        }
        if (Resources.money < 0)
        {
            builder.Append("You don't have enough to pay rent. You'll have to pawn something off.\n");
        }
        summaryPanel.Set(builder.ToString());
        Resources.IncreaseSanity(nightSanity);
    }


    public void OnCorrectWord(int wordLen)
    {
        if (drainSainityPerWord)
            Resources.DecreaseSanity(rightWordSanityDrain);

        float pay = cashReward;
        float bonus = 0;
        if (Upgrades.hasUpgrade("rightfulPay"))
        {
            int multiplier =Mathf.Max(1, wordLen / 4);
            pay *= multiplier;
            if (Upgrades.hasUpgrade("longerWords")&& wordLen>=8)
            {
                if (Upgrades.hasUpgrade("onlyLongWords") && !hitBackspace)
                    bonus = pay * 0.6f;
                else
                    bonus = pay * 0.2f;
            }
            else if(Upgrades.hasUpgrade("shorterWords") && Upgrades.hasUpgrade("onlyShortWords"))
            {
                print(Time.time - lastRightWordTime);
                if (Time.time - lastRightWordTime < 2)
                {
                    bonus = pay * 0.4f;
                }

            }
        }
        Resources.IncreaseMoney(pay + bonus);
        hitBackspace = false;
        lastRightWordTime = Time.time;

        if (Upgrades.hasUpgrade("positiveMindset"))
        {
            Resources.IncreaseSanity(spentSanity * 0.5f);

        }
        else if(Upgrades.hasUpgrade("romanticMindset"))
        {
            Resources.daysDiscounts+= wordLen* 0.1f;

        }
        spentSanity = 0;
        Resources.daysEarnings += pay;
        Resources.daysBonus += bonus;
        Resources.numRightWords ++;

    }
    public void OnWrongWord()
    {
        if (drainSainityPerWord)
            Resources.DecreaseSanity(wrongWordSanityDrain);
        Resources.DecreaseMoney(cashPenalty);
        hitBackspace = false;
        spentSanity = 0;
        Resources.daysPenalty += cashPenalty;
        Resources.numWrongWords++;
    }

    public void OnCoffee()
    {
        if (Resources.money >= coffeeMoney)
        {
            Resources.IncreaseSanity(coffeeSanity);
            Resources.DecreaseMoney(coffeeMoney);
            DecreaseTime(coffeeTime);
            Resources.coffeeExpenditure += coffeeMoney;
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
        Resources.DecreaseMoney(Resources.rentAmt);
    }
    
  

  
    private void DecreaseTime(float amt)
    {
        dayTimeLeft = Mathf.Clamp(dayTimeLeft - amt, 0f, 100f);

    }

    public void TogglePause()
    {
        if (!paused)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
            inputField.interactable = false;
            paused = true;
            
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
            inputField.interactable = true;
            inputField.ActivateInputField();
            paused = false;
        }

    }

}