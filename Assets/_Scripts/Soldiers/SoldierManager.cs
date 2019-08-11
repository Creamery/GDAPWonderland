using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierManager : MonoBehaviour {
	[SerializeField] private PlayerManager player;
	//[SerializeField] private Soldier[] soldiers;

	void Awake() {
		this.Instantiate ();
	}

    void Instantiate() {
        //this.soldiers = new Soldier[GameConstants.MAX_DEFENSE_SOLDIER];
    }

	public PlayerManager GetPlayerOwner()
	{
		if(this.player == null) {
			this.player = this.GetComponentInParent<PlayerManager>();
		}
		return player;
	}

}
