using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    Vector3 ir_para_frente;

    public float tempo;

    public float velocidade;

    public Animator animador;
    public float velocidade_mov; 

    public TMP_Text pontuacao;

    float tempopontuacao;

    Vector3 andarfrente;
    Vector3 andaresquerda;
    Vector3 andardireita;
    Vector3 andarmeio;
    Vector3 ir_para_baixo;

    Vector3 pulo;
    public float posicao;
    public float forcapulo;
    public float graviteforce;
    bool tanochao;


    float tempoanimacao;
    bool direita;
    bool esquerda;
    bool noar;



    int estado;
    bool morte;


    private Vector2 startTouchPosition;
    private Vector2 currentPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch = false;

    public float swipeRange;
    public float tapRange;

    bool pulando;

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "chao") 
        {
        estado = 0;
        }

        if(collision.gameObject.tag == "obstaculo") 
        {
            morte = true;
            animador.SetTrigger("morte");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "chao")
        {
            tanochao = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "chao")
        {
            tanochao = false;
        }
    }


    void Start()
    {
        morte=false;
        rb = GetComponent<Rigidbody>();
        Time.timeScale = 1;
        posicao = 0;
        tanochao = false;
    }

    


    void Update()
    {
        //variaveis animação
        animador.SetInteger("estado", estado);
        animador.SetFloat("velocityY", rb.velocity.y);
        animador.SetBool("tanochao", tanochao);



        Swipe();

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                esquerda = true;
                estado = 2;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                direita = true;
                estado = 1;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                animador.SetTrigger("cambalhota");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                pulando = true;

            }

            if (posicao == 2 && transform.position.x > andardireita.x)
            {
                estado = 0;
                rb.velocity = andarfrente;
            }

            if (posicao == 0 && transform.position.x > -0.1 && transform.position.x < 0.1)
            {
                estado = 0;
                rb.velocity = andarfrente;
            }

            if (posicao == 3 && transform.position.x < andaresquerda.x)
            {
                estado = 0;
                rb.velocity = andarfrente;
            }

        
      

    }

    private void FixedUpdate()
    {
        //gravidade
        rb.AddForce(new Vector3(rb.velocity.x, graviteforce));

        if (!morte)
        {
            

            





            //ir pra frente e acelerar
            ir_para_frente = new Vector3(rb.velocity.x,0, 1 * velocidade);

            rb.velocity = ir_para_frente;
            tempo = tempo + Time.deltaTime;
            if (tempo > 0.1f)
            {
                tempo = 0;

                if (Time.timeScale < 1.5f)
                {
                    Time.timeScale = Time.timeScale + 0.0002f;
                }
                if (velocidade < 40)
                {
                    velocidade = velocidade + 0.005f;
                }

            }
            //-------------------------------------------------------------------------------------------------------------------------




            //ir para os lados

            andaresquerda = new Vector3(-3.5f,0, transform.position.z);
            andardireita = new Vector3(3.5f, 0, transform.position.z);
            andarmeio = new Vector3(0, 0, transform.position.z);
            andarfrente = new Vector3(0, 0, velocidade);
            pulo = new Vector3(0, forcapulo,0);
            ir_para_baixo = new Vector3(rb.velocity.x, -10, rb.velocity.z);



           



            if (direita)
            {
                
                if (posicao == 0)
                {
                    posicao = 2;
                }
                else if (posicao == 3)
                {
                    posicao = 0;
                }
                direita = false;
                rb.velocity = Vector3.right * velocidade_mov;
            }


            if (esquerda)
            {
                
                if (posicao == 0)
                {
                    posicao = 3;
                }
                else if (posicao == 2)
                {
                    posicao = 0;
                }
                esquerda = false;


                rb.velocity = Vector3.left * velocidade_mov;
            }



           
           
            //===============================================================================================================================

            //pulo
            if (pulando) 
            {
                pulando = false;
                rb.AddForce(pulo, ForceMode.Impulse);
            }





            //===============================================================================================================================

        }


        



      
    }
    

    public void Swipe()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
        {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) 
        {
        currentPosition = Input.GetTouch(0).position;
        Vector2 Distance = currentPosition - startTouchPosition;


            if (!stopTouch)
                {
                if (Distance.x < -swipeRange)
                {
                    esquerda = true;
                    estado = 2;
                    stopTouch = true;
                }
                else if (Distance.x > swipeRange)
                {
                    direita = true;
                    estado = 1;
                    stopTouch = true;
                }
                else if (Distance.y < -swipeRange)
                {
                    rb.AddForce(ir_para_baixo, ForceMode.Impulse);
                    animador.SetTrigger("cambalhota");
                    stopTouch = true;
                }
                else if (Distance.y > swipeRange) 
                {
                    rb.AddForce(pulo, ForceMode.Impulse);
                    stopTouch = true;
                }
            }
        }
     
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) 
        {
            stopTouch = false;

            endTouchPosition = Input.GetTouch(0).position;
            Vector2 Distance = endTouchPosition - startTouchPosition;
            if (Mathf.Abs(Distance.x)< tapRange && Mathf.Abs(Distance.y) < tapRange) 
            {
                print("doideira");
            }

        }
        
    }

    
}




