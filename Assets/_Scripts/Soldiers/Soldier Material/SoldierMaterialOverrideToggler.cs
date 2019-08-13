﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMaterialOverrideToggler : MonoBehaviour {

	[SerializeField] private Material[] materialForOverride;
	[SerializeField] private bool toggleMaterial;
	private bool isOverriden;

	private List<Material[]> materialsCache;

	void Awake() {
		materialsCache = new List<Material[]>();
		ForceMaterial();
	}

	private void Update() {
		if (toggleMaterial) {
			ToggleOverride();
			toggleMaterial = false;
		}
	}

	public void ToggleOverride() {
		if (isOverriden) {
			RestoreMaterial();
			isOverriden = false;
		}
		else {
			OverrideMaterial();
			isOverriden = true;
		}
	}

	public void OverrideMaterial() {
		materialsCache.Clear();
		Renderer[] children = GetComponentsInChildren<Renderer>(false);
		foreach(Renderer child in children) {
			materialsCache.Add(child.materials);
			child.materials = materialForOverride;
		}
	}

	public void RestoreMaterial() {
		Renderer[] children = GetComponentsInChildren<Renderer>(false);
		int i = 0;
		foreach (Renderer child in children) {
			child.materials = materialsCache[i];
			i++;
		}
	}

	/// <summary>
	/// For forcing the set material for materialForOverride.
	/// </summary>
	private void ForceMaterial() {
		materialForOverride = new Material[1];
		materialForOverride[0] = General.GetMaterial("TranslucentSoldierMaterial");
	}
}
