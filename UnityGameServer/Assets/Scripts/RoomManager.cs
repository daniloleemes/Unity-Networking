using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    public readonly string id = Guid.NewGuid().ToString();

    #region GameObjects
    public GameObject floor;
    public GameObject spawnBottom;
    public GameObject spawnLeft;
    public GameObject spawnTop;
    public GameObject spawnRight;

    #endregion

    #region Properties
    public int maxClients = 2;
    public List<Client> clients = new List<Client>();

    private List<Vector3> spawnPoints = new List<Vector3>();

    #endregion

    private void Awake()
    {
        spawnPoints.Add(spawnBottom.transform.position);
        spawnPoints.Add(spawnLeft.transform.position);
        spawnPoints.Add(spawnTop.transform.position);
        spawnPoints.Add(spawnRight.transform.position);
    }

    public void AddPlayer(Client client)
    {
        if (clients.Count < maxClients)
        {
            clients.Add(client);
        }

        if (clients.Count == maxClients)
        {
            StartGame();
        }
    }

    public Vector3? GetNextSpawn()
    {
        if (spawnPoints.Count > 1)
        {
            var spawn = spawnPoints[0];
            spawnPoints.Remove(spawn);
            return spawn;
        }

        return null;
    }
}
