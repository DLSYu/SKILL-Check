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
    private string condaPath = "D:/Miniconda";
    // private string condaPath = "/Users/hanzpatrickyu/miniconda3";
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
        var workingDirectory = Path.Combine(Application.dataPath, "Resources", "PartsOfSpeech");

        string pythonExe = IsWindows()
            ? Path.Combine(condaPath, "envs", envName, "python.exe")
            : $"{condaPath}/envs/{envName}/bin/python";

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = pythonExe,
                Arguments = $"\"{scriptPath}\" \"{text}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = workingDirectory,
            }
        };

        try
        {
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (!string.IsNullOrWhiteSpace(output))
                UnityEngine.Debug.Log($"Python Output:\n{output}");

            if (!string.IsNullOrWhiteSpace(error))
                UnityEngine.Debug.LogError($"Python Error:\n{error}");

            // Parse output
            List<string> lines = new();
            using (StringReader reader = new StringReader(output))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("VALUE "))
                    {
                        string[] splitLine = line.Split(' ').Skip(1).ToArray();
                        string result = string.Join(' ', splitLine);
                        lines.Add(result);
                    }
                }
            }

            string title = GetTitle(text);
            File.WriteAllLines(Path.Combine(workingDirectory, $"{title}.txt"), lines);
            UnityEngine.Debug.Log($"Wrote POS to {title}.txt");
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.LogError($"Error running Python script: {ex.Message}");
        }
    }


    private bool IsWindows()
    {
        return SystemInfo.operatingSystemFamily == OperatingSystemFamily.Windows;
    }

    private string GetTitle(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        // Convert escaped newlines ("\n" as text) into actual newlines
        input = input.Replace("\\n", "\n");

        // Normalize newlines
        input = input.Replace("\r\n", "\n");

        // Find the first occurrence of a double newline
        int index = input.IndexOf("\n\n");

        string title = index != -1 ? input.Substring(0, index) : input;

        // Remove invalid filename characters
        char[] invalidChars = Path.GetInvalidFileNameChars();
        title = new string(title.Where(c => !invalidChars.Contains(c)).ToArray());

        // Truncate title length
        const int maxLength = 50;
        if (title.Length > maxLength)
        {
            title = title.Substring(0, maxLength);
        }

        return title.Trim();
    }



}