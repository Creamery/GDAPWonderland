using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {
	private string CurrentCollider = "";
	// Use this for initialization
	void Start () {
		
	}

	private void OnTriggerStay(Collider col) {
		if (!CurrentCollider.Equals(col.name)) {
			CurrentCollider = col.name;
			Debug.Log("ENTERED COLLISION: " + CurrentCollider);
		}
	}

	void OnTriggerExit(Collider col){
	}

	public string GetCurrentCollider(){
		return CurrentCollider;
	}
}
