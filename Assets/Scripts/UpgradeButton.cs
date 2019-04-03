using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField]
    private string upgradeName;

    [SerializeField]
    private UpgradeButton lockingUpgrade;
    [SerializeField]
    private UpgradeButton precludingUpgrade;

    private bool activated;
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = this.GetComponent<Button>();
        activated = Upgrades.hasUpgrade(upgradeName);
        if (lockingUpgrade != null)
        {
            button.interactable = false;
        }

    }

    public void CheckUnlock()
    {
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
