using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tank : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bulletPos; // 子弹发射位置
    private float lastShotTime;
    private Tower tower; // 砖塔脚本
    private Transform startPos;
    private Transform endPos;
    private bool isPassStartPos = false; // 坦克是否已经到达第一个位置
    private bool isTankRota = false; // 坦克移动到中间是，是否已经旋转


    // Start is called before the first frame update
    void Start()
    {
        tower = GameObject.FindGameObjectWithTag("tower").GetComponent<Tower>();
        startPos = GameObject.FindGameObjectWithTag("startPos").transform;
        endPos = GameObject.FindGameObjectWithTag("endPos").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // 射击
        fire();
        // 判断是否需要移动到下一个平台
        // moveNextPlatform();
        moveNextPlatform2();
    }

    void moveNextPlatform2()
    {
        // 如果砖块已经打完，则坦克移动到下一个平台位置
        //if (tower.brickList.Count != 0)
        //{
        //    return;
        //}
        if (!isPassStartPos)
        {
            moveStartPos();
        }
        else {
            moveEndPos();
        }
    }


    void moveStartPos()
    {
        float distance = Vector3.Distance(transform.position, startPos.position);
        print(distance);
        if (distance < 10.1) // 往第一个点移动
        {
            isPassStartPos = true;
            return;
        }
        this.transform.position = Vector3.MoveTowards(this.transform.position, startPos.localPosition, Time.deltaTime);
        //this.transform.Translate(0, 0, 2* Time.deltaTime);

    }

    void moveEndPos()
    {
        //
        if (!isTankRota) {
            this.transform.Rotate(0, -25, 0);
            isTankRota = true;
            return;
        }
        this.transform.position = Vector3.MoveTowards(this.transform.position, endPos.position, 2 * Time.deltaTime);
        
    }

    // 发射子弹
    private void fire()
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
