using UnityEngine;
using System.Collections.Generic;
public class Player
{
public bool m_dump = false;
public int m_sid;
public string m_uid = "";
public string[] m_tags;
public bool m_interaction;
public Serial<bool> m_hasScroll;
public Serial<float> m_scrollSmooth;
public Serial<bool> m_hasZoom;
public Serial<float> m_zoomSmooth;
public bool m_hasShowPath;
public Color m_pathColor;
public Serial<int> m_icon;
public Sprite[] m_icons;
public Obj m_obj;
public SceneObj m_sceneObj;
public List<Obj> m_items = new List<Obj>();
public int m_scroll = 0;
public Scene m_lastScene;
public float m_lastX = G.INVALIDCOORD;
public float m_lastY = G.INVALIDCOORD;
public Mesh m_mesh;
public static implicit operator bool(Player exists) { return exists!=null; }
public void Reset()
{
End();
m_sceneObj = null;
for ( int i=0 ; i<m_items.Count ; i++ )
{
Sprite icon = m_items[i].__454();
if ( icon )
icon.End();
}
m_items.Clear();
m_obj = null;
m_hasScroll.Reset();
m_scrollSmooth.Reset();
m_hasZoom.Reset();
m_zoomSmooth.Reset();
m_lastScene = null;
m_lastX = G.INVALIDCOORD;
m_lastY = G.INVALIDCOORD;
m_icon.Reset();
m_scroll = 0;
}
public void __46(JsonObj json)
{
json.__383("sid", m_sid);
json.__381("uid", m_uid);
json.__381("tag", m_tags[0]);
json.__381("obj", m_obj==null ? "" : m_obj.m_uid);
json.__381("lastScene", m_lastScene==null ? "" : m_lastScene.m_uid);
json.__382("lastX", (int)m_lastX);
json.__382("lastY", (int)m_lastY);
if ( m_hasScroll.modified )
json.__385("scroll", m_hasScroll.cur);
if ( m_hasZoom.modified )
json.__385("zoom", m_hasZoom.cur);
if ( m_icon.modified )
json.__382("icon", m_icon.cur);
JsonArray jItems = json.__390("items");
for ( int i=0 ; i<m_items.Count ; i++ )
jItems.__381(m_items[i].m_uid);
}
public void __47(JsonObj json)
{
m_tags[0] = json.GetString("tag");
m_obj = G.m_game.__277(json.GetString("obj"));
m_lastScene = G.m_game.__274(json.GetString("lastScene"));
m_lastX = json.GetInt("lastX");
m_lastY = json.GetInt("lastY");
if ( json.__391("scroll") )
m_hasScroll.Set(json.__401("scroll"));
if ( json.__391("zoom") )
m_hasZoom.Set(json.__401("zoom"));
if ( json.__391("icon") )
m_icon.Set(json.GetInt("icon"));
JsonArray jItems = json.__395("items");
if ( jItems )
{
for ( int i=0 ; i<jItems.__67() ; i++ )
{
Obj obj = G.m_game.__277(jItems.GetString(i));
if ( obj && m_items.IndexOf(obj)==-1 )
m_items.Add(obj);
}
}
}
public void __469()
{
Asset asset = G.__96(G.m_pathGraphics);
if ( asset )
{
for ( int i=0 ; i<m_items.Count ; i++ )
{
Sprite icon = m_items[i].__454();
if ( icon )
icon.__469(asset);
}
asset.Close();
}
}
public void End()
{
m_scroll = 0;
if ( m_dump==false )
{
for ( int i=0 ; i<m_items.Count ; i++ )
{
Sprite icon = m_items[i].__454();
if ( icon )
icon.End();
}
}
}
public bool __48(string nameOrTag)
{
return __48(ref nameOrTag);
}
public bool __48(ref string nameOrTag)
{
if ( nameOrTag.Length==0 )
return false;
if ( nameOrTag[0]=='@' )
return G.__149(m_tags, nameOrTag.Substring(1));
return G.__148(ref m_uid, ref nameOrTag);
}
public void __477(string uid)
{
m_lastScene = G.m_game.__274(uid);
m_lastX = G.INVALIDCOORD;
m_lastY = G.INVALIDCOORD;
}
public bool __478()
{
return m_uid.Length==0;
}
public CAMERA __479()
{
if ( m_hasScroll.cur && m_hasZoom.cur )
return CAMERA.AUTO;
if ( m_hasScroll.cur )
return CAMERA.AUTO_POS;
if ( m_hasZoom.cur )
return CAMERA.AUTO_SCALE;
return CAMERA.OFF;
}
public Sprite __454()
{
if ( m_icons==null )
return null;
return m_icons[m_icon.cur];
}
public void __480(Obj obj, int index = -1)
{
if ( obj && m_items.IndexOf(obj)==-1 )
{
if ( index==-1 )
m_items.Add(obj);
else
m_items.Insert(index, obj);
G.m_game.m_layout.__219();
if ( m_dump==false || G.m_game.__293()==this )
{
Asset asset = G.__96(G.m_pathGraphics);
if ( asset )
{
Sprite icon = obj.__454();
if ( icon )
icon.__469(asset);
asset.Close();
}
}
}
}
public void __481(Obj obj)
{
int index = m_items.IndexOf(obj);
if ( index!=-1 )
{
m_items.RemoveAt(index);
G.m_game.m_layout.__219();
__486();
if ( m_dump==false )
{
Sprite icon = obj.__454();
if ( icon )
icon.End();
}
}
}
public void __482()
{
while ( m_items.Count>0 )
__481(m_items[m_items.Count-1]);
}
public void __483(Player trg)
{
if ( trg==null )
return;
while ( m_items.Count>0 )
{
Obj obj = m_items[0];
__481(obj);
trg.__480(obj);
}
}
public bool __484(Obj obj)
{
return obj && m_items.IndexOf(obj)!=-1;
}
public bool __484(string nameOrTag)
{
return __484(ref nameOrTag);
}
public bool __484(ref string nameOrTag)
{
bool ok = true;
if ( nameOrTag=="NONE" )
{
if ( m_items.Count>0 )
ok = false;
}
else if ( nameOrTag=="ANY" )
{
if ( m_items.Count==0 )
ok = false;
}
else if ( nameOrTag[0]=='@' )
{
ok = false;
string tag = nameOrTag.Substring(1);
for ( int i=0 ; i<m_items.Count ; i++ )
{
if ( G.__149(m_items[i].m_tags, tag) )
{
ok = true;
break;
}
}
}
else
{
if ( __484(G.m_game.__277(nameOrTag))==false )
ok = false;
}
return ok;
}
public int __485(Obj obj)
{
if ( obj )
return m_items.IndexOf(obj);
return -1;
}
public void __486()
{
if ( m_scroll<0 )
{
m_scroll = 0;
return;
}
int itemCount = G.m_game.m_layout.Get(LAYOUT_CTRL.ITEMS).m_itemCount;
int emptyCount = itemCount - m_items.Count;
if ( emptyCount>=0 )
{
m_scroll = 0;
return;
}
if ( m_scroll>-emptyCount )
{
m_scroll = m_items.Count - itemCount;
return;
}
}
public bool __487()
{
int itemCount = G.m_game.m_layout.Get(LAYOUT_CTRL.ITEMS).m_itemCount;
int emptyCount = itemCount - m_items.Count;
if ( emptyCount>=0 || m_scroll>=-emptyCount )
return true;
return false;
}
public Obj __432(float xView, float yView, out bool empty)
{
empty = false;
int index = G.m_game.m_layout.Get(LAYOUT_CTRL.ITEMS).__432(xView, yView);
if ( index==-1 )
return null;
index += m_scroll;
if ( index<0 || index>=m_items.Count )
{
empty = true;
return null;
}
return m_items[index];
}
public Router __296()
{
if ( m_hasShowPath==false )
return null;
if ( m_sceneObj==null )
return null;
if ( m_sceneObj.m_grids==null )
return null;
if ( m_sceneObj.m_grids.Length==0 || m_sceneObj.m_iGrid.cur>=m_sceneObj.m_grids.Length )
return null;
Grid grid = m_sceneObj.m_grids[m_sceneObj.m_iGrid.cur];
if ( grid==null )
return null;
return grid.m_routerHud;
}
public void __488()
{
Router router = __296();
if ( router )
router.__515();
}
public void __43()
{
if ( G.m_game.__254() && m_sceneObj )
{
Router routerHud = __296();
Scene scene = m_sceneObj.m_scene;
if ( routerHud && routerHud.m_pathBank.Count>0 )
{
int iStart = routerHud.m_hasBridgeAtStart ? 4 : 0;
G.m_materialErase.color = G.m_colorWhite;
for ( int i=iStart ; i<routerHud.m_pathBank.Count-2 ; i+=2 )
{
int col = routerHud.m_pathBank[i];
int row = routerHud.m_pathBank[i+1];
if ( col==0 && row==0 )
continue;
int col2 = routerHud.m_pathBank[i+2];
int row2 = routerHud.m_pathBank[i+3];
if ( col2==0 && row2==0 )
continue;
if ( Mathf.Abs(col-col2)>1 || Mathf.Abs(row-row2)>1 )
{
float x = scene.__549(G.__146(col));
float y = scene.__550(G.__146(row));
float x2 = scene.__549(G.__146(col2));
float y2 = scene.__550(G.__146(row2));
G.m_graphics.__366(x, y, x2, y2, m_pathColor, 3);
}
}
int count = 0;
for ( int i=iStart ; i<routerHud.m_pathBank.Count ; i+=2 )
{
if ( routerHud.m_pathBank[i]!=0 || routerHud.m_pathBank[i+1]!=0 )
count++;
}
Vector3[] vertices = new Vector3[count*4];
Vector2[] uvs = new Vector2[count*4];
int[] triangles = new int[count*6];
for ( int i=iStart, j=0 ; i<routerHud.m_pathBank.Count ; i+=2, j++ )
{
int col = routerHud.m_pathBank[i];
int row = routerHud.m_pathBank[i+1];
if ( col==0 && row==0 )
continue;
float x = col * G.PATH_GRID_CELLSIZE;
float y = row * G.PATH_GRID_CELLSIZE;
int iV = j*4;
int iT = j*6;
vertices[iV+0].x = x;
vertices[iV+0].y = y;
vertices[iV+1].x = x + G.PATH_GRID_CELLSIZE;
vertices[iV+1].y = y;
vertices[iV+2].x = x + G.PATH_GRID_CELLSIZE;
vertices[iV+2].y = y + G.PATH_GRID_CELLSIZE;
vertices[iV+3].x = x;
vertices[iV+3].y = y + G.PATH_GRID_CELLSIZE;
vertices[iV+0].z = 0.0f;
vertices[iV+1].z = 0.0f;
vertices[iV+2].z = 0.0f;
vertices[iV+3].z = 0.0f;
uvs[iV+0].x = 0.0f;
uvs[iV+0].y = 1.0f;
uvs[iV+1].x = 1.0f;
uvs[iV+1].y = 1.0f;
uvs[iV+2].x = 1.0f;
uvs[iV+2].y = 0.0f;
uvs[iV+3].x = 0.0f;
uvs[iV+3].y = 0.0f;
triangles[iT+0] = iV + 0;
triangles[iT+1] = iV + 1;
triangles[iT+2] = iV + 2;
triangles[iT+3] = iV + 2;
triangles[iT+4] = iV + 3;
triangles[iT+5] = iV + 0;
}
Material material = G.m_game.m_uiWay==null ? G.m_materialErase : G.m_game.m_uiWay.m_material;
G.__178(ref m_mesh);
m_mesh.vertices = vertices;
m_mesh.uv = uvs;
m_mesh.triangles = triangles;
G.m_graphics.__346(scene);
G.m_graphics.__365(m_mesh, material);
G.m_materialErase.color = Color.black;
}
}
Layout layout = G.m_game.m_layout;
LayoutCtrl ctrl = layout.Get(LAYOUT_CTRL.ITEMS);
if ( ctrl.__428() )
{
int index = m_scroll;
for ( int i=0 ; i<ctrl.m_itemCount && index<m_items.Count ; i++, index++ )
{
Obj obj = m_items[index];
if ( obj )
{
Sprite icon = obj.__454();
if ( icon && icon.m_material )
G.m_graphics.__355(icon.m_material, ref ctrl.m_rcCells[ctrl.m_iFirstItem+i]);
}
}
}
ctrl = layout.Get(LAYOUT_CTRL.PLAYERS);
if ( ctrl.__428() )
{
int index = layout.m_playerScroll;
for ( int i=0 ; i<ctrl.m_itemCount && index<layout.m_visiblePlayers.Length ; i++, index++ )
{
Player player = layout.m_visiblePlayers[index];
if ( player && player!=this )
{
Sprite icon = player.__454();
if ( icon && icon.m_material )
G.m_graphics.__355(icon.m_material, ref ctrl.m_rcCells[ctrl.m_iFirstItem+i]);
}
}
}
}
}
