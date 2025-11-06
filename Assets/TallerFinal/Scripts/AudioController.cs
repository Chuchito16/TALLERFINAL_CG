using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;


    public AudioSource backgroundMusicSource;
    public AudioSource sfxSource;


    public AudioClip effectOnCaptureBad;
    public AudioClip effectOnCaptureGood;

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
    }

    public void PlayCaptureGoodSound()
    {
        if (sfxSource == null || effectOnCaptureGood == null)
            return;

        sfxSource.clip = effectOnCaptureGood;
        sfxSource.Play();
    }

    public void PlayCaptureBadSound()
    {
        if (sfxSource == null || effectOnCaptureBad == null)
            return;

        sfxSource.clip = effectOnCaptureBad;
        sfxSource.Play();
    }
}

