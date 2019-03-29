﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Resources
{
    public static int money;
    public static float sanity;

    public static void DecreaseSanity(float amt)
    {
        sanity = Mathf.Clamp(sanity - amt, 0f, 100f);
    }
    public static void IncreaseSanity(float amt)
    {
        sanity = Mathf.Clamp(sanity + amt, 0f, 100f);
    }

    public static void IncreaseMoney(int amt)
    {
        money += amt;
    }
    public static void DecreaseMoney(int amt)
    {
        money -= amt;
    }
}