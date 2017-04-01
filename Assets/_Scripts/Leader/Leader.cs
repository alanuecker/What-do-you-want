using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour {
	public List<Follower> _followerActive;
	public List<Follower> _followerAll;
	public List<Target> _targets;
	public Target[] _possibleTargets;
	public CrowdManager _crowdManager;

	private Transform _lastTarget;


	void AskForTargets(){
		foreach(Follower follower in _followerActive)
			follower.DemandTarget(this);
	}

	IEnumerator UpdateTarget(){
		WaitForSeconds wait = new WaitForSeconds(1);
		Vector3 oldPos = transform.position; 
		while(true){
			yield return wait;
			_targets = new List<Target>();
			foreach(Target target in _crowdManager._targets){
				float angle = Vector3.Angle(oldPos - transform.position, oldPos - target.transform.position);
				if(angle < 35f){
					_targets.Add(target);
				} 
			}
		}
	}
	public void ReachTarget(Target target){
		if(target == _lastTarget)
			return;

		_lastTarget = target.transform;
		foreach(Follower follower in _followerActive)
			follower.ReachTarget(target, this);
	}

	public void AddFollower(Follower follower){
		_crowdManager.AddActiveFollower(follower);
	}

	public void RemoveFollower(Follower follower){
		_crowdManager.RemoveActiveFollower(follower);
	}

	public void AddDemandCount(Target.Type type){
		_crowdManager.AddDemandCount(type);
	}

	public void RemoveDemandCount(Target.Type type){
		_crowdManager.RemoveDemandCount(type);
	}

	public void SetAllFollowers(List<Follower> allFollower){
		_followerAll = allFollower;
	}

	public void SetActiveFollowers(List<Follower> activeFollower){
		_followerActive = activeFollower;
	}

	// Use this for initialization
	void Start () {
		_followerAll = new List<Follower>();
		_followerActive = new List<Follower>();
		
		_targets = new List<Target>();
		_crowdManager = GameObject.FindGameObjectWithTag("CrowdManager").GetComponent<CrowdManager>();
		AskForTargets();
		StartCoroutine(UpdateTarget());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
