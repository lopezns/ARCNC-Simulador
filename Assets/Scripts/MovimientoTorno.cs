using UnityEngine;

public class MovimientoTorno : MonoBehaviour
{
    [SerializeField] private Transform mandril;
    [SerializeField] private Transform ejeX;
    [SerializeField] private Transform ejeZ;

    [SerializeField] private float velocidadRPM = 100f;
    private bool moviendo = false;

    void Update()
    {
        if (moviendo)
        {
            float gradosPorSegundo = (velocidadRPM / 60f) * 360f;
            mandril.Rotate(Vector3.forward, gradosPorSegundo * Time.deltaTime);

            // ejemplo de movimiento lineal en 0–1
            ejeX.localPosition = new Vector3(Mathf.PingPong(Time.time, 1f), ejeX.localPosition.y, ejeX.localPosition.z);
            ejeZ.localPosition = new Vector3(ejeZ.localPosition.x, ejeZ.localPosition.y, Mathf.PingPong(Time.time, 1f));
        }
    }

    public void IniciarMovimiento() => moviendo = true;
    public void DetenerMovimiento() => moviendo = false;
    public bool SeEstaMoviendo() => moviendo;
}
