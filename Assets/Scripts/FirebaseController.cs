using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.EditorUtilities;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using System.Net.Mail;
using UnityEngine.Tilemaps;
using Firebase;
using Firebase.Auth;
using System;
using System.Threading.Tasks;

public class FirebaseController : MonoBehaviour
{
    public GameObject LoginPanel, SignUpPanel, ProfilePanel, forgetPasswordPanel, NotificationPanel;

    public TMP_InputField LoginEmail, LogInPassword, SignUpEmail, SignUpPassword, SignUpConfirmPassword, SignUpUserName, forgetPassEmail;

    public TMP_Text notif_Title_Text, notif_Message_Text, ProfileUserName_Text, ProfileUserEmail_Text;

    public Toggle rememberMe;

    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    void Start()
    {
        
    }


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
        SignInUser(LoginEmail.text, LogInPassword.text);
    }

    public void SignUpUser()
    {
        if (string.IsNullOrEmpty(SignUpEmail.text) && string.IsNullOrEmpty(SignUpPassword.text)&&string.IsNullOrEmpty(SignUpUserName.text))
        {
            showNotificationMessage("Error", "Fields Empty! Please, Input details in all Fields");
            return;
        }
        //Do SignUp
        CreateUser(SignUpEmail.text, SignUpPassword.text, SignUpUserName.text);
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

    void CreateUser(string email, string password, string Username)
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

            UpdateUserProfile(Username);
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

            Firebase.Auth.AuthResult result = task.Result;//FirebaseUser newUser
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                 result.User.DisplayName, result.User.UserId);//newUser without result

            ProfileUserName_Text.text = "" + result.User.DisplayName;//newUser
            ProfileUserEmail_Text.text = "" + result.User.Email;
            OpenProfilePanel();
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
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                //displayName = user.DisplayName ?? "";
                //emailAddress = user.Email ?? "";
                //photoUrl = user.PhotoUrl ?? "";
            }
        }
    }

    void OnDestroy()
    {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
    }


    void UpdateUserProfile(string UserName)
    {
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile
            {
                DisplayName = UserName,
                PhotoUrl = new System.Uri("https://via.placeholder.com/150"),
            };
            //https://via.placeholder.com/150C/0%20https://placeholder.com/
            user.UpdateUserProfileAsync(profile).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("User profile updated successfully.");

                showNotificationMessage("Alert", "Account Successfully Created");
            });
        }
    }
}

