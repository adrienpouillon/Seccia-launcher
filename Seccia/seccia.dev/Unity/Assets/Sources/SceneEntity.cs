using System.Collections.Generic;
using UnityEngine;
abstract public class SceneEntity
{
public ENTITY m_entity;
public Scene m_scene;
public Serial<Vec2> m_local;
public Serial<string> m_parentName;
public SceneEntity m_parent;
public Serial<PLACEMENT> m_placement;
public bool m_hud;
public static implicit operator bool(SceneEntity inst) { return inst!=null; }
public bool __606()
{
return m_entity==ENTITY.OBJ;
}
public bool __607()
{
return m_entity==ENTITY.LABEL;
}
public bool __608()
{
return m_entity==ENTITY.BOKEH;
}
public bool __609()
{
return m_entity==ENTITY.STILL;
}
public bool __610()
{
return m_entity==ENTITY.CURSOR;
}
public float __611()
{
return m_local.cur.x;
}
public float __612()
{
return m_local.cur.y;
}
public void __613(ref Vec2 world, ref int counter)
{
if ( m_parent==null )
{
world = m_local.cur;
return;
}
if ( counter==32 )
{
world = Vec2.Zero;
return;
}
counter++;
m_parent.__613(ref world, ref counter);
world.x += m_local.cur.x;
world.y += m_local.cur.y;
}
public void __614(float x, float y)
{
if ( m_parent==null )
{
m_local.cur.x = x;
m_local.cur.y = y;
}
else
{
m_local.cur.x = 0.0f;
m_local.cur.y = 0.0f;
Vec2 pos = __616();
m_local.cur.x = x - pos.x;
m_local.cur.y = y - pos.y;
}
}
public void __615(float dx, float dy)
{
m_local.cur.x += dx;
m_local.cur.y += dy;
}
public Vec2 __616()
{
if ( m_parent==null )
return m_local.cur;
Vec2 pos = Vec2.Zero;
int counter = 0;
__613(ref pos, ref counter);
return pos;
}
public void __617(float x)
{
if ( m_parent==null )
{
m_local.cur.x = x;
}
else
{
m_local.cur.x = 0.0f;
m_local.cur.x = x - __35();
}
}
public void __618(float y)
{
if ( m_parent==null )
{
m_local.cur.y = y;
}
else
{
m_local.cur.y = 0.0f;
m_local.cur.y = y - __36();
}
}
public float __35()
{
if ( m_parent==null )
return m_local.cur.x;
return __616().x;
}
public float __36()
{
if ( m_parent==null )
return m_local.cur.y;
return __616().y;
}
public virtual float __619()
{
return __36();
}
public virtual void __605()
{
}
public virtual void __43()
{
}
}
