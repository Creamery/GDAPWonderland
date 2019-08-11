using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnBtn : MonoBehaviour {

	private Button btn;
	private void Awake() {
		btn = GetComponent<Button>();
	}

	public void EndTurn() {
        GameMaster.Instance.EndTurn();
        SoundManager.Instance.Play(AudibleNames.Button.DEFAULT);
        //GameMaster.Instance.SetInActionPhase(false);
    }

	public void SetBtnEnable(bool val) {
		btn.enabled = val;
	}
}
