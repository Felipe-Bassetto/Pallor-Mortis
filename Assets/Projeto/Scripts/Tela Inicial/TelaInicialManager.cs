using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TelaInicialManager : MonoBehaviour
{
    [Header("Objetos")]
    [SerializeField] private GameObject opcoes;
    [SerializeField] private GameObject btnOpcoes;
    [SerializeField] private GameObject btnJogar;
    [SerializeField] private GameObject btnSair;
    [SerializeField] private GameObject nameGame;
    [SerializeField] private GameObject memoriesObj;
    [SerializeField] private TextMeshProUGUI memoriesUI;

    [Header("Scripts")]
    [SerializeField] private PlayerPOV POV;
    [SerializeField] private Movement MOV;
    [SerializeField] private CamMenu CamMenu;
    [SerializeField] private GameDatabase db;

    private List<Memories> memories;


    // Start is called before the first frame update
    void Start()
    {
        POV.CamLock(false);
        memories = db.CarregarMemoria(0);
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
        nameGame.SetActive(false);
    }

    public void FecharOpcoes()
    {
        opcoes.SetActive(false);
        btnOpcoes.SetActive(true);
        btnJogar.SetActive(true);
        btnSair.SetActive(true);
        nameGame.SetActive(true);
    }

    public void Jogar()
    {
        StartCoroutine(InicioGame());
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator InicioGame()
    {
        CamMenu.CameraMovement(true);
        memoriesObj.SetActive(true);
        nameGame.SetActive(false);
        btnOpcoes.SetActive(false);
        btnJogar.SetActive(false);
        btnSair.SetActive(false);
        StartCoroutine(MessageTyping());
        yield return new WaitForSeconds(5f);
        MOV.PlayMovement(false);
        POV.CamLock(true);
    }

    IEnumerator MessageTyping()
    {
        int line = 0;
        bool textShowing = true;
        while (textShowing)
        {
            memoriesUI.text = memories[line].Fala;
            yield return new WaitForSeconds(1f);
            line++;

            if (memories.Count == line + 1) textShowing = false;
        }
    }
}
