using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensitiveObjectMarker : ObjectMarker {
    public enum Type {
        PLAYER_1, PLAYER_2
    }
    [SerializeField] private Type playerType;
    
    public void Refresh() {
        this.Hide();
        switch(this.playerType) {
            case Type.PLAYER_1:
                if (GameMaster.Instance.GetCurPlayer().playerNo == 1) {
                    this.Show();
                }
                break;
            case Type.PLAYER_2:
                if (GameMaster.Instance.GetCurPlayer().playerNo == 2) {
                    this.Show();
                }
                break;
        }
    }
}
