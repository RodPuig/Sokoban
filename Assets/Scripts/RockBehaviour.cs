using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : MonoBehaviour {

    public int ID, posX = 0, posY = 0, ancho, alto;
    bool once = false;
    public GameObject Player;
    private char[,] nivel;

    float timer = 0.5f;

    // Use this for initialization
    void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
		if(!once)
        {
            if (timer <= 0)
            {
                
                int contador = 0;
                bool encontrado = false;

                nivel = Player.GetComponent<PlayerBehaviour>().getNivel();
                ancho = Player.GetComponent<PlayerBehaviour>().getAncho();
                alto = Player.GetComponent<PlayerBehaviour>().getAlto();

                for (int i = 0; i < alto && !encontrado; i++)
                    for (int j = 0; j < ancho && !encontrado; j++)
                    {
                        if (nivel[i, j] == 'O' && contador == ID)
                        {
                            encontrado = true;
                            posX = j;
                            posY = i;
                        }
                    }

                once = true;
            }

            timer -= Time.deltaTime;
        }
	}

    public void setID(int id)
    {
        ID = id;
    }

    public int getX()
    {
        return posX;
    }

    public int getY()
    {
        return posY;
    }

    public void SetX(int x)
    {
        posX = x;
    }

    public void SetY(int y)
    {
        posY = y;
    }
}
