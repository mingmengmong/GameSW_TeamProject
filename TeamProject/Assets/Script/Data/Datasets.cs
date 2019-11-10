using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Datasets : MonoBehaviour
{
    private int gold;
    private const string GOLD_KEY = "gold";
    public int Gold
    {
        get { return gold; }
        set
        {
            gold = value;
            PlayerPrefs.SetInt(GOLD_KEY,gold);
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(this);
        if (!PlayerPrefs.HasKey(GOLD_KEY))
        {
            gold = 0;
            PlayerPrefs.SetInt(GOLD_KEY, gold);
        }
        else
        {
            gold = PlayerPrefs.GetInt(GOLD_KEY);
        }
    }
    
}