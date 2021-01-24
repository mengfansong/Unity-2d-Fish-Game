using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    //射击计时器
    float m_shootTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void UpdateInput()
    {
        m_shootTimer -= Time.deltaTime;
        //获得鼠标位置
        Vector3 ms = Input.mousePosition;
        ms = Camera.main.ScreenToWorldPoint(ms);

        //大炮的位置
        Vector3 mypos = this.transform.position;

        //计算鼠标指针的角度
        Vector2 targetDir = ms - mypos;
        float angle = Vector2.Angle(targetDir, Vector3.up);
        if (ms.x > mypos.x)
        {
            angle = -angle;
        }
        this.transform.eulerAngles = new Vector3(0, 0, angle);

        //单机左键开火
        if (Input.GetMouseButtonDown(0))
        {
            

            if (m_shootTimer <= 0)
            {
                m_shootTimer = 0.1f;//射击cd
                //开火
                Fire.Create(this.transform.TransformPoint(0, 1, 0), new Vector3(0, 0, angle));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
    }
}
