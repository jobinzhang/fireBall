using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailUI : MonoBehaviour
{
    private Button restartBtn;
    private Button exitBtn;
    // Start is called before the first frame update
    void Start()
    {
        restartBtn = GameObject.Find("RestartBtn").GetComponent<Button>();
        exitBtn = GameObject.Find("ExitBtn").GetComponent<Button>();

        restartBtn.onClick.AddListener(restartGame);
        exitBtn.onClick.AddListener(exitGame);
    }

    void restartGame()
    {
        print("restartGame");
        SceneManager.LoadScene("MainGameScene");
    }

    void exitGame()
    {
        print("exitGame");
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
