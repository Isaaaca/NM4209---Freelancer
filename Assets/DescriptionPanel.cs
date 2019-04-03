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
    Text sellPrice;
    [SerializeField]
    Text description;

    private Sprite defaultSprite;
    // Start is called before the first frame update
    void Start()
    {
        title.text = "";
        cost.text = "";
        sellPrice.text = "";
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
        if(upgrade!= null)
        {
            string upgradeName = upgrade.upgradeName;
            print(upgradeName);
            title.text = Upgrades.getTitle(upgradeName);
            cost.text = "$" + Upgrades.getCost(upgradeName).ToString();
            sellPrice.text = "";
            description.text = Upgrades.getDescription(upgradeName);
            image.sprite = upgrade.GetSprite();

        }
        
    }
    
    public void Clear()
    {
        title.text = "";
        cost.text = "";
        sellPrice.text = "";
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
