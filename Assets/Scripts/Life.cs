using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {

	private int maxLife=5;
	private int currentLife=1;

	// Use this for initialization
	void Start () {

	}

	public void GainLife(int life=1){
		currentLife += life;
	}

	public void TakeDamage(int damage=1){
		currentLife -= damage;
		if(currentLife <= 0){
			GameManager.instance.GameOver();
		}
	}

}
