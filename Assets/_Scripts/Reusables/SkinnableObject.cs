using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Handles material switching. Requires meshRenderer and a default material variable.
/// </summary>
public class SkinnableObject : MonoBehaviour {

    [SerializeField] protected MeshRenderer meshRenderer;
    [SerializeField] protected Material matDefault;

    private void Awake() {
        if (this.matDefault == null) {
            this.matDefault = this.GetMeshRenderer().material;
        }
    }

    public virtual void Skin(Material newMaterial) {
        if (newMaterial != null) {
            this.GetMeshRenderer().material = newMaterial;
        }
    }
    public MeshRenderer GetMeshRenderer() {
        if (this.meshRenderer == null) {
            this.meshRenderer = GetComponent<MeshRenderer>();
        }
        return this.meshRenderer;
    }

}
