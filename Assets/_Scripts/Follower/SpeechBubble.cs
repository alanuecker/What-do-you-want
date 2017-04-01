using UnityEngine;
using System.Collections;

public class SpeechBubble : MonoBehaviour {
	public SpriteRenderer _icon;

	public Sprite Icon{
		set{
			_icon.sprite = value;
		}
	}
	
	public void Die(){
		Destroy(gameObject);
	}
}
