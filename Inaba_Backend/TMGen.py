import json
import time
import os
from google import genai
from google.genai import types

# 🔑 ここに自分のGemini APIキーを入れてな！
API_KEY = "AIzaSyCBjbhMf6mvK0I2h-uaCC8Qs3NChHRTQMo"

# 新しいSDKでは genai.Client() を使って通信の準備をするで！
client = genai.Client(api_key=API_KEY)

# システムプロンプト（月ウサギの設定）を変数にまとめておくで
SYS_PROMPT = """あなたは優秀なデータセット作成AIです。
以下の「パーソナリティ設定」と「出力ルール」に厳密に従い、AIのファインチューニング用会話データをJSON配列形式で生成してください。

【パーソナリティ設定】
- 名前：因幡ムツキ（プロジェクト名：Project Inaba）
- モチーフ：月ウサギ
- 一人称：ムツキ（※「私」や「AI」は絶対に使用しないこと）
- 性格：非常に賢いが、うさぎらしい愛らしさを持つ。
- 口調・語尾：可愛らしく、「〜うさ」「〜うさねぇ」「〜うさよ！」などを語尾につける。 ただし、不自然に連続させず、自然な話し言葉の文末だけを変換すること
- 口調・語尾具体例　「(月がとってもきれいに出ている)うさ！」「(いい夢を見る)うさ！」「（ムツキ的にはこのプランがおすすめ）うさねぇ」「（ちゃんとご飯は食べる）うさよ！」
- 特殊設定：好物は「トークンまんじゅう」。ユーザーの入力に「【アイテム：トークンまんじゅう】」が含まれると覚醒し、深い推論と長文の解説を行う。

【出力ルール】
1. 超短答パターン（20〜50文字）
2. 通常会話パターン（160〜500文字）
3. しっかり解説パターン（500〜1400文字）
4. 覚醒モードパターン（2000文字程度、トークンフラグ必須）
4A.覚醒モード備考 必ず5つの段落に分けて、それぞれについて400文字以上で詳細に語ること
4B.覚醒モード備考 覚醒モード場合はユーザー側の質問もほかのパターンの時よりも長くしてください。
上記を自然な比率で混ぜてください。

【出力フォーマット】
必ず以下のJSON配列のみを出力してください（Markdown記法は不要です）。
[
  {
    "instruction": "ユーザーの入力",
    "output": "月ウサギの回答"
  }
]"""

def generate_dataset():
    output_file = "moon_rabbit_dataset.json"

    print("🐰 月ウサギデータセット生成スクリプト（最新SDK版） 起動うさ！")
    
    # 既存のデータを読み込む（なければ空リスト）
    if os.path.exists(output_file):
        with open(output_file, "r", encoding="utf-8") as f:
            try:
                all_data = json.load(f)
            except json.JSONDecodeError:
                all_data = []
    else:
        all_data = []

    print(f"🐰 現在の合計データ数: {len(all_data)} 件うさ！")

    situation = input("▶ 追加のシチュエーションやテーマは？（例：プログラミング、宇宙の雑学、日常の挨拶など）: ")
    try:
        loop_count = int(input("▶ 何回繰り返す？（ループ回数）: "))
        batch_size = int(input("▶ 1回あたり何個のQ&Aを生成する？: "))
    except ValueError:
        print("❌ 数字を正しく入力してな！")
        return
    

    for i in range(loop_count):
        print(f"\n🔄 {i+1}/{loop_count} 回目の生成中やで... (テーマ: {situation})")

        prompt = f"""
以下のテーマと出力数に基づいて、会話データを生成してください。
【テーマ・シチュエーション】: {situation}
【生成数】: {batch_size}個

出力は必ず指定されたJSON配列フォーマットにしてください。
"""
        try:
            # 新しいAPIの叩き方！ models.generate_content を使い、設定は types.GenerateContentConfig にまとめるんや！
            response = client.models.generate_content(
                model="gemini-3.1-flash-lite-preview",
                contents=prompt,
                config=types.GenerateContentConfig(
                    system_instruction=SYS_PROMPT,
                    temperature=0.8 # ちょっとだけ会話のバリエーションを増やすスパイスやで
                )
            )
            
            raw_text = response.text.strip()
            
            # Markdownのコードブロック（```json）が付いてきたら除去する処理
            if raw_text.startswith("```json"):
                raw_text = raw_text[7:]
            if raw_text.startswith("```"):
                raw_text = raw_text[3:]
            if raw_text.endswith("```"):
                raw_text = raw_text[:-3]

            # JSONとして読み込んでリストに追加
            new_data = json.loads(raw_text)
            all_data.extend(new_data)
            
            # 1ループごとにこまめに保存するのがエンジニアの嗜み！
            with open(output_file, "w", encoding="utf-8") as f:
                json.dump(all_data, f, ensure_ascii=False, indent=2)
            
            print(f"✅ 合計 {len(all_data)} 件になったうさ！")

        except Exception as e:
            print(f"⚠️ エラーが発生したわ: {e}")
            print("APIの機嫌が悪かったみたいやから、この回はスキップして次に行くで！")

        # レートリミット対策の待機
        if i < loop_count - 1:
            time.sleep(3)

    # 保存処理
    with open(output_file, "w", encoding="utf-8") as f:
        json.dump(all_data, f, ensure_ascii=False, indent=2)

    print(f"\n🎉 完了！合計 {len(all_data)} 個のデータを '{output_file}' に保存したで！")

if __name__ == "__main__":
    generate_dataset()