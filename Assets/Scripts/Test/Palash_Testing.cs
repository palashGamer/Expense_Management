using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Palash_Testing : MonoBehaviour {

    [SerializeField]
    Text m_text;

    // Use this for initialization
    //   IEnumerator Start () {
    //       //Calendar a = new GregorianCalendar();
    //       Hashtable hash;
    //       m_text.text = m_text.text + Application.persistentDataPath+ "/utt.txt";
    //       try
    //       {
    //            hash = FileHandler.GetData("utt", ref m_text);
    //
    //       }
    //       catch(Exception ex)
    //       {
    //           m_text.text = m_text.text + ex.Message; 
    //       }
    //       try
    //       {
    //           FileHandler.SetData("utt", "  Oye Who the hell is there = Main hoon be");
    //       }
    //       catch(Exception ex)
    //       {
    //           m_text.text = m_text.text + ex.Message; 
    //       }
    //
    //       yield return new WaitForSeconds(2);
    //       hash = FileHandler.GetData("utt", ref m_text);
    //
    //
    //}
    //void Start()
    //{
    //    DayWiseShortDataInfo[] shortData = CalendarHandler.GetSpecificMonthData(DateTime.Today);
    //    Debug.Log("Short data: ");
    //}
    private void Awake()
    {
        //TextAsset l_Text = Resources.Load("uttas") as TextAsset;

        //YearlyExpenseInfo exp = new YearlyExpenseInfo();
        //JsonUtility.FromJsonOverwrite(l_Text.text,exp);

        //Debug.Log("boom: "+exp.expenseInfo[0].Bazaar[2]);

        ExpenseDataHandler.LoadData("2018");
        ExpenseDataHandler.SaveData("2018", ExpenseDataHandler.m_yearlyExpenseInfo);

       // m_text.text = AndroidUtility.GetAndroidExternalStoragePath();
        Debug.Log("Data loading complete");
    }
}
