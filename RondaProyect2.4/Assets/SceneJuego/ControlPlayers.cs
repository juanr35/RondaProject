using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPlayers : MonoBehaviour {

	Text Player1;
	Text Player2;

	void Start () {
		_AsignarPlayers ();
	}
	
	public void _AsignarPlayers(){

		Player1 = transform.parent.Find("Player1").gameObject.GetComponent<Text>();
		Player1.text = StaticPlayers.p1;

		Player2 = transform.parent.Find("Player2").gameObject.GetComponent<Text>();
		Player2.text = StaticPlayers.p2;

	}
}
