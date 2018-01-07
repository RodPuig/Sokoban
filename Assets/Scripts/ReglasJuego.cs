using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReglasJuego : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        bool todo_ok = true;

        GameObject[] go = GameObject.FindGameObjectsWithTag("Meta");

        for(int i = 0; i<go.Length; i++)
        {
            if (!go[i].GetComponent<MetaBehaviour>().GetOk())
                todo_ok = false;
        }

        if (todo_ok)
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
	}
}
