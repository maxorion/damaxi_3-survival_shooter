using UnityEngine;
public class Speedter : MonoBehaviour
{
    public float speedUpAmount = 10f;
    public float speedUpLength = 10f;
    public float powerLifeLength = 10f;
    private float PULifeTimer = 0f;
    PlayerMovement playerMovement;

    GameObject player;
    AudioSource sfxAudio;


    private void Awake()
    {
        // Mencari game object dengan tag "Player"
        player = GameObject.FindGameObjectWithTag("Player");

        // Mendapatkan komponen Player Health
        playerMovement = player.GetComponent<PlayerMovement>();

        sfxAudio = gameObject.GetComponent<AudioSource>();
    }

    // Callback ketika ada suatu object masuk ke dalam trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && other.isTrigger == false)
        {
            sfxAudio.Play();
            playerMovement.SpeedUp(speedUpAmount, speedUpLength);
            gameObject.transform.localScale= Vector3.zero;
            Light objLight = gameObject.GetComponent<Light>();
            objLight.enabled = false;
            Collider selfTrigColl = gameObject.GetComponent<Collider>();
            selfTrigColl.enabled = false;
            Destroy(gameObject, 2f);
        }
    }

    private void Update()
    {
        PULifeTimer += Time.deltaTime;
        if (PULifeTimer > powerLifeLength)
        {
            Destroy(gameObject);
        }
    }
}