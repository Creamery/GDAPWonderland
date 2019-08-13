using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBackupFloatingCard : MonoBehaviour {
    [SerializeField] MeshRenderer cardRenderer;
    [SerializeField] MeshRenderer[] childRenderers;
    protected bool isEnabled = true;

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
}
