using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float walk = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WASD();
    }

    void WASD()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * walk * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * walk * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * walk * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * walk * Time.deltaTime);
        }
    }
    /*void CameraRotation()
    {   
        //* 1- Lê os movimentos do mouse 
        float mouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;

        //* 2- Movimentação para cima/baixo e trava de segurança(para não rolar um mortal com a camera)
        rotacaoX -= mouseY;
        rotacaoX = Mathf.Clamp(rotacaoX, -90f, 90f); 

        //* 3- Rotação de camera 
        transform.localRotation = Quaternion.Euler(rotacaoX, 0f, 0f); 
        corpoPlayer.Rotate(Vector3.up * mouseX); 
    }*/
}
