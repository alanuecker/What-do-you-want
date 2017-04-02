using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {
	public enum Type{
		assi = 0,
		nerd = 1,
		goth = 2,
		hippie = 3		
	}
	public float _chanceToFollow = .25f;
	public Type _type;
	public List<Target> _possibleTargetsTierOne;
	public List<Target>_possibleTargetsTierTwo;
	public List<Target> _possibleTargetsTierThree;

	public Sprite _like;
	public Sprite _dislike;
	public Sprite _love;
	public Sprite _hate;
	public float _lowerLoyaltyMinTime = 5f;
	public float _lowerLoyaltyMaxTime = 15f;
	

	private Leader _leader;
	private float _loyalty;
	private int _demandLevel;
	private SpeechBubbleSpawner _speechBubble;
	private Target _currentTarget;
	private List<Target> _unusedTargets;
	private FollowTarget _followTarget;
	private FollowMoshPit _followMoshPit;
	private bool _isFollowingPlayer;


	private bool _pit;

	private float Loyalty{
		set {
			if(value - _loyalty >= 3)
				_speechBubble.TargetIcon = _love;
			else if(value - _loyalty >= 1)
				_speechBubble.TargetIcon = _like;

			else if(value - _loyalty <= -1){
				_speechBubble.TargetIcon = _dislike;
			}

			else if(value - _loyalty <= -3)
				_speechBubble.TargetIcon = _hate;

			StopCoroutine(LowerLoyalty(null));
			StartCoroutine(LowerLoyalty(new WaitForSeconds(Random.Range(10, 30))));
			_loyalty = value;
			if(_loyalty < -4)
				Remove();
			else if(_loyalty > 5)
				ConvertFollower();
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

		_followMoshPit = GetComponent<FollowMoshPit>();

		_unusedTargets = new List<Target>(_possibleTargetsTierOne);
		_speechBubble = GetComponentInChildren<SpeechBubbleSpawner>();
	}

	void OnTriggerEnter(Collider collider){
		if(_isFollowingPlayer)
			return;

		Leader leader = collider.gameObject.GetComponent<Leader>();
		if(leader != null){
			foreach(Target target in leader._targets){
				foreach(Type targetType in target._followerLoveTypes){
					if(targetType == _type){
						//check if target is in active demand tier
						if(CheckTargetInDemands(target._type)){
							if(Random.value > _chanceToFollow)
								return;
							Add(leader);
							_leader = leader;
							SetTarget(target);
							return;
						}
					}
				}
			}
		}
	}

	IEnumerator LowerLoyalty(WaitForSeconds wait){
		yield return wait;
		foreach(Target target in _leader._targets){
				if(_currentTarget == target){
					
					yield return null;
				}
		}
		Loyalty--;
	}

	public void ReachTarget(Target target, Leader leader){
		if(target == _currentTarget){
			Loyalty += 3;
			_followTarget._target = target.transform;
		} else {
			/*foreach(Follower.Type type in target._followerLoveTypes)
				if(type == _type){
					Loyalty++;
					_followTarget._target = target.transform;
				}*/
			foreach(Follower.Type type in target._followerHateTypes)
				if(type == _type){
					Loyalty -= 3;
				}
		}

		StartCoroutine(WaitAndDemandTarget(new WaitForSeconds(3f), leader));
	}

	IEnumerator WaitAndDemandTarget(WaitForSeconds wait, Leader leader){
		yield return wait;
		DemandTarget(leader);
	}
	public void DemandTarget(Leader leader){
		leader.RemoveDemandCount(_currentTarget._type);
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
		_leader.AddDemandCount(target._type);
	}

	public void ConvertFollower(){
		foreach(Follower follower in _leader._crowdManager.ActiveFollower){
			if(follower._type != _type){
				Vector3 pos = follower.transform.position;
				_leader._crowdManager.ActiveFollower.Remove(follower);
				_leader._crowdManager.AllFollower.Remove(follower);
				Destroy(follower.gameObject);

				_leader._crowdManager.CreateNewActiveFollower(_type, pos, _leader);
				_loyalty -= 5;
				return;
			}
		}

	}

	public void MoshPit(List<Vector3> path){
		_followTarget.enabled = false;
		_followMoshPit.enabled = true;
		_leader = null;
		_followMoshPit.SetPath(path);
	}

	public void SetPossibleTargets(List<Target> tierOne, List<Target> tierTwo, List<Target> tierThree){
		_possibleTargetsTierOne = tierOne;
		_possibleTargetsTierTwo = tierTwo;
		_possibleTargetsTierThree = tierThree;
	}

	public void Add(Leader leader){
		StopCoroutine(LowerLoyalty(null));
		StartCoroutine(LowerLoyalty(new WaitForSeconds(Random.Range(5, 10))));
		_followTarget.enabled = true;
		_followMoshPit.enabled = false;
		leader.GetComponent<Leader>().AddFollower(this);
		_isFollowingPlayer = true;
	}

	IEnumerator Remove(){
		StopCoroutine(LowerLoyalty(null));
		yield return new WaitForFixedUpdate();
		_followTarget.enabled = false;
		_isFollowingPlayer = false;
		_leader.RemoveDemandCount(_currentTarget._type);
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

	bool CheckTargetInDemands(Target.Type type){

		switch(_demandLevel){
			case 0: 
				foreach(Target target in _possibleTargetsTierOne){
					if(target._type == type)
						return true;
				}
				break;
			case 1: 
				foreach(Target target in _possibleTargetsTierTwo){
					if(target._type == type)
						return true;
				}
				break;
			case 2: 
				foreach(Target target in _possibleTargetsTierThree){
					if(target._type == type)
						return true;
				}
				break;
		}

		return false;
	}

	// Update is called once per frame
	void Update () {
	}
}
