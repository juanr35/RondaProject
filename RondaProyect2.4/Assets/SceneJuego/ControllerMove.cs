using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMove : MonoBehaviour {

	float speed;
	Vector3 newPos;

	// Use this for initialization
	void Start () 
	{
		speed = Random.Range( 2, 5);
		newPos = new Vector3( Random.onUnitSphere.x * 4, Random.onUnitSphere.y * 4, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = Vector3.MoveTowards( transform.position, newPos, speed  * Time.deltaTime);
		//transform.SetPositionAndRotation( Vector3.MoveTowards( transform.position, newPos, speed * Time.deltaTime), Quaternion.identity);

		if( transform.position == newPos)
			_NewPositionAndSpeed();
	}

	void _NewPositionAndSpeed()
	{
		speed = Random.Range( 2, 5) * 0.08f;
		newPos = new Vector3( Random.onUnitSphere.x * 4, Random.onUnitSphere.y * 4, 0);

	}

	void OnDrawGizmos() 
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere( new Vector3( 0, 0, 0), 4);
	}
}
