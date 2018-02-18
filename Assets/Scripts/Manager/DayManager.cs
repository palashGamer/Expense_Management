using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DayZone))]
public class DayManager : MonoBehaviour {

    DayZone dayZone;
    DayZone m_dayZone
    {
        get
        {
            if (dayZone == null)
                dayZone = GetComponent<DayZone>();
            return dayZone;
        }
    }

    public void AssignDay(int index)
    {
        switch (index)
        {
            case 0:
                m_dayZone.m_DayText.text = "MON";
                m_dayZone.m_DayText.color = m_dayZone.m_weekdayColor;
                break;
            case 1:
                m_dayZone.m_DayText.text = "TUE";
                m_dayZone.m_DayText.color = m_dayZone.m_weekdayColor;
                break;
            case 2:
                m_dayZone.m_DayText.text = "WED";
                m_dayZone.m_DayText.color = m_dayZone.m_weekdayColor;
                break;
            case 3:
                m_dayZone.m_DayText.text = "THU";
                m_dayZone.m_DayText.color = m_dayZone.m_weekdayColor;
                break;
            case 4:
                m_dayZone.m_DayText.text = "FRI";
                m_dayZone.m_DayText.color = m_dayZone.m_weekdayColor;
                break;
            case 5:
                m_dayZone.m_DayText.text = "SAT";
                m_dayZone.m_DayText.color = m_dayZone.m_weekendColor;
                break;
            case 6:
                m_dayZone.m_DayText.text = "SUN";
                m_dayZone.m_DayText.color = m_dayZone.m_weekendColor;
                break;
        }
    }
}
