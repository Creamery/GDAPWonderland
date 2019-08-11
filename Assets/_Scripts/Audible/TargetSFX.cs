using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSFX : SFX {

	//[SerializeField] private TargetAudible[] audibles;
	private Dictionary<AudibleNames.Target, AudioClip> audioPairs;

	void Start () {
		this.audioSource = gameObject.AddComponent<AudioSource> ();
		//this.audibles = GetComponents<TargetAudible>();
		audioSource.playOnAwake = false;
		GenerateAudioPairs ();
	}


	public AudioClip GetAudioPairs(AudibleNames.Target name) {
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

	public AudioSource GetAudioSource(AudibleNames.Target name) {
		this.GetAudioSource().clip = this.GetAudioPairs (name);
		return this.audioSource;
	}

	void GenerateAudioPairs() {
		this.audioPairs = new Dictionary<AudibleNames.Target, AudioClip>();
		foreach (TargetAudible audible in this.GetAudibles()) {
			audioPairs.Add (audible.GetID(), audible.GetAudioClip());
		}
	}

    public TargetAudible[] GetAudibles() {
        return this.GetComponents<TargetAudible>();
    }

}
