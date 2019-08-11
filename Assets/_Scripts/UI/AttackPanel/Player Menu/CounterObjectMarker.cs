using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Counter class that hides it's text object marker if 0.
/// </summary>
public class CounterObjectMarker : TextObjectMarker {
    [SerializeField] protected TextObjectMarker counterObject;
    [SerializeField] protected int count;
    [SerializeField] protected bool triggerZero = true;

    void Awake() {
        this.Initialize();
    }

    public virtual void Initialize() {
        this.SetCount(0);
    }

    public virtual void SetCount(int newCount) {
        this.count = newCount;
        this.GetCounterObject().SetText(""+this.count);
        if (this.triggerZero) {
            this.TriggerZero();
        }
    }

    /// <summary>
    /// Calls a function if zero.
    /// Default is hide count if zero.
    /// </summary>
    public virtual void TriggerZero() {
        if(this.count == 0) {
            this.Hide();
        }
    }


    public TextObjectMarker GetCounterObject() {
        if(this.counterObject == null) {
            this.counterObject = GetComponentInChildren<TextObjectMarker>();
        }
        return this.counterObject;
    }
}
