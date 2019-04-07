using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public static Item selected;

    [SerializeField]
    private string itemName;
    [SerializeField]
    private float sellPrice;
    [SerializeField]
    private float pawnPrice;
    [SerializeField]
    private Text amtTxt;

    public float amount;
    public bool inStore;
    private Sprite sprite;
    private Button button;



    // Start is called before the first frame update
    void Start()
    {
        button = this.GetComponent<Button>();
        sprite = button.image.sprite;
        sellPrice = ItemInfo.itemStats[itemName]["sellPrice"];
        pawnPrice = ItemInfo.itemStats[itemName]["pawnPrice"];
    }

    void Update()
    {
        if (!inStore || Resources.money > sellPrice)
        {
            //TODO: Check enough money
            button.interactable = true;

        }
        else
        {
            //show not enough cash
            button.interactable = false;
        }
    }

    public void Refresh()
    {
        amtTxt.text = Mathf.CeilToInt(amount).ToString();
    }

    public float getSellPrice()
    {
        return sellPrice;
    }
    public float getPawnPrice()
    {
        return pawnPrice;
    }
    public string getItemName()
    {
        return itemName;
    }

    public void SelectThis()
    {
        Item.selected = this;
        ExecuteEvents.ExecuteHierarchy<IStoreInteraction>(gameObject, null, (x, y) => x.OnStoreInteraction());
    }

    public Sprite GetSprite()
    {
        return sprite;
    }
}
