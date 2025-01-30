using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swapper : MonoBehaviour {
    public GameObject a;
    public GameObject b;
    public bool doit = false;
    
    void Update() {
        if (doit)
        {
            doit = false;
            if (a.activeSelf)
            {
                a.SetActive(false);
                b.SetActive(true);
            }
            else
            {
                a.SetActive(true);
                b.SetActive(false);
            }
        }
    }
}
