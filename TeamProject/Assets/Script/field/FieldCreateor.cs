using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCreateor : MonoBehaviour {
	public GameObject MyObjectList;
	public GameObject ObjectList;

	private Datasets datasets;
	private List<int> chickList;
	
	void Start() {
		datasets = GameObject.Find("Dataset").GetComponent<Datasets>();
		chickList = datasets.ChickList;
		for (int i = 0; i < chickList.Count; ++i) {
			GameObject obj = Instantiate(ObjectList.transform.GetChild(chickList[i]).gameObject);
			obj.transform.SetParent(MyObjectList.transform);
			obj.transform.position = getPosition();
			obj.GetComponent<Object_Moving>().isMoving = true;
			obj.SetActive(true);
		}
	}

	void Update() {
		if (chickList.Count != datasets.ChickList.Count) {
			// 데이터가 추가될시
			for (int i = 0; i < datasets.ChickList.Count; ++i) {
				if (!chickList.Contains(datasets.ChickList[i])) {
					GameObject obj = Instantiate(ObjectList.transform.GetChild(datasets.ChickList[i]).gameObject);
					obj.transform.SetParent(MyObjectList.transform);
					obj.transform.position = getPosition();
					obj.SetActive(true);
				}
			}
		}

		chickList = datasets.ChickList;
	}

	private Vector2 getPosition() {
		return new Vector3(
			Random.Range(StaticData.OBJECT_X_POS_MIN, StaticData.OBJECT_X_POS_MAX),
			Random.Range(StaticData.OBJECT_Y_POS_MIN, StaticData.OBJECT_Y_POS_MAX),
			-5f
		);
	}
}
