# video-stream

A simple Unity project to troubleshoot and test streaming a video using VideoPlayer.

## Background

We are experiencing audio loss, when streaming _some_ videos via URL. This typically occurs with videos of certain lengths, and over a cellular or poor / slow connection. The issue has been mostly tested on iOS, but reported on Android as well.

This project / repo was created to isolate any causes, and test potential solutions.

### To Reproduce Audio Loss Issue

* turn WiFi off for the mobile device (ensure using cellular or a slow connection)
* build and run the project on an iOS device
* press the play button on the app
* a video will begin to play after some seconds (allow for buffering)
* the video begins to play successfully with audio
* after 5 to 14 seconds, depending on connection, audio will cut out, video will continue to play
  - you _may_ notice some video frames dropping, or stuttering

NOTE: it is a 33 second video (URL below). the video is set to play on loop. after the first (or sometimes second) complete play of the video, the audio no longer cuts out.

## Notes
* Current Unity Editor version is 2019.1.12f1
* __CODE: the current implemented functionality, simply calls '.Play()' on button press and does not use '.Prepare()'. I am implementing '.Prepare()' now, and uploading it shortly. Our main project does use '.Prepare()' and still experiences the audio loss.__
* Here is a video we experience audio loss with most often
   - https://avoinsights.com/dashboard/uploads/videos/46070F26-706A-467D-B800-D22584942A0F.mp4
