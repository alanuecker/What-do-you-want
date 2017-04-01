﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour {
	public List<Follower> _followerActive;
	public List<Follower> _followerAll;
	public Target _target;
	public Target[] _possibleTargets;

	private Transform _lastTarget;

	void AskForTargets(){
		foreach(Follower follower in _followerActive)
			follower.DemandTarget(this);
	}

	public void ReachTarget(Target target){
		if(target == _lastTarget)
			return;

		_lastTarget = target.transform;
		foreach(Follower follower in _followerActive)
			follower.ReachTarget(target, this);
	}

	public void AddFollower(Follower follower){
		_followerActive.Add(follower);
	}

	public void SetAllFollowers(List<Follower> allFollower){
		_followerAll = allFollower;
	}

	public void SetActiveFollowers(List<Follower> activeFollower){
		_followerActive = activeFollower;
	}

	// Use this for initialization
	void Start () {
		AskForTargets();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}