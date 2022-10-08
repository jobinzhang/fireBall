using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRoot : MonoBehaviour
{
    private GameObject alphaObj;
    private GameObject logoObj;
    private float baseLerpSpeed;
    // Logo是否加载完成，加载完之后再执行砖块上升动作
    internal bool isLogoLoaded = false;
    // Start is called before the first frame update
    void Start()
    {
        alphaObj = GameObject.FindGameObjectWithTag("AlphaObj");
        logoObj = GameObject.FindGameObjectWithTag("LogoObj");
        StartCoroutine(logoUI());
    }

    // Update is called once per frame
    void Update()
    {
        
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
        // 等待两秒
        yield return new WaitForSeconds(1);
        logoObj.SetActive(false);
        isLogoLoaded = true;

    }
}
