using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandCardManager : MonoBehaviour {

	private static UIHandCardManager sharedInstance;

	public static UIHandCardManager Instance {
		get { return sharedInstance; }
	}

	[Header("Setup")]
	[SerializeField] private GameObject UIHandCardContainer;
	[SerializeField] private GameObject UIHandCardPreview;
	[SerializeField] private GameObject dragContainer;
	[SerializeField] private GameObject UIHandCardPrefab;
	[SerializeField] private ParticleSystem powerEffect;
	[Header("Configuration")]
	[SerializeField] private float previewCardYPos;
	//[SerializeField] private float previewCardScaleMultiplier = 1.5f;

	private UIHandCard previewedCard;
	private UIHandCard selectedCard;
	private List<UIHandCard> hiddenCards;
	private GameObject previewCopy;
	private Vector3 prevCardOriginalScale;
	private int originalIndex;
	private bool allowDragging;
	private bool isHiding;

	private Animator anim;


	bool isPoweredUp;
	public bool IsPoweredUp {
		get { return isPoweredUp; }
	}
	private bool isHidden;
	public bool IsHidden {
		get { return isHidden; }
	}

	private void Awake() {
		isHidden = false;
		isHidden = false;
		isPoweredUp = false;
		sharedInstance = this;
	}

	private void Start() {
		hiddenCards = new List<UIHandCard>();
		anim = GetComponent<Animator>();
	}

	#region Attack Bonus Methods
	public void EnableBonus(bool val) {

		UIHandCard[] handCards = UIHandCardContainer.GetComponentsInChildren<UIHandCard>();
		foreach(UIHandCard hc in handCards) {
			hc.EnableBonus(val);
		}

		if (val) {
			PlayEffect();
		}
		else {
			StopEffect();
		}
		isPoweredUp = val;
	}

	public void PlayEffect() {
		powerEffect.Play();
	}

	public void StopEffect() {
		powerEffect.Stop();
	}
	#endregion

	#region Animator Methods
	public void ShowHand(bool value) {
        General.LogEntrance("SHOW_HAND");
		GetAnimator().SetBool("Show", value);

        if (value) {
			isHidden = false;
			isHiding = false;
			allowDragging = true;
			Debug.Log("Showing hand: " + allowDragging);
		}
		else {
			// Hide Hand

			GetAnimator().SetBool("PeekDown", false);
			RemovePreview();
			StopDragging();
			isHiding = true;
			allowDragging = false;
			Debug.Log("Hiding hand: " + allowDragging);
		}
	}
    public void PeekDown(bool peekDown) {
        if (isHidden)
            return;

        General.LogEntrance("PEEK_HAND");
        // if (!GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("Shown")) {
        GetAnimator().SetBool("PeekDown", peekDown);
        // }
		if (peekDown) {
			RemovePreview();
			StopDragging();
			allowDragging = false;
		}
		else {
			allowDragging = true;
		}
	}

    public Animator GetAnimator() {
        if(this.anim == null) {
            anim = GetComponent<Animator>();
        }
        return this.anim;
    }

	void CompleteHidden() {
		isHidden = true;
		isHiding = false;
		RemovePreview();
	}
	#endregion

	#region Card Preview Methods
	/// <summary>
	/// Sets a card to be previewed.
	/// </summary>
	/// <param name="card">card to be previewed.</param>
	public void PreviewCard(UIHandCard card) {
		if(!isHiding && !isHidden)
			PeekDown(false);

		this.previewedCard = card;
		//this.prevCardOriginalScale = card.transform.localScale;
		this.originalIndex = card.transform.GetSiblingIndex();

		this.previewCopy = Instantiate(card.gameObject, UIHandCardPreview.transform, true);
		this.previewedCard.ShouldShow(false);
		this.previewCopy.layer = 2;
		//this.previewCopy.transform.localEulerAngles = Vector3.zero;
		//this.previewCopy.transform.localScale = new Vector3(this.previewCardScaleMultiplier, this.previewCardScaleMultiplier, this.previewCardScaleMultiplier);
		this.previewCopy.transform.localPosition = new Vector3(this.previewCopy.transform.localPosition.x, previewCardYPos);
		
		
	}

	/// <summary>
	/// Removes the previewed card, if any.
	/// </summary>
	public void RemovePreview() {
		if (this.previewedCard == null)
			return;
		this.previewedCard.ShouldShow(true);
		Destroy(this.previewCopy);
		this.previewedCard = null;
	}
	#endregion

	#region Card Hiding Methods
	public void HideCard(Card c) {
		foreach (UIHandCard uhc in UIHandCardContainer.GetComponentsInChildren<UIHandCard>()) {
			if(c == uhc.GetCardReference()) {
				uhc.gameObject.SetActive(false);
				this.hiddenCards.Add(uhc);
				return;
			}
		}
	}

	public void UnhideAllCards() {
		foreach(UIHandCard uhc in hiddenCards) {
			uhc.gameObject.SetActive(true);
		}
		this.hiddenCards.Clear();
	}

	#endregion

	#region UIHandCards Methods
	public void UpdateHand(List<Card> cards) {
		this.ClearHand();
		foreach(Card c in cards) {
			AddHandCard(c);
		}
	}

	/// <summary>
	/// Adds a UIHandCard for the specified card.
	/// </summary>
	/// <param name="card">The card to be add to UI</param>
	public void AddHandCard(Card card) {
		GameObject newCard = Instantiate(UIHandCardPrefab);

		UIHandCard uhc = newCard.GetComponent<UIHandCard> ();

		newCard.transform.SetParent(UIHandCardContainer.transform, false);
		uhc.Set(card);

	}

	/// <summary>
	/// Removes the first UIHandcard that matches the 'card' parameter
	/// </summary>
	/// <param name="card">The card to be removed from hand</param>
	/// <returns>True if removing is successful, false if no match found.</returns>
	public bool RemoveHandCard(Card card) {
		foreach (UIHandCard uhc in UIHandCardContainer.GetComponentsInChildren<UIHandCard>()) {
			if (uhc.GetCardReference() == card) {
				Destroy(uhc.gameObject);
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// Removes all UI Hand cards.
	/// </summary>
	public void ClearHand() {
		foreach(UIHandCard uhc in UIHandCardContainer.GetComponentsInChildren<UIHandCard>()) {
			Destroy(uhc.gameObject);
		}
	}
	#endregion

	#region Drag Methods
	/// <summary>
	/// Returns true if success, false if not.
	/// </summary>
	/// <returns></returns>
	public bool BeginDragging() {
		if (!allowDragging)
			return false;
		Debug.Log("begin drag: " + allowDragging);
        SoundManager.Instance.Play(AudibleNames.Button.DRAG);
        StartCoroutine("DragCoroutine");
		return true;
	}

	IEnumerator DragCoroutine() {
		selectedCard = this.UIHandCardContainer.transform.GetChild(originalIndex).GetComponent<UIHandCard>();
		selectedCard.transform.SetParent(this.dragContainer.transform, true);
		selectedCard.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
		selectedCard.transform.localEulerAngles = Vector3.zero;
		RemovePreview();
		while (true) {
			Vector3 newPos = Input.mousePosition;
			newPos.z = 1;
			newPos = Camera.main.ScreenToWorldPoint(newPos);
			selectedCard.transform.position = newPos;
			yield return null;
		}
	}

	public void StopDragging() {
		if (selectedCard == null)
			return;

        selectedCard.transform.SetParent(this.UIHandCardContainer.transform);
		selectedCard.transform.SetSiblingIndex(this.originalIndex);
		selectedCard = null;
		this.originalIndex = -1;
		StopCoroutine("DragCoroutine");
		
	}
	#endregion

	#region Getters and Setters
	public UIHandCard GetPreviewedCard() {
		return this.previewedCard;
	}
    // TODO: Null check?
	public Card GetSelectedCard() {
        if(selectedCard != null) {
            return this.selectedCard.GetCardReference();
        }
        return null;
	}
	#endregion
}
