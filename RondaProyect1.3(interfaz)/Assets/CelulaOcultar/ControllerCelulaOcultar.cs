using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCelulaOcultar : MonoBehaviour {

	static bool delete;
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

		if( delete)
			Destroy( gameObject);
	}

	public void _EnaTouch()
	{
		touch = true;
		countTouch = ++countTouch;
	}

	public void _Aparecer()
	{
	//	if( countTouch > 3) {
			GameObject celulaReturn = Instantiate( celulaReturnPrefab, transform.position, Quaternion.identity);
			celulaReturn.transform.SetParent( transform.parent, false);
			celulaReturn.transform.SetPositionAndRotation( transform.position, Quaternion.identity);

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
}
