using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public static GameManager Instance = null;                //Static instance of GameManager which allows it to be accessed by any other script.
	public GameObject p1,p2;
	public int p1Control=0,p2Control=-1;
	public bool soloGame = false;
	public bool gameEnded = false;

	private DataSaver<HighScore> gameSaver;
	private HighScore scores;

	//Awake is always called before any Start functions
	void Awake()
	{
		//Check if instance already exists
		if (Instance == null){
			//if not, set instance to this
			Instance = this;
			gameSaver = new DataSaver<HighScore>("scores",true);
			scores = gameSaver.LoadData();
		}

		//If instance already exists and it's not this:
		else if (Instance != this){
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);
			return;
		}

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

		SceneManager.sceneLoaded += OnSceneLoaded;

		//Call the InitGame function to initialize the first level
		//InitGame();
	}

	void InitGame(){
		p1 = GameObject.Find("P1");
		p2 = GameObject.Find("P2");

		if(p2Control != -1){
			soloGame = false;
			p2.GetComponent<Player>().control = p2Control;
		}
		else{
			soloGame = true;
			Destroy(p2);
			UiManager.Instance.InitLife(0,false);
		}

		p1.GetComponent<Player>().control = p1Control;
		gameEnded = false;

	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Game"){
			InitGame();
		}
    }

	public void GameOver(bool isP1){
		gameEnded = true;

		if(soloGame && UiManager.Instance.timeCounter > scores.highScore){
			scores.highScore = UiManager.Instance.timeCounter;
			gameSaver.SaveData(scores);
		}

		UiManager.Instance.GameOver(isP1,soloGame,scores.highScore);
	}

}

[System.Serializable]
class HighScore{

	public HighScore(){
		highScore = 0f;
	}
	public float highScore;
}