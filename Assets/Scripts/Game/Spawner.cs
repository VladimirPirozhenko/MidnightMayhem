using FishNet;
using FishNet.Object;
using System.Collections;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    [SerializeField] private Vector3 spawnZoneSize;
    [SerializeField] private Vector3 spawnZoneCenter;
    [SerializeField] private Box boxPrefab;
    public void Spawn()
    {
        Vector3 spawnPos = RandomPositionInZone();
        spawnPos.y = 0;
        Box box = Instantiate(boxPrefab);
        box.transform.position = spawnPos;
        InstanceFinder.ServerManager.Spawn(box.gameObject);       
    }
    public Vector3 RandomPositionInZone()
    {
        float randomXPos = Random.Range(-spawnZoneSize.x / 2, spawnZoneSize.x / 2);
        float randomYPos = Random.Range(-spawnZoneSize.y / 2, spawnZoneSize.y / 2);
        float randomZPos = Random.Range(-spawnZoneSize.z / 2, spawnZoneSize.z / 2);
        return spawnZoneCenter + new Vector3(randomXPos, randomYPos, randomZPos);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;     
        Gizmos.DrawWireCube(spawnZoneCenter, spawnZoneSize);   
    }
}
