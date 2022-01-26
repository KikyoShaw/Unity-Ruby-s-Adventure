using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//播放音乐音效

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance { get; private set; }

	private AudioSource audioSource;

	void Awake()
	{
		instance = this;
		audioSource = GetComponent<AudioSource>();
	}

	//播放音效文件
	public void AudioPlay(AudioClip clip)
	{
		audioSource.PlayOneShot(clip);
	}

	public void AudioMovePlay(AudioClip clip)
	{
		if (audioSource.isPlaying)
			return;

		audioSource.PlayOneShot(clip);
	}
}
