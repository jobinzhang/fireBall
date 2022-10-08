using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject bulletPos; // 子弹发射位置
    private float lastShotTime;
    private float pressFireButtonTime;
    private Tower tower; // 砖塔脚本

    // Start is called before the first frame update
    void Start()
    {
        tower = GameObject.FindGameObjectWithTag("tower").GetComponent<Tower>();
    }

    // Update is called once per frame
    void Update()
    {
        fire();
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
