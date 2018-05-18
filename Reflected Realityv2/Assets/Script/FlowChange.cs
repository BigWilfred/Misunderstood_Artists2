using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FlowChange : MonoBehaviour {

	private SpriteRenderer render;
	public Sprite[] sprites;
	private bool eyeTrack = false;
	private int count =0;
	private int fallCount=0;
	private float targetTime;
	private float timer;
	private float videoTimer;

	// Use this for initialization
	void Start () {
		render = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		switch (count) {
		case 0:
			targetTime = 3.0f;
			checkSitting (1, 0);
			break;
		case 1:
			render.sprite = sprites [2];
			videoTimer += Time.deltaTime;
			if (videoTimer > 2.0f && eyeTrack) {
				count = 3;
				videoTimer = 0;
			} else if (videoTimer > 2.0f && !eyeTrack){
				count = 2;
				videoTimer = 0;
			}
			break;
		case 2:
			render.sprite = sprites [3];
			timer += Time.deltaTime;
			if (eyeTrack) {
				count = 3;
				timer = 0;
			} else if (timer > 5.0f) {
				render.sprite = sprites [5];
				videoTimer += Time.deltaTime;
				if (videoTimer > 5.0f) {
					reset ();
				}
			}
			break;
		case 3:
			render.sprite = sprites [4];
			if (Input.GetMouseButton (0)) {
				count = 6;
			} else {
				count = 4;
			}
			break;
		case 4:
			render.sprite = sprites [6];
			timer += Time.deltaTime;
			if (timer > 10.0f) {
				count = 5;
				timer = 0;
			} else if (Input.GetMouseButton (0)) {
				timer = 0;
				videoTimer = 0;
				count = 7;
				fallCount += 1;
				if (fallCount >= 3) {
					count = 9;
				}
			}
			break;
		case 5:
			render.sprite = sprites [7];
			videoTimer += Time.deltaTime;
			if (videoTimer > 5.0f) {
				reset ();
			}
			break;
		case 6:
			render.sprite = sprites [8];
			timer += Time.deltaTime;
			if (timer > 3.0f && Input.GetMouseButton (0)) {
				timer = 0;
				count = 8;
			} else if (!Input.GetMouseButton (0)) {
				count = 4;
			}
			break;
		case 7:
			render.sprite = sprites [9];
			timer += Time.deltaTime;
			if (timer > 3.0f && Input.GetMouseButton (0)) {
				count = 8;
			} else if (!Input.GetMouseButton (0)) {
				count = 4;
			}
			break;
		case 8:
			render.sprite = sprites [10];
			videoTimer += Time.deltaTime;
			if (videoTimer > 5.0f && !Input.GetMouseButton (0)) {
				videoTimer = 0;
				count = 9;
			}
			break;
		case 9:
			render.sprite = sprites [11];
			videoTimer += Time.deltaTime;
			if (videoTimer > 5.0f) {
				reset ();
			}
			break;
		}

	}


	private void checkSitting(int sprite1, int sprite2){
		if (Input.GetMouseButton (0)) {
			render.sprite = sprites [sprite1];
			timer += Time.deltaTime;
			if (timer > targetTime) {
				count += 1;
				timer = 0;
			}
		} else {
			render.sprite = sprites [sprite2];
			timer = 0;
		}
	}
		

	private void reset(){
		count = 0;
		targetTime = 0f;
		timer = 0f;
		videoTimer = 0f;
		eyeTrack = false;
	}

	public void SetEyeTrack(bool eyeLook){
		eyeTrack = eyeLook;
	}
}
