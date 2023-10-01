using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class multiadmanager : MonoBehaviour
{
    private static multiadmanager _instance;
    public static multiadmanager Instance
    {
        get
        {
            return _instance;
        }
    }
    public MultiStoresManager multiStoresManager;
    public Googleads googleads;
    public string googleAdId;
    public huwawiads hwawiads;
    public string hwawiAdId;

    public enum Adtype { Exit, revive, nextevel, restart, rewared }
    public Adtype adtype;

    public Text toast;
    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
            adinit();
        }
    }
    void adinit()
    {
        switch (multiStoresManager.store)
        {
            case MultiStoresManager.Store.Google:
                googleads.adUnitId = googleAdId;
                googleads.init();
                break;
            case MultiStoresManager.Store.Huwaui:
                hwawiads.REWARD_AD_ID = hwawiAdId;
                hwawiads.InitRewardedAds();
                break;
            default:
                break;
        }
    }
    public void playad()
    {
        switch (multiStoresManager.store)
        {
            case MultiStoresManager.Store.Google:
                googleads.UserChoseToWatchAd();
                break;
            case MultiStoresManager.Store.Huwaui:
                hwawiads.ShowRewardedAd();
                break;
            default:
                break;
        }
    }
    public void adreward()
    {
        switch (adtype)
        {
            case Adtype.revive:
                FindObjectOfType<PlayerController>().resume();
                break;
            case Adtype.Exit:
                break;
            case Adtype.nextevel:
                break;
            case Adtype.restart:
                break;
        }
    }
    public void adskipped()
    {
        switch (adtype)
        {
            case Adtype.revive:
                showToast("Poor Internet", 4);
                break;
            case Adtype.Exit:
                break;
            case Adtype.nextevel:
                break;
            case Adtype.restart:
                break;

        }
    }
    public void onAdloaded()
    {

    }
    ///show toast///
    void showToast(string text, int duration)
    {
        StartCoroutine(showToastCOR(text, duration));
    }
    private IEnumerator showToastCOR(string text, int duration)
    {
        Color orginalColor = toast.color;

        toast.text = text;
        toast.enabled = true;

        //Fade in
        yield return fadeInAndOut(toast, true, 0.5f, orginalColor);

        //Wait for the duration
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return null;
        }

        //Fade out
        yield return fadeInAndOut(toast, false, 0.5f, orginalColor);

        toast.enabled = false;
        toast.color = orginalColor;
    }

    IEnumerator fadeInAndOut(Text targetText, bool fadeIn, float duration, Color originalcolor)
    {
        //Set Values depending on if fadeIn or fadeOut
        float a, b;
        if (fadeIn)
        {
            a = 0f;
            b = 1f;
        }
        else
        {
            a = 1f;
            b = 0f;
        }

        Color currentColor = originalcolor;
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);

            targetText.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;
        }
    }
}
