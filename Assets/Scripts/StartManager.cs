using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public static int nomberoflevels = 5;
    int currentlevel = 1;

    [System.Serializable]
    public class Levels
    {
        public GameObject locked;

    }
    public Levels[] level;

    public GameObject loadingscreen;
    public Image slider;
    public Text progresstext;

    void Start()
    {
        CheckLockedLevels();
    }
    public void Selectlevel(int level)
    {

        int lvl = PlayerPrefs.GetInt("level:" + level);
        if (lvl <= currentlevel)
        {
            LoadLevel("Level" + level);
        }
    }
    void CheckLockedLevels()
    {
        for (int j = 0; j < nomberoflevels; j++)
        {
            int levelIndex = (j + 1);
            if ((PlayerPrefs.GetInt("level:" + levelIndex.ToString())) == 1)
            {
                currentlevel = levelIndex;
                level[levelIndex].locked.SetActive(false);
            }
        }
    }
    public void quickPlay()
    {
        LoadLevel("Level" + currentlevel);

    }
    public void LoadLevel(string scenename)
    {
        StartCoroutine(loadasync(scenename));
    }
    IEnumerator loadasync(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        loadingscreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.fillAmount = progress;
            float app = progress * 100f;
            progresstext.text = Mathf.Round(app) + " %";
            yield return null;
        }
    }
}
