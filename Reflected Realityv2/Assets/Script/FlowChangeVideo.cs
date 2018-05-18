using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class FlowChangeVideo : MonoBehaviour {

	private Texture texture;

	public MovieTexture[] clips;

	private bool eyeTrack = false;
	private int count =0;
	private int fallCount=0;
	private float targetTime;
	private float timer;

	private bool checkInput= false;

	private bool videoRun = false;

	// Use this for initialization
	void Start () {
		GetComponent<RawImage> ().texture = clips[0] as MovieTexture;
		clips [0].Play ();
		clips [0].loop = true;

	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (count);
		switch (count) {
		// background nothing
		case 0:
			if (Input.GetMouseButton (0)) {
				videoRun = false;
				playVideo (1, true);
				timer += Time.deltaTime;
				if (timer > 3.0f) {
					videoRun = false;
					count = 1;
					timer = 0;
				}
			} else {
				videoRun = false;
				playVideo (0, true);
				timer = 0;
			}
			break;
		// old man walks in
		case 1:
			playVideo (2, false);
			// wait for the old man to appear on screen
			if (!clips [2].isPlaying && eyeTrack) {
				count = 3;
				videoRun = false;
			} else if (!clips [2].isPlaying && !eyeTrack){
				count = 2;
				videoRun = false;
			}
			break;
		// user ignore, cough
		case 2:
			playVideo (3, true);
			timer += Time.deltaTime;
			Debug.Log (eyeTrack);
			if (eyeTrack) {
				count = 3;
				timer = 0;
				videoRun = false;
			} else if (timer > 10.0f) {
				count = 10;
				timer = 0;
				videoRun = false;
			}
			break;
		// user look, gesture
		case 3:
			playVideo (4,false);
			if (!clips [4].isPlaying && Input.GetMouseButton (0)) {
				count = 6;
				videoRun = false;
			} else if (!Input.GetMouseButton (0)) {
				count = 4;
				videoRun = false;
			}
			break;
		// 	user standing, old man sit down
		case 4:
			playVideo (6, false);
			Debug.Log (timer);
			timer += Time.deltaTime;
			if (!clips [6].isPlaying && timer > 20.0f) {
				count = 5;
				timer = 0;
				videoRun = false;
			} else if (Input.GetMouseButton (0)) {
				fallCount += 1;
				timer = 0;
				count = 7;
				videoRun = false;
			}
			break;
		// user still standing, old man thanks and leave
		case 5:
			playVideo (7,false);
			if (!clips [7].isPlaying) {
				reset ();
			}
			break;
		// eye tack but still sitting, old man face and stare
		case 6:
			playVideo (8,false);
			timer += Time.deltaTime;
			if (!clips [8].isPlaying && Input.GetMouseButton (0)) {
				timer = 0;
				count = 8;
				videoRun = false;
			} else if (!Input.GetMouseButton (0)) {
				count = 4;
				timer = 0;
				videoRun = false;
			}
			break;
		// user sits down while the old man is sitting falls over
		case 7:
			playVideo (9, false);
			if (!clips [9].isPlaying && Input.GetMouseButton (0)) {
				count = 8;
				videoRun = false;
			} else if (!clips [9].isPlaying && !Input.GetMouseButton (0)) {
				count = 4;
				videoRun = false;
			}

			if (!clips [9].isPlaying &&  fallCount >= 3) {
				count = 9;
				videoRun = false;
			}
			break;
		// not giving up seat, old man gets angry
		case 8:
			playVideo (10,false);
			if (!clips [10].isPlaying && Input.GetMouseButton (0)) {
				count = 9;
				videoRun = false;
			} else if (!Input.GetMouseButton (0)){
				count = 4;
				videoRun = false;
			}
			break;
		// still not giving up seat
		case 9:
			playVideo (11,false);
			if (!clips [11].isPlaying) {
				reset ();
			}
			break;
		// old man falls over
		case 10:
			playVideo (5,false);
			if (!clips [5].isPlaying) {
				reset ();
			}
			break;
		}


	}
		

	private void playVideo(int num, bool loop){
		if (!videoRun) {
			GetComponent<RawImage> ().texture = clips [num] as MovieTexture;
			clips [num].Stop ();
			clips [num].Play ();
			clips [num].loop = false;
			if (loop) {
				clips [num].loop = true;
			} 
			videoRun = true;
		}
	}
		

	private void reset(){
		count = 0;
		targetTime = 0f;
		timer = 0f;
		eyeTrack = false;
		videoRun = false;
	}

	public void SetEyeTrack(bool eyeLook){
		eyeTrack = eyeLook;
	}
		
}
