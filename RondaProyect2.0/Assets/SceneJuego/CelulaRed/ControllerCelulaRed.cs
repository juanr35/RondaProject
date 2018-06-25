using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCelulaRed : MonoBehaviour {

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
		if( touch) {
			animator.SetTrigger("Activar");
			touch = false;
		}

		if( delete)
			Destroy( gameObject);
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