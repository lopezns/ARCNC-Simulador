using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System;

[System.Serializable]
public class UserType
{
    public int user_Type_ID;
    public string userType;
    public bool isDeleted;
}

[System.Serializable]
public class User
{
    public int user_ID;
    public string first_Name;
    public string last_Name;
    public string email;
    public string password;
    public UserType user_Type;
    public bool isDeleted;
}

public class LoginManager : MonoBehaviour
{
    public InputField inputEmail;
    public InputField inputPassword;
    public Button loginButton;
    public Text feedbackText;

    private string loginUrl = "https://simuladortornoyfresadoracnc.somee.com/api/User_/login";
    private string userUrl = "https://simuladortornoyfresadoracnc.somee.com/api/User_";

    void Start()
    {
        loginButton.onClick.AddListener(AttemptLogin);
    }

    public void AttemptLogin()
    {
        StartCoroutine(LoginCoroutine());
    }

    IEnumerator LoginCoroutine()
    {
        string email = UnityWebRequest.EscapeURL(inputEmail.text);
        string password = UnityWebRequest.EscapeURL(inputPassword.text);
        string fullUrl = $"{loginUrl}?Email={email}&Password={password}";

        UnityWebRequest www = UnityWebRequest.PostWwwForm(fullUrl, "");
        www.SetRequestHeader("accept", "*/*");

        yield return www.SendWebRequest();

        Debug.Log("Código HTTP: " + www.responseCode);
        Debug.Log("Respuesta cruda: " + www.downloadHandler.text);

        string response = www.downloadHandler.text;

        if (www.result == UnityWebRequest.Result.Success)
        {
            if (response.Contains("Login successful"))
            {
                PlayerPrefs.SetString("user_email", inputEmail.text);

                // Llamar al endpoint general para obtener el userType
                string userInfoUrl = $"{userUrl}?Email={email}";
                UnityWebRequest userReq = UnityWebRequest.Get(userInfoUrl);
                userReq.SetRequestHeader("accept", "application/json");

                yield return userReq.SendWebRequest();

                if (userReq.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Datos del usuario: " + userReq.downloadHandler.text);

                    User[] users = JsonHelper.FromJson<User>(userReq.downloadHandler.text);

                    if (users.Length > 0)
                    {
                        User currentUser = users[0];
                        int typeId = currentUser.user_Type.user_Type_ID;

                        switch (typeId)
                        {
                            case 1:
                                UnityEngine.SceneManagement.SceneManager.LoadScene("MenuEstudiante");
                                break;
                            case 2:
                                UnityEngine.SceneManagement.SceneManager.LoadScene("MenuTeacher");
                                break;
                            case 3:
                                UnityEngine.SceneManagement.SceneManager.LoadScene("MenuEstudiante");
                                break;
                            default:
                                feedbackText.text = "Tipo de usuario no válido.";
                                break;
                        }
                    }
                    else
                    {
                        feedbackText.text = "No se encontró información del usuario.";
                    }
                }
                else
                {
                    feedbackText.text = "Error obteniendo datos del usuario.";
                }
            }
            else
            {
                feedbackText.text = "Credenciales inválidas.";
            }
        }
        else
        {
            feedbackText.text = "No se encuentra registrado.";
        }
    }
}


// Helper para arreglos JSON
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}
