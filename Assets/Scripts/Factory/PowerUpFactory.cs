using System;
using UnityEngine;

public class PowerUpFactory : MonoBehaviour, IFactory
{

    [SerializeField]
    public GameObject[] powerUpPrefab;

    public GameObject FactoryMethod(int tag)
    {
        GameObject powerUp = Instantiate(powerUpPrefab[tag]);
        return powerUp;
    }
}