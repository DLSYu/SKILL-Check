
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PartOfSpeechGen : MonoBehaviour
{
    private string scriptPath = Application.dataPath + "/PythonScripts/pos-gen.py";
    private string condaPath = "D:/Programs/Miniconda";
    private string envName = "spacy";

    [ContextMenu("GeneratePOS")]
    public void GeneratePOS()
    {
        List<TextMeshInputHelper> relevantText = FindObjectsByType<TextMeshInputHelper>(FindObjectsSortMode.InstanceID).ToList();
        foreach (var text in relevantText)
        {
            GeneratePartOfSpeech(text.GetComponent<TextMeshProUGUI>().text);

        }
    }

    private void GeneratePartOfSpeech(string text)
    {
        var workingDirectory = Path.GetFullPath(Application.dataPath + "/Resources/PartsOfSpeech");
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = workingDirectory,
                CreateNoWindow = true,
            }
        };
        process.Start();

        using (var sw = process.StandardInput)
        {
            if (sw.BaseStream.CanWrite)
            {
                sw.WriteLine($"cmd.exe /K {condaPath}/Scripts/activate.bat {condaPath}"); // change this later depending on your path
                sw.WriteLine($"conda activate {envName}"); // change this later depending on your environment file
            }
            sw.WriteLine($"python {scriptPath} \"{text}\"");
            sw.Flush();
        }

        List<string> lines = new();
        string line = process.StandardOutput.ReadLine();
        while (line != null && !line.Contains("PROCESS DONE"))
        {
            line = process.StandardOutput.ReadLine();
            if (line != null && line.StartsWith("VALUE"))
            {
                string[] splitLine = line.Split(' ').Skip(1).ToArray();
                string result = string.Join<string>(' ', splitLine);
                lines.Add(result);
            }
        }
        File.WriteAllLines(workingDirectory + $"/{text}.txt", lines);
        process.WaitForExit();
    }
}