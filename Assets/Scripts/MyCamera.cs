using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    private GameObject tankObj;
    private Vector3 distance;
    private Tank tank;
    // 相机跟随坦克移动到第二个平台的位置，需要旋转一次
    private bool isRotate = false;
    // Start is called before the first frame update
    void Start()
    {
        tankObj = GameObject.Find("Tank");
        tank = tankObj.GetComponent<Tank>();
        distance = transform.position - tankObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(tankObj.transform.position + distance, transform.position, Time.deltaTime);
        transform.LookAt(tankObj.transform.position);

    }
}
