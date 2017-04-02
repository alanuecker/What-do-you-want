using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour {
	public List<Target> _targets;
	public Target[] _possibleTargets;
	public CrowdManager _crowdManager;
	public FollowerPercentages _followerPercentages;

	private Target _lastTarget;


	void AskForTargets(){
		foreach(Follower follower in _crowdManager.ActiveFollower)
			follower.DemandTarget(this);
	}

	IEnumerator UpdateTarget(){
		WaitForSeconds wait = new WaitForSeconds(1);
		Vector3 oldPos = transform.position; 
		while(true){
			yield return wait;
			_targets = new List<Target>();
			foreach(Target target in _crowdManager._targets){
				Vector3 ownPosOffset = oldPos - transform.position;
				ownPosOffset.y = 0;
				Vector3 targetPosOffset = oldPos - target.transform.position;
				targetPosOffset.y = 0;
				float angle = Vector3.Angle(ownPosOffset, targetPosOffset);
				if(angle < 35f){
					_targets.Add(target);
				} 
			}
		}
	}
	public void ReachTarget(Target target){
		if(target == _lastTarget)
			return;
		
		_lastTarget = target;
		foreach(Follower follower in _crowdManager.ActiveFollower)
			follower.ReachTarget(target, this);
		
		float followerCount = _crowdManager.ActiveFollower.Count;
		_followerPercentages.SetPercentages((float)_crowdManager.AssiCount / followerCount, (float)_crowdManager.HippieCount / followerCount,
				(float)_crowdManager.NerdCount / followerCount, (float)_crowdManager.GothCount / followerCount );
		_followerPercentages._activeFollowers.text = "" + (int)followerCount;
		_followerPercentages._totalFollowers.text = "" + (int)_crowdManager.AllFollower.Count;
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

	// Use this for initialization
	void Start () {		
		_targets = new List<Target>();
		_crowdManager = GameObject.FindGameObjectWithTag("CrowdManager").GetComponent<CrowdManager>();
		AskForTargets();
		StartCoroutine(UpdateTarget());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
