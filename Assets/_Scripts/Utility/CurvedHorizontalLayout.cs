using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CurvedHorizontalLayout : MonoBehaviour {

	public float twistInterval;
	public float maxTwist;
	public float circleRadius;

	private RectTransform[] childrenTf;
	
	// Update is called once per changes made in the scene
	void Update() {
		//Initialization
		List<RectTransform> listTf = new List<RectTransform>();
		foreach (RectTransform t in transform) {
			if (t.parent == transform)
				listTf.Add(t);
		}
		childrenTf = listTf.ToArray();

		if (childrenTf.Length < 1)
			return;

		// Layouting
		//Pivot
		foreach (RectTransform card in childrenTf) {
			card.pivot = new Vector2(card.pivot.x, -circleRadius);
		}

		if (childrenTf.Length == 1) {
			childrenTf[0].localPosition = Vector3.zero;
			childrenTf[0].localEulerAngles = Vector3.zero;
			return;
		}
		float twistStep = twistInterval;
		//Twist
		if (childrenTf.Length * twistInterval > maxTwist * 2)
			twistStep = maxTwist * 2 / (childrenTf.Length - 1);
		float curTwist = twistStep * (childrenTf.Length - 1) / 2;
		foreach(RectTransform rTf in childrenTf) {
			//Twist
			rTf.localPosition = Vector3.zero;
			rTf.localEulerAngles = new Vector3(0, 0, curTwist);
			curTwist -= twistStep;
		}
	}
}
