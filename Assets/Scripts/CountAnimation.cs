//----------------------------------------------
//           	   Highway Racer
//
// Copyright © 2014 - 2017 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountAnimation : MonoBehaviour {

	private Text text;
	public string originalText;

	public float originalValue = 0f;
	public float targetValue = 0f;

	public bool actNow = false;
	public bool endedAnimation = false;

	public AudioSource countingAudioSource;
	public AudioClip countingAudioClip;

	void Awake ()
    {
        countingAudioSource.clip = countingAudioClip;
        countingAudioSource.loop = false;
		text = GetComponent<Text>();
		originalText = text.text;

	}

	public void GetNumber(){

		originalValue = float.Parse(text.text, System.Globalization.NumberStyles.Number);
		text.text = "0";

	}

	public void Count () {

        actNow = true;

	}

	void Update () {

		if(!actNow || endedAnimation)
			return;

		if(countingAudioSource && !countingAudioSource.isPlaying)
			countingAudioSource.Play();

		targetValue = Mathf.MoveTowards(targetValue, originalValue, Time.unscaledDeltaTime * 50f);

		text.text = targetValue.ToString("F0");

		if((originalValue - targetValue) <= .05f){

			if(countingAudioSource && countingAudioSource.isPlaying)
				countingAudioSource.Stop();

			text.text = originalValue.ToString("F0");

			if(GetComponentInParent<ButtonSlideAnimation>())
				GetComponentInParent<ButtonSlideAnimation>().endedAnimation = true;
			
			endedAnimation = true;

		}

		if(endedAnimation)
			enabled = false;
	
	}

}
