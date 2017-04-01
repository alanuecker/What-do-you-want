using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class FireEventOnTrigger : MonoBehaviour {

    [SerializeField] public UnityEvent _onTriggerEnter;

	void OnTriggerEnter(){
		_onTriggerEnter.Invoke();
	}
}
