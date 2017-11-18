using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCelulaCambiar : MonoBehaviour {

	bool touch;
	Animator animator;
	public GameObject celulaRedPrefab;

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
	}

	public void EnaTouch()
	{
		touch = true;
	}

	public void Cambiar()
	{
		GameObject celulaRed = Instantiate( celulaRedPrefab, transform.position, Quaternion.identity);
		celulaRed.transform.SetParent( transform.parent, false);
		celulaRed.transform.SetPositionAndRotation( transform.position, Quaternion.identity);

		Destroy( gameObject);
	}
}
