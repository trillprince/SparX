using System.Collections;
using System.Collections.Generic;
using System.Text;
using Photon.Pun;
using Unity.Profiling;
using UnityEngine;

public class ProfilerWidgetComponent : MonoBehaviour
{
    private string text;
    private ProfilerRecorder totalReservedMemoryRecorder;
    private ProfilerRecorder gcReservedMemoryRecorder;
    private ProfilerRecorder textureMemoryRecorder;
    private ProfilerRecorder meshMemoryRecorder;

    private float fps;

    void OnEnable()
    {
        totalReservedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Reserved Memory");
        gcReservedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Reserved Memory");
        textureMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Texture Memory");
        meshMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Mesh Memory");
    }

    void OnDisable()
    {
        totalReservedMemoryRecorder.Dispose();
        gcReservedMemoryRecorder.Dispose();
        textureMemoryRecorder.Dispose();
        meshMemoryRecorder.Dispose();
    }

    void Update()
    {
        updateText();
    }

    void updateText()
    {
        var stringBuilder = new StringBuilder(500);
        if (totalReservedMemoryRecorder.Valid)
        {
            stringBuilder.AppendLine($"Total Reserved Memory: {toMegaBytes(totalReservedMemoryRecorder.LastValue)} Mb");
        }

        if (gcReservedMemoryRecorder.Valid)
        {
            stringBuilder.AppendLine($"GC Reserved Memory: {toMegaBytes(gcReservedMemoryRecorder.LastValue)} Mb");
        }

        if (textureMemoryRecorder.Valid)
        {
            stringBuilder.AppendLine($"Texture Used Memory: {toMegaBytes(textureMemoryRecorder.LastValue)} Mb");
        }

        if (meshMemoryRecorder.Valid)
        {
            stringBuilder.AppendLine($"Mesh Used Memory: {toMegaBytes(meshMemoryRecorder.LastValue)} Mb");
        }

        stringBuilder.AppendLine("FPS: " + fps);

        stringBuilder.AppendLine("Ping: " + PhotonNetwork.GetPing());

        text = stringBuilder.ToString();
    }

    IEnumerator Start()
    {
        GUI.depth = 2;
        while (true)
        {
            if (Mathf.Approximately(Time.timeScale, 1f))
            {
                yield return new WaitForSeconds(0.1f);
                fps = Mathf.Round(1 / Time.deltaTime);
            }
            else
            {
                fps = -1;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private long toMegaBytes(long bytes)
    {
        return bytes / 1000000;
    }

    void OnGUI()
    {
        GUI.TextArea(new Rect(10, 30, 250, 110), text);
    }
}