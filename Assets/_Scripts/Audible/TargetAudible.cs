using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAudible : Audible {
	[SerializeField] protected AudibleNames.Target id = AudibleNames.Target.FOUND;

	public AudibleNames.Target GetID() {
		return this.id;
	}
}
