using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectAnnouncement : MonoBehaviour {

	public const string PLAYERNO_PARAM = "playerNo";
	private const string ANIM_TRIGGER_SHOW = "show";

	[SerializeField] GameObject player1Graphic;
	[SerializeField] GameObject player2Graphic;

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();

		EventBroadcaster.Instance.AddObserver(EventNames.UI.SHOW_CHARSELECT_ANNOUNCEMENT, ShowAnnouncement);
		player1Graphic.SetActive(false);
		player2Graphic.SetActive(false);
	}

	private void OnDestroy() {
		EventBroadcaster.Instance.RemoveObserver(EventNames.UI.SHOW_CHARSELECT_ANNOUNCEMENT);
	}

	public void ShowAnnouncement(Parameters param) {
		int curPlayerNo = param.GetIntExtra(PLAYERNO_PARAM, 1);
		anim.SetTrigger(ANIM_TRIGGER_SHOW);
		SwitchGraphic(curPlayerNo);
	}

	private void SwitchGraphic(int playerNo) {
		if(playerNo == 1) {
			player1Graphic.SetActive(true);
			player2Graphic.SetActive(false);
		}
		else {
			player2Graphic.SetActive(true);
			player1Graphic.SetActive(false);
		}
	}
}
