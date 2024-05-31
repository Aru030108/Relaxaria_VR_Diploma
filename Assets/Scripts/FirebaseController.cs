using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FirebaseController : MonoBehaviour
{
    public GameObject LoginPanel, SignUpPanel, ProfilePanel, forgetPasswordPanel;

    public TMP_InputField LoginEmail, LogInPassword, SignUpEmail, SignUpPassword, SignUpConfirmPassword, SignUpUserName, forgetPassEmail;

    public void OpenLoginPanel()
    {
        LoginPanel.SetActive(true);
        SignUpPanel.SetActive(false);
        ProfilePanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);
    }

    public void OpenSignUpPanel()
    {
        LoginPanel.SetActive(false);
        SignUpPanel.SetActive(true);
        ProfilePanel.SetActive(false);
        forgetPasswordPanel.SetActive(false);
    }

    public void OpenProfilePanel()
    {
        LoginPanel.SetActive(false);
        SignUpPanel.SetActive(false);
        ProfilePanel.SetActive(true);
        forgetPasswordPanel.SetActive(false);
    }

    public void OpenforgetPasswordPanel()
    {
        LoginPanel.SetActive(false);
        SignUpPanel.SetActive(false);
        ProfilePanel.SetActive(false);
        forgetPasswordPanel.SetActive(true);
    }


    public void LoginUser()
    {
        if(string.IsNullOrEmpty(LoginEmail.text)&&string.IsNullOrEmpty(LogInPassword.text))
        {
            return;
        }
        //Do login
    }

    public void SignUpUser()
    {
        if (string.IsNullOrEmpty(SignUpEmail.text) && string.IsNullOrEmpty(SignUpPassword.text)&&string.IsNullOrEmpty(SignUpUserName.text))
        {
            return;
        }
        //Do SignUp
    }


    public void forgetPassword()
    {
        if (string.IsNullOrEmpty(forgetPassEmail.text))
        {
            return;
        }
        //Do SignUp
    }

}

