using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBackupCollider : MonoBehaviour {

	private SoldierBackup backup;

	public SoldierBackup GetSoldierBackup() {
		if(this.backup == null) {
			this.backup = GetComponentInParent<SoldierBackup>();
		}
		return this.backup;
	}
}
