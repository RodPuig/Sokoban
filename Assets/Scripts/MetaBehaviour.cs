﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaBehaviour : MonoBehaviour {

    public bool ok = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Piedra")
            ok = true;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Piedra")
            ok = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Piedra")
            ok = false;
    }

    public bool GetOk()
    {
        return ok;
    }
}
