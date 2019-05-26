using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {

	private int maxLife=3;
	private int currentLife=1;

	// Use this for initialization
	void Start () {

	}

	public void GainLife(int life=1){
		currentLife += life;
		if(currentLife > maxLife){
			currentLife = maxLife;
		}
		UiManager.Instance.UpdateLife(currentLife);
	}

	public void TakeDamage(int damage=1){
		currentLife -= damage;
		UiManager.Instance.UpdateLife(currentLife);
		if(currentLife <= 0){
			GameManager.Instance.GameOver();
		}
	}

}
