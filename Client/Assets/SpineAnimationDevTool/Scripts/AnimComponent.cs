using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimComponent
{
    #region[Field]
    private SkeletonAnimation m_skelAnim;
    private List<Spine.Bone> m_boneList = new();
    #endregion

    #region[Cycle]
    public AnimComponent(SkeletonAnimation skelAnim)
    {
        m_skelAnim = skelAnim;
        m_skelAnim.UpdateLocal += SkeletonAnimation_UpdateLocal;
    }
    #endregion

    #region[Method]
    public void SetAnimation(string _animName, int _trackIndex, bool _loop, float _mixDuration, float _timeScale, Spine.MixBlend _blendType, float _alpha)
    {
        var trackEntry = m_skelAnim.state.SetAnimation(_trackIndex, _animName, _loop);
        trackEntry.MixDuration = _mixDuration;
        trackEntry.TimeScale = _timeScale;
        trackEntry.MixBlend = _blendType;
        trackEntry.Alpha = _alpha;
    }

    public void SetBone(List<string> _boneNameList)
    {
        m_boneList.Clear();
        foreach (var boneName in _boneNameList)
            SetBone(boneName);
    }

    private void SetBone(string _boneName)
    {
        var bone = m_skelAnim.skeleton.FindBone(_boneName);
        if (bone != null)
            m_boneList.Add(bone);
    }

    public void SkeletonAnimation_UpdateLocal(ISkeletonAnimation _animation)
    {
        foreach (var bone in m_boneList)
            bone.SetPositionSkeletonSpace(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
    #endregion
}