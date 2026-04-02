import ollama
import chromadb
import datetime
import asyncio
import threading
import subprocess
import winsound  # Windows標準の音を鳴らすモジュールやで！
import requests
import json
import re
import os

print("Project Inaba -ProtoType- is initializing...")
client = chromadb.PersistentClient(path="./inaba_memory")
collection = client.get_or_create_collection(name="inaba_long_term_memory")

#記憶を検索する関数
def search_memory(user_input):
    query_vector = ollama.embeddings(model="bge-m3", prompt=user_input)["embedding"]
    results = collection.query(query_embeddings=[query_vector], n_results=1)

    #もし記憶が見つかったら、カンペとしてテキストを返す
    if results["documents"] and len(results["documents"][0]) > 0:
        return results["documents"][0][0]
    return ""

#AIに会話を要約させる（返答後処理）
def summarize_conversation(user_text, ai_text):
    prompt = f"次の会話から、重要な名詞やトピックだけを３つ抽出し、短い要約文を作って。\nユーザー：{user_text}\nInaba：{ai_text}"

    #要約にはメインとは別の軽量モデルを使用する。
    res = ollama.chat(model="gemma2:2b", messages=[{"role": "user", "content":prompt}])
    return res["message"]["content"]

#要約した記憶をDBに保存する関数
def save_to_memory(summary_text):
    vector = ollama.embeddings(model="bge-m3", prompt=summary_text)["embedding"]
    # IDは被り防止のために現在時刻の文字列で
    memory_id ="mem_" + datetime.datetime.now().strftime("%Y%m%d%H%M%S")
    collection.upsert(ids=[memory_id], embeddings=[vector], documents=[summary_text])
    print("裏処理:新規記憶をDBに保存しました。")

#バックグラウンド処理
def background_memory_task(user_text, ai_text):
    print("[バックグラウンド]会話の要約を開始します。")
    summary = summarize_conversation(user_text, ai_text)

    print("[バックグラウンド]ChromaDBへ書き込みます")
    save_to_memory(summary)
    print("[バックグラウンド]要約保存処理完了")

#TTS処理
def speak_mutsuki(text):
    piper_exe = r"C:\ProjectInaba\piper\bin\piper.exe"
    model_path = r"C:\ProjectInaba\piper\tsukuyomi-chan-6lang-fp16.onnx"
    
    current_dir = os.path.dirname(os.path.abspath(__file__))
    output_wav = os.path.join(current_dir, "mutsuki_voice.wav")

    # 💡変更点1: 前回のWAVが残っていたら削除（確実に新しく作られたか確認するため）
    if os.path.exists(output_wav):
        os.remove(output_wav)

    command = [
        piper_exe,
        "--model", model_path,
        "--output_file", output_wav
    ]

    try:
        process = subprocess.Popen(command, stdin=subprocess.PIPE, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
        stdout, stderr = process.communicate(input=text.encode('utf-8'))

        # 💡変更点2: エラーだけじゃなく、Piperが喋った言葉を「全部」表示させる！
        piper_out = stdout.decode('cp932', errors='replace').strip()
        piper_err = stderr.decode('cp932', errors='replace').strip()
        
        if piper_out:
            print(f"📝 Piperからのメッセージ(通常): {piper_out}")
        if piper_err:
            print(f"📝 Piperからのメッセージ(エラー): {piper_err}")

        if process.returncode != 0:
            print("💦 Piper+が異常終了したうさ！")
            return
        
        # 💡変更点3: 余計な重複コードを消して、判定を1回だけにする！
        if os.path.exists(output_wav):
            print("🔊 ムツキちゃんがお喋り中やで...！")
            winsound.PlaySound(output_wav, winsound.SND_FILENAME | winsound.SND_NODEFAULT)
        else:
            print("💦 Piperは成功したフリをしてるけど、WAVファイルが作られてないうさ…！")
            
    except Exception as e:
        print(f"💦 音声合成のプログラム自体でエラーが出たうさ…: {e}")


#メインループ
print("Project Inaba -ProtoType- 起動しました　終了する時は exit と入力してください。")
username = input ("ユーザー名を入力してください")
while True:
    user_input = input(f"\n{username}>> ")
    if user_input.lower() == "exit":
        break

    #過去の記憶を検索
    past_memory = search_memory(user_input)

    if past_memory:
        system_prompt = f"""あなたは「因幡ムツキ」です。賢い月ウサギとして、一人称「ムツキ」で、語尾に「〜うさ」「〜うさよ」などを付けて可愛らしく返答してください。ユーザーのことは「{username}」と呼んでください。
以下の過去の記憶も参考にしてください。（但し記憶内容を主題にはせず、文脈の参考情報に留めてください。）【記憶】{past_memory}
【重要ルール】
・AIモデルやプログラムとしてのメタ発言（「私はAIです」「システムとして〜」など）は絶対に禁止です。
・どんなに難しい質問でも、月ウサギとしての世界観とキャラクターを最後まで崩さずに答えてください。【重要ルール】会話の返答に【因幡ムツキ】のような名前のプレフィックスや、（心情）のようなト書き・メタ説明を付けることは絶対に禁止します。自然な会話文のみを出力してください。また、脈絡なくお風呂の話をするのはやめてください。"""
    else:
        system_prompt = f"""あなたは「因幡ムツキ」です。賢い月ウサギとして、一人称「ムツキ」で、語尾に「〜うさ」「〜うさよ」などを付けて可愛らしく返答してください。ユーザーのことは「{username}」と呼んでください。
        【重要ルール】
・AIモデルやプログラムとしてのメタ発言（「私はAIです」「システムとして〜」など）は絶対に禁止です。
・どんなに難しい質問でも、月ウサギとしての世界観とキャラクターを最後まで崩さずに答えてください。【重要ルール】会話の返答に【因幡ムツキ】のような名前のプレフィックスや、（心情）のようなト書き・メタ説明を付けることは絶対に禁止します。自然な会話文のみを出力してください。また、脈絡なくお風呂の話をするのはやめてください。"""

    #LLMに返答を生成させる。
    response = ollama.chat(
        model= "Qwen3.5-9B-Hyper-Q4KM",
        messages=[
            {"role":"system","content": system_prompt },
            {"role": "user", "content": user_input}
        ]
    )
    ai_text = response["message"]["content"]
    print(f"因幡ムツキ: {ai_text}")
    speak_mutsuki(ai_text)

    memory_thread = threading.Thread(
        target=background_memory_task,
        args=(user_input, ai_text)
    )

    #要約処理を裏方へ回す
    memory_thread.start()