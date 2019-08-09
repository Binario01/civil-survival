using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBuff : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
    {
		if(col.gameObject.CompareTag("Player")){
			col.gameObject.GetComponent<Life>().GainLife(1);
			Destroy(gameObject);
		}
    }

	public void SetParent(HeartSpawner hs){
		hs.spawned = false;
	}

}
