using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[ExecuteInEditMode]
public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] roomPrefabs;

    public int maxRooms = 2;
    public Transform startPoint;
    private List<GameObject> spawnedRooms = new List<GameObject>(); // a list of all spawned rooms
    private int currentRoomCount = 0; // the current number of spawned rooms

    // Start is called before the first frame update
    void Start()
    {
        GenerateDungeon();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateDungeon()
    {
        // Spawn the starting room at the starting point
        GameObject startingRoom = Instantiate(roomPrefabs[0], startPoint.position, startPoint.rotation);
        spawnedRooms.Add(startingRoom);
        currentRoomCount++;
        
        // Keep spawning rooms until we reach the maximum number
        while (currentRoomCount < maxRooms)
        {
            // Choose a random room prefab to spawn
            //int roomIndex = Random.Range(1, roomPrefabs.Length);
            GameObject roomPrefab = roomPrefabs[0];

            // Choose a random room to attach to
            GameObject parentRoom = spawnedRooms[Random.Range(0, spawnedRooms.Count)];

            // Find an unattached SnapPoint on the parent room
            List<Transform> unattachedSnapPoints = new List<Transform>();
            ProcGenRoom roomSnapPoints = parentRoom.GetComponent<ProcGenRoom>();
            foreach (var snapPoint in roomSnapPoints.SnapPoints)
            {
                if (!snapPoint.isAttached)
                {
                    unattachedSnapPoints.Add(snapPoint.gameObject.transform);
                }
            }

            // Choose a random unattached SnapPoint to attach to
            if (unattachedSnapPoints.Count > 0)
            {
                Transform snapPoint = unattachedSnapPoints[Random.Range(0, unattachedSnapPoints.Count)];

                // Spawn the new room and attach it to the SnapPoint
                Vector3 snapPosition = snapPoint.position;
                Quaternion snapRotation = snapPoint.rotation;
                GameObject newRoom = Instantiate(roomPrefab, snapPosition, Quaternion.LookRotation(snapPoint.forward, snapPoint.up));
                newRoom.transform.position += snapPosition;
                newRoom.transform.SetParent(parentRoom.transform, true);

                // Set the SnapPoint to be attached
                snapPoint.GetComponent<SnapPoint>().isAttached = true;

                // Add the new room to the list of spawned rooms
                spawnedRooms.Add(newRoom);
                currentRoomCount++;
            }
            else
            {
                // If we can't find an unattached SnapPoint, choose a new parent room
                continue;
            }
        }
    }
}
