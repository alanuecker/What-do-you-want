using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrowdManager : MonoBehaviour {
	public int _spawnRange = 80;
	public Transform _spawnCenter;
	public Target[] _targets;

	public int[] targetTierOne;
	public int[] targetTierTwo;
	public int[] targetTierThree;

	[Header("Order: Assis - Nerds - Goths - Hippies")]
	public GameObject[] _followers;
	public int _totalAmount;

	[Space(5)]
	public int[] assisTierOne;
	public int[] assisTierTwo;
	public int[] assisTierThree;

	[Space(5)]
	public int[] nerdsTierOne;
	public int[] nerdsTierTwo;
	public int[] nerdsTierThree;

	[Space(5)]
	public int[] gothsTierOne;
	public int[] gothsTierTwo;
	public int[] gothsTierThree;

	[Space(5)]
	public int[] hippiesTierOne;
	public int[] hippiesTierTwo;
	public int[] hippiesTierThree;

	private Transform _player;
	private List<Follower> _allFollower = new List<Follower>();
	private List<Follower> _activeFollower = new List<Follower>();

	private List<int[]> _tierOne = new List<int[]>();
	private List<int[]> _tierTwo = new List<int[]>();
	private List<int[]> _tierThree = new List<int[]>();

	private List<GameObject> _targetTierOne = new List<GameObject>();
	private List<GameObject> _targetTierTwo = new List<GameObject>();
	private List<GameObject> _targetTierThree = new List<GameObject>();

	private List<Follower> _activeAssis = new List<Follower>();
	private List<Follower> _activeNerds = new List<Follower>();
	private List<Follower> _activeGoths = new List<Follower>();
	private List<Follower> _activeHippies = new List<Follower>();

	private int _demandAssis;
	private int _demandNerds;
	private int _demandGoths;
	private int _demandHippies;

	private Dictionary<Target.Type, int> _demandCount = new Dictionary<Target.Type, int>();

	// Use this for initialization
	void Awake (){
		_tierOne.Add(assisTierOne);
		_tierOne.Add(nerdsTierOne);
		_tierOne.Add(gothsTierOne);
		_tierOne.Add(hippiesTierOne);

		_tierTwo.Add(assisTierTwo);
		_tierTwo.Add(nerdsTierTwo);
		_tierTwo.Add(gothsTierTwo);
		_tierTwo.Add(hippiesTierTwo);

		_tierThree.Add(assisTierThree);
		_tierThree.Add(nerdsTierThree);
		_tierThree.Add(gothsTierThree);
		_tierThree.Add(hippiesTierThree);

		foreach(int i in targetTierOne)
			_targetTierOne.Add(_targets[i].gameObject);
		foreach(int i in targetTierTwo)
			_targetTierTwo.Add(_targets[i].gameObject);
		foreach(int i in targetTierThree)
			_targetTierThree.Add(_targets[i].gameObject);

		DeactivateAllDemandDirectionIndicator();
	}

	public void AddActiveFollower(Follower follower){
		_activeFollower.Add(follower);
		SetFollowerDemand(follower);
		AddType(follower);
	}

	public void RemoveActiveFollower(Follower follower){
		_activeFollower.Remove(follower);
		RemoveType(follower);
	}

	void AddType(Follower follower){
		switch(follower._type){
			case Follower.Type.assi:
				_activeAssis.Add(follower);
				CalculateDemandTier(_activeAssis, ref _demandAssis);
				break;
			case Follower.Type.nerd:
				_activeNerds.Add(follower);
				CalculateDemandTier(_activeNerds, ref _demandNerds);
				break;
			case Follower.Type.goth:
				_activeGoths.Add(follower);
				CalculateDemandTier(_activeGoths, ref _demandGoths);
				break;
			case Follower.Type.hippie:
				_activeHippies.Add(follower);
				CalculateDemandTier(_activeHippies, ref _demandHippies);
				break;
		}
	}

	void RemoveType(Follower follower){
		switch(follower._type){
			case Follower.Type.assi:
				_activeAssis.Remove(follower);
				CalculateDemandTier(_activeAssis, ref _demandAssis);
				break;
			case Follower.Type.nerd:
				_activeNerds.Remove(follower);
				CalculateDemandTier(_activeNerds, ref _demandNerds);
				break;
			case Follower.Type.goth:
				_activeGoths.Remove(follower);
				CalculateDemandTier(_activeGoths, ref _demandGoths);
				break;
			case Follower.Type.hippie:
				_activeHippies.Remove(follower);
				CalculateDemandTier(_activeHippies, ref _demandHippies);
				break;
		}
	}

	void CalculateDemandTier(List<Follower> active, ref int totalNumber){
		if(active.Count >= (_totalAmount/4)*0.75f){
			if(totalNumber != 2){
				totalNumber = 2;
				SwitchDemandTarget(active[0]._type);
				foreach(Follower act in active)
					act.SetDemand(2);
			}
		}
		else if(active.Count >= (_totalAmount/4)*0.5f){
			if(totalNumber != 1){
				if(totalNumber == 2)
					SwitchDemandTarget(active[0]._type);
				totalNumber = 1;
				foreach(Follower act in active)
					act.SetDemand(1);
			}
		}else{
			if(totalNumber != 0){
				if(totalNumber == 2)
					SwitchDemandTarget(active[0]._type);
				totalNumber = 0;
				foreach(Follower act in active)
					act.SetDemand(0);
			}
		}
	}

	void ActivateDemandDirectionIndicator(Target.Type type){
		foreach(Target tar in _targets){
			if(tar._type == type)
				tar.transform.Find("DirectionIndicator").gameObject.SetActive(true);
		}
	}

	void DeactivateDemandDirectionIndicator(Target.Type type){
		foreach(Target tar in _targets){
			if(tar._type == type)
				tar.transform.Find("DirectionIndicator").gameObject.SetActive(false);
		}
	}

		void DeactivateAllDemandDirectionIndicator(){
		foreach(Target tar in _targets){
			tar.transform.Find("DirectionIndicator").gameObject.SetActive(false);
		}
	}

	void SwitchDemandTarget(Follower.Type type){
		foreach(GameObject target in _targetTierTwo){
			foreach(Follower.Type objType in target.GetComponent<Target>()._followerLoveTypes){
				if(objType == type)
					target.SetActive(!target.activeSelf);
			}
		}
		foreach(GameObject target in _targetTierThree){
			foreach(Follower.Type objType in target.GetComponent<Target>()._followerLoveTypes){
				if(objType == type)
					target.SetActive(!target.activeSelf);
			}
		}
	}

	void SetFollowerDemand(Follower follower){
		switch(follower._type){
			case Follower.Type.assi:
				follower.SetDemand(_demandAssis);
				break;
			case Follower.Type.nerd:
				follower.SetDemand(_demandNerds);
				break;
			case Follower.Type.goth:
				follower.SetDemand(_demandGoths);
				break;
			case Follower.Type.hippie:
				follower.SetDemand(_demandHippies);
				break;
		}
	}

	public void AddDemandCount(Target.Type type){
		if(_demandCount.ContainsKey(type))
			_demandCount[type]++;
		else
			_demandCount.Add(type, 1);

		ActivateDemandDirectionIndicator(type);
	}

	public void RemoveDemandCount(Target.Type type){
		if(_demandCount.ContainsKey(type))
			if(_demandCount[type]-- <= 0){
				DeactivateDemandDirectionIndicator(type);
				_demandCount.Remove(type);
			}
			else
				_demandCount[type]--;
	}
	void Start () {
		_player = GameObject.FindGameObjectWithTag("Player").transform;

		for(int i = 0; i < _followers.Length; i++){
			List<Target> tierOne = new List<Target>();
			List<Target> tierTwo = new List<Target>();
			List<Target> tierThree = new List<Target>();

			foreach(int x in _tierOne[i]){
				tierOne.Add(_targets[x]);
			}
			foreach(int x in _tierTwo[i]){
				tierTwo.Add(_targets[x]);
			}
			foreach(int x in _tierThree[i]){
				tierThree.Add(_targets[x]);
			}

			for(int j = 0; j < _totalAmount/4; j++){
				NavMeshHit hit;
				Vector3 randomPosition = new Vector3(Random.Range(_spawnCenter.position.x -_spawnRange, _spawnCenter.position.x + _spawnRange + 1), Random.Range(_spawnCenter.position.y - 4, _spawnCenter.position.y + 2), Random.Range(_spawnCenter.position.z -_spawnRange, _spawnCenter.position.z + _spawnRange + 1));
				if(NavMesh.SamplePosition(randomPosition, out hit, 10.0f, NavMesh.AllAreas)){
					randomPosition = hit.position;
				}else{
					j--;
					continue;
				}

				GameObject follower = (GameObject)Instantiate(_followers[i], randomPosition, Quaternion.identity);
				follower.GetComponent<Follower>().SetPossibleTargets(tierOne, tierTwo, tierThree);
				follower.GetComponent<FollowTarget>().SetTarget(_player);

				_allFollower.Add(follower.GetComponent<Follower>());
			}
		}

		_player.GetComponent<Leader>().SetAllFollowers(_allFollower);
		_player.GetComponent<Leader>().SetActiveFollowers(_activeFollower);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
