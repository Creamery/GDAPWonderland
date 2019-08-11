using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
/// <summary>
/// Script attached to AttackCardsContent game object.
/// UIAttackMode > CenterPanel > SidePanel > CardPanel_AM >
/// Scroll View > Viewport > AttackCardsContent
/// 
/// Always use getters and setters when accessing properties for null checking.
/// </summary>

public class AttackCardsContent : MonoBehaviour {
    [SerializeField] private CardPanel_AM parent;

    [SerializeField] private bool willUpdate;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private VerticalLayoutGroup verticalLayoutGroup;
    [SerializeField] private List<AttackCardHand> cardList;
    [SerializeField] private int newCardIndex;

    /// <summary>
    /// Set from CardPanel_AM.cs
    /// </summary>
    private GameObject prefabAttackCardHand;

    /// <summary>
    /// Initializes card objects.
    /// </summary>
    public void Initialize(GameObject cardPrefab) {
        this.prefabAttackCardHand = cardPrefab;
        this.GenerateCardObjects();
    }

    /// <summary>
    /// Generates the card object holders.
    /// </summary>
    public void GenerateCardObjects() {
        for (int i = 0; i < GameConstants.MAX_HAND_CARDS; i++) {
            Instantiate(prefabAttackCardHand, gameObject.transform);
        }
        this.cardList = gameObject.GetComponentsInChildren<AttackCardHand>().ToList();
        this.UpdateHeight();
        this.ClearCards();
    }

    /// <summary>
    /// Add a single card in the attack mode scroll view.
    /// </summary>
    public void AddCardContent(Card card) {
        this.GetCardList()[this.GetNewCardIndex()].Set(card);
        this.SetNewCardIndex(this.GetNewCardIndex() + 1);
        this.UpdateHeight();
    }

    /// <summary>
    /// Clears hand cards by setting the card reference to null and disabling the game object.
    /// Also sets the new card index to 0.
    /// </summary>
    public void ClearCards() {
        foreach (AttackCardHand card in this.cardList) {
            card.Clear();
        }
        this.newCardIndex = 0;
    }

    /// <summary>
    /// Update scroll view height based on number of children.
    /// Considers the height (rectTransform.rect.height) and padding (top and bottom from verticalLayoutGroup).
    /// </summary>
    public void UpdateHeight() {
        RectTransform childTransform = GetComponentInChildren<AttackCardHand>().gameObject.GetComponent<RectTransform>();

        if (childTransform != null) {
            int childCount = GetComponentsInChildren<AttackCardHand>().Length;
            float childHeight = childTransform.rect.height +
                this.GetVerticalLayoutGroup().spacing;

            Debug.Log("childHeight " + childHeight);
            Debug.Log("childCount " + childCount);

            this.GetRectTransform().sizeDelta = new Vector2(
                this.GetRectTransform().sizeDelta.x,
                childHeight * childCount + this.GetVerticalLayoutGroup().spacing);

        }
    }

    private void Update() {
        if (willUpdate) {
            this.willUpdate = false;
            this.UpdateHeight();
        }
    }

    /// <summary>
    /// newCardIndex setter.
    /// </summary>
    /// <param name="newIndex"></param>
    public void SetNewCardIndex(int newIndex) {
        this.newCardIndex = newIndex;
    }

    /// <summary>
    /// VerticalLayoutGroup getter.
    /// </summary>
    /// <returns></returns>
    public VerticalLayoutGroup GetVerticalLayoutGroup() {
        if (this.verticalLayoutGroup == null) {
            this.verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        }
        General.LogNull(verticalLayoutGroup);
        return this.verticalLayoutGroup;
    }

    /// <summary>
    /// RectTransform getter.
    /// </summary>
    /// <returns></returns>
    public RectTransform GetRectTransform() {
        if (this.rectTransform == null) {
            this.rectTransform = gameObject.GetComponent<RectTransform>();
        }
        General.LogNull(rectTransform);
        return this.rectTransform;
    }

    public List<AttackCardHand> GetCardList() {
        return this.cardList;
    }

    public int GetNewCardIndex() {
        return this.newCardIndex;
    }
}
