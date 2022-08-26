using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Listens to mouse clicks to create polygon points and instantiate a new gameObject.
public class PolygonCreator : MonoBehaviour {

	public GameObject widget;
	public GameObject emptyPoly;

	public static bool placeMode = true;

	List<Vector2> points = new List<Vector2>();

	bool editMode = false;

	void Update()
	{		
		if(placeMode) {

			if(Input.GetMouseButtonDown(0) && editMode) {
				float x = Input.mousePosition.x;
				float y = Input.mousePosition.y;
				Vector3 v3 = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));
				AddNewPoint(v3);
			}

			if(Input.GetMouseButtonDown(1)) {

				if(editMode) {
					editMode = false;
					ClearPoints();
					CreateObject();
					ClearList();
				} else {
					editMode = true;
				}
			}	
		}

	}

	void AddNewPoint(Vector2 v2) {
		// Add the point to the List
		points.Add(new Vector2(v2.x,v2.y));

		// Create the widget at the point
		GameObject.Instantiate(widget, v2, Quaternion.identity);
	}

	void CreateObject() {
		if(points.Count > 1) {
			CreatePolygon(points.ToArray(), emptyPoly);	
		}
	}

	void ClearPoints() {
		GameObject[] widgets = GameObject.FindGameObjectsWithTag("PointWidget");

		foreach(GameObject widget in widgets) {
			Destroy(widget);
		}
	}

	void ClearList() {
		points.Clear();
	}

	void CreatePolygon(Vector2[] vertices2D, GameObject emptyPrefab) {

		Vector2 centreV = Vector2.zero;
		foreach(Vector2 v in vertices2D) {
			centreV += v;
		}
		centreV = centreV / vertices2D.Length;

		GameObject gameObject = GameObject.Instantiate(emptyPrefab, centreV, Quaternion.identity) as GameObject;

		// Normalise the vertices array to the GameObject position.
		for(int i = 0; i < vertices2D.Length; i++) {
			vertices2D[i] -= centreV;
		}

		// Use the triangulator to get indices for creating triangles
		Triangulator tr = new Triangulator(vertices2D);
		int[] indices = tr.Triangulate();

		// Create the Vector3 vertices
		Vector3[] vertices = new Vector3[vertices2D.Length];
		for (int i=0; i<vertices.Length; i++) {
			vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
		}

		// Create the mesh
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = indices;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();

		// Set up game object with mesh;
		gameObject.AddComponent(typeof(MeshRenderer));
		MeshFilter filter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
		filter.mesh = mesh;

		// Add a rigidbody to the gameobject
		Rigidbody2D rigidBody = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
		rigidBody.mass = 1000;

		// Add collider to object
		PolygonCollider2D collider = gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
		collider.points = vertices2D;

		// Add the Mouse Drag script
		//gameObject.AddComponent<MouseDrag>();

		// Add the Color Change Script
		gameObject.AddComponent<ColorChanger>();

		// Add Color Indicator Script
		//gameObject.AddComponent<ColorIndicator>();
	}
}
