using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlayerSensitiveObjectManager : MonoBehaviour {

    [SerializeField] private List<PlayerSensitiveObjectMarker> playerSensitiveMarkers;

    public void RefreshMarkers() {
        foreach (PlayerSensitiveObjectMarker marker in this.GetPlayerSensitiveMarkers()) {
            marker.Refresh();
        }
    }

    public List<PlayerSensitiveObjectMarker> GetPlayerSensitiveMarkers() {
        if (this.playerSensitiveMarkers == null) {
            this.playerSensitiveMarkers = GetComponentsInChildren<PlayerSensitiveObjectMarker>().ToList();
        }
        return this.playerSensitiveMarkers;
    }
}
