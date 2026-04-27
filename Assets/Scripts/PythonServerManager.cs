using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class PythonServerManager : MonoBehaviour
{
    private static PythonServerManager instance;
    private Process pythonProcess;

    const int PORT = 65432;

    public static PythonServerManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("PythonServerManager");
                instance = obj.AddComponent<PythonServerManager>();
                DontDestroyOnLoad(obj);
            }

            return instance;
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        EnsureServerRunning();
    }

    void EnsureServerRunning()
    {
        if (!IsServerAlive())
        {
            StartPythonServer();
        }
    }

    bool IsServerAlive()
    {
        try
        {
            TcpClient client = new TcpClient();
            client.Connect("127.0.0.1", PORT);
            client.Close();
            return true;
        }
        catch
        {
            return false;
        }
    }

    void StartPythonServer()
    {
        string basePath = Application.streamingAssetsPath;

        string python = Path.Combine(basePath, "python/python.exe");
        string script = Path.Combine(basePath, "bert_server.py");

        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = python,
            Arguments = $"\"{script}\"",
            WorkingDirectory = basePath,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        pythonProcess = Process.Start(psi);

        StartCoroutine(WaitForServer());
    }

    System.Collections.IEnumerator WaitForServer()
    {
        float timeout = 20f;
        float timer = 0f;

        while (timer < timeout)
        {
            if (IsServerAlive())
            {
                UnityEngine.Debug.Log("Python server ready");
                yield break;
            }

            timer += 0.5f;
            yield return new WaitForSeconds(0.5f);
        }

        UnityEngine.Debug.LogError("Python server failed to start");
    }

    void OnApplicationQuit()
    {
        Shutdown();
    }

    void OnDestroy()
    {
        Shutdown();
    }

    void Shutdown()
    {
        if (pythonProcess != null && !pythonProcess.HasExited)
        {
            pythonProcess.Kill();
            pythonProcess.Dispose();
        }
    }
}