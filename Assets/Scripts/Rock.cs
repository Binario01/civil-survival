using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

	int damage=1;

	void Start(){
		Destroy(this,2);
	}

	void OnCollisionEnter2D(Collision2D col)
    {
		if(col.gameObject.CompareTag("Player")){
			col.gameObject.GetComponent<Life>().TakeDamage(damage);
			Destroy(this);
		}
    }


}
