using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeObjectCollider : MonoBehaviour {

	private const string BLOCK = "Block";

	[Header("Setup")]
	[SerializeField] private Animator barrierAnim;
	[SerializeField] private OutlinedModel outline;
	[Header("Optional")]
	[SerializeField] private LifeObject parent;
	private PlayerManager playerOwner;

	private void Start() {
		EnableOutline(true);
		EnableOutline(false);
	}

	public bool IsHit(PlayerManager shotOwner) {
		if (ValidateHit(shotOwner)) {
			GetPlayer().Damage();
			return true;
		}
		else {
			TriggerBlockAnim();
			return false;
		}
	}

	private bool ValidateHit(PlayerManager shotOwner) {
		return GetPlayer().GetCardManager().GetDefenseManager().DoNeedReplenish() && shotOwner != GetPlayer();
	}

	public void TriggerBlockAnim() {
		barrierAnim.SetTrigger(BLOCK);
	}


	public void EnableOutline(bool val) {
		outline.enabled = val;
	}

	public LifeObject GetParent() {
		if (this.parent == null)
			this.parent = GetComponentInParent<LifeObject>();
		return this.parent;
	}

	public PlayerManager GetPlayer() {
		if (this.playerOwner == null)
			this.playerOwner = GetParent().GetPlayer();
		return this.playerOwner;
	}
}
