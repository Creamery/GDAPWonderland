using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackupMatCollider : MonoBehaviour {

	private SoldierDefenseGroup parent;

	private void Awake() {
		parent = GetComponentInParent<SoldierDefenseGroup>();
	}

	public SoldierDefenseGroup GetParent() {
		if (this.parent == null) {
			this.parent = GetComponentInParent<SoldierDefenseGroup>();
		}
		return this.parent;
	}

	public PlayerManager GetPlayer() {
		return GetParent().GetPlayer();
	}
}