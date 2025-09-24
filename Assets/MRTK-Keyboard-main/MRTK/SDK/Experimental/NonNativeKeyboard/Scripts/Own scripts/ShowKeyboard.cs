using Microsoft.MixedReality.Toolkit.Experimental.UI;
using TMPro;
using UnityEngine;
using static Microsoft.MixedReality.Toolkit.Experimental.UI.NonNativeKeyboard;
public class ShowKeyboard : MonoBehaviour
{
    private TMP_InputField inputField;

    public float scale = 0.1f;
    public Transform positionSource;
    public float rightOffset = 0.25f; // Ajusta este valor para mover el teclado más a la derecha

    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onSelect.AddListener(x => OpenKeyboard());
    }   

    public void OpenKeyboard()
    {

        //si es una contraseña el LayoutType es Alpha
        //Si no es una contraseña el LayoutType es Email
        NonNativeKeyboard.Instance.InputField = inputField;

        if (inputField.contentType == TMP_InputField.ContentType.Password)
        {
            NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);
        }
        else
        {
            NonNativeKeyboard.Instance.PresentKeyboard(inputField.text, LayoutType.Email);
        }

        


        // Escala inicial
        NonNativeKeyboard.Instance.transform.localScale = new Vector3(scale, scale, scale);
    }

    void Update()
    {
        if (NonNativeKeyboard.Instance != null && positionSource != null)
        {
            // Offset lateral para que esté más a la derecha de la muñeca
            Vector3 offset = positionSource.right * rightOffset;
            Vector3 targetPosition = positionSource.position + offset;

            NonNativeKeyboard.Instance.RepositionKeyboard(targetPosition);
            NonNativeKeyboard.Instance.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
