using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventItemGet : MonoBehaviour {


    [SerializeField] private EventAttackUpMarker attackUpIcon;
    [SerializeField] private EventDefenseUpMarker defenseUpIcon;
    [SerializeField] private EventItemSkillMarker skillIcon;
    [SerializeField] private EventItemBombMarker bombIcon;

    private const string NAME_ATTACK_UP = "Attack Up";
    private const string NAME_DEFENSE_UP = "Defense Up";
    private const string NAME_BOMB_ICON = "Bomb Icon";
    private const string NAME_SKILL_ICON = "Skill Icon";


    public void GetItem(Rules rule) {
        switch (rule) {
            case Rules.BOMB:
                this.GetBomb().Show();
                break;

            case Rules.SUMMON:
                this.GetSkill().Show();
                break;

            case Rules.STRDEF:
                this.GetDefenseUp().Show();
                break;

            case Rules.STRHAND:
                this.GetAttackUp().Show();
                break;
        }
    }

    public void HideAll() {
        this.GetAttackUp().Hide();
        this.GetDefenseUp().Hide();
        this.GetSkill().Hide();
        this.GetBomb().Hide();
    }

    public EventAttackUpMarker GetAttackUp() {
        if (this.attackUpIcon == null) {
            this.attackUpIcon = GetComponentInChildren<EventAttackUpMarker>();
        }
        return this.attackUpIcon;
    }
    public EventDefenseUpMarker GetDefenseUp() {
        if (this.defenseUpIcon == null) {
            this.defenseUpIcon = GetComponentInChildren<EventDefenseUpMarker>();
        }
        return this.defenseUpIcon;
    }
    public EventItemSkillMarker GetSkill() {
        if (this.skillIcon == null) {
            this.skillIcon = GetComponentInChildren<EventItemSkillMarker>();
        }
        return this.skillIcon;
    }
    public EventItemBombMarker GetBomb() {
        if (this.bombIcon == null) {
            this.bombIcon = GetComponentInChildren<EventItemBombMarker>();
        }
        return this.bombIcon;
    }

}
