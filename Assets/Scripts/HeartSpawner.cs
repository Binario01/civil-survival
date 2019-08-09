using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSpawner : MonoBehaviour {

	public HeartBuff heart;
	public bool spawned = false;

	void Start () {
		StartCoroutine(SpawnHeart());
	}

	IEnumerator SpawnHeart(){
		while(true){
			yield return new WaitForSeconds(3);
			if(!spawned && Random.Range(0,100f) > 60){
				Instantiate(heart,transform.position,Quaternion.identity).SetParent(this);
			}
		}
	}

}
