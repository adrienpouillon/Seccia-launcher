using UnityEngine;
public class InputManager
{
public Layout m_layout;
public float m_xViewReal = 0.0f;
public float m_yViewReal = 0.0f;
float m_xMagnifyStart = 0.0f;
float m_yMagnifyStart = 0.0f;
public bool m_hasMagnify = false;
public bool m_isDown = false;
public float m_isDownTime = 0.0f;
public float m_stationaryTime = 0.0f;
public float m_arrowTime = 0.0f;
public bool m_isAltDown = false;
public float m_isAltDownTime = 0.0f;
public Touch m_touch;
public static implicit operator bool(InputManager inst) { return inst!=null; }
public void Reset()
{
m_isDown = false;
m_isDownTime = 0.0f;
m_stationaryTime = G.m_game.m_time;
m_hasMagnify = false;
m_arrowTime = 0.0f;
m_isAltDown = false;
m_isAltDownTime = 0.0f;
}
public bool __368()
{
return m_isDown && G.m_game.m_time-m_isDownTime>G.SKIP_DELAY;
}
public void __42()
{
bool isMove = false;
bool callAction = false;
bool callAltAction = false;
bool skipDialog = false;
bool dropRequest = false;
bool mouseLeftButton = false;
bool mouseRightButton = false;
if ( G.m_mouseCaps )
{
mouseLeftButton = Input.GetMouseButton(0);
mouseRightButton = Input.GetMouseButton(1);
}
bool isTouchBegin = false;
bool isTouchEnd = false;
bool touchScrollAvailable = false;
int touchCount = 0;
if ( G.m_touchCaps )
{
touchCount = Input.touchCount;
if ( touchCount>0 )
m_touch = Input.GetTouch(0);
}
switch ( G.m_controller )
{
case CONTROLLER.MOUSE:
{
if ( touchCount>0 )
{
G.m_controller = CONTROLLER.TOUCH;
Reset();
}
break;
}
case CONTROLLER.TOUCH:
{
if ( G.m_mouseCaps && (mouseLeftButton || mouseRightButton) )
{
G.m_controller = CONTROLLER.MOUSE;
Reset();
}
break;
}
}
switch ( G.m_controller )
{
case CONTROLLER.MOUSE:
{
if ( m_isDown  )
{
if ( mouseLeftButton==false && G.m_game.m_time-m_isDownTime>0.1f )
{
m_isDown = false;
m_isDownTime = 0.0f;
if ( G.m_game.m_dragObj )
dropRequest = true;
}
}
else
{
if ( mouseLeftButton )
{
m_isDown = true;
m_isDownTime = G.m_game.m_time;
callAction = true;
}
}
if ( G.m_game.m_configDebug && G.m_game.m_menuDialog.__38() && G.m_game.m_menuDialog.m_speaker )
{
if ( Input.GetKey(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) )
skipDialog = true;
}
break;
}
case CONTROLLER.TOUCH:
{
if ( G.m_game.m_gestureMenuLocked==false && touchCount>=4 )
{
m_isDown = false;
m_isDownTime = 0.0f;
G.m_game.__252(false);
return;
}
if ( G.m_game.m_configDebug && G.m_game.m_menuDialog.__38() && G.m_game.m_menuDialog.m_speaker )
{
if ( touchCount==3 )
skipDialog = true;
}
if ( touchCount==0 )
{
m_isDown = false;
m_isDownTime = 0.0f;
}
else
{
switch ( m_touch.phase )
{
case TouchPhase.Ended:
{
callAction = true;
isTouchEnd = true;
m_isDown = false;
m_isDownTime = 0.0f;
if ( G.m_game.m_dragObj )
dropRequest = true;
break;
}
case TouchPhase.Began:
case TouchPhase.Moved:
case TouchPhase.Stationary:
{
touchScrollAvailable = true;
if ( m_touch.phase==TouchPhase.Began )
{
callAction = true;
isTouchBegin = true;
m_arrowTime = G.m_game.m_time;
}
else if ( m_touch.phase==TouchPhase.Moved )
isMove = true;
if ( m_isDown==false )
{
m_isDown = true;
m_isDownTime = G.m_game.m_time;
}
break;
}
default:
{
m_isDown = false;
m_isDownTime = 0.0f;
break;
}
}
}
break;
}
}
switch ( G.m_controller )
{
case CONTROLLER.MOUSE:
{
if ( m_isAltDown )
{
if ( mouseRightButton==false && G.m_game.m_time-m_isAltDownTime>0.1f )
{
m_isAltDown = false;
m_isAltDownTime = 0.0f;
}
}
else
{
if ( mouseRightButton )
{
m_isAltDown = true;
m_isAltDownTime = G.m_game.m_time;
callAltAction = true;
}
}
break;
}
}
bool coords = false;
switch ( G.m_controller )
{
case CONTROLLER.MOUSE:
{
float x = ((Input.mousePosition.x-G.m_rcClient.x)/G.m_rcClient.width) * G.m_rcView.width;
float y = ((G.m_rcWindow.height-Input.mousePosition.y-G.m_rcClient.y)/G.m_rcClient.height) * G.m_rcView.height;
x = (float)G.Clamp((int)x, 0, (int)(G.m_rcView.width-1.0f));
y = (float)G.Clamp((int)y, 0, (int)(G.m_rcView.height-1.0f));
if ( x!=m_xViewReal || y!=m_yViewReal )
{
isMove = true;
m_stationaryTime = G.m_game.m_time;
}
m_xViewReal = x;
m_yViewReal = y;
coords = true;
break;
}
case CONTROLLER.TOUCH:
{
if ( touchCount>0 )
{
Vector2 pos = m_touch.position;
float x = ((pos.x-G.m_rcClient.x)/G.m_rcClient.width) * G.m_rcView.width;
float y = ((G.m_rcWindow.height-pos.y-G.m_rcClient.y)/G.m_rcClient.height) * G.m_rcView.height;
x = (float)G.Clamp((int)x, 0, (int)(G.m_rcView.width-1.0f));
y = (float)G.Clamp((int)y, 0, (int)(G.m_rcView.height-1.0f));
if ( Mathf.Abs(x-m_xViewReal)>5.0f || Mathf.Abs(y-m_yViewReal)>5.0f )
m_stationaryTime = G.m_game.m_time;
m_xViewReal = x;
m_yViewReal = y;
coords = true;
}
break;
}
}
bool keepMagnifyCoords = false;
LayoutCtrl magnify = G.m_game.m_layout.Get(LAYOUT_CTRL.MAGNIFY);
#if UNITY_EDITOR
if ( magnify.m_active==false )
#else
if ( magnify.m_active==false || G.m_controller!=CONTROLLER.TOUCH )
#endif
m_hasMagnify = false;
else
{
if ( m_hasMagnify )
{
if ( m_isDown==false )
{
m_hasMagnify = false;
keepMagnifyCoords = isTouchEnd;
}
else if ( touchCount==2 )
{
m_xMagnifyStart = m_xViewReal;
m_yMagnifyStart = m_yViewReal;
}
}
else
{
if ( m_isDown && (G.m_game.m_time-m_isDownTime)>G.MAGNIFY_DURATION && (G.m_game.m_time-m_stationaryTime)>G.MAGNIFY_DURATION )
{
m_hasMagnify = true;
m_xMagnifyStart = m_xViewReal;
m_yMagnifyStart = m_yViewReal;
}
}
}
if ( m_hasMagnify || keepMagnifyCoords )
{
float xMagnify = m_xMagnifyStart + (float)(int)((m_xViewReal-m_xMagnifyStart)*magnify.m_invZoom);
float yMagnify = m_yMagnifyStart + (float)(int)((m_yViewReal-m_yMagnifyStart)*magnify.m_invZoom);
G.m_game.m_cursorViewX = xMagnify;
G.m_game.m_cursorViewY = yMagnify;
G.m_game.m_entityCursor.m_local.cur.x = xMagnify;
G.m_game.m_entityCursor.m_local.cur.y = yMagnify;
G.m_game.m_rcCursor.x = xMagnify - G.m_game.m_cursorHalfSize;
G.m_game.m_rcCursor.y = yMagnify - G.m_game.m_cursorHalfSize;
}
else
{
G.m_game.m_cursorViewX = m_xViewReal;
G.m_game.m_cursorViewY = m_yViewReal;
G.m_game.m_entityCursor.m_local.cur.x = m_xViewReal;
G.m_game.m_entityCursor.m_local.cur.y = m_yViewReal;
G.m_game.m_rcCursor.x = m_xViewReal - G.m_game.m_cursorHalfSize;
G.m_game.m_rcCursor.y = m_yViewReal - G.m_game.m_cursorHalfSize;
}
G.m_game.m_rcCursor.width = G.m_game.m_cursorSize;
G.m_game.m_rcCursor.height = G.m_game.m_cursorSize;
G.m_game.m_cursor = coords;
if ( G.m_game.m_cursorObj )
{
for ( int i=0 ; i<G.ICON_COUNT ; i++ )
{
if ( G.m_game.m_cursorObj.m_icons[i] )
G.m_game.m_cursorObj.m_icons[i].Set(ref G.m_game.m_rcCursor);
}
}
if ( touchScrollAvailable && G.m_game.m_cursorObj && m_layout.Get(LAYOUT_CTRL.ITEMS).m_hasArrows && m_layout.Get(LAYOUT_CTRL.ITEMS).__428() && G.m_game.m_time-m_arrowTime>0.75f )
{
Player player = G.m_game.__293();
if ( player )
{
if ( m_layout.Get(LAYOUT_CTRL.ITEMS).__430(G.m_game.m_cursorViewX, G.m_game.m_cursorViewY) )
{
player.m_scroll--;
player.__486();
m_arrowTime = G.m_game.m_time;
}
if ( m_layout.Get(LAYOUT_CTRL.ITEMS).__431(G.m_game.m_cursorViewX, G.m_game.m_cursorViewY) )
{
player.m_scroll++;
player.__486();
m_arrowTime = G.m_game.m_time;
}
}
}
if ( dropRequest )
{
__329();
return;
}
if ( callAction )
__458(isTouchBegin);
else if ( callAltAction )
OnAltAction();
if ( MessageBox.m_instance.__38() )
return;
if ( skipDialog )
{
G.m_game.m_menuDialog.m_speaker.__655();
G.m_game.m_menuDialog.m_iScroll = -1;
}
if ( G.m_game.m_cinematicPlayer.__38() && G.m_game.m_cinematicPlayer.m_cinematic.m_skip==CINEMATICSKIP.DELAY && __368() )
G.m_game.m_cinematicPlayer.__41();
Router routerHud = G.m_game.__296();
if ( routerHud )
{
Player player = G.m_game.__293();
if ( player && player.m_hasShowPath && player.m_sceneObj )
{
if ( G.m_rcView.Contains(G.m_game.m_cursorViewX, G.m_game.m_cursorViewY) )
{
if ( callAction || callAltAction || isMove )
__369();
else if ( player.m_sceneObj.m_routeStatus==Router.FOUND && player.m_sceneObj.__656()==false && player.m_sceneObj.m_routeTurn==false )
__369();
}
else
routerHud.__515();
}
}
}
public void __369()
{
Router routerHud = G.m_game.__296();
if ( routerHud==null )
return;
Player player = G.m_game.__293();
if ( player==null || player.m_hasShowPath==false || player.m_sceneObj==null )
return;
if ( G.m_rcView.Contains(G.m_game.m_cursorViewX, G.m_game.m_cursorViewY)==false )
return;
Vec2 pt = G.m_game.__299();
bool exist;
SceneCell cell = player.m_sceneObj.__639(pt.x, pt.y, out exist);
if ( cell==null )
return;
{
if ( routerHud.__516(player.m_sceneObj.__35(), player.m_sceneObj.__36(), cell.__35(), cell.__36())==Router.FOUND )
{
}
}
}
private void __458(bool isTouchBegin)
{
bool abort = false;
if ( MessageBox.m_instance.__38() )
{
if ( isTouchBegin==false )
MessageBox.m_instance.__458(G.m_game.m_cursorViewX, G.m_game.m_cursorViewY);
return;
}
if ( G.m_game.m_cinematicPlayer.__38() )
{
if ( isTouchBegin==false && G.m_game.m_cinematicPlayer.m_cinematic.m_skip==CINEMATICSKIP.CLICK )
G.m_game.m_cinematicPlayer.__41();
return;
}
if ( G.m_game.m_menuGame.__38() )
{
if ( isTouchBegin==false )
G.m_game.m_menuGame.__458(G.m_game.m_cursorViewX, G.m_game.m_cursorViewY);
return;
}
if ( G.m_game.m_menuDialog.__38() )
{
if ( isTouchBegin==false )
G.m_game.m_menuDialog.__458(G.m_game.m_cursorViewX, G.m_game.m_cursorViewY);
return;
}
if ( isTouchBegin==false )
G.m_game.__324();
Scene scene = G.m_game.__291();
if ( scene==null )
return;
if ( G.m_game.__222() )
{
if ( isTouchBegin==false )
{
Frame frame = G.m_game.m_examine;
G.m_game.m_examine = null;
bool playable = G.m_game.__253();
G.m_game.m_examine = frame;
if ( playable )
G.m_game.__223(true);
}
return;
}
if ( G.m_game.__253()==false )
return;
Player player = G.m_game.__293();
if ( player==null || player.m_sceneObj==null )
return;
SceneObj playerSceneObj = player.m_sceneObj;
if ( m_layout.__426(LAYOUT_CTRL.MENU, G.m_game.m_cursorViewX, G.m_game.m_cursorViewY) )
{
if ( isTouchBegin==false )
G.m_game.__257();
return;
}
if ( m_layout.m_bagLocked==false && m_layout.__426(LAYOUT_CTRL.BAG, G.m_game.m_cursorViewX, G.m_game.m_cursorViewY) )
{
if ( isTouchBegin==false )
{
m_layout.m_bagOpened = !m_layout.m_bagOpened;
G.m_game.__314();
}
return;
}
if ( m_layout.__426(LAYOUT_CTRL.DETACH, G.m_game.m_cursorViewX, G.m_game.m_cursorViewY) )
{
if ( isTouchBegin==false && G.m_game.m_cursorObj )
{
m_layout.m_bagOpened = false;
CallDetachEvent(player, G.m_game.m_cursorObj);
}
return;
}
for ( int i=0 ; i<8 ; i++ )
{
if ( m_layout.__426(LAYOUT_CTRL.USER1+i, G.m_game.m_cursorViewX, G.m_game.m_cursorViewY) )
{
if ( isTouchBegin==false )
G.m_game.__323(RoleBoxEventLayout.ID, (i+1).ToString());
return;
}
}
bool emptyCell;
Obj objHit = player.__432(G.m_game.m_cursorViewX, G.m_game.m_cursorViewY, out emptyCell);
if ( objHit )
{
if ( G.m_game.m_cursorObj==null )
{
if ( G.m_game.m_useLocked )
G.m_game.__325(objHit, null);
else
{
G.m_game.m_cursorObj = objHit;
for ( int i=0 ; i<G.ICON_COUNT ; i++ )
{
if ( objHit.m_icons[i] )
objHit.m_icons[i].Set(0, 0, 0, 0);
}
}
}
else
{
if ( isTouchBegin==false )
{
if ( objHit!=G.m_game.m_cursorObj )
{
m_layout.m_bagOpened = false;
CallUseEvent(player, G.m_game.m_cursorObj, objHit, null, null, true, null);
}
}
}
return;
}
if ( emptyCell )
return;
if ( m_layout.Get(LAYOUT_CTRL.ITEMS).__430(G.m_game.m_cursorViewX, G.m_game.m_cursorViewY) )
{
if ( isTouchBegin==false )
{
player.m_scroll--;
player.__486();
}
return;
}
if ( m_layout.Get(LAYOUT_CTRL.ITEMS).__431(G.m_game.m_cursorViewX, G.m_game.m_cursorViewY) )
{
if ( isTouchBegin==false )
{
player.m_scroll++;
player.__486();
}
return;
}
Player playerHit = m_layout.__427(G.m_game.m_cursorViewX, G.m_game.m_cursorViewY);
if ( playerHit )
{
if ( isTouchBegin==false )
{
if ( G.m_game.m_cursorObj==null )
G.m_game.__304(playerHit.m_uid);
else if ( playerHit.m_obj )
CallUseEvent(player, G.m_game.m_cursorObj, playerHit.m_obj, null, null, false, null);
}
return;
}
if ( m_layout.Get(LAYOUT_CTRL.PLAYERS).__430(G.m_game.m_cursorViewX, G.m_game.m_cursorViewY) )
{
if ( isTouchBegin==false )
{
m_layout.m_playerScroll--;
m_layout.__420();
}
return;
}
if ( m_layout.Get(LAYOUT_CTRL.PLAYERS).__431(G.m_game.m_cursorViewX, G.m_game.m_cursorViewY) )
{
if ( isTouchBegin==false )
{
m_layout.m_playerScroll++;
m_layout.__420();
}
return;
}
if ( isTouchBegin )
{
if ( G.m_game.m_dragObj==null )
{
SceneEntity dragEntity;
SubObj dragSub;
scene.__426(out dragEntity, out dragSub, G.m_game.m_cursorViewX, G.m_game.m_cursorViewY, true, DRAG.SOURCE);
if ( dragEntity )
G.m_game.__328((SceneObj)dragEntity, dragSub);
}
return;
}
m_layout.m_bagOpened = false;
SceneEntity entity;
SubObj sub;
if ( scene.__426(out entity, out sub, G.m_game.m_cursorViewX, G.m_game.m_cursorViewY, G.m_game.m_cursorObj==null, DRAG.NONE)==false )
return;
if ( entity==null && G.m_game.m_dragObj==null )
{
SceneEntity dragEntity;
SubObj dragSub;
if ( scene.__426(out dragEntity, out dragSub, G.m_game.m_cursorViewX, G.m_game.m_cursorViewY, true, DRAG.SOURCE)==false )
return;
if ( dragEntity )
{
G.m_game.__328((SceneObj)dragEntity, dragSub);
return;
}
}
if ( entity )
{
if ( entity.__606() )
{
SceneObj sceneObj = (SceneObj)entity;
if ( sceneObj==playerSceneObj )
{
if ( G.m_game.m_cursorObj )
{
CallUseEvent(player, G.m_game.m_cursorObj, sceneObj.m_obj, sceneObj, sub, false, null);
return;
}
}
else
{
if ( G.m_game.m_cursorObj==null )
{
SceneCell cell = playerSceneObj.__642(sceneObj);
if ( cell==null )
{
playerSceneObj.__651();
G.m_game.__325(sceneObj.m_obj, sub);
}
else
{
Message msg = new Message();
msg.m_type = Message.SELECT;
msg.m_player = player;
msg.m_sceneObj = sceneObj;
msg.m_subObj = sub;
SceneCellLink link = cell.__604(LINK.SELECT);
if ( link.m_puzzle==0 || G.m_game.m_scenario.__522(link.m_puzzle) )
{
msg.m_dist = link.m_dist;
if ( msg.m_dist==0.0f )
{
msg.m_anim = player.m_obj.__471(ref link.m_anim);
msg.m_dir = link.m_dir;
}
}
msg.m_state = Message.S_MOVE;
playerSceneObj.__649(cell.__35(), cell.__36(), msg);
}
}
else
{
CallUseEvent(player, G.m_game.m_cursorObj, sceneObj.m_obj, sceneObj, sub, false, null);
}
return;
}
}
else
{
SceneLabel label = (SceneLabel)entity;
if ( G.m_game.m_cursorObj==null )
{
SceneCell cell = playerSceneObj.__642(label);
if ( cell==null )
{
playerSceneObj.__651();
scene.__561(label);
}
else
{
Message msg = new Message();
msg.m_player = player;
msg.m_type = Message.LABEL;
msg.m_label = label;
SceneCellLink link = cell.__604(LINK.LABEL);
if ( link.m_puzzle==0 || G.m_game.m_scenario.__522(link.m_puzzle) )
{
msg.m_dist = link.m_dist;
msg.m_anim = player.m_obj.__471(ref link.m_anim);
msg.m_dir = link.m_dir;
}
msg.m_state = Message.S_MOVE;
playerSceneObj.__649(cell.__35(), cell.__36(), msg);
}
}
else
{
if ( label.__473() )
CallUseEvent(player, G.m_game.m_cursorObj, null, null, null, false, label);
else
G.m_game.m_cursorObj = null;
}
return;
}
}
if ( G.m_game.m_cursorObj )
{
G.m_game.m_cursorObj = null;
return;
}
SceneDoor door = scene.__546(ref abort, G.m_game.m_cursorViewX, G.m_game.m_cursorViewY);
if ( abort )
return;
if ( door )
{
SceneCell cell = playerSceneObj.__642(door);
if ( cell==null )
{
playerSceneObj.__651();
scene.__563(door);
}
else
{
Message curMsg = playerSceneObj.m_message;
if ( curMsg && curMsg.m_type==Message.DOOR && curMsg.m_door==door )
{
scene.__563(door, true);
}
else
{
Message msg = new Message();
msg.m_player = player;
msg.m_type = Message.DOOR;
msg.m_door = door;
SceneCellLink link = cell.__604(LINK.DOOR);
if ( link.m_puzzle==0 || G.m_game.m_scenario.__522(link.m_puzzle) )
{
msg.m_dist = link.m_dist;
msg.m_anim = player.m_obj.__471(ref link.m_anim);
msg.m_dir = link.m_dir;
}
msg.m_state = Message.S_MOVE;
playerSceneObj.__649(cell.__35(), cell.__36(), msg);
}
}
return;
}
Vec2 pt = G.m_game.__299();
if ( scene.__560(pt.x, pt.y) )
return;
Player curPlayer = G.m_game.__293();
if ( curPlayer==player && curPlayer.m_sceneObj==playerSceneObj )
playerSceneObj.__649(pt.x, pt.y);
}
private void OnAltAction()
{
G.m_game.__314();
if ( MessageBox.m_instance.__38() )
return;
if ( G.m_game.m_menuDialog.__38() )
{
if ( G.m_game.m_menuDialog.m_isWaitingUser )
{
if ( G.m_game.m_menuDialog.m_locked==false )
{
if ( G.m_game.m_layout.Get(LAYOUT_CTRL.SHUTUP).m_rightClick )
G.m_game.m_menuDialog.Quit();
}
}
else
{
G.m_game.m_menuDialog.__458(G.m_game.m_cursorViewX, G.m_game.m_cursorViewY);
}
return;
}
G.m_game.__324(true);
Scene scene = G.m_game.__291();
if ( scene==null )
return;
Player player = G.m_game.__293();
if ( player==null || player.m_sceneObj==null )
return;
if ( G.m_game.__253()==false )
return;
if ( m_layout.m_bagLocked==false && m_layout.Get(LAYOUT_CTRL.BAG).m_rightClick )
m_layout.m_bagOpened = !m_layout.m_bagOpened;
}
private void __329()
{
SceneObj dragObj = G.m_game.m_dragObj;
G.m_game.m_dropPos.x = dragObj.__35();
G.m_game.m_dropPos.y = dragObj.__36();
G.m_game.m_dragObj = null;
Scene scene = G.m_game.__291();
if ( scene==null )
{
G.m_game.m_dragging = false;
return;
}
SceneEntity dropEntity;
SubObj dropSub;
if ( scene.__426(out dropEntity, out dropSub, G.m_game.m_cursorViewX, G.m_game.m_cursorViewY, false, DRAG.TARGET)==false )
{
G.m_game.m_dragging = false;
return;
}
if ( dropEntity )
{
if ( dropEntity.__606() )
{
SceneObj dropObj = (SceneObj)dropEntity;
if ( dropObj!=dragObj )
{
G.m_game.__326(dragObj.m_obj, dropObj.m_obj, dropSub, false);
G.m_game.m_dragging = false;
return;
}
}
else
{
SceneLabel dropLabel = (SceneLabel)dropEntity;
scene.__562(dragObj.m_obj, dropLabel);
G.m_game.m_dragging = false;
return;
}
}
G.m_game.__329(dragObj.m_obj);
G.m_game.m_dragging = false;
}
private void CallUseEvent(Player player, Obj objCursor, Obj objTrg, SceneObj sceneObjTrg, SubObj subObjTrg, bool bothFromInventory, SceneLabel labelTrg)
{
SceneCell cell = null;
if ( objTrg )
cell = player.m_sceneObj.__643(objTrg, objCursor);
else
cell = player.m_sceneObj.__643(labelTrg, objCursor);
if ( cell==null )
{
player.m_sceneObj.__651();
G.m_game.m_cursorObj = null;
if ( objTrg )
G.m_game.__326(objCursor, objTrg, subObjTrg, bothFromInventory);
else
{
Scene scene = G.m_game.__291();
if ( scene )
scene.__562(objCursor, labelTrg);
}
}
else
{
Message msg = new Message();
msg.m_type = objTrg ? Message.USE : Message.USELABEL;
msg.m_player = player;
msg.m_sceneObj = sceneObjTrg;
msg.m_subObj = subObjTrg;
msg.m_objA = objCursor;
msg.m_objB = objTrg;
msg.m_label = labelTrg;
msg.m_bothFromInventory = bothFromInventory;
G.m_game.m_cursorObj = null;
SceneCellLink link = cell.__604(objTrg==null ? LINK.USELABEL : LINK.USE);
if ( link.m_puzzle==0 || G.m_game.m_scenario.__522(link.m_puzzle) )
{
msg.m_dist = link.m_dist;
msg.m_anim = player.m_obj.__471(ref link.m_anim);
msg.m_dir = link.m_dir;
}
msg.m_state = Message.S_MOVE;
player.m_sceneObj.__649(cell.__35(), cell.__36(), msg);
}
}
private void CallDetachEvent(Player player, Obj obj)
{
SceneCell cell = player.m_sceneObj.__644(obj);
if ( cell==null )
{
player.m_sceneObj.__651();
G.m_game.m_cursorObj = null;
G.m_game.__327(obj);
}
else
{
Message msg = new Message();
msg.m_type = Message.DETACH;
msg.m_player = player;
msg.m_sceneObj = null;
msg.m_subObj = null;
msg.m_objA = obj;
msg.m_objB = null;
msg.m_label = null;
msg.m_bothFromInventory = false;
G.m_game.m_cursorObj = null;
SceneCellLink link = cell.__604(LINK.DETACH);
if ( link.m_puzzle==0 || G.m_game.m_scenario.__522(link.m_puzzle) )
{
msg.m_dist = link.m_dist;
msg.m_anim = player.m_obj.__471(ref link.m_anim);
msg.m_dir = link.m_dir;
}
msg.m_state = Message.S_MOVE;
player.m_sceneObj.__649(cell.__35(), cell.__36(), msg);
}
}
}
