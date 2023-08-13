using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.UI;
using System;

public class FirebaseAuthManager 
{
    private static FirebaseAuthManager instance = null;

    public static FirebaseAuthManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new FirebaseAuthManager();
            }

            return instance;
        }
    }




    private FirebaseAuth auth; // 로그인 및 회원가입 등에 사용하는 파이어베이스
    private FirebaseUser user; // 인증이 완료된 유저 정보

    public string userId => user.UserId;
    public string userEmail => user.Email;


    public Action<bool> LoginState;
    public void Init()
    {
        auth = FirebaseAuth.DefaultInstance;

        if(auth.CurrentUser != null)
        {
            LogOut();
        }

        auth.StateChanged += OnChanged;
    }
    private void OnChanged(object sender, EventArgs e)
    {
        if(auth.CurrentUser != user)
        {
            bool signed = (auth.CurrentUser != user && auth.CurrentUser != null);

            if(!signed && user != null)
            {
                Debug.Log("로그아웃");
                LoginState?.Invoke(false);
            }

            user = auth.CurrentUser;

            if (signed)
            {
                Debug.Log("로그인");
                LoginState?.Invoke(true);
            }
        }
    }

    public void Create(string email, string password) {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
         {

             if (task.IsCanceled)
             {
                 Debug.LogError("회원가입 취소");
                 return;
             }
             if (task.IsFaulted)
             {
                 // 회원가입 실패 이유 => 이메일 비정상 / 비밀번호가 너무 간단 /이미 가입된 이메일 등등...
                 Debug.LogError("회원가입 실패");
                 return;
             }

             AuthResult authResult = task.Result;
             FirebaseUser newUser = authResult.User;


             Debug.Log("회원가입 완료");

         });
    }

    public void Login(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {

            if (task.IsCanceled)
            {
                Debug.LogError("로그인 취소");
                return;
            }
            if (task.IsFaulted)
            {
                // 회원가입 실패 이유 => 이메일 비정상 / 비밀번호가 너무 간단 /이미 가입된 이메일 등등...
                Debug.LogError("로그인 실패");
                return;
            }

            AuthResult authResult = task.Result;
            FirebaseUser newUser = authResult.User;


            Debug.Log("로그인 완료");

        });
    }

    public void LogOut()
    {
        auth.SignOut();
        Debug.Log("로그아웃 됨 !");
    }
}
