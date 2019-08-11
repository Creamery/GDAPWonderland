using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSFX : SFX {

	//[SerializeField] private FireAudible[] audibles;
	private Dictionary<AudibleNames.Fire, AudioClip> audioPairs;

	void Start () {
		this.audioSource = gameObject.AddComponent<AudioSource> ();
		//this.audibles = GetComponents<FireAudible>();
		audioSource.playOnAwake = false;
		GenerateAudioPairs ();
	}


	public AudioClip GetAudioPairs(AudibleNames.Fire name) {
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

	public AudioSource GetAudioSource(AudibleNames.Fire name) {
		this.GetAudioSource().clip = this.GetAudioPairs (name);
		return this.audioSource;
	}

	void GenerateAudioPairs() {
		this.audioPairs = new Dictionary<AudibleNames.Fire, AudioClip>();
		foreach (FireAudible audible in this.GetAudibles()) {
			audioPairs.Add (audible.GetID(), audible.GetAudioClip());
		}
	}

    public FireAudible[] GetAudibles() {
        return this.GetComponents<FireAudible>();
    }

}
