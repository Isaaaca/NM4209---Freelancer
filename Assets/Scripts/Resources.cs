using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Resources
{
    public static float money =20;
    public static float sanity;

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
