using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Singleton instance
    public static SoundManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource; // Background music
    public AudioSource effectsSource; // Sound effects

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;  
    public AudioClip buttonClickSound;

     private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object alive across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }
    }

    private void Start()
    {
        // Automatically play background music if assigned and not already playing
        if (backgroundMusic != null && !musicSource.isPlaying)
        {
            PlayMusic(backgroundMusic);
        }
    }

    /// <summary>
    /// Plays background music that loops continuously.
    /// </summary>
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        if (musicSource.clip != clip) // Avoid restarting the same clip
        {
            musicSource.clip = clip;
            musicSource.loop = true; // Loop the music
            musicSource.Play();
        }
    }

    /// <summary>
    /// Plays a sound effect once.
    /// </summary>
    public void PlaySoundEffect(AudioClip clip)
    {
        if (clip == null) return;

        effectsSource.PlayOneShot(clip); // Play sound effect once
    }

    /// <summary>
    /// Stops the background music.
    /// </summary>
    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }
}
