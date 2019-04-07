using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DescriptionPanel : MonoBehaviour, IHasChanged
{
    [SerializeField]
    Text title;
    [SerializeField]
    Image image;
    [SerializeField]
    Text cost;
    [SerializeField]
    Text pawnPrice;
    [SerializeField]
    Text description;

    private Sprite defaultSprite;
    // Start is called before the first frame update
    void Start()
    {
        title.text = "";
        cost.text = "";
        pawnPrice.text = "";
        description.text = "";
        defaultSprite = image.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HasChanged()
    {
        GameObject hovered = Hoverable.current.hoverObject;
        UpgradeButton upgrade = hovered.GetComponent<UpgradeButton>();
        Item item = hovered.GetComponent<Item>();
        if(upgrade!= null)
        {
            string upgradeName = upgrade.upgradeName;
            print(upgradeName);
            title.text = Upgrades.getTitle(upgradeName);
            cost.text = "$" + Upgrades.getCost(upgradeName).ToString();
            pawnPrice.text = "";
            description.text = Upgrades.getDescription(upgradeName);
            image.sprite = upgrade.GetSprite();

        }
        else if(item != null)
        {
            string itemName = item.getItemName();
            print(itemName);
            title.text = itemName;
            cost.text = "$" + ItemInfo.itemStats[itemName]["sellPrice"].ToString();
            pawnPrice.text = "$" + ItemInfo.itemStats[itemName]["pawnPrice"].ToString();
            description.text = ItemInfo.itemDescription[itemName];
            image.sprite = item.GetSprite();
        }
        
    }
    
    public void Clear()
    {
        title.text = "";
        cost.text = "";
        pawnPrice.text = "";
        description.text = "";
        image.sprite = defaultSprite;
    }
}

namespace UnityEngine.EventSystems
{
    public interface IHasChanged : IEventSystemHandler
    {
        void HasChanged();
    }
}
