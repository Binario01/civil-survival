using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

	public static GameManager instance = null;                //Static instance of GameManager which allows it to be accessed by any other script.
	public Text timer = null;
	public GameObject p1,p2;
	public bool soloGame = false;

	public float timeCounter = 0;

	//Awake is always called before any Start functions
	void Awake()
	{
		//Check if instance already exists
		if (instance == null){
			//if not, set instance to this
			instance = this;
		}

		//If instance already exists and it's not this:
		else if (instance != this){
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);
		}

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

		//Call the InitGame function to initialize the first level
		InitGame();
	}

	void InitGame(){
		timer = GameObject.Find("Timer").GetComponent<Text>();
		p1 = GameObject.Find("Timer");
		p2 = GameObject.Find("Timer");
		if(p2 == null){
			// TODO: SOLO GAME
			soloGame = true;
		}

	}

	public void GameOver(){
		Debug.Log("Fim de jogo");
	}

	void Update(){
		timeCounter += Time.deltaTime;
		timer.text = timeCounter.ToString("0.00");
	}

}