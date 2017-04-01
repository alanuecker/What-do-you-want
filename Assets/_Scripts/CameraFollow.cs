using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public Transform _target;

	private Vector3 _offset;
	// Use this for initialization
	void Start () {
		_offset = _target.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = _target.position - _offset;
	}
}
