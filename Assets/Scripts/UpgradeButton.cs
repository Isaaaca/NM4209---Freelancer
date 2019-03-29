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

        if (lockingUpgrade != null && !activated)
        {
            bool unlocked = lockingUpgrade.isActivated();
            if (unlocked)
            {
                //TODO: Check enough money
                if(Resources.money > Upgrades.getCost(upgradeName))
                {
                    button.interactable = true;
                }
                else
                {
                    //show not enough cash
                }
            }
        }
    }
}
