using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    SortedList<string, Item> items;
    [SerializeField]
    private bool isStore;

    // Start is called before the first frame update
    void Start()
    {
        items = new SortedList<string, Item>();
        string itemName;
        foreach (Item i in GetComponentsInChildren<Item>())
        {
            i.inStore = isStore;
            itemName = i.getItemName();
            if (isStore)
            {
                i.amount = (int)ItemInfo.itemStats[itemName]["startStoreStock"];
            }
            else
            {
                i.amount = (int)ItemInfo.itemStats[itemName]["startInventory"];
            }

            if (i.amount <= 0)
                i.gameObject.SetActive(false);
            else
                i.gameObject.SetActive(true);
            items.Add(i.getItemName(), i);
            i.Refresh();
        }
    }

    public void Reset()
    {
        string itemName;
        foreach (Item i in items.Values)
        {
            i.inStore = isStore;
            itemName = i.getItemName();
            if (isStore)
            {
                i.amount = (int)ItemInfo.itemStats[itemName]["startStoreStock"];
            }
            else
            {
                i.amount = (int)ItemInfo.itemStats[itemName]["startInventory"];
            }
        }
    }

    public void OnEndDay()
    {
        foreach (Item i in items.Values)
        {
            string itemName = i.getItemName();
            DecreaseItem(itemName, ItemInfo.itemStats[itemName]["sellOutRate"]);
        }
    }

    public void IncreaseItem(string itemName, float amt)
    {
        Item i = items[itemName];
        i.gameObject.SetActive(true);
        i.amount += amt;
        i.Refresh();
    }

    public void DecreaseItem(string itemName, float amt)
    {
        Item i = items[itemName];
        i.amount -= amt;
        i.Refresh();
        if (i.amount <= 0)
        {
            i.amount = 0;
            i.gameObject.SetActive(false);
        }
    }


    public void OnGameEnd()
    {
        float affection = 0;
        foreach (Item i in items.Values)
        {
            affection += ItemInfo.itemStats[i.getItemName()]["affectionModifier"] * i.amount;
        }
        Resources.affection = affection;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
