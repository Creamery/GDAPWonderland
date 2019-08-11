using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNumberOperator : ObjectMarker {


    [SerializeField] private EventOperatorPlusMarker plusOperator;
    [SerializeField] private EventOperatorTimesMarker timesOperator;
    [SerializeField] private EventOperatorDivideMarker divideOperator;



    public void ShowPlus() {
        this.HideAll();
        this.GetPlusMarker().Show();

    }

    public void ShowTimes() {
        this.HideAll();
        this.GetTimesMarker().Show();

    }

    public void ShowDivide() {
        this.HideAll();
        this.GetDivideMarker().Show();
    }

    public void HideAll() {
        this.GetPlusMarker().Hide();
        this.GetTimesMarker().Hide();
        this.GetDivideMarker().Hide();
    }

    public EventOperatorPlusMarker GetPlusMarker() {
        if (this.plusOperator == null) {
            this.plusOperator = GetComponentInChildren<EventOperatorPlusMarker>();
        }
        return this.plusOperator;
    }
    public EventOperatorTimesMarker GetTimesMarker() {
        if (this.timesOperator == null) {
            this.timesOperator = GetComponentInChildren<EventOperatorTimesMarker>();
        }
        return this.timesOperator;
    }
    public EventOperatorDivideMarker GetDivideMarker() {
        if (this.divideOperator == null) {
            this.divideOperator = GetComponentInChildren<EventOperatorDivideMarker>();
        }
        return this.divideOperator;
    }
}
