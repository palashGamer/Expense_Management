using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInfo : IInfo {

	
}

/// <summary>
/// This holds data for day wise short info...
/// </summary>
public struct DayWiseShortDataInfo {
    public bool m_hasEntry;
    [Range(1,31)]
    public int m_dayOfMonth;
    public int m_totalExpense;
    public DateTime m_date;
}

public class DayWiseDetailsDataInfo : IInfo{
    
}

public class MonthWiseShortDataInfo : IInfo{
    
}

[System.Serializable]
public class YearlyExpenseInfo{
    public List<ExpenseInfo> expenseInfo = new List<ExpenseInfo>(12);
}

[System.Serializable]
public class ExpenseInfo{
    public List<int> Bazaar, Office, Shopping = new List<int>(31);
    public List<string> Others = new List<string>(31);
    public string Fixed;
}

public struct Expenses{
    public int Bazaar, Office, Shopping;
    public string Other, Fixed;
}
