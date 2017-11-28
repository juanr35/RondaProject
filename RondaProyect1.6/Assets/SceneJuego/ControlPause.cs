using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlPause : MonoBehaviour {

	public GameObject celulaDefault;
	public GameObject celulaCambiar;
	public GameObject celulaOcultar;
	public GameObject celulaDividir;

	public Canvas canvas;
	public Canvas canvasPause;

	// Use this for initialization
	void Start () 
	{
		canvasPause.enabled = false;
	}

	// Update is called once per frame
	void Update () {

	}

	public void _Pause()
	{
		Time.timeScale = 0;
		canvas.enabled = false;
		canvasPause.enabled = true;
	}

	public void _Continuar()
	{
		Time.timeScale = 1;
		canvas.enabled = true;
		canvasPause.enabled = false;
	}

	public void _Salir()
	{
		Time.timeScale = 1;

		celulaDefault.GetComponent<ControllerCelulaDefault>()._ReiniciarCeDefDel();
		celulaCambiar.GetComponent<ControllerCelulaCambiar>()._ReiniciarCeCamDel();
		celulaOcultar.GetComponent<ControllerCelulaOcultar>()._ReiniciarCeOcuDel();
		celulaDividir.GetComponent<ControllerCelulaDividir>()._ReiniciarCeDivDel();

		celulaDefault.GetComponent<ControllerCelulaDefault>()._ReiniciarNumCeDef();
		celulaCambiar.GetComponent<ControllerCelulaCambiar>()._ReiniciarNumCeCam();
		celulaOcultar.GetComponent<ControllerCelulaOcultar>()._ReiniciarNumCeOcu();
		celulaDividir.GetComponent<ControllerCelulaDividir>()._ReiniciarNumCeDiv();

		SceneManager.LoadScene( "Menu");
	}
}
