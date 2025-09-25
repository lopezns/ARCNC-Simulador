using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class MatchData
{
    public int match_ID;
    public string startDate;
    public string endDate;
    public bool isFinished;
    public int currentScore;
    public bool isDeleted;
}

[System.Serializable]
public class GCodeData
{
    public int gCode_ID;
    public MatchData match;
    public string code;
    public bool isDeleted;
}

public class ValidarCodigo : MonoBehaviour
{
    [SerializeField] private Text feedbackText;

    private string apiUrl = "https://simuladortornoyfresadoracnc.somee.com/api/GCode_";

    public void StartValidacion()
    {
        string codigoUsuario = CodigoInput.CodigoIngresado;
        StartCoroutine(ValidarConAPI(codigoUsuario));
    }

    private IEnumerator ValidarConAPI(string codigoUsuario)
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            feedbackText.text = "Error en conexión: " + request.error;
        }
        else
        {
            string json = request.downloadHandler.text;
            GCodeData[] codigos = JsonHelper.FromJson<GCodeData>(json);

            bool encontrado = false;
            foreach (var item in codigos)
            {
                if (item.code.Contains(codigoUsuario)) 
                {
                    feedbackText.text = "Código encontrado en base de datos.";
                    encontrado = true;
                    break;
                }
            }

            if (!encontrado)
                feedbackText.text = "Código no válido.";
        }
    }
}
