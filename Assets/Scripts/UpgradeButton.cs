using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public string upgradeName;

    [SerializeField]
    private UpgradeButton lockingUpgrade;
    [SerializeField]
    private UpgradeButton precludingUpgrade;


    private bool activated;
    private Button button;
    private Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        button = this.GetComponent<Button>();
        sprite = button.image.sprite;
        activated = Upgrades.hasUpgrade(upgradeName);
        if (lockingUpgrade != null)
        {
            button.interactable = false;
        }

    }

    public void Activate()
    {
        Upgrades.setUpgrade(upgradeName);
        Resources.DecreaseMoney(Upgrades.getCost(upgradeName));
        activated = true;
        button.interactable = false;
        //SHOW UI for Bought
    }

    public bool isActivated()
    {
        return activated;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    void Update()
    {
        bool unlocked = true;
        if (activated||(precludingUpgrade != null && precludingUpgrade.isActivated()))
        {
            unlocked = false;
        }

        else if (lockingUpgrade != null && !activated)
        {
            unlocked = lockingUpgrade.isActivated();
            
        }

        if (unlocked && Resources.money > Upgrades.getCost(upgradeName))
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
}
