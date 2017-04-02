using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Leader : MonoBehaviour {
	public List<Target> _targets;
	public Target[] _possibleTargets;
	public CrowdManager _crowdManager;
	public FollowerPercentages _followerPercentages;
	public AudioClip[] _whatClips;
	public Animator _endScreenAnimator;
	public int _amountToWin = 100;
	public Sprite[] _newsPaper;
	public Image _newspaperImage;

	private AudioSource _audioSource;
	private Target _lastTarget;


	IEnumerator UpdateTarget(){
		WaitForSeconds wait = new WaitForSeconds(1);
		Vector3 oldPos = transform.position; 
		while(true){
			yield return wait;
			_targets = new List<Target>();
			foreach(Target target in _crowdManager._targets){
				Vector3 ownPosOffset = transform.position - oldPos;
				ownPosOffset.y = 0;
				Vector3 targetPosOffset = target.transform.position - oldPos;
				targetPosOffset.y = 0;
				float angle = Vector3.Angle(ownPosOffset, targetPosOffset);
				if(angle < 35f){
					_targets.Add(target);
				}
				
			}
			oldPos = transform.position;
			_targets.Remove(_lastTarget);
		}
	}
	public void ReachTarget(Target target){
		if(target == _lastTarget)
			return;
		
		_lastTarget = target;
		foreach(Follower follower in _crowdManager.ActiveFollower)
			follower.ReachTarget(target, this);
		if(_whatClips.Length > 0){
		_audioSource.clip = _whatClips[Random.Range(0, _whatClips.Length)];
		_audioSource.PlayScheduled(2.5f);
		}
	}

	public void AddFollower(Follower follower){
		_crowdManager.AddActiveFollower(follower);

		float followerCount = _crowdManager.ActiveFollower.Count;
		_followerPercentages.SetPercentages((float)_crowdManager.AssiCount / followerCount, (float)_crowdManager.HippieCount / followerCount,
				(float)_crowdManager.NerdCount / followerCount, (float)_crowdManager.GothCount / followerCount );
		_followerPercentages._activeFollowers.text = "" + (int)followerCount;
		_followerPercentages._totalFollowers.text = "" + (int)_amountToWin;

		if(_crowdManager.ActiveFollower.Count / _amountToWin >= 1f){
			Victory();
		}
	}

	void Victory(){
		Time.timeScale = 0;
		if(_crowdManager.AssiCount > _crowdManager.NerdCount && _crowdManager.AssiCount > _crowdManager.HippieCount && _crowdManager.AssiCount > _crowdManager.GothCount)
			_newspaperImage.sprite = _newsPaper[0];
		else if(_crowdManager.NerdCount > _crowdManager.AssiCount && _crowdManager.NerdCount > _crowdManager.GothCount && _crowdManager.NerdCount > _crowdManager.HippieCount)
			_newspaperImage.sprite = _newsPaper[1];
		else if(_crowdManager.GothCount > _crowdManager.AssiCount && _crowdManager.GothCount > _crowdManager.NerdCount  && _crowdManager.GothCount > _crowdManager.HippieCount )
			_newspaperImage.sprite = _newsPaper[2];
		else
			_newspaperImage.sprite = _newsPaper[3];



		_endScreenAnimator.Play("Victory");
	}

	public void RemoveFollower(Follower follower){
		_crowdManager.RemoveActiveFollower(follower);

		float followerCount = _crowdManager.ActiveFollower.Count;
		_followerPercentages.SetPercentages((float)_crowdManager.AssiCount / followerCount, (float)_crowdManager.HippieCount / followerCount,
				(float)_crowdManager.NerdCount / followerCount, (float)_crowdManager.GothCount / followerCount );
		_followerPercentages._activeFollowers.text = "" + (int)followerCount;
		_followerPercentages._totalFollowers.text = "" + (int)_crowdManager.AllFollower.Count;
	}

	public void AddDemandCount(Target.Type type){
		_crowdManager.AddDemandCount(type);
	}

	public void RemoveDemandCount(Target.Type type){
		_crowdManager.RemoveDemandCount(type);
	}

	// Use this for initialization
	void Start () {		
		_targets = new List<Target>();
		_crowdManager = GameObject.FindGameObjectWithTag("CrowdManager").GetComponent<CrowdManager>();
		StartCoroutine(UpdateTarget());
		_audioSource = GetComponent<AudioSource>();
		_followerPercentages._totalFollowers.text = "" + (int)_amountToWin;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
