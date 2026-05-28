using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPOV : MonoBehaviour
{

    [Header("Configurações de Camera(POV)")]
    public float sens = 100f;
    public Transform corpoPlayer;
    public bool camLocked;

    Camera cam;
    private float rotacaoX = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(!camLocked)
        {
            CameraRotation();
        }
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
        transform.localRotation = Quaternion.Euler(rotacaoX, 180f, 0f); 
        corpoPlayer.Rotate(Vector3.up * mouseX); 
    }
    
    public void CamLock(bool isLocked)
    {
        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            camLocked = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            camLocked = true;
        }
    }

    public void Cutscene(bool playing)
    {
        camLocked = playing;
    }
}