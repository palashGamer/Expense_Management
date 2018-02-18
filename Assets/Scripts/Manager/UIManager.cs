using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UIZone))]
public class UIManager : MonoBehaviour {

    UIZone uiZone;
    public UIZone m_uiZone
    {
        get
        {
            if (uiZone == null)
                uiZone = GetComponent<UIZone>();
            return uiZone;
        }
    }

    public static UIManager m_instance;

    void Awake()
    {
        m_instance = this;
    }
    public void OpenExpensePanel()
    {
        m_uiZone.ExpensePanel.gameObject.SetActive(true);
        m_uiZone.MonthPanel.SetActive(false);
        m_uiZone.BackButton.SetActive(true);
        m_uiZone.BackButton.GetComponent<Button>().onClick.AddListener(delegate {
            m_uiZone.BackButton.GetComponent<Button>().onClick.RemoveAllListeners();
            OpenMonthPanel();   
        });
    }

    public void OpenMonthPanel()
    {
        m_uiZone.ExpensePanel.gameObject.SetActive(false);
        m_uiZone.MonthPanel.SetActive(true);
        m_uiZone.BackButton.SetActive(false);
    }

    public void OnFixedPanelClicked()
    {
        m_uiZone.ExpensePanel.gameObject.SetActive(false);
        m_uiZone.MonthPanel.SetActive(false);
        m_uiZone.BackButton.SetActive(true);
        m_uiZone.BackButton.GetComponent<Button>().onClick.AddListener(delegate {
            m_uiZone.BackButton.GetComponent<Button>().onClick.RemoveAllListeners();
            OpenMonthPanel();
            m_uiZone.FixedPanel.gameObject.SetActive(false);
        });
        m_uiZone.FixedPanel.gameObject.SetActive(true);

        UIManager.m_instance.m_uiZone.FixedPanel.LoadData_ExpensePanel(ExpenseDataHandler.GetMonthlyFixedData(GlobalRuntimeValue.m_CurrentMonthStartDate));
    }
    private void OnApplicationQuit()
    {
        AndroidUtility.SaveExternally(JsonUtility.ToJson(ExpenseDataHandler.m_yearlyExpenseInfo));
    }
    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            AndroidUtility.SaveExternally(JsonUtility.ToJson(ExpenseDataHandler.m_yearlyExpenseInfo));
        }
    }
}
