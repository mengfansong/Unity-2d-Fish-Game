using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    float m_moveSpeed = 10.0f;

    //创建子弹实例
    public static Fire Create(Vector3 pos,Vector3 angle)
    {
        //读取子弹Sprite Prefab
        GameObject prefab = Resources.Load<GameObject>("fire");
        //创建子弹 
        GameObject fireSprite = (GameObject)Instantiate(prefab, pos, Quaternion.Euler(angle));
        Fire f = fireSprite.AddComponent<Fire>();
        Destroy(fireSprite, 2.0f);
        return f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //更新子弹位置
        this.transform.Translate(new Vector3(0, m_moveSpeed * Time.deltaTime, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Fish f = collision.GetComponent<Fish>();
        if (f == null)
        {
            return;
        }
        else //命中了鱼
        {
            f.SetDamage(1);
        }

        Destroy(this.gameObject);
    }
}
