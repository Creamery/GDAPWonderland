using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen_UIAnnouncement : MainScreen {
    /// <summary>
    /// Variable that holds the rule passed by the event broadcaster.
    /// TODO: Access via singleton for consistency (?)
    /// </summary>
    [SerializeField] Rules currentRule;
    [SerializeField] private AnnouncementScreenAnimatable screenAnimatable;
    [SerializeField] private AnnouncementTextObjectMarker announcementTextObject;
    [SerializeField] private EventIconChanger eventIconChanger;

    //private void Awake() {
    //    EventBroadcaster.Instance.AddObserver(EventNames.UI.RULE_UPDATE, UpdateRule);
    //}
    //private void OnDestroy() {
    //    EventBroadcaster.Instance.RemoveObserver(EventNames.UI.RULE_UPDATE);
    //}

    /// <summary>
    /// Confirm button action. Switch what to do next depending on current rule.
    /// </summary>
    public void Confirm() {
        General.LogEntrance("MainScreenUI_Announcement Confirm");

        SoundManager.Instance.Play(AudibleNames.Button.DEFAULT);
        switch(this.currentRule) {
            case Rules.RED_CARDS_ONLY:
                GameMaster.Instance.SetEventSettled(true);
                this.Hide(); // Hide screen then UPDATE RULE PANEL (TODO)
                break;

            default:
                GameMaster.Instance.SetEventSettled(true);
                this.Hide(); // Hide screen then UPDATE RULE PANEL (TODO)
                break;
        }
        //UIHandCardManager.Instance.ShowHand(true);
    }

    public AnnouncementTextObjectMarker GetAnnouncementTextObject() {
        if(this.announcementTextObject == null) {
            this.announcementTextObject = GetComponentInChildren<AnnouncementTextObjectMarker>();
        }
        return this.announcementTextObject;
    }

    // Receiver of the rule update event. TODO: change?
    //public void UpdateRule(Parameters param) {
    //    RuleInfo ruleInfo = param.GetObjectExtra(RuleManager.RULE_PARAM) as RuleInfo;
    //    this.currentRule = ruleInfo.rule;
    //}

    public IEnumerator ShowRoutine(Rules rule) {
        this.GetEventIconChanger().setEvent(rule);
        // Set description text according to rule
        this.GetAnnouncementTextObject().SetText(rule);
        base.Show();
        this.GetScreenAnimatable().Show();
        yield return new WaitForSeconds(AnnouncementScreenAnimatable.f_SHOW);

        // TODO: Make dynamic. Must get current rule (recently updated)
        this.currentRule = rule; // Store rule for Confirm use
        //this.GetScreenAnimatable().TriggerRule(rule);
        this.GetScreenAnimatable().TriggerRule(rule);
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
    /// Announcement screen animatable getter.
    /// </summary>
    /// <returns></returns>
    public AnnouncementScreenAnimatable GetScreenAnimatable() {
        if(this.screenAnimatable == null) {
            this.screenAnimatable = GetComponent<AnnouncementScreenAnimatable>();
        }
        return this.screenAnimatable;
    }

    public EventIconChanger GetEventIconChanger() {
        if (this.eventIconChanger == null) {
            this.eventIconChanger = GetComponent<EventIconChanger>();
        }
        return this.eventIconChanger;
    }
}
