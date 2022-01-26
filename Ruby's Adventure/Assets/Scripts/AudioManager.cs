using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����������Ч

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance { get; private set; }

	private AudioSource audioSource;

	void Awake()
	{
		instance = this;
		audioSource = GetComponent<AudioSource>();
	}

	//������Ч�ļ�
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
