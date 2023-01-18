using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class BonusItem : MonoBehaviour
{
    public int _itemNumber=0;
    public GameObject BonusNumText; 

    private void Awake()
    {
        
        int numMultiplier = UnityEngine.Random.Range(1, 101);
        _itemNumber = numMultiplier * 10;

        BonusNumText = transform.Find("BonusNumber").gameObject;
        BonusNumText.GetComponent<TMP_Text>().text = _itemNumber.ToString();
        
    }

}
