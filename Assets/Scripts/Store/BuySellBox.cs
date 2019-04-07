using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuySellBox : MonoBehaviour
{
    private int max;
    private int currAmt;
    private float price;
    private float totalPrice;
    [SerializeField]
    private StoreManager store;
    
    [SerializeField]
    private Text headingTxt;
    [SerializeField]
    private Text amtTxt;
    [SerializeField]
    private Text priceTxt;
    [SerializeField]
    private Text discountTxt;
    [SerializeField]
    private Text totalPriceTxt;
    [SerializeField]
    private Button upButton;
    [SerializeField]
    private Button downButton;

    private bool buying;

    public void StartTransaction()
    {
        this.max = Mathf.CeilToInt(Item.selected.amount);
        if (Item.selected.inStore)
        {
            buying = true;
            headingTxt.text = "Buy " + Item.selected.getItemName();
            discountTxt.gameObject.SetActive(true);
            discountTxt.text = Mathf.Floor(Resources.daysDiscounts).ToString();
            price = Item.selected.getSellPrice();
        }
        else
        {
            buying = false;
            headingTxt.text = "Pawn " + Item.selected.getItemName();
            discountTxt.gameObject.SetActive(false);
            price = Item.selected.getPawnPrice();
        }
        priceTxt.text = price.ToString();
        currAmt = 1;
        OnChange();
    }

    public void OnConfirm()
    {
        store.MakeTransaction(currAmt, totalPrice);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void IncreaseAmt()
    {
        currAmt++;
        OnChange();
    }
    public void DecreaseAmt()
    {
        currAmt--;
        OnChange();
    }

    public void OnChange()
    {
        if (currAmt <= 1)
            downButton.interactable = false;
        else
            downButton.interactable = true;
        if (currAmt >= max)
            upButton.interactable = false;
        else
            upButton.interactable = true;
        if (buying && Resources.money - totalPrice < price)
            upButton.interactable = false;
        amtTxt.text = currAmt.ToString();
        totalPrice = currAmt * price - Mathf.Floor(Resources.daysDiscounts);
        totalPriceTxt.text = totalPrice.ToString();
    }
}
