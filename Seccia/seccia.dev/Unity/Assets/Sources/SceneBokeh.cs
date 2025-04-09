using UnityEngine;
public struct Bokeh
{
public const int SIZE = 8;
public uint pos;
public uint posInDepth;
}
public class SceneBokeh : SceneEntity
{
public int m_sid;
public string m_name;
public Serial<bool> m_visible;
public static implicit operator bool(SceneBokeh inst) { return inst!=null; }
public SceneBokeh()
{
m_entity = ENTITY.BOKEH;
}
public void Reset()
{
m_parentName.Reset();
m_parent = m_scene.__536(ref m_parentName.cur);
m_local.Reset();
m_placement.Reset();
m_visible.Reset();
}
public void __46(JsonObj json)
{
json.__382("id", m_sid);
if ( m_parentName.modified )
json.__381("parent", m_parentName.cur);
if ( G.m_game.__291()==m_scene )
{
json.__382("x", (int)m_local.cur.x);
json.__382("y", (int)m_local.cur.y);
}
if ( m_visible.modified )
json.__385("visible", m_visible.cur);
}
public void __47(JsonObj json)
{
if ( json.__391("parent") )
{
m_parentName.Set(json.GetString("parent"));
m_parent = m_scene.__536(ref m_parentName.cur);
}
if ( json.__391("x") )
{
m_local.Set(new Vec2((float)json.GetInt("x"), (float)json.GetInt("y")));
m_local.modified = false;
}
if ( json.__391("visible") )
m_visible.Set(json.__401("visible"));
}
public override void __43()
{
if ( m_visible.cur==false )
return;
}
}
