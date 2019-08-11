using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBackupAnimListener : MonoBehaviour {

	private SoldierBackup parent;

	private void Awake() {
		parent = this.GetComponentInParent<SoldierBackup>();
	}

	public void OnCompleteAnim() {
		parent.CompleteHideAnim();
	}

	public void OnOpenComplete() {
		parent.CompleteOpenAnim();
	}
}
