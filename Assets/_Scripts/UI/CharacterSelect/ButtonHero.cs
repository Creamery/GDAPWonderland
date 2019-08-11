using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHero : MonoBehaviour {

	[SerializeField] private Hero.Type hero;
	[SerializeField] private Image selectImg;
	private CharacterSelectManager parent;

	// Use this for initialization
	void Start () {
		parent = GetComponentInParent<CharacterSelectManager>();
	}

	public void OnClick() {
        SoundManager.Instance.Play(AudibleNames.Button.DEFAULT);
		parent.SelectHero(this);
	}

	public Hero.Type GetHero() {
		return hero;
	}

	public void SetActive(bool val) {
		selectImg.gameObject.SetActive(val);
		if (val) {
			selectImg.color = parent.IsPlayer1Turn() ? CharacterSelectManager.PLAYER1_COLOR : CharacterSelectManager.PLAYER2_COLOR;
		}
	}

	public void SetColor(Color c) {
		selectImg.color = c;
	}
}
