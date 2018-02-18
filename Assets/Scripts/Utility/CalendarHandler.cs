using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CalendarHandler : BMonoBehaviour {

    const int m_MaxPossibleRowInAMonth = 42;

    /* This is chart for max 42 days(as a month can expand max upto 6 weeks)... the index which are present in current month, will have data in their, rest will have no data...
     * Mon     Tues    Wed     Thurs    Fri     Satu    Sun
     * 1        2       3       4       5       6       7      
     * 8        9       10      11      12      13      14
     * 15       16      17      18      19      20      21
     * 22       23      24      25      26      27      28
     * 29       30      31      32      33      34      35
     * 36       37      38      39      40      41      42
     */
   /// <summary>
   /// Returns the specific month data, provided as Input
   /// </summary>
   /// <returns>The specific month data.</returns>
   /// <param name="a_MonthStartDate">Start date of the mmonth</param>
    public static DayWiseShortDataInfo[] GetSpecificMonthData(DateTime a_MonthStartDate){
        
        DayWiseShortDataInfo[] l_dayWiseShortData = new DayWiseShortDataInfo[m_MaxPossibleRowInAMonth];

        int l_dayInMonth = DateTime.DaysInMonth(a_MonthStartDate.Year,a_MonthStartDate.Month);

        int l_startingDateInIndex = (int)a_MonthStartDate.DayOfWeek;
        int l_endingDateinIndex = l_startingDateInIndex + l_dayInMonth;
        Debug.Log(l_startingDateInIndex+" : "+a_MonthStartDate.DayOfWeek);
        int[] a_totalExpenseDayWise = ExpenseDataHandler.LoadTotalExpense_InAMonth(a_MonthStartDate.Month);

        for (int count = l_startingDateInIndex; count < l_endingDateinIndex; count++)
        {
            l_dayWiseShortData[count].m_hasEntry = true;

            int l_dayOfMonth = count - l_startingDateInIndex + 1;
            l_dayWiseShortData[count].m_dayOfMonth = l_dayOfMonth;
            l_dayWiseShortData[count].m_totalExpense = a_totalExpenseDayWise[l_dayWiseShortData[count].m_dayOfMonth-1];
            l_dayWiseShortData[count].m_date = a_MonthStartDate.AddDays(l_dayOfMonth-1);
        }

        GlobalRuntimeValue.m_CurrentMonthStartDate = a_MonthStartDate;
        return l_dayWiseShortData;
    } 
}
