using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ExpenseOtherCellZone))]
public class ExpenseOtherCellManager : BManager {

    ExpenseOtherCellZone expenseOtherCellZone;
    public ExpenseOtherCellZone m_expenseOtherCellZone
    {
        get
        {
            if (expenseOtherCellZone == null)
                expenseOtherCellZone = GetComponent<ExpenseOtherCellZone>();
            return expenseOtherCellZone;
        }
    }

    public void ShowDetails(string a_data)
    {
        GenerateChild();

        if (string.IsNullOrEmpty(a_data))
            return;

        string[] l_data = a_data.Split(',');
        //m_expenseOtherCellZone.m_expenseChilds = new ExpenseCellManager[l_data.Length];
        string key = "";
        int value = 0;

        for (int count = 0; count < l_data.Length;count++)
        {
            if (count >= m_expenseOtherCellZone.m_expenseChilds.Count)
                break;
            
            string[] l_indiData = l_data[count].Split('=');

            key = l_indiData[0];
            value = int.Parse(l_indiData[1]);

            m_expenseOtherCellZone.m_expenseChilds[count].ShowDetails(key, value);

        }
        
    }
    void GenerateChild()
    {Debug.LogError("GEnerate child called");
        GameObject Obj = null;
        ClearChild();
        for (int count = 0; count < m_expenseOtherCellZone.m_PrefabCount; count++)
        {
            Obj = Instantiate<GameObject>(m_expenseOtherCellZone.m_expensePrefab);
            Obj.transform.SetParent(this.transform,false);
            m_expenseOtherCellZone.m_expenseChilds.Add(Obj.GetComponent<ExpenseCellManager>());
        }
    }
    void ClearChild()
    {
        for (int count = 0; count < m_expenseOtherCellZone.m_expenseChilds.Count; count++)
        {
            Destroy(m_expenseOtherCellZone.m_expenseChilds[count].gameObject);
            Debug.Log("cleared: "+count);
        }

        m_expenseOtherCellZone.m_expenseChilds.Clear();
    }

	
}
