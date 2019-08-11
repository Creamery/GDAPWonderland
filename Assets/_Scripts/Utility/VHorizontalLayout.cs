using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class VHorizontalLayout : MonoBehaviour {

	public float distanceBetweenCards;
	public float maxLength;
	public float maxTwist;
	public float amplitude;

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
		
		if(childrenTf.Length == 1) {
			childrenTf[0].localPosition = Vector3.zero;
			childrenTf[0].localEulerAngles = Vector3.zero;
			return;
		}

		//Layouting
		//Distance
		if(childrenTf.Length * distanceBetweenCards > maxLength)
			distanceBetweenCards = maxLength / (childrenTf.Length - 1);

		float curXOffset = -distanceBetweenCards * (childrenTf.Length - 1) / 2;
		//Twist
		float twistStep = maxTwist * 2 / (childrenTf.Length - 1);
		float curTwist = maxTwist;
		//YPos
		float yStep = amplitude / (childrenTf.Length / 2);
		float centerIndex = (childrenTf.Length - 1) / 2f;

		float curYPos = 0f;
		//Adjusting
		int i = 0;
		foreach(RectTransform rTf in childrenTf) {
			//Distance and Height
			rTf.localPosition = new Vector3(curXOffset, curYPos);
			curXOffset += distanceBetweenCards;

			i++;
			if (i == (centerIndex + 0.5))
				curYPos += 0;
			else if (i > centerIndex)
				curYPos -= yStep;
			else
				curYPos += yStep;
			//Twist
			rTf.localEulerAngles = new Vector3(0, 0, curTwist);
			curTwist -= twistStep;
		}
	}
}
