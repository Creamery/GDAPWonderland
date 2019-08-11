using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class HandTargetManager : ImageTargetBehaviour {
    [SerializeField] private PlayerManager player;

    /// <summary>
    /// Handles image target status updates.
    /// </summary>
    /// <param name="newStatus"></param>
    public override void OnTrackerUpdate(Status newStatus) {
        base.OnTrackerUpdate(newStatus);

        // If target is seen
        if (newStatus == Status.TRACKED) {
            this.PostTrackedHandEvent();
        }

        // If target is not seen
        else if (/*GameManager.gamePhase == GameManager.GamePhases.REPLENISH
            && (*/newStatus == Status.NO_POSE) {
            //Parameters parameters = new Parameters();
            //parameters.PutObjectExtra("player", this.player);
            //EventBroadcaster.Instance.PostEvent(EventNames.WonderlandEvents.ON_REPLENISH_HAND_CARD_TARGET_LOST, parameters);
        }
    }

    /// <summary>
    /// Posts a tracked hand image target event.
    /// </summary>
    public void PostTrackedHandEvent() {
        Debug.Log("<color=green>POST Tracked Hand Image Target</color>");
        Parameters parameters = new Parameters();
        parameters.PutObjectExtra(GameMaster.PLAYER, this.player);
        EventBroadcaster.Instance.PostEvent(EventNames.IMAGE_TARGETS.TRACKED_HAND, parameters);
    }

    public PlayerManager GetPlayer() {
        return this.player;
    }

    public void SetPlayer(PlayerManager playerReference) {
        this.player = playerReference;
    }
}
