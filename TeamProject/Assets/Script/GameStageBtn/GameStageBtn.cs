using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStageBtn : MonoBehaviour {
	private Datasets datasets;
    AudioSource audiosource;
    void Start()
    {
		datasets = GameObject.Find("Dataset").GetComponent<Datasets>();
	}
	
    /// <summary>
    /// 계란 구입 버튼 클릭 이벤트
    /// </summary>
	public void onBuyBtnClick() {
		Debug.Log("but btn click");
		int gold = datasets.Gold;
		if (gold >= 1000) {
			List<int> eggList = datasets.EggList;
			// 계란판 인덱스 검색 및 현재 계란 개수 값 가져오기
			int index = -1;
			int eggCnt = 0;
			for (int i = 0; i < 10; ++i) {
				if (eggList[i] <= 0) {
					if (index == -1) {
						index = i;
					}
				} else {
					++eggCnt;
				}
			}

			// 계란이 다 안차있고 획득 가능한 오브젝트가 존재할시 계란 구입 성공
			if ( eggCnt < 10 && datasets.CanTakeObjList.Count > eggCnt) {
				eggList[index] = 1000;
				gold -= 1000;
				datasets.Gold = gold;
				datasets.EggList = eggList;
			}
		}
	}

	

    public void goToDictionaryClick()
    {
        SceneManager.LoadScene(2);
    }

    public void goToFieldClick()
    {
        SceneManager.LoadScene(1);
    }

    public void clicksound()
    {
        audiosource = GetComponent<AudioSource>();
        audiosource.Play();
    }
}