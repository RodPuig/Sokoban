using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TileMap : MonoBehaviour {

    public Transform content;
    public GameObject prefabTile;
    public Transform Parent;
    public Text Ancho;
    public Text Alto;

    private int alto, ancho;
    public RectTransform rt;
    public RectTransform r_content;

	// Use this for initialization
	void Start () {
        rt = (RectTransform)prefabTile.transform;
        r_content = (RectTransform)content.transform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CrearTiles()
    {
        if(Alto.text.Length > 0 && Ancho.text.Length > 0)
        {

            foreach(Transform child in Parent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            //asignamos alto y ancho
            alto = int.Parse(Alto.text);
            ancho = int.Parse(Ancho.text);

            //creamos mapa
            for(int i = 0; i<alto*ancho; i++)
            {
                GameObject go = Instantiate(prefabTile, Parent, true);

                go.transform.localPosition = new Vector2((i % ancho) * rt.rect.width*rt.localScale.x + (i%ancho)*1.2f,
                                                    -100 - (i / ancho) * rt.rect.height*rt.localScale.y - (i/ancho)*1.2f);
            }
            Debug.Log(rt.rect.height * rt.localScale.y);
            r_content.sizeDelta = new Vector2(ancho * rt.rect.width * rt.localScale.x + ancho*1.2f, alto * rt.rect.height * rt.lossyScale.y + alto*1.2f);
        }
    }
}
