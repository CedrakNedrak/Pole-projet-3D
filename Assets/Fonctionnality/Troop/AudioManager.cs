using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Loops & Sources")]
    [SerializeField] private AudioSource battleSounds;
    [SerializeField] private AudioSource minerFootsteps;
    [SerializeField] private AudioSource golemFootsteps;
    [SerializeField] private AudioSource sfxSource;

    [Header("Clips")]
    [SerializeField] private AudioClip golemAttackSound;
    [SerializeField] private AudioClip monsterSound;
    [SerializeField] private AudioClip rockBrokenSound;


    private int movingMiners = 0;
    private int movingGolems = 0;
    private int fightingSoldiers = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
        Destroy(gameObject);
        return;
        }

        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void StartBattleSounds()
    {
        if (!battleSounds.isPlaying)
            battleSounds.Play();
    }
    public void StopBattleSounds()
    {
        if (battleSounds.isPlaying)
            battleSounds.Stop();
    }
    public void StartMinerFootsteps()
    {
        if (!minerFootsteps.isPlaying)
            minerFootsteps.Play();
    }
    public void StopMinerFootsteps()
    {   
        if (minerFootsteps.isPlaying)
            minerFootsteps.Stop();
    }
    public void StartGolemFootsteps()
    {
        if (!golemFootsteps.isPlaying)
            golemFootsteps.Play();
    }
    public void StopGolemFootsteps()
    {
        if (golemFootsteps.isPlaying)
            golemFootsteps.Stop();
    }

    public void GolemAttack()
    {
        sfxSource.PlayOneShot(golemAttackSound);
    }
    public void MonsterSound()
    {
        sfxSource.PlayOneShot(monsterSound);
    }
    public void RockBroken()
    {
        sfxSource.PlayOneShot(rockBrokenSound);
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
