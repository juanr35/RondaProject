using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMain2 : MonoBehaviour {

	public GameObject celulaDefault;
	public GameObject celulaDividir;

	// Use this for initialization
	void Start () 
	{
	}

	// Update is called once per frame
	void Update () {

	}

	public void Instanciar1()
	{ 
			celulaDefault.GetComponent<ControllerCelulaDefault>()._NotDelete();
			celulaDefault.GetComponent<ControllerCelulaDefault>()._NumCeAumen();
			celulaDefault.GetComponent<ControllerCelulaDefault>()._PrintNumCe();
			GameObject ceDef = Instantiate( celulaDefault, transform.position, Quaternion.identity);
			ceDef.transform.SetParent( transform.parent);
			ceDef.transform.localPosition = new Vector3( 100, 0, 0);
	}

	public void Instanciar2()
	{ 
		celulaDividir.GetComponent<ControllerCelulaDividir>()._NotDelete();
		celulaDividir.GetComponent<ControllerCelulaDividir>()._NumCeAumen();
		celulaDividir.GetComponent<ControllerCelulaDividir>()._PrintNumCe();
		GameObject ceDiv = Instantiate( celulaDividir, transform.position, Quaternion.identity);
		ceDiv.transform.SetParent( transform.parent);
		ceDiv.transform.localPosition = new Vector3( -100, 0, 0);
	}
		
}