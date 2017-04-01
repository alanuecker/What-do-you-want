using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrowdManager : MonoBehaviour {

	public int numberOfAssis = 10;
	public int numberOfGoths = 10;
	public int numberOfHippies = 10;
	public int numberOfNerds = 10;

	public Target[] _targets;
	[Header("Order: Assis - Nerds - Goths - Hippies")]
	public GameObject[] _followers;

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

	private Dictionary<string,int[]> _tierOne;
	private Dictionary<string,int[]> _tierTwo;
	private Dictionary<string,int[]> _tierThree;

	// Use this for initialization
	void Awake(){
		_tierOne.Add("assis", assisTierOne);
		_tierOne.Add("nerds", nerdsTierOne);
		_tierOne.Add("goths", gothsTierOne);
		_tierOne.Add("hippies", hippiesTierOne);

		_tierTwo.Add("assis", assisTierTwo);
		_tierTwo.Add("nerds", nerdsTierTwo);
		_tierTwo.Add("goths", gothsTierTwo);
		_tierTwo.Add("hippies", hippiesTierTwo);

		_tierThree.Add("assis", assisTierThree);
		_tierThree.Add("nerds", nerdsTierThree);
		_tierThree.Add("goths", gothsTierThree);
		_tierThree.Add("hippies", hippiesTierThree);
	}

	void Start () {
		for(int i = 0; i < _followers.Length; i++){
			
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
