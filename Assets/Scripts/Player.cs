using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int control;

	Rigidbody2D rigid;

	public float speed;

	bool dashing = false;

	int dashCount = 0;
	public float dashSpeed = 8.0f;
	public float fallFactor = 2.5f;
	public float lowJumpFactor = 2.0f;
	public float jumpSpeed = 5.0f;
	bool grounded;
	Transform groundCheck;
	public LayerMask groundLayer;
	Animator anim;

	// Use this for initialization
	void Start () {
		rigid = this.GetComponent<Rigidbody2D>();
		anim = this.GetComponent<Animator>();
		groundCheck = transform.GetChild(0);
	}

	// Update is called once per frame
	void Update () {
		grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
		if (grounded && !dashing) dashCount = 0;
		if(!dashing){
			Run();
			Jump();
		}
		Dash();
	}

	void Run(){
		//horizontal
		bool run;
		if (Input.GetAxis(axis("hor")) > 0.0f){
			rigid.velocity = new Vector2(speed * Time.deltaTime, rigid.velocity.y);
			transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
			run = true;
		}else if (Input.GetAxis(axis("hor")) < 0.0f){
			rigid.velocity = new Vector2(- speed * Time.deltaTime, rigid.velocity.y);
			transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			run = true;
		}else{
			rigid.velocity = new Vector2(0.0f, rigid.velocity.y);
			run = false;
		}
		anim.SetBool("run", run);
	}

	void Jump(){
		//vertical
		if(Input.GetButtonDown(axis("jump")) && grounded)
			rigid.velocity = new Vector2(rigid.velocity.x, jumpSpeed);

		if(rigid.velocity.y < 0.0f)
			rigid.velocity += Vector2.up * Physics2D.gravity.y * (fallFactor - 1.0f) * Time.deltaTime;
		else if (rigid.velocity.y > 0.0f && !Input.GetButton(axis("jump")))
			rigid.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpFactor - 1.0f) * Time.deltaTime;
		
		anim.SetBool("jump", !grounded);
	}

	void Dash(){
		if(Input.GetButtonDown(axis("dash")) && !dashing && dashCount < 1){
			float x = Input.GetAxis(axis("hor"));
			float y = Input.GetAxis(axis("ver"));
			if(grounded)
				y = Mathf.Clamp(y, 0.0f, 1.0f);

			Vector2 dir = new Vector2(x,y).normalized;

			if (dir.magnitude == 0.0f){
				if (rigid.velocity.x > 0.0f)
					dir = Vector2.right;
				else if (rigid.velocity.x > 0.0f)
					dir = Vector2.left;
				else if (!grounded)
					dir = Vector2.up;
				else
					dir = Vector2.left * transform.localScale.x;
			}

			rigid.velocity = dir * dashSpeed;
			dashing = true;
			dashCount++;
			anim.SetBool("dash", true);
			Invoke("stopDash", 0.1f);
			Invoke("resetDash", 0.5f);
		}
	}

	void stopDash(){
		rigid.velocity = Vector2.zero;
		anim.SetBool("dash", false);
	}

	void resetDash(){
		dashing = false;
	}

	string axis(string button){
		if (control == 0)
			button += "_t";
		else
			button += "_j" + control.ToString();

		return button;
	}
}
