using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

	public static UiManager Instance;

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

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

		//Call the InitGame function to initialize the first level
		InitGame();
	}

	// Use this for initialization
	void InitGame () {
		p1Life = GameObject.Find("P1 Life");
		p2Life = GameObject.Find("P2 Life");

		timer = GameObject.Find("Timer").GetComponent<Text>();
	}

	// Update is called once per frame
	public void UpdateLife (int life) {
		int count = transform.childCount;
		if(count < life){
			for(int i=0;i<life-count;i++){
				GameObject.Instantiate(heart,Vector3.zero,Quaternion.identity).transform.parent = transform;
			}
		}
		else if(count > life){
			for(int i=0;i<count-life;i++){
				Destroy(transform.GetChild(0).gameObject);
			}
		}
	}


	void Update(){
		timeCounter += Time.deltaTime;
		timer.text = timeCounter.ToString("0.00");
	}

}
