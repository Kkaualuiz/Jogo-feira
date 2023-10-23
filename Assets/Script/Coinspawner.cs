using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coinspawner : MonoBehaviour
{
    public GameObject coinprefab;
    public float spawntime = 1.5f;

    private Camera mainCamera;
    private float xMax, YMax;

    void Start()
    {
        InvokeRepeating("SpawnCoin", spawntime, spawntime);
        
        mainCamera = Camera.main;
        xMax = mainCamera.orthographicSize * mainCamera.aspect;
        YMax = mainCamera.orthographicSize;

    }

    private void Spawncoin()
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 coinPosition = new Vector3(cameraPosition.x + Random.Range(-xMax, xMax), Random.Range(-YMax, YMax), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
