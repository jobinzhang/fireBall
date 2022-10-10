using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tank : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bulletPos; // 子弹发射位置
    private float lastShotTime;
    private Tower tower1; // 砖塔脚本
    private Tower tower2;
    private Transform midPos; // 坦克移动的中间位置
    private Transform endPos; // 坦克移动的最终位置
    public bool isPassStartPos = false; // 坦克是否已经到达第一个位置
    private bool isTankRota = false; // 坦克移动到中间是，是否已经旋转
    public bool isToEndPos = false; // 坦克是否达到最后的位置
    private bool isFireTower2 = false; // 开始打第二个砖塔


    // Start is called before the first frame update
    void Start()
    {
        tower1 = GameObject.FindGameObjectWithTag("tower1").GetComponent<Tower>();
        tower2 = GameObject.FindGameObjectWithTag("tower2").GetComponent<Tower>();
        midPos = GameObject.Find("TankMidPos").transform;
        endPos = GameObject.FindGameObjectWithTag("endPos").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // 射击
        fire();
    }

    /**
     * 判断砖块是否打完，坦克移动到下一个位置
     */
    internal void isBrickFiredAndMove()
    {
        // 如果砖块已经打完，则坦克移动到下一个平台位置
        if (tower1.brickList.Count == 0)
        {
            StartCoroutine(moveTankPos());
        }
    }

    IEnumerator moveTankPos()
    {
        while (!isToEndPos)
        {
            if (!isPassStartPos)
            {
                moveStartPos();
            }
            else
            {
                moveEndPos();
            }
            yield return null;
        }
        isFireTower2 = true;
        yield break;

    }

    void moveStartPos()
    {
        
        if (this.transform.position == midPos.position) // 往第一个点移动
        {
            isPassStartPos = true;
            return;
        }
        this.transform.position = Vector3.MoveTowards(this.transform.position, midPos.position, 10 * Time.deltaTime);

    }

    // 坦克往第二个平台的位置移动
    void moveEndPos()
    {
        //
        if (!isTankRota) {
            this.transform.Rotate(0, -25, 0);
            isTankRota = true;
            return;
        }

        if (transform.position.x == endPos.position.x) // 是否移动到第二个平台射击位置
        {
            isToEndPos = true;
            return;
        }
        this.transform.position = Vector3.MoveTowards(this.transform.position, endPos.position, 10 * Time.deltaTime);
    }

    // 发射子弹
    private void fire()
    {
        // print(isFireTower2);
        if (!isFireTower2)
        {
            fireTower(tower1); // 打第一块砖塔
        }
        else
        {
            fireTower(tower2); // 打第二块砖塔
        }
    }

    void fireTower(Tower tower)
    {
        // 如果塔块正在上升 or 砖块都已经打完，则禁止射击
        if (tower.isBrickRising || tower.brickList.Count == 0)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) && (Time.time - lastShotTime) > 0.2) // 按下鼠标左键
        {
            GameObject bullet = Instantiate(bulletPrefab);
            // 根据鼠标点击评率，提高子弹发射的音调
            risePitch(bullet);
            bullet.transform.SetParent(bulletPos.transform, false);
            bullet.transform.position = bulletPos.transform.position;
            // 子弹往前，速度20
            Vector3 forward = bulletPos.transform.forward * 25;
            // 给子弹施加一个往前的力
            bullet.GetComponent<Rigidbody>().AddForce(forward, ForceMode.Impulse);
            lastShotTime = Time.time;
            // 2秒后销毁子弹
            Destroy(bullet, 2f);
        }
    }

    // 根据鼠标点击评率，提高子弹发射的音调
    private void risePitch(GameObject bullet)
    {
        float timeInterval = 2 / (Time.time - lastShotTime);
        if (timeInterval > 2) {
            timeInterval = 2;
        }
        bullet.GetComponent<AudioSource>().pitch = 1 + timeInterval;
    }
}
