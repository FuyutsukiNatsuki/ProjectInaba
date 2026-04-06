using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections; // コルーチン用

public class LogManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI logText; // さっき作った巨大な黒板（Text）
    [SerializeField] ScrollRect scrollRect;   // 親のScroll View

    // ★最初から入ってる文字を消して綺麗にしておくで！
    void Start()
    {
        logText.text = "";
    }

    // ★ログを追加する関数
    public void AddLog(string speakerName, string message)
    {
        // テキストの下に、発言者とメッセージを追加！（\nは改行やで！）
        // <b></b>で名前を太字にすると見やすくておすすめ！
        logText.text += $"<b>【{speakerName}】</b>\n{message}\n\n";

        // 文字が追加された後、自動で一番下までスクロールする処理を呼ぶ！
        StartCoroutine(ScrollToBottom());
    }

    // ★文字が追加された瞬間に一番下へスクロールさせるハッカーの技！
    private IEnumerator ScrollToBottom()
    {
        // UnityがUIのサイズを再計算するのを1フレーム待つんや！
        yield return new WaitForEndOfFrame();
        
        // スクロールバーの位置を一番下（0）に強制移動！
        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = 0f;
        }
    }
}