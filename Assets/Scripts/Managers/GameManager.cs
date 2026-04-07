using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int score;
    public TextMeshProUGUI scoreText;
    private InputManager myInput;
    public GameObject pauseMenu;
    public bool pause = false;

    private void Awake()
    {
        instance = this;
        //UpdateScore(0);
        myInput = GetComponent<InputManager>();
    }
    public void UpdateScore(int amount)
    {
        score += amount;
        if(score<0)
        {
            score = 0;
        }
        scoreText.text = "Enemies Slain: " + score;
    }
    public void Update()
    {
        if (InputManager._pause.WasPressedThisFrame())
        {
            Pause();
        }
    }
    public void Pause()
    {
        if(!pause)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            pause = true;
        }
        else
        {
            Resume();
        }
        
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pause = false;
    }

    public void EndGame()
    {
        StartCoroutine("EndTheGame");
    }
    
    public IEnumerator EndTheGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("WinningScreen");
    }



}
