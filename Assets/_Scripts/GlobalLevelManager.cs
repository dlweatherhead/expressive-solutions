using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Gamelogic.Extensions;
using UnityStandardAssets._2D;

// This class manages the level, keeping track of edit and play mode,
//	as well as player scores. It saves the scores and game state for a particular 
//	level in order to make sure the correct player starts.
public class GlobalLevelManager : MonoBehaviour {

	public static GlobalLevelManager Instance;

	// Global UI Fields to Update.
	public Text p1TextMesh;
	public Text p2TextMesh;
	public Text gameNotice;
	public Button startButton;
	public Text startText;

	static float P1Score = 0f;
	static float P2Score = 0f;

	static string P1Name = "Player 1";
	static string P2Name = "Player 2";

	static string editPlayer = P2Name;
	static string activePlayer = P1Name;

	static public Color colorSelected;

	// Use this for initialization
	void Start () {

		if(PlayerPrefs.HasKey("P1Score")) {
			P1Score = PlayerPrefs.GetFloat("P1Score");
		}

		if(PlayerPrefs.HasKey("P2Score")) {
			P2Score = PlayerPrefs.GetFloat("P2Score");
		}

		Instance = gameObject.GetComponent<GlobalLevelManager>();

		p1TextMesh = GameObject.Find("P1Name").GetComponent<Text>();
		p2TextMesh = GameObject.Find("P2Name").GetComponent<Text>();
		gameNotice = GameObject.Find("NoticeText").GetComponent<Text>();
		startButton = GameObject.Find("StartButton").GetComponent<Button>();
		startText = GameObject.Find("StartButtonText").GetComponent<Text>();

		// we ensure this object survives scene loads.
		//DontDestroyOnLoad(gameObject);

		p1TextMesh.text = P1Name + ": " + P1Score.ToString("F2");
		p2TextMesh.text = P2Name + ": " + P2Score.ToString("F2");
		startText.text = "Start " + activePlayer;

		gameNotice.text = editPlayer + " editing level";

		PolygonCreator.placeMode = true;
		LevelExit.editMode = true;
		Platformer2DUserControl.moveMode = false;


	}

	public void Update() {
		if(Input.GetKey(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	public void ExitReached(float timeTaken, float maxTime) {

		int levelToLoad = SceneManager.GetActiveScene().buildIndex;

		if(activePlayer.Equals(P1Name)) {
			P1Score += maxTime - timeTaken;
			P2Score += timeTaken;
			activePlayer = P2Name;
			editPlayer = P1Name;
		}
		else if(activePlayer.Equals(P2Name)) {
			P2Score += maxTime - timeTaken;
			P1Score += timeTaken;
			activePlayer = P1Name;
			editPlayer = P2Name;

			// Override scene to get next scene, as both players have gone.
			levelToLoad += 1;
		}

		PolygonCreator.placeMode = true;
		LevelExit.editMode = true;
		Platformer2DUserControl.moveMode = false;

		LoadLevel(levelToLoad);
	}

	public void MaxTimeReached() {
		gameNotice.text = "Time Ran Out! No Score for anyone :( \n Retry";
		StartCoroutine("ReloadLevel");
	}

	IEnumerator ReloadLevel() {
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void UpdateTimer(float timeRemaining) {
		gameNotice.text = activePlayer + " Playing\n";
		gameNotice.text += "Time: " + timeRemaining.ToString("F2");
	}

	public void StartClicked() {
		startButton.interactable = false;
		startButton.enabled = false;
		StartCoroutine("CountdownToStart");
	}

	IEnumerator CountdownToStart() {
		gameNotice.text = activePlayer + " starting in... 3";
		yield return new WaitForSeconds(1);

		gameNotice.text = activePlayer + " starting in... 2";
		yield return new WaitForSeconds(1);

		gameNotice.text = activePlayer + " starting in... 1";
		yield return new WaitForSeconds(1);

		PolygonCreator.placeMode = false;
		LevelExit.editMode = false;
		Platformer2DUserControl.moveMode = true;
	}

	// Only called from tutorials and at game restart
	public void LoadLevel(int level) {
		PlayerPrefs.SetFloat("P1Score", P1Score);
		PlayerPrefs.SetFloat("P2Score", P2Score);
		SceneManager.LoadScene(level);
	}

	public void RestartGame() {
		PlayerPrefs.SetFloat("P1Score", 0f);
		PlayerPrefs.SetFloat("P2Score", 0f);
		SceneManager.LoadScene(3);
	}

	public void ColorChanged(Color color) {
		colorSelected = color;
	}
}
