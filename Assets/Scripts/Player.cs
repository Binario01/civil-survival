using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int control;

	Rigidbody2D rigid;

	public float speed;

	public float fallFactor = 2.5f;
	public float lowJumpFactor = 2.0f;
	public float jumpSpeed = 5.0f;
	bool grounded;
	Transform groundCheck;
	public LayerMask groundLayer;

	// Use this for initialization
	void Start () {
		rigid = this.GetComponent<Rigidbody2D>();
		groundCheck = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
		//horizontal
		if (Input.GetAxis(axis("hor")) > 0.0f)
			rigid.velocity = new Vector2(speed * Time.deltaTime, rigid.velocity.y);
		else if (Input.GetAxis(axis("hor")) < 0.0f)
			rigid.velocity = new Vector2(- speed * Time.deltaTime, rigid.velocity.y);
		else
			rigid.velocity = new Vector2(0.0f, rigid.velocity.y);
		
		Debug.Log(Input.GetAxis(axis("hor")));
		
		//vertical
		grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

		if(Input.GetButtonDown(axis("jump")) && grounded)
			rigid.velocity = new Vector2(rigid.velocity.x, jumpSpeed);
		
		if(rigid.velocity.y < 0.0f)
			rigid.velocity += Vector2.up * Physics2D.gravity.y * (fallFactor - 1.0f) * Time.deltaTime;
		else if (rigid.velocity.y > 0.0f && !Input.GetButton(axis("jump")))
			rigid.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpFactor - 1.0f) * Time.deltaTime;
	}

	string axis(string button){
		if (control == 0)
			button += "_t";
		else
			button += "_j" + control.ToString();

		return button;
	}
}
