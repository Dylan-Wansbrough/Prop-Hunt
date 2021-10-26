using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    public GameObject musicPlayer = null;

    private void Awake()
    {
        musicPlayer = GameObject.Find("Music");
        if (musicPlayer == null)
        {
            musicPlayer = this.gameObject;
            musicPlayer.name = "Music";
            DontDestroyOnLoad(musicPlayer);
        }
        else if (this.gameObject.name != "Music")
        {
            Destroy(this.gameObject);
        }
    }
}