using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen_UIQuitConfirm : MainScreen_TitleScene {
    [SerializeField] private CircleConfirmationAnimatable circleConfirmationAnimatable;

    /// <summary>
    /// Plays an animated hide before hiding game object.
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShowRoutine() {
        General.LogEntrance("QuitConfirm Show");
        base.Show();

        this.GetContentScreenAnimatable().QuitConfirmShow();
        // Stall while hiding animation is still playing. Animation must trigger a AnimationStop event.
        while (this.GetContentScreenAnimatable().IsPlaying()) {
            yield return null;
        }

        this.GetCircleConfirmationAnimatable().Show();
        // Stall while hiding animation is still playing. Animation must trigger a AnimationStop event.
        yield return new WaitForSeconds(CircleConfirmationAnimatable.f_SHOW);
    }

    /// <summary>
    /// Plays an animated hide before hiding game object.
    /// </summary>
    /// <returns></returns>
    public IEnumerator HideRoutine() {
        General.LogEntrance("QuitConfirm Hide");

        //this.GetContentScreenAnimatable().QuitConfirmShow();
        this.GetCircleConfirmationAnimatable().Hide();
        yield return new WaitForSeconds(CircleConfirmationAnimatable.f_HIDE);

        //base.Hide();
        yield return null;
    }

    public CircleConfirmationAnimatable GetCircleConfirmationAnimatable() {
        if(this.circleConfirmationAnimatable == null) {
            this.circleConfirmationAnimatable = GetComponent<CircleConfirmationAnimatable>();
        }
        return this.circleConfirmationAnimatable;
    }
}
