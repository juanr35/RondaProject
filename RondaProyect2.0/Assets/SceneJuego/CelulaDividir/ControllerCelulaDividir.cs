using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCelulaDividir : MonoBehaviour {

	static bool delete;
	static int numCeDiv;
	static int ceDivDel;
	bool touch;
	Animator animator;
	public GameObject celulaDefault;
	public int setX;

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
			numCeDiv--;
			_ReiniciarCeDivDel();
		}
	}

	public void _EnaTouch()
	{
		touch = true;
	}

	public void _Dividir()
	{
		celulaDefault.GetComponent<ControllerCelulaDefault>()._NumCeDefAum();
		GameObject celula1 = Instantiate( celulaDefault, transform.position, Quaternion.identity);
		celula1.transform.SetParent( transform, false);
		celula1.transform.localPosition = new Vector3( 1 * setX, 0, 0);
		celula1.transform.SetParent( transform.parent);

		celulaDefault.GetComponent<ControllerCelulaDefault>()._NumCeDefAum();
		GameObject celula2 = Instantiate( celulaDefault, transform.position, Quaternion.identity);
		celula2.transform.SetParent( transform, false);
		celula2.transform.localPosition = new Vector3( -1 * setX, 0, 0);
		celula2.transform.SetParent( transform.parent);

		Destroy( gameObject);
		numCeDiv--;
		ceDivDel++;
	}
		
	public void _Delete()
	{
		delete = true;
	}

	public void _NotDelete()
	{
		delete = false;
	}

	public void _NumCeDivAum()
	{
		numCeDiv++;
	}

	public int _NumCeDiv()
	{
		return numCeDiv;
	}

	public void _ReiniciarCeDivDel()
	{
		ceDivDel = 0;
	}

	public int _NumCeDivDel()
	{
		return ceDivDel;
	}

	public void _ReiniciarNumCeDiv()
	{
		numCeDiv = 0;
	}
}