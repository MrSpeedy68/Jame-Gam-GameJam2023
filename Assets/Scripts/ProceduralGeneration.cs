using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] roomPrefabs;

    public int maxRooms = 5;
    public Transform startPoint;
    private List<GameObject> spawnedRooms = new List<GameObject>(); // a list of all spawned rooms
    private int currentRoomCount = 0; // the current number of spawned rooms

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }



    private void Generate()
    {
        GameObject startingRoom = Instantiate(roomPrefabs[0], startPoint.position, startPoint.rotation);
        var startRoomSnapPoint = startingRoom.GetComponent<ProcGenRoom>();
        currentRoomCount++;
        spawnedRooms.Add(startingRoom);

        for (int i = 1; i < maxRooms; i++)
        {
            var previousSnapPoint = spawnedRooms[i - 1].GetComponent<ProcGenRoom>();
            var room = Instantiate(roomPrefabs[Random.Range(1,roomPrefabs.Length)], previousSnapPoint.SnapPoints[0].transform.position, previousSnapPoint.SnapPoints[0].transform.rotation);
            
            spawnedRooms.Add(room);
            currentRoomCount++;
        }
    }
    
}
