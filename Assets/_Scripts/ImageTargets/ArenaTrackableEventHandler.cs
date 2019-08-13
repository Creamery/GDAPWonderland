using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaTrackableEventHandler : DefaultTrackableEventHandler {

	/// <summary>
	/// Called once image target is found.
	/// </summary>
	protected override void OnTrackingFound() {
		base.OnTrackingFound();

        var floatingCardBackComponents = GetComponentsInChildren<SoldierBackupFloatingCard>(true);

        // Refresh floating card backs:
        foreach (var component in floatingCardBackComponents)
            ((SoldierBackupFloatingCard)component).Refresh();
    }

	/// <summary>
	/// Called once image target is lost.
	/// </summary>
	protected override void OnTrackingLost() {
		base.OnTrackingLost();
	}
}
