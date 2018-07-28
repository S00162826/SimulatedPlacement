﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{

    public bool doesPlayerHaveKey;
    public bool doesPlayerHaveLvl2Key;
    public bool doesPlayerHaveLvl3Key;

    public Image keyPickedUp;
    //public Image lvl2KeyPickedUp;
    //public Image lvl3KeyPickedUp;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Key")
        {
            doesPlayerHaveKey = true;
            Destroy(other.gameObject);
            keyPickedUp.gameObject.SetActive(true);    
        }

        if (other.gameObject.tag == "KeyLv2")
        {
            doesPlayerHaveLvl2Key = true;
            Destroy(other.gameObject);
            //lvl2KeyPickedUp.gameObject.SetActive(true);
        }

        if (other.gameObject.tag == "KeyLv3")
        {
            doesPlayerHaveLvl3Key = true;
            Destroy(other.gameObject);
            //lvl3KeyPickedUp.gameObject.SetActive(true);
        }
    }

        void OnCollisionEnter(Collision col)
        {
        if (col.gameObject.tag == "Door" && doesPlayerHaveKey == true)
        {
           Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "DoorLvl2" && doesPlayerHaveLvl2Key == true)
        {
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "DoorLvl3" && doesPlayerHaveLvl3Key == true)
        {
            Destroy(col.gameObject);
        }
    }
}
