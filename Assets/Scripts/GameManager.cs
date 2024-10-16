using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] cards;


    // Start is called before the first frame update
    void Start()
    {
        cards = GameObject.FindGameObjectsWithTag("CardTag");
    

        // Crear una matriz bidimensional (4x4) de GameObjects
        GameObject[,] matriz2D = new GameObject[4, 4];
        int i=0;

        // Asignar e instanciar los objetos
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                // Instanciar el prefab en una posición del espacio 3D
                Vector3 posicion = new Vector3((x * 2)-3.5, 0, (y * 2)-4); // Ajustar la posición de cada objeto
                cards[i].transform.position = posicion;
                i++;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cardTouched(GameObject cardtouched){
        Debug.Log("Me han tocado la carta "+cardtouched.name);

    }
    void bidimensionalTable(){

    }
}
