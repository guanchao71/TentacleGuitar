﻿namespace TentacleGuitarUnity {
using UnityEngine;
using System.Collections;

public class ForTest : MonoBehaviour {

	bool KeyboardCamera = false;
	bool RandomNote = true;
	string SongPath = "";
	string BeatMapPath = "";
	int currentTestTruck = 12;
	float currentTestAngle = 0f;
	int speedScale = 4;


	void Start () {

		RandomNote = PlayerPrefs.GetInt("Test_RandomNote", 1) == 1;
		KeyboardCamera = PlayerPrefs.GetInt("Test_KeyboardCamera", 1) == 1;
		SongPath = PlayerPrefs.GetString("Test_SongPath", Application.dataPath + @"/Short.wav");
		BeatMapPath = PlayerPrefs.GetString("Test_BeatMapPath", Application.dataPath + @"/Short.wav");
		speedScale = PlayerPrefs.GetInt("Test_speedScale", 4);
		

	}


	void Update () {


		if (RandomNote && Stage.GamePlaying) {
			if (StageMusic.Main.IsPlaying && Stage.Time + Stage.TheStageSetting.ShowNoteTime < StageMusic.Main.Length) {
				Stage.RandomAddNote(
					Mathf.Clamp(Stage.Time + Stage.TheStageSetting.ShowNoteTime, 0f, StageMusic.Main.Length),
					0, 24, 0, 6, 0f, 1.03f
				);
			}
		}


		if (KeyboardCamera && Stage.GamePlaying) {

			if (Input.GetKeyDown(KeyCode.A)) {
				currentTestTruck = Mathf.Clamp(currentTestTruck - 1, 2, 21);
				Stage.MoveCamera(currentTestTruck, 0.1f);
			}

			if (Input.GetKeyDown(KeyCode.D)) {
				currentTestTruck = Mathf.Clamp(currentTestTruck + 1, 2, 21);
				Stage.MoveCamera(currentTestTruck, 0.1f);
			}

			if (Input.GetKeyDown(KeyCode.E)) {
				currentTestAngle = Mathf.Clamp(currentTestAngle + 10f, -30f, 30f);
				Stage.RotCamera(new Vector3(0f, currentTestAngle, 0f), 0.1f);
			}

			if (Input.GetKeyDown(KeyCode.Q)) {
				currentTestAngle = Mathf.Clamp(currentTestAngle - 10f, -30f, 30f);
				Stage.RotCamera(new Vector3(0f, currentTestAngle, 0f), 0.1f);
			}


		}

	}


	void OnGUI () {

		GUILayout.BeginHorizontal();
		GUI.Label(GUILayoutUtility.GetRect(70f, 24f), "Song");
		SongPath = GUI.TextField(GUILayoutUtility.GetRect(170f, 30f), SongPath);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUI.Label(GUILayoutUtility.GetRect(70f, 24f), "BeatMap");
		BeatMapPath = GUI.TextField(GUILayoutUtility.GetRect(170f, 30f), BeatMapPath);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUI.Label(GUILayoutUtility.GetRect(70f, 24f), "Speed Scale");
		string ss = GUI.TextField(GUILayoutUtility.GetRect(170f, 30f), speedScale.ToString());
		if (int.TryParse(ss, out speedScale)) {
			speedScale = Mathf.Clamp(speedScale, 0, 8);
		}
		GUILayout.EndHorizontal();

		GUILayout.Space(4f);

		GUILayout.BeginHorizontal();
		if(GUI.Button(GUILayoutUtility.GetRect(80f, 20f), "Start")){
			Stage.StartGame(SongPath, BeatMapPath);
		}

		GUILayout.Space(10f);

		if (GUI.Button(GUILayoutUtility.GetRect(80f, 24f), "Stop")) {
			Stage.StopGame();
		}

		GUILayout.EndHorizontal();
		GUILayout.Space(4f);

		GUILayout.BeginHorizontal();
		if (GUI.Button(GUILayoutUtility.GetRect(80f, 24f), "Pause / Play")) {
			Stage.PlayPauseGame();
		}
		GUILayout.Space(10f);
		if (GUI.Button(GUILayoutUtility.GetRect(80f, 24f), "SignOut")) {
			Stage.Main.StartSignOut();
		}
		GUILayout.EndHorizontal();
		GUILayout.Space(4f);

		KeyboardCamera = GUI.Toggle(GUILayoutUtility.GetRect(170f, 30f), KeyboardCamera, " ADEQ Move Camera");

		RandomNote = GUI.Toggle(GUILayoutUtility.GetRect(170f, 30f), RandomNote, " Random Add Note");

		GUILayout.BeginHorizontal();

		GUI.Label(GUILayoutUtility.GetRect(70f, 24f), "Perfect: " + StageScore.PerfectNum.ToString());
		GUI.Label(GUILayoutUtility.GetRect(70f, 24f), "Good: " + StageScore.GoodNum.ToString());
		GUI.Label(GUILayoutUtility.GetRect(70f, 24f), "Miss: " + StageScore.MissNum.ToString());

		GUILayout.EndHorizontal();


		GUILayout.BeginHorizontal();

		GUI.Label(GUILayoutUtility.GetRect(70f, 24f), "Score: " + StageScore.CurrentScore.ToString());
		GUI.Label(GUILayoutUtility.GetRect(70f, 24f), "Num: " + StageScore.NoteNum.ToString());

		GUILayout.EndHorizontal();



		if (GUI.changed) {
			PlayerPrefs.SetString("Test_SongPath", SongPath);
			PlayerPrefs.SetInt("Test_KeyboardCamera", KeyboardCamera ? 1 : 0);
			PlayerPrefs.SetInt("Test_RandomNote", RandomNote ? 1 : 0);
			PlayerPrefs.SetString("Test_BeatMapPath", BeatMapPath);
			PlayerPrefs.SetInt("Test_speedScale", speedScale);
		}

		

	}



}
}