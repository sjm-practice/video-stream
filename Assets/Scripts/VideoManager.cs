using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoManager : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {

  }

  public void StreamVideoUrl(string url)
  {
    Debug.Log("stream video:" + url);
  }
}
