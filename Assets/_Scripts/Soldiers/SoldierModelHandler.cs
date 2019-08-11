using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierModelHandler : MonoBehaviour {

	[Header("Setup")]
	[SerializeField] private GameObject meshParent;
	[Header("Debugging")]
	[SerializeField] private GameObject[] weapons;

	private GameObject soldierModel;

	public void SetSoldierModel(Card.Suit suit, int playerNo, int rank) {

		if (soldierModel != null) {
			Destroy(soldierModel);
			Debug.Log("Destroyed");
		}
		soldierModel = null;
		weapons = null;

		if (rank < 0) {
			return;
		}

		soldierModel = Instantiate(General.GetSoldierModelPrefab(suit, playerNo), meshParent.transform);
		Transform weaponsParent = soldierModel.FindComponentInChildWithTag<Transform>("Weapon");
		weapons = new GameObject[weaponsParent.childCount];
		for(int i=0; i < weapons.Length; i++) {
			weapons[i] = weaponsParent.GetChild(i).gameObject;
			weapons[i].SetActive(false);
		}
		//Debug.Log("Spawning soldier: "+suit+" || Rank "+rank);
		weapons[rank - 1].SetActive(true);
	}

	public void SetEquippedWeapon(int rank) {
		for(int i = 0; i < weapons.Length; i++) {
			weapons[i].SetActive(false);
		}
		weapons[rank - 1].SetActive(true);
	}
}
