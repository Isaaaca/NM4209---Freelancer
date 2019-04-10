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
            { "rightfulPay", 120 },
            { "shorterWords", 200 },
            { "onlyShortWords", 250},
            { "longerWords", 200 },
            { "onlyLongWords", 250 },

            { "positiveMindset", 150 },
            { "romanticMindset", 150 },

            { "wordGrid", 200 },
            { "rigidWords", 100 },

            { "betterCoffee", 50 },
            { "betterNap", 50 }
        };

    private static  Dictionary<string, string> upgradeTitle = new Dictionary<string, string>
        {
            { "rightfulPay", "Pricing Your Work: Getting Your Rightful Pay" },
            { "shorterWords", "Abandon Tiresome Work" },
            { "onlyShortWords", "Machine Gun Efficiency"},
            { "longerWords", "Choosing What's Worth"},
            { "onlyLongWords", "Tackling the Niche Market" },

            { "positiveMindset", "Positive Thinking" },
            { "romanticMindset", "Poems for Romance" },

            { "wordGrid", "Organising: Finding Your Alignment" },
            { "rigidWords", "Organising: Straightening Your Topsy-Turvy Life" }
        };

    private static  Dictionary<string, string> upgradeDescription = new Dictionary<string, string>
        {
            { "rightfulPay", "Effect: Get X2 pay for words of 8 letters or more, X3 for words of 12 letters or more." },
            { "shorterWords", "Effect: All words will be at most 8 letters long." },
            { "onlyShortWords", "Effect: All words will be at most 6 letters long. 40% Bonus pay if a correct word is submitted within 2secs of the previous word."},
            { "longerWords", "Effect: All words will be at least 6 letters long. 20% Bonus pay if a word is at least 8 letters."},
            { "onlyLongWords", "Effect: All Words will be at least 8 letters long. 60% Bonus pay if no backspace is used for a correct word." },

            { "positiveMindset", "Effect: On submission, recover 50% of the sanity expended on a word, given it is correct." },
            { "romanticMindset", "Effect: Recieve a small discount at the store for every letter in a correct word." },

            { "wordGrid", "Effect: Falling words will be aligned." },
            { "rigidWords", "Effect: Words no longer rotate when falling." }
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
