using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void _Jugar()
	{
		SceneManager.LoadScene( "Ronda");
	}

	public void _Salir()
	{
		Application.Quit();
	}
}
