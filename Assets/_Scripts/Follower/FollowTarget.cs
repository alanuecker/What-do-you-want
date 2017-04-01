using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {
	public Vector3 _target;

	private NavMeshAgent _navMeshAgent;

	// Use this for initialization
	void Awake () {
		_navMeshAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		_navMeshAgent.destination = _target;
	}

	void OnDisable(){
		Quaternion rot = Quaternion.Euler(0,180,0);
		transform.rotation = rot;
		if(_navMeshAgent.isActiveAndEnabled)
			_navMeshAgent.Stop();
	}

	void OnEnable(){
		//_navMeshAgent.
	}

	public bool GetAtTarget(){
		return _navMeshAgent.remainingDistance < 0.5f;
	}

	public void SetTarget(Vector3 target){
		_target = target;
	}
}
