using UnityEngine;
using UnityEngine.UI;

public class InfoButton : MonoBehaviour {
    private Sprite InfoButtonSprite;
    private Sprite CloseButtonSprite;
    private Image InfoButtonImage;
    private GameObject InfoPanel;

    void Awake() {
        InfoPanel = transform.parent.Find("InfoPanel").gameObject;
        InfoButtonImage = transform.GetComponent<Image>();
    }

    public void Click() {
        if(InfoButtonSprite == null) InfoButtonSprite = Resources.Load<Sprite>("Images/StageSelector/1x/info_button");
        if(CloseButtonSprite == null) CloseButtonSprite = Resources.Load<Sprite>("Images/general/1x/close");
        if(InfoPanel.activeSelf) {
            InfoPanel.SetActive(false);
            InfoButtonImage.sprite = InfoButtonSprite;
        }
        else {
            InfoPanel.SetActive(true);
            InfoButtonImage.sprite = CloseButtonSprite;
        }
    }
}