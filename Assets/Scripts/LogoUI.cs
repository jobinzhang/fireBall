using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoUI : MonoBehaviour
{
    private GameObject alphaObj;
    private GameObject logoObj;
    private float baseLerpSpeed;
    // Logo是否加载完成，加载完之后再执行砖块上升动作
    internal bool isLogoLoaded = false;
    private Button startBtn;
    // Start is called before the first frame update
    void Start()
    {
        alphaObj = GameObject.FindGameObjectWithTag("AlphaObj");
        logoObj = GameObject.FindGameObjectWithTag("LogoObj");
        startBtn = GameObject.Find("StartBtn").GetComponent<Button>();
        startBtn.onClick.AddListener(toMainGameScene);
        StartCoroutine(logoUI());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void toMainGameScene()
    {
        print("chargeToMainScene");
        SceneManager.LoadScene("MainGameScene");
    }

    // 首页logo的ui展示
    IEnumerator logoUI()
    {
        CanvasGroup canvas = alphaObj.GetComponent<CanvasGroup>();
        while (canvas.alpha <= 0.95)
        {
            float lerp = Mathf.Lerp(canvas.alpha, 1, baseLerpSpeed);
            canvas.alpha = lerp;
            baseLerpSpeed += (Time.deltaTime / 100);
            yield return null;
        }
        print("logoUI is ok...");
    }
}
