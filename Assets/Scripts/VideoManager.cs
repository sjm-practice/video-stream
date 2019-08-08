using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
  private VideoPlayer videoPlayer;

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

    vp.Play();
  }

  public void StreamVideoUrl(string url)
  {
    Debug.Log("(SJM) " + Time.time + " Video Play Requested (button click).");

    videoPlayer.url = url;
    videoPlayer.Prepare();
  }

  public void StopVideo()
  {
    videoPlayer.Stop();
  }
}
