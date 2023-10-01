using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerController player;
    public bool Gameispaused = false;
    public GameObject pausemenuui;
    public GameObject pausebutton;
    public GameObject resumebutton;
    public GameObject Revivebutton;

    public Text TotalScore;
    public Text TotalSteps;
    public Text TotalTime;

    public TextMeshProUGUI scoretext;

    public GameObject loadingscreen;
    public Image slider;
    public Text progresstext;
    private void Start()
    {
        Time.timeScale = 1f;
        pausemenuui.SetActive(false);
    }
    private void Update()
    {
        scoretext.text = player.score.ToString() + " / " + player.unlockMinScore.ToString();
    }
    public void resume()
    {
        pausemenuui.SetActive(false);
        Time.timeScale = 1f;
        Gameispaused = false;
        pausebutton.SetActive(true);
      
    }
    public void pause()
    {
        pausemenuui.SetActive(true);
        // freez time if zero it competly freez the game if number it slows it down
        Time.timeScale = 0f;
        Gameispaused = true;
        pausebutton.SetActive(false);
        resumebutton.SetActive(true);
        Revivebutton.SetActive(false);
      
    }
    public void Restart()
    {
        LoadLevel(SceneManager.GetActiveScene().name);

    }
    public void exit()
    {
        LoadLevel("World1");

    }
    public void deadmenu()
    {
        pausemenuui.SetActive(true);
        // freez time if zero it competly freez the game if number it slows it down
        Time.timeScale = 0f;
        pausebutton.SetActive(false);
        resumebutton.SetActive(false);
        Revivebutton.SetActive(true);
    }
    public void resumewithad()
    {
        pausemenuui.SetActive(false);
        Time.timeScale = 1f;
        pausebutton.SetActive(true);
    }
    public void WinGame()
    {
        DisplayResults(Time.timeSinceLevelLoad * player.score,player.score,Time.timeSinceLevelLoad);
    }
    public IEnumerator DisplayResults(float score, int steps, float time)
    {
        yield return new WaitForSecondsRealtime(0f);

        TotalScore.text = (score).ToString("F2");
        TotalSteps.text = (steps).ToString("F0");
        TotalTime.text = Mathf.Floor(time).ToString("F0");

        BroadcastMessage("Animate");
        BroadcastMessage("GetNumber");
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
