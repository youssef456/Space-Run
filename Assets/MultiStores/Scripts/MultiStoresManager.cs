using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

public class MultiStoresManager : MonoBehaviour
{
    private static MultiStoresManager _instance;
    public static MultiStoresManager Instance
    {
        get
        {
            return _instance;
        }
    }
    //Packege Name
    public string PackageName;
    //Google
    public playservice googleplayservices;
    //huawui
    public huwauiservices huwauiservices;
    public enum Store{None, Google ,Huwaui};
    public Store store;
    void Start()
    {

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            init();
        }

    }
    void init()
    {
        switch (store)
        {
            case Store.Google:
                googleplayservices.gameObject.SetActive(true);
                googleplayservices.initgoogle();
                break;
            case Store.Huwaui:
                huwauiservices.gameObject.SetActive(true);
                huwauiservices.init();
                break;
            default:
                break;
        }
    }
    public void showleaderboard() {

        switch (store)
        {
            case Store.Google:
                googleplayservices.showleaderboardui();
                break;
            case Store.Huwaui:
                //huwauiservices.openleaderboard();
                break;
            default:
                break;
        }
    }
    public void ReportScore(int score) {

        switch (store)
        {
            case Store.Google:
                googleplayservices.reportscore(score);
                break;
            case Store.Huwaui:
                huwauiservices.submitscore(score);
                break;
            default:
                break;
        }
    }
    public void SaveGame() {

        switch (store)
        {
            case Store.Google:
                googleplayservices.save();
                break;
            case Store.Huwaui:
                
            default:
                break;
        }
    }
    public void LoadGame() {

        switch (store)
        {
            case Store.Google:
                googleplayservices.load();
                break;
            case Store.Huwaui:

            default:
                break;
        }
    }
}
