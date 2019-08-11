using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour {

	[SerializeField] bool isEnabled = true;
	[SerializeField] float updateEverySec = .1f;

	private Text text;
	private int fps;
	public int FPS {
		get { return fps; }
	}
	private float timeElapsed = 0f;
	private float fpsSum = 0f;
	private int fpsCount = 0;

	private void Awake() {

		if (isEnabled) {
			DontDestroyOnLoad(gameObject);
		}
		else {
			Destroy(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		this.text = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		fpsSum += 1 / Time.deltaTime;
		fpsCount += 1;

		if (timeElapsed >= updateEverySec) {
			fps = (int) fpsSum / fpsCount;
			text.text = fps.ToString();

			fpsSum = 0;
			fpsCount = 0;
			timeElapsed = 0f;
		}
		timeElapsed += Time.deltaTime;
	}
}
