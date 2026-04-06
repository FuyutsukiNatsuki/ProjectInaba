using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro; // UIテキスト用

public class ApiManager : MonoBehaviour
{
    [Header("API設定")]
    [SerializeField] private string apiUrl = "http://127.0.0.1:8000/api/chat";
    
    [Header("UI連携")]
    [SerializeField] private TMP_InputField chatInputField; // ★追加：なっちゃんの文字入力欄！
    [SerializeField] private TextMeshProUGUI aiReplyText;   // ムツキちゃんの返答を表示するテキスト
    [SerializeField] private AudioSource audioSource;       // 音声を鳴らすスピーカー
    [SerializeField] private TMP_InputField userNameField;
    public LogManager logManager;

    [Serializable]
    public class ChatRequest
    {
        public string user_name;
        public string user_text;
    }

    [Serializable]
    public class ChatResponse
    {
        public string ai_text;
        public string audio_url;
    }

    // ==========================================
    // 🚀 送信ボタンを押した時に呼ばれる関数（引数なし！）
    // ==========================================
    public void OnSendButtonClicked()
    {
        // ★ここでInputFieldの中身を直接引っこ抜く！
        string inputText = chatInputField.text;
        string userName = userNameField.text;

        // 空っぽなら何もしない
        if (string.IsNullOrEmpty(inputText)) return;

        // 通信中は「考え中...」って出しておくと親切やね！
        aiReplyText.text = "考え中なのだ...";

        logManager.AddLog(PlayerPrefs.GetString("UserName", "ユーザー"), chatInputField.text);//ログに追記！
        
        // ★送信した瞬間に入力欄を空にしてあげる（連続送信しやすいように！）
        chatInputField.text = "";

        

        // コルーチン（非同期処理）をスタート！
        StartCoroutine(CommunicateWithPython(inputText, userName));
    }

    // ==========================================
    // ✈️ 実際の通信処理（コルーチン）
    // ==========================================
    private IEnumerator CommunicateWithPython(string inputText, string userName)
    {
        ChatRequest requestData = new ChatRequest
        {
            user_name = userName,
            user_text = inputText
        };
        string jsonToSend = JsonUtility.ToJson(requestData);

        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonToSend);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"通信エラーや！: {request.error}");
                aiReplyText.text = "ごめん、通信エラーなのだ...！";
            }
            else
            {
                string responseJson = request.downloadHandler.text;
                ChatResponse responseData = JsonUtility.FromJson<ChatResponse>(responseJson);

                aiReplyText.text = responseData.ai_text;

                logManager.AddLog("因幡ムツキ", aiReplyText.text);//ログに追記！

                if (!string.IsNullOrEmpty(responseData.audio_url))
                {
                    StartCoroutine(DownloadAndPlayAudio(responseData.audio_url));
                }
            }
        }
    }

    // ==========================================
    // 🎵 音声をダウンロードして再生する処理
    // ==========================================
    private IEnumerator DownloadAndPlayAudio(string audioUrl)
    {
        // ★追加の魔法：Windowsのローカルパス(C:\...)を、Unityが読めるURI(file:///)に変換！
        if (!audioUrl.StartsWith("http") && !audioUrl.StartsWith("file://"))
        {
            // バックスラッシュ(\)をスラッシュ(/)に直して、頭に file:/// を付けるで！
            audioUrl = "file:///" + audioUrl.Replace("\\", "/");
        }

        Debug.Log($"変換後の音声URL: {audioUrl}"); // ちゃんと変換されたか確認用！

        using (UnityWebRequest audioRequest = UnityWebRequestMultimedia.GetAudioClip(audioUrl, AudioType.WAV))
        {
            yield return audioRequest.SendWebRequest();

            if (audioRequest.result == UnityWebRequest.Result.ConnectionError || audioRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"音声の取得に失敗したで！: {audioRequest.error}");
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(audioRequest);
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }
}