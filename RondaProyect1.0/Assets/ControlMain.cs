using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMain : MonoBehaviour {

	public GameObject celulaDefault;
	public GameObject celulaCambiar;
	public GameObject celulaOcultar;
	public GameObject celulaDividir;
	public GameObject celulaRed;

	int opcion, valX, valY;

	// Use this for initialization
	void Start () 
	{
		valX = -150;
		valY = 150;
		opcion = Random.Range( 1, 5);

		for( int i = 0; i < 3; i++) {
			for( int j = 0; j < 3; j++) {

				if( valX == 0 && valY == 0) {
					Instanciar( 0, 0, 0);
					opcion = Random.Range( 1, 5);
					valX += 150;
				}

				else {
					Instanciar( opcion, valX, valY);
					opcion = Random.Range( 1, 5);
					valX += 150;
				}
			}

			valY -= 150;
			valX = -150;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Instanciar( int opc, int x, int y)
	{ 
		switch( opc) {

			case 0:
				GameObject ceRed = Instantiate( celulaRed, transform.position, Quaternion.identity);
				ceRed.transform.parent = transform.parent;
				ceRed.transform.localPosition = new Vector3( x, y, 0);
				break;

			case 1:
				GameObject ceDef = Instantiate( celulaDefault, transform.position, Quaternion.identity);
				ceDef.transform.parent = transform.parent;
				ceDef.transform.localPosition = new Vector3( x, y, 0);
				break;

			case 2:
				GameObject ceCam = Instantiate( celulaCambiar, transform.position, Quaternion.identity);
				ceCam.transform.parent = transform.parent;
				ceCam.transform.localPosition = new Vector3( x, y, 0);
				break;

			case 3:
				GameObject ceOcu = Instantiate( celulaOcultar, transform.position, Quaternion.identity);
				ceOcu.transform.parent = transform.parent;
				ceOcu.transform.localPosition = new Vector3( x, y, 0);
				break;

			case 4:
				GameObject ceDiv = Instantiate( celulaDividir, transform.position, Quaternion.identity);
				ceDiv.transform.parent = transform.parent;
				ceDiv.transform.localPosition = new Vector3( x, y, 0);
				break;

			default:
				print("Opcion invalida :(");
				break;
		}
	}
}