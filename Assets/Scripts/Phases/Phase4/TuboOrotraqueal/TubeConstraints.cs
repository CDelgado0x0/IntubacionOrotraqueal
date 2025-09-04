using UnityEngine;

public class TubeConstraints : MonoBehaviour
{
    [SerializeField] private GameObject ManosTubo;
    [SerializeField] private GameObject ManosAnimacion;

    private void Start()
    {
        ManosTubo.SetActive(true);
        ManosAnimacion.SetActive(false);
    }

    public void changeGrabMovement()
    {
        ManosTubo.SetActive(!ManosTubo.activeSelf);
        ManosAnimacion.SetActive(!ManosAnimacion.activeSelf);
    }
}
