using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controlador : MonoBehaviour {

	public string escena;

	public void _changeScene(){

		SceneManager.LoadScene(escena);
	}
}
