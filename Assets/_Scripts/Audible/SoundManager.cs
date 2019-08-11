using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the sound effects.
/// </summary>
public class SoundManager : MonoBehaviour {

	static SoundManager instance;

	public static SoundManager Instance {
		get { return instance; }
	}

    /// <summary>
    /// Reference to the background music.
    /// </summary>
	[SerializeField] private AudioSource backgroundMusic;

	// Add an SFX for each object and a Play(AudibleNames.<Class> name) function
	// Also create an enum entry in AudibleNames.cs

    // : ADD :
	[SerializeField] private ButtonSFX buttonSFX;
    [SerializeField] private CrosshairSFX crosshairSFX;
    [SerializeField] private TargetSFX targetSFX;
    [SerializeField] private FireSFX fireSFX;

    /// <summary>
    /// List of all SFX.
    /// </summary>
    public List<SFX> sfxList;

    /// <summary>
	/// Unity Function. Called once upon creation of the object.
	/// </summary>
	private void Awake() {
		instance = this;
        sfxList = new List<SFX>();
        GetAllSFX();
	}

    /// <summary>
    /// Put all SFX reference into the SFX list.
    /// </summary>
    void GetAllSFX() {
        // : ADD :
        sfxList.Add(buttonSFX);
        sfxList.Add(crosshairSFX);
        sfxList.Add(targetSFX);
        sfxList.Add(fireSFX);
    }

    /// <summary>
	/// Standard Unity Function. Called once in the game object's lifetime to initiate the script once it has been enabled.
	/// </summary>
	void Start () {
		if(backgroundMusic != null)
			backgroundMusic.Play();
	}

    /// <summary>
    /// Stop background music.
    /// </summary>
	public void StopBG() {
		if(backgroundMusic != null)
			backgroundMusic.Stop();
	}

    /// <summary>
    /// Play an audio source which can be an SFX or music.
    /// </summary>
    /// <param name="source">Audio Source</param>
	public void PlaySource (AudioSource source) {
		source.Play ();
	}

    /// <summary>
    /// Play an audio source. Uses Unity's PlayOneShot function.
    /// </summary>
    /// <param name="source">Audio Source</param>
	public void PlayOneShot(AudioSource source) {
		source.PlayOneShot (source.clip);
	}


    // : ADD:
    
    /// <summary>
    /// Override Play function for door SFX.
    /// </summary>
    /// <param name="name">SFX name</param>
    /// <param name="isLoop">If the sound will loop.</param>
	public void Play(AudibleNames.Button name, bool isLoop = false) {
		if (buttonSFX == null) {
			if (GetComponentInChildren<ButtonSFX> () != null) {
				this.buttonSFX = GetComponentInChildren<ButtonSFX> ();
				if (this.buttonSFX.GetAudioSource (name) != null) {
					this.buttonSFX.GetAudioSource (name).loop = isLoop;
					this.PlaySource (buttonSFX.GetAudioSource(name));
				}
			}
		}
		else {
			if (this.buttonSFX.GetAudioSource (name) != null) {
				this.buttonSFX.GetAudioSource (name).loop = isLoop;
				this.PlaySource (buttonSFX.GetAudioSource(name));
			}
		}
	}

    public void Play(AudibleNames.Crosshair name, bool isLoop = false) {
		if (crosshairSFX == null) {
			if (GetComponentInChildren<CrosshairSFX> () != null) {
				this.crosshairSFX = GetComponentInChildren<CrosshairSFX> ();
				if (this.crosshairSFX.GetAudioSource (name) != null) {
					this.crosshairSFX.GetAudioSource (name).loop = isLoop;
					this.PlaySource (crosshairSFX.GetAudioSource(name));
				}
			}
		}
		else {
			if (this.crosshairSFX.GetAudioSource (name) != null) {
				this.crosshairSFX.GetAudioSource (name).loop = isLoop;
				this.PlaySource (crosshairSFX.GetAudioSource(name));
			}
		}
	}

    public void Play(AudibleNames.Target name, bool isLoop = false) {
        if (targetSFX == null) {
            if (GetComponentInChildren<TargetSFX>() != null) {
                this.targetSFX = GetComponentInChildren<TargetSFX>();
                if (this.targetSFX.GetAudioSource(name) != null) {
                    this.targetSFX.GetAudioSource(name).loop = isLoop;
                    this.PlaySource(targetSFX.GetAudioSource(name));
                }
            }
        }
        else {
            if (this.targetSFX.GetAudioSource(name) != null) {
                this.targetSFX.GetAudioSource(name).loop = isLoop;
                this.PlaySource(targetSFX.GetAudioSource(name));
            }
        }
    }

    public void Play(AudibleNames.Fire name, bool isLoop = false) {
        if (fireSFX == null) {
            if (GetComponentInChildren<FireSFX>() != null) {
                this.fireSFX = GetComponentInChildren<FireSFX>();
                if (this.fireSFX.GetAudioSource(name) != null) {
                    this.fireSFX.GetAudioSource(name).loop = isLoop;
                    this.PlaySource(fireSFX.GetAudioSource(name));
                }
            }
        }
        else {
            if (this.fireSFX.GetAudioSource(name) != null) {
                this.fireSFX.GetAudioSource(name).loop = isLoop;
                this.PlaySource(fireSFX.GetAudioSource(name));
            }
        }
    }
}