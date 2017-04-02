using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FollowerPercentages : MonoBehaviour {
	public RectTransform _assiTransform;
	public RectTransform _hippieTransform;
	public RectTransform _nerdTransform;
	public RectTransform _gothTransform;
	public Text _totalFollowers;
	public Text _activeFollowers;
	
	float targetPosAssi = -800;
	float targetPosHippie = -400;
	float targetPosNerd = 0;
	float targetPosGoth = 400;

	public float _speed = 800f;
	
	public void SetPercentages(float assi, float hippie, float nerd, float goth){
		if(assi == 0 && hippie == 0 && nerd == 0 && goth == 0)
			assi = hippie = nerd = goth = .25f;
		targetPosAssi = -800;
		targetPosHippie = targetPosAssi + assi * 1600f;
		targetPosNerd = targetPosHippie + hippie * 1600f;
		targetPosGoth = targetPosNerd + nerd * 1600f;
	}

	IEnumerator MoveToPercentages(float assi, float hippie, float nerd, float goth){
		

		float currentPosAssi = _assiTransform.localPosition.x;
		float currentPosHippie = _hippieTransform.localPosition.x;
		float currentPosNerd = _nerdTransform.localPosition.x;
		float currentPosGoth = _gothTransform.localPosition.x;
		
		WaitForEndOfFrame wait = new WaitForEndOfFrame();

		while(currentPosAssi != targetPosAssi || currentPosHippie != targetPosHippie ||
				currentPosNerd != targetPosNerd || currentPosGoth != targetPosGoth){
					yield return wait;
					currentPosAssi = Mathf.MoveTowards(currentPosAssi, targetPosAssi, _speed * Time.deltaTime);
					currentPosHippie = Mathf.MoveTowards(currentPosHippie, targetPosHippie, _speed * Time.deltaTime);
					currentPosNerd = Mathf.MoveTowards(currentPosNerd, targetPosNerd, _speed * Time.deltaTime);
					currentPosGoth = Mathf.MoveTowards(currentPosGoth, targetPosGoth, _speed * Time.deltaTime);
					
					Vector3 pos = _assiTransform.localPosition;
					pos.x = currentPosAssi;
					_assiTransform.localPosition = pos;

					pos = _hippieTransform.localPosition;
					pos.x = currentPosHippie;
					_hippieTransform.localPosition = pos;

					pos = _nerdTransform.localPosition;
					pos.x = currentPosNerd;
					_nerdTransform.localPosition = pos;

					pos = _gothTransform.localPosition;
					pos.x = currentPosGoth;
					_gothTransform.localPosition = pos;
				}
		
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float currentPosAssi = _assiTransform.localPosition.x;
		float currentPosHippie = _hippieTransform.localPosition.x;
		float currentPosNerd = _nerdTransform.localPosition.x;
		float currentPosGoth = _gothTransform.localPosition.x;

		currentPosAssi = Mathf.MoveTowards(currentPosAssi, targetPosAssi, _speed * Time.deltaTime);
		currentPosHippie = Mathf.MoveTowards(currentPosHippie, targetPosHippie, _speed * Time.deltaTime);
		currentPosNerd = Mathf.MoveTowards(currentPosNerd, targetPosNerd, _speed * Time.deltaTime);
		currentPosGoth = Mathf.MoveTowards(currentPosGoth, targetPosGoth, _speed * Time.deltaTime);
					
		Vector3 pos = _assiTransform.localPosition;
		pos.x = currentPosAssi;
		_assiTransform.localPosition = pos;

		pos = _hippieTransform.localPosition;
		pos.x = currentPosHippie;
		_hippieTransform.localPosition = pos;

		pos = _nerdTransform.localPosition;
		pos.x = currentPosNerd;
		_nerdTransform.localPosition = pos;

		pos = _gothTransform.localPosition;
		pos.x = currentPosGoth;
		_gothTransform.localPosition = pos;
	}
}
