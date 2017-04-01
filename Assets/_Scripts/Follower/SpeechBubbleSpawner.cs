using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleSpawner : MonoBehaviour {
	public GameObject _prefab;

	public Sprite TargetIcon{
		set {
			(Instantiate(_prefab, transform.position, Quaternion.Euler(45,45,0)) as GameObject).GetComponent<SpeechBubble>().Icon = value;
		}
	}
}
