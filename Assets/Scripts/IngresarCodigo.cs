using UnityEngine;
using UnityEngine.UI;

public class CodigoInput : MonoBehaviour
{
    [SerializeField] private InputField codigoInput;
    [SerializeField] private Button enviarBtn;

    public static string CodigoIngresado; // accesible globalmente

    void Start()
    {
        enviarBtn.onClick.AddListener(GuardarCodigo);
    }

    void GuardarCodigo()
    {
        CodigoIngresado = codigoInput.text;
        Debug.Log("Código ingresado: " + CodigoIngresado);
    }
}
