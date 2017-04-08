using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlAudio : MonoBehaviour {

	public AudioClip musica;

	private AudioSource player;

	void Awake(){

		player = GetComponent<AudioSource>();

		if(FindObjectsOfType(GetType()).Length == 1){
			player.clip = musica;
			player.Play();
		}

	}

	void Update(){

		DontDestroyOnLoad(player);

	}
}
