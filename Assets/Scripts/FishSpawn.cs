using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawn : MonoBehaviour
{
    //生成计时器
    public float timer = 0;
    //最大生成数量
    public int max_fish = 30;
    //当前鱼的数量
    public int fish_count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            //重新计时
            timer = 2.0f;

            //如果鱼的数量达到最大值
            if (fish_count >= max_fish)
            {
                return;
            }

            //随机生成不同种类的鱼
            int index = 1 + (int)(Random.value * 3.0f);
            if (index > 3)
            {
                index = 3;
            }

            //更新鱼的数量
            fish_count++;

            //读取鱼的prefab
            GameObject fishPrefab = (GameObject)Resources.Load("fish" + index);

            float cameraz = Camera.main.transform.position.z;

            //鱼的初始随机位置
            Vector3 randPos = new Vector3(Random.value, Random.value, -cameraz);
            randPos = Camera.main.ViewportToWorldPoint(randPos);

            //鱼的随机初始方向
            Fish.Target target = Random.value > 0.5f ? Fish.Target.Right : Fish.Target.Left;
            
            Fish f = Fish.Create(fishPrefab, target, randPos);

            //注册鱼的死亡信息
            f.OnDeath += OnDeath;
        }
    }

    void OnDeath(Fish fish)
    {
        //更新鱼的数量
        fish_count--;
    }
}
