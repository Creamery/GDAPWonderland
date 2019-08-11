using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {

	Transform mainCameraTf;

	// Use this for initialization
	void Start () {
		mainCameraTf = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		var lookPos =mainCameraTf.position - transform.position;
		lookPos.y = 0;
		var rotation = Quaternion.LookRotation(lookPos);
		transform.rotation = rotation;
		transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
	}
}
