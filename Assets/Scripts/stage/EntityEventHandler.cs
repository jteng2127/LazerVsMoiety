using UnityEngine;

public class EntityEventHandler : StageStateReactBase {
    #region data
    private EnemySpawnHandler EnemySpawnHandler;
    private AllyCardSpawnHandler AllyCardSpawnHandler;
    #endregion

    #region Creator
    public static EntityEventHandler Create(
            EnemySpawnHandler enemySpawnHandler,
            AllyCardSpawnHandler allyCardSpawnHandler) {
        // create new GameObject to hold the EntityEventHandler
        GameObject go = new GameObject("EntityEventHandler");
        EntityEventHandler entityEventHandler = go.AddComponent<EntityEventHandler>();
        entityEventHandler.Initial(enemySpawnHandler, allyCardSpawnHandler);
        return entityEventHandler;
    }
    private void Initial(
            EnemySpawnHandler enemySpawnHandler,
            AllyCardSpawnHandler allyCardSpawnHandler) {
        EnemySpawnHandler = enemySpawnHandler;
        AllyCardSpawnHandler = allyCardSpawnHandler;
    }
    #endregion

    #region StageStateReactBase
    public override void EnemyDestroyed() {
        AudioManager.Instance.Play("kill", 0.1f);
        if (EnemySpawnHandler.IsEnemyAllDead()) {
            StageManager.Instance.TriggerGameWin();
        }
    }
    public override void AllyCardDestroyed() {
        AllyCardSpawnHandler.FreeSpawnSpace();
    }
    #endregion
}