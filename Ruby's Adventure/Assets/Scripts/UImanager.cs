using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//UI 管理相关

public class UImanager : MonoBehaviour
{
	public static UImanager instance { get; private set; }

	void Awake()
	{
		instance = this;
	}

	public Image healthBar; //角色的血条

	public Text bulletCountText; //子弹数量显示

	public Text robotCountText; //机器人数量显示

	private int maxRobotCount = 10; //最大机器人数

	private int curRobotCount = 0; //当前机器人数

	//更新血条
	public void UpdateHealthBar(int curAmount, int maxAmout)
	{
		healthBar.fillAmount = (float)curAmount / maxAmout;
	}

	//更新子弹数量显示
	public void UpdateBulletCount(int cutAmount, int maxAmount)
	{
		bulletCountText.text = cutAmount.ToString() + "/" + maxAmount.ToString();
	}

	//更新机器人信息个数
	public void UpdateRobotCount(int amount)
	{
		curRobotCount = Mathf.Clamp(curRobotCount + amount, 0, maxRobotCount);
		robotCountText.text = curRobotCount.ToString() + "/" + maxRobotCount.ToString();
		if (curRobotCount == maxRobotCount) //代表任务完成
			CanvasManager.instance.ShowFinshTaskTip("谢谢你，冒险家，你帮助我修复了所有失控的机器人！");
	}

}
