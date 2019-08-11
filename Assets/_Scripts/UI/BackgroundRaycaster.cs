using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class runs in the background perpetually.
/// </summary>
public class BackgroundRaycaster : MonoBehaviour {

	[SerializeField] private GameObject onHitParticleSystem;

	private static BackgroundRaycaster sharedInstance;
	public static BackgroundRaycaster Instance {
		get { return sharedInstance; }
	}

	private bool inAttackMode;
	public bool InAttackMode {
		get { return inAttackMode; }
	}

	//[SerializeField] private float backupMatShowDuration = 1.5f;
	//float curMatShown;

	private Soldier previousSoldier;
	private LifeObjectCollider previousLOC = null;

	private BackupMatCollider bmcReference;

	private void Awake() {
		sharedInstance = this;
	}

	// Use this for initialization
	void Start () {
		inAttackMode = false;
	}

	// Update is called once per frame
	void Update() {
		PlayerManager curPlayer = MainScreenManager_GameScene.Instance.GetPlayer();

		RaycastHit hit;
		Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
		Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, .2f);
		// If hit
		if (Physics.Raycast(ray, out hit, float.PositiveInfinity)) {

			// If the AttackManager Panel is shown
			if (inAttackMode) {
				// If game object hit contains SoldierCollider.cs, enable the outline for the target soldier
				if (hit.collider.gameObject.GetComponent<SoldierCollider>() != null) {
					Debug.Log("<color=red>Soldier HIT </color>");
					SetTarget(hit.collider.gameObject.GetComponent<SoldierCollider>());
				}
				else if(hit.collider.GetComponent<LifeObjectCollider>() != null) {
					LifeObjectCollider targetLOC = hit.collider.GetComponent<LifeObjectCollider>();
					if (targetLOC.GetPlayer() != curPlayer) {
						Debug.Log("<color=red>targetLOC HIT </color>");
						SetTarget(targetLOC);
					}
				}
				else {
					// If no hit during attack mode
					RemoveTarget();
				}
			}
			//// If game object hit contains BackupMatCollider.cs, open the Backup Mat Drawer and flip all backup cards up.
			//else if (hit.collider.gameObject.GetComponent<BackupMatCollider>() != null) {
			//	//targetBackup = hit.collider.gameObject.GetComponent<SoldierBackupCollider>().GetSoldierBackup();
			//	if (bmcReference == null) {
			//		BackupMatCollider bmc = hit.collider.gameObject.GetComponent<BackupMatCollider>();
			//		if (bmc.GetPlayer() == curPlayer) {
			//			bmcReference = bmc;
			//			bmc.GetParent().OpenBackupMat(true);
			//		}
			//	}
			//	//curMatShown = 0f;
			//}
			//// If Backup mat is not hit and the backup mat is opened
			//else if (hit.collider.gameObject.GetComponent<SoldierBackupCollider>() == null && hit.collider.gameObject.GetComponent<BackupMatCollider>() == null && bmcReference != null) {
			//	curMatShown += Time.deltaTime;
			//	// If the backup mat has not been hit for X duration, hide the backup mat
			//	if (curMatShown >= backupMatShowDuration) {
			//		curMatShown = 0f;
			//		bmcReference.GetParent().OpenBackupMat(false);
			//		bmcReference = null;
			//	}
			//}
			// If player hit

			// If others hit
		}
		else {
			// If no hit
		}
	}

	public void ActivateParticle() {
		if (this.previousSoldier != null) {
			Transform targetCollider = this.previousSoldier.GetComponentInChildren<Collider>().transform;
			GameObject hitPs = Instantiate(onHitParticleSystem, targetCollider);
			hitPs.transform.position = targetCollider.position;
		}

		if (this.previousLOC != null) {
			Transform targetCollider = this.previousLOC.GetComponentInChildren<Collider>().transform;
			GameObject hitPs = Instantiate(onHitParticleSystem, targetCollider);
			hitPs.transform.position = targetCollider.position;
		}
		//RaycastHit hit;
		//Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
		//if (Physics.Raycast(ray, out hit, float.PositiveInfinity)) {
		//	GameObject hitPs = Instantiate(onHitParticleSystem,hit.collider.gameObject.transform);
		//	hitPs.transform.position = hit.collider.gameObject.transform.position;
		//	//hitPs.transform.LookAt(Camera.main.transform.position);
		//	Debug.Log("particle playing");
		//}
	}

	/// <summary>
	/// Hides the backup mat
	/// </summary>
	public void ResetBackupMat() {
		if (bmcReference != null)
			bmcReference.GetParent().OpenBackupMat(false);
		else
			ArenaUpdateHandler.Instance.GetSoldierDefenseGroup(GameMaster.Instance.GetCurPlayer().GetPlayerNumber()).OpenBackupMat(false);
		bmcReference = null;
	}

	public Soldier GetTargetSoldier() {
		if (!inAttackMode)
			return null;
		return this.previousSoldier;
	}

	public LifeObjectCollider GetLifeCollider() {
		if (!inAttackMode)
			return null;
		return this.previousLOC;
	}

	public void SetAttackMode(bool val) {
		this.inAttackMode = val;
		if (!val) {
			if (previousLOC != null)
				previousLOC.EnableOutline(false);
			if (previousSoldier != null)
				previousSoldier.EnableOutline(false);

			previousLOC = null;
			previousSoldier = null;
		}
	}

	private void RemoveTarget() {
		if (this.previousLOC != null)
			previousLOC.EnableOutline(false);
		if (this.previousSoldier != null)
			previousSoldier.EnableOutline(false);

		previousLOC = null;
		previousSoldier = null;
	}

	private void SetTarget(SoldierCollider target) {
		Soldier newTarget = target.GetSoldier();
		if (newTarget.GetPlayerOwner() != MainScreenManager_GameScene.Instance.GetPlayer() && newTarget.GetCardReference() != null) {

			if (this.previousSoldier != newTarget && this.previousSoldier != null)
				this.previousSoldier.EnableOutline(false);

			newTarget.EnableOutline(true);
			this.previousSoldier = newTarget;

			if (previousLOC != null)
				this.previousLOC.EnableOutline(false);

			this.previousLOC = null;
		}
	}

	private void SetTarget(LifeObjectCollider target) {
		LifeObjectCollider newLOC = target;
		if (target.GetPlayer() != MainScreenManager_GameScene.Instance.GetPlayer()) {
			if (this.previousLOC != null)
				previousLOC.EnableOutline(false);
			if (this.previousSoldier != null)
				previousSoldier.EnableOutline(false);

            // Check if no soldiers before enabling outline?
            if(!target.GetPlayer().GetCardManager().GetDefenseManager().HasFrontDefense()) {
                newLOC.EnableOutline(true);
            }
            else {
                newLOC.EnableOutline(false);
            }
			previousLOC = newLOC;
			previousSoldier = null;
		}
	}
}
