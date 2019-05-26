using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlManager : MonoBehaviour {

	int[] controls;

	public Dropdown[] drops;

	// Use this for initialization
	void Start () {
		controls = new int[2];
		controls[0] = -1;
		controls[1] = -1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetControl(Dropdown d){
		if(d == drops[0]){
			controls[0] = d.value;
		}else if(d == drops[1]){
			controls[1] = d.value;
		}
	}
}
