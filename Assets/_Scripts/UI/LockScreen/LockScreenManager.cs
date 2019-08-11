using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScreenManager : MonoBehaviour {
    [SerializeField] private GameObject lockScreenPanel;
    [SerializeField] private LockScreenAnimatable lockScreenAnimatable;
    [SerializeField] private LockScreenPlayerText lockScreenPlayerText;

    //[SerializeField] private bool triggerOpen;
    //[SerializeField] private bool triggerClose;
    //[SerializeField] private bool triggerUnlocked;

    [SerializeField] private bool isOpen;

    [SerializeField] private PlayerSensitiveObjectManager playerSensitiveManager;

    private void Awake() {
        EventBroadcaster.Instance.AddObserver(EventNames.IMAGE_TARGETS.TRACKED_HAND, VerifyLock);
        EventBroadcaster.Instance.AddObserver(EventNames.UI.OPEN_LOCKSCREEN, Open);
        EventBroadcaster.Instance.AddObserver(EventNames.UI.CLOSE_LOCKSCREEN, Close);
    }
    private void Start() {
        General.LogEntrance("LockScreenManager Start");
	}

	private void OnDestroy() {
		EventBroadcaster.Instance.RemoveObserver(EventNames.IMAGE_TARGETS.TRACKED_HAND);
		EventBroadcaster.Instance.RemoveObserver(EventNames.UI.OPEN_LOCKSCREEN);
        EventBroadcaster.Instance.RemoveObserver(EventNames.UI.CLOSE_LOCKSCREEN);
    }

	// Conditions used for testing through inspector.
	//void Update() {
 //       if(this.triggerClose) {
 //           this.UncheckAll();
 //           Close();
 //       }

 //       else if(this.triggerOpen) {
 //           this.UncheckAll();
 //           Open();
 //       }
 //       else if (this.triggerUnlocked) {
 //           this.UncheckAll();
 //           Unlock();
 //       }
 //   }

    public void UncheckAll() {
        //this.triggerOpen = false;
        //this.triggerClose = false;
        //this.triggerUnlocked = false;
    }

	void VerifyLock(Parameters param) {
		General.LogEntrance("VerifyLock");
        if(!GameMaster.Instance.GetHasVerified()) {
            PlayerManager player = param.GetObjectExtra(GameMaster.PLAYER) as PlayerManager;
            if (player == GameMaster.Instance.GetCurPlayer()) {
                Unlock();
                GameMaster.Instance.SetHasVerified(true);
                Debug.Log("verify player");
            }
        }
	}

    public PlayerSensitiveObjectManager GetPlayerSensitiveManager() {
        if(this.playerSensitiveManager == null) {
            this.playerSensitiveManager = GetComponent<PlayerSensitiveObjectManager>();
        }
        return this.playerSensitiveManager;
    }

    /// <summary>
    /// Enables the lock screen game object.
    /// </summary>
    public void Open() {
        General.LogEntrance("LockScreenManager Open");
        this.isOpen = true;
        this.GetPlayerSensitiveManager().RefreshMarkers();
        UIHandCardManager.Instance.ShowHand(false);
        PlayerPanel.Instance.Hide();
        BombPanel.Instance.Hide();
        RuleManager.Instance.Hide();
        this.GetLockScreenPanel().gameObject.SetActive(true);

		string playerText = MainScreenManager_GameScene.Instance.GetPlayer() == GameMaster.Instance.GetPlayer1() ? "Player 1" : "Player 2";
		this.SetPlayerText(playerText);
    }


    public void Unlock() {
        SoundManager.Instance.Play(AudibleNames.Target.UNLOCK);
		General.LogEntrance("Unlock");
        StartCoroutine(UnlockRoutine());
    }

    /// <summary>
    /// Unlock routine waits for the unlock animation to finish before closing/hiding the object.
    /// The object is already considered "unlocked" even before the animation finishes.
    /// </summary>
    /// <returns></returns>
    IEnumerator UnlockRoutine() {
        General.LogEntrance("LockScreenManager.cs UnlockRoutine");
        this.GetLockScreenAnimatable().Unlock();
        yield return new WaitForSeconds(LockScreenAnimatable.f_UNLOCK);

        //PlayerPanel.Instance.Show();
        //RulePanel.Instance.Show();
        //UIHandCardManager.Instance.ShowHand(true);

        General.LogExit("LockScreenManager.cs UnlockRoutine");
        this.Close();
    }

    /// <summary>
    /// Disables the lock screen game object.
    /// </summary>
    public void Close() {
        this.isOpen = false;
        this.GetLockScreenPanel().gameObject.SetActive(false);
    }

    /// <summary>
    /// Sets the player text (i.e. PLAYER 1 or PLAYER 2)
    /// </summary>
    /// <param name="newText"></param>
    public void SetPlayerText(string newText) {
        this.GetLockScreenPlayerText().SetPlayerText(newText);
    }

    /// <summary>
    /// Check if the lock screen is open.
    /// </summary>
    /// <returns></returns>
    public bool IsOpen() {
        return this.isOpen;
    }

    public LockScreenPlayerText GetLockScreenPlayerText() {
        if(this.lockScreenPlayerText == null) {
            this.lockScreenPlayerText = GetComponentInChildren<LockScreenPlayerText>();
        }
        return this.lockScreenPlayerText;
    }

    /// <summary>
    /// Lock screen panel getter. Assumes that the panel will have a LockScreenAnimatable component.
    /// </summary>
    /// <returns></returns>
    public GameObject GetLockScreenPanel() {
        if(this.lockScreenPanel == null) {
            this.lockScreenPanel = GetComponentInChildren<LockScreenAnimatable>().gameObject;
        }
        return this.lockScreenPanel;
    }

    /// <summary>
    /// LockScreenAnimatable getter.
    /// </summary>
    /// <returns></returns>
    public LockScreenAnimatable GetLockScreenAnimatable() {
        if (this.lockScreenAnimatable == null) {
            this.lockScreenAnimatable = GetComponentInChildren<LockScreenAnimatable>();
        }
        return this.lockScreenAnimatable;
    }
}
