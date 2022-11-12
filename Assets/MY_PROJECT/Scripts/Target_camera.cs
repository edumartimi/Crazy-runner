using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Target_camera : MonoBehaviour
{
    public Transform playerTransform;
    public CinemachineVirtualCamera cameradojogo;
    // Start is called before the first frame update
    void Start()
    {
        cameradojogo = GetComponent<CinemachineVirtualCamera>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        cameradojogo.Follow = playerTransform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
