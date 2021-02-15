using UnityEngine;
public class Heart : MonoBehaviour {
    public int healAmount = 10;

    public float powerLifeLength = 15f;
    private float PULifeTimer = 0f;
    PlayerHealth playerHealth;

    GameObject player;

    AudioSource sfxAudio;

    private void Awake() {
        // Mencari game object dengan tag "Player"
        player = GameObject.FindGameObjectWithTag("Player");

        // Mendapatkan komponen Player Health
        playerHealth = player.GetComponent<PlayerHealth>();

        sfxAudio = gameObject.GetComponent<AudioSource>();
    }

    // Callback ketika ada suatu object masuk ke dalam trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && other.isTrigger == false)
        {
            sfxAudio.Play();
            playerHealth.Heal(healAmount);
            gameObject.transform.localScale= Vector3.zero;
            Light objLight = gameObject.GetComponent<Light>();
            objLight.enabled = false;
            Destroy(gameObject, 2f);
        }
    }

    private void Update() {
        PULifeTimer += Time.deltaTime;
        if (PULifeTimer > powerLifeLength) {
            Destroy(gameObject);
        }
    }
}