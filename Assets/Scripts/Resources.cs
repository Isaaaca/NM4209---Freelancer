using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Resources
{
    public static float money =20;
    public static float sanity;
    public static float rentAmt;

    public static float daysEarnings=0;
    public static float daysBonus=0;
    public static float daysPenalty=0;
    public static int numRightWords=0;
    public static int numWrongWords = 0;
    public static float coffeeExpenditure = 0;

    public static void DecreaseSanity(float amt)
    {
        sanity = Mathf.Clamp(sanity - amt, 0f, 100f);
    }
    public static void IncreaseSanity(float amt)
    {
        sanity = Mathf.Clamp(sanity + amt, 0f, 100f);
    }

    public static void IncreaseMoney(float amt)
    {
        money += amt;
    }
    public static void DecreaseMoney(float amt)
    {
        money -= amt;
    }
}
