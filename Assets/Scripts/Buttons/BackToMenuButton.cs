using UnityEngine;

public class BackToMenuButton : MonoBehaviour {
    public void Click() {
        if(StageManager.Instance.StageState is StagePauseState) {
            StageManager.Instance.changeState(new StageOverState(StageManager.Instance));
            StageManager.Instance.TriggerRestart();
        } else {
            GameManager.Instance.LoadScene(SceneType.SignIn);
        }
    }
}