using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敌人的控制脚本
public class EnemyController : MonoBehaviour
{
    //移动速度
    public float speed = 3f;
    //是否垂直方向运动
    public bool isVertical;
    //移动方向
    private Vector2 moveDirection;
    //改变方向的时间
    public float changeDirectionTime = 2f;
    //改变方向计时器
    private float changeTimer;
    //刚体组件
    private Rigidbody2D rbody;
    //动画组件
    private Animator anim;

	public ParticleSystem brokenEffect; //损坏特效

    private bool isFixed; //是否被修复了

	public AudioClip fixedClip; //修复音效

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //如果是垂直移动，那么向上移动，否则向右移动
        moveDirection = isVertical ? Vector2.up : Vector2.right;

        changeTimer = changeDirectionTime;

        isFixed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFixed)
            return;

        changeTimer -= Time.deltaTime;
        if (changeTimer < 0)
		{
            moveDirection *= -1;
            changeTimer = changeDirectionTime;
		}

        Vector2 position = rbody.position;
        position.x += moveDirection.x * speed * Time.fixedDeltaTime;
        position.y += moveDirection.y * speed * Time.fixedDeltaTime;
        rbody.MovePosition(position);

        anim.SetFloat("moveX", moveDirection.x);
        anim.SetFloat("moveY", moveDirection.y);
    }

    //与玩家的碰撞检测
    void OnCollisionEnter2D(Collision2D other)
	{
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if(pc != null)
		{
            pc.ChangeHealth(-1);
		}
	}

	//敌人被修复
	public void Fixed()
	{
		if (brokenEffect.isPlaying)
		{
			brokenEffect.Stop();
		}
		//禁用物理
		rbody.simulated = false;
		//播放被修复的动画
		anim.SetTrigger("fix");
        AudioManager.instance.AudioPlay(fixedClip);
        isFixed = true;
	}
}
