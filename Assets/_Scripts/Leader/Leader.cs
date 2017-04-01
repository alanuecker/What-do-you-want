using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour {
	public Transform _followerParent;
	public Target _target;
	public Target[] _possibleTargets;

	private Transform _lastTarget;

	void AskForTargets(){
		foreach(Follower follower in _followerParent.GetComponentsInChildren<Follower>())
			follower.DemandTarget(this);
	}

	public void ReachTarget(Target target){
		if(target == _lastTarget)
			return;

		_lastTarget = target.transform;
		foreach(Follower follower in _followerParent.GetComponentsInChildren<Follower>())
			follower.ReachTarget(target, this);
	}

	// Use this for initialization
	void Start () {
		AskForTargets();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
