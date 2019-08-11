using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour {

	public const int CLUBS = 0;
	public const int SPADES = 1;
	public const int HEARTS = 2;
	public const int DIAMONDS = 3;
	public const int COMBI = 4;

	private ParticleSystem ps;
	private int curColor;
	[SerializeField] private Color[] colors;
	[SerializeField] private float transitionDuration = 1.5f;

	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem>();
		curColor = -1;
	}

	private void Update() {
		transform.LookAt(Camera.main.transform);
	}

	public void Activate(int colorIndex) {
		var main = ps.main;
		main.startColor = colors[colorIndex];
		curColor = colorIndex;
		ps.Play();
	}

	public void ChangeColor(int toColorIndex) {
		if (curColor == toColorIndex)
			return; 

		StartCoroutine(ColorTransition(curColor, toColorIndex));
		curColor = toColorIndex;
		
	}

	IEnumerator ColorTransition(int prevColor, int toColorIndex) {
		var main = ps.main;
		float t=0;
		while(t <= 1) { 
			main.startColor = Color.Lerp(colors[prevColor], colors[toColorIndex], t);
			t += Time.deltaTime / transitionDuration;
			yield return null;
		}
	}

	public void Stop() {
		ps.Stop();
		curColor = -1;
	}
}
