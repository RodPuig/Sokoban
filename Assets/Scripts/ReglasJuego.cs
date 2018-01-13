using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReglasJuego : MonoBehaviour {


    public bool todo_ok;

    // Use this for initialization
    void Start () {

        todo_ok = true;
	}
	
	// Update is called once per frame
	void Update () {

        todo_ok = true;

        GameObject[] go = GameObject.FindGameObjectsWithTag("Meta");

        for(int i = 0; i<go.Length; i++)
        {

            if (!go[i].GetComponent<MetaBehaviour>().GetOk())
                todo_ok = false;

            
        }


            if (todo_ok)
            {

                /*//borrar elementos anterioriores
                foreach (Transform child in GameObject.FindGameObjectWithTag("InfoLevel").transform.GetChild(0).transform)
                    Destroy(child);
                foreach (Transform child in GameObject.FindGameObjectWithTag("InfoLevel").transform.GetChild(1).transform)
                    Destroy(child);
                foreach (Transform child in GameObject.FindGameObjectWithTag("InfoLevel").transform.GetChild(2).transform)
                    Destroy(child);*/
                //cargar siguiente nivel
                GameObject.FindGameObjectWithTag("InfoLevel").GetComponent<CargaNiveles>().CargarSiguienteNivel();
                //UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
                //Debug.Log("");
                //if(!cargado)
                //  UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");

            }

	}
}
