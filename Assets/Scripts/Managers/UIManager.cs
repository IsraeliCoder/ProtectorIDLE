    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public TMP_Text moneyText;
    public GameObject statsPanel;
    public Button statsBtn;
    void Update() 
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        Button btn = statsBtn.GetComponent<Button>();
        btn.onClick.AddListener(StatsPanelToggle);
    }

    public void StatsPanelToggle()
    {
        statsPanel.SetActive(!statsPanel.activeSelf);
    }

}
