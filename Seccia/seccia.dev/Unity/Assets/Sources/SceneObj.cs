using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
public class SceneObj : SceneEntity
{
public Obj m_obj;
public int m_sid;
public string[] m_tags;
public int m_visibleInThisScene;
public Serial<float> m_elevator;
public bool m_elevatorScaling;
public bool m_elevatorRotation;
public Serial<float> m_z;
public Serial<bool> m_autoZ;
public Serial<float> m_manualScale;
public Serial<float> m_manualZoom;
public float m_parallax;
public Serial<int> m_angle;
public float m_speedX;
public float m_speedY;
public BLEND m_blend;
public DRAG m_drag;
public Serial<bool> m_cheat;
public bool m_light;
public Serial<bool> m_lightVisible;
public Serial<float> m_lightAmbient;
public Serial<Color> m_lightDiffuse;
public Serial<float> m_lightAngle;
public Serial<float> m_lightDir;
public Serial<float> m_lightDist;
public Serial<float> m_lightAttn;
public Mesh m_lightMesh;
public SpotPath[] m_paths = null;
public Grid[] m_grids = null;
public int m_depthScaleCount;
public float[] m_depthScaleYs;
public float[] m_depthScaleValues;
public int m_depthZoomCount;
public float[] m_depthZoomYs;
public float[] m_depthZoomValues;
public Serial<string> m_sticker;
public Serial<bool> m_visible;
public Serial<string> m_defaultStopAnim;
public Serial<string> m_defaultWalkAnim;
public Serial<string> m_defaultTalkAnim;
public Serial<int> m_iPath;
public Serial<bool> m_pathStarted;
public RoleBox m_pathRoleBox;
public int m_pathRoleBoxToken;
public Serial<int> m_iGrid;
public AnimInfo m_anim;
public bool m_rotating;
public float m_rotateAngle;
public float m_rotateSpeed;
public DrawInfo m_draw;
public float m_scale;
public int m_iSay = -1;
public int m_sayCount = 0;
public BreakTextInfo m_say = new BreakTextInfo();
public Vec2 m_sayPosition;
public bool m_sayDrawnOnce = true;
public float m_timeSay = 0.0f;
public float m_timeSayDuration = 0.0f;
public float m_timeSayDurationForced = 0.0f;
public bool m_sayTyping = false;
public float m_timeSayTypingFinished = 0.0f;
public int m_sayAnimDirOld;
public bool m_sayKeepAnim;
public bool m_sayHasAnimToWait;
public SceneObj m_chaseObj = null;
public int m_chaseObjDeltaCol = 0;
public int m_chaseObjDeltaRow = 0;
public int m_chaseObjCol = -1;
public int m_chaseObjRow = -1;
public Message m_message = null;
public bool m_routeTurn = false;
public Anim m_routeTurnAnim;
public int m_routeTurnDir;
public bool m_routeLocked = false;
public int m_routeStatus = 0;
public bool m_routeStraight = false;
public float m_routeStraightTime;
public float m_routeStraightDuration;
public bool m_routeStraightEaseIn;
public bool m_routeStraightEaseOut;
public float m_routeStraightSrcX;
public float m_routeStraightSrcY;
public float m_routeStraightTrgX;
public float m_routeStraightTrgY;
public float m_routeStraightDistance;
public float m_scrollSmoothSpeed;
public float m_zoomSmoothSpeed;
public string m_eventCell;
public float m_angleWalk;
public Serial<float> m_opacity;
public Serial<Vec2> m_storedPos;
public bool m_lightMeshChanged;
public static implicit operator bool(SceneObj inst) { return inst!=null; }
public SceneObj()
{
m_entity = ENTITY.OBJ;
}
public void Reset(bool fromScript = false)
{
if ( fromScript==false )
{
End();
}
if ( m_obj )
{
m_parentName.Reset();
m_parent = m_scene.__536(ref m_parentName.cur);
}
m_local.Reset();
m_elevator.Reset();
m_z.Reset();
m_autoZ.Reset();
m_manualScale.Reset();
m_manualZoom.Reset();
m_angle.Reset();
m_placement.Reset();
m_cheat.Reset();
m_defaultStopAnim.Reset();
m_defaultWalkAnim.Reset();
m_defaultTalkAnim.Reset();
m_sticker.Reset();
m_visible.Reset();
m_iPath.Reset();
m_pathStarted.Reset();
m_pathRoleBox = null;
m_pathRoleBoxToken = 0;
m_iGrid.Reset();
m_anim.sceneObj = this;
m_anim.Reset();
m_rotating = false;
m_iSay = -1;
m_chaseObj = null;
m_chaseObjDeltaCol = 0;
m_chaseObjDeltaRow = 0;
m_chaseObjCol = -1;
m_chaseObjRow = -1;
m_opacity.Reset();
m_storedPos.Init(new Vec2(G.INVALIDCOORD, G.INVALIDCOORD));
m_draw.Reset();
m_lightMeshChanged = true;
m_lightVisible.Reset();
m_lightAmbient.Reset();
m_lightDiffuse.Reset();
m_lightAngle.Reset();
m_lightDir.Reset();
m_lightDist.Reset();
m_lightAttn.Reset();
G.Release(m_lightMesh);
m_lightMesh = null;
if ( m_paths!=null )
{
for ( int i=0 ; i<G.PATH_GRID_COUNT ; i++ )
{
if ( m_paths[i] )
m_paths[i].Stop();
}
}
if ( m_obj==null )
return;
__632(ref m_defaultStopAnim.cur);
}
public void __46(JsonObj json)
{
json.__382("id", m_sid);
json.__381("tag", m_tags[0]);
if ( m_defaultStopAnim.modified )
json.__381("defStop", m_defaultStopAnim.cur);
if ( m_defaultWalkAnim.modified )
json.__381("defWalk", m_defaultWalkAnim.cur);
if ( m_defaultTalkAnim.modified )
json.__381("defTalk", m_defaultTalkAnim.cur);
if ( m_obj && m_parentName.modified )
json.__381("parent", m_parentName.cur);
if ( m_sticker.modified )
json.__381("sticker", m_sticker.cur);
if ( m_visible.modified )
json.__385("visible", m_visible.cur);
if ( m_iPath.modified )
json.__382("path", m_iPath.cur);
if ( m_pathStarted.modified )
json.__385("pathStarted", m_pathStarted.cur);
if ( m_iGrid.modified )
json.__382("grid", m_iGrid.cur);
if ( m_opacity.modified )
json.__384("opacity", m_opacity.cur);
if ( m_angle.modified )
json.__382("angle", m_angle.cur);
if ( m_elevator.modified )
json.__384("elevator", m_elevator.cur);
if ( m_z.modified )
json.__384("z", m_z.cur);
if ( m_autoZ.modified )
json.__385("autoZ", m_autoZ.cur);
if ( m_manualScale.modified )
json.__384("scale", m_manualScale.cur);
if ( m_manualZoom.modified )
json.__384("zoom", m_manualZoom.cur);
if ( m_cheat.modified )
json.__385("cheat", m_cheat.cur);
if ( m_placement.modified )
json.__382("placement", (int)m_placement.cur);
if ( m_anim.cur==null )
json.__381("anim", m_defaultStopAnim.cur);
else
{
if ( m_anim.cur.m_name==m_defaultWalkAnim.cur || m_anim.cur.m_name==m_defaultTalkAnim.cur )
json.__381("anim", m_defaultStopAnim.cur);
else
json.__381("anim", m_anim.cur.m_name);
}
json.__382("animAction", m_anim.notif.actionFrame);
json.__381("animDialog", m_anim.notif.dialog);
json.__382("animSentence", m_anim.notif.sentence);
json.__381("animRole", m_anim.notif.roleBox==null ? "" : m_anim.notif.roleBox.m_parent.m_uid);
json.__382("animRoleBox", m_anim.notif.roleBox==null ? 0 : m_anim.notif.roleBox.m_id);
json.__382("animRoleBoxToken", m_anim.notif.roleBoxToken);
json.__382("dir", m_anim.iDir);
json.__381("chase", m_chaseObj ? m_chaseObj.m_obj.m_uid : "");
json.__382("chaseCol", m_chaseObjDeltaCol);
json.__382("chaseRow", m_chaseObjDeltaRow);
if ( G.m_game.__291()==m_scene )
{
json.__382("x", (int)m_local.cur.x);
json.__382("y", (int)m_local.cur.y);
}
if ( m_storedPos.modified )
{
json.__382("storedX", (int)m_storedPos.cur.x);
json.__382("storedY", (int)m_storedPos.cur.y);
}
if ( m_light )
{
JsonObj jLight = json.__389("light");
if ( m_lightVisible.modified )
jLight.__385("visible", m_lightVisible.cur);
if ( m_lightAmbient.modified )
jLight.__382("ambient", (int)(m_lightAmbient.cur*255.0f));
if ( m_lightDiffuse.modified )
jLight.__382("diffuse", (int)G.__128(ref m_lightDiffuse.cur));
if ( m_lightAngle.modified )
jLight.__382("angle", (int)(m_lightAngle.cur*G.RAD_TO_DEG));
if ( m_lightDir.modified )
jLight.__382("dir", (int)(m_lightDir.cur*G.RAD_TO_DEG));
if ( m_lightDist.modified )
jLight.__382("dist", (int)m_lightDist.cur);
if ( m_lightAttn.modified )
jLight.__384("attn", m_lightAttn.cur);
}
}
public void __47(JsonObj json)
{
m_tags[0] = json.GetString("tag");
if ( json.__391("defStop") )
m_defaultStopAnim.Set(json.GetString("defStop"));
if ( json.__391("defWalk") )
m_defaultWalkAnim.Set(json.GetString("defWalk"));
if ( json.__391("defTalk") )
m_defaultTalkAnim.Set(json.GetString("defTalk"));
if ( m_obj && json.__391("parent") )
{
m_parentName.Set(json.GetString("parent"));
m_parent = m_scene.__536(ref m_parentName.cur);
}
if ( json.__391("sticker") )
m_sticker.Set(json.GetString("sticker"));
if ( json.__391("visible") )
m_visible.Set(json.__401("visible"));
if ( json.__391("path") )
m_iPath.Set(json.GetInt("path"));
if ( json.__391("pathStarted") )
m_pathStarted.Set(json.__401("pathStarted"));
if ( json.__391("grid") )
m_iGrid.Set(json.GetInt("grid"));
if ( json.__391("opacity") )
m_opacity.Set(json.GetFloat("opacity"));
if ( json.__391("angle") )
m_angle.Set(json.GetInt("angle"));
if ( json.__391("elevator") )
m_elevator.Set(json.GetFloat("elevator"));
if ( json.__391("z") )
m_z.Set(json.GetFloat("z"));
if ( json.__391("autoZ") )
m_autoZ.Set(json.__401("autoZ"));
if ( json.__391("scale") )
m_manualScale.Set(json.GetFloat("scale"));
if ( json.__391("zoom") )
m_manualZoom.Set(json.GetFloat("zoom"));
if ( json.__391("cheat") )
m_cheat.Set(json.__401("cheat"));
if ( json.__391("placement") )
m_placement.Set((PLACEMENT)json.GetInt("placement"));
string anim = json.GetString("anim");
__632(ref anim);
m_anim.notif.actionFrame = json.GetInt("animAction");
m_anim.notif.dialog = json.GetString("animDialog");
m_anim.notif.sentence = json.GetInt("animSentence");
Role role = G.m_game.__283(json.GetString("animRole"));
m_anim.notif.roleBox = role==null ? null : role.__497(json.GetInt("animRoleBox"));
m_anim.notif.roleBoxToken = m_anim.notif.roleBox==null ? 0 : json.GetInt("animRoleBoxToken");
m_anim.__678(json.GetInt("dir"));
m_chaseObj = G.m_game.__307(m_scene.m_uid, json.GetString("chase"));
m_chaseObjDeltaCol = json.GetInt("chaseCol");
m_chaseObjDeltaRow = json.GetInt("chaseRow");
if ( json.__391("x") )
m_local.Set(new Vec2((float)json.GetInt("x"), (float)json.GetInt("y")));
if ( json.__391("storedX") )
m_storedPos.Set(new Vec2((float)json.GetInt("storedX"), (float)json.GetInt("storedY")));
if ( m_light )
{
JsonObj jLight = json.__394("light");
if ( jLight )
{
if ( jLight.__391("visible") )
m_lightVisible.Set(jLight.__401("visible"));
if ( jLight.__391("ambient") )
m_lightAmbient.Set(G.Clamp(jLight.GetInt("ambient")/255.0f));
if ( jLight.__391("diffuse") )
m_lightDiffuse.Set(G.__126((uint)jLight.GetInt("diffuse")));
if ( jLight.__391("angle") )
m_lightAngle.Set(jLight.GetInt("angle")*G.DEG_TO_RAD);
if ( jLight.__391("dir") )
m_lightDir.Set(jLight.GetInt("dir")*G.DEG_TO_RAD);
if ( jLight.__391("dist") )
m_lightDist.Set((float)jLight.GetInt("dist"));
if ( jLight.__391("attn") )
m_lightAttn.Set(jLight.GetFloat("attn"));
}
}
}
public void __469(Asset asset)
{
if ( m_obj==null )
return;
m_scrollSmoothSpeed = 0.0f;
m_zoomSmoothSpeed = 0.0f;
m_obj.__469(asset, m_scene);
if ( m_local.modified )
{
m_local.modified = false;
m_lightMeshChanged = true;
}
if ( m_paths!=null )
{
for ( int i=0 ; i<m_paths.Length ; i++ )
{
if ( m_paths[i] )
m_paths[i].m_spline.Load(m_paths[i].m_spots);
}
}
if ( m_pathStarted.cur )
{
m_pathStarted.Set(false);
if ( m_paths!=null && m_paths[m_iPath.cur] && m_paths[m_iPath.cur].m_spots.Length>0 )
Move(m_paths[m_iPath.cur].m_spots[0].m_x, m_paths[m_iPath.cur].m_spots[0].m_y);
}
m_eventCell = __638(__35(), __36());
if ( m_grids!=null )
{
for ( int i=0 ; i<G.PATH_GRID_COUNT ; i++ )
{
if ( m_grids[i] )
{
if ( m_grids[i].m_router )
m_grids[i].m_router.__469();
if ( m_grids[i].m_routerHud )
m_grids[i].m_routerHud.__469();
}
}
}
}
public void End(Scene nextScene = null)
{
if ( m_obj==null )
return;
m_anim.__688();
G.Release(m_lightMesh);
m_lightMesh = null;
if ( m_paths!=null )
{
for ( int i=0 ; i<m_paths.Length ; i++ )
{
if ( m_paths[i] )
m_paths[i].m_spline.Clear();
}
}
if ( m_grids!=null )
{
for ( int i=0 ; i<G.PATH_GRID_COUNT ; i++ )
{
if ( m_grids[i] )
{
if ( m_grids[i].m_router )
m_grids[i].m_router.End();
if ( m_grids[i].m_routerHud )
m_grids[i].m_routerHud.End();
}
}
}
m_routeStatus = 0;
m_obj.End(m_scene, nextScene);
m_message = null;
m_chaseObjCol = -1;
m_chaseObjRow = -1;
}
public bool __48(ref string nameOrTag)
{
if ( nameOrTag.Length==0 )
return false;
if ( nameOrTag[0]=='@' )
{
string tag = nameOrTag.Substring(1);
if ( G.__149(m_tags, ref tag) )
return true;
if ( G.__149(m_obj.m_tags, ref tag) )
return true;
return false;
}
return G.__148(ref m_obj.m_uid, ref nameOrTag);
}
public bool __473()
{
if ( m_obj && m_obj.m_title.Get().Length>0 )
return true;
return false;
}
public void __624()
{
if ( G.m_game.__291()!=m_scene )
return;
m_storedPos.Set(m_local.cur);
}
public void __625()
{
if ( G.m_game.__291()!=m_scene || m_storedPos.modified==false )
return;
__630(m_storedPos.cur.x, m_storedPos.cur.y);
}
public int __626()
{
return G.__145(__35());
}
public int __627()
{
return G.__145(__36());
}
public override float __619()
{
if ( m_hud )
return G.INVALIDCOORD;
if ( m_autoZ.cur==false )
return m_z.cur;
float dy = m_elevator.cur;
if ( m_elevatorScaling )
dy *= m_scale;
if ( m_elevatorRotation==false || m_angle.cur==0 )
return __36() + dy;
Vec2 v;
v.x = 0.0f;
v.y = dy;
v.Rotate((float)m_angle.cur);
return __36() + v.y;
}
public float __34()
{
float scale;
if ( m_manualScale.cur==0.0f )
scale = __646(__619());
else
scale = m_manualScale.cur;
SceneCell cell = __635(__35(), __36());
if ( cell )
scale *= cell.__600();
return scale;
}
public float __628()
{
if ( m_manualZoom.cur==0.0f )
return __647(__619());
return m_manualZoom.cur;
}
public void __629()
{
__614(G.__147(__35()), G.__147(__36()));
}
public void Move(float x, float y)
{
__614(x, y);
m_lightMeshChanged = true;
}
public void __630(float x, float y)
{
m_local.cur.x = x;
m_local.cur.y = y;
m_lightMeshChanged = true;
}
public void Rotate(int degreePerSecond)
{
if ( degreePerSecond==0 )
m_rotating = false;
else
{
m_rotating = true;
m_rotateSpeed = (float)degreePerSecond;
m_rotateAngle = (float)m_angle.cur;
}
}
public bool __631(bool visible = true, bool resetLight = true)
{
if ( visible==m_lightVisible.cur )
return false;
m_lightVisible.Set(visible);
m_lightMeshChanged = true;
if ( resetLight )
m_scene.__565();
return true;
}
public void __632(ref string name)
{
__632(m_obj.__471(ref name));
}
public void __632(Anim anim)
{
m_anim.__688();
bool isSame = m_anim.cur==anim;
if ( m_anim.cur && m_anim.cur.m_sprites!=null && isSame==false )
{
for ( int i=0 ; i<m_anim.cur.m_sprites.Count ; i++ )
m_anim.cur.m_sprites[i].End();
}
m_anim.notif.__689();
if ( anim==null )
m_anim.Reset();
else
{
m_anim.cur = anim;
bool forceReload = m_anim.cur.m_sprites!=null && m_anim.cur.m_sprites.Count>0 && m_anim.cur.m_sprites[0].m_material==null;
if ( m_anim.cur.m_sprites!=null && (isSame==false || forceReload) )
{
Asset asset = G.__96(G.m_pathGraphics);
if ( asset )
{
for ( int i=0 ; i<m_anim.cur.m_sprites.Count ; i++ )
m_anim.cur.m_sprites[i].__469(asset);
asset.Close();
}
}
m_anim.__678(m_anim.iDir);
}
}
public void __633(SceneObj sceneObj)
{
if ( m_anim.cur==null || sceneObj==null )
return;
m_anim.__678(__35()>sceneObj.__35() ? AnimDir.LEFT : AnimDir.RIGHT);
}
public void __633(SceneLabel sceneLabel)
{
if ( m_anim.cur==null || sceneLabel==null )
return;
m_anim.__678(__35()>sceneLabel.__620() ? AnimDir.LEFT : AnimDir.RIGHT);
}
public bool __634(SceneObj sceneObj, int dir = -1)
{
if ( m_anim.cur==null || sceneObj==null )
return false;
switch ( m_anim.iDir )
{
case AnimDir.LEFT:
if ( (dir==-1 || dir==AnimDir.LEFT) && sceneObj.__626()<__626() )
return true;
break;
case AnimDir.RIGHT:
if ( (dir==-1 || dir==AnimDir.RIGHT) && sceneObj.__626()>__626() )
return true;
break;
}
return false;
}
public SceneCell __635(float x, float y)
{
if ( m_grids==null || m_grids[m_iGrid.cur]==null )
return null;
int col = G.__145(x);
int row = G.__145(y);
uint index = G.__99(col, row);
SceneCell cell;
if ( m_grids[m_iGrid.cur].m_mapCellByIndex.TryGetValue(index, out cell) )
return cell;
return null;
}
public SceneCell __636(int col, int row)
{
if ( m_grids==null || m_grids[m_iGrid.cur]==null )
return null;
uint index = G.__99(col, row);
SceneCell cell;
if ( m_grids[m_iGrid.cur].m_mapCellByIndex.TryGetValue(index, out cell) )
return cell;
return null;
}
public SceneCell __637(string name, bool objpos = false)
{
if ( m_grids==null || m_grids[m_iGrid.cur]==null )
return null;
SceneCell cell;
if ( m_grids[m_iGrid.cur].m_mapCellByName.TryGetValue(name, out cell) )
{
if ( objpos && (__626()!=cell.m_col || __627()!=cell.m_row) )
return null;
return cell;
}
return null;
}
public string __638(float x, float y)
{
if ( m_grids==null || m_grids[m_iGrid.cur]==null )
return "";
int col = G.__145(x);
int row = G.__145(y);
uint index = G.__99(col, row);
SceneCell cell;
if ( m_grids[m_iGrid.cur].m_mapEventCellByIndex.TryGetValue(index, out cell) )
return cell.__393();
return "";
}
public SceneCell __639(float x, float y, out bool exist)
{
if ( m_grids==null || m_grids[m_iGrid.cur]==null )
{
exist = false;
return null;
}
int col = G.__145(x);
int row = G.__145(y);
float closestDist = 0xFFFFFFF;
SceneCell closestCell = null;
for ( int i=0 ; i<m_grids[m_iGrid.cur].m_cells.Length ; i++ )
{
SceneCell cell = m_grids[m_iGrid.cur].m_cells[i];
if ( cell.__596()==false )
continue;
if ( cell.m_col==col && cell.m_row==row )
{
exist = true;
return cell;
}
float dist = G.__140(x, y, cell.__35(), cell.__36());
if ( dist<closestDist )
{
closestDist = dist;
closestCell = cell;
}
}
exist = false;
return closestCell;
}
public SceneCell __640(Scene scene)
{
if ( m_grids==null || m_grids[m_iGrid.cur]==null || scene==null )
return null;
SceneCell cell;
if ( m_grids[m_iGrid.cur].m_mapCellByEnterFromLink.TryGetValue(scene.m_uid, out cell) )
return cell;
if ( m_grids[m_iGrid.cur].m_mapCellByEnterFromLink.TryGetValue("", out cell) )
return cell;
return null;
}
public SceneCell __641(Scene scene)
{
if ( m_grids==null || m_grids[m_iGrid.cur]==null || scene==null )
return null;
SceneCell cell;
if ( m_grids[m_iGrid.cur].m_mapCellByEnterToLink.TryGetValue(scene.m_uid, out cell) )
return cell;
if ( m_grids[m_iGrid.cur].m_mapCellByEnterToLink.TryGetValue("", out cell) )
return cell;
return null;
}
public SceneCell __642(SceneObj sceneObj)
{
if ( m_grids==null || m_grids[m_iGrid.cur]==null )
return null;
SceneCell cell;
if ( m_grids[m_iGrid.cur].m_mapCellBySelectLink.TryGetValue(sceneObj.m_obj.m_uid, out cell) )
return cell;
return null;
}
public SceneCell __642(SceneLabel label)
{
if ( m_grids==null || m_grids[m_iGrid.cur]==null )
return null;
SceneCell cell;
if ( m_grids[m_iGrid.cur].m_mapCellByLabelLink.TryGetValue(label.m_name, out cell) )
return cell;
return null;
}
public SceneCell __642(SceneDoor door)
{
if ( m_grids==null || m_grids[m_iGrid.cur]==null )
return null;
SceneCell cell;
if ( m_grids[m_iGrid.cur].m_mapCellByDoorLink.TryGetValue(door.m_name, out cell) )
return cell;
return null;
}
public SceneCell __643(Obj obj, Obj cursor)
{
if ( m_grids==null || m_grids[m_iGrid.cur]==null )
return null;
SceneCell cell;
if ( m_grids[m_iGrid.cur].m_mapCellByUseLink.TryGetValue(cursor.m_uid+"+"+obj.m_uid, out cell) )
return cell;
return null;
}
public SceneCell __643(SceneLabel label, Obj cursor)
{
if ( m_grids==null || m_grids[m_iGrid.cur]==null )
return null;
SceneCell cell;
if ( m_grids[m_iGrid.cur].m_mapCellByUseLabelLink.TryGetValue(cursor.m_uid+"+"+label.m_name, out cell) )
return cell;
return null;
}
public SceneCell __644(Obj obj)
{
if ( m_grids==null || m_grids[m_iGrid.cur]==null )
return null;
SceneCell cell;
if ( m_grids[m_iGrid.cur].m_mapCellByDetachLink.TryGetValue(obj.m_uid, out cell) )
return cell;
return null;
}
public void __645(out float speedX, out float speedY)
{
speedX = m_speedX;
speedY = m_speedY;
SceneCell cell = __635(__35(), __36());
if ( cell )
{
speedX *= cell.__598();
speedY *= cell.__599();
}
}
public float __646(float z)
{
if ( m_depthScaleCount<2 )
return 1.0f;
float scale = 1.0f;
if ( z<=m_depthScaleYs[0] )
scale = m_depthScaleValues[0];
else if ( z>=m_depthScaleYs[m_depthScaleYs.Length-1] )
scale = m_depthScaleValues[m_depthScaleValues.Length-1];
else
{
for ( int i=0 ; i<m_depthScaleYs.Length-1 ; i++ )
{
float z1 = m_depthScaleYs[i];
float z2 = m_depthScaleYs[i+1];
if ( z>=z1 && z<=z2 )
{
z1 += G.PATH_GRID_CELLSIZE_HALF;
z2 -= G.PATH_GRID_CELLSIZE_HALF;
if ( z<z1 )
z = z1;
else if ( z>z2 )
z = z2;
float ratio = 0.5f;
if ( z1!=z2 )
ratio = (z-z1)/(z2-z1);
scale = G.__135(ratio, m_depthScaleValues[i], m_depthScaleValues[i+1]);
break;
}
}
}
return G.Clamp(scale, 0.0001f, 4.0f);
}
public float __647(float z)
{
if ( m_pathStarted.cur )
{
SpotPath path = m_paths[m_iPath.cur];
Spot spotA = path.m_spots[path.m_index];
Spot spotB = path.m_index+1>=path.m_spots.Length ? spotA : path.m_spots[path.m_index+1];
if ( spotA.m_zoom!=0.0f && spotB.m_zoom!=0.0f )
{
float len = G.__139(spotA.m_x, spotA.m_y, spotB.m_x, spotB.m_y);
if ( len==0.0f )
return spotA.m_zoom;
float remainingLen = G.__139(__35(), __36(), spotB.m_x, spotB.m_y);
float zoom = G.__135(1.0f-G.Clamp(remainingLen/len), spotA.m_zoom, spotB.m_zoom);
return G.Clamp(zoom, 1.0f, 4.0f);
}
}
if ( m_depthZoomCount>=2 )
{
float zoom = 1.0f;
if ( z<=m_depthZoomYs[0] )
zoom = m_depthZoomValues[0];
else if ( z>=m_depthZoomYs[m_depthZoomYs.Length-1] )
zoom = m_depthZoomValues[m_depthZoomValues.Length-1];
else
{
for ( int i=0 ; i<m_depthZoomYs.Length-1 ; i++ )
{
float z1 = m_depthZoomYs[i];
float z2 = m_depthZoomYs[i+1];
if ( z>=z1 && z<=z2 )
{
z1 += G.PATH_GRID_CELLSIZE_HALF;
z2 -= G.PATH_GRID_CELLSIZE_HALF;
if ( z<z1 )
z = z1;
else if ( z>z2 )
z = z2;
float ratio = 0.5f;
if ( z1!=z2 )
ratio = (z-z1)/(z2-z1);
zoom = G.__135(ratio, m_depthZoomValues[i], m_depthZoomValues[i+1]);
break;
}
}
}
return G.Clamp(zoom, 1.0f, 4.0f);
}
return 1.0f;
}
public void __648()
{
if ( m_parallax==1.0f || m_scene.m_renderScale==1.0f )
return;
m_scale /= m_scene.m_renderScale;
if ( m_parallax!=0.0f )
m_scale *= Mathf.Pow(m_scene.m_renderScale, m_parallax);
}
public bool __649(float x, float y, Message msg = null, bool locked = false)
{
if ( m_grids==null || m_grids[m_iGrid.cur]==null || m_grids[m_iGrid.cur].m_router==null )
{
__651();
return false;
}
Grid grid = m_grids[m_iGrid.cur];
if ( m_routeStatus==Router.FOUND && grid.m_router.__514(__35(), __36())==false )
return false;
bool existingCell;
SceneCell cell = __639(x, y, out existingCell);
if ( cell==null )
{
__651();
return false;
}
bool isWalking = __657();
__651(isWalking);
bool isSameCell = __626()==cell.m_col && __627()==cell.m_row;
if ( isSameCell==false && m_obj.m_tolerance>0 && cell.__594()==false && msg==null )
{
int tolerance = G.Clamp((int)((m_obj.m_tolerance+1)*m_scale), 0, m_obj.m_tolerance);
if ( Math.Abs(__626()-cell.m_col)<=tolerance && Math.Abs(__627()-cell.m_row)<=tolerance )
isSameCell = true;
}
m_routeTurn = false;
m_routeLocked = locked;
m_routeStraight = false;
if ( isSameCell )
m_routeStatus = Router.FOUND;
else
m_routeStatus = grid.m_router.__516(__35(), __36(), cell.__35(), cell.__36());
if ( m_routeStatus==Router.FOUND )
{
__654();
m_eventCell = "";
m_message = msg;
if ( isSameCell )
{
Stop();
return false;
}
else
{
grid.m_router.__517(__35(), __36(), 0.0f);
m_angleWalk = G.__141(__35(), __36(), grid.m_router.m_xTrg, grid.m_router.m_yTrg);
if ( m_angleWalk==-1.0f )
m_angleWalk = m_anim.__681();
Anim anim = m_obj.__471(ref m_defaultWalkAnim.cur);
if ( anim==null || anim.m_maxFrameCount==0 )
__632(ref m_defaultStopAnim.cur);
else if ( isWalking==false )
__632(ref m_defaultWalkAnim.cur);
AnimDir animDir = m_anim.__679();
if ( animDir )
{
int dirSrc = animDir.m_id;
int dirTrg = G.__153(m_angleWalk);
Turn turn = m_obj.__472(dirTrg, m_defaultStopAnim.cur);
if ( turn==null )
{
dirTrg = G.__152(m_angleWalk, dirSrc);
turn = m_obj.__472(dirTrg, m_defaultStopAnim.cur);
}
if ( turn && turn.m_anim.__475(dirSrc) )
{
m_routeTurn = true;
m_routeTurnAnim = m_anim.cur;
m_routeTurnDir = dirTrg;
__632(turn.m_anim);
}
}
if ( isWalking==false )
{
G.m_game.__323(RoleBoxEventWalk.ID, m_obj.m_uid, "ENTER");
__658();
}
}
}
else
{
__651();
return false;
}
return true;
}
public bool __650(float x, float y, float duration, bool easeIn, bool easeOut, Message msg, bool locked = false)
{
__654();
__651();
m_routeTurn = false;
m_routeLocked = locked;
m_routeStatus = Router.FOUND;
m_routeStraight = true;
m_routeStraightTime = 0.0f;
m_routeStraightDuration = duration;
m_routeStraightEaseIn = easeIn;
m_routeStraightEaseOut = easeOut;
m_routeStraightSrcX = __35();
m_routeStraightSrcY = __36();
m_routeStraightTrgX = x;
m_routeStraightTrgY = y;
m_routeStraightDistance = G.__139(m_routeStraightSrcX, m_routeStraightSrcY, m_routeStraightTrgX, m_routeStraightTrgY);
m_eventCell = "";
m_message = msg;
m_angleWalk = G.__141(m_routeStraightSrcX, m_routeStraightSrcY, m_routeStraightTrgX, m_routeStraightTrgY);
if ( m_angleWalk==-1.0f )
m_angleWalk = m_anim.__681();
__659(true);
return true;
}
public void Stop()
{
if ( m_routeStatus==0 )
return;
m_routeStatus = 0;
__632(ref m_defaultStopAnim.cur);
if ( m_message && m_message.m_state==Message.S_MOVE )
{
Scene scene = m_scene;
Message msg = m_message;
switch ( m_message.m_type )
{
case Message.SELECT:
if ( msg.m_anim==null )
{
m_message = null;
if ( msg.m_dir==-1 )
__633(msg.m_sceneObj);
else
m_anim.__678(msg.m_dir);
G.m_game.__325(msg.m_sceneObj.m_obj, msg.m_subObj, msg.m_byUser);
if ( msg.m_roleBox )
msg.m_roleBox.__458(msg.m_roleBoxToken);
}
else
{
msg.m_state++;
__632(msg.m_anim);
if ( msg.m_dir==-1 )
__633(msg.m_sceneObj);
else
m_anim.__678(msg.m_dir);
}
break;
case Message.USE:
if ( msg.m_anim==null )
{
m_message = null;
if ( msg.m_dir==-1 )
__633(msg.m_sceneObj);
else
m_anim.__678(msg.m_dir);
G.m_game.__326(msg.m_objA, msg.m_objB, msg.m_subObj, msg.m_bothFromInventory);
}
else
{
msg.m_state++;
__632(msg.m_anim);
if ( msg.m_dir==-1 )
__633(msg.m_sceneObj);
else
m_anim.__678(msg.m_dir);
}
break;
case Message.USELABEL:
if ( msg.m_anim==null )
{
m_message = null;
if ( msg.m_dir==-1 )
__633(msg.m_label);
else
m_anim.__678(msg.m_dir);
scene.__562(msg.m_objA, msg.m_label);
}
else
{
msg.m_state++;
__632(msg.m_anim);
if ( msg.m_dir==-1 )
__633(msg.m_label);
else
m_anim.__678(msg.m_dir);
}
break;
case Message.DETACH:
if ( msg.m_anim==null )
{
m_message = null;
if ( msg.m_dir!=-1 )
m_anim.__678(msg.m_dir);
G.m_game.__327(msg.m_objA);
}
else
{
msg.m_state++;
__632(msg.m_anim);
if ( msg.m_dir!=-1 )
m_anim.__678(msg.m_dir);
}
break;
case Message.LABEL:
if ( msg.m_anim==null )
{
m_message = null;
if ( msg.m_dir==-1 )
__633(msg.m_label);
else
m_anim.__678(msg.m_dir);
scene.__561(msg.m_label);
}
else
{
msg.m_state++;
__632(msg.m_anim);
if ( msg.m_dir==-1 )
__633(msg.m_label);
else
m_anim.__678(msg.m_dir);
}
break;
case Message.DOOR:
if ( msg.m_anim==null )
{
m_message = null;
if ( msg.m_dir!=-1 )
m_anim.__678(msg.m_dir);
scene.__563(msg.m_door);
}
else
{
msg.m_state++;
__632(msg.m_anim);
if ( msg.m_dir!=-1 )
m_anim.__678(msg.m_dir);
}
break;
case Message.WALK:
if ( msg.m_anim==null )
{
m_message = null;
if ( msg.m_dir!=-1 )
m_anim.__678(msg.m_dir);
else if ( msg.m_sceneObj )
__633(msg.m_sceneObj);
if ( msg.m_enter )
scene.__509("WALK", msg.m_enterCell);
if ( msg.m_roleBox )
msg.m_roleBox.__458(msg.m_roleBoxToken);
}
else
{
msg.m_state++;
__632(msg.m_anim);
if ( msg.m_dir!=-1 )
m_anim.__678(msg.m_dir);
else if ( msg.m_sceneObj )
__633(msg.m_sceneObj);
}
break;
}
}
}
public void __651(bool keepCurAnim = false)
{
m_routeStatus = 0;
m_message = null;
if ( keepCurAnim==false )
__632(ref m_defaultStopAnim.cur);
}
public bool __652(int index = -1, string spot = "", int loop = -2, RoleBox box = null)
{
if ( m_paths==null )
return false;
if ( index<0 || index>=G.PATH_GRID_COUNT )
index = m_iPath.cur;
SpotPath path = m_paths[index];
if ( path==null )
return false;
m_iPath.Set(index);
if ( loop<-1 )
loop = path.m_initLoop;
int iSpot = 0;
if ( spot.Length>0 )
iSpot = path.__674(spot);
__651();
path.Start(loop, iSpot);
Move(path.m_spots[path.m_index].m_x, path.m_spots[path.m_index].m_y);
m_pathRoleBox = box;
m_pathRoleBoxToken = box==null ? 0 : box.m_parent.m_token;
m_pathStarted.Set(true);
return true;
}
public void __653()
{
if ( m_paths==null )
return;
SpotPath path = m_paths[m_iPath.cur];
if ( path==null )
return;
path.Stop();
Move(path.m_spots[path.m_index].m_x, path.m_spots[path.m_index].m_y);
m_pathStarted.Set(false);
if ( m_pathRoleBox )
{
m_pathRoleBox.__458(m_pathRoleBoxToken);
m_pathRoleBox = null;
}
m_pathRoleBoxToken = 0;
}
public void __654()
{
if ( m_iSay==-1 )
return;
m_iSay = -1;
m_sayCount = 0;
m_say.Clear();
m_timeSay = 0.0f;
m_timeSayDuration = 0.0f;
m_timeSayDurationForced = 0.0f;
m_sayTyping = false;
m_timeSayTypingFinished = 0.0f;
if ( m_obj )
{
if ( m_sayKeepAnim )
{
if ( m_routeStatus==Router.FOUND )
__632(ref m_defaultWalkAnim.cur);
}
else
{
bool animLocked = false;
if ( m_routeStatus==Router.FOUND )
__632(ref m_defaultWalkAnim.cur);
else
{
if ( m_anim.cur==null || m_anim.cur.m_name!=m_defaultStopAnim.cur )
__632(ref m_defaultStopAnim.cur);
else
animLocked = true;
}
if ( m_sayAnimDirOld!=-1 && animLocked==false )
m_anim.__678(m_sayAnimDirOld);
}
}
G.m_game.__266();
}
public int Say(Sentence sentence, int indexParagraph, bool ignoreRandom = false)
{
if ( m_iSay!=-1 )
return 0;
if ( sentence.m_text==null )
return 0;
float maxWidth = 0.0f;
if ( m_obj==null || m_obj.m_speechMovie )
maxWidth = G.m_rcViewUI.width * G.SUBTITLE_WIDTH;
else if ( m_obj.m_avatar==null )
{
if ( G.m_game.m_uiBalloon==null )
maxWidth = G.m_rcViewUI.width * 0.5f;
else
maxWidth = G.m_rcViewUI.width * 0.3f;
}
else
maxWidth = m_obj.m_avatar.m_avatarText.width;
if ( m_obj )
{
AnimDir animDir = m_anim.__679();
m_sayAnimDirOld = animDir==null ? -1 : animDir.m_id;
m_sayHasAnimToWait = false;
m_sayKeepAnim = false;
Anim anim = null;
if ( sentence.m_anim.Length>0 )
{
anim = m_obj.__471(ref sentence.m_anim);
if ( anim && anim.m_maxFrameCount>0 && anim.m_loopCount!=-1 && anim.m_loopRangeCount!=-1 )
m_sayHasAnimToWait = true;
}
if ( anim==null || anim.m_maxFrameCount==0 )
anim = m_obj.__471(ref m_defaultTalkAnim.cur);
else
m_sayKeepAnim = sentence.m_keepAnim;
if ( anim==null || anim.m_maxFrameCount==0 )
{
if ( m_anim.cur==null || m_anim.cur.m_name!=m_defaultStopAnim.cur )
__632(ref m_defaultStopAnim.cur);
}
else
__632(ref anim.m_name);
SceneObj sceneObj = m_scene.__277(sentence.m_lookat);
if ( sceneObj )
__633(sceneObj);
else if ( sentence.m_iDir!=-1 )
m_anim.__678(sentence.m_iDir);
if ( sentence.m_keepDir && m_anim.iDir!=-1 )
m_sayAnimDirOld = m_anim.iDir;
}
string[] paragraphs = sentence.m_text.GetParagraphs();
if ( indexParagraph>=paragraphs.Length )
indexParagraph = 0;
if ( ignoreRandom==false && sentence.m_choice==false && sentence.m_randomly )
indexParagraph = G.__156(paragraphs.Length);
m_iSay = 0;
m_sayCount = 0;
m_timeSayDurationForced = sentence.m_duration;
G.__161(m_say, paragraphs[indexParagraph], G.m_game.__215(), maxWidth);
__655();
if ( sentence.m_voice==null )
G.m_game.__267("");
else
G.m_game.__267(sentence.m_voice.__63(indexParagraph));
return indexParagraph;
}
public void __655()
{
if ( m_iSay==-1 )
return;
G.m_game.m_menuDialog.m_timeLastAction = G.m_game.m_time;
m_iSay += m_sayCount;
if ( m_iSay>=m_say.__67() )
{
__654();
return;
}
int letterCount = 0;
int wordCount = 0;
m_sayCount = 0;
for ( int i=m_iSay ; i<m_say.__67() ; i++ )
{
if ( m_say.m_texts[i].Length==0 )
{
if ( m_sayCount==0 )
{
m_iSay++;
continue;
}
break;
}
m_sayCount++;
wordCount += m_say.m_words[i];
letterCount += m_say.m_texts[i].Length;
}
m_sayDrawnOnce = false;
m_sayPosition.x = __35();
m_sayPosition.y = m_scene.__554(m_draw.aabb.y-20.0f);
m_timeSay = 0.0f;
m_timeSayDuration = Localization.__433(G.m_game.__206().m_cjk ? letterCount : wordCount);
m_timeSayTypingFinished = 0.0f;
m_sayTyping = G.m_game.m_typewriter && G.m_game.m_optionSubtitle!=SUBTITLE.NONE;
}
public bool __656()
{
return m_iSay!=-1;
}
public bool __657()
{
return m_anim.cur && m_anim.cur.m_name==m_defaultWalkAnim.cur;
}
public void Update()
{
m_anim.__675();
if ( m_rotating )
{
m_rotateAngle += m_rotateSpeed * G.m_game.m_elapsed;
m_angle.cur = Mathf.RoundToInt(m_rotateAngle);
}
bool sayNext = false;
if ( __656() )
{
m_timeSay += G.m_game.m_elapsed;
if ( m_timeSay>=m_timeSayDurationForced && m_timeSay>=m_timeSayDuration && m_timeSayDuration!=-1.0f && m_sayTyping==false && (m_timeSayTypingFinished==0.0f || m_timeSay-m_timeSayTypingFinished>1.0f) )
{
if ( G.m_game.m_currentVoice==null || m_iSay+m_sayCount<m_say.__67() )
{
if ( m_sayHasAnimToWait )
sayNext = true;
else
__655();
}
}
}
__660();
if ( m_routeStatus==Router.FOUND && __656()==false )
{
if ( m_routeStraight )
__659();
else
__658();
}
if ( m_chaseObj && __656()==false && m_routeTurn==false )
{
int col = m_chaseObj.__626();
int row = m_chaseObj.__627();
if ( m_chaseObjCol!=col || m_chaseObjRow!=row )
{
m_chaseObjCol = col;
m_chaseObjRow = row;
float x = G.__146(m_chaseObj.__626()+m_chaseObjDeltaCol);
float y = G.__146(m_chaseObj.__627()+m_chaseObjDeltaRow);
Message msg = new Message();
msg.m_type = Message.WALK;
msg.m_sceneObj = m_chaseObj;
msg.m_state = Message.S_MOVE;
__649(x, y, msg, false);
}
}
if ( m_anim.__686(sayNext, out bool ended)==false )
return;
if ( m_routeTurn )
return;
if ( m_message && m_message.m_state==Message.S_ANIM && ended )
__668();
}
public void __658()
{
if ( m_routeTurn )
return;
Grid grid = m_grids[m_iGrid.cur];
float speedX, speedY;
__645(out speedX, out speedY);
m_scale = __34();
m_scale *= m_scene.m_renderScale;
__648();
float xCurTarget = grid.m_router.m_xTrg;
float yCurTarget = grid.m_router.m_yTrg;
float oldX = __35();
float oldY = __36();
float x = Mathf.Sin(m_angleWalk);
float y = Mathf.Cos(m_angleWalk);
float xabs = Mathf.Abs(x);
float yabs = Mathf.Abs(y);
float total = xabs + yabs;
float speed = (speedX*xabs/total+speedY*yabs/total) * m_scale;
float len = speed * G.m_game.m_elapsed;
if ( len>=G.PATH_GRID_CELLSIZE-0.1f )
len = G.PATH_GRID_CELLSIZE - 0.2f;
bool changed = grid.m_router.__517(__35(), __36(), len);
__615(x*len, y*len);
if ( len>0.0f )
m_lightMeshChanged = true;
m_scrollSmoothSpeed = xabs * (len/G.m_game.m_elapsed);
m_zoomSmoothSpeed = yabs * (len/G.m_game.m_elapsed);
if ( changed )
{
Move(xCurTarget, yCurTarget);
m_angleWalk = G.__141(__35(), __36(), grid.m_router.m_xTrg, grid.m_router.m_yTrg);
if ( m_angleWalk==-1.0f )
m_angleWalk = m_anim.__681();
}
bool isArrived = false;
if ( m_chaseObj==null || m_chaseObj.m_routeStatus==0 )
{
if ( grid.m_router.m_pathLocation==grid.m_router.m_pathLength && __626()==G.__145(grid.m_router.m_xTrg) && __627()==G.__145(grid.m_router.m_yTrg) )
isArrived = true;
else if ( m_message && m_message.m_dist>0 )
{
float dist = G.__140(__35(), __36(), grid.m_router.m_xFinalTrg, grid.m_router.m_yFinalTrg);
if ( dist<=m_message.m_dist )
__668();
}
}
if ( isArrived )
{
SceneCell cell = __636(__626(), __627());
if ( cell && cell.__595() )
{
__629();
m_lightMeshChanged = true;
}
Stop();
G.m_game.__323(RoleBoxEventWalk.ID, m_obj.m_uid, "EXIT");
}
else
{
SceneCell cell = __635(__35(), __36());
if ( cell && cell.__601() && m_anim.cur )
{
string name = cell.__602();
if ( name[0]=='*' )
{
if ( name=="*STOP" )
name = m_defaultStopAnim.cur;
else if ( name=="*WALK" )
name = m_defaultWalkAnim.cur;
else if ( name=="*TALK" )
name = m_defaultTalkAnim.cur;
else
name = name.Substring(1);
}
if ( name!=m_anim.cur.m_name )
__632(ref name);
}
float angle = m_angleWalk;
if ( angle!=-1.0f )
{
int dir = G.__153(angle);
if ( dir!=m_anim.iDir && m_anim.cur && m_anim.cur.__475(dir)==false )
dir = G.__152(angle, m_anim.iDir);
if ( dir!=m_anim.iDir && m_anim.cur.__475(dir) )
{
if ( cell==null || cell.__603(dir) )
m_anim.__678(dir);
}
}
}
if ( __662()==false )
{
__651();
Move(oldX, oldY);
m_angleWalk = m_anim.__681();
m_eventCell = "";
}
}
public void __659(bool init = false)
{
if ( init==false )
m_routeStraightTime += G.m_game.m_elapsed;
m_scale = __34();
m_scale *= m_scene.m_renderScale;
__648();
if ( m_routeStraightDuration==0.0f )
{
float oldX = __35();
float oldY = __36();
float remainingLen = G.__139(oldX, oldY, m_routeStraightTrgX, m_routeStraightTrgY);
float s = Mathf.Sin(m_angleWalk);
float c = Mathf.Cos(m_angleWalk);
float sabs = Mathf.Abs(s);
float cabs = Mathf.Abs(c);
float total = sabs + cabs;
float speed = (m_speedX*sabs/total+m_speedY*cabs/total) * m_scale;
float len = speed * G.m_game.m_elapsed;
if ( len>=G.PATH_GRID_CELLSIZE-0.1f )
len = G.PATH_GRID_CELLSIZE - 0.2f;
bool isArrived = false;
if ( G.__132(len, remainingLen) )
{
len = remainingLen;
isArrived = true;
}
m_scrollSmoothSpeed = sabs * (len/G.m_game.m_elapsed);
m_zoomSmoothSpeed = cabs * (len/G.m_game.m_elapsed);
if ( isArrived )
{
Move(m_routeStraightTrgX, m_routeStraightTrgY);
Stop();
m_lightMeshChanged = true;
}
else
{
__615(s*len, c*len);
if ( len>0.0f )
m_lightMeshChanged = true;
}
}
else
{
float ratio = G.Clamp(m_routeStraightTime/m_routeStraightDuration);
if ( m_routeStraightEaseIn || m_routeStraightEaseOut )
ratio = G.__138(ratio, 0.0f, 1.0f, m_routeStraightEaseIn, m_routeStraightEaseOut);
float len = ratio*m_routeStraightDistance;
float s = Mathf.Sin(m_angleWalk);
float c = Mathf.Cos(m_angleWalk);
float sabs = Mathf.Abs(s);
float cabs = Mathf.Abs(c);
float x = m_routeStraightSrcX + s*len;
float y = m_routeStraightSrcY + c*len;
float dlen = G.__139(__35(), __36(), x, y);
m_scrollSmoothSpeed = sabs * (dlen/G.m_game.m_elapsed);
m_zoomSmoothSpeed = cabs * (dlen/G.m_game.m_elapsed);
Move(x, y);
if ( ratio==1.0f )
Stop();
m_lightMeshChanged = true;
}
int dir = G.__153(m_angleWalk);
if ( dir!=m_anim.iDir && m_anim.cur && m_anim.cur.__475(dir)==false )
dir = G.__152(m_angleWalk, m_anim.iDir);
if ( dir!=m_anim.iDir && m_anim.cur.__475(dir) )
m_anim.__678(dir);
}
public void __660()
{
if ( m_paths==null || m_paths[m_iPath.cur]==null || m_pathStarted.cur==false )
return;
SpotPath path = m_paths[m_iPath.cur];
if ( m_obj==null || path.m_spots.Length<2 || path.m_index>=path.m_spots.Length || path.m_paused )
return;
if ( path.m_pauseDuration==-1.0f )
return;
else if ( path.m_pauseDuration>0.0f )
{
path.m_pauseTime += G.m_game.m_elapsed;
if ( path.m_pauseTime<path.m_pauseDuration )
return;
path.m_pauseDuration = 0.0f;
}
m_lightMeshChanged = true;
float offset = path.m_offset;
SplinePoint pt = new SplinePoint();
bool finished = path.m_spline.Next(ref offset, ref pt);
int nextIndex = pt.step.m_spot.m_index;
for ( int index=path.m_index ; index<=nextIndex ; index++ )
{
Spot spot = path.m_spots[index];
if ( index!=path.m_index )
{
path.m_index = index;
path.m_passed = false;
}
if ( index==nextIndex )
{
path.m_offset = offset;
__614(pt.x, pt.y);
m_scrollSmoothSpeed = pt.speed;
m_zoomSmoothSpeed = pt.speed;
}
else
{
path.m_offset = path.m_spline.m_steps[index].m_offset;
__614(spot.m_x, spot.m_y);
m_scrollSmoothSpeed = spot.m_speed;
m_zoomSmoothSpeed = spot.m_speed;
}
if ( __661(path, spot) )
return;
if ( finished && index==nextIndex )
{
path.m_indexLoop++;
if ( path.m_loop!=-1 && path.m_indexLoop>path.m_loop )
{
path.Stop();
m_pathStarted.Set(false);
__614(path.m_spline.m_pathEnd.x, path.m_spline.m_pathEnd.y);
m_scrollSmoothSpeed = path.m_spline.m_pathEnd.speed;
m_zoomSmoothSpeed = path.m_spline.m_pathEnd.speed;
if ( m_pathRoleBox )
{
m_pathRoleBox.__458(m_pathRoleBoxToken);
m_pathRoleBox = null;
}
m_pathRoleBoxToken = 0;
}
else
{
path.m_index = 0;
path.m_passed = false;
path.m_offset = 0.0f;
__614(path.m_spline.m_pathStart.x, path.m_spline.m_pathStart.y);
m_scrollSmoothSpeed = path.m_spline.m_pathStart.speed;
m_zoomSmoothSpeed = path.m_spline.m_pathStart.speed;
}
}
}
}
public bool __661(SpotPath path, Spot spot)
{
if ( path.m_passed )
return false;
path.m_passed = true;
bool quit = false;
if ( spot.m_pause!=0.0f )
{
path.m_pauseDuration = spot.m_pause;
path.m_pauseTime = 0.0f;
quit = true;
}
if ( spot.m_name!="SPOT" )
{
G.m_game.__323(RoleBoxEventSpot.ID, m_obj.m_uid, spot.m_name);
if ( m_pathStarted.cur==false )
return true;
}
if ( path.m_paused )
return true;
if ( quit )
return true;
return false;
}
public bool __662()
{
string oldEventCell = m_eventCell;
m_eventCell = __638(__35(), __36());
if ( m_eventCell==oldEventCell )
return true;
if ( oldEventCell.Length>0 )
{
G.m_game.__323(RoleBoxEventCell.ID, m_obj.m_uid, oldEventCell, "OUT");
}
bool revert = false;
if ( m_eventCell.Length>0 )
{
Variable var = new Variable();
G.m_game.__323(RoleBoxEventCell.ID, m_obj.m_uid, m_eventCell, "IN", var);
if ( var.m_value!="0" )
revert = true;
}
return !revert;
}
public override void __605()
{
if ( m_obj==null )
return;
Frame frame = m_anim.__683();
Rect rc = frame==null ? Rect.Zero : frame.m_rcTrim;
float xView = m_scene.__549(__35(), m_hud);
float yView = m_scene.__550(__36(), m_hud);
m_scale = __34();
if ( m_hud==false )
{
m_scale *= m_scene.m_renderScale;
__648();
}
m_draw.view.x = xView + (rc.x-m_obj.m_anchorX.cur)*m_scale;
m_draw.view.y = yView + (rc.y-m_obj.m_anchorY.cur)*m_scale;
m_draw.view.width = rc.width*m_scale;
m_draw.view.height = rc.height*m_scale;
m_draw.obb.Set(ref m_draw.view);
if ( m_angle.cur==0 )
{
m_draw.aabb = m_draw.view;
m_draw.hasObb = false;
}
else
{
m_draw.obb.Move(-xView, -yView);
m_draw.obb.Rotate((float)m_angle.cur);
m_draw.obb.Move(xView, yView);
m_draw.aabb.Set(ref m_draw.obb);
m_draw.hasObb = true;
}
}
public void __663(out DrawInfo info)
{
info = m_draw;
Frame frame = m_anim.__683();
Rect rc = frame==null ? Rect.Zero : frame.m_rcTrim;
float scaleOld = m_scale;
m_scale = __34();
if ( m_hud==false )
{
m_scale *= m_scene.m_renderScale;
__648();
}
m_draw.view.x = G.m_game.m_cursorViewX + (rc.x-m_obj.m_anchorX.cur)*m_scale;
m_draw.view.y = G.m_game.m_cursorViewY + (rc.y-m_obj.m_anchorY.cur)*m_scale;
m_draw.view.width = rc.width*m_scale;
m_draw.view.height = rc.height*m_scale;
m_draw.obb.Set(ref m_draw.view);
m_draw.obb.Move(-G.m_game.m_cursorViewX, -G.m_game.m_cursorViewY);
m_draw.obb.Rotate((float)m_angle.cur);
m_draw.obb.Move(G.m_game.m_cursorViewX, G.m_game.m_cursorViewY);
m_draw.hasObb = true;
m_draw.aabb.Set(ref m_draw.obb);
m_scale = scaleOld;
}
public void __664(ref DrawInfo info)
{
m_draw = info;
}
public Vec2 __665()
{
if ( m_draw.hasObb )
return m_draw.obb.__439();
return m_draw.aabb.__439();
}
public bool Contains(float x, float y)
{
if ( m_draw.hasObb )
return m_draw.obb.Contains(x, y);
return m_draw.aabb.Contains(x, y);
}
public bool __666(float x, float y)
{
if ( m_obj.m_bboxDetection )
return true;
Frame frame = m_anim.__683();
if ( frame==null )
return false;
if ( frame.m_mask==null )
return true;
__667(ref x, ref y, frame);
return __666(frame, x, y);
}
public bool __666(Frame frame, float xLocal, float yLocal)
{
if ( m_obj.m_bboxDetection )
return true;
if ( frame==null )
return false;
if ( frame.m_mask==null )
return true;
int index = (int)yLocal*(int)frame.m_rcTrim.width + (int)xLocal;
int index8 = index/8;
if ( index<0 || index8>=frame.m_mask.m_buffer.Length )
return false;
byte pack = frame.m_mask.m_buffer[index8];
if ( (pack & (0x01<<(7-index%8)))==0 )
return false;
return true;
}
public void __667(ref float x, ref float y, Frame frame)
{
if ( m_angle.cur==0 )
{
x = (x-m_draw.view.x)/m_scale;
y = (y-m_draw.view.y)/m_scale;
return;
}
Vec2 v;
v.x = (x-m_draw.view.x)/m_scale;
v.y = (y-m_draw.view.y)/m_scale;
float dx = m_obj.m_anchorX.cur - frame.m_rcTrim.x;
float dy = m_obj.m_anchorY.cur - frame.m_rcTrim.y;
v.x -= dx;
v.y -= dy;
v.Rotate(-(float)m_angle.cur);
x = v.x + dx;
y = v.y + dy;
}
public void __668()
{
if ( m_message==null )
return;
Scene scene = m_scene;
int type = m_message.m_type;
bool byUser = m_message.m_byUser;
SceneObj sceneObj = m_message.m_sceneObj;
SubObj subObj = m_message.m_subObj;
Obj objA = m_message.m_objA;
Obj objB = m_message.m_objB;
bool bothFromInventory = m_message.m_bothFromInventory;
SceneLabel label = m_message.m_label;
SceneDoor door = m_message.m_door;
bool enter = m_message.m_enter;
string enterCell = m_message.m_enterCell;
RoleBox roleBox = m_message.m_roleBox;
int roleBoxToken = m_message.m_roleBoxToken;
m_message = null;
switch ( type )
{
case Message.SELECT:
G.m_game.__325(sceneObj.m_obj, subObj, byUser);
if ( roleBox )
roleBox.__458(roleBoxToken);
break;
case Message.USE:
G.m_game.__326(objA, objB, subObj, bothFromInventory);
break;
case Message.USELABEL:
scene.__562(objA, label);
break;
case Message.DETACH:
G.m_game.__327(objA);
break;
case Message.LABEL:
scene.__561(label);
break;
case Message.DOOR:
scene.__563(door);
break;
case Message.WALK:
if ( enter )
scene.__509("WALK", enterCell);
if ( roleBox )
roleBox.__458(roleBoxToken);
break;
}
}
public override void __43()
{
if ( m_visible.cur==false )
return;
Frame frame = m_anim.__683();
if ( frame && frame.m_sprite && m_scene.__559(ref m_draw.aabb)==false )
{
frame.m_sprite.__996();
G.m_graphics.__362(this, frame);
}
#if UNITY_EDITOR
#endif
}
public void __589(bool balloon = false)
{
if ( m_iSay==-1 || G.m_game.m_optionSubtitle==SUBTITLE.NONE || G.m_game.m_userSubtitleVisible==false )
return;
int count = 0;
for ( int i=0 ; i<m_sayCount ; i++ )
{
int index = m_iSay + i;
if ( m_say.m_paraSizes[index].y==0 )
continue;
count++;
}
if ( count==0 )
return;
if ( m_obj==null || m_obj.m_speechMovie )
{
if ( balloon==false )
__669(count);
}
else if ( m_obj.m_avatar==null )
{
__670(count, balloon);
}
else
{
if ( balloon==false )
__671(count);
}
}
public void __669(int count)
{
Police font = G.m_game.__215();
bool leftToRight = font.__490();
string[] lines = new string[count];
Vec2[] positions = new Vec2[count];
float sayX = G.m_rcViewUI.x + G.m_rcViewUI.width*0.5f;
float sayY = G.m_rcViewUI.__438() - G.m_game.m_subtitleMargin;
for ( int i=0,j=0 ; i<m_sayCount ; i++ )
{
int index = m_iSay + i;
if ( m_say.m_paraSizes[index].y==0 )
continue;
float x = sayX;
if ( leftToRight )
{
x -= m_say.m_paraSizes[index].x*0.5f;
x += m_say.m_lineRects[index].x;
}
else
{
x += m_say.m_paraSizes[index].x*0.5f;
x -= m_say.m_paraSizes[index].x - m_say.m_lineRects[index].__437();
}
float y = sayY;
y -= m_say.m_paraSizes[index].y;
y += m_say.m_lineRects[index].y;
lines[j] = m_say.m_texts[index];
positions[j] = new Vec2(x, y);
j++;
}
int maxCharCount = -1;
if ( m_sayTyping )
maxCharCount = Mathf.CeilToInt(m_timeSay*50.0f);
Color color = m_obj==null ? (G.m_game.m_colorVoiceOver.a==0.0f ? G.m_game.m_colorText : G.m_game.m_colorVoiceOver) : m_obj.m_speech;
FontDrawInfo info = new FontDrawInfo(lines, positions, ref color);
info.maxCharCount = maxCharCount;
info.background = true;
bool finished = font.__71(ref info);
if ( m_sayTyping && finished )
{
m_sayTyping = false;
m_timeSayTypingFinished = m_timeSay;
}
}
public void __670(int count, bool balloon)
{
Police font = G.m_game.__215();
bool leftToRight = font.__490();
string[] lines = new string[count];
Vec2[] positions = new Vec2[count];
float margin = font.m_spaceWidth;
float vmargin = font.__447() * 0.5f;
float sayX = m_scene.__549(m_sayPosition.x);
float sayY = m_scene.__550(m_sayPosition.y);
if ( sayY>G.m_rcViewUI.__438()-vmargin )
sayY = G.m_rcViewUI.__438() - vmargin;
if ( m_sayDrawnOnce==false && m_scene.__479(CAMERA.TRACK)==null )
{
float paraDocLeft = (float)m_scene.m_width;
float paraDocRight = 0.0f;
for ( int i=0 ; i<m_sayCount ; i++ )
{
int iSay = m_iSay + i;
if ( m_say.m_paraSizes[iSay].y!=0.0f )
{
float x = m_scene.__553(sayX - m_say.m_paraSizes[iSay].x*0.5f + m_say.m_lineRects[iSay].x);
if ( x<paraDocLeft )
paraDocLeft = x;
x = m_scene.__553(sayX - m_say.m_paraSizes[iSay].x*0.5f + m_say.m_lineRects[iSay].__437());
if ( x>paraDocRight )
paraDocRight = x;
}
}
float delta = m_scene.__555(G.m_rcViewUI.width)*0.5f;
m_sayDrawnOnce = true;
if ( m_scene.__549(paraDocLeft)<G.m_rcViewUI.x )
{
Cam cam = m_scene.__572(CAMERA.TALK);
cam.m_fromX = m_scene.m_renderX;
cam.m_toX = m_scene.__557(paraDocLeft+delta);
cam.m_fromY = m_scene.m_renderY;
cam.m_toY = m_scene.m_renderY;
cam.m_fromScale = m_scene.m_renderScale;
cam.m_toScale = m_scene.m_renderScale;
cam.m_duration = 0.5f;
}
else if ( m_scene.__549(paraDocRight)>G.m_rcViewUI.__437() )
{
Cam cam = m_scene.__572(CAMERA.TALK);
cam.m_fromX = m_scene.m_renderX;
cam.m_toX = m_scene.__557(paraDocRight-delta);
cam.m_fromY = m_scene.m_renderY;
cam.m_toY = m_scene.m_renderY;
cam.m_fromScale = m_scene.m_renderScale;
cam.m_toScale = m_scene.m_renderScale;
cam.m_duration = 0.5f;
}
}
float startY = sayY;
if ( sayY-m_say.m_paraSizes[m_iSay].y<G.m_rcViewUI.y )
startY += G.m_rcViewUI.y - (sayY-m_say.m_paraSizes[m_iSay].y);
float dxleft = 0.0f;
float dxright = 0.0f;
for ( int i=0 ; i<m_sayCount ; i++ )
{
int iSay = m_iSay + i;
if ( m_say.m_paraSizes[iSay].y==0 )
continue;
float x = sayX - m_say.m_paraSizes[iSay].x*0.5f + m_say.m_lineRects[iSay].x;
if ( x<margin )
{
x = margin - x;
if ( x>dxleft )
dxleft = x;
}
else if ( x+m_say.m_lineRects[iSay].width>G.m_rcViewUI.__437()-margin )
{
x = (x+m_say.m_lineRects[iSay].width)-(G.m_rcViewUI.__437()-margin);
if ( x>dxright )
dxright = x;
}
}
float paraLeft = 10000.0f;
float paraTop = 10000.0f;
float paraRight = -10000.0f;
float paraBottom = -10000.0f;
for ( int i=0,index=0 ; i<m_sayCount ; i++ )
{
int iSay = m_iSay + i;
if ( m_say.m_paraSizes[iSay].y==0 )
continue;
float x = sayX;
if ( leftToRight )
{
x -= m_say.m_paraSizes[iSay].x * 0.5f;
x += m_say.m_lineRects[iSay].x;
x += dxleft;
x -= dxright;
if ( x<paraLeft )
paraLeft = x;
if ( x+m_say.m_lineRects[iSay].width>paraRight )
paraRight = x + m_say.m_lineRects[iSay].width;
}
else
{
x += m_say.m_paraSizes[iSay].x * 0.5f;
x -= m_say.m_lineRects[iSay].x;
x += dxleft;
x -= dxright;
if ( x-m_say.m_lineRects[iSay].width<paraLeft )
paraLeft = x - m_say.m_lineRects[iSay].width;
if ( x>paraRight )
paraRight = x;
}
float y = startY;
y -= m_say.m_paraSizes[iSay].y;
y += m_say.m_lineRects[iSay].y;
if ( y<paraTop )
paraTop = y;
if ( y+m_say.m_lineRects[iSay].height>paraBottom )
paraBottom = y + m_say.m_lineRects[iSay].height;
lines[index] = m_say.m_texts[iSay];
positions[index] = new Vec2(x, y);
index++;
}
if ( balloon )
{
Rect rc = Rect.Zero;
Rect rcUV = Rect.Zero;
Margin marginImage = G.m_game.m_balloonImageMargin;
Margin marginImage01 = G.m_game.m_balloonImageMargin01;
Margin marginText = G.m_game.m_balloonTextMargin;
bool flip = m_anim.__682();
if ( flip )
{
marginImage.__448();
marginText.__448();
}
Rect rcText = new Rect(paraLeft, paraTop, paraRight-paraLeft, paraBottom-paraTop);
if ( rcText.width<marginImage.__446() )
{
float dx = (marginImage.__446()-rcText.width)*0.5f;
rcText.x -= dx;
rcText.width = marginImage.__446();
}
if ( rcText.height<marginImage.__447() )
{
float dy = (marginImage.__447()-rcText.height)*0.5f;
rcText.y -= dy;
rcText.height = marginImage.__447();
}
Rect rcBalloon = rcText;
rcBalloon.x -= marginText.left;
rcBalloon.y -= marginText.top;
rcBalloon.width += marginText.__446();
rcBalloon.height += marginText.__447();
rcUV.x = flip ? 1.0f-marginImage01.right : 0.0f;
rcUV.width = flip ? marginImage01.right : marginImage01.left;
rcUV.height = marginImage01.top;
rc.x = rcBalloon.x;
rc.y = rcBalloon.y;
rc.width = marginImage.left;
rc.height = marginImage.top;
G.m_graphics.__358(G.m_game.m_uiBalloon, ref rcUV, ref rc, flip);
rcUV.x = flip ? 0.0f : 1.0f-marginImage01.right;
rcUV.width = flip ? marginImage01.left : marginImage01.right;
rc.x = rcBalloon.__437() - marginImage.right;
rc.width = marginImage.right;
G.m_graphics.__358(G.m_game.m_uiBalloon, ref rcUV, ref rc, flip);
rcUV.x = flip ? 1.0f-marginImage01.right : 0.0f;
rcUV.y = 1.0f - marginImage01.bottom;
rcUV.width = flip ? marginImage01.right : marginImage01.left;
rcUV.height = marginImage01.bottom;
rc.x = rcBalloon.x;
rc.y = rcBalloon.__438() - marginImage.bottom;
rc.width = marginImage.left;
rc.height = marginImage.bottom;
G.m_graphics.__358(G.m_game.m_uiBalloon, ref rcUV, ref rc, flip);
rcUV.x = flip ? 0.0f : 1.0f-marginImage01.right;
rcUV.width = flip ? marginImage01.left : marginImage01.right;
rc.x = rcBalloon.__437() - marginImage.right;
rc.width = marginImage.right;
G.m_graphics.__358(G.m_game.m_uiBalloon, ref rcUV, ref rc, flip);
rcUV.x = marginImage01.left;
rcUV.y = 0.0f;
rcUV.width = 1.0f - marginImage01.__446();
rcUV.height = marginImage01.top;
rc.x = rcBalloon.x + marginImage.left;
rc.y = rcBalloon.y;
rc.width = rcBalloon.width - marginImage.__446();
rc.height = marginImage.top;
G.m_graphics.__358(G.m_game.m_uiBalloon, ref rcUV, ref rc, flip);
rcUV.y = 1.0f - marginImage01.bottom;
rcUV.height = marginImage01.bottom;
rc.y = rcBalloon.__438() - marginImage.bottom;
rc.height = marginImage.bottom;
G.m_graphics.__358(G.m_game.m_uiBalloon, ref rcUV, ref rc, flip);
rcUV.x = flip ? 1.0f-marginImage01.right : 0.0f;
rcUV.y = marginImage01.top;
rcUV.width = flip ? marginImage01.right : marginImage01.left;
rcUV.height = 1.0f - marginImage01.__447();
rc.x = rcBalloon.x;
rc.y = rcBalloon.y + marginImage.top;
rc.width = marginImage.left;
rc.height = rcBalloon.height - marginImage.__447();
G.m_graphics.__358(G.m_game.m_uiBalloon, ref rcUV, ref rc, flip);
rcUV.x = flip ? 0.0f : 1.0f-marginImage01.right;
rcUV.width = flip ? marginImage01.left : marginImage01.right;
rc.x = rcBalloon.__437() - marginImage.right;
rc.width = marginImage.right;
G.m_graphics.__358(G.m_game.m_uiBalloon, ref rcUV, ref rc, flip);
rcUV.x = marginImage01.left;
rcUV.y = marginImage01.top;
rcUV.width = 1.0f - marginImage01.__446();
rcUV.height = 1.0f - marginImage01.__447();
rc.x = rcBalloon.x + marginImage.left;
rc.y = rcBalloon.y  + marginImage.top;
rc.width = rcBalloon.width - marginImage.__446();
rc.height = rcBalloon.height - marginImage.__447();
G.m_graphics.__358(G.m_game.m_uiBalloon, ref rcUV, ref rc, flip);
return;
}
int maxCharCount = -1;
if ( m_sayTyping )
maxCharCount = Mathf.CeilToInt(m_timeSay*50.0f);
FontDrawInfo info = new FontDrawInfo(lines, positions, ref m_obj.m_speech);
info.maxCharCount = maxCharCount;
info.background = true;
bool finished = font.__71(ref info);
if ( m_sayTyping && finished )
{
m_sayTyping = false;
m_timeSayTypingFinished = m_timeSay;
}
}
public void __671(int count)
{
Police font = G.m_game.__215();
bool leftToRight = font.__490();
string[] lines = new string[count];
Vec2[] positions = new Vec2[count];
Rect rcAvatar = m_obj.m_avatar.m_avatarText;
int avatarAlign = m_obj.m_avatar.m_avatarTextAlign;
float fontHeight = font.__447();
float lineSpacing = font.__491();
float height = 0.0f;
for ( int i=0,index=0 ; i<m_sayCount ; i++ )
{
int iSay = m_iSay + i;
if ( m_say.m_paraSizes[iSay].y==0 )
continue;
if ( index>0 )
height += lineSpacing;
if ( iSay>0 && m_say.m_paraSizes[iSay-1].y==0.0f && m_say.m_paraSizes[iSay].y==0.0f )
height += fontHeight;
else
height += m_say.m_lineRects[iSay].height;
index++;
}
float y = rcAvatar.y;
if ( G.__102(avatarAlign, (int)ALIGN.MIDDLE) )
y = rcAvatar.__439().y - height*0.5f;
else if ( G.__102(avatarAlign, (int)ALIGN.BOTTOM) )
y = rcAvatar.__438() - height;
float x;
ALIGN halign = ALIGN.LEFT;
if ( G.__102(avatarAlign, (int)ALIGN.JUSTIFY) )
halign = ALIGN.JUSTIFY;
else if ( G.__102(avatarAlign, (int)ALIGN.CENTER) )
halign = ALIGN.CENTER;
else if ( G.__102(avatarAlign, (int)ALIGN.RIGHT) )
halign = ALIGN.RIGHT;
for ( int i=0,index=0 ; i<m_sayCount ; i++ )
{
int iSay = m_iSay + i;
if ( m_say.m_paraSizes[iSay].y==0 )
continue;
switch ( halign )
{
case ALIGN.LEFT:
case ALIGN.JUSTIFY:
x = leftToRight ? rcAvatar.x : rcAvatar.x+m_say.m_lineRects[iSay].width;
break;
case ALIGN.RIGHT:
x = leftToRight ? rcAvatar.__437()-m_say.m_lineRects[iSay].width : rcAvatar.__437();
break;
default:
x = leftToRight ?  rcAvatar.__440()-m_say.m_lineRects[iSay].width*0.5f : rcAvatar.__440()+m_say.m_lineRects[iSay].width*0.5f;
break;
}
lines[index] = m_say.m_texts[iSay];
positions[index] = new Vec2(x, y);
if ( index>0 )
y += lineSpacing;
if ( iSay>0 && m_say.m_paraSizes[iSay-1].y==0.0f && m_say.m_paraSizes[iSay].y==0.0f )
y += fontHeight;
else
y += m_say.m_lineRects[iSay].height;
index++;
}
int maxCharCount = -1;
if ( m_sayTyping )
maxCharCount = Mathf.CeilToInt(m_timeSay*50.0f);
FontDrawInfo info = new FontDrawInfo(lines, positions, ref m_obj.m_speech);
info.maxCharCount = maxCharCount;
info.background = true;
if ( halign==ALIGN.JUSTIFY )
{
info.justify = true;
info.maxRowWidth = m_say.m_maxRowWidth;
info.lineRects = m_say.m_lineRects;
}
bool finished = font.__71(ref info);
if ( m_sayTyping && finished )
{
m_sayTyping = false;
m_timeSayTypingFinished = m_timeSay;
}
}
}
public class SpotPath
{
public Spot[] m_spots;
public Spline m_spline;
public int m_index;
public bool m_passed;
public float m_offset;
public float m_pauseTime;
public float m_pauseDuration;
public bool m_paused;
public int m_initLoop;
public int m_loop;
public int m_indexLoop;
public static implicit operator bool(SpotPath inst) { return inst!=null; }
public void Start(int loop, int index = 0)
{
m_index = index;
m_passed = false;
m_offset = 0.0f;
m_pauseDuration = 0.0f;
m_paused = false;
m_loop = loop;
m_indexLoop = 0;
}
public void Stop()
{
m_index = 0;
m_passed = false;
m_offset = 0.0f;
m_pauseDuration = 0.0f;
m_paused = false;
m_loop = m_initLoop;
m_indexLoop = 0;
}
public void Pause()
{
m_paused = true;
}
public void __672()
{
m_paused = false;
}
public void __673()
{
if ( m_pauseDuration==-1.0f )
m_pauseDuration = 0.0f;
}
public int __674(string name)
{
if ( name.Length==0 )
return -1;
for ( int i=0 ; i<m_spots.Length ; i++ )
{
if ( G.__148(ref m_spots[i].m_name, ref name) )
return i;
}
return -1;
}
}
public class Spot
{
public int m_index;
public string m_name;
public float m_x;
public float m_y;
public float m_speed;
public float m_pause;
public float m_zoom;
public static implicit operator bool(Spot inst) { return inst!=null; }
}
public class Grid
{
public SceneCell[] m_cells;
public Dictionary<uint, SceneCell> m_mapCellByIndex;
public Dictionary<string, SceneCell> m_mapCellByName;
public Dictionary<string, SceneCell> m_mapCellByEnterFromLink;
public Dictionary<string, SceneCell> m_mapCellByEnterToLink;
public Dictionary<string, SceneCell> m_mapCellBySelectLink;
public Dictionary<string, SceneCell> m_mapCellByLabelLink;
public Dictionary<string, SceneCell> m_mapCellByDoorLink;
public Dictionary<string, SceneCell> m_mapCellByUseLink;
public Dictionary<string, SceneCell> m_mapCellByUseLabelLink;
public Dictionary<string, SceneCell> m_mapCellByDetachLink;
public Dictionary<uint, SceneCell> m_mapEventCellByIndex;
public Router m_router = null;
public Router m_routerHud = null;
public static implicit operator bool(Grid inst) { return inst!=null; }
}
public struct DrawInfo
{
public Rect view;
public Rect aabb;
public Obb obb;
public bool hasObb;
public void Reset()
{
view.Reset();
obb.Reset();
aabb.Reset();
hasObb = false;
}
}
public struct AnimInfo
{
public SceneObj sceneObj;
public Anim cur;
public int iDir;
public bool reverse;
public int iFrame;
public int iFrameBeforeUpdate;
public int iFrameAfterUpdate;
public float timeFrame;
public int iLoop;
public int iLoopRange;
public int[] randomFrames;
public int[] profileFrames;
public int profileFrameIndex;
public bool[] passedFrames;
public AnimNotif notif;
public void Reset()
{
cur = null;
iDir = AnimDir.RIGHT;
reverse = false;
iFrame = 0;
timeFrame = 0.0f;
iLoop = 0;
iLoopRange = 0;
profileFrameIndex = 0;
if ( profileFrames!=null )
{
profileFrames[0] = -1;
profileFrameIndex = -1;
__687();
}
__675();
notif.Reset();
}
public void __675()
{
if ( passedFrames!=null )
{
for ( int i=0 ; i<passedFrames.Length ; i++ )
passedFrames[i] = false;
}
}
public string __393()
{
return cur==null ? "" : cur.m_name;
}
public void __676(bool reversed)
{
int lastFrame = G.__133(cur.m_dirs[iDir].__476()-1);
reverse = reversed;
iFrame = reverse ? lastFrame : 0;
__675();
}
public void __677(int startFrame)
{
int lastFrame = G.__133(cur.m_dirs[iDir].__476()-1);
if ( startFrame<0 || startFrame>lastFrame )
iFrame = 0;
else
iFrame = startFrame;
__675();
}
public void __678(int dir)
{
if ( cur==null || dir==-1 )
return;
__688();
if ( cur.__475(dir) )
iDir = dir;
else if ( cur.__475(iDir)==false )
iDir = cur.m_defaultDir.m_id;
iFrame = 0;
timeFrame = 0.0f;
iLoop = 0;
iLoopRange = 0;
__684();
if ( profileFrames!=null )
{
profileFrames[0] = -1;
profileFrameIndex = -1;
__687();
__675();
}
else
{
__676(cur.m_reversed);
}
}
public AnimDir __679()
{
if ( cur==null )
return null;
if ( iDir==-1 )
return null;
return cur.m_dirs[iDir];
}
public string __680()
{
if ( cur==null )
return "";
if ( iDir==-1 )
return "";
return cur.m_dirs[iDir].__393();
}
public float __681()
{
if ( cur==null )
return 0.0f;
switch ( iDir )
{
case AnimDir.LEFT:		return G.RAD_270;
case AnimDir.RIGHT:		return G.RAD_90;
case AnimDir.FRONT:		return 0.0f;
case AnimDir.BACK:		return G.RAD_180;
case AnimDir.FL:		return G.RAD_315;
case AnimDir.FR:		return G.RAD_45;
case AnimDir.BL:		return G.RAD_225;
case AnimDir.BR:		return G.RAD_135;
}
return 0.0f;
}
public bool __682()
{
if ( cur==null )
return false;
switch ( iDir )
{
case AnimDir.LEFT:
case AnimDir.FL:
case AnimDir.BL:
return true;
}
return false;
}
public Frame __683()
{
if ( cur==null )
return null;
AnimDir dir = __679();
if ( dir==null )
return null;
if ( iFrame<0 || iFrame>=dir.__476() )
return null;
if ( cur.m_random==RANDOM.NONE )
return dir.m_frames[iFrame];
return dir.m_frames[randomFrames[iFrame]];
}
public void __684()
{
int frameCount = 0;
if ( iDir!=-1 )
frameCount = cur.m_dirs[iDir].__476();
switch ( cur.m_random )
{
case RANDOM.ANIM:
case RANDOM.LOOP:
{
List<int> list = new List<int>();
for ( int i=0 ; i<frameCount ; i++ )
list.Add(i);
for ( int i=0 ; i<frameCount ; i++ )
{
int index = G.__156(list.Count);
randomFrames[i] = list[index];
}
break;
}
case RANDOM.FRAME:
{
if ( iFrame>=0 && iFrame<randomFrames.Length )
randomFrames[iFrame] = G.__156(frameCount);
break;
}
}
}
public bool __685(int index)
{
if ( cur==null )
return false;
AnimDir dir = __679();
if ( dir==null )
return false;
if ( index<0 || index>=dir.__476() )
return iFrame<0 || iFrame>=dir.__476();
if ( cur.m_random!=RANDOM.NONE )
index = dir.m_frames[randomFrames[index]].m_index;
if ( index==iFrame )
return true;
return passedFrames[index];
}
public bool __686(bool sayNext, out bool ended)
{
ended = false;
if ( cur==null )
return false;
AnimDir dir = __679();
if ( dir==null )
return false;
int actionFrame = notif.actionFrame==-2 ? cur.m_actionFrame : notif.actionFrame;
int frameCountToAdd = 0;
timeFrame += G.m_game.m_elapsed;
while ( timeFrame>=cur.m_fpsInv )
{
timeFrame -= cur.m_fpsInv;
frameCountToAdd++;
}
if ( frameCountToAdd==0 )
return true;
__688();
if ( cur.m_profile!=PROFILE.NONE )
{
__687();
return true;
}
bool animFinished = false;
bool actionFrameReached = false;
for ( int it=0 ; it<frameCountToAdd ; it++ )
{
int frame;
if ( reverse )
{
iFrame--;
frame = iFrame;
if ( iFrame>=0 )
passedFrames[iFrame] = true;
if ( cur.m_random==RANDOM.NONE && iLoopRange!=-1 )
{
int loopRangeMax = cur.m_loopRangeMax==-1 ? dir.__476()-1 : cur.m_loopRangeMax;
if ( iFrame<cur.m_loopRangeMin )
{
if ( cur.m_loopRangeCount==-1 )
iFrame = loopRangeMax;
else if ( cur.m_loopRangeCount>0 )
{
iLoopRange++;
if ( iLoopRange<=cur.m_loopRangeCount )
iFrame = loopRangeMax;
else
iLoopRange = -1;
}
else
iLoopRange = -1;
}
}
if ( iFrame<0 )
{
if ( cur.m_loopCount==-1 )
{
iFrame = G.__133(cur.m_dirs[iDir].__476()-1);
iLoopRange = 0;
}
else if ( cur.m_loopCount>0 )
{
iLoop++;
if ( iLoop<=cur.m_loopCount )
{
iFrame = G.__133(cur.m_dirs[iDir].__476()-1);
iLoopRange = 0;
}
else
animFinished = true;
}
else
animFinished = true;
}
}
else
{
iFrame++;
frame = iFrame;
if ( iFrame<passedFrames.Length )
passedFrames[iFrame] = true;
if ( cur.m_random==RANDOM.FRAME )
__684();
if ( cur.m_random==RANDOM.NONE && iLoopRange!=-1 )
{
int loopRangeMax = cur.m_loopRangeMax==-1 ? dir.__476()-1 : cur.m_loopRangeMax;
if ( iFrame>loopRangeMax )
{
if ( cur.m_loopRangeCount==-1 )
iFrame = cur.m_loopRangeMin;
else if ( cur.m_loopRangeCount>0 )
{
iLoopRange++;
if ( iLoopRange<=cur.m_loopRangeCount )
iFrame = cur.m_loopRangeMin;
else
iLoopRange = -1;
}
else
iLoopRange = -1;
}
}
if ( iFrame>=dir.__476() )
{
if ( cur.m_loopCount==-1 )
{
iFrame = 0;
iLoopRange = 0;
if ( cur.m_random==RANDOM.LOOP )
__684();
}
else if ( cur.m_loopCount>0 )
{
iLoop++;
if ( iLoop<=cur.m_loopCount )
{
iFrame = 0;
iLoopRange = 0;
if ( cur.m_random==RANDOM.LOOP )
__684();
}
else
animFinished = true;
}
else
animFinished = true;
}
}
if ( iFrame>=0 && iFrame<passedFrames.Length )
passedFrames[iFrame] = true;
if ( animFinished )
break;
if ( actionFrame!=-1 && actionFrameReached==false )
{
if ( reverse )
{
if ( frame<=actionFrame )
actionFrameReached = true;
}
else
{
if ( frame>=actionFrame )
actionFrameReached = true;
}
}
if ( actionFrameReached )
break;
}
AnimNotif prevNotif = notif;
if ( animFinished )
{
if ( sceneObj.m_routeTurn )
{
sceneObj.m_routeTurn = false;
sceneObj.__632(sceneObj.m_routeTurnAnim);
__678(sceneObj.m_routeTurnDir);
return false;
}
sceneObj.__632(ref sceneObj.m_defaultStopAnim.cur);
if ( sayNext )
sceneObj.__655();
}
else if ( actionFrameReached )
{
notif.__689();
}
if ( animFinished || actionFrameReached )
{
if ( prevNotif.dialog.Length>0 )
G.m_game.__308(prevNotif.dialog, prevNotif.sentence);
if ( prevNotif.roleBox )
prevNotif.roleBox.__458(prevNotif.roleBoxToken);
ended = true;
}
return true;
}
public void __687()
{
AnimDir dir = __679();
if ( dir==null || dir.__476()==0 )
return;
profileFrameIndex++;
int iRealFrame = profileFrames[profileFrameIndex];
if ( iRealFrame!=-1 )
{
iFrame = iRealFrame<dir.__476() ? iRealFrame : 0;
return;
}
int frameCount = 0;
switch ( cur.m_profile )
{
case PROFILE.MODEST:
case PROFILE.CALM:
case PROFILE.ARROGANT:
frameCount = G.__156(4, 6);
break;
case PROFILE.SHY:
case PROFILE.NEUTRAL:
case PROFILE.PROUD:
frameCount = G.__156(2, 5);
break;
case PROFILE.NERVOUS:
case PROFILE.HYSTERIC:
case PROFILE.ANGRY:
frameCount = G.__156(1, 3);
break;
}
int step = dir.__476()/3;
if ( step==0 )
return;
int curPose = iFrame/step;
int newPose = 0;
if ( curPose==0 )
{
switch ( cur.m_profile )
{
case PROFILE.MODEST:
case PROFILE.SHY:
case PROFILE.NERVOUS:
newPose = G.__156(6)==0 ? 2 : 1;
break;
case PROFILE.CALM:
case PROFILE.NEUTRAL:
case PROFILE.HYSTERIC:
newPose = G.__156(2)==0 ? 1 : 2;
break;
case PROFILE.ARROGANT:
case PROFILE.PROUD:
case PROFILE.ANGRY:
newPose = G.__156(6)==0 ? 1 : 2;
break;
}
}
profileFrameIndex = 0;
profileFrames[frameCount] = -1;
for ( int i=0 ; i<frameCount ; i++ )
{
while ( true )
{
int index = newPose*step + G.__156(step);
if ( i>0 && index==profileFrames[i-1] )
continue;
if ( cur.m_profile==PROFILE.NEUTRAL && i>0 && profileFrames[i-1]%step==index%step )
continue;
profileFrames[i] = index;
break;
}
}
iFrame = profileFrames[0]==-1 ? 0 : profileFrames[0];
}
public void __688()
{
Frame frame = __683();
if ( frame )
frame.m_sprite.__688();
}
}
public struct AnimNotif
{
public bool withLock;
public int actionFrame;
public string dialog;
public int sentence;
public RoleBox roleBox;
public int roleBoxToken;
public void Reset()
{
withLock = false;
actionFrame = -2;
dialog = "";
sentence = 0;
roleBox = null;
roleBoxToken = 0;
}
public void __689()
{
if ( withLock )
G.m_game.m_lockedByAnim--;
Reset();
}
}
