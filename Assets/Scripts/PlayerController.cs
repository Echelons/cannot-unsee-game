﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 0f;
	public int health = 5;

	Rigidbody rigidbody;
	Camera viewCamera;
	Vector3 velocity;
    float lastTimeLostHealth;

    public void takeDamage(int damage)
    {
        if (Time.time - lastTimeLostHealth > 1.2f)
        {
            lastTimeLostHealth = Time.time;
            health = Math.Max(health - damage, 0);
        }
    }

	void Start () {
		rigidbody = this.GetComponent<Rigidbody> ();
		viewCamera = Camera.main;
	}

	// Update is called once per frame
	void Update () {
		Vector3 mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));
		transform.LookAt (mousePos + Vector3.up * transform.position.y);
		velocity = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized * moveSpeed;

        if (health <= 0)
        {
            GameManager.Instance.killPlayer();
        }
	}

	void FixedUpdate() {
		rigidbody.MovePosition (rigidbody.position + velocity * Time.fixedDeltaTime);
	}
}
