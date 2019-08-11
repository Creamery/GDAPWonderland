using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairAudible : Audible {
	[SerializeField] protected AudibleNames.Crosshair id = AudibleNames.Crosshair.MISS;

	public AudibleNames.Crosshair GetID() {
		return this.id;
	}
}
