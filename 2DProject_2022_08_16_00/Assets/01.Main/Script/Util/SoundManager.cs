using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    public enum AudioType
    {
        BGM,
        SFX,
        Max
    }
    public enum BgmList
    {
        None = -1,
        dragon_flight,
        Max
    }
    public enum SfxList
    {
        get_coin,
        get_gem,
        get_invincible,
        get_item,
        mon_die,
        Max
    }
    [Header("현재 플레이중인 BGM")]
    [SerializeField]
    BgmList m_curBgm = BgmList.None;
    [SerializeField]

    AudioSource[] m_audio;
    [SerializeField]
    AudioClip[] m_bgmClips;
    [SerializeField]
    AudioClip[] m_sfxClips;
    const int MaxVolumeLevel = 10;
    const int MaxPlayCount = 5;

    Dictionary<SfxList, int> m_sfxPlayList = new Dictionary<SfxList, int>();
    public bool IsPlayingBGM { get { return m_audio[(int)AudioType.BGM].isPlaying; } }
    // Start is called before the first frame update

    protected override void OnStart()
    {
        m_audio = new AudioSource[(int)AudioType.Max];
        m_audio[(int)AudioType.BGM] = gameObject.AddComponent<AudioSource>();
        m_audio[(int)AudioType.BGM].loop = true;
        m_audio[(int)AudioType.BGM].playOnAwake = false;
        m_audio[(int)AudioType.BGM].rolloffMode = AudioRolloffMode.Linear;

        m_audio[(int)AudioType.SFX] = gameObject.AddComponent<AudioSource>();
        m_audio[(int)AudioType.SFX].loop = false;
        m_audio[(int)AudioType.SFX].playOnAwake = false;
        m_audio[(int)AudioType.SFX].rolloffMode = AudioRolloffMode.Linear;

        m_bgmClips = Resources.LoadAll<AudioClip>("Sound/BGM");
        m_sfxClips = Resources.LoadAll<AudioClip>("Sound/SFX");
    }
    public void PlayBGM(BgmList bgm)
    {
        m_curBgm = bgm;
        m_audio[(int)AudioType.BGM].clip = m_bgmClips[(int)bgm];
        m_audio[(int)AudioType.BGM].Play();
    }
    public void SetMute(bool active)
    {
        m_audio[(int)AudioType.BGM].mute = active;
        m_audio[(int)AudioType.SFX].mute = active;
    }
    public void SetVolume(int level)
    {
        SetVolumeBGM(level);
        SetVolumeSFX(level);
    }
    public void SetVolumeBGM(int level)
    {
        if (level > 10) level = MaxVolumeLevel;
        if (level < 0) level = 0;
        m_audio[(int)AudioType.BGM].volume = (float)level / MaxVolumeLevel;
    }
    public void SetVolumeSFX(int level)
    {        
        if (level > 10) level = MaxVolumeLevel;
        if (level < 0) level = 0;
        m_audio[(int)AudioType.SFX].volume = (float)level / MaxVolumeLevel;
    }
    public void StopBGM()
    {
        m_audio[(int)AudioType.BGM].Stop();
    }
    IEnumerator Coroutine_RemoveSfxPlayList(SfxList sfx, float duration)
    {
        yield return new WaitForSeconds(duration);
        m_sfxPlayList[sfx]--;
        if(m_sfxPlayList[sfx] <= 0)
        {
            m_sfxPlayList.Remove(sfx);
        }
    }
    public void PlaySFX(SfxList sfx)
    {
        if (m_sfxPlayList.ContainsKey(sfx))
        {
            if (m_sfxPlayList[sfx] >= MaxPlayCount)
                return;
            m_sfxPlayList[sfx]++;
        }
        else
        {
            m_sfxPlayList.Add(sfx, 1);
        }
        StartCoroutine(Coroutine_RemoveSfxPlayList(sfx, m_sfxClips[(int)sfx].length));
        m_audio[(int)AudioType.SFX].PlayOneShot(m_sfxClips[(int)sfx]);
    }
    public void StopSFX()
    {
        m_audio[(int)AudioType.SFX].Stop();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (!IsPlayingBGM)
            {
                PlayBGM(BgmList.dragon_flight);
            }
            else
            {
                StopBGM();
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlaySFX((SfxList)Random.Range((int)SfxList.get_coin, (int)SfxList.Max));
        }
    }
}
