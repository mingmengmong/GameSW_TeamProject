using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RightField : MonoBehaviour {

	public GameObject eggListObj;

	private List<int> eggList;
	private Datasets datasets;

	private int hatchingCnt = 0;
		
	private void Start() {
		datasets = GameObject.Find("Dataset").GetComponent<Datasets>();
		eggList = datasets.EggList;
		Debug.Log(eggList.Count);
		Debug.Log(String.Join(", ", eggList.ToArray()));
		setEggPlate();
	}

	private void OnMouseDown() {
		Debug.Log("right field click");
		for (int i=0; i<10; ++i) {
			if (eggList[i] > 0) {
//					eggList[i]--;
				eggList[i] -= 50;
				if (eggList[i] <= 0) {
					++hatchingCnt;
				}
			}
		}

		datasets.EggList = eggList;
	}

	private void setEggPlate() {
		for (int i = 0; i < 10; ++i) {
			if (eggList[i] > 0) {
				if (eggList[i] < 400) {
					eggListObj.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
					eggListObj.transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
				}else if (eggList[i] < 700) {
					eggListObj.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
					eggListObj.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
				} else {
					eggListObj.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
					eggListObj.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
				}
				eggListObj.transform.GetChild(i).gameObject.SetActive(true);
			} else {
				eggListObj.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}

	public GameObject ObjectList;
	public GameObject MyObjectList;
		
	void Update() {
		eggList = datasets.EggList;
		setEggPlate();

		if (hatchingCnt > 0) {
			--hatchingCnt;
			List<int> myObjectList = datasets.ChickList;
			int size = ObjectList.transform.childCount;
			List<int> addList = new List<int>();
			for (int i = 0; i < size; ++i) {
				if (!myObjectList.Contains(i)) {
					addList.Add(i);
				}
			}

			// 일단 동일 확률
			int randomIndex = Random.Range(0, addList.Count);
			myObjectList.Add(addList[randomIndex]);
			datasets.ChickList = myObjectList;
				
			GameObject obj = Instantiate(ObjectList.transform.GetChild(addList[randomIndex]).gameObject);
			obj.transform.SetParent(MyObjectList.transform);
			obj.transform.position = getPosition();
			obj.SetActive(true);
		}
	}
		
	private Vector3 getPosition() {
		return new Vector3(
			Random.Range(StaticData.OBJECT_X_POS_MIN, StaticData.OBJECT_X_POS_MAX),
			Random.Range(StaticData.OBJECT_Y_POS_MIN, StaticData.OBJECT_Y_POS_MAX),
			-5f
		);
	}
	
}