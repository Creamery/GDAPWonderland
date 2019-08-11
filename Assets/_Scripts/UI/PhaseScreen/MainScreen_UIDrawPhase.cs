using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MainScreen_UIDrawPhase : MainScreen {

    [SerializeField] Rules currentRule;
    [SerializeField] private DrawPhaseAnimatable screenAnimatable;
    [SerializeField] private ItemGetRuleTextManager ruleTextManager;
    [SerializeField] private List<PlayerSensitiveObjectMarker> playerSensitiveObjectMarkers;

    //[SerializeField] private CardImageChangeManager cardImageManager;
    //[SerializeField] private MultipleCardImageChangeManager cardImageManager;

    [SerializeField] private List<PhaseCardParentManager> phaseCardParentManagers;
    //[SerializeField] private List<MultipleCardImageChangeManager> multipleCardImageManager;


    public IEnumerator ShowRoutine() {
        General.LogEntrance("UI DRAW PHASE");
        //Card drawnCard = GameMaster.Instance.GetCurPlayer().GetLastDrawnCard();
        //this.LoadCardFront(drawnCard);
        List<Card> drawnCards = GameMaster.Instance.GetCurPlayer().GetCardManager().GetLastDrawnCards();
        this.LoadMultipleCardFront(drawnCards);

        this.RefreshPlayerSensitiveMarkers();
        base.Show();


        Debug.Log("<color=cyan> pre get ()</color>");
        this.GetScreenAnimatable().Show();
        // Stall while animation is playing

        Debug.Log("<color=cyan>post get ()</color>");
        yield return new WaitForSeconds(DrawPhaseAnimatable.f_SHOW);
    }

    public IEnumerator RouletteRoutine() {
       
        base.Show();
        this.GetScreenAnimatable().Roulette();
        // Stall while animation is playing
        yield return new WaitForSeconds(DrawPhaseAnimatable.f_ROULETTE);
    }

    public IEnumerator AttackRoutine() {
        base.Show();
        this.GetScreenAnimatable().Attack();
        // Stall while animation is playing
        yield return new WaitForSeconds(DrawPhaseAnimatable.f_ATTACK);
    }

    /// <summary>
    /// Set the image to the last drawn card.
    /// </summary>
    /// <param name="card"></param>
    //public void LoadCardFront(Card card) {
    //    // TODO: Make this an array
    //    //foreach (MultipleCardImageChangeManager manager in this.GetMultipleCardImageManager()) {
    //    //    manager.UpdateCardValues(card);
    //    //}
    //    this.GetCardImageManager().ChangeImage(card);
    //}

    /// <summary>
    /// Set the image to the drawn cards.
    /// </summary>
    /// <param name="cards"></param>
    public void LoadMultipleCardFront(List<Card> cards) {
        // TODO: Make this an array
        Card card;
        bool isBurn;
        List<int> burnIndices = GameMaster.Instance.GetCurPlayer().GetCardManager().GetBurntCardIndeces();
        for (int i = 0; i < cards.Count; i++) {
            this.GetPhaseCardParentManagers()[i].Show();
            isBurn = false;
            card = cards[i];
            if (burnIndices.Contains(i)) {
                Debug.Log("<color=red>BURN INDEX " + i + "</color>");
                isBurn = true;
            }
            this.GetPhaseCardParentManagers()[i].UpdateCardValues(card, isBurn);
        }

        for(int i = cards.Count; i < this.GetPhaseCardParentManagers().Count; i++) {
            // Disable card sets
            this.GetPhaseCardParentManagers()[i].Hide();
        }
    }
    /// <summary>
    /// Confirm button action. Switch what to do next depending on current rule.
    /// </summary>
    public void Confirm() {
        General.LogEntrance("MainScreenUI_DrawPhase Confirm");

        SoundManager.Instance.Play(AudibleNames.Button.DEFAULT);
        // Check for card burn
        StartCoroutine(DrawnCardValidationRoutine());

        //switch (MainScreenManager_PhaseScreen.Instance.GetCurrentPhase()) {

        //    case MainScreenManager_PhaseScreen.Phase.DRAW:
        //        SoundManager.Instance.Play(AudibleNames.Button.DEFAULT);
        //        // Check for card burn
        //        StartCoroutine(DrawnCardValidationRoutine());
        //        break;
        //}
        //// Play rule animation panel if necessary
        //RuleManager.Instance.RuleDebut(this.currentRule);
        //UIHandCardManager.Instance.ShowHand(false);
    }

    /// <summary>
    /// Checks if the most recently drawn card was burned and does the necessary actions.
    /// Must stop phase
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

        //RulePanel.Instance.Show();
        //UIHandCardManager.Instance.ShowHand(true);
        GameMaster.Instance.StopPhase();
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
    public DrawPhaseAnimatable GetScreenAnimatable() {
        if(this.screenAnimatable == null) {
            this.screenAnimatable = GetComponent<DrawPhaseAnimatable>();
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

    //public CardImageChangeManager GetCardImageManager() {
    //    if (this.cardImageManager == null) {
    //        this.cardImageManager = GetComponentInChildren<CardImageChangeManager>();
    //    }
    //    return this.cardImageManager;
    //}
    public List<PhaseCardParentManager> GetPhaseCardParentManagers() {
        if (this.phaseCardParentManagers == null) {
            this.phaseCardParentManagers = GetComponentsInChildren<PhaseCardParentManager>().ToList<PhaseCardParentManager>();
        }
        return this.phaseCardParentManagers;
    }
    //public List<MultipleCardImageChangeManager> GetMultipleCardImageManager() {
    //    if (this.multipleCardImageManager == null) {
    //        this.multipleCardImageManager = GetComponentsInChildren<MultipleCardImageChangeManager>().ToList<MultipleCardImageChangeManager>();
    //    }
    //    return this.multipleCardImageManager;
    //}
}
