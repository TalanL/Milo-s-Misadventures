﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject player;
	public GameObject particleBlood;
	public GameObject bloodStainEffect;
	private int health = 1;
	public delegate void Killed();
	public event Killed killed;

	// Use this for initialization
	void Start () {
		this.player = (GameObject.Find ("CubeShuro") as GameObject);
	}
	
	// Update is called once per frame
	void Update () {
		moveTowardsPlayer ();
		if (health <= 0) {
			killed();
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter(Collider theCollision){
		if (theCollision.gameObject.name=="Weapon") {
			Hit (1);
		}
	}


	void moveTowardsPlayer(){
		Vector3 moveDirection = new Vector3(this.transform.position.x - player.transform.position.x, this.transform.position.y - player.transform.position.y, this.transform.position.z - player.transform.position.z);
		this.transform.position = this.transform.position - (moveDirection.normalized * (Time.deltaTime*2));
	}

	void Hit(int damage){
		Vector3 vectorToPlayer = new Vector3(this.transform.position.x - player.transform.position.x, this.transform.position.y - player.transform.position.y, this.transform.position.z - player.transform.position.z);
		Quaternion bloodRotation = new Quaternion();
		bloodRotation.SetFromToRotation (new Vector3(0,0,1), vectorToPlayer); 
		Vector3 bloodPosition = new Vector3 (this.transform.position.x, this.transform.position.y+0.5f, this.transform.position.z);
		var bloodEffect = (Instantiate (particleBlood, bloodPosition, bloodRotation) as GameObject);
		Destroy (bloodEffect, 1.0f);
		var bloodStain = (Instantiate (bloodStainEffect, this.transform.position, this.transform.rotation) as GameObject);
		Destroy (bloodStain, 1.0f);
		health -= damage;
		KillCounterScript.Increment ();
	}
}
