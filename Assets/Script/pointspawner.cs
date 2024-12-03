using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pointspawner : MonoBehaviour
{
    public GameObject pointPrefab; // Prefab punktu do instancjacji
    public int numberOfPoints = 20; // Liczba punktów do rozmieszczenia
    public float checkRadius = 0.5f; // Promieñ sprawdzania kolizji
    public LayerMask wallLayer; // Warstwa œcian (ustawiona w Unity)

    public float minX, maxX, minZ, maxZ; // Granice obszaru labiryntu

    void Awake()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            if (randomPosition != Vector3.zero)
            {
               
                GameObject newPoint = Instantiate(pointPrefab, randomPosition, Quaternion.identity, transform);
                newPoint.name = $"Point_{i + 1}"; // Opcjonalnie: zmiana nazwy obiektu
            }
            else
            {
                Debug.Log("Nie uda³o siê znaleŸæ odpowiedniego miejsca dla punktu.");
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        for (int attempt = 0; attempt < 10; attempt++) // 100 prób znalezienia odpowiedniej pozycji
        {
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);
            float yPosition = Terrain.activeTerrain.SampleHeight(new Vector3(randomX, 0, randomZ));
            Vector3 potentialPosition = new Vector3(randomX, yPosition+1f, randomZ);
            if (!Physics.CheckSphere(potentialPosition, checkRadius, wallLayer) && yPosition<9)
            {
                return potentialPosition;
            }
            for (int attempt2 = 0; attempt2 < 10; attempt2++)
            {
                if (randomX < (minX + maxX) / 2)
                {
                    randomX += 6;
                }
                else
                {
                    randomX-= 6; 
                }
                // Sprawdzenie, czy pozycja jest na œcianie
                if (!Physics.CheckSphere(potentialPosition, checkRadius, wallLayer) && yPosition < 9)
                {
                    return potentialPosition;
                }
                if (randomZ < (minZ + maxZ) / 2)
                {
                    randomZ += 6;
                }
                else { 
                randomZ-= 6;
                }
                // Sprawdzenie, czy pozycja jest na œcianie
                if (!Physics.CheckSphere(potentialPosition, checkRadius, wallLayer) && yPosition < 9)
                {
                    return potentialPosition;
                }
            }
        }
        return Vector3.zero; // Zwrot Vector3.zero oznacza, ¿e nie znaleziono odpowiedniego miejsca
    }
}