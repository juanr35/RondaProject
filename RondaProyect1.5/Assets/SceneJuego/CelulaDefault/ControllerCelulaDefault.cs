using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCelulaDefault : MonoBehaviour {

	static bool delete;
	static int numCeDef;
	static int ceDefDel;
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

		if( delete) {
			Destroy( gameObject);
			numCeDef--;
			_ReiniciarCeDefDel();
		}
	}

	void _DeleteCelula()
	{
		Destroy(gameObject);
		numCeDef--;
		ceDefDel++;
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

	public void _NumCeDefAum()
	{
		numCeDef++;
	}

	public int _NumCeDef()
	{
		return numCeDef;
	}

	public void _ReiniciarCeDefDel()
	{
		ceDefDel = 0;
	}

	public int _NumCeDefDel()
	{
		return ceDefDel;
	}
}