using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static Player;

public class Inicio : MonoBehaviour
{
    public Button botao;
    public InputField nome;
    public InputField email;


    void Start(){
        botao.onClick.AddListener(TaskOnClick);
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Return)){
            TaskOnClick();
        }
        
    }

    void TaskOnClick(){
        if (nome.text != "Digite seu nome " && email.text != "Digite seu email " 
            && nome.text != "" && nome.text != ""
            && nome.text != " " && nome.text != " ")

                Player.name = nome.text;
                Player.email = email.text;
                Player.points = 0;

                SceneManager.LoadScene("SampleScene");
    }

}
