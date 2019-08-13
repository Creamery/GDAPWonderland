using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player {
	PLAYER_1, PLAYER_2
}

public class SoldierDefenseGroup : MonoBehaviour {

	[SerializeField] Soldier[] soldiers;
	[SerializeField] SoldierBackup[] backups;
	[SerializeField] BackupMatAnimHandler backupContainer;
	[SerializeField] Player designatedOwner;
    [SerializeField] PlayerHeartManager heartManager;
    private PlayerManager player;

	bool isBackupMatShown;
	public bool IsBackupMatShown {
		get { return isBackupMatShown; }
	}

	private void Start() {
		if (this.player == null)
			Initialize();
		isBackupMatShown = false;
	}

	public void Initialize(PlayerManager owner) {
		this.player = owner;
	}

	public void Initialize() {
		if (designatedOwner == Player.PLAYER_1)
			this.player = GameMaster.Instance.GetPlayer1();
		else
			this.player = GameMaster.Instance.GetPlayer2();
	}

	public Soldier GetSoldier(int index) {
		return soldiers[index];
	}

	public SoldierBackup GetSoldierBackup(int index) {
		return backups[index];
	}

    public void Refresh() {
        if(backups.Length > 0) {
            backups[0].GetFloatingCard().Refresh();
            backups[1].GetFloatingCard().Refresh();
            backups[2].GetFloatingCard().Refresh();
        }
    }

	/// <summary>
	/// Finds the index of the specified soldier.
	/// </summary>
	/// <param name="soldier"></param>
	/// <returns>the index of the specified soldier, -1 if not found. </returns>
	public int FindSoldierIndex(Soldier soldier) {
		for(int i=0; i < soldiers.Length; i++) {
			if (soldier == soldiers[i])
				return i;
		}
		return -1;
	}

	/// <summary>
	/// Finds the index of the specified soldierBackup.
	/// </summary>
	/// <param name="backup"></param>
	/// <returns>the index of the specified SoldierBackup, -1 if not found. </returns>
	public int FindBackupIndex(SoldierBackup backup) {
		for (int i = 0; i < backups.Length; i++) {
			if (backup == backups[i])
				return i;
		}
		return -1;
	}


    public PlayerHeartManager GetHeartManager() {
        if(this.heartManager == null) {
            this.heartManager = GetComponentInChildren<PlayerHeartManager>();
        }
        return this.heartManager;
    }

	public void FortifyFrontDefense(int amount) {
		foreach(Soldier s in soldiers) {
			s.BuffHealth(amount);
		}
	}

	public void SetSoldier(int index, Card cardreference) {
		soldiers[index].SetCardReference(cardreference);
    }

	public void SetBackup(int index, Card cardReference) {
		backups[index].SetCard(cardReference);
    }

	/// <summary>
	/// Sets the backup cards to be targetable, if possible.
	/// This places a transluscent placeholder block on the position of the backup card.
	/// </summary>
	/// <param name="val"></param>
	public void SetTargetableAll(bool val) {
		for (int i = 0; i < backups.Length; i++) {
			backups[i].SetTargetable(val);
		}
	}

	public void OpenBackupMat(bool val) {
		if (!backupContainer.Open(val))
			isBackupMatShown = !val;
	}

	public void ShowSoldierHistories(bool val) {
		foreach(Soldier s in soldiers) {
			SoldierHistoryManager histManager = s.GetSoldierHistory();
			histManager.ShowHistoryPanel(val);
		}
	}

	public void ClearSoldierHistories() {
		foreach (Soldier s in soldiers) {
			SoldierHistoryManager histManager = s.GetSoldierHistory();
			histManager.ClearHistories();
		}
	}

	public void ToggleBackupMat() {
		isBackupMatShown = !isBackupMatShown;
		backupContainer.Open(isBackupMatShown);
	}

	public int GetPlayerNo() {
		if (designatedOwner == Player.PLAYER_1) {
			return 1;
		}
		else {
			return 2;
		}
	}

	public PlayerManager GetPlayer() {
		if(player == null)
			Initialize();
		return this.player;
	}
}
