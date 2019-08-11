using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MainScreen child for the Title scene. Contains the scene's animatable and
/// other repeated functions on the MainScreen instances for this scene only.
/// </summary>
public class MainScreen_TitleScene : MainScreen {

    [SerializeField] protected ContentScreenAnimatable contentScreenAnimatable;
    /// <summary>
    /// IsPlaying getter.
    /// </summary>
    /// <returns></returns>
    public virtual bool IsPlaying() {
        return this.GetContentScreenAnimatable().IsPlaying();
    }

    /// <summary>
    /// ContentScreenAnimatable getter.
    /// </summary>
    /// <returns></returns>
    public ContentScreenAnimatable GetContentScreenAnimatable() {
        if (this.contentScreenAnimatable == null) {
            this.contentScreenAnimatable = GetComponentInParent<ContentScreenAnimatable>();
        }
        return this.contentScreenAnimatable;
    }
}
