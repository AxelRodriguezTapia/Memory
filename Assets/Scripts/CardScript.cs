using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{

    public GameObject figura;
    private GameObject gm;
    private int id;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController"); // Encuentra el GameManager

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            esconder();
        }
    }

    public void setId(double idp){
        id=(int)idp;
        //set color
    }

    public int getId(){
        return id;
    }

    void OnMouseDown()
    {
        //Trigger del game object
        if(gm.GetComponent<GameManager>().canClickTrigger()){
            gm.GetComponent<GameManager>().setClickTrigger(true);
            AnimatorStateInfo stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            if(stateInfo.IsName("FigureDown")){
                if(gm.gameObject.GetComponent<GameManager>().hiHaPuesto()){
                    if(stateInfo.IsName("FigureDown")){
                        Animator an = GetComponent<Animator>();
                        an.SetTrigger("FigureShowTrigger");
                    }
                    gm.GetComponent<GameManager>().cardTouched(gameObject);
                }else{
                    Debug.Log("Ja hi ha cartes aixecades!");
                }
            }
        }
    }

    public void esconder(){
        AnimatorStateInfo stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("FigureUp"))
        {
            Animator an = GetComponent<Animator>();
            an.SetTrigger("FigureHideTrigger");
        }
    }

    public void showFigure(){
        AnimatorStateInfo stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("FigureDown"))
        {
            Animator an = GetComponent<Animator>();
            an.SetTrigger("FigureShowTrigger");
        }
    }

    
}
