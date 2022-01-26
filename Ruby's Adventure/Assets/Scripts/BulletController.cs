using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//控制子弹的移动和碰撞

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rbody; //获取刚体组件

	//音效
	public AudioClip hitClip;

	// Start is called before the first frame update
	void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //子弹的移动
    public void Move(Vector2 moveDirection, float moveForce)
	{
        rbody.AddForce(moveDirection*moveForce);
	}

    //碰撞检测
    public void OnCollisionEnter2D(Collision2D other)
	{
        EnemyController ec = other.gameObject.GetComponent<EnemyController>();

        if(ec != null)
		{
			UImanager.instance.UpdateRobotCount(1);
            ec.Fixed();
            Debug.Log("碰到敌人了");
		}
        AudioManager.instance.AudioPlay(hitClip);
        Destroy(this.gameObject);
	}
}
