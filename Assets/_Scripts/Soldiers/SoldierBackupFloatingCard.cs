using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBackupFloatingCard : MonoBehaviour {
    [SerializeField] MeshRenderer cardRenderer;
    [SerializeField] MeshRenderer[] childRenderers;



    [SerializeField] Material matParentOriginal;
    [SerializeField] Material matParentTransluscent;

    [SerializeField] Material matChildOriginal;
    [SerializeField] Material matChildTransluscent;


    protected bool isEnabled = true;
    protected bool isTransluscent = false;



    protected const string strParentTransluscent = "FloatingCardMaterialInvisible";
    protected const string strChildTransluscent = "TranslucentFloatingCardMaterial";



    private void Awake() {
        this.GetParentOriginalMaterial();
        this.GetChildOriginalMaterial();
    }

    public void Refresh() {
        if(isEnabled) {
            this.Show();
        }
        else {
            this.Hide();
        }
    }
    /*
    private void FixedUpdate() {
        this.Refresh();
    }
    */
    public void Show() {
        this.GetCardRenderer().enabled = true;
        // Handle children
        foreach (var component in this.GetChildRenderers())
            ((MeshRenderer)component).enabled = true;
        isEnabled = true;
    }

    public void Hide() {
        this.GetCardRenderer().enabled = false;

        // Handle children
        foreach (var component in this.GetChildRenderers())
            ((MeshRenderer)component).enabled = false;

        isEnabled = false;
    }

	//TODO: write code here
	public void SetTranslucent(bool val) {
        this.isTransluscent = val;

        if(isTransluscent) {
            this.GetCardRenderer().material = this.GetParentTransluscentMaterial();
            this.GetChildRenderers()[0].material = this.GetChildTransluscentMaterial();
        }
        else {
            this.GetCardRenderer().material = this.GetParentOriginalMaterial();
            this.GetChildRenderers()[0].material = this.GetChildOriginalMaterial();
        }

	}

    public MeshRenderer GetCardRenderer() {
        if(this.cardRenderer == null) {
            this.cardRenderer = GetComponent<MeshRenderer>();
        }
        return this.cardRenderer;
    }

    public MeshRenderer[] GetChildRenderers() {
        if (this.childRenderers == null || this.childRenderers.Length == 0) {
            this.childRenderers = GetComponentsInChildren<MeshRenderer>();
        }
        return this.childRenderers;
    }




    public Material GetParentTransluscentMaterial() {
        if (this.matParentTransluscent == null) {
            this.matParentTransluscent = General.GetMaterial(strParentTransluscent);
        }
        return this.matParentTransluscent;
    }


    public Material GetChildTransluscentMaterial() {
        if (this.matChildTransluscent == null) {
            this.matChildTransluscent = General.GetMaterial(strChildTransluscent);
        }
        return this.matChildTransluscent;
    }



    public Material GetParentOriginalMaterial() {
        if (this.matParentOriginal == null) {
            this.matParentOriginal = GetComponent<MeshRenderer>().material;
        }
        return this.matParentOriginal;
    }

    public Material GetChildOriginalMaterial() {
        if (this.matChildOriginal == null) {
            this.matChildOriginal = GetComponentInChildren<MeshRenderer>().material;
        }
        return this.matChildOriginal;
    }
}
