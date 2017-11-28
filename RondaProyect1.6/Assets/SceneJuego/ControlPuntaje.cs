using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPuntaje : MonoBehaviour {

	public GameObject celulaDefault;
	public GameObject celulaCambiar;
	public GameObject celulaOcultar;
	public GameObject celulaDividir;
	public GameObject flecha;

	Transform player;

	Text puntaje;

	int numCe, countMoves, opc;
	bool turno, aumentar;

	// Use this for initialization
	void Start () 
	{
		opc = 1;
		aumentar = true;

		player = transform.parent.GetChild( opc);
		flecha.transform.position = new Vector3( player.transform.position.x - 2.7f, player.transform.position.y, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		_CountNumCeDel();

		if( countMoves == 3)
			if( numCe == 1 && aumentar) {
				_SumarPunto();
				_NextPlayer();
			}
			else {
				_NextPlayer();
			}

		if( turno) {
			if( countMoves > 0) {
				_NextPlayer();
				_DisableButtomTurno();
			}

			else
				_DisableButtomTurno();
		}	

		_CountNumCe();

		if( numCe == 0 && aumentar)
			_SumarPunto();
	}

	void _CountNumCe()
	{
		numCe = celulaDefault.GetComponent<ControllerCelulaDefault>()._NumCeDef() +
				celulaCambiar.GetComponent<ControllerCelulaCambiar>()._NumCeCam() +
				celulaOcultar.GetComponent<ControllerCelulaOcultar>()._NumCeOcu() +
				celulaDividir.GetComponent<ControllerCelulaDividir>()._NumCeDiv();
	}

	void _CountNumCeDel()
	{
		countMoves = celulaDefault.GetComponent<ControllerCelulaDefault>()._NumCeDefDel() +
					 celulaCambiar.GetComponent<ControllerCelulaCambiar>()._NumCeCamDel() +
					 celulaOcultar.GetComponent<ControllerCelulaOcultar>()._NumCeOcuDel() +
					 celulaDividir.GetComponent<ControllerCelulaDividir>()._NumCeDivDel();
	}
		
	void _NextPlayer()
	{
		celulaDefault.GetComponent<ControllerCelulaDefault>()._ReiniciarCeDefDel();
		celulaCambiar.GetComponent<ControllerCelulaCambiar>()._ReiniciarCeCamDel();
		celulaOcultar.GetComponent<ControllerCelulaOcultar>()._ReiniciarCeOcuDel();
		celulaDividir.GetComponent<ControllerCelulaDividir>()._ReiniciarCeDivDel();

		player = transform.parent.GetChild( opc == 1 ? ++opc : --opc);
		flecha.transform.position = new Vector3( player.transform.position.x - 2.7f, player.transform.position.y, 0);
	}

	public void _EnableButtomTurno()
	{
		turno = true;
	}

	void _DisableButtomTurno()
	{
		turno = false;
	}

	public void _EnableAumentar()
	{
		aumentar = true;
	}

	void _SumarPunto()
	{
		puntaje = transform.parent.Find( "Puntaje" + opc.ToString() ).gameObject.GetComponent<Text>();
		puntaje.text = ( int.Parse( puntaje.text) + 1).ToString();
		aumentar = false;
	}

	public void _CederPunto()
	{
		if( numCe > 0) {
			_NextPlayer();
			_SumarPunto();
		}
	}	
}