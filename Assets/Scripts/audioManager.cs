using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class audioManager : MonoBehaviour
{
    public AudioClip back;                                 // AudioClip : ���� ���� ����
    public AudioSource audioSource;                        // AudioSource : ���� ���� ������ �÷��� �� ���ΰ�


    //float leftTime
    //float limitTime = gameManager.I.Type


    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = back;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        string leftTimeString = gameManager.I.timeTxt.GetComponent<Text>().text;
        float leftTime = Convert.ToSingle(leftTimeString);
        if (leftTime == 10f)
        {
            FastMusicInvoke();
        }
        if (gameManager.I.isGameEnd)
        {
            audioSource.Stop();
        }

    }
    void FastMusicInvoke()
    {
        audioSource.Stop();
        audioSource.GetComponent<AudioSource>().pitch = 2.0f;
        audioSource.Play();
    }
}