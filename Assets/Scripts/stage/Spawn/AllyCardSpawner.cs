using System.Collections.Generic;
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
    public void SetPrioritySpawn(int id, int withinSpawningRound = 3) {
        // make sure the specified ally card will spawn in given round
        // but if the queue is full, it will be inserted to the end of the queue
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
            return;
        }

        int index = UnityEngine.Random.Range(0, count);
        if (SpawnIDQueue.First == null) {
            SpawnIDQueue.AddFirst(-1);
        }
        node = SpawnIDQueue.First;
        for (int i = 0; i < index; i++) {
            if (node.Next == null) SpawnIDQueue.AddLast(-1);
            node = node.Next;
        }
        node.Value = id;
    }

    public void Spawn() {
        if (SpawnIDQueue.Count == 0) {
            SpawnIDQueue.AddFirst(-1);
        }
        int allyCardID = SpawnIDQueue.First.Value;
        SpawnIDQueue.RemoveFirst();

        if (allyCardID == -1) {
            allyCardID = Types[UnityEngine.Random.Range(0, TypesTotal)];
        }
        AllyCard.Spawn(allyCardID, SpawnPosition); // TODO: ally card and enemy should have a moving speed
    }
    #endregion
}