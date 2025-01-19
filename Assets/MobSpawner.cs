using UnityEngine;
using System.Collections.Generic;

public class MobSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public List<GameObject> mobPrefabs; // Lista prefabów mobów
    public int maxMobs = 30; // Maksymalna liczba mobów
    public float spawnRadius = 100; // Promieñ spawnowania
    public float despawnDistance = 200; // Odleg³oœæ, po której moby despawnuj¹ siê

    private List<GameObject> spawnedMobs = new List<GameObject>(); // Lista aktywnych mobów
    private Transform player; // Transform gracza
    private bool isPlayerInTrigger = false; // Czy gracz jest w triggerze

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Znalezienie gracza po tagu
    }

    private void Update()
    {
        if (isPlayerInTrigger)
        {
            SpawnMob();
            CheckDespawnMobs();
        }
    }
    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        Debug.Log(bounds);
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            77.4788f,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    // Spawnowanie moba
    private void SpawnMob()
    {
        if (spawnedMobs.Count >= maxMobs || mobPrefabs.Count == 0) return; // SprawdŸ, czy nie osi¹gniêto limitu lub lista prefabów jest pusta

        // Wybieranie losowego prefabrykat z listy
        GameObject randomMobPrefab = mobPrefabs[Random.Range(0, mobPrefabs.Count)];

        Vector3 spawnPosition = RandomPointInBounds(gameObject.GetComponent<Collider>().bounds); ;
        spawnPosition.y = player.position.y; // Dopasowanie wysokoœci do gracza

        GameObject mob = Instantiate(randomMobPrefab, spawnPosition, Quaternion.identity);
        spawnedMobs.Add(mob); // Dodanie moba do listy
    }

    // Sprawdzanie, czy moby s¹ za daleko i ich despawnowanie
    private void CheckDespawnMobs()
    {
        for (int i = spawnedMobs.Count - 1; i >= 0; i--)
        {
            if (Vector3.Distance(player.position, spawnedMobs[i].transform.position) > despawnDistance)
            {
                Destroy(spawnedMobs[i]); // Zniszczenie obiektu
                spawnedMobs.RemoveAt(i); // Usuniêcie z listy
            }
        }
    }

    // Zdarzenie wejœcia do triggera
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true; // Gracz wszed³ w trigger
        }
    }

    // Zdarzenie wyjœcia z triggera
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false; // Gracz opuœci³ trigger
        }
    }
}
