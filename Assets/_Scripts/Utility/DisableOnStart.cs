using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnStart : MonoBehaviour {

	[SerializeField] private int numberOfFramesBeforeDisable = 1;

	// Use this for initialization
	void Start () {
		StartCoroutine(DisableOnCountdown());
	}
	
	IEnumerator DisableOnCountdown() {
		int i = 0;
		while (i < numberOfFramesBeforeDisable) {
			yield return null;
			i++;
		}
		gameObject.SetActive(false);
	}
}
