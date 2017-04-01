using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrowdManager : MonoBehaviour {
	public int _spawnRange = 40;
	public Target[] _targets;

	public int[] targetTierOne;
	public int[] targetTierTwo;
	public int[] targetTierThree;

	[Header("Order: Assis - Nerds - Goths - Hippies")]
	public GameObject[] _followers;
	public int[] _numberOf;

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

	private int _countAssis;
	private int _countNerds;
	private int _countGoths;
	private int _countHippies;

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
	}

	//call if you want to switch from tier two to three or back
	void SwitchTargetObjects(){
		foreach(GameObject target in _targetTierTwo)
			target.SetActive(!target.activeSelf);
		foreach(GameObject target in _targetTierThree)
			target.SetActive(!target.activeSelf);
	}

	public void AddActiveFollower(Follower follower){
		_activeFollower.Add(follower);
		AddType(follower._type);
	}

	public void RemoveActiveFollower(Follower follower){
		_activeFollower.Remove(follower);
		RemoveType(follower._type);
	}

	void AddType(Follower.Type type){
		switch(type){
			case Follower.Type.assi:
				_countAssis++;
				break;
			case Follower.Type.nerd:
				_countNerds++;
				break;
			case Follower.Type.goth:
				_countGoths++;
				break;
			case Follower.Type.hippie:
				_countHippies++;
				break;
		}
	}

	void RemoveType(Follower.Type type){
				switch(type){
			case Follower.Type.assi:
				_countAssis--;
				break;
			case Follower.Type.nerd:
				_countNerds--;
				break;
			case Follower.Type.goth:
				_countGoths--;
				break;
			case Follower.Type.hippie:
				_countHippies--;
				break;
		}
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

			for(int j = 0; j < _numberOf[i]; j++){
				GameObject follower = (GameObject)Instantiate(_followers[i],new Vector3(Random.Range(-_spawnRange, _spawnRange + 1), 1, Random.Range(-_spawnRange, _spawnRange + 1)), Quaternion.identity);
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
