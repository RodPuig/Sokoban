using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class ControlBotones : MonoBehaviour {

    public Transform ParentNiveles;
    public GameObject boton_niveles;
    public GameObject panel;
    public GameObject prefab_nivel;
    public GameObject panel_de_niveles;
    public GameObject panel_de_menu;


    public Sprite prefab_muro;
    public Sprite prefab_meta;
    public Sprite prefab_suelo;
    public Sprite prefab_piedra;
    public Sprite prefab_personaje;

    public Sprite tile_Actual;


    public 


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ActivarPanel()
    {
        panel.SetActive(true);
    }

    public void DesactivarPanel()
    {
        panel.SetActive(false);
    }

    public void setPersonaje()
    {
        tile_Actual = prefab_personaje;
    }

    public void setMuro()
    {
        tile_Actual = prefab_muro;
    }

    public void setMeta()
    {
        tile_Actual = prefab_meta;
    }

    public void setSuelo()
    {
        tile_Actual = prefab_suelo;
    }

    public void setPiedra()
    {
        tile_Actual = prefab_piedra;
    }

    public Sprite getTileActual()
    {
        return tile_Actual;
    }

    public void GuardarContenido()
    {
        string path = "Assets/Niveles/Niveles.txt";

        //guarda el contenido en un fichero
        if (File.Exists(path))
        {
            StreamWriter escritor = new StreamWriter(path, true);

            Debug.Log("Nombre del nivel: " + GameObject.FindGameObjectWithTag("NombreNivel").GetComponent<Text>().text + ".lvl");
            escritor.WriteLine(GameObject.FindGameObjectWithTag("NombreNivel").GetComponent<Text>().text + ".lvl");
            Debug.Log("Alto: " + GameObject.FindGameObjectWithTag("Alto").GetComponent<Text>().text);
            escritor.WriteLine(GameObject.FindGameObjectWithTag("Alto").GetComponent<Text>().text);
            Debug.Log("Ancho: " + GameObject.FindGameObjectWithTag("Ancho").GetComponent<Text>().text);
            escritor.WriteLine(GameObject.FindGameObjectWithTag("Ancho").GetComponent<Text>().text);

            encriptacion(ref escritor, int.Parse(GameObject.FindGameObjectWithTag("Ancho").GetComponent<Text>().text));
            /*Debug.Log("Fichero abierto");
            
            string nombre_nivel = GameObject.FindGameObjectWithTag("NombreNivel").GetComponent<Text>().text;
            string alto = GameObject.FindGameObjectWithTag("Alto").GetComponent<Text>().text;
            string ancho = GameObject.FindGameObjectWithTag("Ancho").GetComponent<Text>().text;

            int num_filas = int.Parse(alto);
            int num_columnas = int.Parse(ancho);
            int num_tiles = num_filas * num_columnas;

            //guardar la info
            StreamWriter escritor = new StreamWriter(filename, System.Text.Encoding.Default);
            //nombre del nivel
            escritor.WriteLine(nombre_nivel);
            //alto
            escritor.WriteLine(alto);
            //ancho
            escritor.WriteLine(ancho);
            //nº tiles
            encriptacion(ref escritor, num_columnas);

            escritor.Close();

            AssetDatabase.ImportAsset(filename);
            TextAsset asset = Resources.Load("Niveles.txt");*/



            escritor.Close();
        }
        
    }

    private string encriptacion(ref StreamWriter escritor, int columnas)
    {
        string cadena = "";
        int contador = 0;

        foreach(Transform child in GameObject.FindGameObjectWithTag("Tiles").transform)
        {
           if (child.GetComponent<Image>().sprite.name == "SokobanTileSet_0")
            {
                //pared '#'
                cadena += '#';
            }
           else if (child.GetComponent<Image>().sprite.name == "SokobanTileSet_1")
            {
                //meta '@'
                cadena += '@';
            }
            else if (child.GetComponent<Image>().sprite.name == "SokobanTileSet_2")
            {
                //suelo '.'
                cadena += '.';
            }
            else if (child.GetComponent<Image>().sprite.name == "SokobanTileSet_3")
            {
                //piedra 'O'
                cadena += 'O';
            }
            else if (child.GetComponent<Image>().sprite.name == "SokobanTileSet_4")
            {
                //personaje 'P'
                cadena += 'P';
            }
            else
            {
                //nada '?'
                cadena += '?';
            }

            contador++;
            Debug.Log("contador: " + contador);
           if(contador%(columnas) == 0)
            {
                escritor.WriteLine(cadena);
                Debug.Log("linea: " + cadena);
                cadena = "";
            }

        }

        return cadena;
    }

    public void ContruirPanelNiveles()
    {
        string path = "Assets/Niveles/Niveles.txt";
        int contador = 0;
        RectTransform rt = (RectTransform)prefab_nivel.transform;
        RectTransform rt_parent = (RectTransform)ParentNiveles;
        //desactivamos el boton
        //boton_niveles.SetActive(false);

        //se leen todos los niveles y se hace un panel con el nombre de los niveles
        StreamReader lector = new StreamReader(path);

        while(!lector.EndOfStream)
        {
            string cad = lector.ReadLine();

            if(isLevel(cad))
            {
                GameObject go = Instantiate(prefab_nivel, ParentNiveles, true);
                go.GetComponentInChildren<Text>().text = cad;
                go.transform.position = new Vector2(ParentNiveles.position.x + (rt.rect.width/2)- (rt_parent.rect.width/2) + (contador%8) * rt.rect.width, ParentNiveles.position.y + (rt_parent.rect.height/2) - (3*rt.rect.height/2) - (contador / 8) * rt.rect.height);
                contador++;
            }
        }

        lector.Close();
    }

    private bool isLevel(string cadena)
    {
        bool ok = false;
        if(cadena.Length >= 5)
        if (cadena[cadena.Length - 4] == '.' &&
           cadena[cadena.Length - 3] == 'l' &&
           cadena[cadena.Length - 2] == 'v' &&
           cadena[cadena.Length - 1] == 'l')
            ok = true;

        return ok;
    }

    public void ActivarPanelNiveles()
    {
        panel_de_niveles.SetActive(true);
    }

    public void DesactivarPanelNiveles()
    {
        panel_de_niveles.SetActive(false);
    }

    public void ActivarPanelMenu()
    {
        panel_de_menu.SetActive(true);
    }

    public void DesactivarPanelMenu()
    {
        panel_de_menu.SetActive(false);
    }

    public void CargarEditor()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Editor");
    }

    public void CargarMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
