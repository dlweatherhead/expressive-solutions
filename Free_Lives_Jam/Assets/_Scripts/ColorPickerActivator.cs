using UnityEngine;
using System.Collections;

public class ColorPickerActivator : MonoBehaviour {

	private bool isActive = false;
	private GameObject colorPicker;


	// Use this for initialization
	void Start () {
		colorPicker = GameObject.FindGameObjectWithTag("ColorPicker");
		colorPicker.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.C)) {
			if(isActive) {
				isActive = false;
				colorPicker.SetActive(false);
				ColorPicker picker = colorPicker.GetComponent<ColorPicker>() as ColorPicker;
				picker.onValueChanged.RemoveAllListeners();
			} else {
				isActive = true;
				colorPicker.SetActive(true);
			}
		}
	}
}
