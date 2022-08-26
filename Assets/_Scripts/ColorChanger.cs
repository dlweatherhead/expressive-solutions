using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour {

	private ColorPicker picker;
	private Renderer renderer;

	private Vector3 screenPoint;
	private Vector3 offset;

	// Use this for initialization
	public void RegisterColorPicker () 
	{
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

	void CycleColorListener() {
		DeregisterColorPicker(); // Deregister all other color listeners.
		RegisterColorPicker(); // Register this object to color picker
	}

	void OnMouseDown() {
		GameObject picker = GameObject.FindGameObjectWithTag("ColorPicker");
		renderer = gameObject.GetComponent<Renderer>();
		if(picker != null) {
			//ColorPicker colorPicker = picker.GetComponent<ColorPicker>();
			//renderer.material.color = colorPicker.CurrentColor;
			renderer.material.color = GlobalLevelManager.colorSelected;
		} else {
			offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
			Cursor.visible = false;
		}
	}

	void OnMouseDrag() 
	{ 
		GameObject picker = GameObject.FindGameObjectWithTag("ColorPicker");
		if(picker == null) {
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
			transform.position = curPosition;
		}

	}

	void OnMouseUp()
	{
		GameObject picker = GameObject.FindGameObjectWithTag("ColorPicker");
		if(picker == null) {
			Cursor.visible = true;	
		}

	}
}
