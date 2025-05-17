using UnityEngine;
using System.Collections.Generic;
using System;
public class SceneCell
{
public int m_col;
public int m_row;
public SceneCellData m_data;
public static implicit operator bool(SceneCell inst) { return inst!=null; }
public float __35()
{
return G.__146(m_col);
}
public float __36()
{
return G.__146(m_row);
}
public string __392()
{
if ( m_data && m_data.m_name!=null )
return m_data.m_name;
return "";
}
public bool __590()
{
if ( m_data )
return m_data.m_event;
return false;
}
public bool __591()
{
if ( m_data )
return m_data.m_magnet;
return false;
}
public bool __592()
{
if ( m_data )
return m_data.m_walkable;
return true;
}
public int __593()
{
if ( m_data )
return m_data.m_flag;
return -1;
}
public float __594()
{
if ( m_data )
return m_data.m_speedFactorX;
return 1.0f;
}
public float __595()
{
if ( m_data )
return m_data.m_speedFactorY;
return 1.0f;
}
public float __596()
{
if ( m_data )
return m_data.m_scaleFactor;
return 1.0f;
}
public bool __597()
{
if ( m_data )
return m_data.m_walkAnim.Length>0;
return false;
}
public string __598()
{
if ( m_data )
return m_data.m_walkAnim;
return "";
}
public bool __599(int dir)
{
if ( m_data && m_data.m_walkAnimDirs!=null )
return m_data.m_walkAnimDirs[dir];
return true;
}
public SceneCellLink __600(LINK type)
{
if ( m_data==null || m_data.m_links==null )
return null;
return m_data.m_links[(int)type];
}
}
public class SceneCellData
{
public bool m_event;
public bool m_magnet;
public bool m_walkable;
public List<uint> m_bridges;
public string m_walkAnim = "";
public bool[] m_walkAnimDirs;
public string m_name = "";
public sbyte m_flag;
public float m_speedFactorX;
public float m_speedFactorY;
public float m_scaleFactor;
public SceneCellLink[] m_links;
public static implicit operator bool(SceneCellData inst) { return inst!=null; }
}
public class SceneCellLink
{
public float m_dist;
public string m_anim = "";
public sbyte m_dir = -1;
public static implicit operator bool(SceneCellLink inst) { return inst!=null; }
};
