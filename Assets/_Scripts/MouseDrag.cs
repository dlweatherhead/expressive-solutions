using UnityEngine;
using System.Collections;

public class MouseDrag : MonoBehaviour {

	private Vector3 screenPoint;
	private Vector3 offset;

	void OnMouseDown() {
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		Cursor.visible = false;
	}

	void OnMouseDrag() 
	{ 
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
	}

	void OnMouseUp()
	{
		Cursor.visible = true;
	}
}
