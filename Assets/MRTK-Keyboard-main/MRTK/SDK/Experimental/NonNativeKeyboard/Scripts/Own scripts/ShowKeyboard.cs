using Microsoft.MixedReality.Toolkit.Experimental.UI;
using TMPro;
using UnityEngine;
using static Microsoft.MixedReality.Toolkit.Experimental.UI.NonNativeKeyboard;
public class ShowKeyboard : MonoBehaviour
{
    private TMP_InputField inputField;

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

    }
}
