using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen_UIMenu : MainScreen_TitleScene {

    /// <summary>
    /// Plays an animated hide before hiding game object.
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShowRoutine() {
        General.LogEntrance("Menu Show");
        base.Show();
        this.GetContentScreenAnimatable().MenuShow();
        // Stall while hiding animation is still playing. Animation must trigger a AnimationStop event.
        while (this.GetContentScreenAnimatable().IsPlaying()) {
            yield return null;
        }
    }


    /// <summary>
    /// Plays an animated hide before hiding game object.
    /// </summary>
    /// <returns></returns>
    public IEnumerator HideRoutine() {
        General.LogEntrance("Menu Hide");
        this.GetContentScreenAnimatable().MenuHide();
        // Stall while hiding animation is still playing. Animation must trigger a AnimationStop event.
        while (this.GetContentScreenAnimatable().IsPlaying()) {
            yield return null;
        }
        base.Hide();
    }
}
