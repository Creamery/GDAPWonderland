using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : SFX {

	//[SerializeField] private ButtonAudible[] audibles;
	private Dictionary<AudibleNames.Button, AudioClip> audioPairs;

	void Start () {
		this.audioSource = gameObject.AddComponent<AudioSource> ();
		//this.audibles = GetComponents<ButtonAudible>();
		audioSource.playOnAwake = false;
		GenerateAudioPairs ();
	}


	public AudioClip GetAudioPairs(AudibleNames.Button name) {
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

	public AudioSource GetAudioSource(AudibleNames.Button name) {
		this.GetAudioSource().clip = this.GetAudioPairs (name);
		return this.audioSource;
	}

	void GenerateAudioPairs() {
		this.audioPairs = new Dictionary<AudibleNames.Button, AudioClip>();
		foreach (ButtonAudible audible in this.GetAudibles()) {
			audioPairs.Add (audible.GetID(), audible.GetAudioClip());
		}
	}

    public ButtonAudible[] GetAudibles() {
        return this.GetComponents<ButtonAudible>();
    }

}
