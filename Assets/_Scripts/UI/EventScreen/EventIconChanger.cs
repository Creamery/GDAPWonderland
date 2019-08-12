using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventIconChanger : MonoBehaviour {


    [SerializeField] private EventTitleBar eventTitleBar;
    [SerializeField] private EventColorBar eventColorBar;

    [SerializeField] private EventMoveChange eventMoveChange;
    [SerializeField] private EventItemGet eventItemGet;

    private Color32 colorBomb = new Color32(55, 38, 68, 255);
    private Color32 colorSkill = new Color32(1, 54, 104, 255);

    private Color32 colorPlus = new Color32(116, 4, 81, 255);
    private Color32 colorTimes = new Color32(255, 255, 255, 255);
    private Color32 colorDivide = new Color32(183, 11, 63, 255);

    private Color32 colorDefense = new Color32(108, 176, 7, 255);
    private Color32 colorAttack = new Color32(0, 183, 183, 255);
    private Color32 colorTransparent = new Color32(255, 255, 255, 0);

	private const string TITLE_HIGHER = "title_higher";
	private const string TITLE_LOWER = "title_lower";


	private const string TITLE_ATTACK_UP = "title_Attack Up";
    private const string TITLE_DEFENSE_UP = "title_Defense Up";
    private const string TITLE_BOMB_GET = "title_Bomb Get";
    private const string TITLE_SKILL_GET = "title_Skill Get";

    private const string TITLE_MOVES = "title_Moves";
    private const string TITLE_MOVE_INCREASED = "title_Move Increased";
    private const string TITLE_MOVE_DECREASED = "title_Move Decreased";
    private const string TITLE_MOVE_DOUBLED = "title_Move Doubled";
    private const string TITLE_MOVE_HALVED = "title_Move Halved";

    public void setEvent(Rules rule) {
        this.GetEventMoveChange().HideAll();
        this.GetEventItemGet().HideAll();
        switch (rule) {
			// Rule Higher
			case Rules.HIGHER:
				this.changeBarColor(colorTimes);
				this.GetEventTitleBar().GetImage().sprite = General.GetEventIconSprite(TITLE_HIGHER);
				this.GetEventMoveChange().setMoves(rule);
				break;
			// Rule Lower
			case Rules.LOWER:
				this.changeBarColor(colorDivide);
				this.GetEventTitleBar().GetImage().sprite = General.GetEventIconSprite(TITLE_LOWER);
				this.GetEventMoveChange().setMoves(rule);
				break;

			// Move plus
			case Rules.MOVEP2:
                this.changeBarColor(colorPlus);
                this.GetEventTitleBar().GetImage().sprite = General.GetEventIconSprite(TITLE_MOVE_INCREASED);
                this.GetEventMoveChange().setMoves(rule);
                break;
            case Rules.MOVEP3:
                this.changeBarColor(colorPlus);
                this.GetEventTitleBar().GetImage().sprite = General.GetEventIconSprite(TITLE_MOVE_INCREASED);
                this.GetEventMoveChange().setMoves(rule);
                break;
            case Rules.MOVEP4:
                this.changeBarColor(colorPlus);
                this.GetEventTitleBar().GetImage().sprite = General.GetEventIconSprite(TITLE_MOVE_INCREASED);
                this.GetEventMoveChange().setMoves(rule);
                break;
            case Rules.MOVEP5:
                this.changeBarColor(colorPlus);
                this.GetEventTitleBar().GetImage().sprite = General.GetEventIconSprite(TITLE_MOVE_INCREASED);
                this.GetEventMoveChange().setMoves(rule);
                break;

            // Move times
            case Rules.MOVEX2:
                this.changeBarColor(colorTimes);
                this.GetEventTitleBar().GetImage().sprite = General.GetEventIconSprite(TITLE_MOVE_DOUBLED);
                this.GetEventMoveChange().setMoves(rule);
                break;
            // Move divide
            case Rules.MOVED2:
                this.changeBarColor(colorDivide);
                this.GetEventTitleBar().GetImage().sprite = General.GetEventIconSprite(TITLE_MOVE_HALVED);
                this.GetEventMoveChange().setMoves(rule);
                break;

            // Defense up
            case Rules.STRDEF:
                this.changeBarColor(colorDefense);
                this.GetEventTitleBar().GetImage().sprite = General.GetEventIconSprite(TITLE_DEFENSE_UP);
                this.GetEventItemGet().GetItem(rule);
                break;
            // Hand up
            case Rules.STRHAND:
                this.changeBarColor(colorAttack);
                this.GetEventTitleBar().GetImage().sprite = General.GetEventIconSprite(TITLE_ATTACK_UP);
                this.GetEventItemGet().GetItem(rule);
                break;


            // Bomb
            case Rules.BOMB:
                this.changeBarColor(colorBomb);
                this.GetEventTitleBar().GetImage().sprite = General.GetEventIconSprite(TITLE_BOMB_GET);
                this.GetEventItemGet().GetItem(rule);
                break;
            // Summon
            case Rules.SUMMON:
                this.changeBarColor(colorSkill);
                this.GetEventTitleBar().GetImage().sprite = General.GetEventIconSprite(TITLE_SKILL_GET);
                this.GetEventItemGet().GetItem(rule);
                break;
        }
    }

    public void changeBarColor(Color color) {
        this.GetEventColorBar().GetImage().color = color;
    }


    public EventTitleBar GetEventTitleBar() {
        if (this.eventTitleBar == null) {
            this.eventTitleBar = GetComponentInChildren<EventTitleBar>();
        }
        return this.eventTitleBar;
    }


    public EventColorBar GetEventColorBar() {
        if (this.eventColorBar == null) {
            this.eventColorBar = GetComponentInChildren<EventColorBar>();
        }
        return this.eventColorBar;
    }

    public EventMoveChange GetEventMoveChange() {
        if (this.eventMoveChange == null) {
            this.eventMoveChange = GetComponentInChildren<EventMoveChange>();
        }
        return this.eventMoveChange;
    }

    public EventItemGet GetEventItemGet() {
        if (this.eventItemGet == null) {
            this.eventItemGet = GetComponentInChildren<EventItemGet>();
        }
        return this.eventItemGet;
    }
}
