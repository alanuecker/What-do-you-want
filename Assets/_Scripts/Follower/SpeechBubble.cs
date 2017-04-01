using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour {
	public SpriteRenderer _targetIcon;
	private Animator _animator;

	public Sprite TargetIcon{
		set {
			_targetIcon.sprite = value;
			_animator.Play("Appear");
		}
	}

	// Use this for initialization
	void Awake () {
		_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(Camera.main.transform);
	}
}
