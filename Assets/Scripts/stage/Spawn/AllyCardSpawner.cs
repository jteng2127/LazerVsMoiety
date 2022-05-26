using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class AllyCardSpawner : ISpawner {

    #region default data
    private static readonly Vector3 DefSpawnPosition = new Vector3(8.0f, 4.0f, 0.0f);
    public const float DefMovingSpeed = 0.8f;
    #endregion

    #region data
    private Vector3 SpawnPosition;
    private List<int> Types;
    private int TypesTotal;
    private LinkedList<int> SpawnIDQueue;
    private float MovingSpeed;
    #endregion

    #region constructor
    public AllyCardSpawner(
            List<int> allyCardTypes,
            Vector3? spawnPosition = null,
            float movingSpeedMultiplier = 1.0f) {
        Types = allyCardTypes;
        if (spawnPosition == null) SpawnPosition = DefSpawnPosition;
        else SpawnPosition = (Vector3)spawnPosition;
        TypesTotal = allyCardTypes.Count;
        SpawnIDQueue = new LinkedList<int>();
        MovingSpeed = DefMovingSpeed * movingSpeedMultiplier;
    }
    #endregion

    #region public method
    public void SetPrioritySpawn(int id, int withinSpawningRound = 5) {
        // make sure the specified ally card will spawn in given round
        // but if the queue is full, it will be inserted to the end of the queue
        while (SpawnIDQueue.Count < withinSpawningRound) {
            SpawnIDQueue.AddLast(-1);
        }
        LinkedListNode<int> node = SpawnIDQueue.First;
        int count = 0;
        for (int i = 0; i < withinSpawningRound && node != null; i++) {
            if (node.Value == -1) {
                count++;
            }
            node = node.Next;
        }

        if (count == 0) {
            SpawnIDQueue.AddLast(id);
        } else {
            int spaceNumber = UnityEngine.Random.Range(0, count);
            node = SpawnIDQueue.First;
            while (node.Value != -1) {
                node = node.Next;
            }
            for (int i = 0; i < spaceNumber; i++) {
                node = node.Next;
                while (node.Value != -1) {
                    node = node.Next;
                }
            }
            node.Value = id;
        }
        Debug.Log("[AllyCardSpawner] SetPrioritySpawn: " + id + " within " + withinSpawningRound + " round(s)");
        Debug.Log("[AllyCardSpawner] SpawnIDQueue: " + String.Join(", ", SpawnIDQueue.Select(x => x.ToString()).ToArray()));
    }

    public int Spawn() {
        if (SpawnIDQueue.Count == 0) {
            SpawnIDQueue.AddFirst(-1);
        }
        int allyCardID = SpawnIDQueue.First.Value;
        SpawnIDQueue.RemoveFirst();

        if (allyCardID == -1) {
            allyCardID = Types[UnityEngine.Random.Range(0, TypesTotal)];
        }
        AllyCard.Spawn(allyCardID, SpawnPosition, MovingSpeed);
        return allyCardID;
    }
    #endregion
}