using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class videoController : MonoBehaviour {
	private VideoPlayer player;
	private bool isDone;

	// Use this for initialization
	void Start () {
		player = GetComponent<VideoPlayer> ();
		player.loopPointReached +=checkOver;
		player.prepareCompleted += prepareCompleted;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void checkOver(VideoPlayer vp){
		Debug.Log ("video over");	
		isDone = true;
	}

	private bool isPrepared(){
		return player.isPrepared;
	}

	private void prepareCompleted(VideoPlayer vp){
		Debug.Log ("Video prepared");
		isDone = false;
	}

	public void loadVideo(VideoClip clip){
		player.clip = clip;
		player.Prepare ();
	}

	public void playVideo(){
		if (!isPrepared())
			return;
		player.Play ();
	}

	public void setActive(bool active){
		gameObject.SetActive (active);
	}
}
