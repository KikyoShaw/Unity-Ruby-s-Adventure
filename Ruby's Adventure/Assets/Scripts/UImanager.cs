using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//UI �������

public class UImanager : MonoBehaviour
{
	public static UImanager instance { get; private set; }

	void Awake()
	{
		instance = this;
	}

	public Image healthBar; //��ɫ��Ѫ��

	public Text bulletCountText; //�ӵ�������ʾ

	public Text robotCountText; //������������ʾ

	private int maxRobotCount = 10; //����������

	private int curRobotCount = 0; //��ǰ��������

	//����Ѫ��
	public void UpdateHealthBar(int curAmount, int maxAmout)
	{
		healthBar.fillAmount = (float)curAmount / maxAmout;
	}

	//�����ӵ�������ʾ
	public void UpdateBulletCount(int cutAmount, int maxAmount)
	{
		bulletCountText.text = cutAmount.ToString() + "/" + maxAmount.ToString();
	}

	//���»�������Ϣ����
	public void UpdateRobotCount(int amount)
	{
		curRobotCount = Mathf.Clamp(curRobotCount + amount, 0, maxRobotCount);
		robotCountText.text = curRobotCount.ToString() + "/" + maxRobotCount.ToString();
		if (curRobotCount == maxRobotCount) //�����������
			CanvasManager.instance.ShowFinshTaskTip("лл�㣬ð�ռң���������޸�������ʧ�صĻ����ˣ�");
	}

}
