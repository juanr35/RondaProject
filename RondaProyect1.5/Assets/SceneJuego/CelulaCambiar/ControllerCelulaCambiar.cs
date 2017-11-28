using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCelulaCambiar : MonoBehaviour {

	static bool delete;
	static int numCeCam;
	static int ceCamDel;
	bool touch;
	Animator animator;
	public GameObject celulaRed;

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

		if( delete) {
			Destroy( gameObject);
			numCeCam--;
			_ReiniciarCeCamDel();
		}
	}

	public void _EnaTouch()
	{
		touch = true;
	}

	public void _Cambiar()
	{
		GameObject ceRed = Instantiate( celulaRed, transform.position, Quaternion.identity);
		ceRed.transform.SetParent( transform.parent, false);
		ceRed.transform.SetPositionAndRotation( transform.position, Quaternion.identity);

		numCeCam--;
		ceCamDel++;
		Destroy( gameObject);
	}

	public void _Delete()
	{
		delete = true;
	}

	public void _NotDelete()
	{
		delete = false;
	}

	public void _NumCeCamAum()
	{
		numCeCam++;
	}

	public int _NumCeCam()
	{
		return numCeCam;
	}

	public void _ReiniciarCeCamDel()
	{
		ceCamDel = 0;
	}

	public int _NumCeCamDel()
	{
		return ceCamDel;
	}
}