using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;


public class ControlMouse : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            GraphicRaycaster hitInfo = GameObject.FindGameObjectWithTag("Canvas").GetComponent<GraphicRaycaster>();
            PointerEventData data = new PointerEventData(EventSystem.current);
            data.position = Input.mousePosition;
            List<RaycastResult> resultados = new List<RaycastResult>();
            EventSystem.current.RaycastAll(data, resultados);

            for(int i = 0; i< resultados.Count; i++)
            {
                if (resultados[i].gameObject.tag == "Tile")
                {
                    if (GameObject.FindGameObjectWithTag("Manager").GetComponent<ControlBotones>().getTileActual() != null)
                        resultados[i].gameObject.GetComponent<Image>().sprite = GameObject.FindGameObjectWithTag("Manager").GetComponent<ControlBotones>().getTileActual();
                    else
                        Debug.Log("Error");

                    Debug.Log("DADO");
                }
                else if(resultados[i].gameObject.tag == "Nivel")
                {
                    string path = "Assets/Niveles/Nivel_Seleccionado.txt";
                    StreamWriter escritor = new StreamWriter(path);

                    escritor.WriteLine(resultados[i].gameObject.GetComponentInChildren<Text>().text);

                    escritor.Close();

                    UnityEngine.SceneManagement.SceneManager.LoadScene("Juego");
                }
            }
        }
	}
}
