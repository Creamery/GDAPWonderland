using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemCountManager : MonoBehaviour {
    [SerializeField] protected CounterObjectMarker counterObject;

    public void UpdateCount(int count) {
        this.GetCounterObject().SetCount(count);
    }

    public CounterObjectMarker GetCounterObject() {
        if (this.counterObject == null) {
            this.counterObject = GetComponentInChildren<CounterObjectMarker>();
        }
        return this.counterObject;
    }
}
