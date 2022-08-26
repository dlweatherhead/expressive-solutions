using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class BasicCharacterMove : MonoBehaviour {

	public float moveSpeed = 50f;
	public float jumpPower = 5000f;

	public static bool moveMode = false;

	private Rigidbody2D rigid2D;

	// Use this for initialization
	void Start () {
		rigid2D = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(moveMode) {

			transform.rotation = Quaternion.identity; // reset the rotation every frame.

			float h = CrossPlatformInputManager.GetAxis("Horizontal");
			rigid2D.velocity = new Vector2(h * moveSpeed, rigid2D.velocity.y);

			if(Input.GetKeyDown(KeyCode.Space)) {
				rigid2D.AddForce(new Vector2(0f, jumpPower));
			}	
		}

	}
}
