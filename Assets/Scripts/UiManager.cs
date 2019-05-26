using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour {

	public static UiManager Instance;

	public GameObject gameOverPanel;
	public GameObject heart;

	public Text timer = null;
	public float timeCounter = 0;

	GameObject p1Life,p2Life;


	//Awake is always called before any Start functions
	void Awake()
	{
		//Check if instance already exists
		if (Instance == null){
			//if not, set instance to this
			Instance = this;
		}

		//If instance already exists and it's not this:
		else if (Instance != this){
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);
		}

		//Call the InitGame function to initialize the first level
		InitGame();
	}

	// Use this for initialization
	public void InitGame () {
		p1Life = GameObject.Find("P1 Life");
		p2Life = GameObject.Find("P2 Life");

		timeCounter = 0;
		timer = GameObject.Find("Timer").GetComponent<Text>();
	}

	// Update is called once per frame
	public void UpdateLife (int life, bool isP1) {
		Transform playerT;
		if(isP1){
			playerT = p1Life.transform;
		}
		else{
			playerT = p2Life.transform;
		}
		int count = playerT.childCount;

		if(count < life){
			for(int i=0;i<life-count;i++){
				GameObject.Instantiate(heart,Vector3.zero,Quaternion.identity).transform.SetParent(transform);
			}
		}
		else if(count > life){
			int lifesLost = count-life;
			lifesLost = life >= 0 ? lifesLost : 0;
			for(int i=0;i<lifesLost;i++){
				Destroy(playerT.GetChild(0).gameObject);
			}
		}
	}

	public void InitLife(int life, bool isP1){
		Transform player;
		if(isP1){
			player = p1Life.transform;
		}
		else{
			player = p2Life.transform;
		}

		int count = player.childCount;
		if(life > count){
			life -= count;
			for(int i=0;i<life;i++){
				GameObject hp = GameObject.Instantiate(heart,Vector3.zero,Quaternion.identity);
				hp.transform.SetParent(player, false);
				hp.transform.localScale = new Vector3(1,1,1);
			}
		}
		else if(life < count){
			int lifesLost = count - life;
			for(int i=0;i<lifesLost;i++){
				Destroy(player.GetChild(0).gameObject);
			}
		}

	}

	public void GameOver(bool isP1,bool soloGame, float record=0){
		gameOverPanel.SetActive(true);
		string gameOvertext;
		if(soloGame){
			gameOvertext = "You survived " + timeCounter.ToString("0.00") + " seconds!\nRecord " + record.ToString("0.00");
		}
		else{
			gameOvertext = "Player " + (isP1 ? "2" : "1") + " win!";
		}
		gameOverPanel.transform.Find("Win Text").GetComponent<Text>().text = gameOvertext;
	}

	void Update(){
		timeCounter += Time.deltaTime;
		timer.text = timeCounter.ToString("0.00");
	}

	public void GoToMenu(){
		 SceneManager.LoadScene("Menu");
	}

	public void RestartGame(){
		 SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void QuitGame(){
		Application.Quit();
	}

}
