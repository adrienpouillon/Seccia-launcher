using UnityEngine;
using System;
using System.Collections.Generic;
public class Scenario
{
uint m_crc32;
Box[] m_boxes;
List<Box> m_stack = new List<Box>();
List<Box> m_newStack = new List<Box>();
Dictionary<int, Box> m_boxesByID;
public string m_debugFirstScene = "";
public static implicit operator bool(Scenario inst) { return inst!=null; }
public void __65(Asset asset)
{
m_crc32 = asset.__16();
m_boxes = new Box[asset.__15()];
m_boxesByID = new Dictionary<int, Box>(m_boxes.Length);
for ( int iBox=0 ; iBox<m_boxes.Length ; iBox++ )
{
Box box = new Box();
m_boxes[iBox] = box;
box.m_type = (Box.TYPE)asset.__13();
box.m_id = asset.__15();
m_boxesByID.Add(box.m_id, box);
if ( box.m_type==Box.TYPE.SEQSTART || box.m_type==Box.TYPE.SEQEND )
box.m_seqRef = asset.__15();
switch ( box.m_type )
{
case Box.TYPE.PUZZLE:
case Box.TYPE.END:
box.m_scriptEnter = asset.__26();
break;
}
switch ( box.m_type )
{
case Box.TYPE.START:
case Box.TYPE.PUZZLE:
case Box.TYPE.SEQSTART:
case Box.TYPE.SEQEND:
case Box.TYPE.OR:
box.m_scriptExit = asset.__26();
break;
}
box.m_circuits = new Circuit[box.m_type==Box.TYPE.OR ? 3 : 1];
for ( int iCircuit=0 ; iCircuit<box.m_circuits.Length ; iCircuit++ )
{
Circuit circuit = new Circuit();
box.m_circuits[iCircuit] = circuit;
if ( iCircuit<2 )
{
circuit.m_idSources = new int[asset.__12()];
circuit.m_sourceOutputCircuits = new Box.CIRCUIT[circuit.m_idSources.Length];
circuit.m_sources = new Box[circuit.m_idSources.Length];
for ( int i=0 ; i<circuit.m_idSources.Length ; i++ )
{
circuit.m_idSources[i] = asset.__15();
circuit.m_sourceOutputCircuits[i] = (Box.CIRCUIT)asset.__12();
}
}
circuit.m_idTargets = new int[asset.__12()];
circuit.m_targetInputCircuits = new Box.CIRCUIT[circuit.m_idTargets.Length];
circuit.m_targetStates = new Box.STATE[circuit.m_idTargets.Length];
circuit.m_targets = new Box[circuit.m_idTargets.Length];
for ( int i=0 ; i<circuit.m_idTargets.Length ; i++ )
{
circuit.m_idTargets[i] = asset.__15();
circuit.m_targetInputCircuits[i] = (Box.CIRCUIT)asset.__12();
}
}
box.m_circuits[0].m_remainingAvailableSourceCount.Init(asset.__12());
if ( box.m_type==Box.TYPE.OR )
box.m_circuits[1].m_remainingAvailableSourceCount.Init(asset.__12());
if ( G.m_game.m_configDebug )
{
box.m_debugResolved = asset.__12()!=0;
box.m_debugLost = asset.__12()!=0;
}
}
for ( int i=0 ; i<m_boxes.Length ; i++ )
{
for ( int j=0 ; j<m_boxes[i].m_circuits.Length ; j++ )
m_boxes[i].m_circuits[j].Init();
}
Reset();
}
public void Reset()
{
for ( int i=0 ; i<m_boxes.Length ; i++ )
m_boxes[i].Reset();
m_boxes[0].m_enter = true;
m_boxes[0].Success();
m_stack.Clear();
m_stack.Add(m_boxes[0]);
m_newStack.Clear();
m_debugFirstScene = "";
}
public void __46(JsonObj json)
{
json.__382("crc", (int)m_crc32);
JsonArray jBoxes = json.__390("boxes");
for ( int iBox=0 ; iBox<m_boxes.Length ; iBox++ )
{
Box box = m_boxes[iBox];
JsonObj jBox = jBoxes.__389();
jBox.__382("id", box.m_id);
jBox.__382("sel", (int)box.m_selected);
jBox.__385("enter", box.m_enter);
jBox.__385("exit", box.m_exit);
jBox.__385("lost", box.m_lost);
jBox.__385("resolved", box.m_resolved);
JsonArray jCircuits = jBox.__390("circuits");
for ( int iCircuit=0 ; iCircuit<box.m_circuits.Length ; iCircuit++ )
{
Circuit circuit = box.m_circuits[iCircuit];
JsonObj jCircuit = jCircuits.__389();
jCircuit.__382("left", circuit.m_remainingAvailableSourceCount.cur);
JsonArray jStates = jCircuit.__390("states");
for ( int i=0 ; i<circuit.m_targetStates.Length ; i++ )
jStates.__382((int)circuit.m_targetStates[i]);
}
}
JsonArray jStack = json.__390("stack");
for ( int i=0 ; i<m_stack.Count ; i++ )
jStack.__382(m_stack[i].m_id);
}
public bool __47(JsonObj json)
{
uint crc = (uint)json.GetInt("crc");
if ( crc!=m_crc32 )
return false;
m_stack.Clear();
JsonArray jBoxes = json.__395("boxes");
if ( jBoxes )
{
for ( int iBox=0 ; iBox<jBoxes.__67() ; iBox++ )
{
JsonObj jBox = jBoxes.__394(iBox);
if ( jBox )
{
Box box = __497(jBox.GetInt("id"));
if ( box )
{
box.m_selected = (Box.CIRCUIT)jBox.GetInt("sel");
box.m_enter = jBox.__401("enter");
box.m_exit = jBox.__401("exit");
box.m_lost = jBox.__401("lost");
box.m_resolved = jBox.__401("resolved");
JsonArray jCircuits = jBox.__395("circuits");
if ( jCircuits )
{
for ( int iCircuit=0 ; iCircuit<box.m_circuits.Length && iCircuit<jCircuits.__67() ; iCircuit++ )
{
Circuit circuit = box.m_circuits[iCircuit];
JsonObj jCircuit = jCircuits.__394(iCircuit);
circuit.m_remainingAvailableSourceCount.Set(jCircuit.GetInt("left"));
JsonArray jStates = jCircuit.__395("states");
for ( int i=0 ; i<circuit.m_targetStates.Length && i<jStates.__67() ; i++ )
{
switch ( (Box.STATE)jStates.GetInt(i) )
{
case Box.STATE.PASSED:
circuit.m_targetStates[i] = Box.STATE.PASSED;
break;
case Box.STATE.MISSED:
circuit.m_targetStates[i] = Box.STATE.MISSED;
break;
}
}
}
}
}
}
}
}
JsonArray jStack = json.__395("stack");
if ( jStack )
{
for ( int i=0 ; i<jStack.__67() ; i++ )
{
Box box = __497(jStack.GetInt(i));
if ( box )
m_stack.Add(box);
}
}
return true;
}
public void __518()
{
for ( int i=0 ; i<m_boxes.Length ; i++ )
{
Box box = m_boxes[i];
if ( box.m_type!=Box.TYPE.OR )
continue;
if ( box.m_circuits[0].m_sources.Length!=0 && box.m_circuits[0].m_targets.Length!=0 )
continue;
if ( box.m_circuits[1].m_sources.Length!=0 && box.m_circuits[1].m_targets.Length!=0 )
continue;
if ( (box.m_circuits[0].m_sources.Length!=0 || box.m_circuits[1].m_sources.Length!=0) && box.m_circuits[2].m_targets.Length!=0 )
continue;
box.__531(Box.CIRCUIT.MAIN);
box.__531(Box.CIRCUIT.ALT);
box.__531(Box.CIRCUIT.EXIT);
box.__532(Box.CIRCUIT.MAIN, true);
box.__532(Box.CIRCUIT.ALT, true);
}
}
public Box __519()
{
return m_boxes[0];
}
public Box __497(int id)
{
Box box;
if ( m_boxesByID.TryGetValue(id, out box)==false )
return null;
return box;
}
public void __520(Box box)
{
if ( m_newStack.IndexOf(box)==-1 )
m_newStack.Add(box);
}
public void __42(SCENARIO state)
{
do
{
m_newStack.Clear();
for ( int i=0 ; i<m_stack.Count ; i++ )
{
Box box = m_stack[i];
__498(box);
if ( G.m_game.m_isGameOver )
return;
}
for ( int i=0 ; i<m_stack.Count ; i++ )
{
Box box = m_stack[i];
if ( box.m_exit || box.m_lost )
m_stack.RemoveAt(i--);
}
for ( int i=0 ; i<m_newStack.Count ; i++ )
{
Box box = m_newStack[i];
if ( box.m_lost==false )
m_stack.Add(box);
}
if ( m_newStack.Count==0 && state==SCENARIO.DEBUG_UPDATE )
{
for ( int i=0 ; i<m_stack.Count ; i++ )
{
Box box = m_stack[i];
if ( box.m_debugResolved && box.Success() )
{
m_newStack.Add(null);
break;
}
}
}
} while ( m_newStack.Count>0 );
}
public void __498(Box box)
{
if ( box.m_enter==false )
{
__509(box);
if ( box.m_enter==false )
return;
}
if ( box.m_exit==false )
__521(box);
}
public void __509(Box box)
{
if ( box.m_type!=Box.TYPE.OR )
{
for ( int i=0 ; i<box.m_circuits[0].m_sources.Length ; i++ )
{
if ( box.m_circuits[0].m_sources[i].m_exit==false )
return;
}
}
if ( box.m_scriptEnter )
box.m_scriptEnter.__690();
box.m_enter = true;
#if UNITY_STANDALONE_WIN
if ( G.m_game.m_configRun )
IDE.Post(IDE_MSG.SCENARIO_BOX_ENTER, 0, box.m_id);
#endif
if ( box.m_type!=Box.TYPE.PUZZLE )
box.Success();
}
public void __521(Box box)
{
if ( box.m_resolved==false )
return;
switch ( box.m_type )
{
case Box.TYPE.END:
{
break;
}
case Box.TYPE.OR:
{
bool sel = box.__530(box.m_selected);
bool exit = box.__530(Box.CIRCUIT.EXIT);
Box.CIRCUIT wrong = box.m_selected==Box.CIRCUIT.MAIN ? Box.CIRCUIT.ALT : Box.CIRCUIT.MAIN;
box.__531(wrong);
box.__532(wrong, true);
box.__532(box.m_selected);
if ( sel==false && exit==false )
{
#if UNITY_STANDALONE_WIN
if ( G.m_game.m_configRun )
IDE.Post(IDE_MSG.SCENARIO_BOX_MISSED, 0, box.m_id);
#endif
box.m_lost = true;
return;
}
break;
}
default:
{
if ( box.__530(Box.CIRCUIT.MAIN)==false )
{
#if UNITY_STANDALONE_WIN
if ( G.m_game.m_configRun )
IDE.Post(IDE_MSG.SCENARIO_BOX_MISSED, 0, box.m_id);
#endif
box.m_lost = true;
return;
}
break;
}
}
for ( int i=0 ; i<G.m_game.m_onSuccessSentences.Count ; i++ )
{
Sentence sentence = G.m_game.m_onSuccessSentences[i];
if ( sentence.m_onSuccess==box.m_id )
{
if ( sentence.m_dialog.__52(sentence.m_sid) )
sentence.m_visible.Set(true);
}
}
if ( box.m_scriptExit )
{
if ( box.m_type==Box.TYPE.OR )
G.m_game.m_sysVarOr.m_value = box.m_selected.ToString();
box.m_scriptExit.__690();
}
box.m_exit = true;
#if UNITY_STANDALONE_WIN
if ( G.m_game.m_configRun )
IDE.Post(IDE_MSG.SCENARIO_BOX_EXIT, 0, box.m_id);
#endif
if ( box.m_type==Box.TYPE.PUZZLE )
G.m_game.__323(RoleBoxEventPuzzle.ID, box.m_id.ToString());
if ( box.m_type==Box.TYPE.END )
{
G.m_game.m_layout.m_bagOpened = false;
G.m_game.__301("");
G.m_game.m_isGameOver = true;
G.m_game.m_menuGame.Open(MenuGame.ID_CREDITS, false, true);
}
}
public void Success(int id)
{
if ( id==0 )
return;
Box box = __497(id);
if ( box==null || box.m_type!=Box.TYPE.PUZZLE )
return;
if ( box.m_enter==false )
{
G.__198("success failed (box not started) #" + box.m_id);
return;
}
if ( box.m_lost )
return;
if ( box.m_resolved )
return;
if ( box.Success() )
{
G.m_game.__250("success ok #" + box.m_id);
}
}
public bool __522(int id)
{
Box box = __497(id);
if ( box && box.m_exit && box.m_type==Box.TYPE.PUZZLE )
return true;
return false;
}
public bool __523(int id)
{
Box box = __497(id);
if ( box && box.m_exit==false && box.m_type==Box.TYPE.PUZZLE )
return true;
return false;
}
public bool __524(int id)
{
Box box = __497(id);
if ( box && box.m_enter && box.m_exit==false && box.m_type==Box.TYPE.PUZZLE )
return true;
return false;
}
public bool __525(int id)
{
Box box = __497(id);
if ( box && box.m_exit && box.m_type==Box.TYPE.SEQSTART )
return true;
return false;
}
public bool __526(int id)
{
Box box = __497(id);
if ( box && box.m_exit==false && box.m_type==Box.TYPE.SEQSTART )
return true;
return false;
}
public bool __527(int id)
{
Box box = __497(id);
if ( box && box.m_exit && box.m_type==Box.TYPE.SEQEND )
return true;
return false;
}
public bool __528(int id)
{
Box box = __497(id);
if ( box==null || box.m_type!=Box.TYPE.SEQSTART || box.m_exit==false )
return false;
Box box2 = __497(box.m_seqRef);
if ( box2==null || box2.m_type!=Box.TYPE.SEQEND )
return true;
return !box2.m_exit;
}
}
public class Box
{
public enum TYPE { START=257, END=260, PUZZLE=256, SEQSTART=258, OR=261, SEQEND=262 }
public enum CIRCUIT { MAIN=0, ALT=1, EXIT=2, MISSED=128, UNDEF=255 }
public enum STATE { UNDEF, PASSED, MISSED }
public TYPE m_type;
public int m_id;
public int m_seqRef;
public Script m_scriptEnter;
public Script m_scriptExit;
public Circuit[] m_circuits;
public bool m_debugResolved = false;
public bool m_debugLost = false;
public bool m_resolved;
public bool m_enter;
public bool m_exit;
public bool m_lost;
public CIRCUIT m_selected;
public static implicit operator bool(Box inst) { return inst!=null; }
public void Reset()
{
m_resolved = false;
m_enter = false;
m_exit = false;
m_lost = false;
m_selected = m_type==TYPE.OR ? CIRCUIT.UNDEF : CIRCUIT.MAIN;
for ( int i=0 ; i<m_circuits.Length ; i++ )
m_circuits[i].Reset();
}
public bool Success()
{
if ( m_resolved )
return false;
if ( m_debugLost )
return false;
m_resolved = true;
if ( m_type==TYPE.PUZZLE && G.m_game.m_autoSavePuzzles )
G.m_game.m_autoSaveAsap = true;
return true;
}
public bool __529()
{
for ( int iCircuit=0 ; iCircuit<m_circuits.Length ; iCircuit++ )
{
Circuit circuit = m_circuits[iCircuit];
for ( int iTrg=0 ; iTrg<circuit.m_targets.Length ; iTrg++ )
{
Box trg = circuit.m_targets[iTrg];
if ( circuit.m_targetStates[iTrg]!=STATE.MISSED )
return false;
}
}
return true;
}
public bool __530(CIRCUIT id)
{
int count = 0;
Circuit circuit = m_circuits[(int)id];
for ( int iTrg=0 ; iTrg<circuit.m_targets.Length ; iTrg++ )
{
Box trg = circuit.m_targets[iTrg];
if ( trg.m_type==TYPE.OR )
{
if ( trg.m_selected!=CIRCUIT.UNDEF )
continue;
#if UNITY_STANDALONE_WIN
if ( G.m_game.m_configRun )
IDE.Post(IDE_MSG.SCENARIO_SIGNAL_PASSED, 0, m_id, (int)id, iTrg);
#endif
circuit.m_targetStates[iTrg] = STATE.PASSED;
G.m_game.m_scenario.__520(trg);
trg.m_selected = circuit.m_targetInputCircuits[iTrg];
}
else
{
#if UNITY_STANDALONE_WIN
if ( G.m_game.m_configRun )
IDE.Post(IDE_MSG.SCENARIO_SIGNAL_PASSED, 0, m_id, (int)id, iTrg);
#endif
circuit.m_targetStates[iTrg] = STATE.PASSED;
G.m_game.m_scenario.__520(trg);
}
count++;
}
return count>0;
}
public void __531(CIRCUIT output)
{
Circuit circuit = m_circuits[(int)output];
for ( int iTrg=0 ; iTrg<circuit.m_targets.Length ; iTrg++ )
{
if ( circuit.m_targetStates[iTrg]!=STATE.UNDEF )
continue;
#if UNITY_STANDALONE_WIN
if ( G.m_game.m_configRun )
IDE.Post(IDE_MSG.SCENARIO_SIGNAL_MISSED, 0, m_id, (int)output, iTrg);
#endif
circuit.m_targetStates[iTrg] = STATE.MISSED;
Box box = circuit.m_targets[iTrg];
CIRCUIT input = circuit.m_targetInputCircuits[iTrg];
box.m_circuits[(int)input].m_remainingAvailableSourceCount.cur--;
#if UNITY_EDITOR
Debug.Assert(box.m_circuits[(int)input].m_remainingAvailableSourceCount.cur>=0);
#endif
if ( box.m_type==TYPE.OR )
{
if ( box.m_selected!=CIRCUIT.UNDEF )
continue;
int leftMain = box.m_circuits[0].m_remainingAvailableSourceCount.cur;
int leftAlt = box.m_circuits[1].m_remainingAvailableSourceCount.cur;
if ( leftMain+leftAlt>0 )
{
if ( input==CIRCUIT.MAIN && leftMain==0 )
box.__531(CIRCUIT.MAIN);
else if ( input==CIRCUIT.ALT && leftAlt==0 )
box.__531(CIRCUIT.ALT);
continue;
}
#if UNITY_STANDALONE_WIN
if ( G.m_game.m_configRun )
IDE.Post(IDE_MSG.SCENARIO_BOX_MISSED, 0, box.m_id);
#endif
box.m_selected = CIRCUIT.MISSED;
box.m_lost = true;
box.__531(CIRCUIT.MAIN);
box.__531(CIRCUIT.ALT);
box.__531(CIRCUIT.EXIT);
}
else
{
if ( box.m_lost )
continue;
#if UNITY_STANDALONE_WIN
if ( G.m_game.m_configRun )
IDE.Post(IDE_MSG.SCENARIO_BOX_MISSED, 0, box.m_id);
#endif
box.m_lost = true;
box.__531(CIRCUIT.MAIN);
box.__532(CIRCUIT.MAIN, true);
}
}
}
public void __532(CIRCUIT input, bool forceMissed = false)
{
if ( (m_type==TYPE.OR && m_selected==CIRCUIT.UNDEF) || (m_type!=TYPE.OR && m_lost==false) )
{
if ( forceMissed || __529() )
{
#if UNITY_STANDALONE_WIN
if ( G.m_game.m_configRun )
IDE.Post(IDE_MSG.SCENARIO_BOX_MISSED, 0, m_id);
#endif
if ( m_type==TYPE.OR )
m_selected = CIRCUIT.MISSED;
m_lost = true;
}
}
Circuit circuit = m_circuits[(int)input];
for ( int iSrc=0 ; iSrc<circuit.m_sources.Length ; iSrc++ )
{
Box src = circuit.m_sources[iSrc];
CIRCUIT idCircuitSrc = circuit.m_sourceOutputCircuits[iSrc];
Circuit circuitSrc = src.m_circuits[(int)idCircuitSrc];
int count = 0;
for ( int iTrg=0 ; iTrg<circuitSrc.m_targets.Length ; iTrg++ )
{
Box trg = circuitSrc.m_targets[iTrg];
if ( trg!=this || circuitSrc.m_targetStates[iTrg]!=STATE.UNDEF )
continue;
#if UNITY_STANDALONE_WIN
if ( G.m_game.m_configRun )
IDE.Post(IDE_MSG.SCENARIO_SIGNAL_MISSED, 0, src.m_id, (int)idCircuitSrc, iTrg);
#endif
circuitSrc.m_targetStates[iTrg] = STATE.MISSED;
count++;
}
if ( count>0 )
{
if ( src.m_type==TYPE.OR )
{
bool force = forceMissed || src.m_selected!=CIRCUIT.UNDEF;
if ( force || src.m_circuits[0].__529() )
src.__532(CIRCUIT.MAIN, forceMissed);
if ( force || src.m_circuits[1].__529() )
src.__532(CIRCUIT.ALT, forceMissed);
}
else
{
if ( src.m_circuits[0].__529() )
src.__532(CIRCUIT.MAIN, forceMissed);
}
}
}
if ( forceMissed && m_type==TYPE.OR )
{
__531(CIRCUIT.MAIN);
__531(CIRCUIT.ALT);
__531(CIRCUIT.EXIT);
}
}
}
public class Circuit
{
public int[] m_idSources;
public int[] m_idTargets;
public Box[] m_sources;
public Box.CIRCUIT[] m_sourceOutputCircuits;
public Serial<int> m_remainingAvailableSourceCount;
public Box[] m_targets;
public Box.CIRCUIT[] m_targetInputCircuits;
public Box.STATE[] m_targetStates;
public void Init()
{
if ( m_sources!=null )
{
for ( int i=0 ; i<m_sources.Length ; i++ )
m_sources[i] = G.m_game.m_scenario.__497(m_idSources[i]);
m_idSources = null;
}
for ( int i=0 ; i<m_targets.Length ; i++ )
m_targets[i] = G.m_game.m_scenario.__497(m_idTargets[i]);
m_idTargets = null;
}
public void Reset()
{
m_remainingAvailableSourceCount.Reset();
for ( int i=0 ; i<m_targetStates.Length ; i++ )
m_targetStates[i] = Box.STATE.UNDEF;
}
public bool __529()
{
int undefCount = 0;
for ( int i=0 ; i<m_targetStates.Length ; i++ )
{
if ( m_targetStates[i]==Box.STATE.PASSED )
return true;
if ( m_targetStates[i]==Box.STATE.UNDEF )
undefCount++;
}
return undefCount==0;
}
}
