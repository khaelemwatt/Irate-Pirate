using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    PauseModel pauseModel;

    public void Start(){
        pauseModel = gameObject.GetComponent<PauseModel>();
    }   

    // Update is called once per frame
    void Update()
    {
        ref bool isPaused = ref pauseModel.IsPaused();
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                ResumeGame();
            }else{
                PauseGame();
            }

        }
    }

    public void ResumeGame(){
        ref GameObject menu = ref pauseModel.Menu();
        ref bool isPaused = ref pauseModel.IsPaused();
        menu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void PauseGame(){
        ref GameObject menu = ref pauseModel.Menu();
        ref bool isPaused = ref pauseModel.IsPaused();
        menu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ReturnToMainMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void DeathScreen(){
        ref GameObject deathMenu = ref pauseModel.DeathMenu();
        Time.timeScale = 0f;
        deathMenu.SetActive(true);
    }
}
