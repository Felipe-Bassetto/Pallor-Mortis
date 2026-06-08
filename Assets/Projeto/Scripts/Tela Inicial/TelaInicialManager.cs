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
    [SerializeField] private GameObject canvasPlayer;

    [Header("Scripts")]
    [SerializeField] private PlayerPOV POV;
    [SerializeField] private Movement MOV;
    [SerializeField] private CamMenu CamMenu;
    [SerializeField] private GameDatabase db;
    [SerializeField] private SoundManager sm;
    [SerializeField] private Cutscenes cut;

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
        nameGame.SetActive(false);
        btnOpcoes.SetActive(false);
        btnJogar.SetActive(false);
        btnSair.SetActive(false);
        StartCoroutine(MessageTyping());
        yield return new WaitForSeconds(28f);

        sm.DiminuirVolumeGradual(2);

        canvasPlayer.SetActive(true);

        yield return new WaitForSeconds(2f);

        cut.Inicio();

        yield return new WaitForSeconds(15f);    

        MOV.PlayMovement(false);
        POV.CamLock(true);
    }

    IEnumerator MessageTyping()
    {
        int line = 0;
        bool textShowing = true;
        yield return new WaitForSeconds(1f);
        memoriesObj.SetActive(true);
        sm.PlaySound(5);
        while (textShowing)
        {
            memoriesUI.text = memories[line].Fala;
            if (line == 0) yield return new WaitForSeconds(4.5f);
            else if (line == 1) yield return new WaitForSeconds(2.6f);
            else if (line == 2) yield return new WaitForSeconds(2.4f);
            else if (line == 3) yield return new WaitForSeconds(2.7f);
            else if (line == 4) yield return new WaitForSeconds(2f);
            else if (line == 5) yield return new WaitForSeconds(3.5f);
            else if (line == 6) yield return new WaitForSeconds(3.7f);
            else if (line == 7) yield return new WaitForSeconds(5f);

            line++;

            if (memories.Count == line) textShowing = false;
        }
        memoriesObj.SetActive(false);
        yield return new WaitForSeconds(2f);
        sm.PlaySound(4);
    }
}
