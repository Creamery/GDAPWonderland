using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen_UILogo : MainScreen_TitleScene {
    

    /// <summary>
    /// Plays an animated hide before hiding game object.
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShowRoutine() {
        General.LogEntrance("Logo Show");
        base.Show();
        this.GetContentScreenAnimatable().LogoShow();
        // Stall while hiding animation is still playing. Animation must trigger a AnimationStop event.
        while (this.GetContentScreenAnimatable().IsPlaying()) {
            yield return null;
        }
        yield return null;
    }

    /// <summary>
    /// Plays an animated hide before hiding game object.
    /// </summary>
    /// <returns></returns>
    public IEnumerator HideRoutine() {
        General.LogEntrance("Logo Hide");
        this.GetContentScreenAnimatable().LogoHide();
        // Stall while hiding animation is still playing. Animation must trigger a AnimationStop event.
        while (this.GetContentScreenAnimatable().IsPlaying()) {
            yield return null;
        }
        base.Hide();
    }
}
