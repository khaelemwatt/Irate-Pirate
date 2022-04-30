using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Main Menu was made with help from https://www.youtube.com/watch?v=zc8ac_qUXQY

public class MainMenuController : MonoBehaviour
{
    MainMenuModel menuModel;

    void Start(){
        menuModel = gameObject.GetComponent<MainMenuModel>();
        ref AudioSource audioSource = ref menuModel.AudioSource();
        ref AudioClip mainMusic = ref menuModel.MainMusic();

        audioSource.clip = mainMusic;
        audioSource.Play();
    }

    public void Play(){     
        ref GameObject mainMenu = ref menuModel.MainMenu();
        ref GameObject loadingScreen = ref menuModel.LoadingScreen();
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
