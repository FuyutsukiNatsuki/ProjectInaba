from fastapi import FastAPI, BackgroundTasks
from pydantic import BaseModel
import ollama
import chromadb
import datetime
import subprocess
import os
import re

print("Project Inaba -API Server- is initializing...")

# FastAPIアプリの初期化
app = FastAPI()

# 📥 Unityから受け取るデータの形（名前も送れるようにしたで！）
class ChatRequest(BaseModel):
    user_name: str = "なっちゃん" 
    user_text: str

# ChromaDBの初期化
client = chromadb.PersistentClient(path="./inaba_memory")
collection = client.get_or_create_collection(name="inaba_long_term_memory")

# --- 記憶関連の関数 ---
def search_memory(user_input):
    query_vector = ollama.embeddings(model="bge-m3", prompt=user_input)["embedding"]
    results = collection.query(query_embeddings=[query_vector], n_results=1)

    if results["documents"] and len(results["documents"][0]) > 0:
        return results["documents"][0][0]
    return ""

def summarize_conversation(user_text, ai_text):
    prompt = f"次の会話から、重要な名詞やトピックだけを３つ抽出し、短い要約文を作って。\nユーザー：{user_text}\nInaba：{ai_text}"
    res = ollama.chat(model="gemma2:2b", messages=[{"role": "user", "content":prompt}])
    return res["message"]["content"]

def save_to_memory(summary_text):
    vector = ollama.embeddings(model="bge-m3", prompt=summary_text)["embedding"]
    memory_id ="mem_" + datetime.datetime.now().strftime("%Y%m%d%H%M%S")
    collection.upsert(ids=[memory_id], embeddings=[vector], documents=[summary_text])
    print("🧠 裏処理: 新規記憶をDBに保存しました。")

def background_memory_task(user_text, ai_text):
    print("⏳ [バックグラウンド]会話の要約を開始します。")
    summary = summarize_conversation(user_text, ai_text)
    print("⏳ [バックグラウンド]ChromaDBへ書き込みます")
    save_to_memory(summary)
    print("✅ [バックグラウンド]要約保存処理完了！")


# --- 音声生成関数（API用に改修：鳴らさずにパスだけ返す！） ---
def generate_audio(text):
    piper_exe = r"C:\MY_GIT_PROJECTS\ProjectInaba\Inaba_Backend\piper\bin\piper.exe"
    model_path = r"C:\MY_GIT_PROJECTS\ProjectInaba\Inaba_Backend\piper\tsukuyomi-chan-6lang-fp16.onnx"
    
    current_dir = os.path.dirname(os.path.abspath(__file__))
    output_wav = os.path.join(current_dir, "mutsuki_voice.wav")

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

        if process.returncode != 0:
            piper_err = stderr.decode('cp932', errors='replace').strip()
            print(f"💦 Piper+が異常終了したうさ！: {piper_err}")
            return "エラー: 音声生成失敗"
        
        if os.path.exists(output_wav):
            return output_wav
        else:
            return "エラー: WAVファイルが作られてないうさ…"
            
    except Exception as e:
        return f"エラー: 音声合成のプログラム自体で例外発生 {e}"


# --- 🚀 APIエンドポイント（Unityからの通信受け口） ---
@app.post("/api/chat")
def chat_endpoint(request: ChatRequest, background_tasks: BackgroundTasks):
    user_input = request.user_text  # ← ここを user_text に！
    username = request.user_name    # ← ここを user_name に！
    print(f"\n📩 Unityからの通信を受信 [{username}]: {user_input}")

    # 1. 過去の記憶を検索
    past_memory = search_memory(user_input)

    # 2. プロンプトの組み立て
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

    # 3. LLMに返答を生成させる
    response = ollama.chat(
        model= "gemma4:E4B-IT-Q4KM",
        messages=[
            {"role":"system","content": system_prompt },
            {"role": "user", "content": user_input}
        ]
    )
    ai_text = response["message"]["content"]
    
    # R1の名残で<think>タグが出た場合のお掃除
    clean_text = re.sub(r'<think>.*?</think>', '', ai_text, flags=re.DOTALL).strip()
    print(f"🐰 因幡ムツキ: {clean_text}")

    # 4. 音声の生成（パスを取得）
    audio_path = generate_audio(clean_text)
    print(f"🎵 音声パス: {audio_path}")

    # 5. 【超重要】記憶の要約＆保存をバックグラウンドタスクに登録！
    # これにより、記憶処理を待たずにUnityへすぐ返事ができるんや！
    background_tasks.add_task(background_memory_task, user_input, clean_text)

    # 6. Unityへ結果を返却！
    return {
        "ai_text": clean_text,
        "audio_url": audio_path
    }