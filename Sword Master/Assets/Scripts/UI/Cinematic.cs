using UnityEngine;
using System.Collections;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Cinematic : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Animator buttonanim;
    public Animator blackanim;

    void Start()
    {
        videoPlayer.loopPointReached += CinematicEnd;
        videoPlayer.Play();
    }
    void Update()
    {
        if (Input.anyKeyDown)
        {
          buttonanim.SetBool("Apear", true);
          if(Input.GetKeyDown(KeyCode.Escape))
          {
            SceneManager.LoadScene("0 Menu");
          }
        }
    }
    void CinematicEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("0 Menu");
    }
}

