using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoldierBackup : MonoBehaviour {

	[Header("Setup")]
	[SerializeField] private CardAttackText attackTM;
	[SerializeField] private CardHealthText healthTM;
	[SerializeField] private GameObject cardObject;
	[SerializeField] private GameObject cardFront;
	[SerializeField] private GameObject placeHolderBlock;
	[Header("Debugging")]
	//[SerializeField] int index;

	private bool hidden;
	public bool IsHidden {
		get { return hidden; }
	}

	private Animator anim;
	private SoldierDefenseGroup parent;
	private Card cardReference;

	private void Awake() {
		this.parent = GetComponentInParent<SoldierDefenseGroup>();
		this.anim = cardObject.GetComponent<Animator>();
		hidden = true;
		RemoveCard();
		this.SetTargetable(false);
	}

	private void Start() {
		//index = GetIndex();
	}

	public Card SwapCard(Card newCard) {
		Card oldCard = GetCard();
		SetCard(newCard);
		return oldCard;
	}

	public Card TakeCard() {
		Card takenCard = GetCard();
		RemoveCard();
		return takenCard;
	}

	public void SetCard(Card card) {
		if (card == null) {
			this.GetCardAttack().SetText("");
			this.GetCardHealth().SetTextMesh("");
			this.cardObject.SetActive(false);
			hidden = false;
			Debug.Log("Set Null");
		}
		else {

			Debug.Log("Card set:" + card.GetCardSuit());
			this.cardObject.SetActive(true);

			this.GetCardAttack().SetText(card.GetCardAttack().ToString(), card.GetCardSuit());
			this.GetCardHealth().SetTextMesh(card.GetCardHealth().ToString());

			this.cardFront.GetComponent<MeshRenderer>().material = General.GetCardMaterial(card.GetCardSuit(), parent.GetPlayerNo(), card.GetCardRank());
			Debug.Log("Set Hidden: " + !parent.IsBackupMatShown);
			SetHidden(!parent.IsBackupMatShown);
		}
		this.cardReference = card;
	}

	/// <summary>
	/// Used to flip the card to its card back. Not to be confused with SetTargetable.
	/// Used for hiding the enemy's backup soldiers
	/// </summary>
	/// <param name="isHidden"></param>
	public void SetHidden(bool isHidden) {
		if (this.cardReference != null) {
			if (isHidden) {
				anim.ResetTrigger("Show");
				//anim.SetBool("Hidden", true);
				anim.SetTrigger("Hide");
				hidden = true;
			}
			else {
				this.GetCardAttack().SetText(cardReference.GetCardAttack().ToString(), cardReference.GetCardSuit());
				this.GetCardHealth().SetTextMesh(cardReference.GetCardHealth().ToString());
				this.cardFront.GetComponent<MeshRenderer>().material = General.GetCardMaterial(this.cardReference.GetCardSuit(), parent.GetPlayerNo(), this.cardReference.GetCardRank());
				//anim.SetBool("Hidden", false);

				anim.ResetTrigger("Hide");
				anim.SetTrigger("Show");
				hidden = false;
			}
		}
	}

	public void CompleteHideAnim() {
		this.GetCardAttack().SetText("");
		this.GetCardHealth().SetTextMesh("");
		this.cardFront.GetComponent<MeshRenderer>().material = General.GetCardHiddenMaterial();
	}

	public void CompleteOpenAnim() {
		//hidden = false;
	}

	/// <summary>
	/// Used to show the blank cards as translucent blocks to indicate that it can be targetted.
	/// Used in replenish defense mode.
	/// </summary>
	/// <param name="isTargetable"></param>
	public void SetTargetable(bool isTargetable) {
		if(this.cardReference == null) {
			this.placeHolderBlock.SetActive(isTargetable);
		}
	}

	public void RemoveCard() {
		SetCard(null);
	}

	public int GetIndex() {
		return parent.FindBackupIndex(this);
	}

	public Card GetCard() {
		return this.cardReference;
	}

	public PlayerManager GetPlayer() {
		return this.parent.GetPlayer();
	}

    public CardAttackText GetCardAttack() {
        if (this.attackTM == null) {
            this.attackTM = GetComponent<CardAttackText>();
            // this.attackTM.fontSharedMaterial = Resources.Load<Material>(fontPath + fontHealth);
        }
        return this.attackTM;
    }

    public CardHealthText GetCardHealth() {
        if (this.healthTM == null) {
            this.healthTM = GetComponent<CardHealthText>();
            // this.healthTM.fontSharedMaterial = Resources.Load<Material>(fontPath + fontHealth);
        }
        return this.healthTM;
    }
}
