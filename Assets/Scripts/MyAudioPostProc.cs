using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MyAudioPostProc : AssetPostprocessor
{
    private void OnPreprocessAudio()
    {
        Debug.Log("Import");
        var audioImporter = assetImporter as AudioImporter;
        if (audioImporter == null) return;

        audioImporter.forceToMono = true;
        audioImporter.preloadAudioData = true;
        audioImporter.loadInBackground = true;
        
        var fileInfo = new FileInfo(audioImporter.assetPath);
        Debug.Log(assetImporter.assetPath);

        var fileSizeInKb = fileInfo.Length / 1000;
        Debug.Log(fileSizeInKb);

        AudioImporterSampleSettings sampleSettings = audioImporter.defaultSampleSettings;
        
        if (fileSizeInKb < 200 )
        {
            sampleSettings.loadType = AudioClipLoadType.DecompressOnLoad;
            Debug.Log("Decompressed");
        }
        else if(fileSizeInKb > 200 && fileSizeInKb < 3000)
        {
            sampleSettings.loadType = AudioClipLoadType.CompressedInMemory;
            Debug.Log("Compressed");
        }
        else if(fileSizeInKb > 3000)
        {
            sampleSettings.loadType = AudioClipLoadType.Streaming;
            Debug.Log("Streaming");
        }
        
        audioImporter.defaultSampleSettings = sampleSettings;
    }
}
