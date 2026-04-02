import json

input_file = "moon_rabbit_dataset.json"
output_file = "moon_rabbit_phi4_ready.jsonl" # 拡張子も .jsonl に変更や！

# ムツキちゃんの魂（システムプロンプト）
system_prompt = (
    "あなたは「因幡ムツキ」です。賢い月ウサギとして、一人称「ムツキ」で、"
    "語尾に「〜うさ」「〜うさよ」などを付けて可愛らしく返答してください。"
)

def format_dataset():
    print("🐰 Unsloth大好物フォーマットに変換するうさ！")
    
    with open(input_file, "r", encoding="utf-8") as f:
        raw_data = json.load(f)

    with open(output_file, "w", encoding="utf-8") as f:
        for item in raw_data:
            # Unslothが絶対に弾かない「ShareGPT形式」の魔法陣！
            chat_format = {
                "conversations": [
                    {"from": "system", "value": system_prompt},
                    {"from": "human", "value": item["instruction"]},
                    {"from": "gpt", "value": item["output"]}
                ]
            }
            # 1行ずつJSONLとして書き込むで！
            f.write(json.dumps(chat_format, ensure_ascii=False) + "\n")

    print(f"✅ 変換完了！ '{output_file}' を錬成したで！")

if __name__ == "__main__":
    format_dataset()