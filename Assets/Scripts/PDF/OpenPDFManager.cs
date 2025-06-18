using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Android;

using UnityEngine.Networking;

public class OpenPDFScript : MonoBehaviour
{

    public void OpenPDF()
    {
        StartCoroutine(CopyAndOpenPDF("shrek.pdf"));
        
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

            Debug.Log(Path.Combine(Application.streamingAssetsPath, "shrek.pdf"));
            AndroidContentOpenerWrapper.OpenContent(Path.Combine(Application.persistentDataPath, "shrek.pdf"));
        }
        else
        {
            Debug.LogError("Failed to load PDF from StreamingAssets: " + www.error);
        }
    }
}
