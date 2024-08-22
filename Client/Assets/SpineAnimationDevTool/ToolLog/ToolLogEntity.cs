using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolLogEntity : MonoBehaviour
{
    #region[Field]
    [SerializeField] private Text m_text;
    private SpineEntity m_spineEnt;
    private TouchFxEntity m_touchEnt;
    #endregion

    #region[Cycle]
    private void Awake()
    {
        if (m_spineEnt == null)
            m_spineEnt = FindObjectOfType<SpineEntity>();

        if (m_touchEnt == null)
            m_touchEnt = FindObjectOfType<TouchFxEntity>();
    }

    private void Update()
    {
        string str = null;
        if (m_spineEnt != null)
            str = $"animation : {m_spineEnt.SkelAnim.AnimationName}";
        
        if (m_touchEnt != null)
            str = $"{str}\ntouch state : {m_touchEnt.CurrState}";

        m_text.text = str;
    }
    #endregion
}