using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] cards;
    private double id_double=0;
    private GameObject[] cardsSelected = new GameObject[2];
    private int cartesAdivinades;
    private bool clickTrigger=false;
    private float clickCooldown=0;


    // Start is called before the first frame update
    void Start()
    {

        



        cardsSelected[0]=null;
        cardsSelected[1]=null;
        cartesAdivinades=0;
        cards = GameObject.FindGameObjectsWithTag("CardTag");
        foreach(GameObject card in cards){
            card.GetComponent<CardScript>().setId(id_double);
            id_double=id_double+0.5;
            Debug.Log(card.GetComponent<CardScript>().getId());
        }
    
        int i=0;

        // Asignar e instanciar los objetos
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                // Instanciar el prefab en una posición del espacio 3D
                Vector3 posicion = new Vector3((float)((x * 2)-3.5), 0, (y * 2)-4); // Ajustar la posición de cada objeto
                cards[i].transform.position = posicion;
                i++;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(clickTrigger){
            clickCooldown+=Time.deltaTime;
            if(clickCooldown>=1){
                clickCooldown=0;
                clickTrigger=false;
            }
        }

        if(cardsSelected[0]!=null && cardsSelected[1]!=null){
            AnimatorStateInfo stateInfo0 = cardsSelected[0].gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            AnimatorStateInfo stateInfo1 = cardsSelected[1].gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if(stateInfo0.IsName("FigureUp") && stateInfo1.IsName("FigureUp")){
                checkIds();
            }
        }
        if(cartesAdivinades==8){
            Debug.Log("End Game");
        }
        
        
    }
    public bool canClickTrigger(){
        return !clickTrigger;
    }

    public void setClickTrigger(bool clickTriger){
        clickTrigger = clickTriger;
    }

    public void cardTouched(GameObject cardtouched){
        //Debug.Log("Me han tocado la carta "+cardtouched.name+" i te la id: "+cardtouched.GetComponent<CardScript>().getId());
        
        if(cardsSelected[0]==null && cardsSelected[1]==null){
            cardsSelected[0]=cardtouched.gameObject;
            //Debug.Log("Carta seleccionada");
        }else{
            cardsSelected[1]=cardtouched.gameObject;
        }   
        
        
        
    }

    //public void allCardsDown(){
    //    foreach(GameObject card in cards){
    //        card.GetComponent<CardScript>().moveDown();
    //    }
    //}
    public void checkIds(){
        if(cardsSelected[0]!=null && cardsSelected[1]!=null){
            if(cardsSelected[0].GetComponent<CardScript>().getId()!=cardsSelected[1].GetComponent<CardScript>().getId()){
                cardsSelected[0].GetComponent<CardScript>().esconder();
                cardsSelected[1].GetComponent<CardScript>().esconder();
                borrarSeleccionados(12);
            }else{
                //Debug.Log("Cartas iguals!!" +cardsSelected[0].GetComponent<CardScript>().getId()+" "+cardsSelected[1].GetComponent<CardScript>().getId());
                cartesAdivinades++;
                borrarSeleccionados(12);
            }
        }
    }

    public bool hiHaPuesto(){
        return cardsSelected[0]==null || cardsSelected[1]==null;
    }

    public void borrarSeleccionados(int num){
        if(num==0){
            cardsSelected[0]=null;
        }
        if(num==1){
            cardsSelected[1]=null;
        }
        if(num==12){
            cardsSelected[0]=null;
            cardsSelected[1]=null;
        }
    }
}
