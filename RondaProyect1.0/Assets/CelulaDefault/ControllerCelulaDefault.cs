using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCelulaDefault : MonoBehaviour {

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
	}

	void DeleteCelula()
	{
		Destroy(gameObject);
	}

	public void EnaTouch()
	{
		touch = true;
	}		
}
