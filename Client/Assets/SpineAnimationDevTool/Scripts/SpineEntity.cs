using EasyButtons;
using Spine.Unity;
using Spine.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class SpineEntity : MonoBehaviour
{
    #region[Field]
    [SerializeField] private SkeletonAnimation m_skelAnim;
    [Space(10), SerializeField] private SODataEntity m_data;
    private AnimComponent m_anim;
    private FsmComponent m_fsm;

    public SODataEntity Data => m_data;
    public SkeletonAnimation SkelAnim => m_skelAnim;
    public AnimComponent Anim => m_anim;
    #endregion

    #region[Cycle]
    private void Awake()
    {
        m_skelAnim ??= GetComponentInChildren<SkeletonAnimation>();
        if (m_skelAnim == null)
            SetSkelAnimation();

        m_anim = new AnimComponent(m_skelAnim);
        m_fsm = new FsmComponent(this);
    }

    private void Update()
    {
        try
        {
            m_fsm.ManualUpdate(Time.deltaTime);
            m_skelAnim.Update(Time.deltaTime);
        }
        catch { }
    }
    #endregion

    #region[Method]
    [Button]
    public void Reload()
    {
        if (Application.isPlaying)
            return;

        SetSkelAnimation();
    }

    private void SetSkelAnimation()
    {
        if (this.transform.childCount > 0)
            DestroyImmediate(this.transform.GetChild(0).gameObject);

        //Set model object.
        var modelObj = new GameObject("Model");
        modelObj.transform.SetParent(transform);
        modelObj.transform.localPosition = Vector3.zero;

        //Set skeleton animation component.
        m_skelAnim = modelObj.AddComponent<SkeletonAnimation>();
        m_skelAnim.UpdateTiming = UpdateTiming.ManualUpdate;
        if (m_data == null || m_data.SkelDataAsset == null)
            return;

        //Set skel data asset.
        m_skelAnim.skeletonDataAsset = m_data.SkelDataAsset;
        SpineEditorUtilities.ReloadSkeletonDataAssetAndComponent(m_skelAnim);
    }
    #endregion
}