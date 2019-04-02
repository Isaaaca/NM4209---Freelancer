using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Upgrades
{
    private static Dictionary<string, bool> upgradeList = new Dictionary<string, bool>
        {
            { "rightfulPay", false },
            { "shorterWords", false },
            { "onlyShortWords", false },
            { "longerWords", false },
            { "onlyLongWords", false },

            { "positiveMindset", false },
            { "romanticMindset", false },

            { "wordGrid", false },
            { "rigidWords", false },

            { "betterCoffee", false },
            { "betterNap", false }
        };

    private static  Dictionary<string, int> upgradeCost = new Dictionary<string, int>
        {
            { "onlyShortWords", 50 },
            { "onlyLongWords", 50 },
            { "rightfulPay", 50 },

            { "positiveMindset", 50 },
            { "romanticMindset", 50 },

            { "wordGrid", 50 },
            { "rigidWords", 50 },

            { "betterCoffee", 50 },
            { "betterNap", 50 }
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
