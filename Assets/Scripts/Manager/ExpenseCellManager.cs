using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ExpenseCellZone))]
public class ExpenseCellManager : BManager {

    ExpenseCellZone expenseCellZone;
    public ExpenseCellZone m_expenseCellZone
    {
        get
        {
            if (expenseCellZone == null)
                expenseCellZone = GetComponent<ExpenseCellZone>();
            return expenseCellZone;
        }
    }

    public void ShowDetails(int value)
    {
        m_expenseCellZone.m_inputField.text = value.ToString();

       // m_expenseCellZone.m_deleteButton.enabled = m_expenseCellZone.m_showDeleteBtn;
    }

    public void ShowDetails(string key, int value)
    {
        m_expenseCellZone.m_OthersHeading.text = key;
        m_expenseCellZone.m_inputField.text = value.ToString();
    }

    public void OnAddClick()
    {
        int a_value = int.Parse(m_expenseCellZone.m_inputField.text);

        a_value += int.Parse(m_expenseCellZone.m_additionInputField.text);

        m_expenseCellZone.m_inputField.text = a_value.ToString();
    }

    public void OnMinusClick()
    {
        
    }


}
