using UnityEngine;
using UnityEngine.UI;

public class TraerNombreEstudiante : MonoBehaviour
{
    public Text nombreEstudianteText; // Texto en la UI donde se mostrará el nombre

    void Start()
    {
        // Recuperamos los datos del usuario guardados en PlayerPrefs
        string userDataJson = PlayerPrefs.GetString("UserData", "");

        if (!string.IsNullOrEmpty(userDataJson))
        {
            // Convertimos el JSON en un objeto User
            UserData user = JsonUtility.FromJson<UserData>(userDataJson);

            if (user != null && !string.IsNullOrEmpty(user.name))
            {
                nombreEstudianteText.text = "Bienvenido, " + user.name;
            }
            else
            {
                nombreEstudianteText.text = "Estudiante";
            }
        }
        else
        {
            nombreEstudianteText.text = "Estudiante";
        }
    }
}

// Asegúrate de que esta clase esté declarada una sola vez en tu proyecto.
// Si ya existe en otro archivo, elimina esta parte para evitar duplicados.
[System.Serializable]
public class UserData
{
    public string id;
    public string name;
    public string email;
    public string user_Type;
}
