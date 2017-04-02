using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubbleSpawner : MonoBehaviour {
	public GameObject _prefab;

	public Sprite TargetIcon{
		set {
			GameObject instance = (Instantiate(_prefab, transform.position + Vector3.up, Quaternion.Euler(45,45,0)) as GameObject);
			instance.GetComponent<SpeechBubble>().Icon = value;
			instance.transform.SetParent(transform);
		}
	}
}
