using UnityEngine;

public class PolygonTester : MonoBehaviour {


	void Start () {
		// Create Vector2 vertices
		Vector2[] vertices2D = new Vector2[] {
			new Vector2(0,0),
			new Vector2(0,2),
			new Vector2(2,2),
			new Vector2(2,4),
			new Vector2(0,4),
			new Vector2(0,6),
			new Vector2(6,6),
			new Vector2(6,4),
			new Vector2(4,4),
			new Vector2(4,2),
			new Vector2(6,2),
			new Vector2(6,0),
		};

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
		gameObject.AddComponent<MouseDrag>();
	}
}