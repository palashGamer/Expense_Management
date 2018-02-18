using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DateZone))]
public class DateManager : BManager {

    DateZone dateZone;
    DateZone m_dateZone
    {
        get
        {
            if (dateZone == null)
                dateZone = GetComponent<DateZone>();
            return dateZone;
        }
    }

    DateTime m_myDate;
    Button m_Button;

    private void Awake()
    {
        m_Button = gameObject.GetComponent<Button>();
    }
    public void AssignData(DayWiseShortDataInfo a_data)
    {
        if (!EnableDisableMe(a_data.m_hasEntry))
            return;

        m_dateZone.m_DateText.text = a_data.m_dayOfMonth.ToString();

        if (a_data.m_totalExpense > 0)
        {
            if (a_data.m_totalExpense > 400)
            {
                m_dateZone.m_ExpenseText.color = Color.red;
            }
            else
                m_dateZone.m_ExpenseText.color = Color.blue;
            
            m_dateZone.m_ExpenseText.text = a_data.m_totalExpense.ToString();
            m_dateZone.m_ExpenseText.enabled = true;
        }
        else
        {
            m_dateZone.m_ExpenseText.enabled = false;
        }
        m_Button.onClick.RemoveAllListeners();
        m_Button.onClick.AddListener(delegate {
            OnClickHandler();
        });
        m_myDate = a_data.m_date;
    }

    public void OnClickHandler()
    {
        UIManager.m_instance.OpenExpensePanel();

        UIManager.m_instance.m_uiZone.ExpensePanel.LoadData(ExpenseDataHandler.GetDailyExpenseData(m_myDate),delegate(Expenses a_expenses) {
            StartCoroutine(SaveData(a_expenses));   
        });
    }
    public IEnumerator SaveData(Expenses a_expenses)
    {
        ExpenseDataHandler.m_yearlyExpenseInfo.expenseInfo[m_myDate.Month - 1].Bazaar[m_myDate.Day - 1] = a_expenses.Bazaar;
        ExpenseDataHandler.m_yearlyExpenseInfo.expenseInfo[m_myDate.Month - 1].Shopping[m_myDate.Day - 1] = a_expenses.Shopping;
        ExpenseDataHandler.m_yearlyExpenseInfo.expenseInfo[m_myDate.Month - 1].Office[m_myDate.Day - 1] = a_expenses.Office;
        ExpenseDataHandler.m_yearlyExpenseInfo.expenseInfo[m_myDate.Month - 1].Others[m_myDate.Day - 1] = a_expenses.Other;

        yield return new WaitForSeconds(0.5f);
        ExpenseDataHandler.SaveData("2018",ExpenseDataHandler.m_yearlyExpenseInfo);
        MonthManager.m_instance.m_Start();
    }
    bool EnableDisableMe(bool status)
    {
        m_dateZone.m_InnerPanel.SetActive(status);
        return status;
    }
}
