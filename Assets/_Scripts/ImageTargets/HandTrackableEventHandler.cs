using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class HandTrackableEventHandler : DefaultTrackableEventHandler {

    [SerializeField] private PlayerManager player;

   
    public PlayerManager GetPlayer() {
        return this.player;
    }

    public void SetPlayer(PlayerManager playerReference) {
        this.player = playerReference;
    }

    /// <summary>
    /// Called once image target is found.
    /// </summary>
    protected override void OnTrackingFound() {
        base.OnTrackingFound();
        this.PostTrackedHandEvent();
    }

    /// <summary>
    /// Called once image target is lost.
    /// </summary>
    protected override void OnTrackingLost() {
        base.OnTrackingLost();
        this.PostLostHandEvent();
    }

    /// <summary>
    /// Post tracked hand image target event.
    /// </summary>
    public void PostTrackedHandEvent() {
        Debug.Log("<color=green>FOUND Hand Image Target</color>");
		Parameters parameters = new Parameters();
		parameters.PutObjectExtra(GameMaster.PLAYER, this.player);
		EventBroadcaster.Instance.PostEvent(EventNames.IMAGE_TARGETS.TRACKED_HAND, parameters);
	}

    public void PostLostHandEvent() {
        Debug.Log("<color=red>LOST Hand Image Target</color>");
    }
}
