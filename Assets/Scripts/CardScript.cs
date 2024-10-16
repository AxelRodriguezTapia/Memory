using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{

    public GameObject figura;
    private GameObject gm;
    private bool selected;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController"); // Encuentra el GameManager

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        Debug.Log("Me apretaste guey");
        gm.GetComponent<GameManager>().cardTouched(gameObject);

        
    }

}
