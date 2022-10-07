using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    private bool isFadeOut = false;
    private Vector3 fadeVector;
    // Start is called before the first frame update
    void Start()
    {
        fadeVector = this.transform.localScale * 2;
    }

    // Update is called once per frame
    void Update()
    {
        fadeOut();
    }

    // 设置淡出动画
    internal void setFadeOut()
    {
        this.isFadeOut = true;
    }

    // 淡出动作 
    private void fadeOut() 
    {
        if (!isFadeOut)
        {
            return;
        }
        // 先变大
        float f = 5f * Time.deltaTime;
        transform.localScale = Vector3.Lerp(transform.localScale, fadeVector, f);
        // 在淡出
        Color color = GetComponent<MeshRenderer>().material.color;
        color.a = Mathf.Lerp(color.a, 0f, f);
        GetComponent<MeshRenderer>().material.color = color;  
    }
}
