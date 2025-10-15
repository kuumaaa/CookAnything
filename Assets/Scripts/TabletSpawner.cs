using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class TabletSpawner : MonoBehaviour
{
    private TabletData[] tablets;
    [SerializeField] private GameObject tabletPrefab;
    [SerializeField] private GameObject spawn;
    [SerializeField] private GameObject end;
    

    private float tabletLivingTime = 120f;
    private float timeBetweenSpawns = 30f;
    
    private float minTabletLivingTime = 400f;
    private float minTimeBetweenSpawns = 15f;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tablets = Resources.LoadAll<TabletData>("Tablets");
        StartCoroutine(Spawner());
        SpawnTablet();
    }

    // Update is called once per frame
    
    IEnumerator Spawner()
    {
        float elapsedTime = 0f;

        while (true)
        {
            elapsedTime += Time.deltaTime;
            
            if (elapsedTime > timeBetweenSpawns)
            {
                tabletLivingTime -= 3f;
                timeBetweenSpawns -= 1f;
                
                tabletLivingTime = Mathf.Max(tabletLivingTime,minTabletLivingTime) + Random.Range(-5f, 5f);
                timeBetweenSpawns = Mathf.Max(timeBetweenSpawns, minTimeBetweenSpawns);
                
                SpawnTablet();
                elapsedTime = Random.Range(-3f, 3f);
            }
            yield return null;
        }
    }

    private void SpawnTablet()
    {
        GameObject newTablet = Instantiate(tabletPrefab, spawn.transform);
        newTablet.GetComponent<Tablet>().Spawn(spawn.transform.position, end.transform.position,tabletLivingTime, tablets[Random.Range(0, tablets.Length)]);
        //newTablet.GetComponent<Tablet>().Spawn(spawn.transform.position, end.transform.position,tabletLivingTime, tablets[0]);
    }
    
    
}
