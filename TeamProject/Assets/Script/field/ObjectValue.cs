using System;
using UnityEngine;

public class ObjectValue : MonoBehaviour {
	// 오브젝트 이름
	public string objectName;
	// 오브젝트 대표 이미지
	public Sprite objectImg;
	// 오브젝트 값
	public int objectValue = -1;
	// 다음 오브젝트 값 (없으면 -1)
	public int nextObjectValue = -1;
	// 진화시 필요한 돈
	public int nextCost = 5000;

	// 오브젝트 진화시 나올 ui
	public GameObject evolutionUI;
	// 백그라운드 오브젝트
	public GameObject backGround;

	private Datasets datasets;
	
	public void Start() {
		datasets = GameObject.Find("Dataset").GetComponent<Datasets>();
	}
	
	// 오브젝트 클릭시 다음 단계 진화가 있고 돈이 충분할시 ui, 백그라운드 active
	public void OnMouseDown() {
		Debug.Log("click : " + objectValue);
		if (nextObjectValue != -1 && datasets.Gold >= nextCost) {
			EvolutionBtn evolBtn = evolutionUI.GetComponent<EvolutionBtn>();
			// 병아리 진화에 필요한 데이터 셋팅
			evolBtn.Value = this.objectValue;
			evolBtn.NextValue = this.nextObjectValue;
			evolBtn.Cost = this.nextCost;
			backGround.SetActive(true);
			evolutionUI.SetActive(true);
		}
	}
}