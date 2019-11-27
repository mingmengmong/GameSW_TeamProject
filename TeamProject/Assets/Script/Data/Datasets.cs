using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script.Data;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Datasets : MonoBehaviour
{
    // 골드
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


    // 획득한 병아리 리스트 ( 진화전 )
    private List<int> takeObjList;
    private const string TAKE_OBJ_LIST_KEY = "tageObjListKey";

    public List<int> TakeObjList {
        get { return takeObjList; }
        set {
            takeObjList = value;
            PlayerPrefs.SetString(TAKE_OBJ_LIST_KEY, JsonUtil.ToJson(takeObjList));
        }
    }

    // 속도 개선을 위한 방법
    // 획득 가능한 병아리 리스트를 최초에 만들어 둬서 검색 속도 향상
    private List<int> canTakeObjList = StaticData.CHICK_OBJECT_LIST.ToList();

    public List<int> CanTakeObjList {
        get { return canTakeObjList; }
        set { canTakeObjList = value; }
    }

    // 게임 실행 후 새로 획득한 오브젝트 저장할 리스트
    // 도감 진입시 초기화 해주면 된다.
    private List<int> newObjList = new List<int>();

    public List<int> NewObjList {
        get { return newObjList; }
        set { newObjList = value; }
    }
    
    private void Start()
    {
        // 화면이 바뀌어도 유지
        DontDestroyOnLoad(this);
        
        // 초기화시 두줄 주석 제거후 실행 -> 다시 주석 처리해야함
//        ChickList = new List<int>();
//        TakeObjList = new List<int>();
        
        // 획득 오브젝트 리스트 초기화
        if (!PlayerPrefs.HasKey(TAKE_OBJ_LIST_KEY)) {
            TakeObjList = new List<int>();
        } else {
            takeObjList = JsonUtil.ToObject<List<int>>(PlayerPrefs.GetString(TAKE_OBJ_LIST_KEY));
            for (int i = 0; i < takeObjList.Count; ++i) {
                canTakeObjList.Remove(takeObjList[i]);
            }
        }

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
            // 달걀은 항시 10개 고정
            eggList = new List<int>();
            for (int i = 0; i < 10; ++i) {
                eggList.Add(0);
            }
            PlayerPrefs.SetString(EGG_LIST_KEY,JsonUtil.ToJson(eggList));
        }
        else
        {
            string str = PlayerPrefs.GetString(EGG_LIST_KEY);
            eggList = JsonUtil.ToObject<List<int>>(str);
        }
    }

    // 데이터 저장
    public void saveData() {
        Gold = gold;
        EggList = eggList;
        ChickList = chickList;
        TakeObjList = takeObjList;
    }

    public void init()
    {
        Gold = 0;
        List<int> egList = new List<int>();
        for (int i = 0; i < 10; ++i) {
            egList.Add(0);
        }

        EggList = egList;
        ChickList = new List<int>();
        TakeObjList = new List<int>();
        CanTakeObjList = StaticData.CHICK_OBJECT_LIST.ToList();
    }
}
