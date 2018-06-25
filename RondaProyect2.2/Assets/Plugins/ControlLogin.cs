using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlLogin : MonoBehaviour {

	public GameObject DBconection;

	public void _Registrar()
	{
		SceneManager.LoadScene( "Ronda");
	}
}
