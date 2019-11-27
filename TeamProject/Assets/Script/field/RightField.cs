using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RightField : MonoBehaviour {

	public GameObject eggListObj;

	private List<int> eggList;
	private Datasets datasets;

	private int hatchingCnt = 0;
		
	private void Start() {
		datasets = GameObject.Find("Dataset").GetComponent<Datasets>();
		eggList = datasets.EggList;
		if (eggList == null) {
			Debug.Log("egg list is null");
		}
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
					Debug.Log("hatchingCnt : " + hatchingCnt);
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

	
	public GameObject alramUI;
	public Text nameText;
	public Image image;
	public GameObject backGround;
	public float popupTime = 2000;
	public float termTime = 200;

	private bool isAlram = false;
	private float timer = 0;
	void Update() {
		eggList = datasets.EggList;
		setEggPlate();

		if (hatchingCnt > 0 && !isAlram) {
			--hatchingCnt;
			isAlram = true;
			timer = 0;


			int randomValue = datasets.CanTakeObjList[Random.Range(0, datasets.CanTakeObjList.Count)];
			datasets.CanTakeObjList.Remove(randomValue);
			datasets.ChickList.Add(randomValue);
			datasets.TakeObjList.Add(randomValue);
			datasets.saveData();
			
			datasets.NewObjList.Add(randomValue);


			for (int i = 0; i < ObjectList.transform.childCount; ++i) {
				if (ObjectList.transform.GetChild(i).GetComponent<ObjectValue>().objectValue == randomValue) {
					GameObject obj = Instantiate(ObjectList.transform.GetChild(i).gameObject);
					obj.transform.SetParent(MyObjectList.transform);
					obj.transform.position = getPosition();
					obj.SetActive(true);


					image.sprite = obj.GetComponent<ObjectValue>().objectImg;
					nameText.text = obj.GetComponent<ObjectValue>().objectName;
					alramUI.SetActive(true);
					backGround.SetActive(true);
					
					
					break;
				}
			}
		}  
		if(isAlram){
			timer += Time.deltaTime * 1000;
			if (timer > (popupTime + termTime)) {
				isAlram = false;
			} else if (timer > popupTime) {
				alramUI.SetActive(false);
				backGround.SetActive(false);
			}
		}
	}
		
	private Vector3 getPosition() {
		return new Vector3(
			Random.Range(StaticData.OBJECT_X_POS_MIN, StaticData.OBJECT_X_POS_MAX),
			Random.Range(StaticData.OBJECT_Y_POS_MIN, StaticData.OBJECT_Y_POS_MAX),
			0f
		);
	}
	
}