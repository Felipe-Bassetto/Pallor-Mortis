using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItensController : MonoBehaviour
{
    [Header("Itens")]
    public List<GameObject> arrItens = new List<GameObject>();
    public int itemActive;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        bool drop = Input.GetKeyDown(KeyCode.Q);
        if(drop)
        {
            dropItem();
        }

        bool change = Input.GetKeyDown(KeyCode.E);
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

        arrItens[itemActive].transform.SetParent(null);
        arrItens[itemActive] = null;
        itemActive = -1;
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
