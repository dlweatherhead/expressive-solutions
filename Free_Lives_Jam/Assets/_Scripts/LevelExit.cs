using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelExit : MonoBehaviour {

	public float maxTime = 60f;
	private float time = 0f;

	public static bool editMode = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!editMode) {
			time += Time.deltaTime;

			if(time > maxTime) {
				GlobalLevelManager.Instance.MaxTimeReached();
			}
				
			GlobalLevelManager.Instance.UpdateTimer(maxTime - time);
		}
	}

	// Check Collision with player
	void OnCollisionEnter2D(Collision2D collision) {

		Debug.Log("Collision occured");

		if(collision.gameObject.tag.Equals("Player")) {
			// Save the score and time and start the Level for Player 2.

			GlobalLevelManager.Instance.ExitReached(time, maxTime);
		}
	}
}
