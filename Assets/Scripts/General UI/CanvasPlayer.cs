using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CanvasPlayer : MonoBehaviour{
    public VideoPlayer _videoPlayer;
     public RawImage _rawImage;
    void Start(){
        StartCoroutine(PlayVideo());
    }
    IEnumerator PlayVideo(){
        _videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
          while (!_videoPlayer.isPrepared)
          {
               yield return waitForSeconds;
               break;
          }
        _videoPlayer.Play();
        _rawImage.texture = _videoPlayer.texture;
    }
}
