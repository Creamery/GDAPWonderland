﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaUpdateHandler : MonoBehaviour {

	[SerializeField] private SoldierDefenseGroup player1;
	[SerializeField] private SoldierDefenseGroup player2;

	private PlayerManager p1;
	private PlayerManager p2;

	private static ArenaUpdateHandler sharedInstance;
	public static ArenaUpdateHandler Instance {
		get { return sharedInstance; }
	}

	private void Start() {
		p1 = player1.GetPlayer();
		p2 = player2.GetPlayer();
		AddObservers();
		sharedInstance = this;
	}

	private void OnDestroy() {
		ClearObservers();
	}

	#region EventBroadcaster core integration
	private void AddObservers() {
		EventBroadcaster.Instance.AddObserver(EventNames.ARENA.DEFENSE_UPDATE, UpdateBoardState);
		EventBroadcaster.Instance.AddObserver(EventNames.ARENA.CLOSE_BACKUPMAT, CloseBothBackupMat);
	}

	private void ClearObservers() {
		EventBroadcaster.Instance.RemoveObserver(EventNames.ARENA.DEFENSE_UPDATE);
		EventBroadcaster.Instance.RemoveObserver(EventNames.ARENA.CLOSE_BACKUPMAT);
	}
	#endregion

	#region Update Board State Functions
	public void UpdateBoardState() { 
		Debug.Log("<color='green'> RECEIVE DEFENSE UPDATE </color>");
		SoloUpdateBoardState(p1);
		SoloUpdateBoardState(p2);
	}
	
	private void SoloUpdateBoardState(PlayerManager player) {
		//Debug.Log("<color='red'> updating board state of Player: " + player.playerNo+"</color>");
		Card[] frontCards = player.GetCardManager().GetDefenseManager().GetFrontCards();
		Card[] backCards = player.GetCardManager().GetDefenseManager().GetBackCards();

		SoldierDefenseGroup target_sdg = player == player1.GetPlayer() ? player1 : player2;

		for(int i=0; i < frontCards.Length; i++) {

			target_sdg.SetSoldier(i, frontCards[i]);
			target_sdg.GetSoldier(i).SetHealthTextActive(frontCards[i] != null);

			//if (frontCards[i] != null)
			//	Debug.Log("<color='blue'> Updating Soldier #" + i + " with: " + frontCards[i].GetCardSuit() + " Rank " + frontCards[i].GetCardRank() + " </color>");

			target_sdg.SetBackup(i, backCards[i]);
			//if (backCards[i] != null)
			//	Debug.Log("<color='blue'> Updating Backup #" + i + " with: " + backCards[i].GetCardSuit() + " Rank " + backCards[i].GetCardRank() + " </color>");
		}
		
	}

	public void CloseBothBackupMat() {
		player1.OpenBackupMat(false);
		player2.OpenBackupMat(false);
	}
	#endregion

	public SoldierDefenseGroup GetSoldierDefenseGroup(int playerNo) {
		if (playerNo == 1)
			return player1;
		else if (playerNo == 2)
			return player2;
		else
			return null;
	}
	
}