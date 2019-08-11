using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAudible : Audible {
	[SerializeField] protected AudibleNames.Fire id = AudibleNames.Fire.HEARTS;

	public AudibleNames.Fire GetID() {
		return this.id;
	}
}
