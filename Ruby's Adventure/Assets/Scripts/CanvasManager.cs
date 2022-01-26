using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//场景脚本

public class CanvasManager : MonoBehaviour
{

	public static CanvasManager instance { get; private set; }

	public GameObject robotCountTip; //机器人提示框

	public GameObject bulletCountTip; //子弹提示框

	public GameObject finshTaskTip; //任务完成提示框

	public Text finshTaskText; //任务完成提示框文本内容

	public float showTime = 5; //任务完成提示框显示时间

	private float showTimer; //任务完成提示框显示计时器

	// Start is called before the first frame update
	void Awake()
	{
		instance = this;
		//初始默认隐藏
		robotCountTip.SetActive(false);
		bulletCountTip.SetActive(false);
		finshTaskTip.SetActive(false);
		showTimer = -1;
	}

    // Update is called once per frame
    void Update()
    {
		showTimer -= Time.deltaTime;
		if (showTimer < 0)
		{
			finshTaskTip.SetActive(false);
		}
	}

	//显示对话框
	public void ShowTip()
	{
		robotCountTip.SetActive(true);
		bulletCountTip.SetActive(true);
	}

	//显示任务完成框
	public void ShowFinshTaskTip(string text)
	{
		showTimer = showTime;
		finshTaskText.text = text;
		finshTaskTip.SetActive(true);
	}
}
