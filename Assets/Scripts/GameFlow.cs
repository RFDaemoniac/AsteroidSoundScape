﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameFlow : MonoBehaviour {

	public enum State { Play, Pause, Menu, End }

	public Fade white;

	public List<CanvasRenderer> text;

	public static State state = State.Menu;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (state == State.Menu) {
			if (Input.GetKeyDown("space")) {
				Debug.Log ("reset");
				StartGame();
			}
		} else if (state == State.Play) {
			if (Input.GetKeyDown("p")) {
				state = State.Pause;
				Time.timeScale = 0f;
				// TODO: show some paused sign
			}
		} else if (state == State.Pause) {
			if (Input.GetKeyDown ("p")) {
				state = State.Play;
				Time.timeScale = 1f;
				// TODO: hide paused sign
			}
		}
	}

	public void EndGame() {
		if (state != State.Play) return;
		state = State.End;
		Debug.Log ("Game End");
		StartCoroutine(ShowMenuAfterEnd());
		StartCoroutine(white.DoFade(1f, 6f, true));
	}

	private IEnumerator ShowMenuAfterEnd() {
		yield return new WaitForSeconds(4f);
		foreach( CanvasRenderer cr in text) {
			cr.gameObject.SetActive(true);
		}
		World.instance.Clear();
		state = State.Menu;
	}

	public void StartGame() {
		Debug.Log("Game Start");
		World.instance.Clear ();
		StartCoroutine(white.DoFade(0f, 3f, false));
		foreach( CanvasRenderer cr in text) {
			cr.gameObject.SetActive(false);
		}
		state = State.Play;
		AudioManager.Begin();
		World.instance.Reset();
	}

	private static GameFlow _instance;
	
	public static GameFlow instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<GameFlow>();
				
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}
	
	void Awake() 
	{
		if(_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance)
				Destroy(this.gameObject);
		}
	}
}
