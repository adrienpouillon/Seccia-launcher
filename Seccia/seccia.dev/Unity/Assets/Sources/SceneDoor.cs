using UnityEngine;
public class SceneDoor
{
public int m_sid;
public Scene m_scene;
public string[] m_tags;
public string m_name;
public float m_x;
public float m_y;
public float m_width;
public float m_height;
public Serial<bool> m_visible;
public Term m_title;
public Serial<bool> m_cheat;
public Rect m_rc;
public static implicit operator bool(SceneDoor inst) { return inst!=null; }
public void Reset()
{
m_title.m_sub.Reset();
m_visible.Reset();
m_cheat.Reset();
}
public bool __473()
{
return m_title.Get().Length>0;
}
public void __605()
{
m_rc.x = m_x;
m_rc.y = m_y;
m_rc.width = m_width;
m_rc.height = m_height;
m_scene.__552(ref m_rc);
}
}
