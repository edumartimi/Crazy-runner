using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game Configuration")]
    public int target_FPS = 60;
    public GameObject imagem;

    private void Awake()
    {
        Application.targetFrameRate = target_FPS;
    }
  

    public void Play_Pause_game()
    {
        if (Time.timeScale == 1) 
        {
            Time.timeScale = 0;
            imagem.SetActive(true);
        }
        else 
        {
            Time.timeScale = 1;
            imagem.SetActive(false);
        }
    }

    public void reiniciar() 
    {
        SceneManager.LoadScene(0);
    }
   

}
