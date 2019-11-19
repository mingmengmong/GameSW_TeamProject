using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestBtn : MonoBehaviour {
	private Datasets datasets;

	void Start() {
		datasets = GameObject.Find("Dataset").GetComponent<Datasets>();
	}
	
	public void onBuyBtnClick() {
		int gold = datasets.Gold;
		if (gold >= 1000) {
			List<int> eggList = datasets.EggList;
			for (int i = 0; i < 10; ++i) {
				if (eggList[i] == 0) {
					eggList[i] = 1000;
					gold -= 1000;
					datasets.Gold = gold;
					datasets.EggList = eggList;

					break;
				}
			}
		}
	}

    public void goToDictionaryClick()
    {
        SceneManager.LoadScene(3);
    }

    public void goToFieldClick()
    {
        SceneManager.LoadScene(2);
    }
}