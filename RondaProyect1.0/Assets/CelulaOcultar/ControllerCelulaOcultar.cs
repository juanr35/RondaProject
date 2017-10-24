using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCelulaOcultar : MonoBehaviour {

	bool touch;
	Animator animator;
	int countTouch;
	public GameObject celulaReturnPrefab;

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
	}

	public void EnaTouch()
	{
		touch = true;
		countTouch = ++countTouch;
	}

	public void Aparecer()
	{
	//	if( countTouch > 3) {
			GameObject celulaReturn = Instantiate( celulaReturnPrefab, transform.position, Quaternion.identity);
			celulaReturn.transform.parent = transform.parent;

			Destroy( gameObject);
	//	}			
	}
}
