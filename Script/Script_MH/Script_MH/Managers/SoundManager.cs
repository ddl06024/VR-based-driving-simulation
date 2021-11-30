using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 전체 게임에서 크게 다 들려야하는 소리 
// 자동차 엔진 소리

public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    // Dictionary로 미리 효과음 클립들을 로드하여 보관 
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");

        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>(); // audioSource에서 component 조작
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Sound.Background].loop = true; // Background 무한 재생 
            _audioSources[(int)Define.Sound.Background].volume = 0.5f;
        }
    }

    // AudioClip을 받아서 오디오 재생 
    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.LowEngine, float pitch = 1.0f, float volume = 1.0f)
    {
        AudioSource audioSource = null;

        if (audioClip == null)
            return;

        // Background 계속 틀고 나머지는 한번 씩 효과음 
        if (type == Define.Sound.Background || type == Define.Sound.Idle)
        {
            audioSource = _audioSources[(int)Define.Sound.Background];
            // 현재 재생되고 있는 음악 멈춤
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();

        }
        else
        {
            audioSource = _audioSources[(int)type];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip); // 한번만 틀기
            audioSource.volume = volume; // 해당 볼륨으로 
        }

    }

    // path를 받아서 음원 재생
    public void Play(string path, Define.Sound type = Define.Sound.LowEngine, float pitch = 1.0f, float volume = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch, volume);
    }

    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.LowEngine)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioClip = null;

        if (type == Define.Sound.Background)
        {
            audioClip = Managers.Resource.Load<AudioClip>(path);
        }
        else
        {
            if(_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.Resource.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing ! {path}");

        return audioClip;
    } 
    // Audio Clear
    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        // Dictionary 비우기
        _audioClips.Clear();
    }

}
