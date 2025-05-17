using UnityEngine;
using System;
using System.IO;
public class Sprite
{
public Obj m_obj = null;
public string m_path = "";
public int m_offset = 0;
public int m_size = 0;
public int m_sizeHalf = 0;
public float m_width = 0.0f;
public float m_height = 0.0f;
public PACKGROUP m_packGroupType = PACKGROUP.OBJ;
public string m_packGroupScene = "";
public Rect m_rc;
public Texture2D m_texture = null;
public Material m_material = null;
public bool m_visible = true;
public static implicit operator bool(Sprite inst) { return inst!=null; }
public void __468(Asset asset, SHADER shaderDef = SHADER.TEXTURE32)
{
byte[] buffer = null;
if ( m_offset==0 )
{
if ( m_path.Length==0 )
return;
#if UNITY_STANDALONE_WIN
buffer = File.ReadAllBytes(m_path);
#endif
}
else
{
if ( G.m_game.__214() )
{
asset.__3(m_offset);
buffer = asset.__9(m_size);
}
else
{
asset.__3(m_offset+m_size);
buffer = asset.__9(m_sizeHalf);
}
}
G.Release(m_material);
m_material = null;
G.Release(m_texture);
m_texture = null;
bool monochrome = m_obj && m_obj.m_monochrome;
ANIMATION animation = m_obj==null ? ANIMATION.NONE : m_obj.m_animation;
m_texture = new Texture2D(4, 4, TextureFormat.RGB24, false);
m_texture.LoadImage(buffer);
if ( G.m_game.m_pixelPerfect )
m_texture.filterMode = FilterMode.Point;
else if ( monochrome )
m_texture.filterMode = FilterMode.Trilinear;
else
m_texture.filterMode = FilterMode.Bilinear;
m_texture.wrapMode = TextureWrapMode.Clamp;
SHADER shader = shaderDef;
if ( monochrome )
shader = SHADER.MONOCHROME;
else
{
switch ( animation )
{
case ANIMATION.FLAME:
shader = SHADER.FLAME;
break;
}
}
m_material = G.__165(shader, m_texture);
if ( monochrome )
{
m_material.SetFloat("_width", m_width-1.0f);
m_material.SetFloat("_height", m_height-1.0f);
}
else
{
switch ( animation )
{
case ANIMATION.FLAME:
m_material.SetFloat("_speed", m_obj.m_animationSpeed);
m_material.SetFloat("_scale", m_obj.m_animationScale);
break;
}
}
}
public void End()
{
G.Release(m_material);
m_material = null;
G.Release(m_texture);
m_texture = null;
}
public bool __988()
{
return m_texture;
}
public void __989()
{
if ( m_packGroupType==PACKGROUP.AIR || m_packGroupType==PACKGROUP.AIR_LOW )
{
if ( m_texture==null )
{
Asset asset = G.__95(G.m_pathGraphics);
if ( asset )
{
__468(asset);
asset.Close();
}
}
}
}
public void __682()
{
if ( m_packGroupType==PACKGROUP.AIR || m_packGroupType==PACKGROUP.AIR_LOW )
{
End();
}
}
public void Set(float x, float y)
{
m_rc.x = x;
m_rc.y = y;
}
public void Set(float x, float y, float w, float h)
{
m_rc.x = x;
m_rc.y = y;
m_rc.width = w;
m_rc.height = h;
}
public void Set(ref Rect rc)
{
m_rc.x = rc.x;
m_rc.y = rc.y;
m_rc.width = rc.width;
m_rc.height = rc.height;
}
public float __35()
{
return m_rc.x;
}
public float __36()
{
return m_rc.y;
}
}
public class LayerSprite
{
public bool m_isBack = false;
public float m_partWidth = 0;
public float m_partHeight = 0;
public int m_rowCount = 0;
public int m_colCount = 0;
public float m_width = 0.0f;
public float m_height = 0.0f;
public Sprite[] m_sprites;
public Mask m_mask;
public Sprite m_depth;
public BLEND m_blend = BLEND.DEFAULT;
public bool m_visible = true;
public bool m_tileX = false;
public bool m_tileY = false;
public float m_offsetX = 0.0f;
public float m_offsetY = 0.0f;
public float m_speedX = 0.0f;
public float m_speedY = 0.0f;
public float m_parallax = 0.0f;
public float m_minOpacity = 1.0f;
public float m_maxOpacity = 1.0f;
public float m_speedOpacity = 0.0f;
public Rect m_curViewRect = Rect.Zero;
public int m_fadeDir;
public float m_fadeTime;
public float m_fadeDuration;
public float m_opacity;
public float m_curOpacity;
public RoleBox m_roleBox;
public int m_roleBoxToken;
public static implicit operator bool(LayerSprite inst) { return inst!=null; }
public void Reset()
{
__990(m_visible);
m_fadeDir = 0;
m_opacity = 1.0f;
m_curOpacity = 1.0f;
m_roleBox = null;
m_roleBoxToken = 0;
}
public void __468(Asset asset)
{
for ( int i=0 ; i<m_sprites.Length ; i++ )
m_sprites[i].__468(asset);
if ( m_depth )
m_depth.__468(asset);
if ( m_mask )
m_mask.__468(asset);
}
public void End()
{
for ( int i=0 ; i<m_sprites.Length ; i++ )
m_sprites[i].End();
if ( m_depth )
m_depth.End();
if ( m_mask )
m_mask.End();
}
public int __66()
{
return m_sprites.Length;
}
public void __990(bool visible)
{
for ( int i=0 ; i<m_sprites.Length ; i++ )
m_sprites[i].m_visible = visible;
}
public bool __427()
{
if ( m_sprites.Length>0 )
return m_sprites[0].m_visible;
return false;
}
public bool __991(float xView, float yView)
{
if ( m_mask==null || m_mask.m_buffer==null )
return false;
if ( __427()==false || m_blend!=BLEND.DEFAULT || m_curOpacity!=1.0f )
return false;
if ( xView>m_curViewRect.__436() )
xView = m_curViewRect.x + Mathf.Repeat(xView-m_curViewRect.x, m_curViewRect.width);
int x = (int)((xView-m_curViewRect.x)/m_curViewRect.width * m_width);
int y = (int)((yView-m_curViewRect.y)/m_curViewRect.height * m_height);
int index = y*(int)m_width + x;
if ( index<0 )
return false;
int index8 = index/8;
if ( index8>=m_mask.m_buffer.Length )
return false;
byte pack = m_mask.m_buffer[index8];
if ( (pack & (0x01<<(7-index%8)))==0 )
return false;
return true;
}
public void Update()
{
if ( __427()==false )
return;
bool ended = false;
float fadeOpacity = m_opacity;
if ( m_fadeDir!=0 )
{
m_fadeTime += G.m_game.m_elapsed;
if ( m_fadeTime>=m_fadeDuration )
m_fadeTime = m_fadeDuration;
if ( m_fadeDir>0 )
{
fadeOpacity = m_fadeTime/m_fadeDuration;
if ( fadeOpacity>=1.0f )
{
m_fadeDir = 0;
ended = true;
}
}
else
{
fadeOpacity = 1.0f - m_fadeTime/m_fadeDuration;
if ( fadeOpacity<=0.0f )
{
m_fadeDir = 0;
__990(false);
ended = true;
}
}
fadeOpacity = G.__138(G.Clamp(fadeOpacity));
}
float animOpacity = 1.0f;
if ( m_minOpacity!=1.0f || m_maxOpacity!=1.0f )
animOpacity = G.__135((Mathf.Cos(G.m_game.m_time*m_speedOpacity)+1.0f)*0.5f, m_minOpacity, m_maxOpacity);
m_curOpacity = G.Clamp(fadeOpacity*animOpacity);
if ( ended && m_roleBox )
{
m_roleBox.__457(m_roleBoxToken);
m_roleBox = null;
m_roleBoxToken = 0;
}
}
}
