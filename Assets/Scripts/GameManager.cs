using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq.Expressions;
using UnityEngine;
using Unity.VisualScripting;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject[] cards;
    public GameObject[] cardSons;
    private double id_double=0;
    private GameObject[] cardsSelected = new GameObject[2];
    private int cartesAdivinades;
    private bool clickTrigger=false;
    private float clickCooldown=0;
    private bool startGameTrigger=false;
    public Button startButton;
    public TextMeshProUGUI timeText;
    private double timeNum;
    public TextMeshProUGUI bestTime;
    public TextMeshProUGUI titolText;
    public TextMeshProUGUI intentsText;
    public AudioClip[] audioClips; // Array para almacenar varios clips de audio
    public AudioSource audioSource;  // Asigna el AudioSource desde el Inspector

    //audioSource.clip = audioClips[index];
    //audioSource.Play();

    // Start is called before the first frame update
    void Start()
    {
        if (startButton != null)
        {
            startButton.onClick.AddListener(AccionBoton);
        }
        titolText.text="Turtle Memory";
        intentsText.text="Intents: "+PlayerPrefs.GetInt("Intents", 0);
        bestTime.text="Best Time: "+PlayerPrefs.GetInt("BestScore", 0);//Buscar el best time en vete tu a saber donde
        timeText.text="Time: "+0;
        cardsSelected[0]=null;
        cardsSelected[1]=null;
        cartesAdivinades=0;
        cards = GameObject.FindGameObjectsWithTag("CardTag");
        cardSons = GameObject.FindGameObjectsWithTag("CardTagSon");

    }

    // Update is called once per frame
    void Update()
    {

        if (startGameTrigger){
            timeNum+=Time.deltaTime;
            timeText.text=("Time: "+(int)timeNum);
            //Debug.Log(timeNum);
        }
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
            audioSource.clip = audioClips[4]; //Audio GameOver
            audioSource.PlayOneShot();
            if(timeNum < PlayerPrefs.GetInt("BestScore", 0)){
                PlayerPrefs.SetInt("BestScore", (int)timeNum);
            }
            cartesAdivinades+=1;
            Invoke("FinishScene",7);
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
            audioSource.clip = audioClips[1]; //Audio Hola
            audioSource.PlayOneShot();
            //Debug.Log("Carta seleccionada");
        }else{
            cardsSelected[1]=cardtouched.gameObject;
            audioSource.clip = audioClips[1]; //Audio Hola
            audioSource.PlayOneShot();
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
                int intentsNum = PlayerPrefs.GetInt("Intents", 0)+1;
                intentsText.text = "Intents: "+ intentsNum;
                PlayerPrefs.SetInt("Intents", intentsNum);
                audioSource.clip = audioClips[2]; //Audio Nonono
                audioSource.PlayOneShot();
                borrarSeleccionados(12);
            }else{
                Debug.Log("Cartas iguals!!" +cardsSelected[0].GetComponent<CardScript>().getId()+" "+cardsSelected[1].GetComponent<CardScript>().getId());
                cartesAdivinades++;
                audioSource.clip = audioClips[3]; //Audio Oleee
                audioSource.PlayOneShot();
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

    void Mezclar(GameObject[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n; i++)
        {
            // Generar un índice aleatorio entre i y n (exclusivo)
            int randomIndex = Random.Range(i, n);

            // Intercambiar el elemento actual con el elemento aleatorio
            GameObject temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    void AccionBoton()
    {
        audioSource.clip = audioClips[0]; //Clash royale inici
        audioSource.PlayOneShot();
        cardSons[0].GetComponent<CardScript>().setStartVar(true);
        startButton.gameObject.SetActive(false);
        startGameTrigger=true;
        titolText.text="";
        PlayerPrefs.SetInt("Intents", 0);
        intentsText.text="Intents: "+PlayerPrefs.GetInt("Intents", 0);
        foreach(GameObject card in cardSons){
            card.GetComponent<CardScript>().setId(id_double);
            id_double=id_double+0.5;
            Debug.Log(card.GetComponent<CardScript>().getId());
            Debug.Log("Materials/FigureMaterial" + card.GetComponent<CardScript>().getId());
            Material materialCargado = Resources.Load<Material>("Materials/FigureMaterial" + card.GetComponent<CardScript>().getId());
            card.GetComponent<CardScript>().getFiguraRenderer().material = materialCargado;
        }
    
        Mezclar(cards);

        int i=0;
        // Asignar e instanciar los objetos
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                // Instanciar el prefab en una posición del espacio 3D
                Vector3 posicion = new Vector3((float)((x * 2)-2), (float)-1.3, (y * 2)-2); // Ajustar la posición de cada objeto
                cards[i].transform.position = posicion;
                i++;
            }
        }
    }
    void FinishScene(){
        // Obtener el nombre de la escena actual
        string currentSceneName = SceneManager.GetActiveScene().name;
        
        // Cargar de nuevo la escena actual
        SceneManager.LoadScene(currentSceneName);
    }
}
