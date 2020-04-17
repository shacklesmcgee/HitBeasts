using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button btnLogin;
    [Space(10)]

    [Header("Text Fields")]
    public Text txtName;
    public InputField inpNamePlaceholder;
    public Text txtNamePlaceholder;

    public Text txtPass;
    public InputField inpPassPlaceholder;
    public Text txtPassPlaceholder;

    public Text txtConsole;

    private Character currentPlayer;

    // Start is called before the first frame update
    void Start()
    {
        btnLogin.onClick.AddListener(delegate { ConfirmLogin(); });
    }

    public void ShowScreen()
    {
        inpNamePlaceholder.text = "";
        txtNamePlaceholder.text = "Player 1 Name...";
        txtName.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetConsoleText(string text)
    {
        txtConsole.text = text;
    }

    public void ConfirmLogin()
    {
        if (txtName.text == "" || txtName.text == null)       
        {
            inpNamePlaceholder.GetComponent<Image>().color = Color.red;
        }

        else if (txtPass.text == "" || txtPass.text == null)
        {
            inpPassPlaceholder.GetComponent<Image>().color = Color.red;
        }

        else
        {
            inpNamePlaceholder.GetComponent<Image>().color = Color.white;
            inpPassPlaceholder.GetComponent<Image>().color = Color.white;

            this.GetComponent<NetworkManager>().LoginPlayer(txtName.text, txtPass.text);
        }
    }
}
