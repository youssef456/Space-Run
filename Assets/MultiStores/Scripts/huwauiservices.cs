using HmsPlugin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class huwauiservices : MonoBehaviour
{

    public AccountManager accountmanager;
    public GameManager gamemanager;
    public LeaderboardManager Leaderboardmanager;
    private static string leaderboardid = "DAEDA6FE4B6AFFA1DB92072973C77741E7CB2D3105AFE9FE12A6EF02487F70EE";

    public void init()
    {

        accountmanager.SignIn();

        enableservices();
        
    } 
    public void enableservices()
    {
        if (accountmanager.HuaweiId != null)
        {
            gamemanager.Init();
        }
        else
        {
            accountmanager.SignIn();

        }

    }
    public void openleaderboard()
    {
        if (accountmanager.HuaweiId != null)
        {
            Leaderboardmanager.ShowLeaderboards();

        }
        else
        {
            accountmanager.SignIn();
            enableservices();
        }

    }
    public void submitscore(int score)
    {
        if (accountmanager.HuaweiId != null)
        {
            Leaderboardmanager.SubmitScore(leaderboardid, score);

        }
        else
        {
            accountmanager.SignIn();
            enableservices();
        }
    }

}
