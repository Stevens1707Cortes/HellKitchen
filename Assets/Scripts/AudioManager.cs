using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Player Config")]
    [SerializeField] AudioSource music, soundEffects, enemyEffect;

    [Header("Music Clips")]
    public AudioClip menuClip, kitchenClip, dungeonClip;

    [Header("SFX Clips")]
    public AudioClip plasmaClip, shotgunClip, deathClip, stoveClip, happyClip, angryClip, pickupClip;

    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        music.clip = clip;
        music.Play();
    }

    public void StopMusic()
    {
        music.Stop();
    }

    public void PlayEffect(AudioClip clip)
    {
        soundEffects.PlayOneShot(clip);
    }

    public void PlayEnemyEffect(AudioClip clip)
    {
        enemyEffect.PlayOneShot(clip);
    }

    public void StopEffect(AudioClip clip)
    {
        soundEffects.clip = clip;
        soundEffects.Stop();
    }
}

