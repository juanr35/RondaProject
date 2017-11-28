﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCelulaDefault : MonoBehaviour {

	static bool delete;
	bool touch;
	Animator animator;

	// Use this for initialization
	void Start () 
	{
		touch = false;	
		animator = GetComponent<Animator>();

	}

	// Update is called once per frame
	void Update () 
	{
		if( touch) 
			animator.SetTrigger("Activar");

		if( delete)
			Destroy( gameObject);
	}

	void _DeleteCelula()
	{
		Destroy(gameObject);
	}

	public void _EnaTouch()
	{
		touch = true;
	}

	public void _Delete()
	{
		delete = true;
	}

	public void _NotDelete()
	{
		delete = false;
	}
}