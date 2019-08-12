using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackManager : MonoBehaviour {

    private static AttackManager sharedInstance;
    public static AttackManager Instance {
        get { return sharedInstance; }
    }

	[Header("Testing (To Remove)")]
	[SerializeField] private bool triggerStartScan;
    [SerializeField] private bool triggerStopScan;

	[Header("Setup")]
	[SerializeField] private AttackCrosshairTransform crosshairTransform;
	[SerializeField] private ParticleController pc;
    [SerializeField] private Camera mainCamera;
	[SerializeField] private CardAttackText cardAttack;
	[SerializeField] private Button cancelBtn;
	[SerializeField] private AttackPanelAnimatable atkAnim;

	[Header("Debug (DO NOT MODIFY)")]
	[SerializeField] private List<Card> loadedBullets;
	[SerializeField] private Card previousTarget;

	private Coroutine checkFireCoroutine;
	private Animator anim;
	private bool isScanning;

    private void Awake() {
        sharedInstance = this;
		isScanning = false;
    }

	private void Start() {
		anim = GetComponentInParent<Animator>();
		loadedBullets = new List<Card>();
		if(atkAnim == null) {
			atkAnim = GetComponentInParent<AttackPanelAnimatable>();
		}
	}

    // TODO: Test cases. To remove.
    private void Update() {
        if(this.triggerStartScan) {
            this.ResetTriggers();
            this.StartScan();
        }
        else if(this.triggerStopScan) {
            this.ResetTriggers();
            this.StopScan();
        }
    }

    // TODO: Remove
    private void ResetTriggers() {
        this.triggerStartScan = false;
        this.triggerStopScan = false;
    }

	public void ShowPanel(bool val) {
        anim.SetBool("Show", val);
        //if (val) {
        //    //this.atkAnim.Show();
        //    RuleManager.Instance.Hide();
        //}
        //else {
        //    //this.atkAnim.Hide();
        //    RuleManager.Instance.Show();
        //}
    }

    /// <summary>
    /// /// <summary>
    /// Call this function to start attack mode.
    /// Loads the card and starts the scan coroutine.
    /// </summary>
    /// <param name="card"></param>
    /// <returns>true if loading is successful, false if not</returns>
    public bool LoadCard(Card card) {
        if(card != null && !this.isScanning) {
			// First card loaded
			
			UIHandCardManager.Instance.PeekDown(true);
			SoundManager.Instance.Play(AudibleNames.Crosshair.LOAD);

            AttackManagerUI.Instance.LoadCard(card);
			loadedBullets.Add(card);
			UIHandCardManager.Instance.HideCard(card);
            //this.cardBullet = card;
			//this.cardAttack.SetText(GetLoadedBulletsSum().ToString());
			//this.cardAttack.SetText((cardBullet.GetCardAttack()+ CombatManager.Instance.GetAtkModifier()).ToString());
            this.StartScan();
			cancelBtn.interactable = true;

			BackgroundRaycaster.Instance.ResetBackupMat();	// Hide the backup mat
            Debug.Log("Card Loaded:: Atk: " + card.GetCardAttack() + " | Health: " + card.GetCardHealth()+" | SUIT: "+card.GetCardSuit());
           
            return true;
        }
        else if(card == null) {
            Debug.LogError("ERROR: No bullet to load!");
            return false;
        }
        else if(this.isScanning) {
			//Combine Bullet (second card and so on...)

			// Check if loaded cards does not exceed maximum
			if (loadedBullets.Count >= GameConstants.MAX_ATTACK_COMBINATION) {
				SoundManager.Instance.Play(AudibleNames.Crosshair.MISS);
				return false;
			}

			SoundManager.Instance.Play(AudibleNames.Crosshair.LOAD);
			//pc.ChangeColor(ParticleController.COMBI);
			AttackManagerUI.Instance.LoadCard(card, true);
			UIHandCardManager.Instance.HideCard(card);
			loadedBullets.Add(card);
			//this.cardAttack.SetText(GetLoadedBulletsSum().ToString());

			BackgroundRaycaster.Instance.ResetBackupMat();  // Hide the backup mat
			return true;
        }
        return false;
    }

    /// <summary>
    /// This method removes the currently loaded cards and stops the scanning coroutine.
    /// </summary>
    public void UnloadCard() {
		//pc.Stop();
		if (loadedBullets.Count == 0)
			return;

        SoundManager.Instance.Play(AudibleNames.Button.CANCEL);
		UIHandCardManager.Instance.UnhideAllCards();

		AttackManagerUI.Instance.UnloadCard();
		// UIHandCardManager.Instance.PeekDown(false);
		//this.cardAttack.SetText("-");
		this.loadedBullets.Clear();
        //this.cardBullet = null;
        this.StopScan();
    }

    // Clean kill if soldier was not damaged before and bullet card is exact value of current health
    public void CleanKillBonus(Soldier targetSoldier) {
        if(GameConstants.HAS_CLEAN_KILL_BONUS) {
            if (targetSoldier.GetCardReference().GetCardHealth() == targetSoldier.GetCurrentHealth() &&
                targetSoldier.GetCurrentHealth() == GetLoadedBulletsSum()) {
                PlayerManager curPlayer = MainScreenManager_GameScene.Instance.GetPlayer();
                curPlayer.IncrementMove();
            }
        }
    }
    /// <summary>
    /// Locks on a target (if any) and stops the scanning process.
    /// Damages the target if applicable.
    /// </summary>
    public void LockOn() {
		if (!isScanning)
			return;

		PlayerManager curPlayer = MainScreenManager_GameScene.Instance.GetPlayer();

		Soldier targetSoldier = this.GetTargetSoldier();
		LifeObjectCollider targetLOC = this.GetTargetLifeCollider();
		// Know if the current target is the player heart or a soldier.
		if(targetSoldier != null) {
			Card targetCard = targetSoldier.GetCardReference();

			// The Target is a Soldier.
			if (targetCard != null && this.previousTarget != targetCard) {
                this.PlayFireSound(targetCard.GetCardSuit());

				// NEW: if exact, +1 move
				this.CleanKillBonus(targetSoldier);

				// The Current target Soldier is a valid target.
				Debug.Log("<color=green>SUCCESS! Target shot using " + GetLoadedBulletsSum() + "</color>");
				bool canFireAgain = CombatManager.Instance.HandleAttack(curPlayer, loadedBullets, targetSoldier);
				// insert fire animation here
				BackgroundRaycaster.Instance.ActivateParticle();

				if (canFireAgain) {
					// Can Fire Again.
					atkAnim.Again();
					this.previousTarget = targetCard;
					cancelBtn.interactable = false;
				}
				else {
					// Out of Shots or no more targets left
					atkAnim.Hit();
					curPlayer.ConsumeMove(); // Subtract move from current player
					UIHandCardManager.Instance.UnhideAllCards();
					this.SuccessfulShot();
					this.previousTarget = targetCard;
                    //ActionsLeftPanel.Instance.Show(); // EDITED (Candy)
                    ActionsLeftPanel.Instance.DelayedShow();
                    this.StopScan(1f); // Update delay time acc. to "Hit" animation prompt
                }
			}
			else {
				atkAnim.Miss();
			}
		}
		else if(targetLOC != null){
			// The Target is a player heart.
			bool isSuccess = CombatManager.Instance.HandleDirectAttack(curPlayer, loadedBullets, targetLOC);
			if (isSuccess) {
				atkAnim.Hit();
				curPlayer.ConsumeMove(); // Subtract move from current player
				// BackgroundRaycaster.Instance.ActivateParticle();
				UIHandCardManager.Instance.UnhideAllCards();
				this.SuccessfulShot();
				EventBroadcaster.Instance.PostEvent(EventNames.UI.SHOW_DIRECT_ATK_SUCCESS);
				this.StopScan();
				// TODO: instantly end the turn.
			}
			else {
				atkAnim.Miss();
			}
		}
		else {
			// There is no target
			atkAnim.Miss();
		}
    }

    public void PlayFireSound(Card.Suit suit) {
        switch(suit) {
            default:
                SoundManager.Instance.Play(AudibleNames.Fire.DEFAULT);
                break;
        }
    }

	private void SuccessfulShot() {
		PlayerManager curPlayer = MainScreenManager_GameScene.Instance.GetPlayer();
		curPlayer.GetCardManager().DiscardHandCard(this.loadedBullets); // Discard card(s)
		this.loadedBullets.Clear();
	}

    /// <summary>
    /// Start scanning for targets.
    /// </summary>
    public void StartScan() {
        Debug.Log("START SCAN");
		isScanning = true;

		BackgroundRaycaster.Instance.SetAttackMode(true);
        PlayerPanel.Instance.Hide();
        this.ShowPanel(true);
        //      this.StartCoroutine("Scanning");
        //this.checkFireCoroutine = StartCoroutine(CheckForFire());
        RuleManager.Instance.Hide();
    }

	/// <summary>
	/// Stop scanning for targets.
	/// </summary>
	public void StopScan(float delay) {
		Debug.Log("Stop Scan");
		isScanning = false;

		previousTarget = null;
        StartCoroutine(StopScanRoutine(delay));
    }

    public void StopScan() {
        StopScan(0.0f);
    }

    IEnumerator StopScanRoutine(float delay) {
        BackgroundRaycaster.Instance.SetAttackMode(false);

        yield return new WaitForSeconds(delay); // TODO DELAY SHOW + HIT TIME UPDATE

        PlayerPanel.Instance.Show();
        // BackgroundRaycaster.Instance.SetAttackMode(false);

        this.ShowPanel(false);

        UIHandCardManager.Instance.ShowHand(true);
        RuleManager.Instance.Show();
        yield return null;
    }

    public Soldier GetTargetSoldier() {
		return BackgroundRaycaster.Instance.GetTargetSoldier();
	}

	public LifeObjectCollider GetTargetLifeCollider() {
		return BackgroundRaycaster.Instance.GetLifeCollider();
	}

    public bool IsScanning() {
		return isScanning;
	}

    public AttackCrosshairTransform GetAttackCrosshairTransform() {
        if (this.crosshairTransform == null) {
            this.crosshairTransform = GetComponentInChildren<AttackCrosshairTransform>();
        }
        return this.crosshairTransform;
    }

	private int GetLoadedBulletsSum() {
		if (loadedBullets == null)
			return -1;
		int sum = 0;
		int modifier = CombatManager.Instance.GetAtkModifier();
		foreach(Card c in loadedBullets) {
			sum += c.GetCardAttack() + modifier;
		}
		return sum;
	}
}
