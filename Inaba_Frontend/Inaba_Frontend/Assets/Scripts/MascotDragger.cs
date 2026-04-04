using UnityEngine;
using UnityEngine.EventSystems;

// ★ IDragHandler などをつけることで、Unityがドラッグ操作を自動で検知してくれるで！
public class MascotDragger : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    void Start()
    {
        // 自分のRectTransformと、親のCanvasを取得しておく！
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 掴んだ瞬間の処理（今回は特に何もしなくてOK！）
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas == null) return;

        // マウスの移動量（delta）をCanvasのスケールで割って、座標に足す！
        // これでマウスにピタッとくっついて動くようになるで！
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 離した瞬間の処理（今回は特に何もしなくてOK！）
    }
}