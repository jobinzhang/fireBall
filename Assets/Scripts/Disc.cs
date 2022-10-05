using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Disc : MonoBehaviour
{
    public Material archMat;
    // 圆环数量
    public int archNumber = 3;
    // 基准圆环速度，默认一秒90度
    public float baseArchSpeed = 90f;
    // 最终计算的圆环速度
    private float finalArchSpeed = 90f;

    // Start is called before the first frame update
    void Start()
    {
        initArch();
    }

    private void initArch()
    {
        int avgAngle = 360 / archNumber;
        for (int i = 0; i < archNumber; i++) {
            createArch(8, Random.Range(40, 80), avgAngle * i);
        }
        // 开启协定程序时改变圆环转向
        InvokeRepeating("changeArchSpeed", 3, 3);
    }

    private void changeArchSpeed() {
        // 计算随机转动角度
        finalArchSpeed = Random.Range(-3, 3f) * this.baseArchSpeed;
    }

    private void createArch(int radius, int angle, int beginAngle)
    {
        // 生成圆环
        ProBuilderMesh archProBM = ShapeGenerator.GenerateArch(PivotLocation.FirstVertex, angle, radius, 0.5f, 1, 20, true, true, true, true, true);
        // 设置圆环材质
        archProBM.GetComponent<MeshRenderer>().material = archMat;
        Transform archTransfrom = archProBM.GetComponent<Transform>();
        archTransfrom.TransformVector(8f, 0, 0);
        archTransfrom.Rotate(-90f, 0, 0);

        // 圆环的父对象，使圆环能够围绕平台中心转
        GameObject yRig = new GameObject("yRig");
        archTransfrom.SetParent(yRig.transform, false);
        // 设置圆环的初始角度位置， 旋转圆环的父节点
        yRig.transform.Rotate(0, beginAngle, 0);

        // 统一设置圆盘为父节点管理
        yRig.transform.SetParent(this.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        // 圆盘disc对象开始旋转
         this.transform.Rotate(0, finalArchSpeed * Time.deltaTime, 0);
    }
}
