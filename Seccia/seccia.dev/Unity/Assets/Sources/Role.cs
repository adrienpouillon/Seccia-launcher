using UnityEngine;
using System;
using System.Collections.Generic;
public class Role
{
public enum LOCK
{
NONE,
EXECUTE,
START,
STOP,
}
public uint m_crc32;
public int m_sid;
public string m_uid = "";
public bool m_autoStart = false;
public RoleBox m_startBox;
public RoleBox m_updateBox;
public RoleBox[] m_boxes;
public Dictionary<int, List<RoleBox>> m_eventBoxes;
public List<RoleBox> m_eventRepeatBoxes;
public Dictionary<string, RoleBoxTask> m_taskByNames;
public Dictionary<int, RoleBox> m_boxesByID;
public LOCK m_internalLock;
public bool m_running;
public int m_token;
public List<RoleBox> m_stack = new List<RoleBox>();
public List<RoleBox> m_stackFrame = new List<RoleBox>();
public RoleBox m_startRoleBox = null;
public int m_startRoleBoxToken = 0;
public static implicit operator bool(Role inst) { return inst!=null; }
public void __64(Asset asset)
{
m_crc32 = asset.__16();
m_autoStart = asset.__10();
if ( asset.__10() )
m_taskByNames = new Dictionary<string, RoleBoxTask>();
List<RoleBox>[] eventBoxes = null;
int eventTypeCount = asset.__12();
if ( eventTypeCount>0 )
{
m_eventBoxes = new Dictionary<int, List<RoleBox>>();
eventBoxes = new List<RoleBox>[eventTypeCount];
for ( int i=0 ; i<eventTypeCount ; i++ )
{
int eventType = asset.__14();
int eventCount = asset.__12();
List<RoleBox> boxes = new List<RoleBox>();
boxes.Capacity = eventCount;
m_eventBoxes.Add(eventType, boxes);
eventBoxes[i] = boxes;
if ( eventType==RoleBoxEventRepeat.ID )
m_eventRepeatBoxes = boxes;
}
}
m_startBox = null;
m_updateBox = null;
m_boxes = new RoleBox[asset.__15()];
m_boxesByID = new Dictionary<int, RoleBox>(m_boxes.Length);
for ( int iBox=0 ; iBox<m_boxes.Length ; iBox++ )
{
RoleBox box;
int type = asset.__13();
switch ( type )
{
case RoleBoxTask.ID:					box = new RoleBoxTask();					break;
case RoleBoxNext.ID:					box = new RoleBoxNext();					break;
case RoleBoxScript.ID:					box = new RoleBoxScript();					break;
case RoleBoxActionAnim.ID:				box = new RoleBoxActionAnim();				break;
case RoleBoxActionCamera.ID:			box = new RoleBoxActionCamera();			break;
case RoleBoxActionCinematic.ID:			box = new RoleBoxActionCinematic();			break;
case RoleBoxActionDelay.ID:				box = new RoleBoxActionDelay();				break;
case RoleBoxActionExamine.ID:			box = new RoleBoxActionExamine();			break;
case RoleBoxActionInterpolate.ID:		box = new RoleBoxActionInterpolate();		break;
case RoleBoxActionJump.ID:				box = new RoleBoxActionJump();				break;
case RoleBoxActionLight.ID:				box = new RoleBoxActionLight();				break;
case RoleBoxActionPath.ID:				box = new RoleBoxActionPath();				break;
case RoleBoxActionPlayer.ID:			box = new RoleBoxActionPlayer();			break;
case RoleBoxActionPopup.ID:				box = new RoleBoxActionPopup();				break;
case RoleBoxActionRole.ID:				box = new RoleBoxActionRole();				break;
case RoleBoxActionSelect.ID:			box = new RoleBoxActionSelect();			break;
case RoleBoxActionShow.ID:				box = new RoleBoxActionShow();				break;
case RoleBoxActionSong.ID:				box = new RoleBoxActionSong();				break;
case RoleBoxActionStop.ID:				box = new RoleBoxActionStop();				break;
case RoleBoxActionSuccess.ID:			box = new RoleBoxActionSuccess();			break;
case RoleBoxActionTalk.ID:				box = new RoleBoxActionTalk();				break;
case RoleBoxActionTake.ID:				box = new RoleBoxActionTake();				break;
case RoleBoxActionTask.ID:				box = new RoleBoxActionTask();				break;
case RoleBoxActionTimeline.ID:			box = new RoleBoxActionTimeline();			break;
case RoleBoxActionWalk.ID:				box = new RoleBoxActionWalk();				break;
case RoleBoxConditionBool.ID:			box = new RoleBoxConditionBool();			break;
case RoleBoxConditionCall.ID:			box = new RoleBoxConditionCall();			break;
case RoleBoxConditionDialog.ID:			box = new RoleBoxConditionDialog();			break;
case RoleBoxConditionDialogItem.ID:		box = new RoleBoxConditionDialogItem();		break;
case RoleBoxConditionList.ID:			box = new RoleBoxConditionList();			break;
case RoleBoxConditionObject.ID:			box = new RoleBoxConditionObject();			break;
case RoleBoxConditionPlayer.ID:			box = new RoleBoxConditionPlayer();			break;
case RoleBoxConditionPlayerItem.ID:		box = new RoleBoxConditionPlayerItem();		break;
case RoleBoxConditionPuzzle.ID:			box = new RoleBoxConditionPuzzle();			break;
case RoleBoxConditionScene.ID:			box = new RoleBoxConditionScene();			break;
case RoleBoxConditionSceneItem.ID:		box = new RoleBoxConditionSceneItem();		break;
case RoleBoxConditionSequence.ID:		box = new RoleBoxConditionSequence();		break;
case RoleBoxEventCell.ID:				box = new RoleBoxEventCell();				break;
case RoleBoxEventChoice.ID:				box = new RoleBoxEventChoice();				break;
case RoleBoxEventClick.ID:				box = new RoleBoxEventClick();				break;
case RoleBoxEventDetach.ID:				box = new RoleBoxEventDetach();				break;
case RoleBoxEventDrag.ID:				box = new RoleBoxEventDrag();				break;
case RoleBoxEventDrop.ID:				box = new RoleBoxEventDrop();				break;
case RoleBoxEventEnterDialog.ID:		box = new RoleBoxEventEnterDialog();		break;
case RoleBoxEventEnterScene.ID:			box = new RoleBoxEventEnterScene();			break;
case RoleBoxEventExitDialog.ID:			box = new RoleBoxEventExitDialog();			break;
case RoleBoxEventExitScene.ID:			box = new RoleBoxEventExitScene();			break;
case RoleBoxEventInput.ID:				box = new RoleBoxEventInput();				break;
case RoleBoxEventLabel.ID:				box = new RoleBoxEventLabel();				break;
case RoleBoxEventLayout.ID:				box = new RoleBoxEventLayout();				break;
case RoleBoxEventPuzzle.ID:				box = new RoleBoxEventPuzzle();				break;
case RoleBoxEventRepeat.ID:				box = new RoleBoxEventRepeat();				break;
case RoleBoxEventSay.ID:				box = new RoleBoxEventSay();				break;
case RoleBoxEventSelect.ID:				box = new RoleBoxEventSelect();				break;
case RoleBoxEventSong.ID:				box = new RoleBoxEventSong();				break;
case RoleBoxEventSpot.ID:				box = new RoleBoxEventSpot();				break;
case RoleBoxEventSwitch.ID:				box = new RoleBoxEventSwitch();				break;
case RoleBoxEventUse.ID:				box = new RoleBoxEventUse();				break;
case RoleBoxEventUseLabel.ID:			box = new RoleBoxEventUseLabel();			break;
case RoleBoxEventWalk.ID:				box = new RoleBoxEventWalk();				break;
default:
box = new RoleBox();
break;
}
box.m_parent = this;
m_boxes[iBox] = box;
#if UNITY_EDITOR
#endif
box.m_type = type;
bool loop = asset.__10();
switch ( box.m_type )
{
case 0x101:
m_startBox = box;
box.m_base = RoleBox.BASE.START;
break;
case 0x102:
box.m_base = RoleBox.BASE.END;
break;
case 0x103:
box.m_base = RoleBox.BASE.RESTART;
break;
case 0x104:
box.m_base = RoleBox.BASE.OR;
break;
case 0x105:
m_updateBox = box;
box.m_base = RoleBox.BASE.UPDATE;
break;
case 0x106:
box.m_base = RoleBox.BASE.RANDOM;
break;
case 0x107:
box.m_base = RoleBox.BASE.SCHEDULE;
break;
case 0x108:
box.m_base = RoleBox.BASE.TASK;
break;
case 0x109:
box.m_base = RoleBox.BASE.SKIP;
break;
case 0x201:
box.m_base = RoleBox.BASE.SCRIPT;
break;
default:
if ( box.m_type<0x400 )
box.m_base = RoleBox.BASE.ACTION;
else if ( box.m_type<0x500 )
box.m_base = loop ? RoleBox.BASE.WHILE : RoleBox.BASE.IF;
else
box.m_base = RoleBox.BASE.EVENT;
break;
}
box.m_id = asset.__15();
m_boxesByID.Add(box.m_id, box);
box.m_lock = asset.__10();
int eventTypeIndex = asset.__12();
if ( eventTypeIndex!=255 )
eventBoxes[eventTypeIndex].Add(box);
int inputConnectionCount = asset.__12();
if ( inputConnectionCount>0 )
box.m_signals = new bool[inputConnectionCount];
int outputPlugCount = asset.__12();
if ( outputPlugCount>0 )
{
box.m_plugs = new RolePlug[outputPlugCount];
for ( int iPlug=0 ; iPlug<box.m_plugs.Length ; iPlug++ )
{
RolePlug plug = new RolePlug();
box.m_plugs[iPlug] = plug;
plug.m_ids = new int[asset.__12()];
plug.m_indexes = new int[plug.m_ids.Length];
plug.m_boxes = new RoleBox[plug.m_ids.Length];
for ( int i=0 ; i<plug.m_ids.Length ; i++ )
{
plug.m_ids[i] = asset.__15();
plug.m_indexes[i] = asset.__12();
}
}
}
box.__64(asset);
if ( box.m_base==RoleBox.BASE.EVENT )
box.__502(asset);
if ( box.m_base==RoleBox.BASE.TASK )
{
RoleBoxTask task = (RoleBoxTask)box;
if ( task.m_name.Length>0 )
m_taskByNames.TryAdd(task.m_name, task);
}
}
for ( int i=0 ; i<m_boxes.Length ; i++ )
{
if ( m_boxes[i].m_plugs!=null )
{
for ( int j=0 ; j<m_boxes[i].m_plugs.Length ; j++ )
{
RolePlug plug = m_boxes[i].m_plugs[j];
for ( int k=0 ; k<plug.m_boxes.Length ; k++ )
{
RoleBox box = __496(plug.m_ids[k]);
if ( box )
plug.m_boxes[k] = box;
}
plug.m_ids = null;
}
}
}
Reset();
}
public void Reset(bool single = true)
{
m_internalLock = LOCK.NONE;
m_running = false;
m_stack.Clear();
#if UNITY_EDITOR
#endif
m_stackFrame.Clear();
m_startRoleBox = null;
m_startRoleBoxToken = 0;
for ( int iBox=0 ; iBox<m_boxes.Length ; iBox++ )
m_boxes[iBox].Reset(false);
#if UNITY_STANDALONE_WIN
if ( single && G.m_game.m_configRun )
IDE.Post(IDE_MSG.ROLE_RESET, m_sid);
#endif
}
public void __46(JsonObj json)
{
json.__381("crc", (int)m_crc32);
json.__382("sid", m_sid);
json.__384("running", m_running);
json.__381("token", m_token);
json.__381("roleBox", m_startRoleBox==null ? 0 : m_startRoleBox.m_id);
json.__381("roleBoxToken", m_startRoleBoxToken);
JsonArray jBoxes = json.__389("boxes");
for ( int i=0 ; i<m_boxes.Length ; i++ )
{
JsonObj jBox = jBoxes.__388();
m_boxes[i].__46(jBox);
}
JsonArray jStack = json.__389("stack");
for ( int i=0 ; i<m_stack.Count ; i++ )
jStack.__381(m_stack[i].m_id);
}
public bool __47(JsonObj json)
{
uint crc = (uint)json.GetInt("crc");
if ( crc!=m_crc32 )
return false;
m_running = json.__400("running");
m_token = json.GetInt("token");
m_startRoleBox = __496(json.GetInt("roleBox"));
m_startRoleBoxToken = json.GetInt("roleBoxToken");
JsonArray jBoxes = json.__394("boxes");
if ( jBoxes )
{
for ( int i=0 ; i<jBoxes.__66() ; i++ )
{
JsonObj jBox = jBoxes.__393(i);
if ( jBox )
{
RoleBox box = __496(jBox.GetInt("id"));
if ( box )
box.__47(jBox);
}
}
}
JsonArray jStack = json.__394("stack");
if ( jStack )
{
for ( int i=0 ; i<jStack.__66() ; i++ )
{
RoleBox box = __496(jStack.GetInt(i));
if ( box )
m_stack.Add(box);
}
}
return true;
}
public void __495()
{
for ( int i=0 ; i<m_stack.Count ; i++ )
{
RoleBox box = m_stack[i];
if ( box.m_purge )
{
#if UNITY_EDITOR
#endif
box.m_purge = false;
m_stack.RemoveAt(i);
i--;
}
}
}
public RoleBox __496(int id)
{
if ( id!=0 )
{
RoleBox box;
if ( m_boxesByID.TryGetValue(id, out box) )
return box;
}
return null;
}
public bool Start(RoleBox roleBox = null)
{
if ( m_internalLock!=LOCK.NONE )
{
m_internalLock = LOCK.START;
return false;
}
Reset();
if ( roleBox==null || roleBox.m_parent.m_uid==m_uid )
{
m_startRoleBox = null;
m_startRoleBoxToken = 0;
}
else
{
m_startRoleBox = roleBox;
m_startRoleBoxToken = m_startRoleBox.m_parent.m_token;
}
if ( m_startBox )
{
m_stack.Add(m_startBox);
#if UNITY_EDITOR
#endif
}
m_running = true;
m_token++;
return true;
}
public void Stop()
{
if ( m_internalLock==LOCK.NONE )
{
Reset();
return;
}
if ( m_internalLock!=LOCK.START )
{
m_internalLock = LOCK.STOP;
if ( m_startRoleBox )
m_startRoleBox.__457(m_startRoleBoxToken);
}
}
public void __42()
{
if ( m_running==false )
return;
if ( m_updateBox )
{
#if UNITY_EDITOR
Debug.Assert(m_stack.Contains(m_updateBox)==false);
#endif
m_updateBox.__503();
__495();
m_updateBox.m_purge = false;
m_stack.Add(m_updateBox);
}
m_internalLock = LOCK.EXECUTE;
do
{
m_stackFrame.Clear();
for ( int i=0 ; i<m_stack.Count ; i++ )
{
__497(m_stack[i]);
switch ( m_internalLock )
{
case LOCK.STOP:
m_internalLock = LOCK.NONE;
Stop();
return;
case LOCK.START:
m_internalLock = LOCK.NONE;
Start();
return;
}
}
for ( int i=0 ; i<m_stack.Count ; i++ )
{
if ( m_stack[i].m_exit )
{
#if UNITY_EDITOR
#endif
m_stack.RemoveAt(i);
i--;
}
}
for ( int i=0 ; i<m_stackFrame.Count ; i++ )
{
#if UNITY_EDITOR
Debug.Assert(m_stack.Contains(m_stackFrame[i])==false);
#endif
m_stackFrame[i].m_purge = false;
m_stack.Add(m_stackFrame[i]);
}
} while ( m_stackFrame.Count>0 );
m_internalLock = LOCK.NONE;
}
public void __497(RoleBox box)
{
bool started = false;
if ( box.m_enter==false )
{
if ( box.__504()==false )
return;
started = true;
box.m_enter = true;
#if UNITY_STANDALONE_WIN
if ( G.m_game.m_configRun )
IDE.Post(IDE_MSG.ROLE_ENTER, m_sid, box.m_id);
#endif
box.__508();
}
if ( box.m_exit==false )
{
bool finished = box.__509();
if ( box.m_base==RoleBox.BASE.ACTION )
{
if ( box.__506(started, finished)!=0 )
finished = true;
}
else if ( box.m_base==RoleBox.BASE.SKIP )
{
if ( finished==false )
box.__506();
}
else
box.__506();
if ( finished==false )
return;
if ( box.m_plugs!=null )
{
switch ( box.m_base )
{
case RoleBox.BASE.RANDOM:
__499(box);
break;
case RoleBox.BASE.SCHEDULE:
for ( int i=0 ; i<box.m_plugs.Length ; i++ )
__498(box, i);
break;
default:
if ( box.m_plugs.Length==1 )
__498(box, 0);
break;
}
}
box.m_exit = true;
#if UNITY_STANDALONE_WIN
if ( G.m_game.m_configRun )
IDE.Post(IDE_MSG.ROLE_EXIT, m_sid, box.m_id);
#endif
switch ( box.m_base )
{
case RoleBox.BASE.END:
Stop();
break;
case RoleBox.BASE.RESTART:
Start();
break;
}
}
}
public int __498(RoleBox box, int iOutPlug)
{
if ( box.m_plugs==null || iOutPlug<0 || iOutPlug>=box.m_plugs.Length )
return 0;
int count = 0;
RolePlug plug = box.m_plugs[iOutPlug];
for ( int i=0 ; i<plug.m_boxes.Length ; i++ )
{
RoleBox trg = plug.m_boxes[i];
if ( trg.m_enter )
continue;
int index = plug.m_indexes[i];
if ( trg.m_signals[index]==false )
{
trg.m_signals[index] = true;
count++;
#if UNITY_STANDALONE_WIN
if ( G.m_game.m_configRun )
IDE.Post(IDE_MSG.ROLE_SIGNAL, m_sid, trg.m_id, index);
#endif
}
if ( m_stackFrame.IndexOf(trg)==-1 )
m_stackFrame.Add(trg);
}
return count;
}
public int __499(RoleBox box)
{
if ( box.m_plugs==null )
return 0;
RolePlug plug = box.m_plugs[0];
int index = 0;
if ( plug.m_boxes.Length>1 )
index = G.__156(plug.m_boxes.Length);
RoleBox trg = plug.m_boxes[index];
if ( trg.m_enter )
return 0;
index = plug.m_indexes[index];
int count = 0;
if ( trg.m_signals[index]==false )
{
trg.m_signals[index] = true;
count++;
#if UNITY_STANDALONE_WIN
if ( G.m_game.m_configRun )
IDE.Post(IDE_MSG.ROLE_SIGNAL, m_sid, trg.m_id, index);
#endif
}
if ( m_stackFrame.IndexOf(trg)==-1 )
m_stackFrame.Add(trg);
return count;
}
public void __500(RoleBox box)
{
if ( box.m_script.m_customOuts==null || box.m_script.m_customOuts.Count==0 )
__498(box, 0);
else
{
for ( int i=0 ; i<box.m_script.m_customOuts.Count ; i++ )
__498(box, box.m_script.m_customOuts[i]);
box.m_script.m_customOuts.Clear();
}
}
public bool __256()
{
if ( m_running==false )
return false;
for ( int i=0 ; i<m_stack.Count ; i++ )
{
if ( m_stack[i].__256() )
return true;
}
return false;
}
public int __501()
{
if ( m_eventRepeatBoxes==null )
return 0;
int count = 0;
for ( int i=0 ; i<m_eventRepeatBoxes.Count ; i++ )
{
RoleBoxEventRepeat box = (RoleBoxEventRepeat)m_eventRepeatBoxes[i];
box.m_elapsed += G.m_game.m_elapsed;
float duration = G.__117(G.__96(ref box.m_duration));
if ( box.m_elapsed>=duration )
{
box.m_elapsed = 0.0f;
box.m_iRender = CameraBehavior.s_iRender;
count++;
}
}
return count;
}
public int __322(Event evt)
{
#if UNITY_EDITOR
#endif
if ( m_stackFrame.Count>0 )
return 0;
if ( m_eventBoxes==null )
return 0;
List<RoleBox> boxes;
if ( m_eventBoxes.TryGetValue(evt.id, out boxes)==false )
return 0;
int count = 0;
for ( int i=0 ; i<boxes.Count ; i++ )
{
RoleBox box = boxes[i];
if ( box.__505()==false )
continue;
if ( box.__467(evt) )
{
box.__503();
__495();
int res = box.__506();
__498(box, 0);
count++;
if ( evt.hasReturnValue==false && res!=0 )
{
evt.hasReturnValue = true;
if ( evt.result )
evt.result.m_value = res.ToString();
}
#if UNITY_STANDALONE_WIN
if ( G.m_game.m_configRun )
IDE.Post(IDE_MSG.ROLE_EXIT, m_sid, box.m_id);
#endif
if ( box.m_consume )
{
evt.consume = true;
break;
}
}
}
if ( count>0 )
{
for ( int i=0 ; i<m_stackFrame.Count ; i++ )
{
#if UNITY_EDITOR
Debug.Assert(m_stack.Contains(m_stackFrame[i])==false);
#endif
m_stackFrame[i].m_purge = false;
m_stack.Add(m_stackFrame[i]);
}
m_stackFrame.Clear();
}
return count;
}
public bool Task(ref string name, ref string value, ref bool result)
{
if ( m_taskByNames==null )
return false;
RoleBoxTask box;
if ( m_taskByNames.TryGetValue(name, out box)==false )
return false;
if ( box.m_isCallingByTask )
return false;
#if UNITY_EDITOR
Debug.Assert(m_stackFrame.Count==0);
#endif
box.__503();
__495();
int count = 0;
if ( box.m_script==null )
{
result = false;
__498(box, 0);
count++;
}
else
{
G.m_game.m_sysVarArg.m_value = value;
G.m_game.m_sysVarRes.m_value = "";
box.m_isCallingByTask = true;
int res = box.m_script.__684();
box.m_isCallingByTask = false;
result = G.__112(ref G.m_game.m_sysVarRes.m_value);
if ( res==0 )
{
__500(box);
count++;
}
}
#if UNITY_STANDALONE_WIN
if ( G.m_game.m_configRun )
IDE.Post(IDE_MSG.ROLE_EXIT, m_sid, box.m_id);
#endif
if ( count>0 )
{
for ( int i=0 ; i<m_stackFrame.Count ; i++ )
{
#if UNITY_EDITOR
Debug.Assert(m_stack.Contains(m_stackFrame[i])==false);
#endif
m_stackFrame[i].m_purge = false;
m_stack.Add(m_stackFrame[i]);
}
m_stackFrame.Clear();
}
return true;
}
}
public class RoleBox
{
public enum BASE
{
START,
UPDATE,
END,
RESTART,
OR,
RANDOM,
SCHEDULE,
TASK,
SKIP,
SCRIPT,
ACTION,
IF,
WHILE,
EVENT,
}
public Role m_parent;
public int m_type;
public BASE m_base;
public int m_id;
public bool m_lock;
public RolePlug[] m_plugs;
public Script m_script;
public bool m_consume;
public string m_eventPlayer;
public string m_eventScene;
public int[] m_eventPuzzles;
public int[] m_eventSequences;
public string m_eventVariableName;
public string m_eventVariableCompare;
public string m_eventVariableValue;
public bool[] m_signals;
public bool m_enter = false;
public bool m_exit = false;
public bool m_actionDone = false;
public bool m_purge = false;
public static implicit operator bool(RoleBox inst) { return inst!=null; }
public virtual void __64(Asset asset)
{
m_script = asset.__26();
}
public void __502(Asset asset)
{
m_consume = asset.__10();
m_eventPlayer = asset.__18();
m_eventScene = asset.__18();
m_eventPuzzles = new int[3];
m_eventPuzzles[0] = asset.__15();
m_eventPuzzles[1] = asset.__15();
m_eventPuzzles[2] = asset.__15();
m_eventSequences = new int[4];
m_eventSequences[0] = asset.__15();
m_eventSequences[1] = asset.__15();
m_eventSequences[2] = asset.__15();
m_eventSequences[3] = asset.__15();
m_eventVariableName = asset.__18();
m_eventVariableCompare = asset.__18();
m_eventVariableValue = asset.__18();
}
public virtual void __46(JsonObj jBox)
{
jBox.__381("id", m_id);
jBox.__384("enter", m_enter);
jBox.__384("exit", m_exit);
jBox.__384("action", m_actionDone);
if ( m_signals!=null )
{
JsonArray jSignals = jBox.__389("signals");
for ( int i=0 ; i<m_signals.Length ; i++ )
jSignals.__384(m_signals[i]);
}
}
public virtual void __47(JsonObj jBox)
{
m_enter = jBox.__400("enter");
m_exit = jBox.__400("exit");
m_actionDone = jBox.__400("action");
if ( m_signals!=null )
{
JsonArray signals = jBox.__394("signals");
if ( signals!=null )
{
for ( int i=0 ; i<signals.__66() && i<m_signals.Length ; i++ )
m_signals[i] = signals.__400(i);
}
}
}
public void Reset(bool single = true)
{
m_purge = false;
m_enter = false;
m_exit = false;
m_actionDone = false;
if ( m_signals!=null )
{
for ( int i=0 ; i<m_signals.Length ; i++ )
m_signals[i] = false;
}
__507();
#if UNITY_STANDALONE_WIN
if ( single && G.m_game.m_configRun )
IDE.Post(IDE_MSG.ROLE_RESET, m_parent.m_sid, m_id);
#endif
}
public void __503()
{
#if UNITY_EDITOR
#endif
Reset();
m_purge = true;
if ( m_plugs!=null )
{
for ( int iPlug=0 ; iPlug<m_plugs.Length ; iPlug++ )
{
RolePlug plug = m_plugs[iPlug];
for ( int iBox=0 ; iBox<plug.m_boxes.Length ; iBox++ )
plug.m_boxes[iBox].__503();
}
}
}
public bool __256()
{
if ( m_lock && m_enter && m_exit==false )
return true;
return false;
}
public bool __504()
{
if ( m_base==RoleBox.BASE.OR )
{
if ( m_signals!=null )
{
for ( int i=0 ; i<m_signals.Length ; i++ )
{
if ( m_signals[i] )
return true;
}
}
return false;
}
else
{
if ( m_signals!=null )
{
for ( int i=0 ; i<m_signals.Length ; i++ )
{
if ( m_signals[i]==false )
return false;
}
}
return true;
}
}
public bool __505()
{
if ( m_eventPuzzles[0]!=0 && G.m_game.m_scenario.__521(m_eventPuzzles[0])==false )
return false;
if ( m_eventPuzzles[1]!=0 && G.m_game.m_scenario.__523(m_eventPuzzles[1])==false )
return false;
if ( m_eventPuzzles[2]!=0 && G.m_game.m_scenario.__522(m_eventPuzzles[2])==false )
return false;
if ( m_eventSequences[0]!=0 && G.m_game.m_scenario.__524(m_eventSequences[0])==false )
return false;
if ( m_eventSequences[1]!=0 && G.m_game.m_scenario.__527(m_eventSequences[1])==false )
return false;
if ( m_eventSequences[2]!=0 && G.m_game.m_scenario.__526(m_eventSequences[2])==false )
return false;
if ( m_eventSequences[3]!=0 && G.m_game.m_scenario.__525(m_eventSequences[3])==false )
return false;
if ( m_eventVariableName.Length>0 )
{
Variable var = G.m_game.__286(ref m_eventVariableName);
if ( var==null )
return false;
string val = G.__96(ref m_eventVariableValue);
string cmp = G.__96(ref m_eventVariableCompare);
switch ( cmp )
{
case "!=":
if ( var.m_value==val )
return false;
break;
case "<":
if ( !(G.__113(ref var.m_value)<G.__113(ref val)) )
return false;
break;
case "<=":
if ( !(G.__113(ref var.m_value)<=G.__113(ref val)) )
return false;
break;
case ">":
if ( !(G.__113(ref var.m_value)>G.__113(ref val)) )
return false;
break;
case ">=":
if ( !(G.__113(ref var.m_value)>=G.__113(ref val)) )
return false;
break;
default:
if ( var.m_value!=val )
return false;
break;
}
}
string scene = G.__96(ref m_eventScene);
if ( scene.Length>0 && G.m_game.__316(ref scene)==false )
return false;
string player = G.__96(ref m_eventPlayer);
if ( player.Length>0 && G.m_game.__318(ref player)==false )
return false;
return true;
}
public int __506(bool enter = false, bool exit = false)
{
if ( m_script==null )
return 0;
switch ( m_type )
{
case RoleBoxScript.ID:
case RoleBoxConditionCall.ID:
return 0;
}
return m_script.__684(enter, exit);
}
public virtual void __507()
{
}
public virtual void __508()
{
}
public virtual bool __509()
{
return true;
}
public bool __457(int token)
{
if ( m_parent.m_running && m_parent.m_token==token && m_enter && m_exit==false && m_actionDone==false )
{
if ( __510()==false )
return false;
m_actionDone = true;
return true;
}
return false;
}
public virtual bool __510()
{
return true;
}
public virtual bool __467(Event evt)
{
return false;
}
}
public class RolePlug
{
public int[] m_ids;
public int[] m_indexes;
public RoleBox[] m_boxes;
}
public class RoleBoxTask : RoleBox
{
public const int ID = 0x108;
public string m_name;
public bool m_isCallingByTask;
public override void __64(Asset asset)
{
base.__64(asset);
m_name = asset.__18();
}
public override void __507()
{
m_isCallingByTask = false;
}
public override bool __509()
{
return true;
}
}
public class RoleBoxNext : RoleBox
{
public const int ID = 0x109;
public bool m_called;
public override void __64(Asset asset)
{
base.__64(asset);
}
public override void __46(JsonObj jBox)
{
base.__46(jBox);
jBox.__384("called", m_called);
}
public override void __47(JsonObj jBox)
{
base.__47(jBox);
m_called = jBox.__400("called");
}
public override void __507()
{
m_called = false;
}
public override bool __509()
{
if ( m_called==false )
{
m_called = true;
return false;
}
return true;
}
}
public class RoleBoxScript : RoleBox
{
public const int ID = 0x201;
public override void __64(Asset asset)
{
base.__64(asset);
}
public override void __508()
{
}
public override bool __509()
{
if ( m_script==null )
{
m_parent.__498(this, 0);
return true;
}
int res = m_script.__684();
if ( res!=0 )
return false;
m_parent.__500(this);
return true;
}
}
public class RoleBoxActionAnim : RoleBox
{
public const int ID = 0x301;
string m_scene;
string m_obj;
string m_anim;
string m_dir;
string m_reverse;
string m_startFrame;
string m_actionFrame;
public override void __64(Asset asset)
{
base.__64(asset);
m_scene = asset.__18();
m_obj = asset.__18();
m_anim = asset.__18();
m_dir = asset.__18();
m_reverse = asset.__18();
m_startFrame = asset.__18();
m_actionFrame = asset.__18();
}
public override void __508()
{
Scene scene = G.m_game.__276(ref m_scene);
if ( scene==null )
{
m_actionDone = true;
return;
}
string obj = G.__96(ref m_obj);
SceneObj sceneObj = scene.__277(obj);
if ( sceneObj==null )
{
m_actionDone = true;
return;
}
string anim = G.__96(ref m_anim);
if ( anim.Length>0 )
sceneObj.__626(ref anim);
string dir = G.__96(ref m_dir);
if ( dir.Length>0 )
sceneObj.m_anim.__672(G.__151(ref dir));
string reverse = G.__96(ref m_reverse);
if ( reverse.Length>0 )
sceneObj.m_anim.__670(G.__112(ref reverse));
string startFrame = G.__96(ref m_startFrame);
if ( startFrame.Length>0 )
sceneObj.m_anim.__671(G.__113(ref startFrame));
string actionFrame = G.__96(ref m_actionFrame);
if ( actionFrame.Length>0 )
sceneObj.m_anim.notif.actionFrame = G.__113(ref actionFrame);
sceneObj.m_anim.notif.roleBox = this;
sceneObj.m_anim.notif.roleBoxToken = m_parent.m_token;
}
public override bool __509()
{
return m_actionDone;
}
}
public class RoleBoxActionCamera : RoleBox
{
public const int ID = 0x310;
string m_scene;
string m_shot;
string m_obj;
string m_cell;
string m_zoom;
string m_shot2;
string m_obj2;
string m_cell2;
string m_zoom2;
string m_dx;
string m_dy;
string m_axis;
string m_fadeout;
string m_fadein;
string m_fadeColor;
string m_duration;
string m_endless;
public override void __64(Asset asset)
{
base.__64(asset);
m_scene = asset.__18();
m_shot = asset.__18();
m_obj = asset.__18();
m_cell = asset.__18();
m_zoom = asset.__18();
m_shot2 = asset.__18();
m_obj2 = asset.__18();
m_cell2 = asset.__18();
m_zoom2 = asset.__18();
m_dx = asset.__18();
m_dy = asset.__18();
m_axis = asset.__18();
m_fadeout = asset.__18();
m_fadein = asset.__18();
m_fadeColor = asset.__18();
m_duration = asset.__18();
m_endless = asset.__18();
}
public override void __508()
{
string scene = G.__96(ref m_scene);
Scene currentScene = G.m_game.__291();
SceneObj playerSceneObj = G.m_game.__295();
if ( currentScene==null || playerSceneObj==null || (scene.Length>0 && currentScene.__48(ref scene)==false) )
{
m_actionDone = true;
return;
}
float renderX = currentScene.m_renderOrgX;
float renderY = currentScene.m_renderOrgY;
float renderScale = currentScene.m_renderScale;
float duration = G.__117(G.__96(ref m_duration));
bool immediate = duration==0.0f;
bool endless = G.__112(G.__96(ref m_endless));
string axis = G.__96(ref m_axis);
bool horz = axis.Length==0 || G.__148(ref axis, "HORZ");
bool vert = axis.Length==0 || G.__148(ref axis, "VERT");
float dx = (float)G.__113(G.__96(ref m_dx));
float dy = (float)G.__113(G.__96(ref m_dy));
Cam camManual = null;
Cam camTrack = null;
if ( endless )
camManual = currentScene.__568(CAMERA.MANUAL);
else
currentScene.__569(CAMERA.MANUAL);
if ( immediate )
currentScene.__569(CAMERA.TRACK);
else
camTrack = currentScene.__568(CAMERA.TRACK);
float fromX = renderX;
float fromY = renderY;
float fromScale = renderScale;
if ( immediate==false )
{
fromScale = G.__121(G.__96(ref m_zoom));
SceneShot fromShot = currentScene.__537(G.__96(ref m_shot));
if ( fromShot )
{
fromScale = fromShot.m_scale;
if ( horz )
fromX = currentScene.__554(fromShot.m_x+dx, fromScale);
if ( vert )
fromY = currentScene.__555(fromShot.m_y+dy, fromScale);
}
else
{
SceneObj fromObj = currentScene.__277(G.__96(ref m_obj));
if ( fromObj )
{
if ( fromScale==0.0f )
fromScale = fromObj.__34();
SceneCell fromCell = fromObj.__631(G.__96(ref m_cell));
if ( fromCell )
{
if ( horz )
fromX = currentScene.__554(fromCell.__35(), fromScale);
if ( vert )
fromY = currentScene.__555(fromCell.__36(), fromScale);
}
else
{
if ( horz )
fromX = currentScene.__554(fromObj.__35(), fromScale);
if ( vert )
fromY = currentScene.__555(fromObj.__36(), fromScale);
}
}
else
{
if ( fromScale==0.0f )
fromScale = renderScale;
}
}
}
if ( endless==false )
{
renderScale = playerSceneObj.__622();
renderX = currentScene.__554(playerSceneObj.__35(), renderScale);
renderY = currentScene.__555(playerSceneObj.__36(), renderScale);
}
float toX = renderX;
float toY = renderY;
float toScale = renderScale;
if ( immediate==false || endless )
{
toScale = G.__121(G.__96(ref m_zoom2));
SceneShot toShot = currentScene.__537(G.__96(ref m_shot2));
if ( toShot )
{
toScale = toShot.m_scale;
if ( horz )
toX = currentScene.__554(toShot.m_x+dx, toScale);
if ( vert )
toY = currentScene.__555(toShot.m_y+dy, toScale);
}
else
{
SceneObj toObj = currentScene.__277(G.__96(ref m_obj2));
if ( toObj )
{
if ( toScale==0.0f )
toScale = toObj.__34();
SceneCell toCell = toObj.__631(G.__96(ref m_cell2));
if ( toCell )
{
if ( horz )
toX = currentScene.__554(toCell.__35()+dx, toScale);
if ( vert )
toY = currentScene.__555(toCell.__36()+dy, toScale);
}
else
{
if ( horz )
toX = currentScene.__554(toObj.__35()+dx, toScale);
if ( vert )
toY = currentScene.__555(toObj.__36()+dy, toScale);
}
}
else
{
if ( toScale==0.0f )
toScale = renderScale;
if ( horz && dx!=0.0f )
toX += currentScene.__554(dx, toScale) - renderX;
if ( vert && dy!=0.0f )
toY += currentScene.__555(dy, toScale) - renderY;
}
}
}
if ( endless )
{
camManual.m_scale = toScale;
camManual.m_x = toX;
camManual.m_y = toY;
}
if ( immediate )
{
m_actionDone = true;
return;
}
camTrack.m_fromX = fromX;
camTrack.m_toX = toX;
camTrack.m_fromY = fromY;
camTrack.m_toY = toY;
camTrack.m_fromScale = fromScale;
camTrack.m_toScale = toScale;
camTrack.m_duration = duration;
camTrack.m_roleBox = this;
camTrack.m_roleBoxToken = m_parent.m_token;
bool fadeout = G.__112(G.__96(ref m_fadeout));
bool fadein = G.__112(G.__96(ref m_fadein));
if ( fadeout || fadein )
{
Color fadeColor = G.__122(G.__96(ref m_fadeColor));
G.m_game.__300();
G.m_game.m_fadeRoleBox = this;
G.m_game.m_fadeRoleBoxToken = m_parent.m_token;
G.m_game.m_fadeColor = fadeColor;
if ( fadeout && fadein )
{
float fadeDuration = G.__133(duration-0.1f) * 0.5f;
G.m_game.m_fadeOutDuration = fadeDuration;
G.m_game.m_fadeWaitDuration = 0.1f;
G.m_game.m_fadeInDuration = fadeDuration;
G.m_game.m_fadeColor.a = 0.0f;
G.m_game.m_fadeEvent = FADING.OPENED;
G.m_game.m_fade = FADING.CLOSING;
}
else if ( fadeout )
{
G.m_game.m_fadeOutDuration = duration;
G.m_game.m_fadeColor.a = 0.0f;
G.m_game.m_fade = FADING.CLOSING;
}
else
{
G.m_game.m_fadeInDuration = duration;
G.m_game.m_fadeEvent = FADING.OPENED;
G.m_game.m_fadeColor.a = 1.0f;
G.m_game.m_fade = FADING.BLACK;
}
}
}
public override bool __509()
{
return m_actionDone;
}
}
public class RoleBoxActionCinematic : RoleBox
{
public const int ID = 0x302;
string m_cinematic;
public override void __64(Asset asset)
{
base.__64(asset);
m_cinematic = asset.__18();
}
public override void __508()
{
string cinematic = G.__96(ref m_cinematic);
if ( G.m_game.m_cinematicPlayer.__39(cinematic, this)==false )
m_actionDone = true;
}
public override bool __509()
{
return m_actionDone;
}
}
public class RoleBoxActionDelay : RoleBox
{
public const int ID = 0x303;
string m_duration;
float m_enterDuration;
float m_enterTime;
public override void __64(Asset asset)
{
base.__64(asset);
m_duration = asset.__18();
}
public override void __46(JsonObj jBox)
{
base.__46(jBox);
jBox.__383("duration", m_enterDuration);
jBox.__383("time", m_enterTime);
}
public override void __47(JsonObj jBox)
{
base.__47(jBox);
m_enterDuration = jBox.GetFloat("duration");
m_enterTime = jBox.GetFloat("time");
}
public override void __507()
{
m_enterDuration = 0.0f;
m_enterTime = 0.0f;
}
public override void __508()
{
m_enterDuration = G.__117(G.__96(ref m_duration));
m_enterTime = G.m_game.m_time;
}
public override bool __509()
{
if ( G.m_game.m_time-m_enterTime>m_enterDuration )
return true;
return false;
}
}
public class RoleBoxActionExamine : RoleBox
{
public const int ID = 0x305;
string m_obj;
string m_anim;
string m_dir;
string m_scale;
string m_opacity;
public override void __64(Asset asset)
{
base.__64(asset);
m_obj = asset.__18();
m_anim = asset.__18();
m_dir = asset.__18();
m_scale = asset.__18();
m_opacity = asset.__18();
}
public override void __508()
{
string obj = G.__96(ref m_obj);
string anim = G.__96(ref m_anim);
int dir = G.__151(G.__96(ref m_dir));
float scale = G.__118(G.__96(ref m_scale));
float opacity = G.__118(G.__96(ref m_opacity));
if ( G.m_game.Examine(this, obj, anim, dir, scale, opacity)==false )
m_actionDone = true;
}
public override bool __509()
{
return m_actionDone;
}
}
public class RoleBoxActionInterpolate : RoleBox
{
public const int ID = 0x30B;
string m_duration;
string m_min;
string m_max;
string m_loop;
string m_pause;
string m_reverse;
string m_easeIn;
string m_easeOut;
string m_variable;
float m_runDuration;
int m_runMin;
int m_runMax;
int m_runLoop;
float m_runPause;
bool m_runReverse;
bool m_runEaseIn;
bool m_runEaseOut;
Variable m_runVariable;
float m_runTime;
bool m_runPausing;
int m_runLoopIndex;
bool m_runLoopReverse;
public override void __64(Asset asset)
{
base.__64(asset);
m_duration = asset.__18();
m_min = asset.__18();
m_max = asset.__18();
m_loop = asset.__18();
m_pause = asset.__18();
m_reverse = asset.__18();
m_easeIn = asset.__18();
m_easeOut = asset.__18();
m_variable = asset.__18();
}
public override void __508()
{
m_runDuration = G.__117(G.__96(ref m_duration));
m_runMin = G.__113(G.__96(ref m_min));
m_runMax = G.__113(G.__96(ref m_max));
m_runLoop = G.__113(G.__96(ref m_loop));
m_runPause = G.__117(G.__96(ref m_pause));
m_runReverse = G.__112(G.__96(ref m_reverse));
m_runEaseIn = G.__112(G.__96(ref m_easeIn));
m_runEaseOut = G.__112(G.__96(ref m_easeOut));
m_runVariable = G.m_game.__288(G.__96(ref m_variable));
m_runTime = 0.0f;
m_runPausing = false;
m_runLoopIndex = 0;
m_runLoopReverse = false;
if ( m_runDuration==0.0f )
{
if ( m_runVariable )
m_runVariable.m_value = m_runMax.ToString();
m_actionDone = true;
}
else
{
if ( m_runVariable )
m_runVariable.m_value = m_runMin.ToString();
}
}
public override bool __509()
{
m_runTime += G.m_game.m_elapsed;
bool nextLoop = false;
bool lastLoop = m_runLoop>-1 && m_runLoopIndex>=m_runLoop;
if ( m_runPausing )
{
if ( m_runTime>=m_runPause )
{
m_runPausing = false;
nextLoop = true;
}
if ( m_runVariable )
{
if ( m_runLoopReverse )
m_runVariable.m_value = m_runMin.ToString();
else
m_runVariable.m_value = m_runMax.ToString();
}
}
else
{
float ratio = G.Clamp(m_runTime/m_runDuration);
if ( m_runEaseIn || m_runEaseOut )
ratio = G.__138(ratio, 0.0f, 1.0f, m_runEaseIn, m_runEaseOut);
if ( m_runVariable )
{
if ( m_runLoopReverse )
{
int value = m_runMax + (int)((m_runMin-m_runMax)*ratio);
m_runVariable.m_value =  value.ToString();
}
else
{
int value = m_runMin + (int)((m_runMax-m_runMin)*ratio);
m_runVariable.m_value =  value.ToString();
}
}
if ( ratio==1.0f )
{
if ( lastLoop || m_runPause==0.0f )
nextLoop = true;
else
{
m_runTime = 0.0f;
m_runPausing = true;
}
}
}
if ( nextLoop )
{
m_runTime = 0.0f;
m_runLoopIndex++;
if ( m_runReverse )
m_runLoopReverse = !m_runLoopReverse;
if ( lastLoop )
m_actionDone = true;
}
return m_actionDone;
}
}
public class RoleBoxActionJump : RoleBox
{
public const int ID = 0x307;
string m_scene;
string m_durationOut;
string m_durationWait;
string m_durationIn;
string m_color;
public override void __64(Asset asset)
{
base.__64(asset);
m_scene = asset.__18();
m_durationOut = asset.__18();
m_durationWait = asset.__18();
m_durationIn = asset.__18();
m_color = asset.__18();
}
public override void __508()
{
string scene = G.__96(ref m_scene);
float durationOut = G.__117(G.__96(ref m_durationOut));
float durationWait = G.__117(G.__96(ref m_durationWait));
float durationIn = G.__117(G.__96(ref m_durationIn));
Color color = G.__122(G.__96(ref m_color));
if ( G.m_game.Jump(this, scene, color, durationOut, durationWait, durationIn)==false )
m_actionDone = true;
}
public override bool __509()
{
return m_actionDone;
}
}
public class RoleBoxActionLight : RoleBox
{
public const int ID = 0x306;
string m_scene;
string m_obj;
string m_visible;
string m_ambient;
string m_diffuse;
string m_angle;
string m_dir;
string m_dist;
string m_attn;
public override void __64(Asset asset)
{
base.__64(asset);
m_scene = asset.__18();
m_obj = asset.__18();
m_visible = asset.__18();
m_ambient = asset.__18();
m_diffuse = asset.__18();
m_angle = asset.__18();
m_dir = asset.__18();
m_dist = asset.__18();
m_attn = asset.__18();
}
public override void __508()
{
Scene scene = G.m_game.__276(ref m_scene);
if ( scene==null )
{
m_actionDone = true;
return;
}
string obj = G.__96(ref m_obj);
SceneObj sceneObj = scene.__277(obj);
if ( sceneObj==null )
{
m_actionDone = true;
return;
}
string visible = G.__96(ref m_visible);
if ( visible.Length>0 )
sceneObj.m_lightVisible.Set(G.__112(visible));
string ambient = G.__96(ref m_ambient);
if ( ambient.Length>0 )
sceneObj.m_lightAmbient.Set(G.__115(ambient));
string diffuse = G.__96(ref m_diffuse);
if ( diffuse.Length>0 )
sceneObj.m_lightDiffuse.Set(G.__123(m_diffuse));
string angle = G.__96(ref m_angle);
if ( angle.Length>0 )
sceneObj.m_lightAngle.Set(G.__113(ref angle) * G.DEG_TO_RAD);
string dir = G.__96(ref m_dir);
if ( dir.Length>0 )
sceneObj.m_lightDir.Set(G.__142(G.__113(ref dir)));
string dist = G.__96(ref m_dist);
if ( dist.Length>0 )
sceneObj.m_lightDist.Set((float)G.__113(ref dist));
string attn = G.__96(ref m_attn);
if ( attn.Length>0 )
sceneObj.m_lightAttn.Set(G.__113(ref attn)/100.0f);
sceneObj.m_lightMeshChanged = true;
scene.__561();
m_actionDone = true;
}
public override bool __509()
{
return m_actionDone;
}
}
public class RoleBoxActionPath : RoleBox
{
public const int ID = 0x308;
string m_scene;
string m_obj;
string m_path;
string m_spot;
string m_loop;
public override void __64(Asset asset)
{
base.__64(asset);
m_scene = asset.__18();
m_obj = asset.__18();
m_path = asset.__18();
m_spot = asset.__18();
m_loop = asset.__18();
}
public override void __508()
{
Scene currentScene = G.m_game.__291();
string scene = G.__96(ref m_scene);
if ( currentScene==null || (scene.Length>0 && currentScene.__48(ref scene)==false) )
{
m_actionDone = true;
return;
}
string obj = G.__96(ref m_obj);
SceneObj sceneObj = currentScene.__277(obj);
if ( sceneObj==null )
{
m_actionDone = true;
return;
}
int path = G.__113(G.__96(ref m_path));
string spot = G.__96(ref m_spot);
string loop = G.__96(ref m_loop);
int iloop = -1;
if ( loop.Length>0 )
{
iloop = G.__113(ref loop);
if ( iloop<0 )
iloop = -2;
}
if ( sceneObj.__646(path, spot, iloop, this)==false )
m_actionDone = true;
}
public override bool __509()
{
return m_actionDone;
}
}
public class RoleBoxActionPlayer : RoleBox
{
public const int ID = 0x315;
string m_player;
string m_scene;
string m_obj;
string m_switch;
string m_empty;
string m_transfer;
public override void __64(Asset asset)
{
base.__64(asset);
m_player = asset.__18();
m_scene = asset.__18();
m_obj = asset.__18();
m_switch = asset.__18();
m_empty = asset.__18();
m_transfer = asset.__18();
}
public override void __508()
{
Player player = G.m_game.__279(G.__96(ref m_player));
if ( player==null )
{
m_actionDone = true;
return;
}
if ( G.__112(G.__96(ref m_empty)) )
player.__481();
else
{
string transfer = G.__96(ref m_transfer);
if ( transfer.Length>0 )
{
Player playerTrg = G.m_game.__279(transfer);
if ( playerTrg )
player.__482(playerTrg);
}
}
string scene = G.__96(ref m_scene);
if ( scene.Length>0 )
player.__476(scene);
string obj = G.__96(ref m_obj);
if ( obj.Length>0 )
G.m_game.__305(player.m_uid, obj);
if ( G.__112(G.__96(ref m_switch)) )
G.m_game.__304(player.m_uid);
m_actionDone = true;
}
public override bool __509()
{
return m_actionDone;
}
}
public class RoleBoxActionPopup : RoleBox
{
public const int ID = 0x309;
string m_title;
string m_message;
string m_style;
public POPUPANSWER m_answer;
public override void __64(Asset asset)
{
base.__64(asset);
m_title = asset.__18();
m_message = asset.__18();
m_style = asset.__18();
}
public override void __508()
{
m_answer = POPUPANSWER.OK;
string title = G.__96(ref m_title);
string message = G.__96(ref m_message);
string style = G.__96(ref m_style);
POPUP id = G.__148(ref style, "QUESTION") ? POPUP.QUESTION : POPUP.DEFAULT;
G.Popup("", title, message, null, id, true, this);
}
public override bool __509()
{
if ( m_actionDone==false )
return false;
m_parent.__498(this, (int)m_answer);
return true;
}
}
public class RoleBoxActionRole : RoleBox
{
public const int ID = 0x30A;
string m_role;
public override void __64(Asset asset)
{
base.__64(asset);
m_role = asset.__18();
}
public override void __508()
{
string uid = G.__96(ref m_role);
Role role = G.m_game.__283(uid);
if ( role==null )
{
m_actionDone = true;
return;
}
if ( role.Start(this)==false )
m_actionDone = true;
}
public override bool __509()
{
return m_actionDone;
}
}
public class RoleBoxActionSelect : RoleBox
{
public const int ID = 0x316;
string m_player;
string m_scene;
string m_obj;
string m_sub;
public override void __64(Asset asset)
{
base.__64(asset);
m_player = asset.__18();
m_scene = asset.__18();
m_obj = asset.__18();
m_sub = asset.__18();
}
public override void __508()
{
Player currentPlayer = G.m_game.__293();
string player = G.__96(ref m_player);
if ( currentPlayer==null || (player.Length>0 && currentPlayer.__48(ref player)==false) )
{
m_actionDone = true;
return;
}
Scene currentScene = G.m_game.__291();
string scene = G.__96(ref m_scene);
if ( currentScene==null || (scene.Length>0 && currentScene.__48(ref scene)==false) )
{
m_actionDone = true;
return;
}
string obj = G.__96(ref m_obj);
SceneObj sceneObj = currentScene.__277(obj);
if ( sceneObj==null || sceneObj==currentPlayer.m_sceneObj )
{
m_actionDone = true;
return;
}
string sub = G.__96(ref m_sub);
SubObj subObj = sceneObj.m_obj.__469(ref sub);
SceneCell cell = currentPlayer.m_sceneObj.__636(sceneObj);
if ( cell==null )
{
currentPlayer.m_sceneObj.Stop();
m_actionDone = true;
G.m_game.__324(sceneObj.m_obj, subObj, false);
return;
}
Message msg = new Message();
msg.m_type = Message.SELECT;
msg.m_byUser = false;
msg.m_player = currentPlayer;
msg.m_sceneObj = sceneObj;
msg.m_subObj = subObj;
SceneCellLink link = cell.__600(LINK.SELECT);
msg.m_dist = link.m_dist;
if ( msg.m_dist==0 )
{
msg.m_anim = currentPlayer.m_obj.__470(ref link.m_anim);
msg.m_dir = link.m_dir;
}
msg.m_roleBox = this;
msg.m_roleBoxToken = m_parent.m_token;
msg.m_state = Message.S_MOVE;
if ( currentPlayer.m_sceneObj.__643(cell.__35(), cell.__36(), msg, true)==false )
{
m_actionDone = true;
return;
}
}
public override bool __509()
{
return m_actionDone;
}
}
public class RoleBoxActionShow : RoleBox
{
public const int ID = 0x312;
string m_visible;
string m_duration;
string m_brightness;
string m_scene;
string m_layer;
string m_still;
string m_obj;
string m_light;
string m_label;
string m_cursor;
string m_bag;
string m_menu;
string m_credits;
string m_dialog;
string m_choice;
public override void __64(Asset asset)
{
base.__64(asset);
m_visible = asset.__18();
m_duration = asset.__18();
m_brightness = asset.__18();
m_scene = asset.__18();
m_layer = asset.__18();
m_still = asset.__18();
m_obj = asset.__18();
m_light = asset.__18();
m_label = asset.__18();
m_cursor = asset.__18();
m_bag = asset.__18();
m_menu = asset.__18();
m_credits = asset.__18();
m_dialog = asset.__18();
m_choice = asset.__18();
}
public override void __508()
{
string scene = G.__96(ref m_scene);
Scene assetScene = scene.Length>0 ? G.m_game.__274(scene) : G.m_game.__291();
string brightness = G.__96(ref m_brightness);
string layer = G.__96(ref m_layer);
string still = G.__96(ref m_still);
string obj = G.__96(ref m_obj);
string light = G.__96(ref m_light);
string label = G.__96(ref m_label);
string choice = G.__96(ref m_choice);
string cursor = G.__96(ref m_cursor);
string bag = G.__96(ref m_bag);
string menu = G.__96(ref m_menu);
string credits = G.__96(ref m_credits);
string dialog = G.__96(ref m_dialog);
Dialog assetDialog = dialog.Length>0 ? G.m_game.__278(dialog) : null;
bool visible = G.__112(G.__96(ref m_visible));
float duration = G.__117(G.__96(ref m_duration));
bool async = false;
if ( brightness.Length>0 )
G.m_game.m_brightness = G.__119(brightness);
if ( assetScene )
{
if ( layer.Length>0 )
{
if ( assetScene.__545(layer, visible, this, duration) )
{
if ( duration!=0.0f )
async = true;
}
}
if ( still.Length>0 )
{
SceneStill item = assetScene.__540(still);
if ( item )
item.m_visible.Set(visible);
}
if ( obj.Length>0 )
G.m_game.__306(assetScene.m_uid, obj, visible);
if ( light.Length>0 )
{
SceneObj item = assetScene.__277(light);
if ( item )
item.__625(visible);
}
if ( label.Length>0 )
{
SceneLabel item = assetScene.__536(label);
if ( item )
item.m_visible.Set(visible);
}
}
if ( assetDialog && choice.Length>0 )
assetDialog.__50(G.__113(choice), visible);
if ( cursor.Length>0 )
G.m_game.m_cursorVisible = G.__112(ref cursor);
if ( bag.Length>0 )
G.m_game.m_layout.m_bagForceHidden = !G.__112(ref bag);
if ( menu.Length>0 )
{
if ( G.__112(ref menu) )
G.m_game.__257();
else
G.m_game.__258();
}
if ( credits.Length>0 )
{
if ( G.__112(ref credits) )
G.m_game.__259();
else if ( G.m_game.m_menuGame.m_id==MenuGame.ID_CREDITS )
G.m_game.__258();
}
if ( async==false )
m_actionDone = true;
}
public override bool __509()
{
return m_actionDone;
}
}
public class RoleBoxActionSong : RoleBox
{
public const int ID = 0x313;
string m_song;
string m_channel;
string m_volume;
string m_loop;
string m_crossfade;
string m_last;
public override void __64(Asset asset)
{
base.__64(asset);
m_song = asset.__18();
m_channel = asset.__18();
m_volume = asset.__18();
m_loop = asset.__18();
m_crossfade = asset.__18();
m_last = asset.__18();
}
public override void __508()
{
string name = G.__96(ref m_song);
int channel = G.Clamp(G.__113(G.__96(ref m_channel)), -1, G.CHANNEL_COUNT-1);
float volume = G.__118(G.__96(ref m_volume));
bool loop = G.__112(G.__96(ref m_loop));
float crossfade = G.__117(G.__96(ref m_crossfade));
bool last = G.__112(G.__96(ref m_last));
if ( crossfade==0.0f )
{
if ( last )
{
if ( channel!=-1 )
{
Song song = G.m_game.m_songs[channel];
G.m_game.__264(channel, song.m_lastName, song.m_lastVolume, song.m_lastLoop);
}
}
else if ( name.Length==0 )
G.m_game.__262(channel);
else
G.m_game.__264(channel, name, volume, loop);
}
else
{
if ( channel!=-1 )
{
Song song = G.m_game.m_songs[channel];
if ( last )
G.m_game.__265(channel, song.m_lastName, crossfade, song.m_lastVolume, song.m_lastLoop);
else if ( song.m_current==null || G.__150(ref name, ref song.m_current.m_name) )
G.m_game.__265(channel, name, crossfade, volume, loop);
}
}
m_actionDone = true;
}
public override bool __509()
{
return m_actionDone;
}
}
public class RoleBoxActionStop : RoleBox
{
public const int ID = 0x311;
string m_obj;
string m_role;
string m_camera;
string m_dialog;
string m_timeline;
string m_cinematic;
string m_song;
public override void __64(Asset asset)
{
base.__64(asset);
m_obj = asset.__18();
m_role = asset.__18();
m_camera = asset.__18();
m_dialog = asset.__18();
m_timeline = asset.__18();
m_cinematic = asset.__18();
m_song = asset.__18();
}
public override void __508()
{
string channel = G.__96(ref m_song);
if ( channel.Length>0 )
G.m_game.__262(G.__113(ref channel));
string cinematic = G.__96(ref m_cinematic);
if ( G.__112(ref cinematic) )
G.m_game.m_cinematicPlayer.Stop();
string dialog = G.__96(ref m_dialog);
if ( G.__112(ref dialog) )
G.m_game.__309();
string role = G.__96(ref m_role);
if ( role.Length>0 )
{
Role r = G.m_game.__283(role);
if ( r )
r.Stop();
}
Scene currentScene = G.m_game.__291();
if ( currentScene )
{
string timeline = G.__96(ref m_timeline);
if ( G.__112(ref timeline) )
G.m_game.__273();
string camera = G.__96(ref m_camera);
if ( G.__112(ref camera) )
currentScene.__567(G.m_game.__293());
string obj = G.__96(ref m_obj);
if ( obj.Length>0 )
{
SceneObj sceneObj = currentScene.__277(obj);
if ( sceneObj )
{
sceneObj.__645();
sceneObj.__647();
}
}
}
m_actionDone = true;
}
public override bool __509()
{
return m_actionDone;
}
}
public class RoleBoxActionSuccess : RoleBox
{
public const int ID = 0x30C;
int m_puzzle;
public override void __64(Asset asset)
{
base.__64(asset);
m_puzzle = asset.__15();
}
public override void __508()
{
G.Success(m_puzzle);
}
public override bool __509()
{
if ( m_actionDone==false )
{
m_actionDone = true;
return false;
}
return true;
}
}
public class RoleBoxActionTalk : RoleBox
{
public const int ID = 0x30D;
string m_dialog;
string m_sentenceStart;
string m_sentenceEnd;
public override void __64(Asset asset)
{
base.__64(asset);
m_dialog = asset.__18();
m_sentenceStart = asset.__18();
m_sentenceEnd = asset.__18();
}
public override void __508()
{
string dialog = G.__96(ref m_dialog);
string start = G.__96(ref m_sentenceStart);
string end = G.__96(ref m_sentenceEnd);
if ( G.m_game.__308(dialog, G.__113(ref start), this, G.__113(ref end))==false )
m_actionDone = true;
}
public override bool __509()
{
return m_actionDone;
}
}
public class RoleBoxActionTake : RoleBox
{
public const int ID = 0x314;
string m_player;
string m_obj;
string m_silent;
string m_replace;
string m_anim;
string m_dir;
string m_frame;
public override void __64(Asset asset)
{
base.__64(asset);
m_player = asset.__18();
m_obj = asset.__18();
m_silent = asset.__18();
m_replace = asset.__18();
m_anim = asset.__18();
m_dir = asset.__18();
m_frame = asset.__18();
}
public override void __508()
{
Player player = __293();
if ( player==null )
{
m_actionDone = true;
return;
}
string anim = G.__96(ref m_anim);
if ( anim.Length>0 )
{
SceneObj sceneObj = player.m_sceneObj;
if ( sceneObj==null )
{
m_actionDone = true;
return;
}
sceneObj.__626(ref anim);
string dir = G.__96(ref m_dir);
if ( dir.Length>0 )
sceneObj.m_anim.__672(G.__151(ref dir));
string frame = G.__96(ref m_frame);
if ( frame.Length>0 )
sceneObj.m_anim.notif.actionFrame = G.__113(ref frame);
sceneObj.m_anim.notif.roleBox = this;
sceneObj.m_anim.notif.roleBoxToken = m_parent.m_token;
}
else
{
Take();
m_actionDone = true;
}
}
public override bool __509()
{
return m_actionDone;
}
public override bool __510()
{
Take();
return true;
}
Player __293()
{
string player = G.__96(ref m_player);
if ( player.Length>0 )
{
if ( G.__148(player, "DUMP") )
return G.m_game.__290();
else
return G.m_game.__279(player);
}
return G.m_game.__293();
}
void Take()
{
Player player = __293();
if ( player==null )
return;
string obj = G.__96(ref m_obj);
if ( obj.Length==0 )
return;
bool silent = G.__112(G.__96(ref m_silent));
string replace = G.__96(ref m_replace);
if ( replace.Length>0 )
{
Replace(player, ref obj, ref replace, silent);
}
else
{
if ( obj[0]=='@' )
{
string tag = obj.Substring(1);
for ( int i=0 ; i<G.m_game.m_objects.Length ; i++ )
{
Obj next = G.m_game.m_objects[i];
if ( next.__48(ref tag) )
Add(player, ref next.m_uid, silent);
}
}
else
{
Add(player, ref obj, silent);
}
}
}
void Add(Player player, ref string uid, bool silent)
{
G.m_game.__306("", uid, false);
Obj obj = G.m_game.__277(uid);
Player objPlayer = obj.__293();
if ( objPlayer )
objPlayer.__480(obj);
player.__479(obj);
if ( silent==false && obj.__453() )
G.m_game.__313(obj);
}
void Replace(Player player, ref string uid, ref string uidReplace, bool silent)
{
G.m_game.__306("", uid, false);
Obj obj = G.m_game.__277(uid);
Player objPlayer = obj.__293();
G.m_game.__306("", uidReplace, false);
Obj objReplace = G.m_game.__277(uidReplace);
Player objPlayerReplace = objReplace.__293();
if ( objPlayer==null || objPlayer!=player || obj==null )
return;
if ( player.__484(obj)==-1 )
return;
if ( objPlayerReplace )
objPlayerReplace.__480(objReplace);
int index = player.__484(obj);
if ( index==-1 )
return;
objPlayer.__480(obj);
G.m_game.__290().__479(obj);
player.__479(objReplace, index);
if ( silent==false && objReplace.__453() )
G.m_game.__313(objReplace);
}
}
public class RoleBoxActionTask : RoleBox
{
public const int ID = 0x317;
string m_name;
string m_arg;
public override void __64(Asset asset)
{
base.__64(asset);
m_name = asset.__18();
m_arg = asset.__18();
}
public override void __508()
{
string name = G.__96(ref m_name);
string arg = G.__96(ref m_arg);
bool result = false;
if ( m_parent.Task(ref name, ref arg, ref result) )
{
if ( result==false )
m_actionDone = true;
}
}
public override bool __509()
{
return m_actionDone;
}
}
public class RoleBoxActionTimeline : RoleBox
{
public const int ID = 0x30E;
string m_scene;
string m_timeline;
public override void __64(Asset asset)
{
base.__64(asset);
m_scene = asset.__18();
m_timeline = asset.__18();
}
public override void __508()
{
Scene currentScene = G.m_game.__291();
string scene = G.__96(ref m_scene);
if ( currentScene==null || (scene.Length>0 && currentScene.__48(ref scene)==false) )
{
m_actionDone = true;
return;
}
string tl = G.__96(ref m_timeline);
Timeline timeline = currentScene.__541(tl);
if ( timeline==null )
{
m_actionDone = true;
return;
}
G.m_game.__272(timeline, this, 0.0f);
}
public override bool __509()
{
return m_actionDone;
}
}
public class RoleBoxActionWalk : RoleBox
{
public const int ID = 0x30F;
string m_scene;
string m_obj;
string m_cell;
string m_x;
string m_y;
string m_cell2;
string m_x2;
string m_y2;
string m_anim;
string m_dir;
string m_straight;
string m_duration;
string m_easeIn;
string m_easeOut;
public override void __64(Asset asset)
{
base.__64(asset);
m_scene = asset.__18();
m_obj = asset.__18();
m_cell = asset.__18();
m_x = asset.__18();
m_y = asset.__18();
m_cell2 = asset.__18();
m_x2 = asset.__18();
m_y2 = asset.__18();
m_anim = asset.__18();
m_dir = asset.__18();
m_straight = asset.__18();
m_duration = asset.__18();
m_easeIn = asset.__18();
m_easeOut = asset.__18();
}
public override void __508()
{
Scene currentScene = G.m_game.__291();
string scene = G.__96(ref m_scene);
if ( currentScene==null || (scene.Length>0 && currentScene.__48(ref scene)==false) )
{
m_actionDone = true;
return;
}
string obj = G.__96(ref m_obj);
SceneObj sceneObj = currentScene.__277(obj);
if ( sceneObj==null )
{
m_actionDone = true;
return;
}
bool hasFrom = false;
float fromX = 0.0f;
float fromY = 0.0f;
SceneCell fromCell = sceneObj.__631(G.__96(ref m_cell2));
if ( fromCell==null )
{
string x = G.__96(ref m_x2);
string y = G.__96(ref m_y2);
if ( x.Length>0 || y.Length>0 )
{
hasFrom = true;
fromX = x.Length>0 ? G.__113(ref x) : sceneObj.__35();
fromY = y.Length>0 ? G.__113(ref y) : sceneObj.__36();
}
}
else
{
hasFrom = true;
fromX = fromCell.__35();
fromY = fromCell.__36();
}
if ( hasFrom )
{
sceneObj.Move(fromX, fromY);
sceneObj.__656();
G.m_game.m_input.__368();
}
bool hasTo = false;
float toX = 0.0f;
float toY = 0.0f;
SceneCell toCell = sceneObj.__631(G.__96(ref m_cell));
if ( toCell==null )
{
string x = G.__96(ref m_x);
string y = G.__96(ref m_y);
if ( x.Length>0 || y.Length>0 )
{
hasTo = true;
toX = x.Length>0 ? G.__113(ref x) : sceneObj.__35();
toY = y.Length>0 ? G.__113(ref y) : sceneObj.__36();
}
}
else
{
hasTo = true;
toX = toCell.__35();
toY = toCell.__36();
}
if ( hasTo==false )
{
m_actionDone = true;
return;
}
Message msg = new Message();
msg.m_type = Message.WALK;
msg.m_roleBox = this;
msg.m_roleBoxToken = m_parent.m_token;
msg.m_state = Message.S_MOVE;
string anim = G.__96(ref m_anim);
if ( anim.Length>0 )
msg.m_anim = sceneObj.m_obj.__470(anim);
string dir = G.__96(ref m_dir);
if ( dir.Length>0 )
msg.m_dir = G.__151(ref dir);
if ( G.__112(G.__96(ref m_straight)) )
{
float duration = G.__117(G.__96(ref m_duration));
bool easeIn = G.__112(G.__96(ref m_easeIn));
bool easeOut = G.__112(G.__96(ref m_easeOut));
if ( sceneObj.__644(toX, toY, duration, easeIn, easeOut, msg)==false )
m_actionDone = true;
}
else
{
if ( sceneObj.__643(toX, toY, msg)==false )
m_actionDone = true;
}
}
public override bool __509()
{
return m_actionDone;
}
}
public class RoleBoxConditionBool : RoleBox
{
public const int ID = 0x401;
string m_variable;
public override void __64(Asset asset)
{
base.__64(asset);
m_variable = asset.__18();
}
public override bool __509()
{
int count = 0;
Variable var = G.m_game.__286(ref m_variable);
if ( var==null || G.__112(var.m_value)==false )
count += m_parent.__498(this, 1);
else
count += m_parent.__498(this, 0);
if ( m_base==BASE.WHILE )
return count>0;
return true;
}
}
public class RoleBoxConditionCall : RoleBox
{
public const int ID = 0x402;
public override void __64(Asset asset)
{
base.__64(asset);
}
public override bool __509()
{
int count = 0;
if ( m_script && m_script.m_instructions.Length>0 )
{
bool res = m_script.m_instructions[0].__684();
count += m_parent.__498(this, res ? 0 : 1);
}
if ( m_base==BASE.WHILE )
return count>0;
return true;
}
}
public class RoleBoxConditionDialog : RoleBox
{
public const int ID = 0x403;
string m_dialog;
string m_sentence;
public override void __64(Asset asset)
{
base.__64(asset);
m_dialog = asset.__18();
m_sentence = asset.__18();
}
public override bool __509()
{
bool ok = true;
if ( G.m_game.m_menuDialog.__38()==false || G.m_game.m_menuDialog.m_sentence==null )
ok = false;
else
{
string dialog = G.__96(ref m_dialog);
if ( dialog.Length>0 && G.m_game.m_menuDialog.m_dialog.__48(ref dialog)==false )
ok = false;
else
{
string sen = G.__96(ref m_sentence);
if ( sen.Length>0 && G.m_game.m_menuDialog.m_sentence.__48(ref sen)==false )
ok = false;
}
}
int count = m_parent.__498(this, ok ? 0 : 1);
if ( m_base==BASE.WHILE )
return count>0;
return true;
}
}
public class RoleBoxConditionDialogItem : RoleBox
{
public const int ID = 0x40B;
string m_dialog;
string m_choice;
string m_said;
public override void __64(Asset asset)
{
base.__64(asset);
m_dialog = asset.__18();
m_choice = asset.__18();
m_said = asset.__18();
}
public override bool __509()
{
bool ok = true;
Dialog dlg;
string dialog = G.__96(ref m_dialog);
if ( dialog.Length>0 )
dlg = G.m_game.__278(dialog);
else
dlg = G.m_game.m_menuDialog.m_dialog;
if ( dlg==null )
ok = false;
else
{
int choice = G.__113(G.__96(ref m_choice));
if ( choice!=0 )
{
Sentence sentence = dlg.m_root.__49(choice);
if ( sentence==null || sentence.m_choice==false || sentence.m_visible.cur==false )
ok = false;
}
int said = G.__113(G.__96(ref m_said));
if ( said!=0 )
{
Sentence sentence = dlg.m_root.__49(said);
if ( sentence==null || sentence.m_visited==false )
ok = false;
}
}
int count = m_parent.__498(this, ok ? 0 : 1);
if ( m_base==BASE.WHILE )
return count>0;
return true;
}
}
public class RoleBoxConditionList : RoleBox
{
public const int ID = 0x404;
string[] m_values = new string[4];
string m_variable;
public override void __64(Asset asset)
{
base.__64(asset);
for ( int i=0 ; i<4 ; i++ )
m_values[i] = asset.__18();
m_variable = asset.__18();
}
public override bool __509()
{
int count = 0;
bool found = false;
Variable var = G.m_game.__286(ref m_variable);
if ( var )
{
for ( int i=0 ; i<4 ; i++ )
{
string value = G.__96(ref m_values[i]);
if ( var.m_value==value )
{
count += m_parent.__498(this, i);
found = true;
}
}
}
if ( found==false )
count += m_parent.__498(this, 4);
if ( m_base==BASE.WHILE )
return count>0;
return true;
}
}
public class RoleBoxConditionObject : RoleBox
{
public const int ID = 0x405;
string m_scene;
string m_obj;
string m_anim;
string m_dir;
string m_frame;
string m_cell;
string m_flag;
string m_sticker;
string m_visible;
public override void __64(Asset asset)
{
base.__64(asset);
m_scene = asset.__18();
m_obj = asset.__18();
m_anim = asset.__18();
m_dir = asset.__18();
m_frame = asset.__18();
m_cell = asset.__18();
m_flag = asset.__18();
m_sticker = asset.__18();
m_visible = asset.__18();
}
public override bool __509()
{
bool ok = true;
string scene = G.__96(ref m_scene);
if ( scene.Length>0 && G.m_game.__292(ref scene)==false )
ok = false;
else
{
Scene currentScene = G.m_game.__291();
string obj = G.__96(ref m_obj);
SceneObj sceneObj = currentScene==null ? null : currentScene.__277(obj);
if ( sceneObj==null )
ok = false;
else
{
string currentVisible = sceneObj.m_visible.cur ? "true" : "false";
string anim = G.__96(ref m_anim);
string dir = G.__96(ref m_dir);
string frame = G.__96(ref m_frame);
string cell = G.__96(ref m_cell);
string flag = G.__96(ref m_flag);
string sticker = G.__96(ref m_sticker);
string visible = G.__96(ref m_visible);
if ( anim.Length>0 && G.__150(ref anim, sceneObj.m_anim.__392()) )
ok = false;
else if ( dir.Length>0 && G.__150(ref dir, sceneObj.m_anim.__674()) )
ok = false;
else if ( frame.Length>0 && sceneObj.m_anim.__679(G.__113(ref frame))==false )
ok = false;
else if ( cell.Length>0 && sceneObj.__631(cell, true)==null )
ok = false;
else if ( sticker.Length>0 && sticker!=sceneObj.m_sticker.cur )
ok = false;
else if ( visible.Length>0 && G.__150(ref visible, currentVisible) )
ok = false;
else if ( flag.Length>0 )
{
SceneCell foundCell = sceneObj.__629(sceneObj.__35(), sceneObj.__36());
if ( foundCell==null || G.__150(ref flag, foundCell.__593().ToString()) )
ok = false;
}
}
}
int count = m_parent.__498(this, ok ? 0 : 1);
if ( m_base==BASE.WHILE )
return count>0;
return true;
}
}
public class RoleBoxConditionPlayer : RoleBox
{
public const int ID = 0x406;
string m_player;
string m_obj;
public override void __64(Asset asset)
{
base.__64(asset);
m_player = asset.__18();
m_obj = asset.__18();
}
public override bool __509()
{
Player player = G.m_game.__293();
string play = G.__96(ref m_player);
string obj = G.__96(ref m_obj);
bool ok = true;
if ( play.Length>0 && (player==null || player.__48(ref play)==false) )
ok = false;
else if ( obj.Length>0 && (player==null || player.m_sceneObj==null || player.m_sceneObj.__48(ref obj)==false) )
ok = false;
int count = m_parent.__498(this, ok ? 0 : 1);
if ( m_base==BASE.WHILE )
return count>0;
return true;
}
}
public class RoleBoxConditionPlayerItem : RoleBox
{
public const int ID = 0x40A;
string m_player;
string m_obj;
public override void __64(Asset asset)
{
base.__64(asset);
m_player = asset.__18();
m_obj = asset.__18();
}
public override bool __509()
{
string play = G.__96(ref m_player);
string obj = G.__96(ref m_obj);
Player player;
if ( play.Length==0 )
player = G.m_game.__293();
else if ( G.__148(ref play, "DUMP") )
player = G.m_game.__290();
else
player = G.m_game.__279(play);
bool ok = true;
if ( player==null )
ok = false;
else if ( obj.Length>0 && player.__483(ref obj)==false )
ok = false;
int count = m_parent.__498(this, ok ? 0 : 1);
if ( m_base==BASE.WHILE )
return count>0;
return true;
}
}
public class RoleBoxConditionPuzzle : RoleBox
{
public const int ID = 0x407;
int m_puzzle;
public override void __64(Asset asset)
{
base.__64(asset);
m_puzzle = asset.__15();
}
public override bool __509()
{
int count = 0;
Box box = G.m_game.m_scenario.__496(m_puzzle);
if ( box && box.m_type==Box.TYPE.PUZZLE )
{
if ( box.m_exit )
count += m_parent.__498(this, 0);
else
{
if ( box.m_enter )
count += m_parent.__498(this, 1);
count += m_parent.__498(this, 2);
}
if ( count==0 )
count += m_parent.__498(this, 3);
}
if ( m_base==BASE.WHILE )
return count>0;
return true;
}
}
public class RoleBoxConditionScene : RoleBox
{
public const int ID = 0x408;
string m_scene;
string m_scene2;
public override void __64(Asset asset)
{
base.__64(asset);
m_scene = asset.__18();
m_scene2 = asset.__18();
}
public override bool __509()
{
bool ok = true;
string scene = G.__96(ref m_scene);
string scene2 = G.__96(ref m_scene2);
if ( scene.Length>0 && G.m_game.__316(ref scene)==false )
ok = false;
else if ( scene2.Length>0 && G.m_game.__317(ref scene2)==false )
ok = false;
int count = m_parent.__498(this, ok ? 0 : 1);
if ( m_base==BASE.WHILE )
return count>0;
return true;
}
}
public class RoleBoxConditionSceneItem : RoleBox
{
public const int ID = 0x40C;
string m_scene;
string m_still;
string m_obj;
string m_label;
public override void __64(Asset asset)
{
base.__64(asset);
m_scene = asset.__18();
m_still = asset.__18();
m_obj = asset.__18();
m_label = asset.__18();
}
public override bool __509()
{
bool ok = true;
string scene = G.__96(ref m_scene);
Scene currentScene = scene.Length>0 ? G.m_game.__274(scene) : G.m_game.__291();
if ( currentScene==null )
ok = false;
if ( ok )
{
string still = G.__96(ref m_still);
if ( still.Length>0 )
{
SceneStill item = currentScene.__540(still);
if ( item==null || item.m_visible.cur==false )
ok = false;
}
}
if ( ok )
{
string obj = G.__96(ref m_obj);
if ( obj.Length>0 )
{
SceneObj item = currentScene.__277(obj);
if ( item==null || item.m_visible.cur==false )
ok = false;
}
}
if ( ok )
{
string label = G.__96(ref m_label);
if ( label.Length>0 )
{
SceneLabel item = currentScene.__536(label);
if ( item==null || item.m_visible.cur==false )
ok = false;
}
}
int count = m_parent.__498(this, ok ? 0 : 1);
if ( m_base==BASE.WHILE )
return count>0;
return true;
}
}
public class RoleBoxConditionSequence : RoleBox
{
public const int ID = 0x409;
int m_sequence;
public override void __64(Asset asset)
{
base.__64(asset);
m_sequence = asset.__15();
}
public override bool __509()
{
int count = 0;
Box box = G.m_game.m_scenario.__496(m_sequence);
if ( box && box.m_type==Box.TYPE.SEQSTART )
{
if ( box.m_exit==false )
count += m_parent.__498(this, 3);
else if ( box.m_seqRef!=0 )
{
Box boxEnd = G.m_game.m_scenario.__496(box.m_seqRef);
if ( boxEnd && box.m_type==Box.TYPE.SEQEND )
{
count += m_parent.__498(this, 0);
if ( boxEnd.m_exit )
count += m_parent.__498(this, 2);
else
count += m_parent.__498(this, 1);
}
}
if ( count==0 )
count += m_parent.__498(this, 4);
}
if ( m_base==BASE.WHILE )
return count>0;
return true;
}
}
public class RoleBoxEventCell : RoleBox
{
public const int ID = 0x501;
string m_scene;
string m_obj;
string m_cell;
string m_mode;
public override void __64(Asset asset)
{
base.__64(asset);
m_scene = asset.__18();
m_obj = asset.__18();
m_cell = asset.__18();
m_mode = asset.__18();
}
public override bool __467(Event evt)
{
string scene = G.__96(ref m_scene);
if ( scene.Length>0 && G.m_game.__316(ref scene)==false )
return false;
string obj = G.__96(ref m_obj);
if ( obj.Length>0 && G.m_game.__319(ref evt.p1, ref obj)==false )
return false;
string cell = G.__96(ref m_cell);
if ( obj.Length>0 && G.__150(ref evt.p2, ref cell) )
return false;
string mode = G.__96(ref m_mode);
if ( mode.Length>0 && G.__150(ref evt.p3, ref mode) )
return false;
G.m_game.m_sysVarObj.m_value = evt.p1;
return true;
}
}
public class RoleBoxEventChoice : RoleBox
{
public const int ID = 0x515;
string m_dialog;
string m_choice;
public override void __64(Asset asset)
{
base.__64(asset);
m_dialog = asset.__18();
m_choice = asset.__18();
}
public override bool __467(Event evt)
{
string dialog = G.__96(ref m_dialog);
if ( dialog.Length>0 && G.__148(ref evt.p1, ref dialog)==false )
return false;
Sentence sentence = G.m_game.__49(evt.p1, G.__113(ref evt.p2));
if ( sentence==null )
return false;
string choice = G.__96(ref m_choice);
if ( choice.Length>0 && sentence.__48(ref choice)==false )
return false;
return true;
}
}
public class RoleBoxEventClick : RoleBox
{
public const int ID = 0x50E;
string m_scene;
string m_x;
string m_y;
string m_w;
string m_h;
public override void __64(Asset asset)
{
base.__64(asset);
m_scene = asset.__18();
m_x = asset.__18();
m_y = asset.__18();
m_w = asset.__18();
m_h = asset.__18();
}
public override bool __467(Event evt)
{
string scene = G.__96(ref m_scene);
if ( scene.Length>0 && G.m_game.__316(ref scene)==false )
return false;
Rect rc;
rc.x = G.__113(G.__96(ref m_x));
rc.y = G.__113(G.__96(ref m_y));
rc.width = G.__113(G.__96(ref m_w));
rc.height = G.__113(G.__96(ref m_h));
if ( rc.x==0.0f && rc.y==0.0f && rc.width==0.0f && rc.height==0.0f )
return true;
Vec2 pt = G.m_game.__299();
return rc.Contains(pt.x, pt.y);
}
}
public class RoleBoxEventDetach : RoleBox
{
public const int ID = 0x502;
string m_obj;
public override void __64(Asset asset)
{
base.__64(asset);
m_obj = asset.__18();
}
public override bool __467(Event evt)
{
string obj = G.__96(ref m_obj);
if ( obj.Length>0 && G.m_game.__319(ref evt.p1, ref obj)==false )
return false;
G.m_game.m_sysVarObj.m_value = evt.p1;
return true;
}
}
public class RoleBoxEventDrag : RoleBox
{
public const int ID = 0x50F;
string m_scene;
string m_obj;
string m_sub;
public override void __64(Asset asset)
{
base.__64(asset);
m_scene = asset.__18();
m_obj = asset.__18();
m_sub = asset.__18();
}
public override bool __467(Event evt)
{
string scene = G.__96(ref m_scene);
if ( scene.Length>0 && G.m_game.__316(ref scene)==false )
return false;
string obj = G.__96(ref m_obj);
if ( obj.Length>0 && G.m_game.__319(ref evt.p1, ref obj)==false )
return false;
string sub = G.__96(ref m_sub);
if ( sub.Length>0 && G.m_game.__321(ref sub)==false )
return false;
G.m_game.m_sysVarObj.m_value = evt.p1;
return true;
}
}
public class RoleBoxEventDrop : RoleBox
{
public const int ID = 0x510;
string m_scene;
string m_obj;
public override void __64(Asset asset)
{
base.__64(asset);
m_scene = asset.__18();
m_obj = asset.__18();
}
public override bool __467(Event evt)
{
string scene = G.__96(ref m_scene);
if ( scene.Length>0 && G.m_game.__316(ref scene)==false )
return false;
string obj = G.__96(ref m_obj);
if ( obj.Length>0 && G.m_game.__319(ref evt.p1, ref obj)==false )
return false;
G.m_game.m_sysVarObj.m_value = evt.p1;
return true;
}
}
public class RoleBoxEventEnterDialog : RoleBox
{
public const int ID = 0x518;
string m_dialog;
public override void __64(Asset asset)
{
base.__64(asset);
m_dialog = asset.__18();
}
public override bool __467(Event evt)
{
string dialog = G.__96(ref m_dialog);
if ( dialog.Length>0 && G.m_game.__315(ref evt.p1, ref dialog)==false )
return false;
return true;
}
}
public class RoleBoxEventEnterScene : RoleBox
{
public const int ID = 0x504;
string m_scene;
string m_mode;
string m_cell;
public override void __64(Asset asset)
{
base.__64(asset);
m_scene = asset.__18();
m_mode = asset.__18();
m_cell = asset.__18();
}
public override bool __467(Event evt)
{
string scene = G.__96(ref m_scene);
if ( scene.Length>0 && G.m_game.__316(ref scene)==false )
return false;
string mode = G.__96(ref m_mode);
if ( mode.Length>0 && G.__150(ref evt.p1, ref mode) )
return false;
if ( evt.p1=="WALK" )
{
string cell = G.__96(ref m_cell);
if ( cell.Length>0 && G.__150(ref evt.p2, ref cell) )
return false;
}
return true;
}
}
public class RoleBoxEventExitDialog : RoleBox
{
public const int ID = 0x519;
string m_dialog;
public override void __64(Asset asset)
{
base.__64(asset);
m_dialog = asset.__18();
}
public override bool __467(Event evt)
{
string dialog = G.__96(ref m_dialog);
if ( dialog.Length>0 && G.m_game.__315(ref evt.p1, ref dialog)==false )
return false;
return true;
}
}
public class RoleBoxEventExitScene : RoleBox
{
public const int ID = 0x50D;
string m_scene;
public override void __64(Asset asset)
{
base.__64(asset);
m_scene = asset.__18();
}
public override bool __467(Event evt)
{
string scene = G.__96(ref m_scene);
if ( scene.Length>0 && G.m_game.__316(ref scene)==false )
return false;
return true;
}
}
public class RoleBoxEventInput : RoleBox
{
public const int ID = 0x513;
string m_button;
public override void __64(Asset asset)
{
base.__64(asset);
m_button = asset.__18();
}
public override bool __467(Event evt)
{
string button = G.__96(ref m_button);
if ( button.Length>0 && evt.p1!=button )
return false;
return true;
}
}
public class RoleBoxEventLabel : RoleBox
{
public const int ID = 0x505;
string m_scene;
string m_label;
string m_move;
public override void __64(Asset asset)
{
base.__64(asset);
m_scene = asset.__18();
m_label = asset.__18();
m_move = asset.__18();
}
public override bool __467(Event evt)
{
string scene = G.__96(ref m_scene);
if ( scene.Length>0 && G.m_game.__316(ref scene)==false )
return false;
string label = G.__96(ref m_label);
if ( label.Length>0 && G.m_game.__320(ref evt.p1, ref label)==false )
return false;
string move = G.__96(ref m_move);
if ( move.Length>0 && G.__150(ref evt.p2, ref move) )
return false;
G.m_game.m_sysVarLabel.m_value = evt.p1;
return true;
}
}
public class RoleBoxEventLayout : RoleBox
{
public const int ID = 0x50C;
string m_button;
public override void __64(Asset asset)
{
base.__64(asset);
m_button = asset.__18();
}
public override bool __467(Event evt)
{
string button = G.__96(ref m_button);
if ( button.Length>0 && G.__150(ref evt.p1, ref button) )
return false;
return true;
}
}
public class RoleBoxEventPuzzle : RoleBox
{
public const int ID = 0x517;
public int m_puzzle;
public override void __64(Asset asset)
{
base.__64(asset);
m_puzzle = asset.__15();
}
public override void __46(JsonObj jBox)
{
base.__46(jBox);
}
public override void __47(JsonObj jBox)
{
base.__47(jBox);
}
public override bool __467(Event evt)
{
if ( m_puzzle!=G.__113(ref evt.p1) )
return false;
return true;
}
}
public class RoleBoxEventRepeat : RoleBox
{
public const int ID = 0x516;
public string m_duration;
public float m_elapsed;
public uint m_iRender;
public override void __64(Asset asset)
{
base.__64(asset);
m_duration = asset.__18();
}
public override void __46(JsonObj jBox)
{
base.__46(jBox);
jBox.__383("elapsed", m_elapsed);
}
public override void __47(JsonObj jBox)
{
base.__47(jBox);
m_elapsed = jBox.GetFloat("elapsed");
}
public override void __507()
{
m_elapsed = 0.0f;
}
public override bool __467(Event evt)
{
if ( m_iRender!=CameraBehavior.s_iRender )
return false;
return true;
}
}
public class RoleBoxEventSay : RoleBox
{
public const int ID = 0x506;
string m_dialog;
string m_sentence;
string m_said;
string m_sub;
public override void __64(Asset asset)
{
base.__64(asset);
m_dialog = asset.__18();
m_sentence = asset.__18();
m_said = asset.__18();
m_sub = asset.__18();
}
public override bool __467(Event evt)
{
string dialog = G.__96(ref m_dialog);
if ( dialog.Length>0 && G.__148(ref evt.p1, ref dialog)==false )
return false;
Sentence sentence = G.m_game.__49(evt.p1, G.__113(ref evt.p2));
if ( sentence==null )
return false;
string sen = G.__96(ref m_sentence);
if ( sen.Length>0 && sentence.__48(ref sen)==false )
return false;
string said = G.__96(ref m_said);
if ( said.Length>0 && G.__112(ref evt.p3)!=G.__112(ref said) )
return false;
if ( evt.p4.Length>0 )
{
string sub = G.__96(ref m_sub);
if ( sub.Length>0 && G.__113(ref evt.p4)!=G.__113(ref sub) )
return false;
}
return true;
}
}
public class RoleBoxEventSelect : RoleBox
{
public const int ID = 0x507;
string m_scene;
string m_obj;
string m_sub;
string m_move;
string m_mode;
public override void __64(Asset asset)
{
base.__64(asset);
m_scene = asset.__18();
m_obj = asset.__18();
m_sub = asset.__18();
m_move = asset.__18();
m_mode = asset.__18();
}
public override bool __467(Event evt)
{
string scene = G.__96(ref m_scene);
if ( scene.Length>0 && G.m_game.__316(ref scene)==false )
return false;
string obj = G.__96(ref m_obj);
if ( obj.Length>0 && G.m_game.__319(ref evt.p1, ref obj)==false )
return false;
string move = G.__96(ref m_move);
if ( move.Length>0 && G.__150(ref evt.p2, ref move) )
return false;
string mode = G.__96(ref m_mode);
if ( G.m_game.m_lastEventByUser )
{
if ( mode=="CALL" )
return false;
}
else
{
if ( mode=="USER" )
return false;
}
string sub = G.__96(ref m_sub);
if ( sub.Length>0 && G.m_game.__321(ref sub)==false )
return false;
G.m_game.m_sysVarObj.m_value = evt.p1;
return true;
}
}
public class RoleBoxEventSong : RoleBox
{
public const int ID = 0x508;
string m_song;
public override void __64(Asset asset)
{
base.__64(asset);
m_song = asset.__18();
}
public override bool __467(Event evt)
{
string song = G.__96(ref m_song);
if ( song.Length>0 && G.__150(ref evt.p1, ref song) )
return false;
return true;
}
}
public class RoleBoxEventSpot : RoleBox
{
public const int ID = 0x511;
string m_scene;
string m_obj;
string m_spot;
public override void __64(Asset asset)
{
base.__64(asset);
m_scene = asset.__18();
m_obj = asset.__18();
m_spot = asset.__18();
}
public override bool __467(Event evt)
{
string scene = G.__96(ref m_scene);
if ( scene.Length>0 && G.m_game.__316(ref scene)==false )
return false;
string obj = G.__96(ref m_obj);
if ( obj.Length>0 && G.m_game.__319(ref evt.p1, ref obj)==false )
return false;
string spot = G.__96(ref m_spot);
if ( spot.Length>0 && G.__150(ref evt.p2, ref spot) )
return false;
G.m_game.m_sysVarObj.m_value = evt.p1;
return true;
}
}
public class RoleBoxEventSwitch : RoleBox
{
public const int ID = 0x509;
string m_player;
string m_player2;
public override void __64(Asset asset)
{
base.__64(asset);
m_player = asset.__18();
m_player2 = asset.__18();
}
public override bool __467(Event evt)
{
string player = G.__96(ref m_player);
if ( player.Length>0 && G.m_game.__318(ref evt.p1, ref player)==false )
return false;
string player2 = G.__96(ref m_player2);
if ( player2.Length>0 && G.m_game.__318(ref evt.p2, ref player2)==false )
return false;
G.m_game.m_sysVarPlayer.m_value = evt.p1;
G.m_game.m_sysVarPlayer2.m_value = evt.p2;
return true;
}
}
public class RoleBoxEventUse : RoleBox
{
public const int ID = 0x50A;
string m_obj;
string m_obj2;
string m_sub;
public override void __64(Asset asset)
{
base.__64(asset);
m_obj = asset.__18();
m_obj2 = asset.__18();
m_sub = asset.__18();
}
public override bool __467(Event evt)
{
bool bothFromInventory = evt.p3.Length>0;
string obj = G.__96(ref m_obj);
string obj2 = G.__96(ref m_obj2);
if ( obj.Length>0 )
{
if ( obj2.Length>0 )
{
bool equal = G.m_game.__319(ref evt.p1, ref obj) && G.m_game.__319(ref evt.p2, ref obj2);
if ( equal==false )
{
equal = G.m_game.__319(ref evt.p2, ref obj) && G.m_game.__319(ref evt.p1, ref obj2);
if ( equal==false )
return false;
}
}
else
{
if ( bothFromInventory )
{
if ( G.m_game.__319(ref evt.p1, ref obj)==false && G.m_game.__319(ref evt.p2, ref obj)==false )
return false;
}
else
{
if ( G.m_game.__319(ref evt.p1, ref obj)==false )
return false;
}
}
}
else
{
if ( obj2.Length>0 )
{
if ( bothFromInventory )
{
if ( G.m_game.__319(ref evt.p2, ref obj2)==false && G.m_game.__319(ref evt.p1, ref obj2)==false )
return false;
}
else
{
if ( G.m_game.__319(ref evt.p2, ref obj2)==false )
return false;
}
}
}
string sub = G.__96(ref m_sub);
if ( sub.Length>0 && G.m_game.__321(ref sub)==false )
return false;
G.m_game.m_sysVarObj.m_value = evt.p1;
G.m_game.m_sysVarObj2.m_value = evt.p2;
return true;
}
}
public class RoleBoxEventUseLabel : RoleBox
{
public const int ID = 0x50B;
string m_scene;
string m_obj;
string m_label;
public override void __64(Asset asset)
{
base.__64(asset);
m_scene = asset.__18();
m_obj = asset.__18();
m_label = asset.__18();
}
public override bool __467(Event evt)
{
string scene = G.__96(ref m_scene);
if ( scene.Length>0 && G.m_game.__316(ref scene)==false )
return false;
string obj = G.__96(ref m_obj);
if ( obj.Length>0 && G.m_game.__319(ref evt.p1, ref obj)==false )
return false;
string label = G.__96(ref m_label);
if ( label.Length>0 && G.m_game.__320(ref evt.p2, ref label)==false )
return false;
G.m_game.m_sysVarObj.m_value = evt.p1;
G.m_game.m_sysVarLabel.m_value = evt.p2;
return true;
}
}
public class RoleBoxEventWalk : RoleBox
{
public const int ID = 0x514;
string m_scene;
string m_obj;
string m_mode;
public override void __64(Asset asset)
{
base.__64(asset);
m_scene = asset.__18();
m_obj = asset.__18();
m_mode = asset.__18();
}
public override bool __467(Event evt)
{
string scene = G.__96(ref m_scene);
if ( scene.Length>0 && G.m_game.__316(ref scene)==false )
return false;
string obj = G.__96(ref m_obj);
if ( obj.Length>0 && G.m_game.__319(ref evt.p1, ref obj)==false )
return false;
string mode = G.__96(ref m_mode);
if ( mode.Length>0 && G.__150(ref evt.p2, ref mode) )
return false;
G.m_game.m_sysVarObj.m_value = evt.p1;
return true;
}
}
