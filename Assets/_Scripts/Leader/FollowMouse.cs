using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {

	public string[] _terrainLayers = { "terrain" };
	public float _deadzoneDistance = 1f;
	private NavMeshAgent _navMeshAgent;

	// Use this for initialization
	void Start () {
		_navMeshAgent = GetComponent<NavMeshAgent>();	
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;
         
        if (Physics.Raycast (ray, out hit, Mathf.Infinity, LayerMask.GetMask(_terrainLayers)))
		{
			if((hit.point - transform.position).magnitude > _deadzoneDistance){
				_navMeshAgent.destination = hit.point;
			} else
				_navMeshAgent.destination = transform.position;
		}
	}
}
