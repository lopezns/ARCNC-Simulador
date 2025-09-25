using UnityEngine;

public class DesgasteMaterial : MonoBehaviour
{
    [SerializeField] private Transform material;
    [SerializeField] private float velocidadDesgaste = 0.1f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Herramienta")) //Buril
        {
            Vector3 scale = material.localScale;

            if (scale.x > 0.1f)
            {
                material.localScale = new Vector3(scale.x - velocidadDesgaste * Time.deltaTime,
                                                  scale.y,
                                                  scale.z);
            }
        }
    }
}
