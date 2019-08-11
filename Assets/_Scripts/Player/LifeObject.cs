using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeObject : MonoBehaviour {

	const string HP_ANIM_DISAPPEAR = "Disappear";
    public const string PLAYER_REDUCED = "player";
    [Header("Setup")]
	[SerializeField] private GameObject hpIndicatorPrefab;
	[SerializeField] private GameObject hpIndicatorContainer;
	private PlayerManager player;
	private SoldierDefenseGroup parent;
	[Header("Config")]
	//[SerializeField] private float xDistanceBetweenInstances = 0.35f;
	[Header("Debugging")]
	[SerializeField] private LifeBarObjectMarker[] hpInstances;

	// Use this for initialization
	void Start () {
		Initialize();
	}

	private void OnDestroy() {
		EventBroadcaster.Instance.RemoveObserver(EventNames.ARENA.REDUCE_HEALTH);
	}

	public void Initialize() {
        //hpInstances = new GameObject[GameConstants.MAX_PLAYER_LIFE];

        if(this.hpInstances == null || this.hpInstances.Length == 0) {
            hpInstances = GetComponentsInChildren<LifeBarObjectMarker>(); // Ensure it's 3
            foreach(LifeBarObjectMarker meshObject in hpInstances) {
                meshObject.SetMaterial(General.GetLifeFillMaterial(player.GetPlayerNumber()+""));
            }
        }

        //float xOffset = (hpInstances.Length - 1) * xDistanceBetweenInstances / 2;
        //if (hpInstances.Length % 2 == 1) {
        //    if odd
        //    xOffset = (hpInstances.Length - 1) / 2 * xDistanceBetweenInstances;
        //}
        //for (int i = 0; i < hpInstances.Length; i++) {
        //    hpInstances[i] = Instantiate(hpIndicatorPrefab, hpIndicatorContainer.transform);
        //    hpInstances[i].transform.localPosition = new Vector3(xOffset, hpInstances[i].transform.localPosition.y, 0);
        //    xOffset -= xDistanceBetweenInstances;
        //}

        EventBroadcaster.Instance.AddObserver(EventNames.ARENA.REDUCE_HEALTH, ReduceHealth);
    }

    public void ReduceHealth(Parameters parameters) {
        PlayerManager playerReduced = parameters.GetObjectExtra(PLAYER_REDUCED) as PlayerManager;
        if(playerReduced == this.GetPlayer()) {
            hpInstances[hpInstances.Length - 1].SetMaterial(General.GetLifeEmptyMaterial()); //.GetComponent<Animator>().SetTrigger(HP_ANIM_DISAPPEAR);
            Array.Resize<LifeBarObjectMarker>(ref hpInstances, hpInstances.Length - 1);
            Debug.Log("new array size: " + hpInstances.Length);
        }
    }

    public SoldierDefenseGroup GetParent() {
		if (this.parent == null)
			this.parent = GetComponentInParent<SoldierDefenseGroup>();
		return this.parent;
	}

	public PlayerManager GetPlayer() {
		if (this.player == null)
			this.player = GetParent().GetPlayer();
		return this.player;
	}
}
