using UnityEngine;
using System.Collections.Generic;

public class MobSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public List<GameObject> mobPrefabs; // Lista prefab�w mob�w
    public int maxMobs = 30; // Maksymalna liczba mob�w
    public float spawnRadius = 100; // Promie� spawnowania
    public float despawnDistance = 200; // Odleg�o��, po kt�rej moby despawnuj� si�

    private List<GameObject> spawnedMobs = new List<GameObject>(); // Lista aktywnych mob�w
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
        if (spawnedMobs.Count >= maxMobs || mobPrefabs.Count == 0) return; // Sprawd�, czy nie osi�gni�to limitu lub lista prefab�w jest pusta

        // Wybieranie losowego prefabrykat z listy
        GameObject randomMobPrefab = mobPrefabs[Random.Range(0, mobPrefabs.Count)];

        Vector3 spawnPosition = RandomPointInBounds(gameObject.GetComponent<Collider>().bounds); ;
        spawnPosition.y = player.position.y; // Dopasowanie wysoko�ci do gracza

        GameObject mob = Instantiate(randomMobPrefab, spawnPosition, Quaternion.identity);
        spawnedMobs.Add(mob); // Dodanie moba do listy
    }

    // Sprawdzanie, czy moby s� za daleko i ich despawnowanie
    private void CheckDespawnMobs()
    {
        for (int i = spawnedMobs.Count - 1; i >= 0; i--)
        {
            if (Vector3.Distance(player.position, spawnedMobs[i].transform.position) > despawnDistance)
            {
                Destroy(spawnedMobs[i]); // Zniszczenie obiektu
                spawnedMobs.RemoveAt(i); // Usuni�cie z listy
            }
        }
    }

    // Zdarzenie wej�cia do triggera
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true; // Gracz wszed� w trigger
        }
    }

    // Zdarzenie wyj�cia z triggera
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false; // Gracz opu�ci� trigger
        }
    }
}
