import chromadb
import ollama

print("🐰 ChromaDBのローカルデータベースを準備中やで...")

# 1. 完全にローカルなフォルダ（同じ階層に 'inaba_memory' フォルダが自動で作られる！）
client = chromadb.PersistentClient(path="./inaba_memory")

# 2. Inabaちゃんの記憶を入れる箱（コレクション）を作る
collection = client.get_or_create_collection(name="inaba_long_term_memory")

# 3. 保存したい要約テキストと、それをOllama(bge-m3)でベクトル化したもの
text_to_save = "なっちゃんは100%ローカル完結のAI環境を目指している。"
vector_data = ollama.embeddings(model='bge-m3', prompt=text_to_save)['embedding']

# 4. データベースにガシャン！と保存（ここでSQLite管理とベクトルが同時に記録されるんや！）
collection.upsert(
    ids=["memory_001"], # IDは適当な連番やタイムスタンプ文字列でOK！
    embeddings=[vector_data],
    documents=[text_to_save],
    metadatas=[{"date": "2026-03-24", "topic": "AI開発"}] # メタデータで後から絞り込みもできるで！
)
print("✨ 記憶の保存完了うさ！フォルダが作られてるか確認してみてな！")

# ==========================================
# 🔍 おまけ：記憶を思い出す（検索する）テスト
# ==========================================
print("\n🔍 なっちゃんなんか目指してたっけ...？って思い出すテスト")
search_query = "AIの環境構築の方針について"
query_vector = ollama.embeddings(model='bge-m3', prompt=search_query)['embedding']

# ベクトルを使って一番意味が近い記憶を1件引っ張ってくる！
results = collection.query(
    query_embeddings=[query_vector],
    n_results=1
)

# 検索結果には documents (テキスト) がそのまま入ってるから、Llamaくんにカンペとして渡せる！
print(f"🧠 思い出したテキスト: {results['documents'][0][0]}")