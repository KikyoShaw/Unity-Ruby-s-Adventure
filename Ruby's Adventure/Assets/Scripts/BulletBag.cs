using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//弹药包操作脚本

public class BulletBag : MonoBehaviour
{
	public int bulletCount = 10; //可以增加的子弹数量

	public ParticleSystem colllectEffect; //特效

	public AudioClip collectClip; //音效

	public void OnTriggerEnter2D(Collider2D collision)
	{
		PlayerController pc = collision.GetComponent<PlayerController>();
		if(pc != null)
		{
			if(pc.MyCurBulletCount < pc.MyMaxBulletCount)
			{
				pc.ChangeBulletCount(bulletCount);
				Instantiate(colllectEffect, transform.position, Quaternion.identity);
				AudioManager.instance.AudioPlay(collectClip);
				Destroy(this.gameObject);
			}
		}
	}
}
