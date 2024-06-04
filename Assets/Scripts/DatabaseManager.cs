using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

public class DatabaseManager : MonoBehaviour
{
    public inputField Name;
    public inputField Email;
    public inputField Password;
    public inputField Points;

    private DatabaseReference dbreference;

    public Text NameText;
    public Text EmailText;
    public Text PasswordText;
    public Text PointsText;

    private string userID;


    void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier
        dbreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void CreateUser(string name, string email, string password, int points)
    {
        User newUser = new User(Name.text, Email.text, Password.text, int.Parse(Points.text));
        string json = JsonUtility.ToJson(newUser);

        dbreference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }


    public IEnumerator GetName(Action<string> onCallBack)
    {
        var userNameData = dbreference.Child("users").Child(userID).Child("name").GetValueAsync();

        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);

        if (userNamerData != null)
        {
            DataSnapshot snapshot = userNameData.Result;

            onCallBack.Invoke(snapshot.Value.ToString());
        }

    }

    public IEnumerator GetEmail(Action<string> onCallBack)
    {
        var userEmailData = dbreference.Child("users").Child(userID).Child("email").GetValueAsync();

        yield return new WaitUntil(predicate: () => userEmailData.IsCompleted);

            if (userNamerData != null)
            {
                DataSnapshot snapshot = userEmailData.Result;

                onCallBack.Invoke(snapshot.Value.ToString());
            }

    }

    public IEnumerator GetPassword(Action<string> onCallBack)
    {
        var userPasswordData = dbreference.Child("users").Child(userID).Child("password").GetValueAsync();

        yield return new WaitUntil(predicate: () => userPasswordData.IsCompleted);

                if (userNamerData != null)
                {
                    DataSnapshot snapshot = userPasswordData.Result;

                    onCallBack.Invoke(snapshot.Value.ToString());
                }

    }

    public IEnumerator GetPoints(Action<string> onCallBack)
    {
        var userPointsData = dbreference.Child("users").Child(userID).Child("points").GetValueAsync();

        yield return new WaitUntil(predicate: () => userPointsData.IsCompleted);

                    if (userNamerData != null)
                    {
                        DataSnapshot snapshot = userPointsData.Result;

                        onCallBack.Invoke(int.Parse(snapshot.Value.ToString()));
                    }
    }

    public void GetUserInfo()
    {
        StartCoroutine(GetName((string name -> {
            NameText.text = "Name: " + name;

        }));

        StartCoroutine(GetEmail((string email -> {
            EmailText.text = "Email: " + email;

        }));

        StartCoroutine(GetPassword((string password -> {
            PasswordText.text = "Password: " + password;

        }));

        StartCoroutine(GetPoints((int points -> {
            PointsText.text = "Points: " + points.ToString();

        }));
    }
}
