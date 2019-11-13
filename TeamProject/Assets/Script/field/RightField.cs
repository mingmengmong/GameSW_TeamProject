using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script.field {
	public class RightField : MonoBehaviour {

		public GameObject eggListObj;
		private List<int> eggList;
		private Datasets datasets;

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
					eggList[i]--;
				}
			}

			datasets.EggList = eggList;
		}

		private void setEggPlate() {
			for (int i = 0; i < 10; ++i) {
				if (eggList[i] > 0) {
					eggListObj.transform.GetChild(i).gameObject.SetActive(true);
				} else {
					eggListObj.transform.GetChild(i).gameObject.SetActive(false);
				}
			}
		}

		void Update() {
			eggList = datasets.EggList;
			setEggPlate();
		}
	}
}