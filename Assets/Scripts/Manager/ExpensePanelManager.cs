using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ExpensePanelZone))]
public class ExpensePanelManager : BManager {

    ExpensePanelZone expensePanelZone;
    ExpensePanelZone m_expensePanelZone
    {
        get
        {
            if (expensePanelZone == null)
                expensePanelZone = GetComponent<ExpensePanelZone>();
            return expensePanelZone;
        }
    }
    UnityAction<Expenses> m_action;

    public void LoadData(Expenses a_expenses, UnityAction<Expenses> a_action)
    {
        m_expensePanelZone.m_bazar.ShowDetails(a_expenses.Bazaar);
        m_expensePanelZone.m_shopping.ShowDetails(a_expenses.Shopping);
        m_expensePanelZone.m_office.ShowDetails(a_expenses.Office);
        m_expensePanelZone.m_other.ShowDetails(a_expenses.Other);
        m_action = a_action;
    }
    public void LoadData_ExpensePanel(string a_data)
    {
        m_expensePanelZone.m_fixed.ShowDetails(a_data);
    }

    public void SaveData()
    {
        if (m_expensePanelZone.m_bazar!=null)
        {
            Expenses l_expenses = new Expenses();

            l_expenses.Bazaar = int.Parse(m_expensePanelZone.m_bazar.m_expenseCellZone.m_inputField.text);
            l_expenses.Shopping = int.Parse(m_expensePanelZone.m_shopping.m_expenseCellZone.m_inputField.text);
            l_expenses.Office = int.Parse(m_expensePanelZone.m_office.m_expenseCellZone.m_inputField.text);
            l_expenses.Other = GenerateOtherString(m_expensePanelZone.m_other.m_expenseOtherCellZone);

            UIManager.m_instance.OpenMonthPanel();
            m_action.Invoke(l_expenses);
        }
        else
        {
            ExpenseDataHandler.m_yearlyExpenseInfo.expenseInfo[GlobalRuntimeValue.m_CurrentMonthStartDate.Month].Fixed = GenerateOtherString(m_expensePanelZone.m_fixed.m_expenseOtherCellZone);
            ExpenseDataHandler.SaveData("2018", ExpenseDataHandler.m_yearlyExpenseInfo);

            //UIManager.m_instance.OpenMonthPanel();
           // UIManager.m_instance.m_uiZone.FixedPanel.gameObject.SetActive(false);
        }
    }

    string GenerateOtherString(ExpenseOtherCellZone l_expZone)
    {
        string l_string = "";

        int iterator = l_expZone.m_expenseChilds.Count;

        for (int count = 0; count < iterator;count++)
        {
            if (string.IsNullOrEmpty(l_expZone.m_expenseChilds[count].m_expenseCellZone.m_OthersHeading.text))
                continue;
            
            if (count != 0)
                l_string += ",";
            
            l_string += l_expZone.m_expenseChilds[count].m_expenseCellZone.m_OthersHeading.text +"="+
                                 l_expZone.m_expenseChilds[count].m_expenseCellZone.m_inputField.text;

        }
        Debug.LogError("Formatted string: "+l_string);
        return l_string;
    }
}
