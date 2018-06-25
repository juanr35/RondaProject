using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCelulaOcultar : MonoBehaviour {

	static bool delete;
	static int numCeOcu;
	static int ceOcuDel;
	bool touch;
	Animator animator;
	int countTouch;
	public GameObject celulaDefault;

	// Use this for initialization
	void Start () 
	{
		touch = false;	
		animator = GetComponent<Animator>();
		countTouch = 0;
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
			numCeOcu--;
			_ReiniciarCeOcuDel();
		}
	}

	public void _EnaTouch()
	{
		touch = true;
		countTouch = ++countTouch;
	}

	public void _Aparecer()
	{
	//	if( countTouch > 3) {
			celulaDefault.GetComponent<ControllerCelulaDefault>()._NumCeDefAum();
			GameObject celulaDef = Instantiate( celulaDefault, transform.position, Quaternion.identity);
			celulaDef.transform.SetParent( transform.parent, false);
			celulaDef.transform.SetPositionAndRotation( transform.position, Quaternion.identity);

			numCeOcu--;
			ceOcuDel++;
			Destroy( gameObject);
	//	}			
	}

	public void _Delete()
	{
		delete = true;
	}

	public void _NotDelete()
	{
		delete = false;
	}

	public void _NumCeOcuAum()
	{
		numCeOcu++;
	}

	public int _NumCeOcu()
	{
		return numCeOcu;
	}

	public void _ReiniciarCeOcuDel()
	{
		ceOcuDel = 0;
	}

	public int _NumCeOcuDel()
	{
		return ceOcuDel;
	}

	public void _ReiniciarNumCeOcu()
	{
		numCeOcu = 0;
	}
}