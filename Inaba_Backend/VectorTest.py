import ollama
import numpy as np

print("🐰 Ollama先生にベクトル化を依頼中やで...")

# 1. 比較したい言葉
text1 = "LLM"
text2 = "テッセレーション"
text3 = "大規模言語モデル"

# 2. OllamaのAPIを叩いて、bge-m3モデルで一瞬でベクトル（数字の配列）にする！
# （※ローカル通信やからAPIキーも何もいらん！最高やね！）
res1 = ollama.embeddings(model='bge-m3', prompt=text1)
res2 = ollama.embeddings(model='bge-m3', prompt=text2)
res3 = ollama.embeddings(model='bge-m3', prompt=text3)

vec1 = res1['embedding']
vec2 = res2['embedding']
vec3 = res3['embedding']

# 3. コサイン類似度を計算する関数（NumPyで数式をサクッと再現！）
def cos_sim(v1, v2):
    return np.dot(v1, v2) / (np.linalg.norm(v1) * np.linalg.norm(v2))

# 4. いざ比較！
sim_1_and_2 = cos_sim(vec1, vec2)
sim_1_and_3 = cos_sim(vec1, vec3)

print("\n📊 【Ollama + BGE-M3 の結果発表うさ！】")
print(f"「LLM」 と 「テッセレーション」 の類似度: {sim_1_and_2:.3f}")
print(f"「LLM」 と 「大規模言語モデル」 の類似度: {sim_1_and_3:.3f}")