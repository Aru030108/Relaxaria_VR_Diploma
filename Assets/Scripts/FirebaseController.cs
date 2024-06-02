using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.EditorUtilities;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using System.Net.Mail;
using UnityEngine.Tilemaps;

public class FirebaseController : MonoBehaviour
{
    public GameObject LoginPanel, SignUpPanel, ProfilePanel, forgetPasswordPanel, NotificationPanel;

    public TMP_InputField LoginEmail, LogInPassword, SignUpEmail, SignUpPassword, SignUpConfirmPassword, SignUpUserName, forgetPassEmail;

    public TMP_Text notif_Title_Text, notif_Message_Text, ProfileUserName_Text, ProfileUserEmail_Text;

    public Toggle rememberMe;

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
            showNotificationMessage("Error", "Fields Empty! Please, Input details in all Fields");
            return;
        }
        //Do login
    }

    public void SignUpUser()
    {
        if (string.IsNullOrEmpty(SignUpEmail.text) && string.IsNullOrEmpty(SignUpPassword.text)&&string.IsNullOrEmpty(SignUpUserName.text))
        {
            showNotificationMessage("Error", "Fields Empty! Please, Input details in all Fields");
            return;
        }
        //Do SignUp
    }


    public void forgetPassword()
    {
        if (string.IsNullOrEmpty(forgetPassEmail.text))
        {
            showNotificationMessage("Error", "Fields Empty! Please, Input details in all Fields");
            return;
        }
        //Do ForgetPass
    }

    public void showNotificationMessage(string title, string message)
    {
        notif_Title_Text.text = "" + title;
        notif_Message_Text.text = "" + message;

        NotificationPanel.SetActive(true);
    }

    public void CloseNotificationMessage()
    {
        notif_Title_Text.text = "";
        notif_Message_Text.text = "";

        NotificationPanel.SetActive(false);
    }

    public void LogOut()
    {
        //ProfilePanel.SetActive(false);
        ProfileUserEmail_Text.text= "";
        ProfileUserName_Text.text= "";
        OpenLoginPanel();
    }

    void CreateUser(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });
    }


    public void SignInUser(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });
    }



    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null
                && auth.CurrentUser.IsValid();
            if (!signedIn && user != null)
            {
                DebugLog("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                DebugLog("Signed in " + user.UserId);
                displayName = user.DisplayName ?? "";
                emailAddress = user.Email ?? "";
                photoUrl = user.PhotoUrl ?? "";
            }
        }
    }

    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }


}

