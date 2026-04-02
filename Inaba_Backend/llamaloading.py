from unsloth import FastLanguageModel
import torch

max_seq_length = 2048 # デスクトップでのお喋りならこれくらいで十分！
dtype = None # 自動設定にお任せ
load_in_4bit = True # VRAMを節約する必須の魔法！

print("🐰 Inabaちゃんのベース脳みそ（Llama 3.2 3B）をダウンロード＆読み込み中...")

model, tokenizer = FastLanguageModel.from_pretrained(
    model_name = "unsloth/Llama-3.2-3B-Instruct", # Llama 3.2 3Bの賢いバージョン！
    max_seq_length = max_seq_length,
    dtype = dtype,
    load_in_4bit = load_in_4bit,
)

# 【重要】ここでLlama 3用のチャットテンプレートを自動適用する魔法！
model, tokenizer = FastLanguageModel.setup_chat_format(model, tokenizer)

print("✨ 読み込み完了うさ！Inabaちゃんの準備バッチリや！")