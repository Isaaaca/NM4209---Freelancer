using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoreManager : MonoBehaviour, IStoreInteraction
{
    [SerializeField]
    private BuySellBox buySellBox;
    [SerializeField]
    private Inventory storeIven;
    [SerializeField]
    private Inventory playerInven;

    public void OnStoreInteraction()
    {

        buySellBox.gameObject.SetActive(true);
        buySellBox.StartTransaction();
        
    }

    public void MakeTransaction(int amt, float money)
    {
        string itemName = Item.selected.getItemName();
        if (Item.selected.inStore)
        {
            Resources.DecreaseMoney(money);
            storeIven.DecreaseItem(itemName, amt);
            playerInven.IncreaseItem(itemName, amt);
        }
        else
        {
            Resources.IncreaseMoney(money);
            playerInven.DecreaseItem(itemName, amt);
            storeIven.IncreaseItem(itemName, amt);
        }
    }
}

namespace UnityEngine.EventSystems
{
    public interface IStoreInteraction : IEventSystemHandler
    {
        void OnStoreInteraction();
    }
}
