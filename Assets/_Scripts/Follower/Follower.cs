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
	public List<Target> _possibleTargetsTierOne;
	public List<Target>_possibleTargetsTierTwo;
	public List<Target> _possibleTargetsTierThree;

	public Sprite _like;
	public Sprite _dislike;
	public Sprite _love;
	public Sprite _hate;
	

	private Leader _leader;
	private float _loyalty;
	private int _demandLevel;
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

	private int Demand{
		set{
			if(value != _demandLevel){
				_demandLevel = value;
				switch(_demandLevel){
					case 0: 
						_unusedTargets = new List<Target>(_possibleTargetsTierOne);
						break;
					case 1: 
						_unusedTargets = new List<Target>(_possibleTargetsTierTwo);
						break;
					case 2: 
						_unusedTargets = new List<Target>(_possibleTargetsTierThree);
						break;
				}
			}
		}
		get{
			return _demandLevel;
		}
	}

	// Use this for initialization
	void Awake () {
		_followTarget = GetComponent<FollowTarget>();
		_isFollowingPlayer = _followTarget.enabled;

		_unusedTargets = new List<Target>(_possibleTargetsTierOne);
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
					_leader = leader;
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
			_unusedTargets = GetPossibleTargetTier();
		
		Target randomTarget = _unusedTargets[Random.Range(0, _unusedTargets.Count)];
		_unusedTargets.Remove(randomTarget);

		SetTarget(randomTarget);
	}

	public void SetDemand(int demand){
		Demand = demand;
	}
	void SetTarget(Target target){
		_speechBubble.TargetIcon = target._targetIcon;
		_currentTarget = target;
	}

	public void SetPossibleTargets(List<Target> tierOne, List<Target> tierTwo, List<Target> tierThree){
		_possibleTargetsTierOne = tierOne;
		_possibleTargetsTierTwo = tierTwo;
		_possibleTargetsTierThree = tierThree;
	}

	void Add(Leader leader){
		_loyalty = -2;
		_followTarget.enabled = true;
		leader.GetComponent<Leader>().AddFollower(this);
		_isFollowingPlayer = true;
	}

	void Remove(){
		_followTarget.enabled = false;
		_isFollowingPlayer = false;
		_leader.RemoveFollower(this);
	}
	
	List<Target> GetPossibleTargetTier(){
		switch(_demandLevel){
			case 0: 
				return new List<Target>(_possibleTargetsTierOne);
			case 1: 
				return new List<Target>(_possibleTargetsTierTwo);
			case 2: 
				return new List<Target>(_possibleTargetsTierThree);
		}

		//default shouldn't be reached
		return new List<Target>(_possibleTargetsTierOne);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
