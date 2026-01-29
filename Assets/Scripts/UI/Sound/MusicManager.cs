using UnityEngine;


//Using For Background Music
[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private AudioClip backgroundMusic;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        SetupAudioSource();
        PlayMusic();
    }

    private void SetupAudioSource()
    {
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = 0.6f;
    }

    private void PlayMusic()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }
}
