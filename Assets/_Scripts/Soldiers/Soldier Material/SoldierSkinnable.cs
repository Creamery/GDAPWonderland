using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoldierSkinnable : SkinnableObject {

    public enum Type {
        DEFAULT, DISABLE, DAMAGE
    }

    [SerializeField] protected Material matDisable;
    [SerializeField] protected Material matDamage;




    /// <summary>
    /// Set the type of material to apply.
    /// </summary>
    /// <param name="type"></param>
    public void Skin(Type type) {
        switch(type) {
            case Type.DEFAULT:
                base.Skin(this.matDefault);
                break;
            case Type.DISABLE:
                base.Skin(this.GetDisabledMaterial());
                break;
            case Type.DAMAGE:
                base.Skin(this.GetDamageMaterial());
                break;
        }
    }

    public Material GetDisabledMaterial() {
        if(this.matDisable == null) {
            this.matDisable = General.GetSoldierDisabledMaterial();
        }
        return this.matDisable;
    }

    public Material GetDamageMaterial() {
        if (this.matDamage == null) {
            this.matDamage = General.GetSoldierDamagedMaterial();
        }
        return this.matDamage;
    }
}
