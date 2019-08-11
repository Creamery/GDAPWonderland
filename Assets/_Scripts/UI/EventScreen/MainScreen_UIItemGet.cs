using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MainScreen_UIItemGet : MainScreen {

    [SerializeField] Rules currentRule;
    [SerializeField] private ItemGetScreenAnimatable screenAnimatable;
    [SerializeField] private ItemGetRuleTextManager ruleTextManager;
    [SerializeField] private List<PlayerSensitiveObjectMarker> playerSensitiveObjectMarkers;

    [SerializeField] private CardImageChangeManager cardImageManager;
    

    public IEnumerator ShowRoutine(Rules rule) {
        // Show the correct rule text image for the given rule.
        this.RefreshPlayerSensitiveMarkers();
        this.GetRuleTextManager().Show(rule);
        base.Show();
        this.GetScreenAnimatable().Show();
        // Stall while animation is playing
        yield return new WaitForSeconds(ItemGetScreenAnimatable.f_SHOW);

        this.currentRule = rule; // Store rule for Confirm use
        yield return StartCoroutine(RuleSpecificAnimationRoutine(rule));
    }

    public IEnumerator RuleSpecificAnimationRoutine(Rules rule) {
        switch (rule) {
            case Rules.DRAW_CARD:
                Card drawnCard = GameMaster.Instance.GetCurPlayer().GetLastDrawnCard();
                this.LoadCardFront(drawnCard);

                this.GetScreenAnimatable().DrawCard();
                yield return new WaitForSeconds(ItemGetScreenAnimatable.f_DRAW_CARD);
                break;

            default:
                this.GetScreenAnimatable().DrawCard();
                yield return new WaitForSeconds(ItemGetScreenAnimatable.f_DRAW_CARD);
                break;
        }

        yield return null;
    }

    /// <summary>
    /// Set the image to the last drawn card.
    /// </summary>
    /// <param name="card"></param>
    public void LoadCardFront(Card card) {
        this.GetCardImageManager().ChangeImage(card);
    }
    /// <summary>
    /// Confirm button action. Switch what to do next depending on current rule.
    /// </summary>
    public void Confirm() {
        General.LogEntrance("MainScreenUI_ItemGet Confirm");

        SoundManager.Instance.Play(AudibleNames.Button.DEFAULT);
        switch (this.currentRule) {
           
            case Rules.DRAW_CARD:
                // Check for card burn
                StartCoroutine(DrawnCardValidationRoutine());
                break;
            case Rules.SUMMON:

                GameMaster.Instance.SetEventSettled(true);
                this.Hide();
                break;
            default:
                GameMaster.Instance.SetEventSettled(true);
                this.Hide();
                break;
        }
    }


    /// <summary>
    /// Checks if the most recently drawn card was burned and does the necessary actions.
    /// </summary>
    /// <returns></returns>
    public IEnumerator DrawnCardValidationRoutine() {

        // If the drawn card was not burned
        if (GameMaster.Instance.GetCurPlayer().WasLastDrawSuccessful()) {
            // Settle event and hide item get screen.
            GameMaster.Instance.SetEventSettled(true);
            this.Hide();
            // TODO: Call draw card hand animation
        }
        else {
            // If card was burned, play the card burn animation
            this.GetScreenAnimatable().BurnCard();
            yield return new WaitForSeconds(ItemGetScreenAnimatable.f_BURN_CARD);
            GameMaster.Instance.SetEventSettled(true);
            this.Hide();
        }
        yield return null;
    }

    public void RefreshPlayerSensitiveMarkers() {
        foreach(PlayerSensitiveObjectMarker marker in this.GetPlayerSensitiveObjectMarkers()) {
            marker.Refresh();
        }
    }

    public IEnumerator HideRoutine() {
        //General.LogEntrance("Logo Hide");
        //this.GetContentScreenAnimatable().LogoHide();
        //// Stall while hiding animation is still playing. Animation must trigger a AnimationStop event.
        //while (this.GetContentScreenAnimatable().IsPlaying()) {
        yield return null;
        //}
        base.Hide();
    }

    /// <summary>
    /// Item get screen animatable getter.
    /// </summary>
    /// <returns></returns>
    public ItemGetScreenAnimatable GetScreenAnimatable() {
        if(this.screenAnimatable == null) {
            this.screenAnimatable = GetComponent<ItemGetScreenAnimatable>();
        }
        return this.screenAnimatable;
    }

    public ItemGetRuleTextManager GetRuleTextManager() {
        if(this.ruleTextManager == null) {
            this.ruleTextManager = GetComponentInChildren<ItemGetRuleTextManager>();
        }
        return this.ruleTextManager;
    }

    public List<PlayerSensitiveObjectMarker> GetPlayerSensitiveObjectMarkers() {
        if(this.playerSensitiveObjectMarkers == null) {
            this.playerSensitiveObjectMarkers = GetComponentsInChildren<PlayerSensitiveObjectMarker>().ToList();
        }
        return this.playerSensitiveObjectMarkers;
    }

    public CardImageChangeManager GetCardImageManager() {
        if (this.cardImageManager == null) {
            this.cardImageManager = GetComponentInChildren<CardImageChangeManager>();
        }
        return this.cardImageManager;
    }

}
