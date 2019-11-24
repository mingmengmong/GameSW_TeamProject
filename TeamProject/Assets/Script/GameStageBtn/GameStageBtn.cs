using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStageBtn : MonoBehaviour {
	private Datasets datasets;
    AudioSource audiosource;
	void Start() {
		datasets = GameObject.Find("Dataset").GetComponent<Datasets>();
	}
	
	public void onBuyBtnClick() {
		Debug.Log("but btn click");
		int gold = datasets.Gold;
		if (gold >= 1000) {
			List<int> eggList = datasets.EggList;
			for (int i = 0; i < 10; ++i) {
				if (eggList[i] <= 0) {
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