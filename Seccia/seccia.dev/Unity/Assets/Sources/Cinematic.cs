using UnityEngine;
using System.Collections.Generic;
public class Cinematic
{
public int m_sid;
public string m_uid;
public CinematicDesc[] m_descs;
public List<CinematicText>[/*lang*/] m_subs;
public CINEMATICSKIP m_skip;
public Serial<Effect> m_effect;
public static implicit operator bool(Cinematic inst) { return inst!=null; }
public void Reset()
{
m_effect.Reset();
}
public CinematicText __37(float time, int lastSub = 0)
{
for ( int i=lastSub ; i<m_subs[G.m_game.m_optionLanguageText].Count ; i++ )
{
CinematicText sub = m_subs[G.m_game.m_optionLanguageText][i];
if ( time<sub.m_start )
break;
if ( time>=sub.m_start && time<sub.m_end )
return sub;
}
return null;
}
}
public class CinematicText
{
public int m_index;
public float m_start;
public float m_end;
public Term m_text;
public static implicit operator bool(CinematicText inst) { return inst!=null; }
}
public class CinematicDesc
{
public string m_video;
public string m_audio;
public int m_width;
public int m_height;
public int m_fps;
public int m_frameCount;
public static implicit operator bool(CinematicDesc inst) { return inst!=null; }
}
