using UnityEngine;
using System.Collections;

public class SpeechBubble : MonoBehaviour {
	public SpriteRenderer _icon;
	private Quaternion rot;
	public Sprite Icon{
		set{
			_icon.sprite = value;
		}
	}

	void Start(){
		rot = transform.rotation;
	}
	
	void Update(){
		transform.rotation = rot;
	}

	public void Die(){
		Destroy(gameObject);
	}
}
