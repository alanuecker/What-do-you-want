using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	public string _gameScene;

	public void StartGame(){
		SceneManager.LoadScene(_gameScene);
	}

	public void Exit(){
		Application.Quit();
	}
	// Use this for initialization
	void Start () {
		Time.timeScale = 1;	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
