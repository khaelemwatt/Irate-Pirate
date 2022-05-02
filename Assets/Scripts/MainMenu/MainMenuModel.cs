using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuModel : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject loadingScreen;
    public GameObject creditScreen;

    public AudioSource audioSource;
    public AudioClip mainMusic;

    public ref GameObject MainMenu(){
        return ref this.mainMenu;
    }

    public ref GameObject LoadingScreen(){
        return ref this.loadingScreen;
    }

    public ref GameObject CreditScreen(){
        return ref this.creditScreen;
    }

    public ref AudioSource AudioSource(){
        return ref this.audioSource;
    }

    public ref AudioClip MainMusic(){
        return ref this.mainMusic;
    }
}
