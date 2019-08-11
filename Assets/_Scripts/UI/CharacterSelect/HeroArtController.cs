using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroArtController : MonoBehaviour {

	public const int ALICE = 0;
	public const int HATTER = 1;


	private const string SHOW = "Show";

	[SerializeField] private GameObject[] heroArts;
	private Animator[] anims;

	private int currentShown;

	// Use this for initialization
	void Start () {
		currentShown = -1;
		anims = new Animator[heroArts.Length];
		for (int i = 0; i < heroArts.Length; i++) {
			anims[i] = heroArts[i].GetComponent<Animator>();
		}
	}

	public void ShowHeroArt(int heroId) {
		if(currentShown > -1) {
			anims[currentShown].SetBool(SHOW, false);
		}
		currentShown = heroId;
		heroArts[heroId].SetActive(true);
		anims[heroId].SetBool(SHOW, true);
	}
}
