using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
	public Follower.Type[] _followerLoveTypes;
	public Follower.Type[] _followerHateTypes;
	public Sprite _targetIcon;
	public SpriteRenderer _directionIndicator;

	private Vector3 _directionIndicatorOriginalPosition;

	void OnTriggerEnter(Collider collider){
		Leader leader = collider.gameObject.GetComponent<Leader>();
		if(leader != null){
			leader.ReachTarget(this);
		}
	}
	// Use this for initialization
	void Start () {
		_directionIndicatorOriginalPosition = _directionIndicator.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 viewport = Camera.main.WorldToViewportPoint(transform.position);
		if(viewport.x < 0 || viewport.x > 1 || viewport.y < 0 || viewport.y > 1){
			viewport.x = Mathf.Clamp(viewport.x, .05f, .95f);
			viewport.y = Mathf.Clamp(viewport.y, .05f, .95f);
			
			_directionIndicator.transform.position = Camera.main.ViewportToWorldPoint(viewport);
		} else {
			_directionIndicator.transform.localPosition = _directionIndicatorOriginalPosition;
		}
	}
}
