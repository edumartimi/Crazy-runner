using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilemanager : MonoBehaviour
{
    public GameObject[] tileprefabs;

    private float spawnz = 0.0f;
    private float tileLeght = 100.0f;
    private float safeZone = 80.0f;
    private int amnTilesScreen = 10;
    private int lastPrefabIndex = 0;

    private List<GameObject> activeTiles;

    // Start is called before the first frame update

    private Transform playerTransform;
    void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < amnTilesScreen; i++) 
        {
            Spawntile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.z - safeZone > (spawnz - amnTilesScreen * tileLeght)) 
        {
            Spawntile();
            DeleteTile();
        }
    }

    void Spawntile(int prefabIndex = -1) 
    {
        GameObject go;
        go = Instantiate(tileprefabs[RandomPrefabIndex()]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnz;
        spawnz += tileLeght;
        activeTiles.Add(go);
    }

    void DeleteTile() 
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex() 
    {
        if(tileprefabs.Length <= 1)
        return 0;

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex) 
        {
            randomIndex = Random.Range(0, tileprefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;

    }
}
