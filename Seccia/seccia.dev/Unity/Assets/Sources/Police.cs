using UnityEngine;
using System.Collections.Generic;
public class Police
{
public const float HEIGHT = 54.0f;
public bool m_isSystem = false;
public bool m_cjk = false;
public int m_textureOffset = 0;
public float m_textureSize;
public int m_textureCount;
public float m_charY;
public float m_spaceWidth;
public float m_spacing;
public float m_lineSpacing;
public int m_count;
public Dictionary<char, FontChar> m_table = new Dictionary<char, FontChar>();
public FontChar m_fontCharError = null;
public FontTexture[] m_textures;
public float m_scale = 1.0f;
public static implicit operator bool(Police inst) { return inst!=null; }
public void __488(float scale)
{
m_isSystem = true;
TextAsset file = (TextAsset)Resources.Load("Texts/font", typeof(TextAsset));
int offset = 0;
m_cjk = G.__10(file.bytes, ref offset);
m_textureSize = (float)G.__14(file.bytes, ref offset);
m_textureCount = G.__12(file.bytes, ref offset);
m_charY = (float)G.__12(file.bytes, ref offset);
float charHeight = (float)G.__12(file.bytes, ref offset);
float convert = HEIGHT / charHeight;
m_spaceWidth = G.__12(file.bytes, ref offset) * convert;
m_spacing = (G.__12(file.bytes, ref offset) - 128.0f) * convert;
m_lineSpacing = (G.__12(file.bytes, ref offset) - 128.0f) * convert;
m_count = G.__14(file.bytes, ref offset);
for ( int i=0 ; i<m_count ; i++ )
{
FontChar fc = new FontChar();
char code = (char)G.__14(file.bytes, ref offset);
if ( code=='?' )
m_fontCharError = fc;
fc.tex = G.__12(file.bytes, ref offset);
fc.x = (float)G.__14(file.bytes, ref offset);
fc.y = (float)G.__14(file.bytes, ref offset);
fc.w = (float)G.__12(file.bytes, ref offset);
fc.w2 = fc.w * convert;
fc.h = charHeight;
m_table.Add(code, fc);
}
m_textures = new FontTexture[m_textureCount];
m_textures[0] = new FontTexture();
m_textures[0].texture = (Texture2D)Resources.Load("Textures/font", typeof(Texture2D));
m_textures[0].material = G.__165(SHADER.TEXT);
m_textures[0].material.mainTexture = m_textures[0].texture;
m_scale = scale;
}
public void __64(Asset asset, float scale)
{
m_textureOffset = asset.__15();
asset.__13();
m_cjk = asset.__10();
m_textureSize = (float)asset.__13();
m_textureCount = asset.__12();
m_charY = (float)asset.__12();
float charHeight = (float)asset.__12();
float convert = HEIGHT / charHeight;
m_spaceWidth = asset.__12() * convert;
m_spacing = (asset.__12() - 128.0f) * convert;
m_lineSpacing = (asset.__12() - 128.0f) * convert;
m_count = asset.__13();
for ( int i=0 ; i<m_count ; i++ )
{
FontChar fc = new FontChar();
char code = (char)asset.__13();
if ( code=='?' )
m_fontCharError = fc;
fc.tex = asset.__12();
fc.x = (float)asset.__13();
fc.y = (float)asset.__13();
fc.w = (float)asset.__12();
fc.w2 = fc.w * convert;
fc.h = charHeight;
m_table.Add(code, fc);
}
m_textures = new FontTexture[m_textureCount];
for ( int i=0 ; i<m_textureCount ; i++ )
{
m_textures[i] = new FontTexture();
m_textures[i].size = asset.__15();
m_textures[i].material = G.__165(SHADER.TEXT);
}
m_scale = scale;
}
public void __65()
{
for ( int i=0 ; i<m_textures.Length ; i++ )
m_textures[i].__494(m_isSystem);
m_fontCharError = null;
m_table.Clear();
m_count = 0;
m_charY = 0.0f;
m_spaceWidth = 0.0f;
m_spacing = 0.0f;
m_lineSpacing = 0.0f;
m_textureSize = 0.0f;
m_textureCount = 0;
m_textureOffset = 0;
}
public void __468(Asset assetGraphics)
{
assetGraphics.__3(m_textureOffset);
for ( int i=0 ; i<m_textureCount ; i++ )
{
byte[] buffer = assetGraphics.__9(m_textures[i].size);
m_textures[i].texture = new Texture2D(4, 4, TextureFormat.RGB24, false);
m_textures[i].texture.LoadImage(buffer);
m_textures[i].texture.filterMode = FilterMode.Trilinear;
m_textures[i].texture.wrapMode = TextureWrapMode.Clamp;
m_textures[i].material.mainTexture = m_textures[i].texture;
}
}
public void End()
{
for ( int i=0 ; i<m_textureCount ; i++ )
{
if ( m_textures[i].texture==null )
continue;
m_textures[i].material.mainTexture = null;
if ( m_isSystem==false )
G.Release(m_textures[i].texture);
m_textures[i].texture = null;
}
}
public bool __489()
{
if ( m_isSystem )
return true;
return G.m_game.__206().m_leftToRight;
}
public float __34()
{
return m_scale;
}
public float __446()
{
return HEIGHT * m_scale;
}
public float __446(float scale)
{
return HEIGHT * scale;
}
public float __490()
{
return m_lineSpacing * m_scale;
}
public float __490(float scale)
{
return m_lineSpacing * scale;
}
public Vec2 __491(string text, float scale = -1.0f)
{
if ( text.Length==0 )
return Vec2.Zero;
if ( scale==-1.0f )
scale = m_scale;
float width = 0.0f;
for ( int i=0 ; i<text.Length ; i++ )
{
if ( i>0 )
width += m_spacing;
if ( text[i]==' ' || text[i]==1 )
width += m_spaceWidth;
else
{
char c = text[i];
FontChar fc;
if ( m_table.TryGetValue(c, out fc) )
width += fc.w2;
else
width += m_fontCharError.w2;
}
}
return new Vec2(width*scale, __446(scale));
}
public bool __70(string text, float x, float y, ref Color color, float scale = -1.0f, bool drawBack = false)
{
FontDrawInfo info = new FontDrawInfo(ref text, x, y, ref color);
info.scale = scale;
info.background = drawBack;
return __70(ref info);
}
public bool __70(ref string text, float x, float y, ref Color color, float scale = -1.0f)
{
FontDrawInfo info = new FontDrawInfo(ref text, x, y, ref color);
info.scale = scale;
return __70(ref info);
}
public bool __70(ref FontDrawInfo info)
{
bool vibration = G.m_game && G.m_game.m_vibration;
bool finished = false;
if ( info.maxCharCount==0 )
return finished;
float scale = info.scale;
if ( scale==-1.0f )
scale = m_scale;
float charHeight = __446(scale);
float spaceWidth = m_spaceWidth * scale;
float spaceWidthExtra = 0.0f;
float spacing = m_spacing * scale;
for ( int i=0 ; i<m_textures.Length ; i++ )
{
FontTexture tex = m_textures[i];
tex.charCount = 0;
tex.index = 0;
}
int charCount = 0;
finished = true;
for ( int iLine=0 ; iLine<info.lines.Length ; iLine++ )
{
for ( int i=0 ; i<info.lines[iLine].Length ; i++ )
{
if ( info.lines[iLine][i]==' ' || info.lines[iLine][i]==1 )
continue;
if ( charCount==info.maxCharCount )
{
finished = false;
iLine = info.lines.Length;
break;
}
if ( m_textures.Length>1 )
{
FontChar fc;
if ( m_table.TryGetValue(info.lines[iLine][i], out fc)==false )
fc = m_fontCharError;
m_textures[fc.tex].charCount++;
}
else
m_textures[0].charCount++;
charCount++;
}
}
if ( charCount==0 )
return finished;
for ( int i=0 ; i<m_textures.Length ; i++ )
{
FontTexture tex = m_textures[i];
if ( tex.charCount>0 )
{
tex.vertices = new Vector3[tex.charCount*4];
tex.uvs = new Vector2[tex.charCount*4];
tex.triangles = new int[tex.charCount*6];
}
}
float minLeft = 1000000.0f;
float maxRight = 0.0f;
if ( __489() )
{
int iChar = 0;
for ( int iLine=0 ; iLine<info.lines.Length ; iLine++ )
{
if ( info.positions[iLine].x<minLeft )
minLeft = info.positions[iLine].x;
float right = info.positions[iLine].x;
float offsetX = info.positions[iLine].x;
float offsetY = info.positions[iLine].y;
if ( info.justify )
{
spaceWidthExtra = 0.0f;
if ( iLine<info.lines.Length-1 )
{
int spaceCount = 0;
for ( int i=0 ; i<info.lines[iLine].Length ; i++ )
{
if ( info.lines[iLine][i]==' ' || info.lines[iLine][i]==1 )
spaceCount++;
}
if ( spaceCount>0 )
spaceWidthExtra = (info.maxRowWidth - info.lineRects[iLine].width)*scale/spaceCount;
}
}
for ( int i=0 ; i<info.lines[iLine].Length ; i++ )
{
if ( info.lines[iLine][i]==' ' || info.lines[iLine][i]==1 )
{
right += spaceWidth + spaceWidthExtra;
offsetX += spaceWidth + spaceWidthExtra;
if ( i>0 )
{
right += spacing;
offsetX += spacing;
}
}
else
{
char charKey = info.lines[iLine][i];
FontChar fc;
if ( m_table.TryGetValue(charKey, out fc)==false )
fc = m_fontCharError;
float fcw = fc.w2*scale;
if ( iChar<charCount )
{
float vibrationX = 0.0f;
float vibrationY = 0.0f;
if ( vibration )
{
float force = 0.05f;
float speed = 2.0f;
float value = Mathf.Cos(G.m_game.m_time*speed+iChar) * 0.5f * force;
vibrationX = value * fcw;
vibrationY = value * charHeight;
}
FontTexture tex = m_textures[fc.tex];
int iV = tex.index*4;
int iT = tex.index*6;
tex.vertices[iV+0].z = 0.0f;
tex.vertices[iV+1].z = 0.0f;
tex.vertices[iV+2].z = 0.0f;
tex.vertices[iV+3].z = 0.0f;
tex.vertices[iV+0].x = offsetX + vibrationX;
tex.vertices[iV+0].y = offsetY + vibrationY;
tex.vertices[iV+1].x = offsetX + fcw + vibrationX;
tex.vertices[iV+1].y = offsetY + vibrationY;
tex.vertices[iV+2].x = offsetX + fcw + vibrationX;
tex.vertices[iV+2].y = (offsetY + charHeight) + vibrationY;
tex.vertices[iV+3].x = offsetX + vibrationX;
tex.vertices[iV+3].y = (offsetY + charHeight) + vibrationY;
tex.uvs[iV+0].x = fc.x/m_textureSize;
tex.uvs[iV+0].y = 1.0f - (fc.y/m_textureSize);
tex.uvs[iV+1].x = fc.__436()/m_textureSize;
tex.uvs[iV+1].y = 1.0f - (fc.y/m_textureSize);
tex.uvs[iV+2].x = fc.__436()/m_textureSize;
tex.uvs[iV+2].y = 1.0f - (fc.__437()/m_textureSize);
tex.uvs[iV+3].x = fc.x/m_textureSize;
tex.uvs[iV+3].y = 1.0f - (fc.__437()/m_textureSize);
tex.triangles[iT+0] = iV + 0;
tex.triangles[iT+1] = iV + 1;
tex.triangles[iT+2] = iV + 2;
tex.triangles[iT+3] = iV + 2;
tex.triangles[iT+4] = iV + 3;
tex.triangles[iT+5] = iV + 0;
tex.index++;
}
right += fcw;
offsetX += fcw;
if ( i>0 )
{
right += spacing;
offsetX += spacing;
}
iChar++;
}
}
if ( right>maxRight )
maxRight = right;
}
}
else
{
int iChar = 0;
for ( int iLine=0 ; iLine<info.lines.Length ; iLine++ )
{
if ( info.positions[iLine].x>maxRight )
maxRight = info.positions[iLine].x;
float left = info.positions[iLine].x;
float offsetX = info.positions[iLine].x;
float offsetY = info.positions[iLine].y;
if ( info.justify )
{
spaceWidthExtra = 0.0f;
if ( iLine<info.lines.Length-1 )
{
int spaceCount = 0;
for ( int i=0 ; i<info.lines[iLine].Length ; i++ )
{
if ( info.lines[iLine][i]==' ' || info.lines[iLine][i]==1 )
spaceCount++;
}
if ( spaceCount>0 )
spaceWidthExtra = (info.maxRowWidth - info.lineRects[iLine].width)*scale/spaceCount;
}
}
for ( int i=0 ; i<info.lines[iLine].Length ; i++ )
{
if ( info.lines[iLine][i]==' ' || info.lines[iLine][i]==1 )
{
left -= spaceWidth + spaceWidthExtra;
offsetX -= spaceWidth + spaceWidthExtra;
}
else
{
char charKey = info.lines[iLine][i];
FontChar fc;
if ( m_table.TryGetValue(charKey, out fc)==false )
fc = m_fontCharError;
float fcw = fc.w2*scale;
if ( iChar<charCount )
{
float vibrationX = 0.0f;
float vibrationY = 0.0f;
if ( vibration )
{
float force = 0.05f;
float speed = 2.0f;
float value = Mathf.Cos(G.m_game.m_time*speed+iChar) * 0.5f * force;
vibrationX = value * fcw;
vibrationY = value * charHeight;
}
FontTexture tex = m_textures[fc.tex];
int iV = tex.index*4;
int iT = tex.index*6;
tex.vertices[iV+0].z = 0.0f;
tex.vertices[iV+1].z = 0.0f;
tex.vertices[iV+2].z = 0.0f;
tex.vertices[iV+3].z = 0.0f;
tex.vertices[iV+0].x = offsetX - fcw + vibrationX;
tex.vertices[iV+0].y = offsetY + vibrationY;
tex.vertices[iV+1].x = offsetX + vibrationX;
tex.vertices[iV+1].y = offsetY + vibrationY;
tex.vertices[iV+2].x = offsetX + vibrationX;
tex.vertices[iV+2].y = (offsetY + charHeight) + vibrationY;
tex.vertices[iV+3].x = offsetX - fcw + vibrationX;
tex.vertices[iV+3].y = (offsetY + charHeight) + vibrationY;
tex.uvs[iV+0].x = fc.x/m_textureSize;
tex.uvs[iV+0].y = 1.0f - (fc.y/m_textureSize);
tex.uvs[iV+1].x = fc.__436()/m_textureSize;
tex.uvs[iV+1].y = 1.0f - (fc.y/m_textureSize);
tex.uvs[iV+2].x = fc.__436()/m_textureSize;
tex.uvs[iV+2].y = 1.0f - (fc.__437()/m_textureSize);
tex.uvs[iV+3].x = fc.x/m_textureSize;
tex.uvs[iV+3].y = 1.0f - (fc.__437()/m_textureSize);
tex.triangles[iT+0] = iV + 0;
tex.triangles[iT+1] = iV + 1;
tex.triangles[iT+2] = iV + 2;
tex.triangles[iT+3] = iV + 2;
tex.triangles[iT+4] = iV + 3;
tex.triangles[iT+5] = iV + 0;
tex.index++;
}
left -= fcw;
left -= spacing;
offsetX -= fcw;
offsetX -= spacing;
iChar++;
}
}
if ( left<minLeft )
minLeft = left;
}
}
for ( int i=0 ; i<m_textures.Length ; i++ )
{
FontTexture tex = m_textures[i];
if ( tex.charCount>0 )
tex.Update();
}
if ( info.background && G.m_game && G.m_game.m_colorTextBack.a!=0.0f )
{
float x = (int)(minLeft-spaceWidth);
float y = info.positions[0].y;
float y2 = info.positions[info.positions.Length-1].y + (int)__446(scale);
Rect rc = new Rect(x, y, maxRight-x+spaceWidth, y2-y);
G.m_materialBrush.color = G.m_game.m_colorTextBack;
G.m_graphics.__354(G.m_materialBrush, ref rc);
}
for ( int i=0 ; i<m_textures.Length ; i++ )
{
FontTexture tex = m_textures[i];
if ( tex.charCount>0 )
{
tex.material.color = new Color(info.color.r, info.color.g, info.color.b, 1.0f);
if ( tex.mesh )
{
G.m_graphics.__344();
G.m_graphics.__364(tex.mesh, tex.material);
}
}
}
return finished;
}
public void __492(string text, ref Color color, float maxRowWidth, float vertMargin = G.SUBTITLE_MARGIN)
{
BreakTextInfo info = new BreakTextInfo();
G.__161(info, text, this, maxRowWidth);
float y = G.m_rcViewUI.__437() - vertMargin;
for ( int i=0 ; i<info.__66() ; i++ )
y -= info.m_lineRects[i].height;
bool leftToRight = __489();
for ( int i=0 ; i<info.__66() ; i++ )
{
float x= G.m_rcViewUI.__439();
if ( leftToRight )
x -= info.m_lineRects[i].width * 0.5f;
else
x += info.m_lineRects[i].width * 0.5f;
__70(info.m_texts[i], x, y, ref color, -1.0f, true);
y += info.m_lineRects[i].height;
}
}
public void __493(string text, Color color)
{
LayoutCtrl ctrl = G.m_game.m_layout.Get(LAYOUT_CTRL.LEGEND);
BreakTextInfo info = new BreakTextInfo();
G.__161(info, text, this, ctrl.m_rcView.width);
float y;
if ( G.__101(ctrl.m_align, (int)ALIGN.BOTTOM) )
{
y = ctrl.m_rcView.__437();
for ( int i=0 ; i<info.__66() ; i++ )
y -= info.m_lineRects[i].height;
}
else if ( G.__101(ctrl.m_align, (int)ALIGN.MIDDLE) )
{
float height = 0.0f;
for ( int i=0 ; i<info.__66() ; i++ )
height += info.m_lineRects[i].height;
y = ctrl.m_rcView.__440() - height*0.5f;
}
else
{
y = ctrl.m_rcView.y;
}
bool leftToRight = __489();
for ( int i=0 ; i<info.__66() ; i++ )
{
float x;
if ( G.__101(ctrl.m_align, (int)ALIGN.RIGHT) )
{
if ( leftToRight )
x = ctrl.m_rcView.__436() - info.m_lineRects[i].width;
else
x = ctrl.m_rcView.__436();
}
else if ( G.__101(ctrl.m_align, (int)ALIGN.CENTER) )
{
if ( leftToRight )
x = ctrl.m_rcView.__439() - info.m_lineRects[i].width * 0.5f;
else
x = ctrl.m_rcView.__439() + info.m_lineRects[i].width * 0.5f;
}
else
{
if ( leftToRight )
x = ctrl.m_rcView.x;
else
x = ctrl.m_rcView.x + info.m_lineRects[i].width;
}
__70(info.m_texts[i], x, y, ref color, -1.0f, true);
y += info.m_lineRects[i].height;
}
}
}
public class FontChar
{
public int tex;
public float x;
public float y;
public float w;
public float w2;
public float h;
public float __436()
{
return x + w;
}
public float __437()
{
return y + h;
}
}
public class FontTexture
{
public int size;
public Texture2D texture;
public Material material;
public Mesh mesh;
public Vector3[] vertices;
public Vector2[] uvs;
public int[] triangles;
public int charCount;
public int index;
public void Update()
{
G.__178(ref mesh);
mesh.vertices = vertices;
mesh.uv = uvs;
mesh.triangles = triangles;
}
public void __494(bool sys)
{
G.Release(mesh);
mesh = null;
vertices = null;
uvs = null;
triangles = null;
G.Release(material);
material = null;
if ( sys==false )
G.Release(texture);
texture = null;
}
}
public struct FontDrawInfo
{
public Color color;
public string[] lines;
public Vec2[] positions;
public bool background;
public float scale;
public int maxCharCount;
public bool justify;
public float maxRowWidth;
public List<Rect> lineRects;
public FontDrawInfo(ref string inLine, float inX, float inY, ref Color inColor)
{
color = inColor;
lines = new string[1];
lines[0] = inLine;
positions = new Vec2[1];
positions[0].x = inX;
positions[0].y = inY;
background = false;
scale = -1.0f;
maxCharCount = -1;
justify = false;
maxRowWidth = 0.0f;
lineRects = null;
}
public FontDrawInfo(string[] inLines, Vec2[] inPositions, ref Color inColor)
{
color = inColor;
lines = inLines;
positions = inPositions;
background = false;
scale = -1.0f;
maxCharCount = -1;
justify = false;
maxRowWidth = 0.0f;
lineRects = null;
}
}
