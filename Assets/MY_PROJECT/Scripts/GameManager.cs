using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Configuration")]
    public int target_FPS = 60;

    private void Awake()
    {
        Application.targetFrameRate = target_FPS;
    }

}
