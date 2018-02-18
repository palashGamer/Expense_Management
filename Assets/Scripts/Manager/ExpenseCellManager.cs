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
    private void OnEnable()
    {
        HandleColor();
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
        HandleColor();
    }

    public void OnAddClick()
    {
        int a_value = int.Parse(m_expenseCellZone.m_inputField.text);

        a_value += int.Parse(m_expenseCellZone.m_additionInputField.text);

        m_expenseCellZone.m_inputField.text = a_value.ToString();
    }

    public void OnMinusClick()
    {
        int a_value = int.Parse(m_expenseCellZone.m_inputField.text);

        a_value -= int.Parse(m_expenseCellZone.m_additionInputField.text);

        m_expenseCellZone.m_inputField.text = a_value.ToString();
    }
    void HandleColor()
    {
        if (m_expenseCellZone.m_OthersHeading != null)
        {
            Debug.Log("hiho: " + m_expenseCellZone.m_OthersHeading.text);
            if (string.IsNullOrEmpty(m_expenseCellZone.m_OthersHeading.text))
            {
               // Debug.Log("grey");
                m_expenseCellZone.m_OthersHeading.selectionColor = Color.grey;
            }
            else
            {
               // Debug.Log("blue");
                m_expenseCellZone.m_OthersHeading.selectionColor = Color.blue;
            }
        }
    }


}
