using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spewnity;

public class AutoPlay : MonoBehaviour {

	void Start()
	{
		SoundManager.instance.Play("song-play");
	}
}
