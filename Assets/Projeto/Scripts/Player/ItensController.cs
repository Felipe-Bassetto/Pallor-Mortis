using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensController : MonoBehaviour
{
    [Header("Itens")]
    public List<GameObject> arrItens = new List<GameObject>();
    public int itemActive;

    private GameObject prefabDrop;
    private LayerMask layerMask;

    [Header("Camera")]
    private Camera cameraPrincipal;

    // Start is called before the first frame update
    void Start()
    {
        cameraPrincipal = Camera.main;

        layerMask = LayerMask.GetMask("Dropable");
    }

    // Update is called once per frame
    void Update()
    {

        bool keyQHolding = Input.GetKey(KeyCode.Q) && itemActive != -1;
        bool keyQReleased = Input.GetKeyUp(KeyCode.Q);

        if (keyQHolding)
        {
            dropItem();
        }

        if (keyQReleased && prefabDrop != null)
        {
            Destroy(prefabDrop);
            prefabDrop = null;
        }

        bool change = Input.GetKeyDown(KeyCode.Alpha1);
        if(change)
        {
            changeItem();
        }


    }

    public int addItem(GameObject item) // Adiciona item a mão
    {
        int returnNum = -1;
        
        for(int i = 0; i < 2; i++)
        {
            if(arrItens[i] == null)
            {
                if(itemActive >= 0)
                {
                    arrItens[itemActive].SetActive(false);
                }
                arrItens[i] = item;
                returnNum = i;
                itemActive = returnNum;
                break;
            }
        }
        
        return returnNum;

    }

    public void dropItem() // Solta o item da mão
    {
        if(itemActive == -1) return;

        if (prefabDrop == null)
        {
            GrabItem grab = arrItens[itemActive].GetComponentInChildren<GrabItem>();

            prefabDrop = Instantiate(grab.prefabDrop);
        }

        Ray ray = cameraPrincipal.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)) prefabDrop.transform.position = hit.point;


        if (Input.GetMouseButtonDown(0))
        {
            arrItens[itemActive].transform.SetParent(null);
            arrItens[itemActive].transform.position = hit.point;
            arrItens[itemActive] = null;
            itemActive = -1;

            Destroy(prefabDrop);
            prefabDrop = null;
        }
    }

    public void changeItem()
    {
        for(int i = 0; i < 2; i++)
        {
            if(i == itemActive)continue;
            else if(arrItens[i] != null)
            {                
                if(itemActive != -1)
                {
                    arrItens[itemActive].SetActive(false);
                }
                if(itemActive == 1)
                {
                    arrItens[itemActive].SetActive(false);
                    itemActive = -1;
                }
                else
                {
                    arrItens[i].SetActive(true);
                    itemActive = i;
                }
                break;                    
            }
            else
            {
                if(itemActive != -1)
                {
                    arrItens[itemActive].SetActive(false);
                }
                itemActive = -1;
            }
        }
    }
}
