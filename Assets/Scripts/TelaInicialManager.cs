using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelaInicialManager : MonoBehaviour
{
    [Header("Objetos")]
    public GameObject opcoes;
    public GameObject btnOpcoes;
    public GameObject btnJogar;
    public GameObject btnSair;
    public GameObject nameGame;

    [Header("Scripts")]
    public PlayerPOV POV;
    public Movement MOV;
    public CamMenu CamMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenOpcoes()
    {
        opcoes.SetActive(true);
        btnOpcoes.SetActive(false);
        btnJogar.SetActive(false);
        btnSair.SetActive(false);
    }

    public void FecharOpcoes()
    {
        opcoes.SetActive(false);
        btnOpcoes.SetActive(true);
        btnJogar.SetActive(true);
        btnSair.SetActive(true);
    }

    public void Jogar()
    {
        CamMenu.CameraMovement(true);
        POV.CamLock(true);
        MOV.PlayMovement(false);
        nameGame.SetActive(false);
        btnOpcoes.SetActive(false);
        btnJogar.SetActive(false);
        btnSair.SetActive(false);
    }
}
