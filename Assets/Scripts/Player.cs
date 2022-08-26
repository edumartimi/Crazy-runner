using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    Vector3 ir_para_frente;

    public float tempo;

    public float velocidade;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Time.timeScale = 3;
    }

    // Update is called once per frame
    void Update()
    {
        ir_para_frente = new Vector3(rb.velocity.x,rb.velocity.y,1*velocidade);

        rb.velocity = ir_para_frente;

        tempo = tempo + Time.deltaTime;
        if (tempo > 1)
        {
            tempo = 0;
            if (Time.timeScale < 25)
            {
                Time.timeScale = Time.timeScale + 0.0001f;
            }

        }
        print(Time.timeScale);

    }

    
    

}
