using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public void PlayGame(){
		SceneManager.LoadScene("Game");
	}

	public void QuitGame(){
		Application.Quit();
	}


}
