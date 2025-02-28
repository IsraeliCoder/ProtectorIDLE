using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{

    protected Number money;
    protected UIManager uiManager;


    void Start()
    {
        money = new Number(0, 0, 0);
        uiManager = GetComponent<UIManager>();
    }


    public void addMoney(Number toAdd)
    {
        money = money.Add(toAdd);
        uiManager.moneyText.text = money.getString(Number.NumberShowFormat.eWords);
    }


}
