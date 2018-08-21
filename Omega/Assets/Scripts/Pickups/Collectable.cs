﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    public float totalCollectables;
    public float collectablesFound;
    public Text collectables;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collectable" && gameObject.tag == "CollectableCollider")
        {
            collectablesFound += 1;
            other.gameObject.SetActive(false);
        }
           
    }
        void Update()
    {
        collectables.text = "Collectables Found : " + collectablesFound.ToString()
                            + "/" + totalCollectables.ToString();
    }
}
