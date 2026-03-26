using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorAction : MonoBehaviour
{
    public enum Porta { Inicial, Porta2, Porta4 }

    [Header("Porta")]
    public Porta portaIdent;
    public bool abre;
    public Transform pivot;
    public Vector3 rotacaoAbertaOffset = new Vector3(0, 45, 0);
    public float velocidadeRotacao = 120f;

    private Quaternion rotacaoAberta;
    private Quaternion rotacaoFechada;
    LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        rotacaoFechada = pivot.rotation;
        rotacaoAberta = rotacaoFechada * Quaternion.Euler(rotacaoAbertaOffset);
        layerMask = LayerMask.GetMask("Door");
    }

    // Update is called once per frame
    void Update()
    {
        switch(portaIdent)
        {
            case Porta.Inicial:
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Input.GetMouseButtonUp(0)) // Verificação se o player clicou na porta
                {
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                    {
                        StartCoroutine("ChangeScene");
                    }
                }
                break;
            case Porta.Porta4:
                Debug.Log("Porta4");
                if(abre)
                {
                    Debug.Log("abrindo");
                    pivot.rotation = Quaternion.RotateTowards(pivot.rotation,rotacaoAberta,velocidadeRotacao * Time.deltaTime);
                    if(pivot.rotation == rotacaoAberta)
                    {
                        abre = false;
                    }
                }
                break;
        }
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Fase 1");
    }

    public void OpenDoor(bool action)
    {
        abre = action;
    }
}
