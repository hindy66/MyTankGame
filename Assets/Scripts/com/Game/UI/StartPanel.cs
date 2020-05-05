using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Start
/// </summary>
public class StartPanel : MonoBehaviour {
    public Image ShopPanel;

    public Image SettingPanel;

    public GameObject inputnamePanel;

    public void StartClick()
    {
        inputnamePanel.SetActive(true);
    }

    public void ShopClick()
    {
        Application.Quit();
    }

    public void SettingClick()
    {
        SettingPanel.enabled = true;
    }
}
