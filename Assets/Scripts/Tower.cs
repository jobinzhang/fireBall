using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tower : MonoBehaviour 
{
    // 塔默认高度
    public int height = 20;
    public GameObject brickPrefab;
    public Color[] colers = new Color[2];
    public List<GameObject> brickList = new List<GameObject>();
    // 砖块是否正在上升
    public bool isBrickRising = true;
    // 当前平台的圆环
    private Disc disc;
    private string towerTag;

    // Start is called before the first frame update
    void Start()
    {
        print(this.gameObject.tag);
        disc = this.transform.parent.GetComponentInChildren<Disc>();
        towerTag = this.gameObject.tag;
        initTower();
    }

    private void initTower()
    {
        for (int i = 0;i < height; i ++) {
            // 根据预制体，创建砖块
            GameObject brickGameObj = Instantiate(brickPrefab);
            brickGameObj.transform.Translate(0, i + 1, 0);
            brickGameObj.transform.Rotate(0, i * 5, 0) ;
            brickGameObj.transform.SetParent(this.transform, false);
            // 设置砖块颜色
            brickGameObj.GetComponent<MeshRenderer>().material.color = colers[i % 2];
            brickList.Add(brickGameObj);
        }
        // 开启协程，上升塔
        StartCoroutine(riseTower());
    }

    IEnumerator riseTower() {
        // 先将塔整体移动到地下
        this.transform.Translate(0, -height, 0);
        while (this.transform.localPosition.y <= -0.5)
        {
            this.transform.Translate(0, 1 * Time.deltaTime, 0);
            yield return null;
        }
        isBrickRising = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 45 * Time.deltaTime, 0);
    }

    // 子弹打到砖块处理方法
    internal void handleBulletFire()
    {
        if (brickList.Count <= 0)
        {
            disc.enabled = false;
            return;
        }
        // 获取最底下的砖块
        GameObject bottomBrick = brickList[0];
        // 设置砖块放大淡出效果
        bottomBrick.GetComponent<Brick>().setFadeOut();
        // 销毁
        Destroy(bottomBrick, 1);
        // 将最下面的去掉， 然后整体下移
        brickList.RemoveAt(0);
        for (int i = 0; i < brickList.Count; i++)
        {
            Vector3 positonV = brickList[i].transform.localPosition;
            positonV.y = positonV.y - 1;
            brickList[i].transform.localPosition = positonV;
        }
        // 判断是否胜利
        isWin();
    }

    void isWin()
    {
        
        if (brickList.Count <= 0)
        {
            // 第一块砖块全部打完， 影藏当前的圆盘
            if (towerTag == "tower1")
            {
                disc.gameObject.SetActive(false);
            } else if (towerTag == "tower2") // 第二块砖块全部打完， 则获得胜利
            {
                // 跳转到结束场景
                PlayerPrefs.SetInt("gameResult", 2);
                SceneManager.LoadScene("EndScene");
            }
        }
    }
}
