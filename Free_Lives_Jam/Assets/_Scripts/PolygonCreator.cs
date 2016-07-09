using UnityEngine;
using System.Collections;

// Listens to mouse clicks to create polygon points and instantiate a new gameObject.
public class PolygonCreator : MonoBehaviour {

	void Update()
	{
		if(Input.GetMouseButtonDown(0)) Debug.Log("Pressed left click.");
		if(Input.GetMouseButtonDown(1)) Debug.Log("Pressed right click.");
		if(Input.GetMouseButtonDown(2)) Debug.Log("Pressed middle click.");
	}

}
