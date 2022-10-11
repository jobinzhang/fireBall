using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailUI : MonoBehaviour
{
    private Button restartBtn;
    private Button exitBtn;
    private GameObject fail;
    private int gameResult = 1; // 游戏闯关结果，1-失败，2-成功
    // Start is called before the first frame update
    void Start()
    {
        gameResult = PlayerPrefs.GetInt("gameResult");
        restartBtn = GameObject.Find("RestartBtn").GetComponent<Button>();
        exitBtn = GameObject.Find("ExitBtn").GetComponent<Button>();
        fail = GameObject.Find("Fail");
        initUiComponent();
        restartBtn.onClick.AddListener(restartGame);
        exitBtn.onClick.AddListener(exitGame);
    }

    // 根据游戏结果，初始化先关组件显示
    void initUiComponent()
    {

        // 游戏闯关成功
        if (gameResult == 2)
        {
            fail.GetComponent<Image>().sprite = Resources.Load<Sprite>("Image/success");
        }
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
