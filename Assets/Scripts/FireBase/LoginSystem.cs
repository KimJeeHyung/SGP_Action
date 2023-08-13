using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginSystem : MonoBehaviour
{
    // Start is called before the first frame update

    public InputField email;
    public InputField password;


    public Text outputText;



    void Start()
    {
        FirebaseAuthManager.Instance.LoginState += OnChangedState;
        FirebaseAuthManager.Instance.Init();

    }

    private void OnChangedState(bool sign)
    {
        outputText.text = sign ? "로그인 : " : "로그아웃 :";
        outputText.text += FirebaseAuthManager.Instance.userId;
        if(sign == true)
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    public void Create()
    {
        string e = email.text;
        string p = password.text;

        FirebaseAuthManager.Instance.Create(e, p);
    }

    public void LogIn()
    {
        FirebaseAuthManager.Instance.Login(email.text, password.text);

    }

    public void LogOut()
    {
        FirebaseAuthManager.Instance.LogOut();
    }

}
