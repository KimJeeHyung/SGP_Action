using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Ŭ���� ��ü�� ����ȭ �Ϸ��� �� Ű���带 �Է��ؾ��Ѵ�.
public class Sound
{
    public string name; // ���� �̸�
    public AudioClip clip; // ��

}


public class SoundManager : MonoBehaviour
{
    // �̱���,Singleton, 1��.

    [SerializeField]
    private float Sound_Volume = 0.5f;

    [SerializeField]
    private float fadeDuration = 10.0f;

    private float currentTime = 0.0f;

    static public SoundManager instance;

    #region singleton
    void Awake() // ��ü ���� �� ���� ����
    {
        if (instance == null)
        { 
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this.gameObject);


    }
    #endregion

    public AudioSource[] audioSourceEffects;
    public AudioSource audioSourceBgm;

    public string[] playSoundName;

    public string playBGMSound;


    public Sound[] effectSounds;
    public Sound[] bgmSounds;

    /* void OnEnable() // �Ź� Ȱ��ȭ �� ������ ���� (�ڷ�ƾ�� ����(ȣ��) �Ұ���)
      {

      }

      void Start() // ���� 1ȸ ȣ��.(�ڷ�ƾ ����(ȣ��) ����)
      {

      }*/

    private void Start()
    {
        playSoundName = new string[audioSourceEffects.Length];
    }

    public IEnumerator PlayBGM(string _name)
    {    
        for (int i = 0; i < bgmSounds.Length; ++i)
        {
            if (_name == bgmSounds[i].name)
            {        
                audioSourceBgm.clip = bgmSounds[i].clip;
                playBGMSound = bgmSounds[i].name;                
                audioSourceBgm.volume = 0.0f;
                audioSourceBgm.Play();               
                break;   
            }
        }

         currentTime = 0.0f;

        while (audioSourceBgm.volume <= Sound_Volume)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / fadeDuration;
            audioSourceBgm.volume = Mathf.Lerp(0f, Sound_Volume, t);
            yield return null;
        }
     }

    

   /* public void PlayBGM(string _name)
    {
        for(int i = 0; i < bgmSounds.Length; ++i)
        {
            if (_name == bgmSounds[i].name)
            {
                if (!audioSourceBgm.isPlaying) {
                    audioSourceBgm.clip = bgmSounds[i].clip;
                    audioSourceBgm.volume = Sound_Volume;
                    playBGMSound = bgmSounds[i].name;
                    audioSourceBgm.Play();
                    break;
                }

                else
                {
                    audioSourceBgm.Stop();
                    audioSourceBgm.clip = bgmSounds[i].clip;
                    audioSourceBgm.volume = Sound_Volume;
                    playBGMSound = bgmSounds[i].name;
                    audioSourceBgm.Play();
                    break;
                }
                    
            }
        }
    }*/




    public void PlaySE(string _name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (_name == effectSounds[i].name)
            {
                for (int j = 0; j < audioSourceEffects.Length; j++)
                {
                    if (!audioSourceEffects[j].isPlaying) // ���� play �ϰ� ���� ���� audiosource�� ã�´�.
                    {
                        playSoundName[j] = effectSounds[i].name;
                        audioSourceEffects[j].clip = effectSounds[i].clip; // ã�Ҵٸ� audiosource �� clip�� �÷����ϰ��� �ϴ� clip�� �־��ش�. 
                        audioSourceEffects[j].Play(); // �������ش�
                        return; // ���� ������ �� ���� �ʿ䰡 �����Ƿ� return ���� �ƿ� �Լ��� �����ش�.
                    }
                }
                //Debug.Log("��� ���� AudioSource�� ������Դϴ�.");
                return;
            }

        }
        Debug.Log(_name + "���尡 SoundManager�� ��ϵ��� �ʾҽ��ϴ�");
    }

    public void StopAllSE()
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].Stop();
        }
    }

    public void StopSE(string _name)
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if (playSoundName[i] == _name)
            {
                audioSourceEffects[i].Stop();
                break;
            }

        }

        Debug.Log("��� ����" + _name + "���尡 �����ϴ�");
    }

  
}
