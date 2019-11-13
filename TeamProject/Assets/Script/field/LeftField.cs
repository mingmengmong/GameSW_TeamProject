using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
 
public class LeftField : MonoBehaviour {

	public Text goldText;
	
	//TODO: 추후에 PlayerPref 바인딩 클레스에서 변수 받아오는 식으로 변경
	private int gold = 0;
	private Datasets datasets;
	
	private void Start() {
		Debug.Log(GameObject.Find("Dataset").name);
		datasets = GameObject.Find("Dataset").GetComponent<Datasets>();
		gold = datasets.Gold;
		goldText.text = gold.ToString("N0");
	}

	private void OnMouseDown() {
		Debug.Log("left field click");
		gold += 111;
		datasets.Gold = gold;
		goldText.text = gold.ToString("N0");
	}

	void Update() {
		gold = datasets.Gold;
		goldText.text = gold.ToString("N0");
	}
}