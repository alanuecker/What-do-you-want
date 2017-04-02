using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour {
	public string _menuScene;

	public void OpenMenu(){
		SceneManager.LoadScene(_menuScene);
	}
}
