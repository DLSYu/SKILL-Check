import os
import json
import socket
from bert_score import score

BASE_DIR = os.path.dirname(__file__)

os.environ["TRANSFORMERS_OFFLINE"] = "1"
os.environ["HF_HOME"] = os.path.join(BASE_DIR, "hf_cache")
os.environ["HUGGINGFACE_HUB_CACHE"] = os.path.join(BASE_DIR, "hf_cache")

print("Loading BERT model...")

score(["hello"], ["hello"], model_type="bert-base-uncased", lang="en")

print("BERT loaded")

HOST = "127.0.0.1"
PORT = 65432

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.bind((HOST, PORT))
server.listen()

print("Server ready")

while True:
    conn, addr = server.accept()

    with conn:
        data = conn.recv(65536)

        if not data:
            continue

        req = json.loads(data.decode())

        candidates = req["candidates"]
        references = req["references"]

        P, R, F1 = score(
            candidates,
            references,
            model_type="bert-base-uncased",
            lang="en"
        )

        result = {
            "f1": [float(x) for x in F1]
        }

        conn.sendall(json.dumps(result).encode())