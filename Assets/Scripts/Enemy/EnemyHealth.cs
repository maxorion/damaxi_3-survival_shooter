using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;


    void Awake()
    {
        // Mendapatkan reference komponen
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        // Set current health
        currentHealth = startingHealth;
    }


    void Update()
    {   
        // Check jika sinking
        if (isSinking)
        {
            // Memindahkan objek ke bawah
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        // check jika dead
        if (isDead)
            return;

        // play audio
        enemyAudio.Play();

        // kurangi health
        currentHealth -= amount;

        // Ganti posisi particle
        hitParticles.transform.position = hitPoint;

        // play particle system
        hitParticles.Play();

        // Dead jika health <= 0
        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        // set isDead
        isDead = true;

        // SetCapsuleCollide ke trigger
        capsuleCollider.isTrigger = true;

        // Trigger animation Dead
        anim.SetTrigger("Dead");

        // play Sound dead
        enemyAudio.clip = deathClip;
        enemyAudio.Play();

        // Adds score
        GameObject scoreObj = GameObject.FindGameObjectWithTag("Score");

        // Mendapatkan komponen Player Health
        ScoreManager scoreMngr = scoreObj.GetComponent<ScoreManager>();
        scoreMngr.addScore(scoreValue);
    }


    public void StartSinking()
    {
        // disable navmesh componen
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        // set rigidbody ke kinematic
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        //ScoreManager.score += scoreValue;
        Destroy(gameObject, 2f);
    }
}
