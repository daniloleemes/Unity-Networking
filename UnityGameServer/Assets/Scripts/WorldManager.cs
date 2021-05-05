using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{

    public static WorldManager instance;

    #region Prefabs
    public GameObject roomPrefab;

    #endregion

    public static List<RoomManager> rooms = new List<RoomManager>();
    public static int nextAvailableRoom = -1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public RoomManager FindAvailableRoom()
    {
        var floor = roomPrefab.GetComponent<RoomManager>().floor;
        var mesh = floor.GetComponent<MeshFilter>().sharedMesh;
        var bounds = mesh.bounds;

        if (rooms.Count == 0)
        {
            var room = Instantiate(roomPrefab, Vector3.zero, Quaternion.identity).GetComponent<RoomManager>();
            rooms.Add(room);
            nextAvailableRoom = 0;
            return room;
        }

        for (int i = 0; i < rooms.Count; i++)
        {
            var room = rooms[i];
            if (room.clients.Count < room.maxClients)
            {
                return room;
            }
        }

        nextAvailableRoom++;
        var r = Instantiate(roomPrefab, new Vector3(nextAvailableRoom * bounds.size.x + nextAvailableRoom, 0f, 0f), Quaternion.identity).GetComponent<RoomManager>();
        rooms.Add(r);
        return r;
    }
}
