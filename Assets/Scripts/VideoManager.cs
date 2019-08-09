using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.Networking;

public class VideoManager : MonoBehaviour
{
  public GameObject objLoading;
  public Text downloadTimeDisplay;
  private VideoPlayer videoPlayer;
  private string downloadedVideoFile;
  private float videoPrepTime;

  private bool runTimer;

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

  void Update()
  {
    if (runTimer)
    {
      videoPrepTime += Time.deltaTime;

      int seconds = (int)(videoPrepTime % 60);
      int minutes = (int)(videoPrepTime / 60) % 60;
      int hours = (int)(videoPrepTime / 3600) % 24;

      string formattedTime = string.Format("{0:0}:{1:00}:{2:00}", hours, minutes, seconds);

      downloadTimeDisplay.text = formattedTime;
    }
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
    runTimer = false;

    vp.Play();
  }

  public void StreamVideoUrl(string url)
  {
    Debug.Log("(SJM) " + Time.time + " Video Stream / Play Requested (button click).");

    objLoading.SetActive(true);
    runTimer = true;
    videoPrepTime = 0f;

    videoPlayer.url = url;
    videoPlayer.Prepare();
  }

  public void DownloadAndPlayVideoUrl(string url)
  {
    Debug.Log("(SJM) " + Time.time + " Video Download and Play Requested (button click).");

    objLoading.SetActive(true);
    runTimer = true;
    videoPrepTime = 0f;

    StartCoroutine(DownloadVideoAndPrepare(url));
  }

  public void StopVideo()
  {
    videoPlayer.Stop();
    videoPlayer.targetTexture.Release();
  }

  IEnumerator DownloadVideoAndPrepare(string url)
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
      Debug.Log("(SJM) " + uwr.error);
    else
    {
      Debug.Log("(SJM) download time:" + (Time.time - startDownloadTime) + "Download saved to: " + downloadedVideoFile);

      videoPlayer.url = downloadedVideoFile;
      videoPlayer.Prepare();
    }
  }
}
