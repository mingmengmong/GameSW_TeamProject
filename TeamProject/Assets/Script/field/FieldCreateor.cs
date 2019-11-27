using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCreateor : MonoBehaviour {
	public GameObject MyObjectList;
	public GameObject ObjectList;

	private Datasets datasets;
	private List<int> chickList;
	
	// 최초 오브젝트 생성
	void Start() {
		datasets = GameObject.Find("Dataset").GetComponent<Datasets>();
		chickList = datasets.ChickList;
		for (int i = 0; i < ObjectList.transform.childCount; ++i) {
			if (chickList.Contains(ObjectList.transform.GetChild(i).GetComponent<ObjectValue>().objectValue)) {
				GameObject obj = Instantiate(ObjectList.transform.GetChild(i).gameObject);
				obj.transform.SetParent(MyObjectList.transform);
				obj.transform.position = getPosition();
				obj.GetComponent<Object_Moving>().isMoving = true;
				obj.SetActive(true);
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
