using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCelulaDividir : MonoBehaviour {

	static bool delete;
	bool touch;
	Animator animator;
	public GameObject celulaPrefab;
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

		if( delete)
			Destroy( gameObject);
	}

	public void _EnaTouch()
	{
		touch = true;
	}

	public void _Dividir()
	{
		GameObject celula1 = Instantiate( celulaPrefab, transform.position, Quaternion.identity);
		celula1.transform.SetParent( transform, false);
		celula1.transform.localPosition = new Vector3( 1 * setX, 0, 0);
		celula1.transform.SetParent( transform.parent);

		GameObject celula2 = Instantiate( celulaPrefab, transform.position, Quaternion.identity);
		celula2.transform.SetParent( transform, false);
		celula2.transform.localPosition = new Vector3( -1 * setX, 0, 0);
		celula2.transform.SetParent( transform.parent);

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
}