using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class video : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;
    [SerializeField]
    private string videoFileName;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        if (videoPlayer)
        {
            videoPlayer.url = Application.streamingAssetsPath + "/" + "Samenurai gameplay.mp4";
            videoPlayer.Prepare();
            videoPlayer.Play();
        }
    }
}
