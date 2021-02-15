using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float spawnTime = 15f;
    public float timeRange = 10f;

    public float spawnArea = 10f;

    [SerializeField]
    MonoBehaviour factory;
    IFactory Factory { get { return factory as IFactory; } }

    void Start()
    {
        // Mengeksekusi fungsi Spawn setelah beberapa detik, sesuai dengan nilai spawnTime
        Invoke("Spawn", spawnTime);
    }


    void Spawn()
    {
        // Jika player telah mati maka tidak membuat power up baru

        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        // Mendapatkan nilai random
        int spawnPU = Random.Range(0, 2);

        // Menduplikasi powerUp
        GameObject neoPU = Factory.FactoryMethod(spawnPU);

        neoPU.transform.position = new Vector3(Random.Range(-spawnArea, spawnArea), 1f, Random.Range(-spawnArea, spawnArea));


        // Mengeksekusi kembali fungsi Spawn setelah beberapa detik, sesuai dengan nilai spawnTime +- timeRange
        Invoke("Spawn", Random.Range(spawnTime - timeRange, spawnTime + timeRange));

    }
}
