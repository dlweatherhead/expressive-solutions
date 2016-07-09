using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour {

	// Use this for initialization
	public void RegisterColorPicker () 
	{
		ColorPicker picker = GameObject.FindGameObjectWithTag("ColorPicker").GetComponent<ColorPicker>() as ColorPicker;

		Renderer renderer = gameObject.GetComponent<Renderer>();

		picker.onValueChanged.AddListener(color =>
			{
				renderer.material.color = color;
			});
		renderer.material.color = picker.CurrentColor;
	}

	public void DeregisterColorPicker() {
		ColorPicker picker = GameObject.FindGameObjectWithTag("ColorPicker").GetComponent<ColorPicker>() as ColorPicker;

		picker.onValueChanged.RemoveAllListeners();
	}

	void OnMouseDown() {
		DeregisterColorPicker(); // Deregister all other color listeners.
		RegisterColorPicker(); // Register this object to color picker
	}
}
