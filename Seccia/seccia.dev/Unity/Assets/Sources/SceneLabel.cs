using System.Collections.Generic;
using UnityEngine;
public class SceneLabel : SceneEntity
{
public int m_sid;
public string[] m_tags;
public string m_name;
public float m_width;
public float m_height;
public float m_size;
public Color m_color;
public int m_align;
public Serial<bool> m_enabled;
public Term m_title;
public Term m_text;
public Serial<string> m_usertext;
public float m_textScale = 1.0f;
public Serial<bool> m_visible;
public DRAG m_drag;
public Serial<bool> m_cheat;
public Rect m_rc;
public BreakTextInfo m_bti = new BreakTextInfo();
public static implicit operator bool(SceneLabel inst) { return inst!=null; }
public SceneLabel()
{
m_entity = ENTITY.LABEL;
}
public void Reset()
{
m_parentName.Reset();
m_parent = m_scene.__536(ref m_parentName.cur);
m_local.Reset();
m_placement.Reset();
m_title.m_sub.Reset();
m_text.m_sub.Reset();
m_usertext.Reset();
m_visible.Reset();
m_enabled.Reset();
m_cheat.Reset();
}
public void __46(JsonObj json)
{
json.__382("id", m_sid);
json.__381("tag", m_tags[0]);
if ( m_parentName.modified )
json.__381("parent", m_parentName.cur);
if ( G.m_game.__291()==m_scene )
{
json.__382("x", (int)m_local.cur.x);
json.__382("y", (int)m_local.cur.y);
}
if ( m_title.m_sub.modified )
json.__382("title", m_title.m_sub.cur);
if ( m_text.m_sub.modified )
json.__382("text", m_text.m_sub.cur);
if ( m_usertext.modified )
json.__381("usertext", m_usertext.cur);
if ( m_visible.modified )
json.__385("visible", m_visible.cur);
if ( m_enabled.modified )
json.__385("enabled", m_enabled.cur);
if ( m_cheat.modified )
json.__385("cheat", m_cheat.cur);
}
public void __47(JsonObj json)
{
m_tags[0] = json.GetString("tag");
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
if ( json.__391("title") )
m_title.m_sub.Set(json.GetInt("title"));
if ( json.__391("text") )
m_text.m_sub.Set(json.GetInt("text"));
if ( json.__391("usertext") )
m_usertext.Set(json.GetString("usertext"));
if ( json.__391("visible") )
m_visible.Set(json.__401("visible"));
if ( json.__391("enabled") )
m_enabled.Set(json.__401("enabled"));
if ( json.__391("cheat") )
m_cheat.Set(json.__401("cheat"));
}
public bool __473()
{
return m_title.Get().Length>0;
}
public float __620()
{
if ( m_parent==null )
return m_local.cur.x + m_width*0.5f;
return __616().x + m_width*0.5f;
}
public string __621()
{
return m_usertext.cur.Length==0 ? m_text.Get() : m_usertext.cur;
}
public void __622()
{
m_bti.Clear();
string text = __621();
if ( text.Length>0 )
{
text = text.Replace("\\n", "\n");
Police font = G.m_game.__215();
m_textScale = 1.0f;
if ( m_size>0.0f )
m_textScale = m_size/font.__447(1.0f);
m_textScale *= m_scene.__479().__34();
G.__161(m_bti, text, font, m_rc.width, m_textScale);
}
}
public int __623()
{
return m_bti.__67();
}
public override void __605()
{
if ( m_parent==null )
{
m_rc.x = m_local.cur.x;
m_rc.y = m_local.cur.y;
}
else
{
Vec2 pos = __616();
m_rc.x = pos.x;
m_rc.y = pos.y;
}
m_rc.width = m_width;
m_rc.height = m_height;
m_scene.__552(ref m_rc, m_hud);
__622();
}
public override void __43()
{
if ( m_visible.cur==false )
return;
int count = __623();
if ( count==0 )
return;
Police font = G.m_game.__215();
bool leftToRight = font.__490();
float rowHeight = font.__447(m_textScale);
float lineSpacingHeight = font.__491(m_textScale);
float height = 0.0f;
for ( int i=0 ; i<count ; i++ )
{
if ( i>0 )
height += lineSpacingHeight;
if ( i>0 && m_bti.m_paraSizes[i-1].y==0.0f && m_bti.m_paraSizes[i].y==0.0f )
height += rowHeight;
else
height += m_bti.m_lineRects[i].height;
}
float y = m_rc.y;
if ( G.__102(m_align, (int)ALIGN.MIDDLE) )
y = m_rc.__439().y - height*0.5f;
else if ( G.__102(m_align, (int)ALIGN.BOTTOM) )
y = m_rc.__438() - height;
float x;
ALIGN halign = ALIGN.LEFT;
if ( G.__102(m_align, (int)ALIGN.JUSTIFY) )
halign = ALIGN.JUSTIFY;
else if ( G.__102(m_align, (int)ALIGN.CENTER) )
halign = ALIGN.CENTER;
else if ( G.__102(m_align, (int)ALIGN.RIGHT) )
halign = ALIGN.RIGHT;
string[] lines = new string[count];
Vec2[] positions = new Vec2[count];
for ( int i=0 ; i<count ; i++ )
{
switch ( halign )
{
case ALIGN.LEFT:
case ALIGN.JUSTIFY:
x = leftToRight ? m_rc.x : m_rc.x+m_bti.m_lineRects[i].width;
break;
case ALIGN.RIGHT:
x = leftToRight ? m_rc.__437()-m_bti.m_lineRects[i].width : m_rc.__437();
break;
default:
x = leftToRight ?  m_rc.__440()-m_bti.m_lineRects[i].width*0.5f : m_rc.__440()+m_bti.m_lineRects[i].width*0.5f;
break;
}
lines[i] = m_bti.m_texts[i];
positions[i] = new Vec2(x, y);
if ( i>0 )
y += lineSpacingHeight;
if ( i>0 && m_bti.m_paraSizes[i-1].y==0.0f && m_bti.m_paraSizes[i].y==0.0f )
y += rowHeight;
else
y += m_bti.m_lineRects[i].height;
}
FontDrawInfo info = new FontDrawInfo(lines, positions, ref m_color);
info.scale = m_textScale;
if ( halign==ALIGN.JUSTIFY )
{
info.justify = true;
info.maxRowWidth = m_bti.m_maxRowWidth;
info.lineRects = m_bti.m_lineRects;
}
font.__71(ref info);
#if UNITY_EDITOR
#endif
}
}
