using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Networking;

public class VideoManager : MonoBehaviour
{
  public GameObject objLoading;
  private VideoPlayer videoPlayer;
  private string downloadedVideoFile;

  void Awake()
  {
    objLoading.SetActive(false);

    downloadedVideoFile = Path.Combine(Application.persistentDataPath, "avovideo.mp4");
  }
  // Start is called before the first frame update
  void Start()
  {
    videoPlayer = GetComponent<VideoPlayer>();

    videoPlayer.errorReceived += ErrorReceived;
    videoPlayer.prepareCompleted += PrepareCompleted;
  }

  void ErrorReceived(VideoPlayer vp, string message)
  {
    Debug.Log("(SJM) " + "*** ERROR RECEIVED ***: " + message);
  }

  void PrepareCompleted(VideoPlayer vp)
  {
    Debug.Log("(SJM) " + Time.time + " Video Prepare Completed.");
    Debug.Log("(SJM) " + " ");
    Debug.Log("(SJM) " + "video URL: " + vp.url);
    Debug.Log("(SJM) " + "audio output mode: " + vp.audioOutputMode.ToString());
    Debug.Log("(SJM) " + "isPrepared: " + vp.isPrepared.ToString());
    Debug.Log("(SJM) " + "frame: " + vp.frame.ToString());
    Debug.Log("(SJM) " + "video frame count: " + vp.frameCount.ToString());
    Debug.Log("(SJM) " + "audio track count: " + vp.audioTrackCount.ToString());
    Debug.Log("(SJM) " + "length (seconds): " + vp.length.ToString());
    Debug.Log("(SJM) " + "playOnAwake: " + vp.playOnAwake.ToString());
    Debug.Log("(SJM) " + "waitForFirstFrame: " + vp.waitForFirstFrame.ToString());
    Debug.Log("(SJM) " + "frameRate: " + vp.frameRate.ToString());
    Debug.Log("(SJM) " + "GetAudioChannelCount: " + vp.GetAudioChannelCount(0).ToString());
    Debug.Log("(SJM) " + "IsAudioTrackEnabled: " + vp.IsAudioTrackEnabled(0).ToString());
    Debug.Log("(SJM) " + " ");

    objLoading.SetActive(false);

    vp.Play();
  }

  public void StreamVideoUrl(string url)
  {
    Debug.Log("(SJM) " + Time.time + " Video Play Requested (button click).");

    // videoPlayer.url = url;
    // videoPlayer.Prepare();

    objLoading.SetActive(true);
    StartCoroutine(DownloadVideo(url));
  }

  public void StopVideo()
  {
    videoPlayer.Stop();
    videoPlayer.targetTexture.Release();
  }

  IEnumerator DownloadVideo(string url)
  {
    float startDownloadTime = Time.time;

    Debug.Log("(SJM) " + startDownloadTime + " downloading file: " + url);

    if (File.Exists(downloadedVideoFile))
    {
      File.Delete(downloadedVideoFile);
    }

    var uwr = new UnityWebRequest(url);
    uwr.method = UnityWebRequest.kHttpVerbGET;
    var dh = new DownloadHandlerFile(downloadedVideoFile);
    dh.removeFileOnAbort = true;
    uwr.downloadHandler = dh;
    yield return uwr.SendWebRequest();
    if (uwr.isNetworkError || uwr.isHttpError)
      Debug.Log(uwr.error);
    else
    {
      Debug.Log("(SJM) download time:" + (Time.time - startDownloadTime) + "Download saved to: " + downloadedVideoFile);

      videoPlayer.url = downloadedVideoFile;
      videoPlayer.Prepare();
    }
  }
}
