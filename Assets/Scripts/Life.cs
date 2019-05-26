using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {

	private int maxLife=3;
	private int currentLife=1;
	private bool isP1;

	// Use this for initialization
	void Start () {
		isP1 = gameObject.name == "P1";

		UiManager.Instance.InitLife(currentLife,isP1);
	}

	public void GainLife(int life=1){
		currentLife += life;
		if(currentLife > maxLife){
			currentLife = maxLife;
		}
		UiManager.Instance.UpdateLife(currentLife, isP1);
	}

	public void TakeDamage(int damage=1){
		if(GameManager.Instance.gameEnded)
			return;
		currentLife -= damage;
		UiManager.Instance.UpdateLife(currentLife, isP1);
		if(currentLife <= 0){
			GameManager.Instance.GameOver(isP1);
		}
	}

}
