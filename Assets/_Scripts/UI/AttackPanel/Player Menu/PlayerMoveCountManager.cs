using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveCountManager : MonoBehaviour {

    [SerializeField] private PlayerMoveCountObjectMarker moveCountObject;

    public void UpdateCount(int moveCount) {
        this.GetMoveCountObject().SetCount(moveCount);
    }

    private PlayerMoveCountObjectMarker GetMoveCountObject() {
        if(this.moveCountObject == null) {
            this.moveCountObject = GetComponentInChildren<PlayerMoveCountObjectMarker>();
        }
        return this.moveCountObject;
    }
}
