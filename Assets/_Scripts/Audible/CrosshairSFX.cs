using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairSFX : SFX {

	//[SerializeField] private CrosshairAudible[] audibles;
	private Dictionary<AudibleNames.Crosshair, AudioClip> audioPairs;

	void Start () {
		this.audioSource = gameObject.AddComponent<AudioSource> ();
		//this.audibles = GetComponents<CrosshairAudible>();
		audioSource.playOnAwake = false;
		GenerateAudioPairs ();
	}


	public AudioClip GetAudioPairs(AudibleNames.Crosshair name) {
		if (this.audioPairs == null) {
			this.GenerateAudioPairs ();
		}
		if (this.audioPairs.ContainsKey (name)) {
			return this.audioPairs [name];
		}
		else {
			return null;
		}
	}

	public AudioSource GetAudioSource(AudibleNames.Crosshair name) {
		this.GetAudioSource().clip = this.GetAudioPairs (name);
		return this.audioSource;
	}

	void GenerateAudioPairs() {
		this.audioPairs = new Dictionary<AudibleNames.Crosshair, AudioClip>();
		foreach (CrosshairAudible audible in this.GetAudibles()) {
			audioPairs.Add (audible.GetID(), audible.GetAudioClip());
		}
	}

    public CrosshairAudible[] GetAudibles() {
        return this.GetComponents<CrosshairAudible>();
    }

}
