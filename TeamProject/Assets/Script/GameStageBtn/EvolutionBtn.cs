using UnityEngine;

public class EvolutionBtn : MonoBehaviour {
	public GameObject myObjList;
	public GameObject objList;
	public GameObject backGround;
	
	private int objectValue;
	private int nextValue;
	private int cost;

	public int Value {
		set { this.objectValue = value; }
	}

	public int NextValue {
		set { this.nextValue = value; }
	}

	public int Cost {
		set { this.cost = value; }
	}


	private Datasets datasets;

	public void Start() {
		datasets = GameObject.Find("Dataset").GetComponent<Datasets>();
	}
	
	/// <summary>
	/// 확인버튼 클릭 이벤트
	/// 기존 오브젝트 제거
	/// 새로운 오브젝트 추가
	/// 데이터 업데이트 후 저장
	/// </summary>
	public void onOKBtnClick() {
		GameObject prevObj = null;
		for (int i = 0; i < myObjList.transform.childCount; ++i) {
			GameObject obj = myObjList.transform.GetChild(i).gameObject;
			if (obj.GetComponent<ObjectValue>().objectValue == objectValue) {
				prevObj = obj;
			}
		}

		GameObject nextObj = null;
		for (int i = 0; i < objList.transform.childCount; ++i) {
			GameObject obj = objList.transform.GetChild(i).gameObject;
			if (obj.GetComponent<ObjectValue>().objectValue == nextValue) {
				nextObj = Instantiate(obj);
				nextObj.transform.SetParent(myObjList.transform);
				nextObj.transform.position = getPosition();
			}
		}
		

		// 데이터 저장
		int gold = datasets.Gold - cost;
		datasets.Gold = gold;
		datasets.ChickList.Remove(objectValue);
		datasets.ChickList.Add(nextValue);
		datasets.saveData();

		// 새로운 오브젝트 표기
		datasets.NewObjList.Add(nextValue);
		
		Destroy(prevObj);
		nextObj.SetActive(true);
		this.gameObject.SetActive(false);
		backGround.SetActive(false);
	}

	/// <summary>
	/// 취소 버튼 클릭 이벤트
	/// 초기화
	/// </summary>
	public void onCancelBtnClick() {
		objectValue = -1;
		nextValue = -1;
		cost = 0;
		this.gameObject.SetActive(false);
		backGround.SetActive(false);
	}
	
	
	private Vector3 getPosition() {
		return new Vector3(
			Random.Range(StaticData.OBJECT_X_POS_MIN, StaticData.OBJECT_X_POS_MAX),
			Random.Range(StaticData.OBJECT_Y_POS_MIN, StaticData.OBJECT_Y_POS_MAX),
			0f
		);
	}
}