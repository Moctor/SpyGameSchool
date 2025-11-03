using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scripts_UI : MonoBehaviour
{

    public GameObject GameLoseUI;
    public GameObject GameWinUI;
    bool gameIsOver;

    Scripts_Timer timer;
    public GameObject time;

    // Start is called before the first frame update
    void Start()
    {
        Scripts_Guard.OnPlayerSpotted += ShowGameLoseUI;
        Scirpts_RadialTrigger.OnPlayerOverlapp += ShowGameWinUI;
        timer = time.GetComponent<Scripts_Timer>();
    }

    void Update()
    {
        if (gameIsOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void ShowGameWinUI()
    {
        OnGameOver(GameWinUI);
    }

    void ShowGameLoseUI()
    {
        OnGameOver(GameLoseUI);
    }

    void OnGameOver(GameObject gameOverUI)
    {
        gameOverUI.SetActive(true);
        gameIsOver = true;
        Scripts_Guard.OnPlayerSpotted -= ShowGameLoseUI;
        Scirpts_RadialTrigger.OnPlayerOverlapp -= ShowGameWinUI;
        timer.timeIsRunning = false;
    }

}
