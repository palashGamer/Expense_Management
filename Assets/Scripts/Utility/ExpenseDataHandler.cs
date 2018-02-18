using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ExpenseDataHandler : BMonoBehaviour
{


    public static YearlyExpenseInfo m_yearlyExpenseInfo = new YearlyExpenseInfo();
    static TextAsset m_Data;

    // Use this for initialization
    void Start()
    {

    }


    public static void LoadData(string fileName)
    {
       // m_Data = Resources.Load(fileName) as TextAsset;
       //JsonUtility.FromJsonOverwrite(m_Data.text, m_yearlyExpenseInfo);

        string l_string = FileHandler.GetData(fileName);

        JsonUtility.FromJsonOverwrite(l_string, m_yearlyExpenseInfo);

        Resources.UnloadAsset(m_Data);
        if(m_yearlyExpenseInfo.expenseInfo.Count==0)
        {
            Debug.Log("no data... so generating...");

            for (int count = 0; count < 12; count++)
            {
                m_yearlyExpenseInfo.expenseInfo.Add(new ExpenseInfo());
                m_yearlyExpenseInfo.expenseInfo[count].Bazaar = new List<int>(31);
                m_yearlyExpenseInfo.expenseInfo[count].Office = new List<int>(31);
                m_yearlyExpenseInfo.expenseInfo[count].Shopping = new List<int>(31);
                m_yearlyExpenseInfo.expenseInfo[count].Others = new List<string>(31);

                for (int day = 0; day < 31; day++)
                {
                    m_yearlyExpenseInfo.expenseInfo[count].Bazaar.Add(0);
                    m_yearlyExpenseInfo.expenseInfo[count].Office.Add(0);
                    m_yearlyExpenseInfo.expenseInfo[count].Shopping.Add(0);
                    m_yearlyExpenseInfo.expenseInfo[count].Others.Add("Add Item=0");
                }
                m_yearlyExpenseInfo.expenseInfo[count].Fixed = null;
            }
            
            Debug.Log("now the length is: "+m_yearlyExpenseInfo.expenseInfo.Count);
        }

        m_Data = null;
       // ValidateAndResizeData();
    }
    private void OnApplicationQuit()
    {
        Resources.UnloadAsset(m_Data);
       // m_Data = null;
    }

    public static void SaveData(string a_fileName, YearlyExpenseInfo a_yearlyExpenseInfo)
    {
        string json = JsonUtility.ToJson(a_yearlyExpenseInfo);
        string path = "";

#if UNITY_EDITOR
        path = Application.dataPath + "/Resources/" + a_fileName + ".txt";
#else
        path = Application.persistentDataPath + a_fileName + ".txt";
#endif
        File.WriteAllText(path, json);
    }

    /// <summary>
    /// This will return 30/31 days array of total expense of all day
    /// </summary>
    /// <param name="a_monthInYear">A month in year. This value is ranged from 1-12.</param>
    public static int[] LoadTotalExpense_InAMonth(int a_monthInYear)
    {
        Debug.Log("Total length is: "+m_yearlyExpenseInfo.expenseInfo.Count+". and asking for: "+a_monthInYear);
        ExpenseInfo l_expenseInfo = m_yearlyExpenseInfo.expenseInfo[a_monthInYear-1];

        int a_MaxDayDataAvailable = l_expenseInfo.Bazaar.Count;
        int[] a_totalExpenseAllDays = new int[31];
        Debug.Log("maxDataAvailable: "+a_MaxDayDataAvailable);
        for (int count = 0; count < 31; count++)
        {
            if (count < a_MaxDayDataAvailable)
            {
                if(l_expenseInfo.Bazaar.Count > count)
                a_totalExpenseAllDays[count] += l_expenseInfo.Bazaar[count];

                if (l_expenseInfo.Office.Count > count)
                a_totalExpenseAllDays[count] += l_expenseInfo.Office[count];

                if (l_expenseInfo.Shopping.Count > count)
                a_totalExpenseAllDays[count] += l_expenseInfo.Shopping[count];

                if (l_expenseInfo.Others.Count > count)
                a_totalExpenseAllDays[count] += TotalOtherInfo(l_expenseInfo.Others[count]);
            }
            else
            {
                a_totalExpenseAllDays[count] = 0;
            }
        }
        return a_totalExpenseAllDays;
    }

    public static Expenses GetDailyExpenseData(DateTime a_dateTime)
    {
        Expenses expenses = new Expenses();
        ExpenseInfo l_expenseInfo = m_yearlyExpenseInfo.expenseInfo[a_dateTime.Month - 1];

        expenses.Bazaar = l_expenseInfo.Bazaar[a_dateTime.Day-1];
        expenses.Shopping = l_expenseInfo.Shopping[a_dateTime.Day - 1];
        expenses.Office = l_expenseInfo.Office[a_dateTime.Day - 1];
        expenses.Other = l_expenseInfo.Others[a_dateTime.Day - 1];

        return expenses;
    }
    public static string GetMonthlyFixedData(DateTime a_dateTime)
    {
        string l_data = "";

        int l_monthIndex = a_dateTime.Month;
        l_data = m_yearlyExpenseInfo.expenseInfo[l_monthIndex].Fixed;

        return l_data;
    }

    static int TotalOtherInfo(string a_data)
    {
        int l_total = 0;

        string[] l_data = a_data.Split(',');
        Debug.Log("l_data: "+a_data+" : "+l_data.Length);
        for (int count = 0; count < l_data.Length; count++)
        {
            string[] l_indiData = l_data[count].Split('=');

            l_total += int.Parse(l_indiData[1]);

        }

        return l_total;
    }

}
