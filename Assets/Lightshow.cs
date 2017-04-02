using UnityEngine;
using System.Collections;

public class Lightshow : MonoBehaviour {

	float rotateSpeed = 1f;
	// Use this for initialization
	void Start () {

		StartCoroutine (UpdateLights ());
		StartCoroutine (UpdateLights2 ());
	}

	// Update is called once per frame
	void Update () {
	/*
		Vector3 euler = transform.eulerAngles;
		float targetX =Random.Range(-60f, -20f);
		float targetY = Random.Range(-30f, 30f);
		euler.x = Mathf.MoveTowardsAngle (euler.x, targetX, 50 * Time.deltaTime);
		euler.y = Mathf.MoveTowardsAngle (euler.y, targetY, 50 * Time.deltaTime);

		transform.eulerAngles = euler;*/
	}

	IEnumerator UpdateLights(){
		WaitForEndOfFrame wait = new WaitForEndOfFrame ();
		while (true) {
			float targetX = Random.Range(-60f, -20f);
			float eulerX = transform.eulerAngles.x;
			while (eulerX != targetX) {
				yield return wait;
				eulerX = Mathf.MoveTowardsAngle (eulerX, targetX, 50 * Time.deltaTime);
				Vector3 euler = transform.rotation.eulerAngles;
				euler.x = eulerX;
				transform.eulerAngles = euler;
			}
		}
	}

	IEnumerator UpdateLights2(){
		WaitForEndOfFrame wait = new WaitForEndOfFrame ();
		while (true) {
			float targetY = Random.Range(-30f, 30f);
			float eulerY = transform.eulerAngles.y;
			while (eulerY != targetY) {
				yield return wait;
				eulerY = Mathf.MoveTowardsAngle (eulerY, targetY, 50 * Time.deltaTime);
				Vector3 euler = transform.rotation.eulerAngles;
				euler.y = eulerY;
				transform.eulerAngles = euler;
			}
		}
	}

}
