using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {
	public enum Type{
		assi,
		hippie,
		goth,
		nerd
	}

	public Type _type;
	public Target[] _possibleTargets;

	public Sprite _like;
	public Sprite _dislike;
	public Sprite _love;
	public Sprite _hate;
	

	private float _loyalty;
	private SpeechBubble _speechBubble;
	private Target _currentTarget;
	private List<Target> _unusedTargets;
	private FollowTarget _followTarget;
	private bool _isFollowingPlayer;

	private float Loyalty{
		set {
			if(value - _loyalty >= 2)
				_speechBubble._targetIcon.sprite = _love;
			else if(value - _loyalty >= 1)
				_speechBubble._targetIcon.sprite = _like;

			else if(value - _loyalty <= -1)
				_speechBubble._targetIcon.sprite = _dislike;

			else if(value - _loyalty <= -2)
				_speechBubble._targetIcon.sprite = _hate;

			_loyalty = value;
			if(_loyalty < -2)
				Remove();
		}
		get {
			return _loyalty;
		}
	}
	// Use this for initialization
	void Awake () {
		_followTarget = GetComponent<FollowTarget>();
		_isFollowingPlayer = _followTarget.enabled;

		_unusedTargets = new List<Target>(_possibleTargets);
		_speechBubble = GetComponentInChildren<SpeechBubble>();
	}

	void OnTriggerEnter(Collider collider){
		if(_isFollowingPlayer)
			return;

		Leader leader = collider.gameObject.GetComponent<Leader>();
		if(leader != null){
			foreach(Type targetType in leader._target._followerLoveTypes){
				if(targetType == _type){
					Add(leader);
					SetTarget(leader._target);
				}
			}

		}
	}

	public void ReachTarget(Target target, Leader leader){
		if(target == _currentTarget){
			Loyalty += 2;
			_followTarget._target = target.transform;
		} else {
			foreach(Follower.Type type in target._followerLoveTypes)
				if(type == _type){
					Loyalty++;
					_followTarget._target = target.transform;
				}
			foreach(Follower.Type type in target._followerHateTypes)
				if(type == _type){
					Loyalty -= 2;
				}
		}

		StartCoroutine(WaitAndDemandTarget(new WaitForSeconds(3f), leader));
	}

	IEnumerator WaitAndDemandTarget(WaitForSeconds wait, Leader leader){
		yield return wait;
		DemandTarget(leader);
	}
	public void DemandTarget(Leader leader){
		_followTarget._target = leader.transform;

		if(_unusedTargets.Count == 0)
			_unusedTargets = new List<Target>(_possibleTargets);
		
		Target randomTarget = _unusedTargets[Random.Range(0, _unusedTargets.Count)];
		_unusedTargets.Remove(randomTarget);

		SetTarget(randomTarget);
	}

	void SetTarget(Target target){
		_speechBubble.TargetIcon = target._targetIcon;
		_currentTarget = target;
	}

	void Add(Leader leader){
		_loyalty = -2;
		_followTarget.enabled = true;
		transform.SetParent(leader._followerParent);
		_isFollowingPlayer = true;
	}

	void Remove(){
		_followTarget.enabled = false;
		transform.SetParent(null);
		_isFollowingPlayer = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
