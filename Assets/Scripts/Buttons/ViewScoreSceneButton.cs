using UnityEngine;

public class ViewScoreSceneButton : MonoBehaviour {

    public void Click() {
        GameManager.Instance.LoadScene(SceneType.ViewScore);
    }
}
