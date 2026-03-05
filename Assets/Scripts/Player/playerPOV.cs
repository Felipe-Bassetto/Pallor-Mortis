using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPOV : MonoBehaviour
{

    [Header("Configurações de Camera(POV)")]
    public float sens = 100f;
    private float rotacaoX = 0f;
    public Transform corpoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation();
    }

    void CameraRotation()
    {   
        //* 1- Lê os movimentos do mouse *//
        float mouseX = Input.GetAxis("Mouse X") * sens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sens * Time.deltaTime;

        //* 2- Movimentação para cima/baixo e trava de segurança(para não rolar um mortal com a camera) *//
        rotacaoX -= mouseY;
        rotacaoX = Mathf.Clamp(rotacaoX, -90f, 90f); 

        //* 3- Rotação de camera *//
        transform.localRotation = Quaternion.Euler(rotacaoX, 0f, 0f); 
        corpoPlayer.Rotate(Vector3.up * mouseX); 
    }
}
