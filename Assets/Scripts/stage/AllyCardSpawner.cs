using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllyCardSpawner : Spawner {

    private Vector3 SpawnPosition;
    private List<int> AllyCardTypes;
    private int AllyCardTypesTotal;
    private LinkedList<int> AllyCardSpawnIDQueue;

    public AllyCardSpawner(Vector3 spawnPosition, List<int> allyCardTypes) {
        SpawnPosition = spawnPosition;
        AllyCardTypes = allyCardTypes;
        AllyCardTypesTotal = allyCardTypes.Count;
        AllyCardSpawnIDQueue = new LinkedList<int>();
    }

    public void PrioritySpawn(int id, int withinRound) {
        // make sure the specified ally card will spawn in given round
        // but if the queue is full, it will be inserted to the end of the queue
        LinkedListNode<int> node = AllyCardSpawnIDQueue.First;
        int count = 0;
        for (int i = 0; i < withinRound && node != null; i++) {
            if (node.Value == -1) {
                count++;
            }
            node = node.Next;
        }

        if (count == 0) {
            AllyCardSpawnIDQueue.AddLast(id);
            return;
        }

        int index = UnityEngine.Random.Range(0, count);
        if (AllyCardSpawnIDQueue.First == null) {
            AllyCardSpawnIDQueue.AddFirst(-1);
        }
        node = AllyCardSpawnIDQueue.First;
        for (int i = 0; i < index; i++) {
            if (node.Next == null) AllyCardSpawnIDQueue.AddLast(-1);
            node = node.Next;
        }
        node.Value = id;
    }

    public void Spawn() {
        if (AllyCardSpawnIDQueue.Count == 0)
            AllyCardSpawnIDQueue.AddFirst(-1);
        int allyCardID = AllyCardSpawnIDQueue.First.Value;
        AllyCardSpawnIDQueue.RemoveFirst();

        if (allyCardID == -1)
            allyCardID = AllyCardTypes[UnityEngine.Random.Range(0, AllyCardTypesTotal)];
        EnemyUnit.Spawn(allyCardID, SpawnPosition);
    }
}