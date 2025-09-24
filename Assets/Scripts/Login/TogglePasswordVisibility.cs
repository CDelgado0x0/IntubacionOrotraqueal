using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TogglePasswordVisibility : MonoBehaviour
{
    public TMP_InputField passwordInputField;
    public Button toggleButton;

    private bool isPasswordHidden = true;

    private void Start()
    {
        toggleButton.onClick.AddListener(ToggleVisibility);
        UpdateInputField();
    }

    private void ToggleVisibility()
    {
        isPasswordHidden = !isPasswordHidden;
        UpdateInputField();
    }

    private void UpdateInputField()
    {
        if (isPasswordHidden)
        {
            passwordInputField.contentType = TMP_InputField.ContentType.Password;
        }
        else
        {
            passwordInputField.contentType = TMP_InputField.ContentType.Standard;
        }
        passwordInputField.ForceLabelUpdate();
    }
}
