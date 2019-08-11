using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Animatable {
	void ResetTriggers ();
	void ResetTriggerVariables ();

	bool IsPlaying ();
	void AnimationPlay ();
	void AnimationStop ();
	void SwitchTriggers ();

}
