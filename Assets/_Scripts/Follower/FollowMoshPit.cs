using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowMoshPit : MonoBehaviour {
	public List<Vector3> _path;

	private int _waypointCounter;

	private NavMeshAgent _navMeshAgent;

	// Use this for initialization
	void Awake () {
		_navMeshAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if(_path.Count != 0){
			if(_navMeshAgent.remainingDistance <= 1.5f){
				_waypointCounter++;
				if(_waypointCounter >= _path.Count)
					_waypointCounter = 0;
				_navMeshAgent.SetDestination(_path[_waypointCounter]);
			}
		}
	}

	void OnDisable(){
		Quaternion rot = Quaternion.Euler(0,180,0);
		transform.rotation = rot;
	}

	void OnEnable(){
		//_navMeshAgent.
	}

	public void GoToPosition(Vector3 lastPos){
		_navMeshAgent.SetDestination(lastPos);
	}

	public void SetPath(List<Vector3> path){
		if(path.Count != 0){
			_path = path;
			_waypointCounter = 0;
			_navMeshAgent.SetDestination(_path[_waypointCounter]);
		}
	}
}
