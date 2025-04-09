using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
public class SceneStill : SceneEntity
{
public int m_sid;
public string[] m_tags;
public string m_name;
public Rect m_rcTrg;
public Rect m_rcSrc;
public bool m_rotated;
public Sprite m_sprite;
public float m_z;
public Serial<bool> m_visible;
public static implicit operator bool(SceneStill inst) { return inst!=null; }
public SceneStill()
{
m_entity = ENTITY.STILL;
}
public void Reset(bool fromScript = false)
{
if ( fromScript==false )
{
End();
}
m_placement.Reset();
m_visible.Reset();
}
public void __469(Asset asset)
{
}
public void End()
{
}
public override float __619()
{
return m_z;
}
public override void __43()
{
if ( m_visible.cur==false )
return;
G.m_graphics.__357(m_sprite, ref m_rcSrc, ref m_rcTrg, m_rotated);
}
}
