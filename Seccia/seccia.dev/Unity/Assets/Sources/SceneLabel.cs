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
public bool m_door;
public bool m_skip;
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
m_parent = m_scene.__535(ref m_parentName.cur);
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
json.__381("id", m_sid);
json.__380("tag", m_tags[0]);
if ( m_parentName.modified )
json.__380("parent", m_parentName.cur);
if ( G.m_game.__291()==m_scene )
{
json.__381("x", (int)m_local.cur.x);
json.__381("y", (int)m_local.cur.y);
}
if ( m_title.m_sub.modified )
json.__381("title", m_title.m_sub.cur);
if ( m_text.m_sub.modified )
json.__381("text", m_text.m_sub.cur);
if ( m_usertext.modified )
json.__380("usertext", m_usertext.cur);
if ( m_visible.modified )
json.__384("visible", m_visible.cur);
if ( m_enabled.modified )
json.__384("enabled", m_enabled.cur);
if ( m_cheat.modified )
json.__384("cheat", m_cheat.cur);
}
public void __47(JsonObj json)
{
m_tags[0] = json.GetString("tag");
if ( json.__390("parent") )
{
m_parentName.Set(json.GetString("parent"));
m_parent = m_scene.__535(ref m_parentName.cur);
}
if ( json.__390("x") )
{
m_local.Set(new Vec2((float)json.GetInt("x"), (float)json.GetInt("y")));
m_local.modified = false;
}
if ( json.__390("title") )
m_title.m_sub.Set(json.GetInt("title"));
if ( json.__390("text") )
m_text.m_sub.Set(json.GetInt("text"));
if ( json.__390("usertext") )
m_usertext.Set(json.GetString("usertext"));
if ( json.__390("visible") )
m_visible.Set(json.__400("visible"));
if ( json.__390("enabled") )
m_enabled.Set(json.__400("enabled"));
if ( json.__390("cheat") )
m_cheat.Set(json.__400("cheat"));
}
public float __616()
{
if ( m_parent==null )
return m_local.cur.x + m_width*0.5f;
return __612().x + m_width*0.5f;
}
public string __617()
{
return m_usertext.cur.Length==0 ? m_text.Get() : m_usertext.cur;
}
public void __618()
{
m_bti.Clear();
string text = __617();
if ( text.Length>0 )
{
text = text.Replace("\\n", "\n");
Police font = G.m_game.__215();
m_textScale = 1.0f;
if ( m_size>0.0f )
m_textScale = m_size/font.__446(1.0f);
m_textScale *= m_scene.__478().__34();
G.__161(m_bti, text, font, m_rc.width, m_textScale);
}
}
public int __619()
{
return m_bti.__66();
}
public override void __601()
{
if ( m_parent==null )
{
m_rc.x = m_local.cur.x;
m_rc.y = m_local.cur.y;
}
else
{
Vec2 pos = __612();
m_rc.x = pos.x;
m_rc.y = pos.y;
}
m_rc.width = m_width;
m_rc.height = m_height;
m_scene.__549(ref m_rc, m_hud);
__618();
}
public override void __43()
{
if ( m_visible.cur==false )
return;
int count = __619();
if ( count==0 )
return;
Police font = G.m_game.__215();
bool leftToRight = font.__489();
float rowHeight = font.__446(m_textScale);
float lineSpacingHeight = font.__490(m_textScale);
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
if ( G.__101(m_align, (int)ALIGN.MIDDLE) )
y = m_rc.__438().y - height*0.5f;
else if ( G.__101(m_align, (int)ALIGN.BOTTOM) )
y = m_rc.__437() - height;
float x;
ALIGN halign = ALIGN.LEFT;
if ( G.__101(m_align, (int)ALIGN.JUSTIFY) )
halign = ALIGN.JUSTIFY;
else if ( G.__101(m_align, (int)ALIGN.CENTER) )
halign = ALIGN.CENTER;
else if ( G.__101(m_align, (int)ALIGN.RIGHT) )
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
x = leftToRight ? m_rc.__436()-m_bti.m_lineRects[i].width : m_rc.__436();
break;
default:
x = leftToRight ?  m_rc.__439()-m_bti.m_lineRects[i].width*0.5f : m_rc.__439()+m_bti.m_lineRects[i].width*0.5f;
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
font.__70(ref info);
#if UNITY_EDITOR
#endif
}
}
