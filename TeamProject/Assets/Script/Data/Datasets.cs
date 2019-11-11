using System;
using System.Collections;
using System.Collections.Generic;
using Script.Data;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;
using Random = UnityEngine.Random;


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

    // 병아리 리스트
    private List<int> chickList;
    private const string CHICK_LIST_KEY = "chickkey";

    public List<int> ChickList
    {
        get { return chickList; }
        set
        {
            chickList = value;
            PlayerPrefs.SetString(CHICK_LIST_KEY,JsonUtil.ToJson(chickList));
        }
    }
    
    // 달걀 리스트
    private List<int> eggList;
    private const string EGG_LIST_KEY = "eggkey";

    public List<int> EggList
    {
        get { return eggList; }
        set
        {
            eggList = value;
            PlayerPrefs.SetString(EGG_LIST_KEY,JsonUtil.ToJson(eggList));
        }
    }

    private void Start()
    {
        // 화면이 바뀌어도 유지
        DontDestroyOnLoad(this);
        
        // Gold 데이터 초기화
        if (!PlayerPrefs.HasKey(GOLD_KEY))
        {
            gold = 0;
            PlayerPrefs.SetInt(GOLD_KEY, gold);
        }
        else
        {
            gold = PlayerPrefs.GetInt(GOLD_KEY);
        }
        
        // 병아리 초기화
        if(!PlayerPrefs.HasKey(CHICK_LIST_KEY))
        {
            chickList = new List<int>();
            PlayerPrefs.SetString(CHICK_LIST_KEY,JsonUtil.ToJson(chickList));
        }
        else
        {
            string str = PlayerPrefs.GetString(CHICK_LIST_KEY);
            chickList = JsonUtil.ToObject<List<int>>(str);
        }
        
        // 달걀 초기화
        if(!PlayerPrefs.HasKey(EGG_LIST_KEY))
        {
            eggList = new List<int>();
            PlayerPrefs.SetString(EGG_LIST_KEY,JsonUtil.ToJson(eggList));
        }
        else
        {
            string str = PlayerPrefs.GetString(EGG_LIST_KEY);
            eggList = JsonUtil.ToObject<List<int>>(str);
        }
    }
}
