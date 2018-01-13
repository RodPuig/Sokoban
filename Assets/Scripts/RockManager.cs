using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        
    }

    public void DesplazarRoca()
    {
        //recibo el mapa actualizado
        char[,] nivel = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().getNivel();
        int alto = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().getAlto();
        int ancho = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().getAncho();
        int contador = 0;

        //Debug.Log("ancho " + ancho + " alto " + alto);

        List<Vector2> posiciones_rocas = new List<Vector2>();

        for (int i = 0; i < alto; i++)
            for (int j = 0; j < ancho; j++)
                if (nivel[i, j] == 'O')
                {
                    posiciones_rocas.Add(new Vector2(j, i));
                    //Debug.Log("X: " + j + " Y: " + i);
                }
    
        //calcular las posiciones reales de las rocas
        foreach(Transform child in GameObject.FindGameObjectWithTag("RocasInfo").transform)
        {
            Vector2 pos = new Vector2(-(ancho * 0.32f) / 2, (alto * 0.32f) / 2);//GameObject.FindGameObjectWithTag("InfoLevel").transform.GetChild(0).transform.GetChild(0).transform.position;
            RectTransform rt = (RectTransform)child.transform;
            //Debug.Log("rect.width: " + rt.rect.width);
            //Debug.Log("Poscicion X roca: " + posiciones_rocas[contador].x + " Posicion Y roca: " + posiciones_rocas[contador].y);

            //Debug.Log(contador);

            child.transform.position = new Vector2(pos.x + posiciones_rocas[contador].x * rt.rect.width, pos.y - posiciones_rocas[contador].y * rt.rect.height);

            contador++;
            
        }
    }
    
}
