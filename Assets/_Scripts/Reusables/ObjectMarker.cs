using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent class for simple identification functions (those used
/// in place of tags).
/// </summary>
public abstract class ObjectMarker : MonoBehaviour {

    /// <summary>
    /// Shows the game object.
    /// </summary>
    public virtual void Show() {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides the game object.
    /// </summary>
    public virtual void Hide() {
        gameObject.SetActive(false);
    }
}
