using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlManager : MonoBehaviour {

	public Dropdown[] drops;

	void Start(){
		drops[0].GetComponent<Dropdown>().value = GameManager.Instance.p1Control;
		drops[1].GetComponent<Dropdown>().value = GameManager.Instance.p2Control+1;
	}

	public void SetControl(Dropdown d){
		if(d == drops[0]){
			GameManager.Instance.p1Control = d.value;
		}else if(d == drops[1]){
			GameManager.Instance.p2Control = d.value-1;
		}
	}
}
