using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlPause : MonoBehaviour {

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
		SceneManager.LoadScene( "Menu");
	}
}
