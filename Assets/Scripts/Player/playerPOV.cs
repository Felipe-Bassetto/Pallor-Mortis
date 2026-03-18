using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPOV : MonoBehaviour
{

    [Header("Configurações de Camera(POV)")]
    public float sens = 100f;
    public Transform corpoPlayer;
    public float velocidade = 1f;

    Camera cam;
    private float rotacaoX = 0f;
    private bool camMove = false;
    private bool camLocked = true;
    private Vector3 camLocation = new Vector3(0, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(!camLocked)
        {
            CameraRotation();
        }

        if(camMove)cam.transform.position = Vector3.MoveTowards(cam.transform.position,camLocation,velocidade * Time.unscaledDeltaTime);
        if(cam.transform.position == camLocation) CameraMovement(false);
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

    public void CameraMovement(bool move)
    {
        camMove = move;
    }
    
    public void CamLock(bool isLocked)
    {
        if(isLocked) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.None;
    }
}