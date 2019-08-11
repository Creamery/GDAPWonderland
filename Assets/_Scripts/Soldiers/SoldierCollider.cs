using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierCollider : MonoBehaviour {
    private Soldier soldier;

    public Soldier GetSoldier() {
        if(this.soldier == null) {
            this.soldier = GetComponentInParent<Soldier>();
        }
        General.LogNull(soldier);
        return this.soldier;
    }
}
