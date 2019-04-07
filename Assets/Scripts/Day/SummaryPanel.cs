using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummaryPanel : MonoBehaviour
{
    [SerializeField]
    Text flavor;
    [SerializeField]
    Text correct;
    [SerializeField]
    Text wrong;
    [SerializeField]
    Text pay;
    [SerializeField]
    Text bonus;
    [SerializeField]
    Text penalty;
    [SerializeField]
    Text coffee;
    [SerializeField]
    Text rent;
    [SerializeField]
    Text net;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Set(string flavorTxt)
    {
        float payAmt, bonusAmt, penaltyAmt, coffeeAmt, rentAmt, netAmt;
        payAmt = Resources.daysEarnings;
        bonusAmt = Resources.daysBonus;
        penaltyAmt = Resources.daysPenalty;
        coffeeAmt = Resources.coffeeExpenditure;
        rentAmt = Resources.rentAmt;
        netAmt = payAmt + bonusAmt - penaltyAmt - coffeeAmt - rentAmt;
        flavor.text = flavorTxt;
        correct.text = Resources.numRightWords.ToString();
        wrong.text = Resources.numWrongWords.ToString();
        pay.text = "$" + payAmt.ToString();
        bonus.text ="$"+ bonusAmt.ToString();
        penalty.text ="-$"+ penaltyAmt.ToString();
        coffee.text ="-$"+ coffeeAmt.ToString();
        rent.text ="-$"+ rentAmt.ToString();
        if(netAmt<0)
            net.text ="-$"+ Mathf.Abs(netAmt).ToString();
        else
            net.text ="$"+ netAmt.ToString();
    }
}
