using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ima : MonoBehaviour
{
    Player jogador;

    private void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        jogador.IMADEMOEDAS = true; 
    }


}
