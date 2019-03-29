using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Upgrades
{
    private static Dictionary<string, bool> upgradeList = new Dictionary<string, bool>
        {
            { "onlyShortWords", false },
            { "onlyLongWords", false },
            { "rightfulPay", true },

            { "positiveMindset", false },
            { "romanticMindset", false },

            { "wordGrid", false },
            { "rigidWords", false },

            { "betterCoffee", false },
            { "betterNap", false }
        };

    private static  Dictionary<string, int> upgradeCost = new Dictionary<string, int>
        {
            { "onlyShortWords", 100 },
            { "onlyLongWords", 100 },
            { "rightfulPay", 100 },

            { "positiveMindset", 100 },
            { "romanticMindset", 100 },

            { "wordGrid", 100 },
            { "rigidWords", 100 },

            { "betterCoffee", 100 },
            { "betterNap", 100 }
        };

    public static void setUpgrade(string upgradeName)
    {
        if (upgradeList.ContainsKey(upgradeName))
        {
            upgradeList[upgradeName] = true;
        }
    }
    public static bool hasUpgrade(string upgradeName)
    {
        if (upgradeList.ContainsKey(upgradeName))
        {
            return upgradeList[upgradeName];
        }
        return false;
    }
    public static int getCost(string upgradeName)
    {
        if (upgradeCost.ContainsKey(upgradeName))
        {
            return upgradeCost[upgradeName];
        }
        return -1;
    }
}
