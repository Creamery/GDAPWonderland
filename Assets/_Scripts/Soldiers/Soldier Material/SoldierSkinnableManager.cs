using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SoldierSkinnableManager : SkinnableObjectManager {

    [SerializeField] private List<SoldierSkinnable> soldierMaterials;



    [SerializeField] bool triggerDefault;
    [SerializeField] bool triggerDisable;
    [SerializeField] bool triggerDamage;


    public void Update() {
        if(triggerDefault) {
            this.UncheckAll();
            this.SkinObject(SoldierSkinnable.Type.DEFAULT);
        }
        else if (triggerDisable) {
            this.UncheckAll();
            this.SkinObject(SoldierSkinnable.Type.DISABLE);
        }
        else if (triggerDamage) {
            this.UncheckAll();
            this.SkinObject(SoldierSkinnable.Type.DAMAGE);
        }
    }

    /// <summary>
    /// Call this function to skin all children skinnable objects
    /// </summary>
    /// <param name="skinType"></param>
    public void SkinObject(SoldierSkinnable.Type skinType) {
        foreach(SoldierSkinnable skinnable in this.GetSoldierMaterials()) {
            skinnable.Skin(skinType);
        }
    }

    // TODO: Testing
    public void UncheckAll() {
        this.triggerDefault = false;
        this.triggerDisable = false;
        this.triggerDamage = false;
    }


    /// <summary>
    /// Soldier material getter.
    /// </summary>
    /// <returns></returns>
    public List<SoldierSkinnable> GetSoldierMaterials() {
        if(this.soldierMaterials == null || this.soldierMaterials.Count == 0) {
            this.soldierMaterials = GetComponentsInChildren<SoldierSkinnable>().ToList<SoldierSkinnable>();
        }
        return this.soldierMaterials;
    }
}
