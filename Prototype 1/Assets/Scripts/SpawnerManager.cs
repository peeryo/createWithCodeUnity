using System.Collections;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public int minWaitTime;
    public int maxWaitTime;
    
    public GameObject[] carPrefabs;
    public Transform[] spawnPosition;

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(InvokeCar());
    }

    private void SpawnCar()
    {
        int idCar = Random.Range(0, carPrefabs.Length);
        int idPosition = Random.Range(0, spawnPosition.Length);

        Instantiate(carPrefabs[idCar], spawnPosition[idPosition].position, Quaternion.Euler(0,180,0));
    }
    
    private IEnumerator InvokeCar()
    {
        while (true)
        {
            SpawnCar();
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        }
    }
}
