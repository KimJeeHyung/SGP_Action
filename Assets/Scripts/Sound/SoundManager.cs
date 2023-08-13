using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // 클래스 자체를 직렬화 하려면 이 키워드를 입력해야한다.
public class Sound
{
    public string name; // 곡의 이름
    public AudioClip clip; // 곡

}


public class SoundManager : MonoBehaviour
{
    // 싱글톤,Singleton, 1개.

    [SerializeField]
    private float Sound_Volume = 0.5f;

    [SerializeField]
    private float fadeDuration = 10.0f;

    private float currentTime = 0.0f;

    static public SoundManager instance;

    #region singleton
    void Awake() // 객체 생성 시 최초 실행
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

    /* void OnEnable() // 매번 활성화 될 때마다 실행 (코루틴은 실행(호출) 불가능)
      {

      }

      void Start() // 최초 1회 호출.(코루틴 실행(호출) 가능)
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
                    if (!audioSourceEffects[j].isPlaying) // 현재 play 하고 있지 않은 audiosource를 찾는다.
                    {
                        playSoundName[j] = effectSounds[i].name;
                        audioSourceEffects[j].clip = effectSounds[i].clip; // 찾았다면 audiosource 내 clip에 플레이하고자 하는 clip을 넣어준다. 
                        audioSourceEffects[j].Play(); // 실행해준다
                        return; // 이후 포문을 더 돌릴 필요가 없으므로 return 으로 아예 함수를 끝내준다.
                    }
                }
                //Debug.Log("모든 가용 AudioSource가 사용중입니다.");
                return;
            }

        }
        Debug.Log(_name + "사운드가 SoundManager에 등록되지 않았습니다");
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

        Debug.Log("재생 중인" + _name + "사운드가 없습니다");
    }

  
}
