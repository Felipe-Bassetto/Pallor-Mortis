using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    [Header("Variáveis")]
    public List<GameObject> listObjects = new List<GameObject>();

    [Header("Camera")]
    private Camera cameraPrincipal;

    [Header("Prefabs")]
    [SerializeField] private GameObject prafabCartao;
    [SerializeField] private Collider craftC;

    // Start is called before the first frame update
    void Start()
    {
        cameraPrincipal = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (listObjects.Count == 3)
        {
            Ray ray = cameraPrincipal.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity) && gameObject.name == hit.collider.gameObject.name)
            {
                if(hit.collider.tag == "Crafting" && Input.GetKeyDown(KeyCode.E))
                {
                    foreach (GameObject objList in listObjects) Destroy(objList);

                    listObjects.Clear();

                    Instantiate(prafabCartao, new Vector3(-13.36f, 2.3f, 13), Quaternion.identity);
                    craftC.enabled = false;
                }
            }
        }
    }

    public void RemoveItem(GameObject obj) => listObjects.Remove(obj);

    public void AddItem(GameObject obj) => listObjects.Add(obj);

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fita" || other.gameObject.tag == "Parte Cartão") listObjects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Fita" || other.gameObject.tag == "Parte Cartão") listObjects.Remove(other.gameObject);
    }
}
