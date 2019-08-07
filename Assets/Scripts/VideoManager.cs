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
    if (!videoPlayer)
    {
      Debug.Log("(SJM) VideoPlayer component NOT found!!");
    }
  }

  public void StreamVideoUrl(string url)
  {
    videoPlayer.url = url;
    videoPlayer.Play();
  }

  public void StopVideo()
  {
    videoPlayer.Stop();
  }
}
