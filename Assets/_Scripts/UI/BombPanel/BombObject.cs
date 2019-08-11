using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombObject : MonoBehaviour {

	private BombPanel parent;
	private BombObjectAnimatable anim;
	private Vector3 originalPos;

	private void Start() {
		anim = GetComponent<BombObjectAnimatable>();
		originalPos = transform.localPosition;
		parent = GetComponentInParent<BombPanel>();
	}

	public void Move(Vector3 newPos) {
		newPos.z = 1;
		newPos = Camera.main.ScreenToWorldPoint(newPos);
		transform.position = newPos;
	}

	public void ResetPosition() {
		transform.localPosition = originalPos;
	}

	public void Explode() {
		anim.Throw();
	}

	void OnExplodeAnimComplete() {
		parent.Throw();
	}
}
