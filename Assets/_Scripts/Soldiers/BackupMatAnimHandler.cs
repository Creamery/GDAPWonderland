using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackupMatAnimHandler : MonoBehaviour {

	public const string OPEN_STATE = "ShouldOpen";

	private Animator anim;
	private SoldierBackup[] backups;
	private bool isOpened;
	public bool HasOpened {
		get { return isOpened; }
	}

	private void Awake() {
		anim = GetComponent<Animator>();
		backups = GetComponentsInChildren<SoldierBackup>();
		isOpened = false;
	}

	public bool Open(bool val) {
		if (!val) {
			foreach (SoldierBackup b in backups) {
				if (b.IsHidden)
					return false;       // Failed Hide. Some backupsoldiers still not hidden.
			}
			foreach (SoldierBackup b in backups) {
				b.SetHidden(true);
			}
		}
		anim.SetBool(OPEN_STATE, val);
		isOpened = val;
		return true;
	}

	public void IsOpened() {
		foreach (SoldierBackup b in backups) {
			b.SetHidden(false);
		}
	}
	
}
