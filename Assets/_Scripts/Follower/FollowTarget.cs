using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowTarget : MonoBehaviour {
	public Transform _target;

	private NavMeshAgent _navMeshAgent;

	// Use this for initialization
	void Awake () {
		_navMeshAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		_navMeshAgent.destination = _target.position;
	}

	void OnDisable(){
		Quaternion rot = Quaternion.Euler(0,180,0);
		transform.rotation = rot;
		if(_navMeshAgent.isActiveAndEnabled)
			_navMeshAgent.isStopped = true;
	}

	void OnEnable(){
		_navMeshAgent.isStopped = false;
	}
}
