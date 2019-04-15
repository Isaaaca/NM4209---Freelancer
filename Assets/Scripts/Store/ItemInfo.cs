using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemInfo 
{
    // Start is called before the first frame update
    public static readonly Dictionary<string, Dictionary<string, float>> itemStats = new Dictionary<string, Dictionary<string, float>>
    {

        { "Rose", new Dictionary<string, float>{
                { "sellPrice", 5f},
                { "pawnPrice", 2f},
                { "startStoreStock", 120f},
                { "startInventory", 0f},
                { "sellOutRate", 2.5f},
                { "affectionModifier", 0.001f}
            }
        },

        { "Ring", new Dictionary<string, float>{
                { "sellPrice", 500f},
                { "pawnPrice", 350f},
                { "startStoreStock", 01f},
                { "startInventory", 00f},
                { "sellOutRate", 0.0f},
                { "affectionModifier", 0.3f}
            }
        },

        { "Mom's Necklace", new Dictionary<string, float>{
                { "sellPrice", 300f},
                { "pawnPrice", 150f},
                { "startStoreStock", 00f},
                { "startInventory", 01f},
                { "sellOutRate", 0.0f},
                { "affectionModifier", -0.02f}
            }
        },

        { "Bag", new Dictionary<string, float>{
                { "sellPrice", 100f},
                { "pawnPrice", 80f},
                { "startStoreStock", 05f},
                { "startInventory", 00f},
                { "sellOutRate", 0.5f},
                { "affectionModifier", 0.05f}
            }
        },

        { "Shoe", new Dictionary<string, float>{
                { "sellPrice", 80f},
                { "pawnPrice", 30f},
                { "startStoreStock", 15f},
                { "startInventory", 00f},
                { "sellOutRate", 1f},
                { "affectionModifier", 0.03f}
            }
        },

    };

    public static readonly Dictionary<string, string> itemDescription = new Dictionary<string,string>
    {

        { "Rose", "All Women like flowers right?"},

        { "Ring", "Must-have for any proposal."},
        { "Shoe", "You've always liked seeing her in heels. She has always been happy to oblige."},
        { "Bag", "Women always complain that there's no space in their purses but truth is they don't want anything bigger. They want you to carry things for them."},
        { "Mom's Necklace", "Mom's parting gift for your future wife. May she rest in peace. Your girlfriend hates it though. "},

    };
}
