using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
public class Scene
{
public int m_sid;
public string m_uid;
public string[] m_tags;
public Role m_role;
public LayerSprite m_backLayer = null;
public LayerSprite[] m_maskLayers = new LayerSprite[G.MASK_COUNT];
public LayerSprite[] m_farLayers = new LayerSprite[G.FAR_COUNT];
public Serial<Effect> m_effect;
public SceneObj m_voiceOverObj;
public SceneObj[] m_objects;
public SceneLabel[] m_labels;
public SceneDoor[] m_doors;
public SceneShot[] m_shots;
public SceneWall[] m_walls;
public SceneBokeh[] m_bokehs;
public SceneStill[] m_stills;
public Sprite[] m_stillSprites;
public List<Vec2> m_wallpts;
public Timeline[] m_timelines;
public int m_width;
public int m_height;
public Rect m_rc;
public float m_dx;
public float m_dy;
public Rect m_rcRender;
public float m_renderHalfWidth;
public float m_renderHalfHeight;
public float m_renderOrgX;
public float m_renderOrgY;
public float m_renderX;
public float m_renderY;
public float m_renderScale;
public List<Cam> m_cameras = new List<Cam>();
public float[] m_waves = new float[4];
public float[] m_shakes = new float[4];
public float m_shakeRadius = 0.0f;
public float m_shakeSpeed = 0.0f;
public bool m_lightBaked;
public Serial<float> m_lightAmbient;
public float m_lightBlur;
public bool m_lightLow;
public float m_bokehShapeWidth;
public float m_bokehShapeHeight;
public Serial<float> m_bokehSize;
public Serial<float> m_bokehMaxZoom;
public bool m_bokeh = false;
public RenderTexture m_bokehDepthRT = null;
public static ComputeBuffer m_bokehCB = null;
public List<Bokeh> m_bokehShapes = new List<Bokeh>();
public float m_bokehShapeScale = 0.0f;
public int m_lightCount;
public List<SceneObj>[] m_lights;
public bool[] m_lightAmbients;
public bool[] m_lightDiffuses;
public bool[] m_lightChanges;
public bool m_lightChanged;
public int[] m_tempPlacementCounts;
public List<SceneEntity>[] m_sortedEntities;
public List<SceneLabel>[] m_sortedLabels;
public Dictionary<string, SceneObj> m_objectsByUID;
public Dictionary<Obj, SceneObj> m_objectsByRef;
public Dictionary<int, SceneObj> m_objectsByID;
public Dictionary<string, SceneDoor> m_doorsByName;
public Dictionary<int, SceneDoor> m_doorsByID;
public Dictionary<string, SceneLabel> m_labelsByName;
public Dictionary<int, SceneLabel> m_labelsByID;
public Dictionary<string, SceneShot> m_shotsByName;
public Dictionary<int, SceneShot> m_shotsByID;
public Dictionary<int, SceneWall> m_wallsByID;
public Dictionary<string, SceneBokeh> m_bokehsByName;
public Dictionary<int, SceneBokeh> m_bokehsByID;
public Dictionary<int, SceneStill> m_stillsByID;
public Dictionary<string, SceneStill> m_stillsByName;
public Dictionary<string, Timeline> m_timelinesByName;
public Dictionary<int, Timeline> m_timelinesByID;
public RenderTexture[] m_lightRTs;
public static implicit operator bool(Scene inst) { return inst!=null; }
public void __219()
{
m_rc.x = m_width>=G.m_rcView.width ? 0.0f : (G.m_rcView.width-m_width);
m_rc.y = m_height>=G.m_rcView.height ? 0.0f : (G.m_rcView.height-m_height);
m_rc.width = m_width;
m_rc.height = m_height;
if ( m_rc.width>G.m_rcView.width )
m_rc.width = G.m_rcView.width;
if ( m_rc.height>G.m_rcView.height )
m_rc.height = G.m_rcView.height;
m_dx = m_rc.width * 0.5f;
m_dy = m_rc.height * 0.5f;
}
public static void __533()
{
if ( m_bokehCB!=null )
{
m_bokehCB.Release();
m_bokehCB = null;
}
}
public void Reset()
{
End();
__533();
m_effect.Reset();
m_lightAmbient.Reset();
m_bokehSize.Reset();
m_bokehMaxZoom.Reset();
for ( int i=0 ; i<G.MASK_COUNT ; i++ )
{
if ( m_maskLayers[i] )
m_maskLayers[i].Reset();
}
m_backLayer.Reset();
for ( int i=0 ; i<G.FAR_COUNT ; i++ )
{
if ( m_farLayers[i] )
m_farLayers[i].Reset();
}
for ( int i=0 ; i<m_labels.Length ; i++ )
m_labels[i].Reset();
for ( int i=0 ; i<m_doors.Length ; i++ )
m_doors[i].Reset();
for ( int i=0 ; i<m_shots.Length ; i++ )
m_shots[i].Reset();
for ( int i=0 ; i<m_walls.Length ; i++ )
m_walls[i].Reset();
for ( int i=0 ; i<m_bokehs.Length ; i++ )
m_bokehs[i].Reset();
for ( int i=0 ; i<m_stills.Length ; i++ )
m_stills[i].Reset();
for ( int i=0 ; i<m_objects.Length ; i++ )
m_objects[i].Reset();
m_voiceOverObj.Reset();
}
public void __46(JsonObj json)
{
json.__383("sid", m_sid);
json.__381("uid", m_uid);
json.__381("tag", m_tags[0]);
if ( m_effect.modified )
json.__383("effect", m_effect.cur.m_sid);
if ( m_lightAmbient.modified )
json.__384("ambient", m_lightAmbient.cur);
if ( m_bokehSize.modified )
json.__384("bokehsize", m_bokehSize.cur);
if ( m_bokehMaxZoom.modified )
json.__384("bokehzoom", m_bokehMaxZoom.cur);
JsonArray jMasks = json.__390("masks");
for ( int i=0 ; i<G.MASK_COUNT ; i++ )
{
JsonObj jMask = jMasks.__389();
jMask.__385("visible", m_maskLayers[i]==null ? false : m_maskLayers[i].__428());
}
JsonObj jBack = json.__389("back");
jBack.__385("visible", m_backLayer.__428());
JsonArray jFarbacks = json.__390("farbacks");
for ( int i=0 ; i<G.FAR_COUNT ; i++ )
{
JsonObj jFarback = jFarbacks.__389();
jFarback.__385("visible", m_farLayers[i]==null ? false : m_farLayers[i].__428());
}
JsonArray jLabels = json.__390("labels");
for ( int i=0 ; i<m_labels.Length ; i++ )
{
JsonObj jLabel = jLabels.__389();
m_labels[i].__46(jLabel);
}
JsonArray jDoors = json.__390("doors");
for ( int i=0 ; i<m_doors.Length ; i++ )
{
SceneDoor door = m_doors[i];
JsonObj jDoor = jDoors.__389();
jDoor.__382("id", door.m_sid);
jDoor.__381("tag", door.m_tags[0]);
if ( door.m_title.m_sub.modified )
jDoor.__382("title", door.m_title.m_sub.cur);
if ( door.m_visible.modified )
jDoor.__385("visible", door.m_visible.cur);
if ( door.m_cheat.modified )
jDoor.__385("cheat", door.m_cheat.cur);
}
JsonArray jBokehs = json.__390("bokehs");
for ( int i=0 ; i<m_bokehs.Length ; i++ )
{
JsonObj jBokeh = jBokehs.__389();
m_bokehs[i].__46(jBokeh);
}
JsonArray jStills = json.__390("stills");
for ( int i=0 ; i<m_stills.Length ; i++ )
{
SceneStill still = m_stills[i];
JsonObj jStill = jStills.__389();
jStill.__382("id", still.m_sid);
jStill.__381("tag", still.m_tags[0]);
if ( still.m_visible.modified )
jStill.__385("visible", still.m_visible.cur);
}
JsonArray jObjects = json.__390("objects");
for ( int i=0 ; i<m_objects.Length ; i++ )
{
JsonObj jObject = jObjects.__389();
m_objects[i].__46(jObject);
}
if ( m_shakes[0]!=0.0f || m_shakes[2]!=0.0f )
{
JsonObj jShake = json.__389("shake");
jShake.__384("x", m_shakes[2]);
jShake.__384("vx", m_shakes[3]);
jShake.__384("y", m_shakes[0]);
jShake.__384("vy", m_shakes[1]);
}
if ( m_waves[0]!=0.0f || m_waves[2]!=0.0f )
{
JsonObj jWave = json.__389("wave");
jWave.__384("x", m_waves[2]);
jWave.__384("vx", m_waves[3]);
jWave.__384("y", m_waves[0]);
jWave.__384("vy", m_waves[1]);
}
}
public void __47(JsonObj json)
{
m_tags[0] = json.GetString("tag");
if ( json.__391("effect") )
m_effect.Set(G.m_game.__282(json.GetInt("effect")));
if ( json.__391("ambient") )
m_lightAmbient.Set(json.GetFloat("ambient"));
if ( json.__391("bokehsize") )
m_bokehSize.Set(json.GetFloat("bokehsize"));
if ( json.__391("bokehzoom") )
m_bokehMaxZoom.Set(json.GetFloat("bokehzoom"));
JsonArray jMasks = json.__395("masks");
if ( jMasks )
{
for ( int i=0 ; i<G.MASK_COUNT ; i++ )
{
JsonObj jMask = jMasks.__394(i);
if ( m_maskLayers[i] && jMask )
m_maskLayers[i].__997(jMask.__401("visible"));
}
}
JsonObj jBack = json.__394("back");
if ( jBack )
{
m_backLayer.__997(jBack.__401("visible"));
}
JsonArray jFarbacks = json.__395("farbacks");
if ( jFarbacks )
{
for ( int i=0 ; i<G.FAR_COUNT ; i++ )
{
JsonObj jFarback = jFarbacks.__394(i);
if ( m_farLayers[i] && jFarback )
m_farLayers[i].__997(jFarback.__401("visible"));
}
}
JsonArray jLabels = json.__395("labels");
if ( jLabels )
{
for ( int i=0 ; i<jLabels.__67() ; i++ )
{
JsonObj jLabel = jLabels.__394(i);
if ( jLabel )
{
SceneLabel label = __538(jLabel.GetInt("id"));
if ( label )
label.__47(jLabel);
}
}
}
JsonArray jDoors = json.__395("doors");
if ( jDoors )
{
for ( int i=0 ; i<jDoors.__67() ; i++ )
{
JsonObj jDoor = jDoors.__394(i);
if ( jDoor )
{
SceneDoor door = __537(jDoor.GetInt("id"));
if ( door )
{
door.m_tags[0] = jDoor.GetString("tag");
if ( jDoor.__391("title") )
door.m_title.m_sub.Set(jDoor.GetInt("title"));
if ( jDoor.__391("visible") )
door.m_visible.Set(jDoor.__401("visible"));
if ( jDoor.__391("cheat") )
door.m_cheat.Set(jDoor.__401("cheat"));
}
}
}
}
JsonArray jBokehs = json.__395("bokehs");
if ( jBokehs )
{
for ( int i=0 ; i<jBokehs.__67() ; i++ )
{
JsonObj jBokeh = jBokehs.__394(i);
if ( jBokeh )
{
SceneBokeh bokeh = __541(jBokeh.GetInt("id"));
if ( bokeh )
bokeh.__47(jBokeh);
}
}
}
JsonArray jStills = json.__395("stills");
if ( jStills )
{
for ( int i=0 ; i<jStills.__67() ; i++ )
{
JsonObj jStill = jStills.__394(i);
if ( jStill )
{
SceneStill still = __542(jStill.GetInt("id"));
if ( still )
{
still.m_tags[0] = jStill.GetString("tag");
if ( jStill.__391("visible") )
still.m_visible.Set(jStill.__401("visible"));
}
}
}
}
JsonArray jObjects = json.__395("objects");
if ( jObjects )
{
for ( int i=0 ; i<jObjects.__67() ; i++ )
{
JsonObj jObj = jObjects.__394(i);
if ( jObj )
{
SceneObj sceneObj = __277(jObj.GetInt("id"));
if ( sceneObj )
sceneObj.__47(jObj);
}
}
}
JsonObj jShake = json.__394("shake");
if ( jShake )
{
m_shakes[0] = jShake.GetFloat("y");
m_shakes[1] = jShake.GetFloat("vy");
m_shakes[2] = jShake.GetFloat("x");
m_shakes[3] = jShake.GetFloat("vx");
}
JsonObj jWave = json.__394("wave");
if ( jWave )
{
m_waves[0] = jWave.GetFloat("y");
m_waves[1] = jWave.GetFloat("vy");
m_waves[2] = jWave.GetFloat("x");
m_waves[3] = jWave.GetFloat("vx");
}
}
public void __469(bool newgame = true)
{
Asset asset = G.__96(G.m_pathGraphics);
if ( asset==null )
return;
m_backLayer.__469(asset);
for ( int i=0 ; i<G.MASK_COUNT ; i++ )
{
if ( m_maskLayers[i] )
m_maskLayers[i].__469(asset);
}
for ( int i=0 ; i<G.FAR_COUNT ; i++ )
{
if ( m_farLayers[i] )
m_farLayers[i].__469(asset);
}
for ( int i=0 ; i<m_stillSprites.Length ; i++ )
m_stillSprites[i].__469(asset);
for ( int i=0 ; i<m_stills.Length ; i++ )
m_stills[i].__469(asset);
for ( int i=0 ; i<m_objects.Length ; i++ )
m_objects[i].__469(asset);
m_voiceOverObj.__469(asset);
for ( int i=0 ; i<m_objects.Length ; i++ )
{
SceneObj sceneObj = m_objects[i];
if ( sceneObj.m_obj.m_killed.cur )
sceneObj.m_visible.cur = false;
else if ( sceneObj.m_visibleInThisScene!=-1 )
sceneObj.m_visible.cur = sceneObj.m_visibleInThisScene==1;
else if ( sceneObj.m_visible.modified==false )
G.m_game.__306(m_uid, sceneObj.m_obj.m_uid, true);
}
asset.Close();
__566();
__568();
for ( int i=0 ; i<4 ; i++ )
{
m_waves[i] = 0.0f;
m_shakes[i] = 0.0f;
}
m_renderOrgX = 0.0f;
m_renderOrgY = 0.0f;
m_renderX = 0.0f;
m_renderY = 0.0f;
__569(1.0f);
__571(G.m_game.__293());
__564();
__565();
if ( newgame && m_role )
m_role.Start();
}
public void End(Scene nextScene = null)
{
if ( m_role )
m_role.Stop();
if ( m_lightRTs!=null )
{
for ( int i=0 ; i<m_lightRTs.Length ; i++ )
{
G.Release(m_lightRTs[i]);
m_lightRTs[i] = null;
}
}
G.m_game.__314();
G.m_game.__273();
m_backLayer.End();
for ( int i=0 ; i<G.MASK_COUNT ; i++ )
{
if ( m_maskLayers[i] )
m_maskLayers[i].End();
}
for ( int i=0 ; i<G.FAR_COUNT ; i++ )
{
if ( m_farLayers[i] )
m_farLayers[i].End();
}
for ( int i=0 ; i<m_stills.Length ; i++ )
m_stills[i].End();
for ( int i=0 ; i<m_stillSprites.Length ; i++ )
m_stillSprites[i].End();
for ( int i=0 ; i<m_objects.Length ; i++ )
m_objects[i].End(nextScene);
m_voiceOverObj.End(nextScene);
m_wallpts = null;
if ( m_lightCount>0 )
{
m_lightChanged = false;
for ( int i=0 ; i<(int)PLACEMENT.COUNT ; i++ )
{
m_lights[i] = null;
m_lightAmbients[i] = false;
m_lightDiffuses[i] = false;
m_lightChanges[i] = false;
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
public float __534()
{
if ( m_lightAmbient.cur!=-1.0f )
return m_lightAmbient.cur;
return G.m_game.m_lightAmbient;
}
public float __535(out bool hasBlur)
{
#if UNITY_WEBGL
const float minValue = 0.01f;
const float maxValue = 0.1f;
#else
const float minValue = 1.0f;
const float maxValue = 64.0f;
#endif
float blur = minValue;
if ( m_lightBlur!=-1.0f )
blur = G.__135(m_lightBlur, minValue, maxValue);
else
blur = G.__135(G.m_game.m_lightBlur, minValue, maxValue);
hasBlur = blur>minValue;
return blur;
}
public SceneEntity __536(ref string name)
{
if ( G.__148(ref name, "CURSOR") )
return G.m_game.m_entityCursor;
SceneObj sceneObj = __277(name);
if ( sceneObj )
return sceneObj;
SceneLabel label = __538(name);
if ( label )
return label;
return __541(name);
}
public SceneObj __277(string uid)
{
if ( uid.Length==0 )
return null;
if ( uid[0]=='O' || uid[0]=='o' )
{
SceneObj val;
if ( m_objectsByUID.TryGetValue(uid, out val)==false )
return null;
return val;
}
Player player = G.m_game.__279(uid);
if ( player==null )
return null;
return player.m_sceneObj;
}
public SceneObj __277(Obj obj)
{
if ( obj==null )
return null;
SceneObj val;
if ( m_objectsByRef.TryGetValue(obj, out val)==false )
return null;
return val;
}
public SceneObj __277(int id)
{
SceneObj val;
if ( m_objectsByID.TryGetValue(id, out val)==false )
return null;
return val;
}
public SceneDoor __537(string name)
{
if ( name.Length==0 )
return null;
SceneDoor val;
if ( m_doorsByName.TryGetValue(name, out val)==false )
return null;
return val;
}
public SceneDoor __537(int id)
{
SceneDoor val;
if ( m_doorsByID.TryGetValue(id, out val)==false )
return null;
return val;
}
public SceneLabel __538(string name)
{
if ( name.Length==0 )
return null;
SceneLabel val;
if ( m_labelsByName.TryGetValue(name, out val)==false )
return null;
return val;
}
public SceneLabel __538(int id)
{
SceneLabel val;
if ( m_labelsByID.TryGetValue(id, out val)==false )
return null;
return val;
}
public SceneShot __539(string name)
{
if ( name.Length==0 )
return null;
SceneShot val;
if ( m_shotsByName.TryGetValue(name, out val)==false )
return null;
return val;
}
public SceneShot __539(int id)
{
SceneShot val;
if ( m_shotsByID.TryGetValue(id, out val)==false )
return null;
return val;
}
public SceneWall __540(int id)
{
SceneWall val;
if ( m_wallsByID.TryGetValue(id, out val)==false )
return null;
return val;
}
public SceneBokeh __541(string name)
{
if ( name.Length==0 )
return null;
SceneBokeh val;
if ( m_bokehsByName.TryGetValue(name, out val)==false )
return null;
return val;
}
public SceneBokeh __541(int id)
{
SceneBokeh val;
if ( m_bokehsByID.TryGetValue(id, out val)==false )
return null;
return val;
}
public SceneStill __542(string name)
{
if ( name.Length==0 )
return null;
SceneStill val;
if ( m_stillsByName.TryGetValue(name, out val)==false )
return null;
return val;
}
public SceneStill __542(int id)
{
SceneStill val;
if ( m_stillsByID.TryGetValue(id, out val)==false )
return null;
return val;
}
public Timeline __543(string name)
{
if ( name.Length==0 )
return null;
Timeline val;
if ( m_timelinesByName.TryGetValue(name, out val)==false )
return null;
return val;
}
public Timeline __543(int id)
{
Timeline val;
if ( m_timelinesByID.TryGetValue(id, out val)==false )
return null;
return val;
}
public LayerSprite __544(PLACEMENT placement)
{
switch ( placement )
{
case PLACEMENT.MA:		return m_maskLayers[0];
case PLACEMENT.MB:		return m_maskLayers[1];
case PLACEMENT.MC:		return m_maskLayers[2];
case PLACEMENT.MD:		return m_maskLayers[3];
case PLACEMENT.BACK:	return m_backLayer;
case PLACEMENT.FA:		return m_farLayers[0];
case PLACEMENT.FB:		return m_farLayers[1];
case PLACEMENT.FC:		return m_farLayers[2];
case PLACEMENT.FD:		return m_farLayers[3];
}
return null;
}
public bool __545()
{
if ( G.__88() )
return false;
for ( int i=0 ; i<m_bokehs.Length ; i++ )
{
if ( m_bokehs[i].m_visible.cur )
return true;
}
return false;
}
public bool __426(out SceneEntity outEntity, out SubObj outSub, float xView, float yView, bool canExcludePlayer, DRAG drag)
{
outEntity = null;
outSub = null;
if ( G.m_rcView.Contains(xView, yView)==false )
return true;
if ( G.m_game.m_layout.__426(xView, yView)!=LAYOUT_CTRL.COUNT )
return true;
Player player = G.m_game.__293();
for ( int ip=0 ; ip<(int)PLACEMENT.COUNT ; ip++ )
{
if ( ip>0 )
{
LayerSprite layer = __544((PLACEMENT)(ip-1));
if ( layer && layer.__998(xView, yView) )
return false;
}
List<SceneEntity> entities = m_sortedEntities[ip];
if ( entities!=null && entities.Count>0 )
{
for ( int iEntity=entities.Count-1 ; iEntity>=0 ; iEntity-- )
{
SceneEntity entity = entities[iEntity];
if ( entity.__606()==false )
continue;
SceneObj sceneObj = (SceneObj)entity;
if ( sceneObj.m_visible.cur==false )
continue;
if ( canExcludePlayer && player && player.m_interaction==false && sceneObj==player.m_sceneObj )
continue;
switch ( drag )
{
case DRAG.NONE:
if ( sceneObj.m_drag!=DRAG.NONE )
continue;
break;
case DRAG.SOURCE:
if ( sceneObj.m_drag!=DRAG.SOURCE && sceneObj.m_drag!=DRAG.BOTH )
continue;
break;
case DRAG.TARGET:
if ( sceneObj.m_drag!=DRAG.TARGET && sceneObj.m_drag!=DRAG.BOTH )
continue;
break;
case DRAG.BOTH:
if ( sceneObj.m_drag==DRAG.NONE )
continue;
break;
}
SubObj sub = null;
if ( sceneObj.m_obj.m_subObjs.Length>0 && sceneObj.m_obj.m_subEnabled.cur )
{
Frame frame = sceneObj.m_anim.__683();
if ( frame )
{
float xLocal = xView;
float yLocal = yView;
sceneObj.__667(ref xLocal, ref yLocal, frame);
for ( int iSub=0 ; iSub<sceneObj.m_obj.m_subObjs.Length ; iSub++ )
{
if ( frame.m_subObjRects[iSub].Contains(xLocal, yLocal) )
{
if ( sceneObj.__666(frame, xLocal, yLocal) )
{
sub = sceneObj.m_obj.m_subObjs[iSub];
break;
}
}
}
}
}
if ( sceneObj.m_obj.m_enabled.cur==false )
{
if ( sub==null )
continue;
outEntity = sceneObj;
outSub = sub;
return true;
}
if ( sub )
{
outEntity = sceneObj;
outSub = sub;
return true;
}
if ( sceneObj.Contains(xView, yView)==false )
continue;
if ( sceneObj.__666(xView, yView)==false )
continue;
outEntity = sceneObj;
outSub = sub;
return true;
}
}
List<SceneLabel> labels = m_sortedLabels[ip];
if ( labels!=null && labels.Count>0 )
{
for ( int iLabel=0 ; iLabel<labels.Count ; iLabel++ )
{
SceneLabel label = labels[iLabel];
if ( label.m_visible.cur==false )
continue;
if ( label.m_enabled.cur==false )
continue;
if ( drag==DRAG.TARGET )
{
if ( label.m_drag!=DRAG.TARGET )
continue;
}
else
{
if ( label.m_drag!=DRAG.NONE )
continue;
}
if ( label.m_rc.Contains(xView, yView)==false )
continue;
outEntity = label;
return true;
}
}
}
return true;
}
public SceneDoor __546(ref bool abort, float xView, float yView)
{
abort = false;
if ( G.m_rcView.Contains(xView, yView)==false )
return null;
if ( G.m_game.m_layout.__426(xView, yView)!=LAYOUT_CTRL.COUNT )
return null;
for ( PLACEMENT placement=PLACEMENT.MA ; placement!=PLACEMENT.BACK ; placement++ )
{
LayerSprite layer = __544(placement);
if ( layer && layer.__998(xView, yView) )
{
abort = true;
return null;
}
}
for ( int i=0 ; i<m_doors.Length ; i++ )
{
if ( m_doors[i].m_visible.cur==false )
continue;
if ( m_doors[i].m_rc.Contains(xView, yView) )
return m_doors[i];
}
return null;
}
public bool __547()
{
if ( G.m_game.m_cursor==false )
return false;
SceneObj dragObj = G.m_game.m_dragObj;
SceneEntity dropEntity;
SubObj dropSub;
if ( __426(out dropEntity, out dropSub, G.m_game.m_cursorViewX, G.m_game.m_cursorViewY, false, DRAG.TARGET)==false )
return false;
if ( dropEntity )
{
if ( dropEntity.__606() )
{
if ( dropEntity!=dragObj )
return true;
}
else
{
return true;
}
}
return false;
}
public bool __548(string id, bool visible, RoleBox roleBox = null, float duration = 0.0f)
{
LayerSprite layer = null;
switch ( id.ToUpper() )
{
case "MA":
layer = m_maskLayers[0];
break;
case "MB":
layer = m_maskLayers[1];
break;
case "MC":
layer = m_maskLayers[2];
break;
case "MD":
layer = m_maskLayers[3];
break;
case "BACK":
layer = m_backLayer;
break;
case "FA":
layer = m_farLayers[0];
break;
case "FB":
layer = m_farLayers[1];
break;
case "FC":
layer = m_farLayers[2];
break;
case "FD":
layer = m_farLayers[3];
break;
}
if ( layer==null )
return false;
if ( duration==0.0f )
{
layer.__997(visible);
layer.m_fadeDir = 0;
layer.m_fadeDuration = 0.0f;
layer.m_fadeTime = 0.0f;
layer.m_roleBox = null;
layer.m_roleBoxToken = 0;
}
else
{
layer.m_fadeDuration = duration;
layer.m_fadeTime = 0.0f;
layer.m_roleBox = roleBox;
layer.m_roleBoxToken = roleBox.m_parent.m_token;
if ( visible )
{
layer.__997(visible);
layer.m_fadeDir = 1;
}
else
{
layer.__997(true);
layer.m_fadeDir = -1;
}
}
return true;
}
public float __549(float xDoc, bool hud = false)
{
if ( hud )
return xDoc;
return m_rc.x + (xDoc-m_renderX)*m_renderScale + m_dx;
}
public float __550(float yDoc, bool hud = false)
{
if ( hud )
return yDoc;
return m_rc.y + (yDoc-m_renderY)*m_renderScale + m_dy;
}
public float __551(float doc, bool hud = false)
{
if ( hud )
return doc;
return doc*m_renderScale;
}
public void __552(ref Vec2 pt, bool hud = false)
{
pt.x = __549(pt.x, hud);
pt.y = __550(pt.y, hud);
}
public void __552(ref Rect rc, bool hud = false)
{
rc.x = __549(rc.x, hud);
rc.y = __550(rc.y, hud);
if ( hud==false )
{
rc.width = __551(rc.width, hud);
rc.height = __551(rc.height, hud);
}
}
public float __553(float xView, bool hud = false)
{
if ( hud )
return xView;
return (xView-m_rc.x-m_dx)/m_renderScale + m_renderX;
}
public float __554(float yView, bool hud = false)
{
if ( hud )
return yView;
return (yView-m_rc.y-m_dy)/m_renderScale + m_renderY;
}
public float __555(float view, bool hud = false)
{
if ( hud )
return view;
return view/m_renderScale;
}
public void __556(ref Vec2 pt)
{
pt.x = __553(pt.x);
pt.y = __554(pt.y);
}
public void __556(ref Rect rc)
{
rc.x = __553(rc.x);
rc.y = __554(rc.y);
rc.width = __555(rc.width);
rc.height = __555(rc.height);
}
public float __557(float xDoc, float scale = 0.0f)
{
if ( scale==0.0f )
{
if ( xDoc<m_rcRender.x )
return m_rcRender.x;
else if ( xDoc>m_rcRender.__437() )
return m_rcRender.__437();
}
else
{
Rect rc = Rect.Zero;
__570(ref rc, scale);
if ( xDoc<rc.x )
return rc.x;
else if ( xDoc>rc.__437() )
return rc.__437();
}
return xDoc;
}
public float __558(float yDoc, float scale = 0.0f)
{
if ( scale==0.0f )
{
if ( yDoc<m_rcRender.y )
return m_rcRender.y;
else if ( yDoc>m_rcRender.__438() )
return m_rcRender.__438();
}
else
{
Rect rc = Rect.Zero;
__570(ref rc, scale);
if ( yDoc<rc.y )
return rc.y;
else if ( yDoc>rc.__438() )
return rc.__438();
}
return yDoc;
}
public bool __559(ref Rect rc)
{
return rc.x>G.m_rcView.width || rc.__437()<0.0f || rc.y>G.m_rcView.height || rc.__438()<0.0f;
}
public void __509(string mode, string cell = "")
{
G.m_game.__323(RoleBoxEventEnterScene.ID, mode, cell);
}
public void __521()
{
G.m_game.__323(RoleBoxEventExitScene.ID);
}
public bool __560(float xDoc, float yDoc)
{
Variable result = new Variable();
if ( G.m_game.__323(RoleBoxEventClick.ID, xDoc.ToString(CultureInfo.InvariantCulture), yDoc.ToString(CultureInfo.InvariantCulture), result)==0 )
return false;
if ( result.m_value!="0" )
return false;
return true;
}
public void __561(SceneLabel label)
{
G.m_game.__323(RoleBoxEventLabel.ID, label.m_name);
}
public void __562(Obj obj, SceneLabel label)
{
G.m_game.__323(RoleBoxEventUseLabel.ID, obj.m_uid, label.m_name);
}
public void __563(SceneDoor door, bool skip = false)
{
if ( skip )
G.m_game.__323(RoleBoxEventDoor.ID, door.m_name, "SKIP");
else
G.m_game.__323(RoleBoxEventDoor.ID, door.m_name, "WALK");
}
public void __564()
{
if ( m_walls.Length==0 )
return;
const int stripe = 10000;
m_wallpts = new List<Vec2>();
SortedSet<int> tmp = new SortedSet<int>();
for ( int i=0 ; i<m_walls.Length ; i++ )
{
SceneWall wall = m_walls[i];
int id = (int)(wall.m_ay*stripe + wall.m_ax);
if ( tmp.Contains(id)==false )
{
tmp.Add(id);
m_wallpts.Add(new Vec2(wall.m_ax, wall.m_ay));
}
id = (int)(wall.m_by*stripe + wall.m_bx);
if ( tmp.Contains(id)==false )
{
tmp.Add(id);
m_wallpts.Add(new Vec2(wall.m_bx, wall.m_by));
}
}
}
public void __565()
{
if ( m_lightCount==0 )
return;
m_lightChanged = false;
for ( int i=0 ; i<(int)PLACEMENT.COUNT ; i++ )
{
m_lights[i] = null;
m_lightAmbients[i] = false;
m_lightDiffuses[i] = false;
m_lightChanges[i] = false;
}
float ambient = __534();
for ( int i=0 ; i<m_objects.Length ; i++ )
{
SceneObj sceneObj = m_objects[i];
if ( sceneObj.m_light==false )
continue;
sceneObj.m_lightMeshChanged = false;
G.Release(sceneObj.m_lightMesh);
sceneObj.m_lightMesh = null;
if ( sceneObj.m_lightVisible.cur==false )
continue;
if ( sceneObj.m_lightAmbient.cur==0 && sceneObj.m_lightDiffuse.cur.a==0.0f )
continue;
if ( sceneObj.m_lightDist.cur==0.0f )
continue;
sceneObj.m_lightMeshChanged = true;
int placement = (int)sceneObj.m_placement.cur;
if ( m_lights[placement]==null )
m_lights[placement] = new List<SceneObj>();
m_lights[placement].Add(sceneObj);
if ( m_lightAmbients[placement]==false && sceneObj.m_lightAmbient.cur!=0 && ambient!=1.0f )
m_lightAmbients[placement] = true;
if ( m_lightDiffuses[placement]==false && sceneObj.m_lightDiffuse.cur.a!=0.0f )
m_lightDiffuses[placement] = true;
}
}
public void __566()
{
for ( int i=0 ; i<m_sortedEntities.Length ; i++ )
m_sortedEntities[i] = null;
for ( int i=0 ; i<m_objects.Length ; i++ )
{
SceneObj item = m_objects[i];
int ip = (int)item.m_placement.cur;
if ( m_sortedEntities[ip]==null )
m_sortedEntities[ip] = new List<SceneEntity>();
m_sortedEntities[ip].Add(null);
}
for ( int i=0 ; i<m_bokehs.Length ; i++ )
{
SceneBokeh item = m_bokehs[i];
int ip = (int)item.m_placement.cur;
if ( m_sortedEntities[ip]==null )
m_sortedEntities[ip] = new List<SceneEntity>();
m_sortedEntities[ip].Add(null);
}
for ( int i=0 ; i<m_stills.Length ; i++ )
{
SceneStill item = m_stills[i];
int ip = (int)item.m_placement.cur;
if ( m_sortedEntities[ip]==null )
m_sortedEntities[ip] = new List<SceneEntity>();
m_sortedEntities[ip].Add(null);
}
}
public int __567(SceneEntity entity, int count)
{
if ( count==0 )
return 0;
int ip = (int)entity.m_placement.cur;
int minval = 0;
int maxval = count;
float z = entity.__619();
float z2 = 0.0f;
while ( maxval-minval>0 )
{
int index = (minval+maxval)/2;
z2 = m_sortedEntities[ip][index].__619();
if ( z==z2 )
return index;
if ( z<z2 )
maxval = index;
else
minval = index + 1;
}
if ( minval>=count )
return count;
z2 = m_sortedEntities[ip][minval].__619();
if ( z<z2 )
return minval;
return minval + 1;
}
public void __568()
{
int count;
for ( int i=0 ; i<m_tempPlacementCounts.Length ; i++ )
m_tempPlacementCounts[i] = 0;
for ( int i=0 ; i<m_objects.Length ; i++ )
{
SceneObj item = m_objects[i];
int ip = (int)item.m_placement.cur;
count = m_tempPlacementCounts[ip];
int index = __567(item, count);
for ( int j=count ; j>index ; j-- )
m_sortedEntities[ip][j] = m_sortedEntities[ip][j-1];
m_sortedEntities[ip][index] = item;
m_tempPlacementCounts[ip]++;
}
for ( int i=0 ; i<m_stills.Length ; i++ )
{
SceneStill item = m_stills[i];
int ip = (int)item.m_placement.cur;
count = m_tempPlacementCounts[ip];
int index = __567(item, count);
for ( int j=count ; j>index ; j-- )
m_sortedEntities[ip][j] = m_sortedEntities[ip][j-1];
m_sortedEntities[ip][index] = item;
m_tempPlacementCounts[ip]++;
}
for ( int i=0 ; i<m_bokehs.Length ; i++ )
{
SceneBokeh item = m_bokehs[i];
int ip = (int)item.m_placement.cur;
count = m_tempPlacementCounts[ip];
int index = __567(item, count);
for ( int j=count ; j>index ; j-- )
m_sortedEntities[ip][j] = m_sortedEntities[ip][j-1];
m_sortedEntities[ip][index] = item;
m_tempPlacementCounts[ip]++;
}
}
public void __569(float scale)
{
m_lightChanged = true;
m_renderScale = scale;
__570(ref m_rcRender, scale);
m_renderHalfWidth = m_rcRender.width*0.5f;
m_renderHalfHeight = m_rcRender.height*0.5f;
}
public void __570(ref Rect rc, float scale)
{
float w = G.m_rcView.width/scale;
float h = G.m_rcView.height/scale;
rc.x = w * 0.5f;
rc.y = h * 0.5f;
rc.width = m_width - w;
rc.height = m_height - h;
}
public void __571(Player player)
{
m_lightChanged = true;
m_cameras.Clear();
if ( player )
__572(player.__479());
}
public Cam __572(CAMERA type)
{
if ( type==CAMERA.OFF )
return __479();
m_lightChanged = true;
if ( type==CAMERA.AUTO || type==CAMERA.AUTO_POS || type==CAMERA.AUTO_SCALE )
{
if ( m_cameras.Count==0 )
{
m_cameras.Add(new Cam(type, this));
return __479();
}
else
{
if ( m_cameras[0].__33() )
{
m_cameras[0].Reset();
return m_cameras[0];
}
else
{
m_cameras.Insert(0, new Cam(type, this));
return m_cameras[0];
}
}
}
else
{
for ( int i=0 ; i<m_cameras.Count ; i++ )
{
if ( m_cameras[i].m_type==type )
{
m_cameras[i].Reset();
return m_cameras[i];
}
if ( m_cameras[i].m_type>type )
{
m_cameras.Insert(i, new Cam(type, this));
return m_cameras[i];
}
}
m_cameras.Add(new Cam(type, this));
return __479();
}
}
public void __573(CAMERA type)
{
for ( int i=0 ; i<m_cameras.Count ; i++ )
{
if ( m_cameras[i].m_type==type )
{
m_lightChanged = true;
m_cameras.RemoveAt(i);
return;
}
}
}
public Cam __479()
{
if ( m_cameras.Count==0 )
return null;
return m_cameras[m_cameras.Count-1];
}
public Cam __479(CAMERA type)
{
for ( int i=0 ; i<m_cameras.Count ; i++ )
{
if ( m_cameras[i].m_type==type )
return m_cameras[i];
}
return null;
}
public void __574()
{
Cam cam = __479();
Player player = G.m_game.__293();
if ( cam==null || player==null || player.m_sceneObj==null )
{
__569(1.0f);
m_renderX = m_rcRender.__440();
m_renderY = m_rcRender.__441();
m_renderOrgX = m_renderX;
m_renderOrgY = m_renderY;
return;
}
__569(cam.__34());
m_renderX = __557(player.m_sceneObj.__35());
m_renderY = __558(player.m_sceneObj.__36());
m_renderOrgX = m_renderX;
m_renderOrgY = m_renderY;
m_lightChanged = true;
}
public void __575(bool onlyAuto = false)
{
Cam cam = __479();
if ( cam==null )
{
__569(1.0f);
m_renderX = m_rcRender.__440();
m_renderY = m_rcRender.__441();
m_renderOrgX = m_renderX;
m_renderOrgY = m_renderY;
return;
}
switch ( cam.m_type )
{
case CAMERA.AUTO:
case CAMERA.AUTO_POS:
case CAMERA.AUTO_SCALE:
__576(cam, G.m_game.__293());
break;
case CAMERA.CURSOR:
if ( onlyAuto==false )
__577(cam);
break;
case CAMERA.MANUAL:
if ( onlyAuto==false )
__578(cam);
break;
case CAMERA.TALK:
if ( onlyAuto==false )
__579(cam);
break;
case CAMERA.TRACK:
if ( onlyAuto==false )
__580(cam);
break;
case CAMERA.TIMELINE:
if ( onlyAuto==false )
__581(cam);
break;
}
if ( m_waves[0]!=0.0f )
{
m_lightChanged = true;
m_renderY = m_renderOrgY + Mathf.Sin(G.m_game.m_time*m_waves[1]) * m_waves[0];
}
if ( m_waves[2]!=0.0f )
{
m_lightChanged = true;
m_renderX = m_renderOrgX + Mathf.Sin(G.m_game.m_time*m_waves[3]) * m_waves[2];
}
if ( m_shakes[0]!=0.0f )
{
float offset = G.m_game.m_time * m_shakes[1];
float noise = G.Clamp(Mathf.PerlinNoise(0.0f, offset)) * 2.0f - 1.0f;
m_renderY = m_renderOrgY + noise * m_shakes[0];
m_lightChanged = true;
}
if ( m_shakes[2]!=0.0f )
{
float offset = G.m_game.m_time * m_shakes[3];
float noise = G.Clamp(Mathf.PerlinNoise(offset, 0.0f)) * 2.0f - 1.0f;
m_renderX = m_renderOrgX + noise * m_shakes[2];
m_lightChanged = true;
}
}
public void __576(Cam cam, Player player)
{
if ( player==null || player.m_sceneObj==null )
{
__569(1.0f);
m_renderX = m_rcRender.__440();
m_renderY = m_rcRender.__441();
m_renderOrgX = m_renderX;
m_renderOrgY = m_renderY;
return;
}
if ( cam.m_type==CAMERA.AUTO || cam.m_type==CAMERA.AUTO_SCALE )
{
if ( player.m_zoomSmooth.cur==0.0f )
{
__569(cam.__34());
}
else
{
if ( cam.m_zoomSmoothInitialized )
{
float renderScale = m_renderScale;
float renderScaleTrg = cam.__34();
float speed = 0.1f * (1.0f-player.m_zoomSmooth.cur);
float delta = speed * G.m_game.m_elapsed;
if ( Mathf.Abs(renderScaleTrg-m_renderScale)<0.0001f )
renderScale = renderScaleTrg;
else if ( m_renderScale<renderScaleTrg )
{
renderScale += delta;
if ( renderScale>renderScaleTrg )
renderScale = renderScaleTrg;
}
else
{
renderScale -= delta;
if ( renderScale<renderScaleTrg )
renderScale = renderScaleTrg;
}
__569(G.Clamp(renderScale, 1.0f, 4.0f));
}
else
{
cam.m_zoomSmoothInitialized = true;
__569(cam.__34());
}
}
}
else
{
__569(1.0f);
}
if ( cam.m_type==CAMERA.AUTO || cam.m_type==CAMERA.AUTO_POS )
{
m_lightChanged = true;
if ( player.m_scrollSmooth.cur==0.0f )
{
m_renderX = __557(player.m_sceneObj.__35());
m_renderY = __558(player.m_sceneObj.__36());
m_renderOrgX = m_renderX;
m_renderOrgY = m_renderY;
}
else
{
if ( cam.m_scrollSmoothInitialized )
{
m_renderX = m_renderOrgX;
m_renderY = m_renderOrgY;
float speed = player.m_sceneObj.m_scrollSmoothSpeed * (1.0f-player.m_scrollSmooth.cur);
float renderX = __557(player.m_sceneObj.__35());
float deadzone = speed * 0.25f;
if ( deadzone<1.0f )
deadzone = 1.0f;
if ( m_renderX<renderX )
{
if ( renderX-m_renderX<deadzone )
speed *= G.Clamp((renderX-m_renderX)/deadzone, 0.1f, 1.0f);
m_renderX += speed * G.m_game.m_elapsed;
if ( m_renderX>renderX )
m_renderX = renderX;
}
else
{
if ( m_renderX-renderX<deadzone )
speed *= G.Clamp((m_renderX-renderX)/deadzone, 0.1f, 1.0f);
m_renderX -= speed * G.m_game.m_elapsed;
if ( m_renderX<renderX )
m_renderX = renderX;
}
m_renderX = __557(m_renderX);
m_renderY = __558(player.m_sceneObj.__36());
m_renderOrgX = m_renderX;
m_renderOrgY = m_renderY;
}
else
{
cam.m_scrollSmoothInitialized = true;
m_renderX = __557(player.m_sceneObj.__35());
m_renderY = __558(player.m_sceneObj.__36());
m_renderOrgX = m_renderX;
m_renderOrgY = m_renderY;
}
}
}
else
{
m_renderX = m_rcRender.__440();
m_renderY = m_rcRender.__441();
m_renderOrgX = m_renderX;
m_renderOrgY = m_renderY;
}
}
public void __577(Cam cam)
{
float scale = cam.__34();
__569(scale);
m_renderX = __557(cam.__35());
m_renderY = __558(cam.__36());
m_renderOrgX = m_renderX;
m_renderOrgY = m_renderY;
m_lightChanged = true;
}
public void __578(Cam cam)
{
float scale = cam.__34();
__569(scale);
m_renderX = __557(cam.__35());
m_renderY = __558(cam.__36());
m_renderOrgX = m_renderX;
m_renderOrgY = m_renderY;
}
public void __579(Cam cam)
{
if ( cam.m_ratio==1.0f )
return;
cam.m_time += G.m_game.m_elapsed;
cam.m_ratio = 1.0f;
if ( cam.m_duration!=0.0f )
{
cam.m_ratio = G.Clamp(cam.m_time/cam.m_duration);
cam.m_ratio = G.__138(cam.m_ratio);
}
m_lightChanged = true;
m_renderX = cam.m_fromX + (cam.m_toX-cam.m_fromX)*cam.m_ratio;
m_renderOrgX = m_renderX;
}
public void __580(Cam cam)
{
cam.m_time += G.m_game.m_elapsed;
cam.m_ratio = 1.0f;
if ( cam.m_duration!=0.0f )
{
cam.m_ratio = G.Clamp(cam.m_time/cam.m_duration);
cam.m_ratio = G.__138(cam.m_ratio);
}
float scale = cam.__34();
__569(scale);
m_renderX = cam.__35();
m_renderY = cam.__36();
m_renderOrgX = m_renderX;
m_renderOrgY = m_renderY;
m_lightChanged = true;
if ( cam.m_ratio==1.0f )
{
__573(CAMERA.TRACK);
if ( cam.m_roleBox )
cam.m_roleBox.__458(cam.m_roleBoxToken);
}
}
public void __581(Cam cam)
{
float scale = cam.__34();
__569(scale);
m_renderX = __557(cam.__35());
m_renderY = __558(cam.__36());
m_renderOrgX = m_renderX;
m_renderOrgY = m_renderY;
m_lightChanged = true;
}
public void __582()
{
if ( m_lightCount==0 )
return;
for ( int ip=0 ; ip<m_lights.Length ; ip++ )
{
List<SceneObj> lights = m_lights[ip];
if ( lights==null || lights.Count==0 )
continue;
for ( int iLight=0 ; iLight<lights.Count ; iLight++ )
{
SceneObj light = lights[iLight];
if ( light.m_lightMeshChanged==false )
continue;
m_lightChanges[ip] = true;
light.m_lightMeshChanged = false;
G.Release(light.m_lightMesh);
light.m_lightMesh = null;
bool light360 = light.m_lightAngle.cur==G.RAD_360;
float lightX = light.__35();
float lightY = light.__36();
float lightDir = light.m_lightDir.cur;
float lightAngle = light360 ? G.RAD_360 : G.__144(light.m_lightAngle.cur);
float lightAngleHalf = lightAngle*0.5f;
float lightDirFirstRaw = light360 ? 0.0f : lightDir-lightAngleHalf;
float lightDirLastRaw = light360 ? G.RAD_360-float.Epsilon : lightDir+lightAngleHalf;
float lightDirFirst = G.__144(lightDirFirstRaw);
float lightDirLast = G.__144(lightDirLastRaw);
SortedSet<float> angles = new SortedSet<float>();
angles.Add(lightDirFirst);
if ( angles.Contains(lightDirLast)==false )
angles.Add(lightDirLast);
for ( float angle=lightDirFirstRaw+G.RAD_30 ; angle<lightDirLastRaw ; angle+=G.RAD_30 )
{
float na = G.__144(angle);
if ( angles.Contains(na)==false )
angles.Add(na);
}
if ( m_wallpts!=null )
{
for ( int i=0 ; i<m_wallpts.Count ; i++ )
{
Vec2 p = m_wallpts[i];
float angle = G.__141(lightX, lightY, p.x, p.y);
if ( Mathf.Abs(G.__143(lightDir, angle))<lightAngleHalf )
{
float a = G.__144(angle);
if ( angles.Contains(a)==false )
angles.Add(a);
a = G.__144(angle-0.00001f);
if ( angles.Contains(a)==false )
angles.Add(a);
a = G.__144(angle+0.00001f);
if ( angles.Contains(a)==false )
angles.Add(a);
}
}
}
SortedList<float, SceneWallIntersect> intersects = new SortedList<float, SceneWallIntersect>(new SceneWallCompare());
foreach ( float angle in angles )
{
float lightX2 = lightX + Mathf.Sin(angle)*1000000.0f;
float lightY2 = lightY + Mathf.Cos(angle)*1000000.0f;
bool found = false;
SceneWallIntersect closestIntersect;
closestIntersect.x = 0.0f;
closestIntersect.y = 0.0f;
closestIntersect.length = 0.0f;
closestIntersect.angle = 0.0f;
closestIntersect.first = angle==lightDirFirst;
for ( int i=0 ; i<m_walls.Length ; i++ )
{
SceneWall wall = m_walls[i];
Vec2 pt;
float len;
if ( G.__160((int)lightX, (int)lightY, (int)lightX2, (int)lightY2, (int)wall.m_ax, (int)wall.m_ay, (int)wall.m_bx, (int)wall.m_by, out pt, out len)==false )
continue;
if ( found==false || len<closestIntersect.length )
{
found = true;
closestIntersect.x = pt.x;
closestIntersect.y = pt.y;
closestIntersect.length = len;
}
}
closestIntersect.angle = G.__144(angle);
if ( found )
{
if ( light.m_lightDist.cur>0.0f && closestIntersect.length>light.m_lightDist.cur )
{
closestIntersect.length = light.m_lightDist.cur;
closestIntersect.x = lightX + Mathf.Sin(closestIntersect.angle)*closestIntersect.length;
closestIntersect.y = lightY + Mathf.Cos(closestIntersect.angle)*closestIntersect.length;
}
}
else
{
closestIntersect.length = light.m_lightDist.cur<0.0f ? 1000000.0f : light.m_lightDist.cur;
closestIntersect.x = lightX + Mathf.Sin(closestIntersect.angle)*closestIntersect.length;
closestIntersect.y = lightY + Mathf.Cos(closestIntersect.angle)*closestIntersect.length;
}
if ( intersects.ContainsKey(angle)==false )
intersects.Add(closestIntersect.angle, closestIntersect);
}
if ( intersects.Count<2 )
continue;
Vector3[] vertices = new Vector3[intersects.Count+1];
vertices[0].x = lightX;
vertices[0].y = lightY;
vertices[0].z = 0.0f;
int index = 1;
bool foundFirst = false;
foreach ( KeyValuePair<float, SceneWallIntersect> it in intersects )
{
if ( foundFirst==false && it.Value.first )
foundFirst = true;
if ( foundFirst )
{
vertices[index].x = it.Value.x;
vertices[index].y = it.Value.y;
vertices[index].z = 0.0f;
index++;
}
}
foreach ( KeyValuePair<float, SceneWallIntersect> it in intersects )
{
if ( it.Value.first )
break;
vertices[index].x = it.Value.x;
vertices[index].y = it.Value.y;
vertices[index].z = 0.0f;
index++;
}
int triCount = vertices.Length - 2;
int[] triangles = new int[triCount*3];
for ( int i=0, j=0, k=1 ; i<triCount ; i++, j+=3 )
{
triangles[j] = 0;
triangles[j+2] = k;
triangles[j+1] = ++k;
}
G.__178(ref light.m_lightMesh);
light.m_lightMesh.vertices = vertices;
light.m_lightMesh.triangles = triangles;
}
}
}
public void __42()
{
if ( G.m_game.m_timeline )
G.m_game.m_timeline.__42();
__575();
for ( int i=0 ; i<G.MASK_COUNT ; i++ )
{
LayerSprite layer = m_maskLayers[i];
if ( layer )
layer.Update();
}
m_backLayer.Update();
for ( int i=0 ; i<G.FAR_COUNT ; i++ )
{
LayerSprite layer = m_farLayers[i];
if ( layer )
layer.Update();
}
for ( int i=0 ; i<m_objects.Length ; i++ )
m_objects[i].Update();
m_voiceOverObj.Update();
}
public void __583()
{
for ( int i=0 ; i<m_doors.Length ; i++ )
m_doors[i].__605();
for ( int i=0 ; i<m_labels.Length ; i++ )
m_labels[i].__605();
for ( int i=0 ; i<m_objects.Length ; i++ )
m_objects[i].__605();
__568();
__582();
}
public void __43()
{
bool drawn;
m_bokeh = false;
if ( __545() )
{
float scale = G.m_game.__291().m_renderScale;
float size = m_bokehSize.cur;
if ( m_bokehMaxZoom.cur!=1.0f && size!=1.0f )
{
float s = (scale-1.0f)/(m_bokehMaxZoom.cur-1.0f);
size += (1.0f-size)*s;
size = G.Clamp(size);
}
if ( size>0.0f )
{
m_bokeh = true;
m_bokehShapeScale = size;
m_bokehDepthRT = G.__168(G.m_bokehWidth, G.m_bokehHeight);
m_bokehShapes.Clear();
if ( m_bokehCB==null )
m_bokehCB = new ComputeBuffer(G.BOKEH_MAX, Bokeh.SIZE, ComputeBufferType.Structured);
RenderTexture old = G.__173(m_bokehDepthRT);
G.__171(Color.black);
G.__173(old);
}
}
__587(PLACEMENT.CLEAR);
__585(PLACEMENT.CLEAR);
__586(PLACEMENT.CLEAR);
__584(false);
Rect rc = new Rect(0.0f, 0.0f, m_width, m_height);
__552(ref rc);
m_backLayer.m_curViewRect = rc;
if ( m_backLayer.__428() && __559(ref rc)==false )
{
drawn = G.m_graphics.__359(m_backLayer, ref rc, G.m_game.__285(), FXO.BACK, m_backLayer.m_curOpacity);
if ( drawn && m_bokeh )
{
RenderTexture old = G.__173(m_bokehDepthRT);
G.m_graphics.__359(m_backLayer, ref rc, G.m_game.__285(), FXO.BACK, m_backLayer.m_curOpacity);
G.__173(old);
}
}
G.m_graphics.__353(FXO.SCENE_BACK);
__587(PLACEMENT.BACK);
__585(PLACEMENT.BACK);
__586(PLACEMENT.BACK);
__587(PLACEMENT.OBJECT);
__585(PLACEMENT.OBJECT);
__586(PLACEMENT.OBJECT);
G.m_graphics.__353(FXO.SCENE_OBJECT);
__584(true);
if ( m_bokeh )
{
__588();
G.__170(m_bokehDepthRT);
m_bokehShapes.Clear();
m_bokehDepthRT = null;
m_bokeh = false;
}
__587(PLACEMENT.HB);
__585(PLACEMENT.HB);
__586(PLACEMENT.HB);
__587(PLACEMENT.HA);
__585(PLACEMENT.HA);
__586(PLACEMENT.HA);
__590();
__591();
__592();
__593();
m_lightChanged = false;
}
public void __584(bool mask)
{
FXO fxo, fxos;
LayerSprite layer;
Rect rc;
PLACEMENT placement;
int count = mask ? G.MASK_COUNT : G.FAR_COUNT;
bool drawn;
for ( int iLayer=count-1 ; iLayer>=0 ; iLayer-- )
{
if ( mask )
{
layer = m_maskLayers[iLayer];
fxo = FXO.MA + iLayer;
fxos = FXO.SCENE_MA + iLayer;
placement = PLACEMENT.MA + iLayer;
}
else
{
layer = m_farLayers[iLayer];
fxo = FXO.FA + iLayer;
fxos = FXO.SCENE_FA + iLayer;
placement = PLACEMENT.FA + iLayer;
}
if ( layer && layer.__428() )
{
rc.width = layer.m_width;
rc.height = layer.m_height;
if ( layer.m_speedX==0.0f )
{
float ratio = (m_renderX-m_rcRender.x)/m_rcRender.width;
rc.x = -ratio * (rc.width-m_width) - layer.m_offsetX;
}
else if ( layer.m_speedX==1.0f )
rc.x = -layer.m_offsetX;
else
rc.x = (m_renderX-m_renderHalfWidth)*layer.m_speedX - (m_renderX-m_renderHalfWidth) - layer.m_offsetX;
if ( layer.m_speedY==0.0f )
{
float ratio = (m_renderY-m_rcRender.y)/m_rcRender.height;
rc.y = -ratio * (rc.height-m_height) - layer.m_offsetY;
}
else if ( layer.m_speedY==1.0f )
rc.y = -layer.m_offsetY;
else
rc.y = (m_renderY-m_renderHalfHeight)*layer.m_speedY - (m_renderY-m_renderHalfHeight) - layer.m_offsetY;
if ( layer.m_parallax==0.0f )
{
float renderScale = m_renderScale;
float renderX = m_renderX;
float renderY = m_renderY;
m_renderScale = 1.0f;
m_renderX = m_rcRender.__440();
m_renderY = m_rcRender.__441();
__552(ref rc);
rc.x -= m_rc.x;
rc.y -= m_rc.y;
m_renderScale = renderScale;
m_renderX = renderX;
m_renderY = renderY;
}
else if ( layer.m_parallax==1.0f )
__552(ref rc);
else
{
float renderScale = m_renderScale;
float renderX = m_renderX;
float renderY = m_renderY;
m_renderScale = Mathf.Pow(m_renderScale, layer.m_parallax);
m_renderX = m_rcRender.__440();
m_renderY = m_rcRender.__441();
__552(ref rc);
rc.x -= (renderX - m_renderX)*2.0f;
rc.y -= (renderY - m_renderY)*2.0f;
rc.x -= m_rc.x * (m_renderScale-renderScale);
rc.y -= m_rc.y * (m_renderScale-renderScale);
m_renderScale = renderScale;
m_renderX = renderX;
m_renderY = renderY;
}
if ( layer.m_tileX && rc.x>m_rc.x )
rc.x -= rc.width;
if ( layer.m_tileY && rc.y>m_rc.y )
rc.y -= rc.height;
layer.m_curViewRect = rc;
float startX = rc.x;
while ( rc.y<m_rc.__438() )
{
rc.x = startX;
while ( rc.x<m_rc.__437() )
{
if ( __559(ref rc)==false )
{
drawn = G.m_graphics.__359(layer, ref rc, G.m_game.__285(), fxo, layer.m_curOpacity);
if ( drawn && m_bokeh )
{
RenderTexture old = G.__173(m_bokehDepthRT);
G.m_graphics.__359(layer, ref rc, G.m_game.__285(), fxo, layer.m_curOpacity);
G.__173(old);
}
}
rc.x += rc.width;
if ( layer.m_tileX==false )
break;
}
rc.y += rc.height;
if ( layer.m_tileY==false )
break;
}
}
__587(placement);
__585(placement);
__586(placement);
G.m_graphics.__353(fxos);
}
}
public void __585(PLACEMENT placement)
{
List<SceneEntity> list = m_sortedEntities[(int)placement];
if ( list==null )
return;
for ( int iEntity=0 ; iEntity<list.Count ; iEntity++ )
{
SceneEntity entity = list[iEntity];
switch ( entity.m_entity )
{
case ENTITY.OBJ:
{
SceneObj sceneObj = (SceneObj)entity;
if ( sceneObj==G.m_game.m_dragObj )
continue;
break;
}
}
entity.__43();
if ( m_bokeh )
{
if ( entity.__608() )
{
float x = __549(entity.__35());
float y = __550(entity.__36());
int xi = Mathf.RoundToInt(x);
int yi = Mathf.RoundToInt(G.m_rcView.height - y - 1.0f);
int xi2 = (int)(x/G.BOKEH_SIZE);
int yi2 = (int)(y/G.BOKEH_SIZE);
Bokeh shape;
shape.pos = (uint)(((yi&0xFFFF)<<16) | (xi&0xFFFF));
shape.posInDepth = (uint)(((yi2&0xFFFF)<<16) | (xi2&0xFFFF));
m_bokehShapes.Add(shape);
Rect rc;
rc.width = G.BOKEH_SIZE;
rc.height = G.BOKEH_SIZE;
rc.x = x - rc.width*0.5f;
rc.y = y - rc.height*0.5f;
RenderTexture old = G.__173(m_bokehDepthRT);
G.m_materialBokeh.color = G.m_colorClear;
G.m_graphics.__355(G.m_materialBokeh, ref rc);
G.__173(old);
}
else
{
RenderTexture old = G.__173(m_bokehDepthRT);
entity.__43();
G.__173(old);
}
}
}
}
public void __586(PLACEMENT placement)
{
if ( m_lightCount==0 )
return;
int ip = (int)placement;
List<SceneObj> lights = m_lights[ip];
if ( lights==null || lights.Count==0 )
return;
if ( m_lightBaked && m_lightRTs[ip] && m_lightChanges[ip]==false && m_lightChanged==false )
{
G.m_materialLightFinal.mainTexture = m_lightRTs[ip];
G.m_graphics.__354(G.m_materialLightFinal);
if ( m_bokeh )
{
RenderTexture old = G.__173(m_bokehDepthRT);
G.m_graphics.__354(G.m_materialLightFinal);
G.__173(old);
}
G.m_materialLightFinal.mainTexture = null;
return;
}
m_lightChanges[ip] = false;
bool hasBlur;
float blur = __535(out hasBlur);
RenderTexture rt = G.__168(m_lightLow ? TEXTURESCALE.QUARTER : TEXTURESCALE.FULL);
RenderTexture main = null;
RenderTexture final;
if ( m_lightBaked )
{
if ( m_lightRTs[ip]==null )
m_lightRTs[ip] = G.__167(m_lightLow ? TEXTURESCALE.QUARTER : TEXTURESCALE.FULL);
final = m_lightRTs[ip];
main = G.__173(rt);
}
else
{
final = G.__173(rt);
}
G.__172();
if ( m_lightDiffuses[ip] )
{
for ( int iLight=0 ; iLight<lights.Count ; iLight++ )
{
SceneObj light = lights[iLight];
if ( light.m_lightMesh==null )
continue;
G.m_graphics.__346(light);
G.m_materialLightDiffuse.color = light.m_lightDiffuse.cur;
G.m_materialLightDiffuse.SetFloat("_x", light.__35());
G.m_materialLightDiffuse.SetFloat("_y", light.__36());
G.m_materialLightDiffuse.SetFloat("_dist", light.m_lightDist.cur);
G.m_materialLightDiffuse.SetFloat("_attn", light.m_lightAttn.cur);
G.m_graphics.__365(light.m_lightMesh, G.m_materialLightDiffuse);
}
}
if ( m_lightAmbients[ip] )
{
RenderTexture rt2 = G.__168(m_lightLow ? TEXTURESCALE.QUARTER : TEXTURESCALE.FULL);
G.__173(rt2);
Color ambient = new Color(0.0f, 0.0f, 0.0f, __534());
G.__171(ambient);
for ( int iLight=0 ; iLight<lights.Count ; iLight++ )
{
SceneObj light = lights[iLight];
if ( light.m_lightMesh==null )
continue;
G.m_graphics.__346(light);
G.m_materialLightAmbient.SetFloat("_x", light.__35());
G.m_materialLightAmbient.SetFloat("_y", light.__36());
G.m_materialLightAmbient.SetFloat("_dist", light.m_lightDist.cur);
G.m_materialLightAmbient.SetFloat("_attn", light.m_lightAttn.cur);
G.m_materialLightAmbient.SetFloat("_ambient", light.m_lightAmbient.cur);
G.m_graphics.__365(light.m_lightMesh, G.m_materialLightAmbient);
}
G.__173(rt);
G.m_materialLightAmbientFinal.mainTexture = rt2;
G.m_graphics.__354(G.m_materialLightAmbientFinal);
G.m_materialLightAmbientFinal.mainTexture = null;
G.__170(rt2);
}
if ( hasBlur )
G.__176(blur, null, m_lightLow ? TEXTURESCALE.QUARTER : TEXTURESCALE.FULL);
G.__173(final);
if ( m_lightBaked )
G.__172();
G.m_materialLightFinal.mainTexture = rt;
G.m_graphics.__354(G.m_materialLightFinal);
if ( m_bokeh )
{
RenderTexture old = G.__173(m_bokehDepthRT);
G.m_graphics.__354(G.m_materialLightFinal);
G.__173(old);
}
G.m_materialLightFinal.mainTexture = null;
G.__170(rt);
if ( m_lightBaked )
{
G.__173(main);
G.m_materialLightFinal.mainTexture = final;
G.m_graphics.__354(G.m_materialLightFinal);
if ( m_bokeh )
{
RenderTexture old = G.__173(m_bokehDepthRT);
G.m_graphics.__354(G.m_materialLightFinal);
G.__173(old);
}
G.m_materialLightFinal.mainTexture = null;
}
}
public void __587(PLACEMENT placement)
{
List<SceneLabel> list = m_sortedLabels[(int)placement];
if ( list==null )
return;
for ( int i=0 ; i<list.Count ; i++ )
list[i].__43();
}
public void __588()
{
if ( m_bokehShapes.Count==0 )
return;
ComputeShader cs = G.__164(COMPUTE.BOKEH);
Vector2Int size = G.__166();
RenderTexture rt = G.__174();
RenderTexture output = G.__169();
cs.SetInt("g_width", size.x);
cs.SetInt("g_height", size.y);
cs.SetInt("g_shapeCount", m_bokehShapes.Count);
cs.SetFloat("g_shapeWidth", m_bokehShapeWidth*m_bokehShapeScale);
cs.SetFloat("g_shapeHeight", m_bokehShapeHeight*m_bokehShapeScale);
cs.SetTexture(0, "g_input", rt);
cs.SetTexture(0, "g_depth", m_bokehDepthRT);
cs.SetTexture(0, "g_output", output);
m_bokehCB.SetData(m_bokehShapes);
cs.SetBuffer(0, "g_shapes", m_bokehCB);
G.Dispatch(cs);
G.__173(output);
G.__176(16);
G.__173(rt);
G.__71(output);
G.__170(output);
}
public void __589()
{
for ( int i=0 ; i<m_sortedEntities.Length ; i++ )
{
List<SceneEntity> list = m_sortedEntities[i];
if ( list!=null )
{
for ( int j=0 ; j<list.Count ; j++ )
{
if ( list[j].__606() )
((SceneObj)list[j]).__589();
}
}
}
m_voiceOverObj.__589();
if ( G.m_game.m_timeline )
G.m_game.m_timeline.__1001();
}
public void __590()
{
if ( G.m_game.m_cheatUsed==false || Input.GetMouseButton(2)==false || G.m_game.__253()==false )
return;
float width = G.m_game.m_cursorHalfSize;
float height = G.m_game.m_cursorHalfSize;
Rect rc = new Rect();
rc.width = width;
rc.height = height;
float dx, dy;
float time = G.m_game.m_time;
Sprite cheat = G.m_game.m_uiCheatLabel ? G.m_game.m_uiCheatLabel : G.m_game.m_uiCheat;
if ( cheat )
{
for ( int i=0 ; i<m_labels.Length ; i++ )
{
SceneLabel label = m_labels[i];
if ( label.m_visible.cur==false || label.m_enabled.cur==false || label.m_cheat.cur==false || label.m_drag!=DRAG.NONE )
continue;
dx = (G.Clamp(Mathf.PerlinNoise(time, 0.0f)) * 2.0f - 1.0f) * G.PATH_GRID_CELLSIZE_HALF;
dy = (G.Clamp(Mathf.PerlinNoise(0.0f, time)) * 2.0f - 1.0f) * G.PATH_GRID_CELLSIZE_HALF;
time += 0.5f;
Vec2 pos = label.m_rc.__439();
rc.x = pos.x - width*0.5f + dx;
rc.y = pos.y - height*0.5f + dy;
G.m_graphics.__355(cheat.m_material, ref rc);
}
}
cheat = G.m_game.m_uiCheatObject ? G.m_game.m_uiCheatObject : G.m_game.m_uiCheat;
if ( cheat )
{
SceneObj playerSceneObj = G.m_game.__295();
for ( int i=0 ; i<m_sortedEntities.Length ; i++ )
{
List<SceneEntity> list = m_sortedEntities[i];
if ( list!=null )
{
for ( int j=0 ; j<list.Count ; j++ )
{
SceneEntity entity = list[j];
if ( entity.__606()==false )
continue;
SceneObj sceneObj = (SceneObj)entity;
if ( sceneObj.m_cheat.cur==false || sceneObj.m_visible.cur==false || sceneObj==playerSceneObj || sceneObj.m_drag!=DRAG.NONE )
continue;
if ( sceneObj.m_obj.m_enabled.cur==false || sceneObj.__473()==false )
continue;
dx = (G.Clamp(Mathf.PerlinNoise(time, 0.0f)) * 2.0f - 1.0f) * G.PATH_GRID_CELLSIZE_HALF;
dy = (G.Clamp(Mathf.PerlinNoise(0.0f, time)) * 2.0f - 1.0f) * G.PATH_GRID_CELLSIZE_HALF;
time += 0.5f;
Vec2 pos = sceneObj.__665();
rc.x = pos.x - width*0.5f + dx;
rc.y = pos.y - height*0.5f + dy;
G.m_graphics.__355(cheat.m_material, ref rc);
}
}
}
}
cheat = G.m_game.m_uiCheatDoor ? G.m_game.m_uiCheatDoor : G.m_game.m_uiCheat;
if ( cheat )
{
for ( int i=0 ; i<m_doors.Length ; i++ )
{
SceneDoor door = m_doors[i];
if ( door.m_visible.cur==false || door.m_cheat.cur==false )
continue;
dx = (G.Clamp(Mathf.PerlinNoise(time, 0.0f)) * 2.0f - 1.0f) * G.PATH_GRID_CELLSIZE_HALF;
dy = (G.Clamp(Mathf.PerlinNoise(0.0f, time)) * 2.0f - 1.0f) * G.PATH_GRID_CELLSIZE_HALF;
time += 0.5f;
Vec2 pos = door.m_rc.__439();
rc.x = pos.x - width*0.5f + dx;
rc.y = pos.y - height*0.5f + dy;
G.m_graphics.__355(cheat.m_material, ref rc);
}
}
}
public void __591()
{
if ( G.m_game.__310()==false )
return;
MenuDialog dialog = G.m_game.m_menuDialog;
SceneObj speaker = dialog.m_speaker;
if ( speaker==null )
return;
Obj obj = speaker.m_obj;
if ( obj==null )
return;
Obj avatar = obj.m_avatar;
if ( obj.m_avatar==null )
return;
string animName = G.m_game.__311() ? "STOP" : "TALK";
Anim anim = avatar.__471(ref animName);
if ( anim==null || anim.m_maxFrameCount==0 )
{
anim = avatar.__471("STOP");
if ( anim==null || anim.m_maxFrameCount==0 )
return;
}
if ( anim!=dialog.m_avatarAnim )
{
dialog.m_avatarAnim = anim;
dialog.m_avatarIndex = 0;
}
AnimDir dir = anim.m_defaultDir;
if ( dir==null || dir.__476()==0 )
return;
dialog.m_avatarTime += G.m_game.m_elapsed;
if ( dialog.m_avatarTime>=anim.m_fpsInv )
{
dialog.m_avatarTime = 0.0f;
if ( anim.m_profile!=PROFILE.NONE )
{
dialog.m_avatarProfileIndex++;
int iFrame = dialog.m_avatarProfileFrames[dialog.m_avatarProfileIndex];
if ( iFrame!=-1 )
{
dialog.m_avatarIndex = iFrame<dir.__476() ? iFrame : 0;
}
else
{
int frameCount = 0;
switch ( anim.m_profile )
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
int curPose = dialog.m_avatarIndex/step;
int newPose = 0;
if ( curPose==0 )
{
switch ( anim.m_profile )
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
dialog.m_avatarProfileIndex = 0;
dialog.m_avatarProfileFrames[frameCount] = -1;
for ( int i=0 ; i<frameCount ; i++ )
{
while ( true )
{
int index = newPose*step + G.__156(step);
if ( i>0 && index==dialog.m_avatarProfileFrames[i-1] )
continue;
if ( anim.m_profile==PROFILE.NEUTRAL && i>0 && dialog.m_avatarProfileFrames[i-1]%step==index%step )
continue;
dialog.m_avatarProfileFrames[i] = index;
break;
}
}
dialog.m_avatarIndex = dialog.m_avatarProfileFrames[0]==-1 ? 0 : dialog.m_avatarProfileFrames[0];
}
}
else
{
dialog.m_avatarIndex++;
int loopRangeMax = anim.m_loopRangeMax==-1 ? dir.__476()-1 : anim.m_loopRangeMax;
if ( dialog.m_avatarIndex>loopRangeMax && dialog.m_avatarLoopRange!=-1 )
{
if ( anim.m_loopRangeCount==-1 )
dialog.m_avatarIndex = anim.m_loopRangeMin;
else if ( anim.m_loopRangeCount>0 )
{
dialog.m_avatarLoopRange++;
if ( dialog.m_avatarLoopRange<=anim.m_loopRangeCount )
dialog.m_avatarIndex = anim.m_loopRangeMin;
else
dialog.m_avatarLoopRange = -1;
}
else
dialog.m_avatarLoopRange = -1;
}
if ( dialog.m_avatarIndex>=dir.__476() )
{
dialog.m_avatarIndex = 0;
dialog.m_avatarLoopRange = 0;
}
}
}
Frame frame = dir.m_frames[dialog.m_avatarIndex];
if ( frame==null )
return;
if ( avatar.m_avatarOpacity>0.0f )
{
G.m_materialBrush.color = new Color(0.0f, 0.0f, 0.0f, avatar.m_avatarOpacity);
G.m_graphics.__355(G.m_materialBrush, ref G.m_rcViewUI);
}
Rect rc = avatar.m_avatarImage;
__552(ref rc, true);
if ( frame.m_layer==-1 )
G.m_graphics.__360(frame, ref rc);
else
G.m_graphics.__361(avatar.m_tint.cur, frame, ref rc);
}
public void __592()
{
if ( G.m_game.m_balloon==false || G.m_game.m_uiBalloon==null )
return;
for ( int i=0 ; i<m_sortedEntities.Length ; i++ )
{
List<SceneEntity> list = m_sortedEntities[i];
if ( list!=null )
{
for ( int j=0 ; j<list.Count ; j++ )
{
if ( list[j].__606() )
((SceneObj)list[j]).__589(true);
}
}
}
}
public void __593()
{
if ( G.m_game.m_dragObj==null )
return;
SceneObj dragObj = G.m_game.m_dragObj;
DrawInfo info;
dragObj.__663(out info);
if ( __547() )
dragObj.m_draw.obb.__442(ref G.m_game.m_timeDragIcon, ref G.m_game.m_iRenderDragIcon, G.m_game.m_cursorViewX, G.m_game.m_cursorViewY);
dragObj.__43();
dragObj.__664(ref info);
}
}
