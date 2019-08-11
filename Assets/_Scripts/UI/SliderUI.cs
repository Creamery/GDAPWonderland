using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The slider attached to the CardSlider game object for the hand cards in Player Menu (PM) mode.
/// </summary>
public class SliderUI : MonoBehaviour {

	public GameObject panel;
	public RectTransform center;

    /// <summary>
    /// Distance of one card from another.
    /// </summary>
	private float btnDistance = 200; // prefab width + 50
	private int interval = 1;
	private int minRange = 1;
	private int maxRange;
	public CardObject prefabCardObject;

	public float snapSpeed = 1.3f;
	//float curLerp = 0f;

	private List<float> distances;   //distance of element from center
	private RectTransform panelTf;
	private bool isDragging = false;
	//private bool isLerping = false;
	private List<CardObject> listCardObjects;
	private List<Card> listCards;
	private int nearestBtnIndex;

	private bool addNewCard;


	void Start () {
		panelTf = panel.GetComponent<RectTransform>();
		nearestBtnIndex = 0;
        
		this.RefreshListContent ();
	}


	void Update () {

		for (int i = 0; i < distances.Count; i++) {
			distances[i] = Mathf.Abs(listCardObjects[i].GetComponent<RectTransform>().position.x - center.position.x);

			if (distances[i] < distances[nearestBtnIndex]) {
				nearestBtnIndex = i;
			}
			if (!isDragging) {
				SnapNearestToCenter(nearestBtnIndex * -btnDistance);
			}
		}
	}

	void SnapNearestToCenter (float position) {
		float newX;
		newX = Mathf.Lerp (panelTf.anchoredPosition.x, position, Time.deltaTime * snapSpeed);
		panelTf.anchoredPosition = new Vector2(newX, panelTf.anchoredPosition.y);
	}          

	public void BeginDrag () {
		//curLerp = 0;
		isDragging = true;
		Debug.Log ("DRAGGING");
	}

	public void EndDrag () {
		isDragging = false;
		Debug.Log ("NOT DRAGGING");
	}

	/// <summary>
	/// Destroy all card objects in preparation for new set.
	/// Note: Consider reusing.
	/// </summary>
	public void Clear() {
		foreach (CardObject cardObject in this.GetListCardObjects()) {
			Destroy (cardObject.gameObject);
		}
		this.GetListCardObjects ().Clear ();
		this.GetDistances ().Clear ();
	}

	/// <summary>
	/// Refreshes the content of the list.
	/// </summary>
	private void RefreshListContent () {
		this.maxRange = this.GetListCards ().Count+1;
		this.Clear ();

		int currentLabel = minRange;
		CardObject tempCard;

		for(int i = 0; i < (maxRange - minRange); i++) {
			tempCard = Instantiate(prefabCardObject, panelTf);
			tempCard.SetCardReference (this.GetListCards () [i]);
			tempCard.transform.localPosition = new Vector2(i * btnDistance, panel.transform.localPosition.y);

			this.GetListCardObjects ().Add (tempCard);
			this.GetDistances ().Add (tempCard.transform.localPosition.x);

			if (minRange < maxRange)
				currentLabel += interval;
			else
				currentLabel -= interval;
		}
	}

	/// <summary>
	/// Adds a card to the slider's card list then refreshes the
	/// card container to add a new card object to the UI.
	/// 
	/// </summary>
	/// <param name="card">Card.</param>
	[System.Obsolete("AddCard is deprecated, please use SetHandCards instead.")]
	public void AddCard(Card card) {
		this.GetListCards().Add(card);
		this.RefreshListContent ();
	}

	/// <summary>
	/// Sets the hand cards.
	/// </summary>
	/// <param name="newHandCards">New hand cards.</param>
	public void SetHandCards(List<Card> newHandCards) {
		this.listCards = newHandCards;
		this.RefreshListContent ();
	}


	public List<Card> GetListCards() {
		if(this.listCards == null) {
			this.listCards = new List<Card> ();
		}
		return this.listCards;
	}
	public List<float> GetDistances() {
		if (this.distances == null) {
			this.distances = new List<float> ();
		}
		return this.distances;
	}
	public List<CardObject> GetListCardObjects() {
		if (this.listCardObjects == null) {
			this.listCardObjects = new List<CardObject> ();
		}
		return this.listCardObjects;
	}
}
