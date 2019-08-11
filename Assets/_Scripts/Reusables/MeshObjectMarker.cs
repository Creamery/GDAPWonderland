using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent class for simple identification functions if mesh objects (those used
/// in place of tags).
/// </summary>
/// 
public class MeshObjectMarker : ObjectMarker {
    [SerializeField] private MeshRenderer meshRenderer;

    /// <summary>
    /// Changes the material of the meshrenderer
    /// </summary>
    public virtual void SetMaterial(Material newMaterial) {
        if(this.GetMeshRenderer() != null) {
            this.GetMeshRenderer().material = newMaterial;
        }
    }

    public MeshRenderer GetMeshRenderer() {
        if(this.meshRenderer == null) {
            this.meshRenderer = GetComponent<MeshRenderer>();
        }
        return this.meshRenderer;
    }
}
