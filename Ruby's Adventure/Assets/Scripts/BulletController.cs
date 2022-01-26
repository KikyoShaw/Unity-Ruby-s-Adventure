using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����ӵ����ƶ�����ײ

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rbody; //��ȡ�������

	//��Ч
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

    //�ӵ����ƶ�
    public void Move(Vector2 moveDirection, float moveForce)
	{
        rbody.AddForce(moveDirection*moveForce);
	}

    //��ײ���
    public void OnCollisionEnter2D(Collision2D other)
	{
        EnemyController ec = other.gameObject.GetComponent<EnemyController>();

        if(ec != null)
		{
			UImanager.instance.UpdateRobotCount(1);
            ec.Fixed();
            Debug.Log("����������");
		}
        AudioManager.instance.AudioPlay(hitClip);
        Destroy(this.gameObject);
	}
}
