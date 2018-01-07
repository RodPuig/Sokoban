using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    public RectTransform rt;
    public GameObject Nivel;

    bool once = false;

    int ancho, alto;
    private char [ , ] nivel;
    public string[] nivel_visualizado;

    int posX = 0;
    int posY = 0;

	// Use this for initialization
	void Start () {
        rt = (RectTransform)this.gameObject.transform;
        this.GetComponent<Rigidbody2D>().freezeRotation = true;

        Nivel = GameObject.FindGameObjectWithTag("InfoLevel");
    }
	
	// Update is called once per frame
	void Update () {

        //se ejecuta una vez
        if(!once)
        {
            bool encontrado = false;

            ancho = Nivel.GetComponent<CargaNiveles>().getAncho();
            alto = Nivel.GetComponent<CargaNiveles>().getAlto();
            nivel_visualizado = Nivel.GetComponent<CargaNiveles>().getNivel();
            nivel = new char[alto, ancho];


            //copiar el nivel
            for (int l = 0; l < alto; l++)
                for (int m = 0; m < ancho; m++)
                    nivel[l, m] = nivel_visualizado[l][m];

            //calculamos la posicion actual del personaje
            for (int i = 0; i < alto && !encontrado; i++)
            {
                for (int j = 0; j < ancho && !encontrado; j++)
                {
                    if(nivel[i, j] == 'P')
                    {
                        encontrado = true;
                        posX = j;
                        posY = i;
                    }
                }
            }

            actualizarString();

            Debug.Log("posX: " + posX + " PosY: " + posY);
            once = true;
        }

        //previous_position = transform.position;

		if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            //personaje
            if(nivel[posY+1 , posX] == '.')
            {
                gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - rt.rect.height);

                //desplazar personaje
                nivel[posY, posX] = '.';
                nivel[posY + 1, posX] = 'P';

                posY = posY + 1;

                actualizarString();
            }
            else if (nivel[posY + 1, posX] == 'O')
            {
                //comprobamos si la piedra tiene camino libre
                if (nivel[posY + 2, posX] == '.' || nivel[posY + 2, posX] == '@')
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - rt.rect.height);

                    //desplazar personaje
                    nivel[posY, posX] = '.';
                    nivel[posY + 1, posX] = 'P';

                    //desplazar piedra
                    nivel[posY + 2, posX] = 'O';

                    Nivel.GetComponent<RockManager>().DesplazarRoca();

                    posY = posY + 1;

                    actualizarString();
                }
            }

        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (nivel[posY - 1, posX] == '.')
            {
                gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + rt.rect.height);

                //desplazar personaje
                nivel[posY, posX] = '.';
                nivel[posY - 1, posX] = 'P';

                posY = posY - 1;

                actualizarString();
            }
            else if (nivel[posY - 1, posX] == 'O')
            {
                //comprobamos si la piedra tiene camino libre
                if (nivel[posY - 2, posX] == '.' || nivel[posY - 2, posX] == '@')
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + rt.rect.height);

                    //desplazar personaje
                    nivel[posY, posX] = '.';
                    nivel[posY - 1, posX] = 'P';

                    //desplazar piedra
                    nivel[posY - 2, posX] = 'O';

                    Nivel.GetComponent<RockManager>().DesplazarRoca();

                    posY = posY - 1;

                    actualizarString();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (nivel[posY, posX - 1] == '.')
            {
                gameObject.transform.position = new Vector2(gameObject.transform.position.x - rt.rect.width, gameObject.transform.position.y);

                //desplazar personaje
                nivel[posY, posX] = '.';
                nivel[posY, posX - 1] = 'P';

                posX = posX - 1;

                actualizarString();
            }
            else if (nivel[posY, posX - 1] == 'O')
            {
                //comprobamos si la piedra tiene camino libre
                if (nivel[posY, posX - 2] == '.' || nivel[posY, posX - 2] == '@')
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x - rt.rect.width, gameObject.transform.position.y);

                    //desplazar personaje
                    nivel[posY, posX] = '.';
                    nivel[posY, posX - 1] = 'P';

                    //desplazar piedra
                    nivel[posY, posX - 2] = 'O';

                    Nivel.GetComponent<RockManager>().DesplazarRoca();

                    posX = posX - 1;

                    actualizarString();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (nivel[posY, posX + 1] == '.')
            {
                gameObject.transform.position = new Vector2(gameObject.transform.position.x + rt.rect.width, gameObject.transform.position.y);

                //desplazar personaje
                nivel[posY, posX] = '.';
                nivel[posY, posX + 1] = 'P';

                posX = posX + 1;

                actualizarString();
            }
            else if(nivel[posY, posX + 1] == 'O')
            {
                //comprobamos si la piedra tiene camino libre
                if(nivel[posY, posX + 2] == '.' || nivel[posY, posX + 2] == '@')
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x + rt.rect.width, gameObject.transform.position.y);

                    //desplazar personaje
                    nivel[posY, posX] = '.';
                    nivel[posY, posX + 1] = 'P';

                    //desplazar piedra
                     nivel[posY, posX + 2] = 'O';
                     
                    Nivel.GetComponent<RockManager>().DesplazarRoca();

                    posX = posX + 1;

                    actualizarString();
                }
            }
        }
        else
        {

        }
    }

    public void actualizarString()
    {
        /*for (int i = 0; i < alto; i++)
        {
            nivel_visualizado[i] = "" + nivel[i, 0] + nivel[i, 1] + nivel[i, 2] + nivel[i, 3] + nivel[i, 4] + nivel[i, 5] + nivel[i, 6] + nivel[i, 7] + nivel[i, 8] + nivel[i, 9] +
                      nivel[i, 10] + nivel[i, 11] + nivel[i, 12] + nivel[i, 13] + nivel[i, 14] + nivel[i, 15] + nivel[i, 16] + nivel[i, 17] + nivel[i, 18] + nivel[i, 19];
        }*/
    }

    public char [,] getNivel()
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
}
