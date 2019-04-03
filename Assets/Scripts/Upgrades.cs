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
            { "rightfulPay", 100 },
            { "shorterWords", 150 },
            { "onlyShortWords", 250},
            { "longerWords", 150 },
            { "onlyLongWords", 250 },

            { "positiveMindset", 100 },
            { "romanticMindset", 100 },

            { "wordGrid", 200 },
            { "rigidWords", 100 },

            { "betterCoffee", 50 },
            { "betterNap", 50 }
        };

    private static  Dictionary<string, string> upgradeTitle = new Dictionary<string, string>
        {
            { "rightfulPay", "rightfulPay" },
            { "shorterWords", "shorterWords" },
            { "onlyShortWords", "onlyShortWords"},
            { "longerWords", "longerWords"},
            { "onlyLongWords", "onlyLongWords" },

            { "positiveMindset", "positiveMindset" },
            { "romanticMindset", "romanticMindset" },

            { "wordGrid", "wordGrid" },
            { "rigidWords", "rigidWords" }
        };

    private static  Dictionary<string, string> upgradeDescription = new Dictionary<string, string>
        {
            { "rightfulPay", "rightfulPay" },
            { "shorterWords", "shorterWords" },
            { "onlyShortWords", "onlyShortWords"},
            { "longerWords", "longerWords"},
            { "onlyLongWords", "onlyLongWords" },

            { "positiveMindset", "positiveMindset" },
            { "romanticMindset", "romanticMindset" },

            { "wordGrid", "wordGrid" },
            { "rigidWords", "rigidWords" }
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
    public static string getTitle(string upgradeName)
    {
        if (upgradeTitle.ContainsKey(upgradeName))
        {
            return upgradeTitle[upgradeName];
        }
        return "";
    }
    public static string getDescription(string upgradeName)
    {
        if (upgradeDescription.ContainsKey(upgradeName))
        {
            return upgradeDescription[upgradeName];
        }
        return "";
    }
}
