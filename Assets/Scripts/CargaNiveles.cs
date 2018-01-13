using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CargaNiveles : MonoBehaviour {

    public Transform inicio_nivel;
    public Transform inicio_nivel_2;

    public GameObject prefab_personaje;
    public GameObject prefab_muro;
    public GameObject prefab_suelo;
    public GameObject prefab_piedra;
    public GameObject prefab_meta;

    public Transform Tiles;
    public Transform personaje;
    public Transform piedras;
    public Transform Metas;

    public string nombre;
    string [] nivel;
    int alto = 0;
    int ancho = 0;


    // Use this for initialization
    void Start () {
        string path_1 = "Assets/Niveles/Nivel_Seleccionado.txt";
        string path_2 = "Assets/Niveles/Niveles.txt";
        string nombre_nivel = "";

        bool encontrado = false;

        Vector2 posicion_inicial_tiles;

        alto = 0;
        ancho = 0;

        //se lee el archivo que contiene el nombre del nivel
        StreamReader lector_1 = new StreamReader(path_1);

        if(lector_1 != null)
        {
            //leemos el nombre
            nombre_nivel = lector_1.ReadLine();
            nombre = nombre_nivel;
            lector_1.Close();
        }

        //leemos el segundo archivo
        StreamReader lector_2 = new StreamReader(path_2);

        if (lector_2 != null)
        {
            //procedemos a buscar el nivel en el archivo
            while (!encontrado)
            {
                string cad = lector_2.ReadLine();

                if (cad == nombre_nivel)
                    encontrado = true;
            }

            if (encontrado)
            {
                //alto
                alto = int.Parse(lector_2.ReadLine());
                //ancho
                ancho = int.Parse(lector_2.ReadLine());

                //reservamos memoria en el string para almacenar las cadenas
                nivel = new string[alto];

                for (int i = 0; i < alto; i++)
                    nivel[i] = lector_2.ReadLine();
            }

            lector_2.Close();

            //calculo posicion inicial (todos los prefabs tienen la misma altura y anchura)
            RectTransform rt_aux = (RectTransform)prefab_muro.transform;
            posicion_inicial_tiles = new Vector2(-(ancho * rt_aux.rect.width) / 2,
                 (alto * rt_aux.rect.height) / 2);
            //Debug.Log(rt_aux.rect.width);

            //intanciación del nivel
            for (int fila = 0; fila < alto; fila++)
            {
                for (int columna = 0; columna < ancho; columna++)
                {
                    GameObject aux;
                    RectTransform rt;

                    switch (nivel[fila][columna])
                    {
                        case '#': //muro
                            aux = Instantiate(prefab_muro, Tiles);
                            rt = (RectTransform)aux.transform;
                            rt.position = new Vector2(posicion_inicial_tiles.x + columna * rt.rect.width, posicion_inicial_tiles.y - fila * rt.rect.height);
                            break;
                        case 'P': //personaje
                                  //en el caso del personaje hay que instanciar un tile de suelo debajo inicialmente
                            aux = Instantiate(prefab_suelo, Tiles);
                            rt = (RectTransform)aux.transform;
                            rt.position = new Vector2(posicion_inicial_tiles.x + columna * rt.rect.width, posicion_inicial_tiles.y - fila * rt.rect.height);

                            aux = Instantiate(prefab_personaje, personaje);
                            rt = (RectTransform)aux.transform;
                            rt.position = new Vector2(posicion_inicial_tiles.x + columna * rt.rect.width, posicion_inicial_tiles.y - fila * rt.rect.height);
                            break;
                        case 'O': //piedra
                                  //en el caso de la piedra hay que instanciar un tile de suelo debajo inicialmente
                            aux = Instantiate(prefab_suelo, Tiles);
                            rt = (RectTransform)aux.transform;
                            rt.position = new Vector2(posicion_inicial_tiles.x + columna * rt.rect.width, posicion_inicial_tiles.y - fila * rt.rect.height);

                            //y ahora instanciamos la piedra
                            aux = Instantiate(prefab_piedra, piedras);
                            rt = (RectTransform)aux.transform;
                            rt.position = new Vector2(posicion_inicial_tiles.x + columna * rt.rect.width, posicion_inicial_tiles.y - fila * rt.rect.height);
                            break;
                        case '.': //suelo
                            aux = Instantiate(prefab_suelo, Tiles);
                            rt = (RectTransform)aux.transform;
                            rt.position = new Vector2(posicion_inicial_tiles.x + columna * rt.rect.width, posicion_inicial_tiles.y - fila * rt.rect.height);
                            break;
                        case '@': //
                            aux = Instantiate(prefab_meta, Metas);
                            rt = (RectTransform)aux.transform;
                            rt.position = new Vector2(posicion_inicial_tiles.x + columna * rt.rect.width, posicion_inicial_tiles.y - fila * rt.rect.height);
                            break;
                        default:
                            break;

                    }
                }
            }

        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private Vector2 CalcularVector(Vector2 vec1, Vector2 vec2)
    {
        return new Vector2(vec2.x - vec1.x, vec2.y - vec1.y);
    }

    public string[] getNivel()
    {
        return nivel;
    }

    public int getAncho()
    {
        return ancho;
    }

    public int getAlto()
    {
        return alto;
    }

   public void LoadLevel(string filename)
    {

        string path_2 = "Assets/Niveles/Niveles.txt";
        string nombre_nivel = filename;
        //nombre = nombre_nivel;

        bool encontrado = false;

        Vector2 posicion_inicial_tiles;

        alto = 0;
        ancho = 0;



        //leemos el segundo archivo
        StreamReader lector_2 = new StreamReader(path_2);

        if (lector_2 != null)
        {
            //procedemos a buscar el nivel en el archivo
            while (!encontrado)
            {
                string cad = lector_2.ReadLine();

                if (cad == nombre_nivel)
                    encontrado = true;
            }

            if (encontrado)
            {
                //alto
                alto = int.Parse(lector_2.ReadLine());
                //ancho
                ancho = int.Parse(lector_2.ReadLine());

                //reservamos memoria en el string para almacenar las cadenas
                nivel = new string[alto];

                for (int i = 0; i < alto; i++)
                    nivel[i] = lector_2.ReadLine();
            }

            lector_2.Close();

            //calculo posicion inicial (todos los prefabs tienen la misma altura y anchura)
            RectTransform rt_aux = (RectTransform)prefab_muro.transform;
            posicion_inicial_tiles = new Vector2(-(ancho * rt_aux.rect.width) / 2,
                 (alto * rt_aux.rect.height) / 2);
            Debug.Log(rt_aux.rect.width);

            //intanciación del nivel
            for (int fila = 0; fila < alto; fila++)
            {
                for (int columna = 0; columna < ancho; columna++)
                {
                    GameObject aux;
                    RectTransform rt;

                    switch (nivel[fila][columna])
                    {
                        case '#': //muro
                            aux = Instantiate(prefab_muro, Tiles);
                            rt = (RectTransform)aux.transform;
                            rt.position = new Vector2(posicion_inicial_tiles.x + columna * rt.rect.width, posicion_inicial_tiles.y - fila * rt.rect.height);
                            break;
                        case 'P': //personaje
                                  //en el caso del personaje hay que instanciar un tile de suelo debajo inicialmente
                            aux = Instantiate(prefab_suelo, Tiles);
                            rt = (RectTransform)aux.transform;
                            rt.position = new Vector2(posicion_inicial_tiles.x + columna * rt.rect.width, posicion_inicial_tiles.y - fila * rt.rect.height);

                            aux = Instantiate(prefab_personaje, personaje);
                            rt = (RectTransform)aux.transform;
                            rt.position = new Vector2(posicion_inicial_tiles.x + columna * rt.rect.width, posicion_inicial_tiles.y - fila * rt.rect.height);
                            break;
                        case 'O': //piedra
                                  //en el caso de la piedra hay que instanciar un tile de suelo debajo inicialmente
                            aux = Instantiate(prefab_suelo, Tiles);
                            rt = (RectTransform)aux.transform;
                            rt.position = new Vector2(posicion_inicial_tiles.x + columna * rt.rect.width, posicion_inicial_tiles.y - fila * rt.rect.height);

                            //y ahora instanciamos la piedra
                            aux = Instantiate(prefab_piedra, piedras);
                            rt = (RectTransform)aux.transform;
                            rt.position = new Vector2(posicion_inicial_tiles.x + columna * rt.rect.width, posicion_inicial_tiles.y - fila * rt.rect.height);
                            break;
                        case '.': //suelo
                            aux = Instantiate(prefab_suelo, Tiles);
                            rt = (RectTransform)aux.transform;
                            rt.position = new Vector2(posicion_inicial_tiles.x + columna * rt.rect.width, posicion_inicial_tiles.y - fila * rt.rect.height);
                            break;
                        case '@': //
                            aux = Instantiate(prefab_meta, Tiles);
                            rt = (RectTransform)aux.transform;
                            rt.position = new Vector2(posicion_inicial_tiles.x + columna * rt.rect.width, posicion_inicial_tiles.y - fila * rt.rect.height);
                            break;
                        default:
                            break;

                    }
                }
            }

        }
    }

    public void CargarSiguienteNivel()
    {
        string nombre_nivel_actual = nombre;
        string nombre_siguiente_nivel = "";
        string anterior_nivel_leido = "";

        StreamReader lector = new StreamReader("Assets/Niveles/Niveles.txt");

        bool encontrado = false;


        while (!lector.EndOfStream)
        {
            string aux = lector.ReadLine();

            if (aux.Length >= 5)
                if (aux[aux.Length - 4] == '.' && aux[aux.Length - 3] == 'l' && aux[aux.Length - 2] == 'v' && aux[aux.Length - 1] == 'l')
                {
                    Debug.Log(aux);

                    if (anterior_nivel_leido == nombre)
                    {
                        nombre_siguiente_nivel = aux;
                        Debug.Log("Encontrado");
                        encontrado = true;
                    }

                    anterior_nivel_leido = aux;
                }
        }

        lector.Close();

        if (!encontrado)
            nombre_siguiente_nivel = "Ninguno";

        StreamWriter escritor = new StreamWriter("Assets/Niveles/Nivel_Seleccionado.txt");

        escritor.WriteLine(nombre_siguiente_nivel);

        escritor.Close();

        //LoadLevel(nombre_siguiente_nivel);
        if(nombre_siguiente_nivel!= "Ninguno")
        UnityEngine.SceneManagement.SceneManager.LoadScene("Juego");
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}
