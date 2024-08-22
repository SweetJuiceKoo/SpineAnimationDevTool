using Darkside;
using EasyButtons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchFxEntity : MonoBehaviour
{
    #region[Field]
    [SerializeField] private SODataTouchFx m_data;
    private GameObject m_objParent;
    private SerializedDictionary<EFxState, ParticleSystem[]> m_fxDict = new();
    private EFxState m_currState;
    private EFxState m_prevState;

    public EFxState CurrState => m_currState;
    #endregion

    #region[Cycle]
    private void Start()
    {
        Reload();
    }

    private void Update()
    {
        //Follow mouse position.
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0);

        //Active effect by mouse state.
        if (Input.GetMouseButtonDown(0))
            m_currState = EFxState.PressDown;
        else if (Input.GetMouseButtonUp(0))
            m_currState = EFxState.PressUp;
        else if (Input.GetMouseButton(0))
            m_currState = EFxState.PressIdle;
        else
            m_currState = EFxState.Idle;

        if (m_currState != m_prevState)
        {
            switch (m_currState)
            {
                case EFxState.Idle:
                    break;
                case EFxState.PressIdle:
                    break;
                case EFxState.PressDown:
                    break;
                case EFxState.PressUp:
                    break;
            }

            ActiveFx(m_currState);
            m_prevState = m_currState;
        }
    }
    #endregion

    #region[Method]
    private void ActiveFx(EFxState state)
    {
        foreach (var pair in m_fxDict)
        {
            if (pair.Key == state)
                foreach (var fx in pair.Value)
                    fx.Play();
            else
                foreach (var fx in pair.Value)
                    fx.Stop();
        }
    }

    private void Reload()
    {
        DestroyImmediate(m_objParent);
        m_objParent = new GameObject("Parent");
        m_objParent.transform.SetParent(this.transform);
        
        m_fxDict.Clear();

        if (m_data == null)
            return;

        foreach (var pair in m_data.ObjFxDict)
        {
            if (pair.Value == null)
                continue;

            var objClone = Instantiate(pair.Value, m_objParent.transform);
            var fxArr = objClone.GetComponentsInChildren<ParticleSystem>(true);
            foreach (var fx in fxArr)
                fx.Stop();

            m_fxDict.TryAdd(pair.Key, fxArr);
        }
    }
    #endregion
}