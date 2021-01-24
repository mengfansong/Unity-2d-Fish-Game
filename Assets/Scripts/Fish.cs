using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    
    protected float m_moveSpeed = 2.0f;     //鱼的游动速度
    protected int m_life = 1;             //生命值

    //移动方向
    public enum Target
    {
        Left=0,
        Right=1
    }
    public Target m_target = Target.Right;  //当前移动方向
    public Vector3 m_targetPosition;        //目标位置

    public delegate void VoidDelegate(Fish fish);
    public VoidDelegate OnDeath;

    //静态函数，创建一个Fish实例
    public static Fish Create(GameObject prefab,Target target,Vector3 pos)
    {
        GameObject go = (GameObject)Instantiate(prefab, pos, Quaternion.identity);
        Fish fish = go.AddComponent<Fish>();
        fish.m_target = target;
        return fish;
    }

    //受到伤害
    public void SetDamage(int damage)
    {
        m_life -= damage;
        if (m_life <= 0)
        {
            GameObject prefab = Resources.Load<GameObject>("explosion");
            GameObject explosion = (GameObject)Instantiate(prefab, this.transform.position, this.transform.rotation);   //鱼死亡后爆炸
            Destroy(explosion, 1.0f);       //1s后爆炸结束
            OnDeath(this);      //发布死亡消息
            Destroy(this.gameObject);       
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        SetTarget();
    }

    //设置移动目标
    void SetTarget()
    {
        float rand = Random.value;
        Vector3 scale = this.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (m_target == Target.Right ? 1 : -1);
        this.transform.localScale = scale;

        float cameraz = Camera.main.transform.position.z;
        m_targetPosition = Camera.main.ViewportToWorldPoint(new Vector3((int)m_target, 1 * rand, -cameraz));
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        Vector3 pos = Vector3.MoveTowards(this.transform.position, m_targetPosition, m_moveSpeed * Time.deltaTime);
        if (Vector3.Distance(pos, m_targetPosition) < 0.1f)
        {
            m_target = m_target == Target.Left ? Target.Right : Target.Left;
            SetTarget();
        }

        this.transform.position = pos;
    }
}
