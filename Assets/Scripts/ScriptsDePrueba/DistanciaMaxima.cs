using Oculus.Interaction;
using System.Collections;
using UnityEngine;

public class DistanciaMaxima : MonoBehaviour
{
    public Transform connectedObject;
    public float maxDistance;
    private GameObject handsGameObject;

    private void Start()
    {
        handsGameObject = transform.Find("Manos")?.gameObject;
    }

    public void ComprobarDistancia()
    {
        Debug.Log("Comprobando distancia...");
        // Medir la distancia entre los dos objetos
        float distance = Vector3.Distance(transform.position, connectedObject.position);

        // Si la distancia es mayor al límite, soltar el objeto
        if (distance > maxDistance)
        {
            handsGameObject.SetActive(false);
            StartCoroutine(EsperarUnSegundo());
        }
    }

    IEnumerator EsperarUnSegundo()
    {
        // Esperamos 1 segundo
        yield return new WaitForSeconds(1f);

        // Después de 1 segundo, se ejecuta este código
        handsGameObject.SetActive(true);
    }
}
