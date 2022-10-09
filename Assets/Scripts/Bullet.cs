using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Arch")
        {
            collisionArch();
        }
        if (collision.gameObject.tag == "Brick")
        {
            collisionBrick(collision);
        }
    }

    // 打到圆环
    private void collisionArch()
    {
        // 销毁子弹
        Destroy(this.gameObject);
        // 跳转到失败场景
        SceneManager.LoadScene("FailScene");
    }

    // 打到砖块
    private void collisionBrick(Collision brick)
    {
        // 调用砖塔的处理子弹打到砖块的方法
        brick.transform.parent.GetComponent<Tower>().handleBulletFire();
        // 销毁子弹
        Destroy(this.gameObject);
    }
}
