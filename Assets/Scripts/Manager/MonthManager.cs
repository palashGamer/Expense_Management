using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(MonthZone))]
public class MonthManager : BManager
{


    MonthZone monthZone;
    MonthZone m_monthZone
    {
        get
        {
            if (monthZone == null)
                monthZone = GetComponent<MonthZone>();
            return monthZone;
        }
    }

    public static MonthManager m_instance;

    const int m_dayInWeek = 7;
    readonly int m_maxDateCells = m_dayInWeek * 6;

    DayManager[] m_dayCells;
    DateManager[] m_dateCells;

    public int month;
    int monthCache;

    void Awake()
    {
        m_instance = this;
    }
    private void OnEnable()
    {
        m_Start();
    }
    public void m_Start()
    {
        GenerateMonthPrefabs();
        GenerateMonthData();
    }
    private void Update()
    {
        if (monthCache != month)
        {
            monthCache = month;
            Generate();
        }
    }
    void Generate()
    {
        m_Start();
    }
    void GenerateMonthPrefabs()
    {
        if(m_dayCells==null)
        m_dayCells = new DayManager[m_dayInWeek];

        if(m_dateCells == null)
        m_dateCells = new DateManager[m_maxDateCells];

        for (int count = 0; count < m_dayInWeek; count++)
        {
            if(m_dayCells[count]==null)
            m_dayCells[count] = Instantiate<GameObject>(m_monthZone.m_dayPrefab).GetComponent<DayManager>();
            
            m_dayCells[count].transform.parent = m_monthZone.m_dayPrefabParent;
            m_dayCells[count].transform.localScale = Vector3.one;
            m_dayCells[count].AssignDay(count);
        }

        for (int count = 0; count < m_maxDateCells; count++)
        {
            if(m_dateCells[count]==null)
            m_dateCells[count] = Instantiate<GameObject>(m_monthZone.m_datePrefab).GetComponent<DateManager>();
            
            m_dateCells[count].transform.parent = m_monthZone.m_datePrefabParent[Mathf.FloorToInt(count / m_dayInWeek)];
            m_dateCells[count].transform.localScale = Vector3.one;
        }
    }


    void GenerateMonthData()
    {
        DayWiseShortDataInfo[] l_dayWiseData = CalendarHandler.GetSpecificMonthData(new DateTime(2018, month, 01));
        for (int count = 0; count < m_maxDateCells; count++)
        {
            m_dateCells[count].AssignData(l_dayWiseData[count]);
        }
    }

   
}
