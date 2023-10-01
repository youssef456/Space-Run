using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SocialPlatforms;
using System.Runtime.Serialization.Formatters.Binary;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System.IO;
using GooglePlayGames.BasicApi.SavedGame;

public class playservice : MonoBehaviour
{

    public enum GameData{ Save,load};
    GameData gameData;

    Texture2D saveGameCover;
    public void initgoogle()
    {
        //initialization
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            // enables saving game progress.
            .EnableSavedGames()
            // requests the email address of the player be available.
            // Will bring up a prompt for consent.
            ///.RequestEmail()
            // requests a server auth code be generated so it can be passed to an
            //  associated back end server application and exchanged for an OAuth token.
            ///.RequestServerAuthCode(false)
            // requests an ID token be generated.  This OAuth token can be used to
            //  identify the player to other services such as Firebase.
            ///.RequestIdToken()
            .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

        Authinticate();

        if (!ES3.KeyExists("haidha", ".dted.sf"))
        {
            ES3.Save("haidha", Social.localUser.id, ".dted.sf");
            
        }
        
    }
    public void Authinticate()
    {
        //Login
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {
            // handle results

        });

    }
    public void showleaderboardui()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            Social.ShowLeaderboardUI();
            //PlayGamesPlatform.Instance.ShowLeaderboardUI("Cfji293fjsie_QA");

        }
        else
        {
            Authinticate();
        }


    }
    public void reportscore(int score)
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            // post score 12345 to leaderboard ID "Cfji293fjsie_QA")
            Social.ReportScore(score, "Cfji293fjsie_QA", (bool success) =>
            {
                // handle success or failure

            });
        }
        else
        {
            Authinticate();
            if (PlayGamesPlatform.Instance.IsAuthenticated())
            {
                reportscore(score);
            }
        }
    }

    //////////////////////////////////////////Save game///////////////////////////////
    public void save()
    {
        gameData = GameData.Save;
        ShowSelectUI();

    }
    public void load()
    {
        gameData = GameData.load;
        ShowSelectUI();

    }
    /// ////////////////////////////////////////////////
    public void ShowSelectUI()
    {
        uint maxNumToDisplay = 5;
        bool allowCreateNew = false;
        bool allowDelete = true;

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ShowSelectSavedGameUI("Select saved game",
            maxNumToDisplay,
            allowCreateNew,
            allowDelete,
            OnSavedGameSelected);
    }

    public void OnSavedGameSelected(SelectUIStatus status, ISavedGameMetadata game)
    {
        if (status == SelectUIStatus.SavedGameSelected)
        {
            // handle selected game save
            OpenSavedGame(game.Filename);
            
        }
        else
        {
            // handle cancel or error
        }
    }
    void OpenSavedGame(string filename)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
    }

    public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
            switch (gameData)
            {
                case GameData.Save:
                    saveGameCover = getScreenshot();
                    SaveGame(game, ES3.LoadRawBytes(".dted.sf"),TimeSpan.FromMinutes(30));
                    break;
                case GameData.load:
                    LoadGameData(game);

                    break;
            }
        }
        else
        {
            // handle error
        }
    }
    void SaveGame(ISavedGameMetadata game, byte[] savedData, TimeSpan totalPlaytime)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        builder = builder
            .WithUpdatedPlayedTime(totalPlaytime)
            .WithUpdatedDescription("Saved game at " + DateTime.Now);
       if (saveGameCover != null)
       {
            // This assumes that savedImage is an instance of Texture2D
            // and that you have already called a function equivalent to
            // getScreenshot() to set savedImage
            // NOTE: see sample definition of getScreenshot() method below
            byte[] pngData = saveGameCover.EncodeToPNG();
            builder = builder.WithUpdatedPngCoverImage(pngData);
        }
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
    }

    public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
        }
        else
        {
            // handle error
        }
    }

    
    void LoadGameData(ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);

    }

    public void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle processing the byte array data
            ES3.SaveRaw(data, ".dted.sf", ES3Settings.defaultSettings);

        }
        else
        {
            // handle error
        }
    }
    public Texture2D getScreenshot()
    {
        // Create a 2D texture that is 1024x700 pixels from which the PNG will be
        // extracted
        Texture2D screenShot = new Texture2D(1024, 700);

        // Takes the screenshot from top left hand corner of screen and maps to top
        // left hand corner of screenShot texture
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, (Screen.width / 1024) * 700), 0, 0);
        return screenShot;
    }
}
