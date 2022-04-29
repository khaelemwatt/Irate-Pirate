using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Main Menu was made with help from https://www.youtube.com/watch?v=zc8ac_qUXQY
public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip MainMusic;

    void Start(){
        audioSource.clip = MainMusic;
        audioSource.Play();
    }

    public void Play(){        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
