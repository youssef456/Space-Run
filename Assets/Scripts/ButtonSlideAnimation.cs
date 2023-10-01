﻿//----------------------------------------------
//           	   Highway Racer
//
// Copyright © 2016 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonSlideAnimation : MonoBehaviour {

	public SlideFrom slideFrom;
	public enum SlideFrom{Left, Right, Top, Buttom}
	public bool actWhenEnabled = false;
	public bool playSound = true;

	private RectTransform getRect;
	private Vector2 originalPosition;
	public bool actNow = false;
	public bool endedAnimation = false;
	public ButtonSlideAnimation playWhenThisEnds;

	public AudioSource slidingAudioSource;
    public AudioClip SlidingaudioClip;

	void Awake () {

        slidingAudioSource.clip = SlidingaudioClip;
        slidingAudioSource.loop = false;

        getRect = GetComponent<RectTransform>();
		originalPosition = GetComponent<RectTransform>().anchoredPosition;

		SetOffset();


    }

	void SetOffset(){

		switch(slideFrom){
		case SlideFrom.Left:
			GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000f, originalPosition.y);
			break;
		case SlideFrom.Right:
			GetComponent<RectTransform>().anchoredPosition = new Vector2(1000f, originalPosition.y);
			break;
		case SlideFrom.Top:
			GetComponent<RectTransform>().anchoredPosition = new Vector2(originalPosition.x, 500f);
			break;
		case SlideFrom.Buttom:
			GetComponent<RectTransform>().anchoredPosition = new Vector2(originalPosition.x, -500f);
			break;
		}

	}

	void OnEnable(){

		if(actWhenEnabled){
			SetOffset();
			endedAnimation = false;
			Animate();
		}

	}

	public void Animate ()
    {
		actNow = true;

	}

	void Update(){

		if(!actNow || endedAnimation)
			return;

		if(playWhenThisEnds != null && !playWhenThisEnds.endedAnimation)
			return;

		if(slidingAudioSource && !slidingAudioSource.isPlaying && playSound)
			slidingAudioSource.Play();

		getRect.anchoredPosition = Vector2.MoveTowards(getRect.anchoredPosition, originalPosition, Time.unscaledDeltaTime * 4000f);

		if(Vector2.Distance(GetComponent<RectTransform>().anchoredPosition, originalPosition) < .05f){

			if(slidingAudioSource && slidingAudioSource.isPlaying && playSound)
				slidingAudioSource.Stop();

			GetComponent<RectTransform>().anchoredPosition = originalPosition;
			if(GetComponentInChildren<CountAnimation>()){
				if(!GetComponentInChildren<CountAnimation>().actNow)
					GetComponentInChildren<CountAnimation>().Count();
			}else{
				endedAnimation = true;
			}

		}

		if(endedAnimation && !actWhenEnabled)
			enabled = false;

	}

}
