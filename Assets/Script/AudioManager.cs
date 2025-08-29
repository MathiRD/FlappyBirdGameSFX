using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Sources")]
    public AudioSource musicSource;   
    public AudioSource sfxSource;     
    public AudioClip flap, score, hit;

    [Header("Mixer & Snapshots")]
    public AudioMixer mainMixer;                
    public AudioMixerSnapshot normalSnapshot;    
    public AudioMixerSnapshot caveSnapshot;      
    public float snapshotBlend = 0.35f;          

    int caveOverlapCount = 0;
    bool inCave = false;
    float lastSwitchTime = -999f;
    [SerializeField] float minSwitchDelay = 0.5f;

    Coroutine pauseCo;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (musicSource && !musicSource.isPlaying)
        {
            musicSource.volume = 1f;
            musicSource.Play();
        }
        if (normalSnapshot) normalSnapshot.TransitionTo(0f);
    }

    public void PlayFlap()
    {
        if (flap) sfxSource.PlayOneShot(flap);
    }
    
    public void PlayScore() { if (score) sfxSource.PlayOneShot(score); }
    
    public void PlayHit()
    {
        if (hit) sfxSource.PlayOneShot(hit);
    }

    public void ZoneEnter(bool isCave)
    {
        if (!isCave) return;
        caveOverlapCount++;
        TryUpdateZone();
    }

    public void ZoneExit(bool isCave)
    {
        if (!isCave) return;
        caveOverlapCount = Mathf.Max(0, caveOverlapCount - 1);
        TryUpdateZone();
    }

    void TryUpdateZone()
    {
        bool wantCave = caveOverlapCount > 0;

        if (wantCave == inCave)
            return;

        if (Time.unscaledTime - lastSwitchTime < minSwitchDelay)
            return;

        lastSwitchTime = Time.unscaledTime;
        inCave = wantCave;

        if (inCave)
        {
            if (pauseCo != null)
                StopCoroutine(pauseCo);
            
            if (caveSnapshot)
                caveSnapshot.TransitionTo(snapshotBlend);

            pauseCo = StartCoroutine(PauseAfter(snapshotBlend));
        }
        else
        {
            if (pauseCo != null)
                StopCoroutine(pauseCo);
            
            if (musicSource)
                musicSource.UnPause();

            if (normalSnapshot)
                normalSnapshot.TransitionTo(snapshotBlend);
        }
    }

    IEnumerator PauseAfter(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        
        if (musicSource)
            musicSource.Pause();
    }
}
