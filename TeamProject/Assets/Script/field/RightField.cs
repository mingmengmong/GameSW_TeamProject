using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.field {
	public class RightField : MonoBehaviour {

		public GameObject eggListCanvas;
		private List<int> eggList;

		private void Start() {
			if (eggList == null) {
				eggList = new List<int>();
				eggList.Add(1000);
				eggList.Add(1000);
				eggList.Add(1000);
			}
		}

		private void OnMouseDown() {
			Debug.Log("right field click");
		}
	}
}