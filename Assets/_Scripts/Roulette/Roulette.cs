using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roulette : MonoBehaviour {
	[SerializeField] private Pointer pointer;
	[SerializeField] private GameObject baseRoulette;
	//[SerializeField] private Rigidbody rb;
	[SerializeField] private float torque;
	[SerializeField] private float sensitivity;

	[SerializeField] private RouletteAnimatable rouletteAnimatable;
	[SerializeField] private RouletteObjectAnimatable rouletteGameObjectAnimatable;

	private Vector3 mouseRef, mouseOffset;

	private const float MIN_ROTATION_SPEED = 15f;
	private const float MAX_ROTATION_SPEED = 50f;
	private const float DELAY_AFTER_SPIN = 1f;

	private const float SPIN_DURATION = 3f;
	private Vector3 rotationSpeed;
	private float timer;

	private bool shown;
    private bool interacted;
    private bool dragged;
	private bool ended;

    //private Vector3 TORQUE_CAP = new Vector3(0, 100f, 0);

	// Use this for initialization
	void Start () {
		Debug.Log ("START");
		this.Instantiate();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (!ended && dragged) {
			timer += Time.deltaTime / SPIN_DURATION;
			Vector3 frameSpeed = new Vector3(0, Mathf.Lerp(rotationSpeed.y, 0, timer), 0);
			transform.Rotate(frameSpeed);
			if (Mathf.Abs(frameSpeed.y) == 0) {
				rotationSpeed = Vector3.zero;

				Debug.Log("ENVIRONMENT : " + pointer.GetCurrentCollider());
				StartCoroutine(HideRoutine());
				this.ended = true;
			}
		}
	}

    public IEnumerator HideRoutine() {
		Debug.Log("wait");
		yield return new WaitForSecondsRealtime(DELAY_AFTER_SPIN);
		Debug.Log("finished");
		rouletteAnimatable.Hide();
		GameMaster.Instance.SetHasSpun(true);
    }
    
	void OnMouseDrag() {
        if (!dragged && !GameMaster.Instance.GetHasSpun()) {
            //if (!GameMaster.Instance.GetHasSpun()) {
                
            //Debug.Log ("DETECTED MOUSE DRAG");
            // offset
            mouseOffset = (mouseRef - Input.mousePosition); //temp comment

            // apply rotation
            rotationSpeed.y = (mouseOffset.x + mouseOffset.y) * sensitivity; //temp comment
			Debug.Log("start rotation speed = " + rotationSpeed.y);
			
        }

    }

	void OnMouseDown(){
		if (!shown)
			return;
        if(!interacted && !GameMaster.Instance.GetHasSpun()) {
            SoundManager.Instance.Play(AudibleNames.Button.SPIN);
            this.interacted = true;
			this.ended = false;
		    mouseRef = Input.mousePosition;
        }
	}

	void OnMouseUp(){
        if(!this.dragged && this.interacted) {
			this.dragged = true;
			if (rotationSpeed.y > 0)
				rotationSpeed.y = Mathf.Clamp(rotationSpeed.y, MIN_ROTATION_SPEED, MAX_ROTATION_SPEED);
			else
				rotationSpeed.y = Mathf.Clamp(rotationSpeed.y, -MAX_ROTATION_SPEED, -MIN_ROTATION_SPEED);
			timer = 0;
			Debug.Log("final speed: "+rotationSpeed.y);
        }
    }

	public void Instantiate(){
		rotationSpeed = Vector3.zero;
		this.shown = false;
	}

	public string GetPointedTo() {
		return this.pointer.GetCurrentCollider();
	}

	public void RouletteShown() {
		this.shown = true;
	}
	
	public void RouletteHidden() {
		this.interacted = false;
		this.dragged = false;
		this.shown = false;
	}
}
