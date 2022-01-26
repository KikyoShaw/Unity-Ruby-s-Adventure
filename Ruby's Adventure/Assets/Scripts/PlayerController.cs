using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//控制角色移动、生命、动画等

public class PlayerController : MonoBehaviour
{
	public float speed = 5f; //移动速度

	private int maxHealth = 5; //最大生命值

	private int currentHealth; //当前生命值

	[SerializeField]
	private int maxBulletCount = 99; //最大子弹数
	private int curBulletCount; //当前子弹数

	public int MyCurBulletCount { get { return curBulletCount; } }

	public int MyMaxBulletCount { get { return maxBulletCount; } }

	public int MyMaxHealth
	{
		get { return maxHealth; }
	}

	public int MyCurrentHealth
	{
		get { return currentHealth; }
	}

	private float invincibleTime = 2f; //无敌时间2s

	private float invincibleTimer; //无敌时间 计时器

	private bool isInvincible; //是个处于无敌状态

	private Rigidbody2D rigidbody2d; //刚体组件

	public GameObject bulletPrefab; //获取子弹

	//动画组件
	private Animator anim;

	//玩家方向信息
	private Vector2 lookDirection = new Vector2(1, 0); //默认向右

	//玩家的音效
	public AudioClip healthClip; //受伤
	public AudioClip launchClip; //发射子弹
	public AudioClip moveClip; //移动声音

	//是否接取NPC任务
	private bool isGetNPCtask = false;

	//void FixedUpdate()
	//{
	//	float moveX = Input.GetAxis("Horizontal"); //控制水平移动方向 A:-1 D:1 0
	//	float moveY = Input.GetAxis("Vertical"); //控制垂直移动方向 W:1 S:-1 0

	//	Vector2 position = rigidbody2d.position;
	//	position.x += moveX * speed * Time.deltaTime;
	//	position.y += moveY * speed * Time.deltaTime;
	//	//position.x += position.x + 0.1f * moveX;
	//	//position.y += position.y + 0.1f * moveY;

	//	//transform.position = position;

	//	//反之抖动
	//	rigidbody2d.MovePosition(position);

	//	//transform.Translate(transform.right * speed * Time.deltaTime);
	//}

	// Start is called before the first frame update
	void Start()
	{
		//Application.targetFrameRate = 30;
		rigidbody2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		SpriteRenderer dog = GetComponent<SpriteRenderer>();

		currentHealth = 3;
		curBulletCount = 10;
		invincibleTimer = 0;
		UImanager.instance.UpdateHealthBar(currentHealth, maxHealth);
		UImanager.instance.UpdateBulletCount(curBulletCount, maxBulletCount);
	}

	// Update is called once per frame
	void Update()
	{
		float moveX = Input.GetAxisRaw("Horizontal"); //控制水平移动方向 A:-1 D:1 0
		float moveY = Input.GetAxisRaw("Vertical"); //控制垂直移动方向 W:1 S:-1 0

		Vector2 moveVector = new Vector2(moveX, moveY);
		if(moveVector.x != 0 || moveVector.y != 0)
		{
			lookDirection = moveVector;
			AudioManager.instance.AudioMovePlay(moveClip);
		}
		anim.SetFloat("Look X", lookDirection.x);
		anim.SetFloat("Look Y", lookDirection.y);
		anim.SetFloat("Speed", moveVector.magnitude);

		Vector2 position = rigidbody2d.position;
		//position.x += moveX * speed * Time.deltaTime;
		//position.y += moveY * speed * Time.deltaTime;

		//position.x += moveX * speed * Time.fixedDeltaTime;
		//position.y += moveY * speed * Time.fixedDeltaTime;

		position += moveVector * speed * Time.fixedDeltaTime;

		//position.x += position.x + 0.1f * moveX;
		//position.y += position.y + 0.1f * moveY;

		//transform.position = position;

		//防止抖动
		rigidbody2d.MovePosition(position);

		//无敌时间
		if (isInvincible)
		{
			invincibleTimer -= Time.deltaTime; 
			if(invincibleTimer < 0)
			{
				isInvincible = false; //2s以后取消无敌状态
			}
		}

		//检测玩家是否按下j键，并且子弹数量大于0，进行攻击
		if (Input.GetKeyDown(KeyCode.J) && curBulletCount > 0 && isGetNPCtask)
		{
			ChangeBulletCount(-1); //每次工具减去一个子弹
			anim.SetTrigger("Launch");
			AudioManager.instance.AudioPlay(launchClip);
			GameObject bullet = Instantiate(bulletPrefab, rigidbody2d.position + Vector2.up*0.5f, Quaternion.identity);
			BulletController bc = bullet.GetComponent<BulletController>();
			if(bc != null)
			{
				bc.Move(lookDirection, 300);
			}
		}

		//按下e键，进行NPC交互
		if(Input.GetKeyDown(KeyCode.E))
		{
			RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position, lookDirection, 2f, LayerMask.GetMask("NPC"));
			if(hit.collider != null)
			{
				NPCmanager npc = hit.collider.GetComponent<NPCmanager>();
				if (npc != null)
				{
					isGetNPCtask = true;
					npc.ShowDialog();
					CanvasManager.instance.ShowTip();
					Debug.Log("碰到了NPC");
				}
			}
		}

		//transform.Translate(transform.right * speed * Time.deltaTime);
	}

	//改变玩家当前生命值
	public void ChangeHealth(int amount)
	{
		//如果玩家受到伤害小于0
		if (amount < 0)
		{
			//处于无敌状态
			if(isInvincible)
				return;
			anim.SetTrigger("Hit");
			AudioManager.instance.AudioPlay(healthClip);
			isInvincible = true;
			invincibleTimer = invincibleTime;
		}

		//把玩家的生命值约束在0和最大值之间
		currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
		UImanager.instance.UpdateHealthBar(currentHealth, maxHealth);
		Debug.Log(currentHealth + "/" + maxHealth);
		if(currentHealth <= 0) //角色死亡
		{ 
			CanvasManager.instance.ShowFinshTaskTip("冒险者，你已阵亡，记得好好保护自己哦！");
			//重生在指定位置
			Vector2 position = new Vector2(2, 10);
			this.gameObject.transform.position = position;
			currentHealth = 3;
			UImanager.instance.UpdateHealthBar(currentHealth, maxHealth);
		}	
	}

	//改变子弹数量
	public void ChangeBulletCount(int amount)
	{
		curBulletCount = Mathf.Clamp(curBulletCount + amount, 0, maxBulletCount);
		UImanager.instance.UpdateBulletCount(curBulletCount, maxBulletCount);
	}
}
