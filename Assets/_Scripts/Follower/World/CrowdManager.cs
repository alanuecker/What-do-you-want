using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrowdManager : MonoBehaviour {
	public Target[] _targets;
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
				GameObject follower = (GameObject)Instantiate(_followers[i],new Vector3(Random.Range(-80, 81), 1, Random.Range(-80, 81)), Quaternion.identity);
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
