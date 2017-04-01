using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowMouse : MonoBehaviour {

	public string[] _terrainLayers = { "terrain" };

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
			_navMeshAgent.destination = hit.point;
		}
	}
}
