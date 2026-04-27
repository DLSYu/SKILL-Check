using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

[System.Serializable]
public class ScoreRequest
{
    public string[] candidates;
    public string[] references;
}
[System.Serializable]
public class ScoreResponse
{
    public float[] f1;
}

public class BERTClient
{
    const int PORT = 65432;

    public static float[] ScoreBatch(string[] candidates, string[] references)
    {
        TcpClient client = new TcpClient("127.0.0.1", PORT);

        NetworkStream stream = client.GetStream();

        ScoreRequest req = new ScoreRequest
        {
            candidates = candidates,
            references = references
        };

        string json = JsonUtility.ToJson(req);
        byte[] data = Encoding.UTF8.GetBytes(json);

        stream.Write(data, 0, data.Length);

        byte[] buffer = new byte[8192];
        int bytes = stream.Read(buffer, 0, buffer.Length);

        string responseJson = Encoding.UTF8.GetString(buffer, 0, bytes);

        ScoreResponse resp = JsonUtility.FromJson<ScoreResponse>(responseJson);

        client.Close();

        return resp.f1;
    }
}