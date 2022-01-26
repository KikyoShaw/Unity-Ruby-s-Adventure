using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//草莓被玩家碰撞时相关类

public class Collectible : MonoBehaviour
{

    public ParticleSystem collectEffect; //食用草莓特效

    public AudioClip collectClip; //音效

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //碰撞检测相关
    void OnTriggerEnter2D(Collider2D other)
	{
        PlayerController pc = other.GetComponent<PlayerController>();
        if(pc != null)
		{
            if (pc.MyMaxHealth > pc.MyCurrentHealth)
            {
                pc.ChangeHealth(1);

                Instantiate(collectEffect, transform.position, Quaternion.identity);
                AudioManager.instance.AudioPlay(collectClip);
                Destroy(this.gameObject);
            }
            Debug.Log("玩家碰到了草莓");
		}
	}
}
