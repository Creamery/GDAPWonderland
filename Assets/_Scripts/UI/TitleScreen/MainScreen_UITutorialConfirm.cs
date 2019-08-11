using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen_UITutorialConfirm : MainScreen_TitleScene {
    [SerializeField] private CircleConfirmationAnimatable circleConfirmationAnimatable;

    /// <summary>
    /// Plays an animated hide before hiding game object.
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShowRoutine() {
        General.LogEntrance("TutorialConfirm Show");
        base.Show();

        this.GetContentScreenAnimatable().TutorialConfirmShow();

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
        General.LogEntrance("TutorialConfirm Hide");

        this.GetCircleConfirmationAnimatable().Hide();
        yield return new WaitForSeconds(CircleConfirmationAnimatable.f_HIDE);

        yield return null;
    }

    public CircleConfirmationAnimatable GetCircleConfirmationAnimatable() {
        if (this.circleConfirmationAnimatable == null) {
            this.circleConfirmationAnimatable = GetComponent<CircleConfirmationAnimatable>();
        }
        return this.circleConfirmationAnimatable;
    }
}
