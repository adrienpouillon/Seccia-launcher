using System;
using UnityEngine;
public class GraphicsDevice
{
public enum MESH
{
DEFAULT,
TEMP,
MAGNIFY,
}
public Mesh m_mesh;
public Mesh m_meshTemp;
public Mesh m_meshMagnify;
public Vector3[] m_vertices;
public Vector3[] m_tempVertices;
public Vector3[] m_magnifyVertices;
public Vector2[] m_uvs;
public Vector2[] m_tempUvs;
public Vector2[] m_magnifyUvs;
public int[] m_triangles;
public int[] m_magnifyTriangles;
public MESH m_useMesh = MESH.DEFAULT;
public bool m_useTempVertices;
public Matrix4x4 m_matrix;
public static implicit operator bool(GraphicsDevice inst) { return inst!=null; }
public GraphicsDevice()
{
m_matrix = Matrix4x4.identity;
m_vertices = new Vector3[4];
m_vertices[0].x = 0.0f;
m_vertices[0].y = 0.0f;
m_vertices[0].z = 0.0f;
m_vertices[1].x = 1.0f;
m_vertices[1].y = 0.0f;
m_vertices[1].z = 0.0f;
m_vertices[2].x = 1.0f;
m_vertices[2].y = -1.0f;
m_vertices[2].z = 0.0f;
m_vertices[3].x = 0.0f;
m_vertices[3].y = -1.0f;
m_vertices[3].z = 0.0f;
m_uvs = new Vector2[4];
m_uvs[0].x = 0.0f;
m_uvs[0].y = 1.0f;
m_uvs[1].x = 1.0f;
m_uvs[1].y = 1.0f;
m_uvs[2].x = 1.0f;
m_uvs[2].y = 0.0f;
m_uvs[3].x = 0.0f;
m_uvs[3].y = 0.0f;
m_triangles = new int[6];
m_triangles[0] = 0;
m_triangles[1] = 1;
m_triangles[2] = 2;
m_triangles[3] = 2;
m_triangles[4] = 3;
m_triangles[5] = 0;
m_mesh = new Mesh();
m_mesh.vertices = m_vertices;
m_mesh.uv = m_uvs;
m_mesh.triangles = m_triangles;
m_tempVertices = new Vector3[4];
m_tempVertices[0].x = 0.0f;
m_tempVertices[0].y = 0.0f;
m_tempVertices[0].z = 0.0f;
m_tempVertices[1].x = 0.0f;
m_tempVertices[1].y = 0.0f;
m_tempVertices[1].z = 0.0f;
m_tempVertices[2].x = 0.0f;
m_tempVertices[2].y = 0.0f;
m_tempVertices[2].z = 0.0f;
m_tempVertices[3].x = 0.0f;
m_tempVertices[3].y = 0.0f;
m_tempVertices[3].z = 0.0f;
m_tempUvs = new Vector2[4];
m_tempUvs[0].x = 0.0f;
m_tempUvs[0].y = 1.0f;
m_tempUvs[1].x = 1.0f;
m_tempUvs[1].y = 1.0f;
m_tempUvs[2].x = 1.0f;
m_tempUvs[2].y = 0.0f;
m_tempUvs[3].x = 0.0f;
m_tempUvs[3].y = 0.0f;
m_meshTemp = new Mesh();
m_meshTemp.MarkDynamic();
m_meshTemp.vertices = m_vertices;
m_meshTemp.uv = m_tempUvs;
m_meshTemp.triangles = m_triangles;
m_magnifyVertices = new Vector3[25];
m_magnifyVertices[0].x = 0.5f;
m_magnifyVertices[0].y = -0.5f;
m_magnifyVertices[0].z = 0.0f;
float angle = 0.0f;
float step = G.PI*2.0f / (m_magnifyVertices.Length-1);
for ( int i=1 ; i<m_magnifyVertices.Length ; i++, angle+=step )
{
m_magnifyVertices[i].x = m_magnifyVertices[0].x + Mathf.Sin(angle)*0.5f;
m_magnifyVertices[i].y = m_magnifyVertices[0].y + Mathf.Cos(angle)*0.5f;
m_magnifyVertices[i].z = 0.0f;
}
m_magnifyTriangles = new int[(m_magnifyVertices.Length-1)*3];
for ( int i=0, j=1 ; i<m_magnifyTriangles.Length ; i+=3, j++ )
{
m_magnifyTriangles[i] = 0;
m_magnifyTriangles[i+1] = j;
m_magnifyTriangles[i+2] = i+3==m_magnifyTriangles.Length ? 1 : j+1;
}
m_magnifyUvs = new Vector2[m_magnifyVertices.Length];
}
public void __341()
{
m_matrix = Matrix4x4.identity;
}
public void __342()
{
Vector3 s;
s.x = 2.0f* G.m_windowRatio;
s.y = 2.0f;
s.z = 1.0f;
Vector3 v;
v.x = -1.0f* G.m_windowRatio;
v.y = 1.0f;
v.z = 0.0f;
m_matrix.SetTRS(v, Quaternion.identity, s);
}
public void __343(ref Rect rc)
{
Vector3 s;
s.x = (rc.width/G.m_rcWindow.width) * G.m_windowRatio * 2.0f;
s.y = rc.height/G.m_rcWindow.height * 2.0f;
s.z = 1.0f;
Vector3 v;
v.x = rc.x;
v.y = rc.y;
v.z = 0.0f;
v = Camera.main.ScreenToWorldPoint(v);
v.y = -v.y;
v.z = 0.0f;
m_matrix.SetTRS(v, Quaternion.identity, s);
}
public void __344(ref Rect rcDoc)
{
Vector3 s;
s.x = rcDoc.width/G.m_rcView.width * G.m_windowRatio * 2.0f;
s.y = rcDoc.height/G.m_rcView.height * 2.0f;
s.z = 1.0f;
Vector3 v;
v.x = (rcDoc.x/G.m_rcView.width * 2.0f - 1.0f) * G.m_windowRatio;
v.y = -rcDoc.y/G.m_rcView.height * 2.0f + 1.0f;
v.z = 0.0f;
m_matrix.SetTRS(v, Quaternion.identity, s);
}
public void __344(ref Obb obb)
{
m_useTempVertices = true;
for ( int i=0 ; i<4 ; i++ )
{
m_tempVertices[i].x = (obb.pts[i].x/G.m_rcView.width * 2.0f - 1.0f) * G.m_windowRatio;
m_tempVertices[i].y = -obb.pts[i].y/G.m_rcView.height * 2.0f + 1.0f;
}
__341();
}
public void __345()
{
Vector3 s;
s.x = 2.0f / G.m_rcView.width * G.m_windowRatio;
s.y = -2.0f / G.m_rcView.height;
s.z = 1.0f;
Vector3 v;
v.x = -G.m_windowRatio;
v.y = 1.0f;
v.z = 0.0f;
m_matrix.SetTRS(v, Quaternion.identity, s);
}
public void __346(Scene scene)
{
Vector3 s;
s.x = 2.0f / G.m_rcView.width * scene.m_renderScale * G.m_windowRatio;
s.y = -2.0f / G.m_rcView.height * scene.m_renderScale;
s.z = 1.0f;
Vector3 v;
v.x = scene.m_renderX * scene.m_renderScale - scene.m_rc.x - scene.m_dx;
v.y = scene.m_renderY * scene.m_renderScale - scene.m_rc.y - scene.m_dy;
v.x = (-v.x/G.m_rcView.width-0.5f) * 2.0f * G.m_windowRatio;
v.y = (-v.y/G.m_rcView.height-0.5f) * -2.0f;
v.z = 0.0f;
m_matrix.SetTRS(v, Quaternion.identity, s);
}
public void __346(SceneObj obj)
{
Scene scene = obj.m_scene;
Vector3 s;
s.z = 1.0f;
if ( obj.m_hud )
{
s.x = 2.0f / G.m_rcView.width * G.m_windowRatio;
s.y = -2.0f / G.m_rcView.height;
}
else
{
s.x = 2.0f / G.m_rcView.width * scene.m_renderScale * G.m_windowRatio;
s.y = -2.0f / G.m_rcView.height * scene.m_renderScale;
}
Vector3 v;
v.z = 0.0f;
if ( obj.m_hud )
{
v.x = 0.0f;
v.y = 0.0f;
}
else
{
v.x = scene.m_renderX * scene.m_renderScale - scene.m_rc.x - scene.m_dx;
v.y = scene.m_renderY * scene.m_renderScale - scene.m_rc.y - scene.m_dy;
}
v.x = (-v.x/G.m_rcView.width-0.5f) * 2.0f * G.m_windowRatio;
v.y = (-v.y/G.m_rcView.height-0.5f) * -2.0f;
m_matrix.SetTRS(v, Quaternion.identity, s);
}
public void __347(SceneObj obj)
{
Scene scene = obj.m_scene;
Vector3 s;
s.x = 2.0f / G.m_rcView.height;
s.y = -2.0f / G.m_rcView.width * G.m_windowRatio;
s.z = 1.0f;
Vector3 v;
v.z = 0.0f;
if ( obj.m_hud )
{
v.x = scene.m_rc.x + obj.__35();
v.y = scene.m_rc.y + obj.__36();
}
else
{
v.x = scene.m_rc.x + (obj.__35()-scene.m_renderX)*scene.m_renderScale + scene.m_dx;
v.y = scene.m_rc.y + (obj.__36()-scene.m_renderY)*scene.m_renderScale + scene.m_dy;
}
v.x = (v.x/G.m_rcView.width-0.5f) * 2.0f * G.m_windowRatio;
v.y = (v.y/G.m_rcView.height-0.5f) * -2.0f;
m_matrix.SetTRS(v, Quaternion.identity, s);
}
public void __348(ref Rect rc, float texWidth, float texHeight, bool rotated = false, bool flipped = false)
{
float left = rc.x/texWidth;
float top = 1.0f - rc.y/texHeight;
float right = rc.__437()/texWidth;
float bottom = 1.0f - rc.__438()/texHeight;
if ( rotated )
{
if ( flipped )
{
m_tempUvs[0].Set(right, bottom);
m_tempUvs[1].Set(right, top);
m_tempUvs[2].Set(left, top);
m_tempUvs[3].Set(left, bottom);
}
else
{
m_tempUvs[0].Set(right, top);
m_tempUvs[1].Set(right, bottom);
m_tempUvs[2].Set(left, bottom);
m_tempUvs[3].Set(left, top);
}
}
else
{
if ( flipped )
{
m_tempUvs[0].Set(right, top);
m_tempUvs[1].Set(left, top);
m_tempUvs[2].Set(left, bottom);
m_tempUvs[3].Set(right, bottom);
}
else
{
m_tempUvs[0].Set(left, top);
m_tempUvs[1].Set(right, top);
m_tempUvs[2].Set(right, bottom);
m_tempUvs[3].Set(left, bottom);
}
}
m_useMesh = MESH.TEMP;
__349();
}
public void __349()
{
G.__178(ref m_meshTemp);
if ( m_useTempVertices )
m_meshTemp.vertices = m_tempVertices;
else
m_meshTemp.vertices = m_vertices;
m_meshTemp.uv = m_tempUvs;
m_meshTemp.triangles = m_triangles;
}
public void __350(ref Rect rc, float texWidth, float texHeight)
{
float x = rc.__440()/texWidth;
float y = rc.__441()/texHeight;
float w = rc.width/texWidth * 0.5f;
float h = rc.height/texHeight * 0.5f;
float angle = 0.0f;
float step = G.PI*2.0f / (m_magnifyUvs.Length-1);
m_magnifyUvs[0].x = x;
m_magnifyUvs[0].y = 1.0f - y;
for ( int i=1 ; i<m_magnifyUvs.Length ; i++, angle+=step )
{
m_magnifyUvs[i].x = x + Mathf.Sin(angle)*w;
m_magnifyUvs[i].y = 1.0f - (y + Mathf.Cos(angle)*-h);
}
G.__178(ref m_meshMagnify);
m_meshMagnify.vertices = m_magnifyVertices;
m_meshMagnify.uv = m_magnifyUvs;
m_meshMagnify.triangles = m_magnifyTriangles;
m_useMesh = MESH.MAGNIFY;
}
public void __351()
{
m_useMesh = MESH.DEFAULT;
m_useTempVertices = false;
}
public void __352()
{
__343(ref G.m_rcClient);
RenderTexture rt = G.__173(G.m_mainRT);
G.m_materialTexture24.mainTexture = rt;
__364(G.m_materialTexture24);
G.m_materialTexture24.mainTexture = null;
G.__170(rt);
Rect rc = Rect.Zero;
if ( G.m_screenLessGame )
{
switch ( G.m_game.m_ratioLess )
{
case RATIOLESS.LETTERBOX:
{
float height = (G.m_rcWindow.height - G.m_rcClient.height)*0.5f;
if ( height>0.0f )
{
rc.Set(0.0f, 0.0f, G.m_rcWindow.width, height);
__343(ref rc);
__364(G.m_materialErase);
rc.Set(0.0f, G.m_rcWindow.height-height, G.m_rcWindow.width, height);
__343(ref rc);
__364(G.m_materialErase);
}
break;
}
case RATIOLESS.LETTERBOX_TOP:
{
float height = G.m_rcWindow.height - G.m_rcClient.height;
if ( height>0.0f )
{
rc.Set(0.0f, 0.0f, G.m_rcWindow.width, height);
__343(ref rc);
__364(G.m_materialErase);
}
break;
}
case RATIOLESS.LETTERBOX_BOTTOM:
{
float height = G.m_rcWindow.height - G.m_rcClient.height;
if ( height>0.0f )
{
rc.Set(0.0f, G.m_rcWindow.height-height, G.m_rcWindow.width, height);
__343(ref rc);
__364(G.m_materialErase);
}
break;
}
}
}
else
{
switch ( G.m_game.m_ratioGreat )
{
case RATIOGREAT.LETTERBOX:
{
float width = (G.m_rcWindow.width - G.m_rcClient.width)*0.5f;
if ( width>0.0f )
{
rc.Set(0.0f, 0.0f, width, G.m_rcWindow.height);
__343(ref rc);
__364(G.m_materialErase);
rc.Set(G.m_rcWindow.width-width, 0, width, G.m_rcWindow.height);
__343(ref rc);
__364(G.m_materialErase);
}
break;
}
case RATIOGREAT.LETTERBOX_LEFT:
{
float width = G.m_rcWindow.width - G.m_rcClient.width;
if ( width>0.0f )
{
rc.Set(0.0f, 0.0f, width, G.m_rcWindow.height);
__343(ref rc);
__364(G.m_materialErase);
}
break;
}
case RATIOGREAT.LETTERBOX_RIGHT:
{
float width = G.m_rcWindow.width - G.m_rcClient.width;
if ( width>0.0f )
{
rc.Set(G.m_rcWindow.width-width, 0.0f, width, G.m_rcWindow.height);
__343(ref rc);
__364(G.m_materialErase);
}
break;
}
}
}
/*if ( G.m_rcClient.x>0 )
{
int space = G.m_rcWindow.width - G.m_rcClient.width;
int spaceHalf = space/2;
int leftWidth = spaceHalf;
int rightWidth = spaceHalf + space%2;
__343(new Rectangle(0, 0, leftWidth, G.m_rcWindow.height));
__364(G.m_materialErase);
__343(new Rectangle(G.m_rcWindow.width-rightWidth, 0, rightWidth, G.m_rcWindow.height));
__364(G.m_materialErase);
}*/
}
public bool __353(FXO fxo)
{
Effect effect = G.m_game.__285();
if ( effect==null )
return false;
int count = effect.__68(fxo);
if ( count==0 )
return false;
if ( count==1 )
{
RenderTexture rt = G.__168();
RenderTexture old = G.__173(rt);
G.__171();
G.__71(old);
G.__173(old);
__354(rt, effect, fxo);
G.__170(rt);
}
else
{
__354(G.__174(), effect, fxo);
}
return true;
}
public void __354(Material material)
{
__344(ref G.m_rcView);
__364(material);
}
public void __354(Texture texture, Effect effect, FXO fxo)
{
__344(ref G.m_rcView);
effect.__71(fxo, texture);
}
public void __355(Material material, ref Rect rc, float opacity = 1.0f, BLEND blend = BLEND.DEFAULT)
{
__344(ref rc);
__364(material, opacity, blend);
}
public void __356(RenderTexture rt, ref Rect rcSrc)
{
__342();
__350(ref rcSrc, (float)rt.width, (float)rt.height);
G.__71(rt);
__351();
}
public void __357(Sprite sprite, ref Rect rcSrc, ref Rect rcTrg, bool rotated = false, bool flipped = false)
{
__344(ref rcTrg);
__348(ref rcSrc, sprite.m_width, sprite.m_height, rotated, flipped);
__364(sprite.m_material);
__351();
}
public void __358(Sprite sprite, ref Rect rcSrcUV, ref Rect rc, bool flip = false)
{
__344(ref rc);
if ( flip )
{
m_tempUvs[0].Set(rcSrcUV.__437(), 1.0f-rcSrcUV.y);
m_tempUvs[1].Set(rcSrcUV.x, 1.0f-rcSrcUV.y);
m_tempUvs[2].Set(rcSrcUV.x, 1.0f-rcSrcUV.__438());
m_tempUvs[3].Set(rcSrcUV.__437(), 1.0f-rcSrcUV.__438());
}
else
{
m_tempUvs[0].Set(rcSrcUV.x, 1.0f-rcSrcUV.y);
m_tempUvs[1].Set(rcSrcUV.__437(), 1.0f-rcSrcUV.y);
m_tempUvs[2].Set(rcSrcUV.__437(), 1.0f-rcSrcUV.__438());
m_tempUvs[3].Set(rcSrcUV.x, 1.0f-rcSrcUV.__438());
}
__349();
m_useMesh = MESH.TEMP;
__364(sprite.m_material);
__351();
}
public bool __359(LayerSprite sprite, ref Rect rc, Effect effect = null, FXO output = FXO.COUNT, float opacity = 1.0f)
{
if ( sprite.m_sprites.Length==0 )
return false;
if ( effect && effect.__68(output)==0 )
effect = null;
if ( sprite.m_rowCount==0 )
{
if ( effect==null )
__355(sprite.m_sprites[0].m_material, ref rc, opacity, sprite.m_blend);
else
{
__344(ref rc);
effect.__71(output, sprite.m_sprites[0].m_texture, sprite.m_width, sprite.m_height, opacity, sprite.m_blend, sprite.m_depth);
}
}
else
{
float partWidth = sprite.m_partWidth * rc.width;
float partHeight = sprite.m_partHeight * rc.height;
partWidth /= sprite.m_width;
partHeight /= sprite.m_height;
Rect rcPart = new Rect();
for ( int row=0 ; row<sprite.m_rowCount ; row++ )
{
for ( int col=0 ; col<sprite.m_colCount ; col++ )
{
int index = row*sprite.m_colCount + col;
rcPart.Set(rc.x+col*partWidth, rc.y+row*partHeight, partWidth, partHeight);
__344(ref rcPart);
if ( effect==null )
__364(sprite.m_sprites[index].m_material, opacity, sprite.m_blend);
else
effect.__71(output, sprite.m_sprites[index].m_texture, sprite.m_width, sprite.m_height, opacity, sprite.m_blend, sprite.m_depth);
}
}
}
return true;
}
public void __360(Frame frame, ref Rect rc)
{
__344(ref rc);
__348(ref frame.m_source, frame.m_sprite.m_width, frame.m_sprite.m_height, frame.m_rotated, frame.m_flipped);
__364(frame.m_sprite.m_material);
__351();
}
public void __361(Color tint, Frame frame, ref Rect rc)
{
__344(ref rc);
__348(ref frame.m_source, frame.m_sprite.m_width, frame.m_sprite.m_height, frame.m_rotated, frame.m_flipped);
Material material = frame.m_sprite.m_material;
material.SetColor("_tint", tint);
material.SetFloat("_layer", (float)(frame.m_layer/8));
material.SetFloat("_layer2", (float)(Mathf.Pow(2.0f, frame.m_layer%8)));
__364(material);
__351();
}
public void __362(SceneObj sceneObj, Frame frame)
{
if ( sceneObj.m_blend==BLEND.SOFTLIGHT )
__363(sceneObj, frame);
else if ( sceneObj.m_obj.m_monochrome )
__361(sceneObj, frame);
else
__360(sceneObj, frame);
}
public void __360(SceneObj sceneObj, Frame frame)
{
if ( sceneObj.m_draw.hasObb )
__344(ref sceneObj.m_draw.obb);
else
__344(ref sceneObj.m_draw.view);
__348(ref frame.m_source, frame.m_sprite.m_width, frame.m_sprite.m_height, frame.m_rotated, frame.m_flipped);
__364(frame.m_sprite.m_material, sceneObj.m_opacity.cur, sceneObj.m_blend);
__351();
}
public void __363(SceneObj sceneObj, Frame frame)
{
G.m_materialSoftLight.mainTexture = frame.m_sprite.m_texture;
if ( sceneObj.m_draw.hasObb )
__344(ref sceneObj.m_draw.obb);
else
__344(ref sceneObj.m_draw.view);
__348(ref frame.m_source, frame.m_sprite.m_width, frame.m_sprite.m_height, frame.m_rotated, frame.m_flipped);
__364(G.m_materialSoftLight);
__351();
G.m_materialSoftLight.mainTexture = null;
}
public void __361(SceneObj sceneObj, Frame frame)
{
if ( sceneObj.m_draw.hasObb )
__344(ref sceneObj.m_draw.obb);
else
__344(ref sceneObj.m_draw.view);
__348(ref frame.m_source, frame.m_sprite.m_width, frame.m_sprite.m_height, frame.m_rotated, frame.m_flipped);
Material material = frame.m_sprite.m_material;
material.SetColor("_tint", sceneObj.m_obj.m_tint.cur);
material.SetFloat("_layer", (float)(frame.m_layer/8));
material.SetFloat("_layer2", (float)Mathf.Pow(2.0f, frame.m_layer%8));
__364(material, sceneObj.m_opacity.cur, sceneObj.m_blend);
__351();
}
public void __364(Material material, float opacity = 1.0f, BLEND blend = BLEND.DEFAULT)
{
if ( material )
{
int srcBlend = 0;
int dstBlend = 0;
if ( blend!=BLEND.DEFAULT )
{
srcBlend = material.GetInt("_SrcBlend");
dstBlend = material.GetInt("_DstBlend");
material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.One);
}
if ( opacity!=1.0f )
material.color = new Color(opacity, opacity, opacity, opacity);
for ( int i=0 ; i<material.passCount ; i++ )
{
if ( material.SetPass(i) )
{
switch ( m_useMesh )
{
case MESH.TEMP:
Graphics.DrawMeshNow(m_meshTemp, m_matrix);
break;
case MESH.MAGNIFY:
Graphics.DrawMeshNow(m_meshMagnify, m_matrix);
break;
default:
Graphics.DrawMeshNow(m_mesh, m_matrix);
break;
}
}
}
if ( opacity!=1.0f )
material.color = G.m_colorWhite;
if ( blend!=BLEND.DEFAULT )
{
material.SetInt("_SrcBlend", srcBlend);
material.SetInt("_DstBlend", dstBlend);
}
}
}
public void __365(Mesh mesh, Material material, float opacity = 1.0f, BLEND blend = BLEND.DEFAULT)
{
if ( mesh && material )
{
int srcBlend = 0;
int dstBlend = 0;
if ( blend!=BLEND.DEFAULT )
{
srcBlend = material.GetInt("_SrcBlend");
dstBlend = material.GetInt("_DstBlend");
material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.One);
}
if ( opacity!=1.0f )
material.color = new Color(opacity, opacity, opacity, opacity);
for ( int i=0 ; i<material.passCount ; i++ )
{
if ( material.SetPass(i) )
Graphics.DrawMeshNow(mesh, m_matrix);
}
if ( opacity!=1.0f )
material.color = G.m_colorWhite;
if ( blend!=BLEND.DEFAULT )
{
material.SetInt("_SrcBlend", srcBlend);
material.SetInt("_DstBlend", dstBlend);
}
}
}
public void __366(float ax, float ay, float bx, float by, Color color, float size = 1.0f)
{
if ( ax==bx && ay==by )
return;
float half = size*0.5f;
G.m_materialErase.color = color;
if ( ax==bx )
{
Rect rc = new Rect(ax-half, Mathf.Min(ay, by), size, Mathf.Abs(ay-by));
__355(G.m_materialErase, ref rc);
}
else if ( ay==by )
{
Rect rc = new Rect(Mathf.Min(ax, bx), ay-half, Mathf.Abs(ax-bx), size);
__355(G.m_materialErase, ref rc);
}
else
{
Rect rc = new Rect(0, 0, size, size);
Vector2 p1 = new Vector2(ax, ay);
Vector2 p2 = new Vector2(bx, by);
Vector2 t = new Vector2(ax, ay);
float frac = Mathf.Sqrt(Mathf.Pow(bx-ax, 2) + Mathf.Pow(by-ay, 2));
if ( G.__129(frac) )
return;
frac = 1.0f/frac;
float ctr = 0.0f;
while ( (int)t.x!=(int)bx || (int)t.y!=(int)by )
{
t = Vector2.Lerp(p1, p2, ctr);
ctr += frac;
rc.x = (int)(t.x-half);
rc.y = (int)(t.y-half);
__355(G.m_materialErase, ref rc);
}
}
G.m_materialErase.color = Color.black;
}
public void __367()
{
const int maxCount = 64;
const float dotSize = 8.0f;
const float circleRadius = 32.0f;
float x = G.m_rcViewUI.__440();
float y = G.m_rcViewUI.__441();
float ratio = G.Clamp((G.m_game.m_time-G.m_game.m_input.m_isDownTime)/G.SKIP_DELAY);
int count = (int)(ratio*maxCount);
Rect rc = new Rect(0.0f, 0.0f, dotSize, dotSize);
for ( int i=0 ; i<count ; i++ )
{
float angle = i * G.RAD_360 / maxCount - G.RAD_90;
rc.x = x + Mathf.Cos(angle)*circleRadius - dotSize*0.5f;
rc.y = y + Mathf.Sin(angle)*circleRadius - dotSize*0.5f;
__355(G.m_spriteSkip.m_material, ref rc);
}
}
}
