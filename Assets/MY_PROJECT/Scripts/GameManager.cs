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
    public int numeroPlayer;
    public GameObject btn_reiniciar;

    public GameObject[] Jogadores;

    private void Awake()
    {
        Instantiate(Jogadores[numeroPlayer],this.transform);
        Application.targetFrameRate = target_FPS;
    }
  

    public void Play_Pause_game()
    {
        if (Time.timeScale == 1) 
        {
            Time.timeScale = 0;
            btn_reiniciar.SetActive(true);
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
        SceneManager.LoadScene(1);
    }

    public void iraomenu() 
    {
        SceneManager.LoadScene(0);
    }

   

}
