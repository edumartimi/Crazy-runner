using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    public GameObject ground;
    public GameObject lugarchao;
    public Player player;

    float existencia;
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(ground,lugarchao.transform);

    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
        existencia = existencia + Time.deltaTime;
       
        if (player.tempo > 3) 
        {
            Destroy(this.gameObject);
        }
    }
}
