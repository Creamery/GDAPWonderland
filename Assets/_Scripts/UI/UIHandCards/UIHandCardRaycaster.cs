using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// This classs handles the Raycasting (Graphics and Physics) for Hand Card Interactions (i.e. hover, load as bullet, swap/replenish defense
/// </summary>
public class UIHandCardRaycaster : MonoBehaviour {

	private GraphicRaycaster raycaster;
	private PointerEventData m_PointerEventData;
	private EventSystem es;

	private UIHandCard selectedCard;
	private bool isHeld;
	

	// Use this for initialization
	void Start () {
		this.isHeld = false;
		this.raycaster = GetComponent<GraphicRaycaster>();
		this.es = GameObject.FindObjectOfType<EventSystem>();
		//StartCoroutine(FlipCardCoroutine());
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameMaster.Instance.IsInActionPhase())
			return;

		if (isHeld) {
			if (Input.GetMouseButtonUp(0)) {
				isHeld = false;
				this.selectedCard = null;
				UIHandCardManager.Instance.RemovePreview();

				PlayerManager curPlayer = MainScreenManager_GameScene.Instance.GetPlayer();
				SoldierPlacementManager.Instance.SetTargetable(curPlayer, false);
				Card finalCard = UIHandCardManager.Instance.GetSelectedCard();

				bool replenishDefenseMode = GameMaster.Instance.IsReplenishMode();
				if (this.GRGeneral("OutZone")) {
					UIHandCardManager.Instance.StopDragging();

					// Hearts card was dropped -- Special Interaction
					if (finalCard.GetCardSuit() == Card.Suit.HEARTS) { 
						// Special interaction
						int rank = finalCard.GetCardRank();
						switch (rank) {
							case 1: // +1 move
								curPlayer.IncrementMove();
								ActionsLeftPanel.Instance.Show();
								Debug.Log("<color='green'> move incremented by 1 </color>");
								break;
							case 2: // Swap rule
									// Show Rule (higher/lower) has been swapped UI
								bool prevRule = GameMaster.Instance.IsRuleHigher;
								GameMaster.Instance.SwapHigherLower();
                                PlayerPanel.Instance.UpdateRuleIcon(!prevRule);
                                ActionsLeftPanel.Instance.ShowReverseRule(!prevRule);

                                Debug.Log("<color='green'> RULE SWAPPED: "+prevRule+"->"+GameMaster.Instance.IsRuleHigher+" </color>");
								break;
							default: //Do nothing
								break;
						}
						curPlayer.GetCardManager().GetHandManager().RemoveFromHand(finalCard);
						Debug.Log("<color='blue'> Hearts removed from hand </color>");
						return;
					}

					// Differentiate Defense interactions w/ Bullet-loadings
					GameObject hit = PRGeneral("Soldier");
					if (replenishDefenseMode) {	// REPLENISH DEFENSE MDOE INTERACTIONS ((TODO: might want to remove this))
						if (hit != null) {
							// Replenish Soldier
							Soldier hitSoldier = hit.GetComponent<SoldierCollider>().GetSoldier();
							SoldierPlacementManager.Instance.ReplenishDefense(curPlayer, finalCard, hitSoldier);
							Debug.Log("Soldier replenish HIT: " + hitSoldier.name);

						}
						else {
							hit = PRGeneral("Backup");
							if(hit != null) {
								// Replenish Backup
								SoldierBackup hitBackup = hit.GetComponent<SoldierBackupCollider>().GetSoldierBackup();
								SoldierPlacementManager.Instance.ReplenishDefense(curPlayer, finalCard, hitBackup);
								Debug.Log("Backup replenish HIT: " + hitBackup.name);
							}
						}
					}
					else {
						hit = PRGeneral("Backup");
						if (hit != null) {
							SoldierBackup hitBackup = hit.GetComponent<SoldierBackupCollider>().GetSoldierBackup();
							if (hitBackup.GetPlayer() == curPlayer && !hitBackup.IsHidden) {
                                // Swap backup / place new backup
                                SoundManager.Instance.Play(AudibleNames.Button.SWITCH);
                                Debug.Log("Backup HIT: " + hitBackup.name);
								SoldierPlacementManager.Instance.ReplenishDefense(curPlayer, finalCard, hitBackup);
							}
							else {
								//Load Bullet
								if (curPlayer.GetMovesLeft() > 0) {
									if (GameMaster.Instance.IsValidCard(finalCard)) {
										// If card passes the current rule
										AttackManager.Instance.LoadCard(finalCard);
										//UIHandCardManager.Instance.ShowHand(false);
									}
									else {
                                        RuleManager.Instance.GetRulePanelAnimatable().Shake();
									}
								}
                                else {
                                    PlayerPanel.Instance.GetMovesLeftAnimatable().Shake();
                                }
							}
						}
						else {
							// Load bullet
							if (curPlayer.GetMovesLeft() > 0) {
								if (GameMaster.Instance.IsValidCard(finalCard)) {
									// If card passes the current rule
									AttackManager.Instance.LoadCard(finalCard);
									//UIHandCardManager.Instance.ShowHand(false);
								}
								else {
									RuleManager.Instance.GetRulePanelAnimatable().Shake();
								}
							}
                            else {
                                PlayerPanel.Instance.GetMovesLeftAnimatable().Shake();
                            }
                        }
					}
					
				}
				else {
                    SoundManager.Instance.Play(AudibleNames.Button.RELEASE);
                    UIHandCardManager.Instance.StopDragging();
                }

				return;
			}
		}
		else {

			bool hasSelected = this.HandCardGR("UIHandCard");

			if (!hasSelected) {
				this.selectedCard = null;
				UIHandCardManager.Instance.RemovePreview();
				if (Input.GetMouseButtonDown(0)) {
					if(this.GRGeneral("OutZone"))
						UIHandCardManager.Instance.PeekDown(true);
				}

			}
			else if (hasSelected && Input.GetMouseButtonDown(0)) {
				isHeld = UIHandCardManager.Instance.BeginDragging();
				SoldierPlacementManager.Instance.SetTargetable(MainScreenManager_GameScene.Instance.GetPlayer(), true);
			}

			// Miscellaneous handling
			if (Input.GetMouseButtonDown(0)) {
				RaycastHit hit;
				Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
				if (Physics.Raycast(ray, out hit, float.PositiveInfinity)) {
					// If Backupmat is pressed on screen, show.
					GameObject mat = PRGeneral("BMC");
					if (!isHeld && mat != null) {
						BackupMatCollider bmc = mat.GetComponent<BackupMatCollider>();
						if (bmc.GetPlayer() == MainScreenManager_GameScene.Instance.GetPlayer()) {
							bmc.GetParent().ToggleBackupMat();
						}
					}
				}
			}
		}
	}

	#region Raycast Utility Functions

	/// <summary>
	/// Initiates a Graphics Raycast and returns whether the parameter tag is found.
	/// </summary>
	/// <param name="tag">The tag to be searched.</param>
	/// <returns>true if the tag is found, false if otherwise.</returns>
	private bool GRGeneral(string tag) {
		m_PointerEventData = new PointerEventData(es);
		m_PointerEventData.position = Input.mousePosition;
		List<RaycastResult> results = new List<RaycastResult>();

		raycaster.Raycast(m_PointerEventData, results);
		
		foreach (RaycastResult r in results) {
			if (r.gameObject.CompareTag(tag) && r.gameObject.layer != 2) {
				return true;
			}
		}
		return false;
	}

	/// <summary>null
	/// Initiates a Graphics Raycast and returns whether the parameter tag is found.
	/// </summary>
	/// <param name="tag">The tag to be searched.</param>
	/// <returns>an object if the tag is found, false if otherwise.</returns>
	private GameObject PRGeneral(string tag) {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 5f);
		RaycastHit[] hits = Physics.RaycastAll(ray);
		foreach(RaycastHit hit in hits) {
			if (hit.collider.CompareTag(tag)) {
				return hit.collider.gameObject;
			}
		}
		return null;
	}

	/// <summary>
	/// Specialized Graphics Raycast for working with the hand card to be focused.
	/// </summary>
	/// <param name="tag">The tag of the card objects</param>
	/// <returns>true if a card is found, false if otherwise.</returns>
	private bool HandCardGR(string tag) {
		m_PointerEventData = new PointerEventData(es);
		m_PointerEventData.position = Input.mousePosition;
		List<RaycastResult> results = new List<RaycastResult>();

		raycaster.Raycast(m_PointerEventData, results);

		
		foreach (RaycastResult r in results) {
			if (r.gameObject.CompareTag(tag) && r.gameObject.layer != 2) {
				this.selectedCard = r.gameObject.GetComponent<UIHandCard>();
				if (UIHandCardManager.Instance.GetPreviewedCard() != this.selectedCard) {
					UIHandCardManager.Instance.RemovePreview();
					UIHandCardManager.Instance.PreviewCard(this.selectedCard);
				}

				return true;
			}
		}
		return false;
	}

	#endregion
}
