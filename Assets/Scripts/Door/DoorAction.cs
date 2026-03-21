using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorAction : MonoBehaviour
{

    LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Door");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonUp(0)) // Verificação se o player clicou na porta
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                StartCoroutine("ChangeScene");
            }
        }
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Fase 1");
    }
}
