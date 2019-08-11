using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenericTooltip : MonoBehaviour {

	public const string SHOW_KEY = "IsShown";

	[SerializeField] private TextMeshProUGUI textUI;
	private Animator anim;
	bool isShown;
	public bool Shown {
		get { return isShown; }
	}

	private void Start() {
		isShown = false;
		anim = GetComponent<Animator>();
	}

	public void ShowText(string text) {
		textUI.text = text;
		if (text == "")
			IsShown(false);
		else
			IsShown(true);
	}

	public void IsShown(bool val) {
		anim.SetBool(SHOW_KEY, val);
		isShown = val;
	}

    public void PlayDefaultSound() {
        SoundManager.Instance.Play(AudibleNames.Button.DEFAULT);
    }
}
