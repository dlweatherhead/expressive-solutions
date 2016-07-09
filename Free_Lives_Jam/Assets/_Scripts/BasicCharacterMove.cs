using UnityEngine;
using System.Collections;

public class BasicCharacterMove : MonoBehaviour {

	public float moveSpeed = 1.0f;
	public float jumpPower = 1.0f;

	private Rigidbody2D rigidbody2D;

	// Use this for initialization
	void Start () {
		rigidbody2D = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
	}
	
	// Update is called once per frame
	void Update () {

		transform.rotation = Quaternion.identity; // reset the rotation every frame.

		float x = Input.GetAxis("Horizontal");
		transform.Translate(new Vector2(x * moveSpeed, 0));

		if(Input.GetKeyDown(KeyCode.Space)) {
			rigidbody2D.AddForce(new Vector2(0f, jumpPower));
		}
	}
}
