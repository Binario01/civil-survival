using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour {

	Vector3 spawnPos;

	void Start(){
		spawnPos = new Vector3(transform.position.x,transform.position.y,0);
	}

	public void Spawn (GameObject rock) {
		GameObject.Instantiate(rock,spawnPos,Quaternion.identity);
	}
}
