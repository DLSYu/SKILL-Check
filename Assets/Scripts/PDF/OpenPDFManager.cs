using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Android;

using UnityEngine.Networking;

public class OpenPDFScript : MonoBehaviour
{

    public void OpenPretestPDF()
    {
        StartCoroutine(CopyAndOpenPDF("1_english_BT_pretest.pdf"));

    }
    public void OpenPosttestPDF()
    {
        StartCoroutine(CopyAndOpenPDF("1_english_BT_posttest.pdf"));

    }
    IEnumerator CopyAndOpenPDF(string pdfFilename)
    {
        string sourcePath = Path.Combine(Application.streamingAssetsPath, pdfFilename);
        string destPath = Path.Combine(Application.persistentDataPath, pdfFilename);

        UnityWebRequest www = UnityWebRequest.Get(sourcePath);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            File.WriteAllBytes(destPath, www.downloadHandler.data);
            Debug.Log("PDF copied to: " + destPath);
            // Now you can pass destPath to your PDF plugin

            Debug.Log(Path.Combine(Application.streamingAssetsPath, pdfFilename));
            AndroidContentOpenerWrapper.OpenContent(Path.Combine(Application.persistentDataPath, pdfFilename));
        }
        else
        {
            Debug.LogError("Failed to load PDF from StreamingAssets: " + www.error);
        }
    }
}
