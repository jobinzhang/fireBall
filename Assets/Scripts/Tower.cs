using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tower : MonoBehaviour 
{
    // 塔默认高度
    public int height = 20;
    public GameObject brickPrefab;
    public Color[] colers = new Color[2];

    // Start is called before the first frame update
    void Start()
    {
        initTower();
    }

    private void initTower()
    {
        // 先将塔整体移动到地下
        this.transform.Translate(0, -height, 0);
        for (int i = 0;i < height; i ++) {
            // 根据预制体，创建砖块
            GameObject brickGameObj = Instantiate(brickPrefab);
            brickGameObj.transform.Translate(0, i + 1, 0);
            brickGameObj.transform.Rotate(0, i * 5, 0) ;
            brickGameObj.transform.SetParent(this.transform, false);
            // 设置砖块颜色
            brickGameObj.GetComponent<MeshRenderer>().material.color = colers[i % 2];
        }

        Debug.Log(this.transform.position.y);
        while (this.transform.position.y <= 0) {
            this.transform.Translate(0, 1, 0);
            Debug.Log(this.transform.position.y);
        }

        
        // 结束后整体往下移动0.5单位
        // this.transform.Translate(0, -0.5f, 0);
    }


    

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 45 * Time.deltaTime, 0);
    }
}
