using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCard : MonoBehaviour
{
    public GameObject generate_drag_preview(){
        GameObject go = new GameObject("cardPreview", typeof(SpriteRenderer), typeof(CanvasGroup));
        go.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
        go.GetComponent<CanvasGroup>().alpha = .6f;
        go.transform.position = transform.position;
        go.transform.localScale = transform.localScale;
        return go;
    }
}
