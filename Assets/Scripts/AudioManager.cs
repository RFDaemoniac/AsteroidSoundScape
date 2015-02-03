﻿using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		Fabric.EventManager.Instance.SetParameter("Music", "Combat", World.instance.shortTermChange, null);
	}

	public static void Progress() {
		Fabric.EventManager.Instance.PostEvent("Progress", Fabric.EventAction.AdvanceSequence);
	}

	public static void Begin() {
		Fabric.EventManager.Instance.PostEvent("Music");
	}

}
