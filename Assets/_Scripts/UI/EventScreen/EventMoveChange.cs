using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMoveChange : MonoBehaviour {

    [SerializeField] private EventNumberIcon eventNumberIcon;
    [SerializeField] private EventNumberOperator eventNumberOperator;


    private const string NAME_2 = "title_2";
    private const string NAME_3 = "title_3";
    private const string NAME_4 = "title_4";
    private const string NAME_5 = "title_5";


    public void setMoves(Rules rule) {
        this.ShowAll();
        switch (rule) {
            case Rules.MOVEP2:
                this.GetEventNumberIcon().GetImage().sprite = General.GetEventIconSprite(NAME_2);
                this.GetEventNumberOperator().ShowPlus();
                break;

            case Rules.MOVEP3:
                this.GetEventNumberIcon().GetImage().sprite = General.GetEventIconSprite(NAME_3);
                this.GetEventNumberOperator().ShowPlus();
                break;

            case Rules.MOVEP4:
                this.GetEventNumberIcon().GetImage().sprite = General.GetEventIconSprite(NAME_4);
                this.GetEventNumberOperator().ShowPlus();
                break;

            case Rules.MOVEP5:
                this.GetEventNumberIcon().GetImage().sprite = General.GetEventIconSprite(NAME_5);
                this.GetEventNumberOperator().ShowPlus();
                break;


            case Rules.MOVEX2:
                this.GetEventNumberIcon().GetImage().sprite = General.GetEventIconSprite(NAME_2);
                this.GetEventNumberOperator().ShowTimes();
                break;


            case Rules.MOVED2:
                this.GetEventNumberIcon().GetImage().sprite = General.GetEventIconSprite(NAME_2);
                this.GetEventNumberOperator().ShowDivide();
                break;
        }
    }

    public void HideAll() {
        this.GetEventNumberIcon().Hide();
        this.GetEventNumberOperator().Hide();
    }
    public void ShowAll() {
        this.GetEventNumberIcon().Show();
        this.GetEventNumberOperator().Show();
    }

    public EventNumberIcon GetEventNumberIcon() {
        if (this.eventNumberIcon == null) {
            this.eventNumberIcon = GetComponentInChildren<EventNumberIcon>();
        }
        return this.eventNumberIcon;
    }


    public EventNumberOperator GetEventNumberOperator() {
        if (this.eventNumberOperator == null) {
            this.eventNumberOperator = GetComponentInChildren<EventNumberOperator>();
        }
        return this.eventNumberOperator;
    }

}
