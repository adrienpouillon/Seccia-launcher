using UnityEngine;
using System;
using System.Collections.Generic;
public class Script
{
public Instruction[] m_instructions;
public bool m_enterNotified;
public bool m_exitNotified;
public List<short> m_customOuts;
public static implicit operator bool(Script inst) { return inst!=null; }
public void __64(Asset asset, int count)
{
int paramCount;
m_instructions = new Instruction[count];
for ( int i=0 ; i<count ; i++ )
{
m_instructions[i] = new Instruction();
m_instructions[i].m_script = this;
m_instructions[i].m_firstGoto = asset.__13();
m_instructions[i].m_secondGoto = asset.__13();
m_instructions[i].m_command = (Instruction.COMMAND)asset.__12();
m_instructions[i].m_commandParam = asset.__18();
m_instructions[i].m_function = asset.__14();
paramCount = asset.__12();
m_instructions[i].m_paramCount = paramCount;
m_instructions[i].m_params = new string[paramCount];
for ( int j=0 ; j<paramCount ; j++ )
m_instructions[i].m_params[j] = asset.__18();
}
}
public int __684(bool enter = false, bool exit = false)
{
m_enterNotified = enter;
m_exitNotified = exit;
if ( m_customOuts!=null && m_customOuts.Count>0 )
m_customOuts.Clear();
const int maxCount = 10000000;
int count = 0;
int index = 0;
while ( index<m_instructions.Length && count<maxCount )
{
count++;
Instruction instruction = m_instructions[index];
switch ( instruction.m_command )
{
case Instruction.COMMAND.IF:
case Instruction.COMMAND.ELSEIF:
case Instruction.COMMAND.WHILE:
index = instruction.__684() ? instruction.m_firstGoto : instruction.m_secondGoto;
break;
case Instruction.COMMAND.IFNOT:
case Instruction.COMMAND.ELSEIFNOT:
case Instruction.COMMAND.WHILENOT:
index = instruction.__684() ? instruction.m_secondGoto : instruction.m_firstGoto;
break;
case Instruction.COMMAND.ELSE:
case Instruction.COMMAND.END:
index = instruction.m_secondGoto;
break;
case Instruction.COMMAND.RETURN:
if ( instruction.m_function==0 )
{
if ( instruction.m_commandParam.Length==0 )
return 0;
return G.__113(G.__96(ref instruction.m_commandParam));
}
else
{
return instruction.__684() ? 1 : 0;
}
case Instruction.COMMAND.BREAK:
case Instruction.COMMAND.CONTINUE:
case Instruction.COMMAND.GOTO:
index = instruction.m_firstGoto;
break;
default:
instruction.__684();
index++;
break;
}
}
return 0;
}
public bool __685()
{
if ( G.m_game.m_configDebug==false )
return true;
return G.m_game.m_started;
}
}
public class Instruction
{
public enum COMMAND { BREAK, CONTINUE, ELSE, ELSEIF, ELSEIFNOT, END, IF, IFNOT, RETURN, WHILE, WHILENOT, GOTO }
public Script m_script;
public int m_firstGoto;
public int m_secondGoto;
public COMMAND m_command;
public string m_commandParam;
public int m_function;
public string[] m_params;
public int m_paramCount;
public bool __684()
{
switch ( m_function )
{
case 001: return __700();
case 002: return __976();
case 003: return __952();
case 004: return __698();
case 005: return __971();
case 006: return __789();
case 007: return __790();
case 008: return __958();
case 009: return __946();
case 010: return __728();
case 011: return __729();
case 012: return __773();
case 013: return __979();
case 014: return __981();
case 015: return __716();
case 016: return __786();
case 017: return __887();
case 018: return __706();
case 019: return __959();
case 020: return __801();
case 021: return __896();
case 022: return __787();
case 023: return __717();
case 024: return __978();
case 025: return __793();
case 026: return __844();
case 027: return __876();
case 028: return __821();
case 029: return __829();
case 030: return __912();
case 031: return __846();
case 032: return __914();
case 033: return __703();
case 034: return __785();
case 035: return __908();
case 036: return __909();
case 037: return __950();
case 038: return __902();
case 039: return __843();
case 040: return __875();
case 041: return __820();
case 042: return __828();
case 043: return __911();
case 044: return __845();
case 045: return __913();
case 046: return __760();
case 047: return __765();
case 048: return __721();
case 049: return __766();
case 050: return __767();
case 051: return __708();
case 052: return __712();
case 053: return __711();
case 054: return __726();
case 055: return __713();
case 056: return __710();
case 057: return __791();
case 058: return __714();
case 059: return __709();
case 060: return __915();
case 061: return __916();
case 062: return __868();
case 063: return __957();
case 064: return __928();
case 065: return __955();
case 066: return __818();
case 067: return __872();
case 068: return __894();
case 069: return __892();
case 070: return __861();
case 071: return __812();
case 072: return __858();
case 073: return __954();
case 074: return __874();
case 075: return __724();
case 076: return __725();
case 077: return __910();
case 078: return __848();
case 079: return __937();
case 080: return __832();
case 081: return __889();
case 082: return __931();
case 083: return __825();
case 084: return __808();
case 085: return __878();
case 086: return __939();
case 087: return __815();
case 088: return __943();
case 089: return __774();
case 090: return __980();
case 091: return __972();
case 092: return __804();
case 093: return __805();
case 094: return __940();
case 095: return __926();
case 096: return __836();
case 097: return __819();
case 098: return __719();
case 099: return __707();
case 100: return __722();
case 101: return __723();
case 102: return __734();
case 103: return __737();
case 104: return __730();
case 105: return __731();
case 106: return __735();
case 107: return __739();
case 108: return __736();
case 109: return __741();
case 110: return __738();
case 111: return __732();
case 112: return __733();
case 113: return __740();
case 114: return __962();
case 115: return __964();
case 116: return __966();
case 117: return __921();
case 118: return __934();
case 119: return __924();
case 120: return __944();
case 121: return __932();
case 122: return __923();
case 123: return __977();
case 124: return __794();
case 125: return __795();
case 126: return __743();
case 127: return __749();
case 128: return __745();
case 129: return __742();
case 130: return __746();
case 131: return __748();
case 132: return __751();
case 133: return __752();
case 134: return __750();
case 135: return __747();
case 136: return __753();
case 137: return __744();
case 138: return __704();
case 139: return __927();
case 140: return __778();
case 141: return __779();
case 142: return __781();
case 143: return __780();
case 144: return __702();
case 145: return __965();
case 146: return __803();
case 147: return __970();
case 148: return __699();
case 149: return __764();
case 150: return __799();
case 151: return __917();
case 152: return __900();
case 153: return __759();
case 154: return __782();
case 155: return __784();
case 156: return __783();
case 157: return __705();
case 158: return __777();
case 159: return __969();
case 160: return __720();
case 161: return __961();
case 162: return __906();
case 163: return __827();
case 164: return __833();
case 165: return __834();
case 166: return __755();
case 167: return __756();
case 168: return __800();
case 169: return __835();
case 170: return __796();
case 171: return __715();
case 172: return __886();
case 173: return __788();
case 174: return __963();
case 175: return __967();
case 176: return __968();
case 177: return __802();
case 178: return __895();
case 179: return __696();
case 180: return __806();
case 181: return __840();
case 182: return __867();
case 183: return __956();
case 184: return __948();
case 185: return __830();
case 186: return __769();
case 187: return __776();
case 188: return __837();
case 189: return __929();
case 190: return __851();
case 191: return __852();
case 192: return __854();
case 193: return __853();
case 194: return __953();
case 195: return __863();
case 196: return __772();
case 197: return __855();
case 198: return __864();
case 199: return __839();
case 200: return __823();
case 201: return __816();
case 202: return __817();
case 203: return __826();
case 204: return __822();
case 205: return __809();
case 206: return __879();
case 207: return __881();
case 208: return __880();
case 209: return __882();
case 210: return __883();
case 211: return __866();
case 212: return __850();
case 213: return __865();
case 214: return __869();
case 215: return __856();
case 216: return __899();
case 217: return __898();
case 218: return __901();
case 219: return __871();
case 220: return __810();
case 221: return __891();
case 222: return __893();
case 223: return __770();
case 224: return __920();
case 225: return __841();
case 226: return __860();
case 227: return __811();
case 228: return __857();
case 229: return __949();
case 230: return __842();
case 231: return __838();
case 232: return __890();
case 233: return __771();
case 234: return __870();
case 235: return __873();
case 236: return __847();
case 237: return __936();
case 238: return __831();
case 239: return __888();
case 240: return __941();
case 241: return __930();
case 242: return __824();
case 243: return __807();
case 244: return __877();
case 245: return __938();
case 246: return __814();
case 247: return __942();
case 248: return __884();
case 249: return __907();
case 250: return __905();
case 251: return __919();
case 252: return __797();
case 253: return __762();
case 254: return __758();
case 255: return __862();
case 256: return __813();
case 257: return __775();
case 258: return __798();
case 259: return __763();
case 260: return __918();
case 261: return __701();
case 262: return __718();
case 263: return __754();
case 264: return __897();
case 265: return __951();
case 266: return __859();
case 267: return __904();
case 268: return __885();
case 269: return __768();
case 270: return __947();
case 271: return __903();
case 272: return __792();
case 273: return __727();
case 274: return __922();
case 275: return __935();
case 276: return __925();
case 277: return __945();
case 278: return __933();
case 279: return __757();
case 280: return __761();
case 281: return __697();
case 282: return __975();
case 283: return __849();
case 284: return __973();
case 285: return __974();
case 286: return __960();
}
return true;
}
public string __686(int index)
{
if ( index<0 || index>=m_paramCount )
return "";
string item = m_params[index];
return G.__96(ref item);
}
public bool __687(int index)
{
return G.__112(__686(index));
}
public int __688(int index)
{
return G.__113(__686(index));
}
public float __689(int index)
{
return G.__114(__686(index));
}
public float __690(int index)
{
return G.__113(__686(index))/1000.0f;
}
public Variable __691(int index)
{
string name = __686(index);
return G.m_game.__288(ref name);
}
public string __692(int index)
{
if ( index<0 || index>=m_paramCount )
return "";
string item = m_params[index];
item = G.__96(ref item);
if ( item.Length==0 || item[0]!='@' )
return "";
return item.Substring(1);
}
public Scene __291(int index)
{
string uid = __686(index);
if ( uid.Length==0 )
return G.m_game.__291();
return G.m_game.__274(uid);
}
public SceneObj __693(int index)
{
Scene scene = null;
SceneObj sceneObj = null;
if ( __695(__686(index), ref scene, ref sceneObj)==false )
return null;
return sceneObj;
}
public Dialog __694(int index)
{
string uid = __686(index);
if ( uid.Length==0 )
return null;
return G.m_game.__278(uid);
}
public bool __695(string uid, ref Scene scene, ref SceneObj sceneObj)
{
string uidScene = "";
string uidObj = "";
G.__97(uid, ref uidScene, ref uidObj);
scene = uidScene.Length==0 ? G.m_game.__291() : G.m_game.__274(uidScene);
if ( scene==null )
return false;
sceneObj = scene.__277(uidObj);
if ( sceneObj==null )
return false;
return true;
}
public bool __696()
{
Variable var = __691(0);
if ( var==null )
return false;
string value = __686(1);
for ( int i=2 ; i<m_paramCount ; i++ )
value += __686(i);
var.m_value = value;
return true;
}
public bool __697()
{
Variable var = __691(0);
if ( var==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( __686(i)==var.m_value )
return true;
}
return false;
}
public bool __698()
{
if ( m_paramCount<1 )
return false;
for ( int i=0 ; i<m_paramCount && i<6 ; i+=2 )
{
Variable var = __691(i);
if ( var==null )
return false;
if ( __686(i+1)!=var.m_value )
return false;
}
return true;
}
public bool __699()
{
if ( m_paramCount<1 )
return false;
for ( int i=0 ; i<m_paramCount && i<6 ; i+=2 )
{
Variable var = __691(i);
if ( var==null )
continue;
if ( __686(i+1)==var.m_value )
return true;
}
return false;
}
public bool __700()
{
Variable var = __691(0);
if ( var==null )
return false;
var.m_value = (G.__113(ref var.m_value) + __688(1)).ToString();
return true;
}
public bool __701()
{
Variable var = __691(0);
if ( var==null )
return false;
var.m_value = (G.__113(ref var.m_value) - __688(1)).ToString();
return true;
}
public bool __702()
{
Variable var = __691(0);
if ( var==null )
return false;
var.m_value = (G.__113(ref var.m_value) * __688(1)).ToString();
return true;
}
public bool __703()
{
Variable var = __691(0);
if ( var==null )
return false;
int divider = __688(1);
var.m_value = divider==0 ? "0" : (G.__113(ref var.m_value)/divider).ToString();
return true;
}
public bool __704()
{
Variable var = __691(0);
if ( var==null )
return false;
int divider = __688(1);
if ( divider<=0 )
var.m_value = "0";
else
{
int value = G.__113(ref var.m_value) % divider;
var.m_value = value.ToString();
}
return true;
}
public bool __705()
{
Variable var = __691(0);
if ( var==null )
return false;
int power = __688(1);
int result = (int)Mathf.Pow((float)G.__113(var.m_value), (float)power);
var.m_value = result.ToString();
return true;
}
public bool __706()
{
Variable var = __691(0);
if ( var==null )
return false;
int val = G.__113(var.m_value);
int min = __688(1);
int max = __688(2);
if ( val<min )
var.m_value = min.ToString();
else if ( val>max )
var.m_value = max.ToString();
return true;
}
public bool __707()
{
Variable var = __691(0);
if ( var==null )
return false;
float ratio = G.Clamp(__689(1));
float min = (float)__688(2);
float max = (float)__688(3);
string ease = __686(4);
bool easeIn = G.__148(ref ease, "IN");
bool easeOut = G.__148(ref ease, "OUT");
float result = G.__138(ratio, min, max, easeIn, easeOut);
var.m_value = ((int)result).ToString();
return true;
}
public bool __708()
{
Variable var = __691(0);
if ( var==null )
return false;
float res = G.__114(ref var.m_value) + __689(1);
G.__111(ref var.m_value, res);
return true;
}
public bool __709()
{
Variable var = __691(0);
if ( var==null )
return false;
float res = G.__114(ref var.m_value) - __689(1);
G.__111(ref var.m_value, res);
return true;
}
public bool __710()
{
Variable var = __691(0);
if ( var==null )
return false;
float res = G.__114(ref var.m_value) * __689(1);
G.__111(ref var.m_value, res);
return true;
}
public bool __711()
{
Variable var = __691(0);
if ( var==null )
return false;
float divider = __689(1);
if ( G.__129(divider) )
var.m_value = "0";
else
{
float res = G.__114(ref var.m_value)/divider;
G.__111(ref var.m_value, res);
}
return true;
}
public bool __712()
{
Variable var = __691(0);
if ( var==null )
return false;
float val = G.__114(var.m_value);
float min = m_paramCount<2 ? 0.0f : __689(1);
float max = m_paramCount<3 ? 1.0f : __689(2);
if ( val<min )
G.__111(ref var.m_value, min);
else if ( val>max )
G.__111(ref var.m_value, max);
return true;
}
public bool __713()
{
Variable var = __691(0);
if ( var==null )
return false;
float ratio = G.Clamp(__689(1));
float min = m_paramCount<3 ? 0.0f : __689(2);
float max = m_paramCount<4 ? 1.0f : __689(3);
string ease = __686(4);
bool easeIn = G.__148(ref ease, "IN");
bool easeOut = G.__148(ref ease, "OUT");
float result = G.__138(ratio, min, max, easeIn, easeOut);
G.__111(ref var.m_value, result);
return true;
}
public bool __714()
{
Variable var = __691(0);
if ( var==null )
return false;
float result = Mathf.Sqrt(G.__114(var.m_value));
G.__111(ref var.m_value, result);
return true;
}
public bool __715()
{
Variable var = __691(0);
if ( var==null )
return false;
var.m_value = Mathf.RoundToInt(G.__114(ref var.m_value)).ToString();
return true;
}
public bool __716()
{
Variable var = __691(0);
if ( var==null )
return false;
var.m_value += __686(1);
return true;
}
public bool __717()
{
Variable var = __691(0);
if ( var==null )
return false;
if ( var.m_value.Length==0 )
return false;
var.m_value = var.m_value.Remove(var.m_value.Length-1, 1);
return true;
}
public bool __718()
{
Variable var = __691(0);
if ( var==null )
return false;
int index = __688(1);
if ( index<0 )
index = 0;
else if ( index>var.m_value.Length )
index = var.m_value.Length;
int count = m_paramCount<3 ? -1 : __688(2);
if ( count==0 )
var.m_value = "";
else if ( count<0 || index+count>var.m_value.Length )
var.m_value = var.m_value.Substring(index);
else
var.m_value = var.m_value.Substring(index, count);
return true;
}
public bool __719()
{
Variable var = __691(0);
if ( var==null )
return false;
var.m_value = __686(1).Length.ToString();
return true;
}
public bool __720()
{
Variable var = __691(0);
if ( var==null )
return false;
var.m_value = G.__156(__688(1), __688(2)).ToString();
return true;
}
public bool __721()
{
string value = __686(0);
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( __686(i)==value )
return true;
}
return false;
}
public bool __722()
{
int value = __688(0);
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( value<__688(i) )
return true;
}
return false;
}
public bool __723()
{
int value = __688(0);
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( value<=__688(i) )
return true;
}
return false;
}
public bool __724()
{
int value = __688(0);
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( value>__688(i) )
return true;
}
return false;
}
public bool __725()
{
int value = __688(0);
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( value>=__688(i) )
return true;
}
return false;
}
public bool __726()
{
Variable varIndex = __691(0);
if ( varIndex==null )
return false;
int col = __688(1);
int row = __688(2);
int width = __688(3);
varIndex.m_value = (row*width+col).ToString();
return true;
}
public bool __727()
{
Variable varCol = __691(0);
Variable varRow = __691(1);
if ( varCol==null || varRow==null )
return false;
int index = __688(2);
int width = __688(3);
if ( width<1 )
return false;
varRow.m_value = (index/width).ToString();
varCol.m_value = (index%width).ToString();
return true;
}
public bool __728()
{
Variable var = __691(0);
if ( var==null )
return false;
var.m_value = (G.__113(ref var.m_value) | __688(1)).ToString();
return true;
}
public bool __729()
{
Variable var = __691(0);
if ( var==null )
return false;
int a = G.__113(ref var.m_value);
int b = __688(1);
a ^= a & b;
var.m_value = a.ToString();
return true;
}
public bool __730()
{
Variable var = __691(0);
if ( var==null )
return false;
if ( var.m_list==null )
var.m_list = new List<string>();
else
var.m_list.Clear();
return true;
}
public bool __731()
{
Variable varCount = __691(1);
if ( varCount==null )
return false;
Variable varList = __691(0);
if ( varList==null )
{
varList.m_value = "0";
return false;
}
int count = varList.m_list==null ? 0 : varList.m_list.Count;
varCount.m_value = count.ToString();
return true;
}
public bool __732()
{
Variable var = __691(0);
if ( var==null || var.m_list==null )
return false;
List<string> list = var.m_list;
var.m_list = new List<string>();
while ( list.Count>0 )
{
int index = G.__156(list.Count);
var.m_list.Add(list[index]);
list.RemoveAt(index);
}
return true;
}
public bool __733()
{
Variable var = __691(0);
if ( var==null )
return false;
if ( var.m_list==null )
var.m_list = new List<string>();
int index = m_paramCount<3 ? -1 : __688(2);
if ( index>=var.m_list.Count )
return false;
if ( index<0 )
{
string value = __686(1);
for ( int i=0 ; i<var.m_list.Count ; i++ )
var.m_list[i] = value;
return true;
}
var.m_list[index] = __686(1);
return true;
}
public bool __734()
{
Variable var = __691(0);
if ( var==null )
return false;
if ( var.m_list==null )
var.m_list = new List<string>();
int index = m_paramCount<3 ? -1 : __688(2);
if ( index<0 || index>=var.m_list.Count )
{
var.m_list.Add(__686(1));
return true;
}
var.m_list.Insert(index, __686(1));
return true;
}
public bool __735()
{
Variable var = __691(0);
if ( var==null || var.m_list==null )
return false;
int index = __688(1);
if ( index<0 || index>=var.m_list.Count )
return false;
var.m_list.RemoveAt(index);
return true;
}
public bool __736()
{
Variable list = __691(0);
Variable var = __691(2);
if ( var==null )
return false;
if ( list==null )
{
var.m_value = "";
return false;
}
int index = __688(1);
if ( list.m_list==null || index<0 || index>=list.m_list.Count )
{
var.m_value = "";
return false;
}
var.m_value = list.m_list[index];
return true;
}
public bool __737()
{
if ( m_paramCount<2 )
return false;
Variable list = __691(0);
if ( list==null || list.m_list==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
string value = __686(i);
for ( int j=0 ; j<list.m_list.Count ; j++ )
{
if ( list.m_list[j]!=value )
return false;
}
}
return true;
}
public bool __738()
{
Variable list = __691(0);
if ( list==null || list.m_list==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
string value = __686(i);
for ( int j=0 ; j<list.m_list.Count ; j++ )
{
if ( list.m_list[j]==value )
return true;
}
}
return false;
}
public bool __739()
{
Variable list = __691(0);
Variable res = __691(2);
if ( res==null )
return false;
if ( list==null || list.m_list==null )
{
res.m_value = "-1";
return false;
}
string value = __686(1);
for ( int i=0 ; i<list.m_list.Count ; i++ )
{
if ( list.m_list[i]==value )
{
res.m_value = i.ToString();
return true;
}
}
res.m_value = "-1";
return false;
}
public bool __740()
{
Variable list = __691(0);
if ( list==null || list.m_list==null )
return false;
list.m_list.Sort();
return false;
}
public bool __741()
{
Variable varRes = __691(1);
if ( varRes==null )
return false;
varRes.m_value = "";
Variable varList = __691(0);
if ( varList==null || varList.m_list==null )
return false;
string sep = __686(2);
if ( sep=="" )
sep = ",";
for ( int i=0 ; i<varList.m_list.Count ; i++ )
{
if ( i>0 )
varRes.m_value += ",";
varRes.m_value += varList.m_list[i];
}
return true;
}
public bool __742()
{
Variable var = __691(1);
if ( var==null )
return false;
float angle = G.__142(__688(0));
var.m_value = Mathf.Cos(angle).ToString();
return true;
}
public bool __743()
{
Variable var = __691(1);
if ( var==null )
return false;
int angle = G.__142(Mathf.Acos(__689(0)));
var.m_value = angle.ToString();
return true;
}
public bool __744()
{
Variable var = __691(1);
if ( var==null )
return false;
float angle = G.__142(__688(0));
var.m_value = Mathf.Sin(angle).ToString();
return true;
}
public bool __745()
{
Variable var = __691(1);
if ( var==null )
return false;
int angle = G.__142(Mathf.Asin(__689(0)));
var.m_value = angle.ToString();
return true;
}
public bool __746()
{
Variable var = __691(4);
if ( var==null )
return false;
int x = __688(0)-__688(2);
int y = __688(1)-__688(3);
int dist = Mathf.RoundToInt(Mathf.Sqrt(x*x + y*y));
var.m_value = dist.ToString();
return true;
}
public bool __747()
{
Variable varX = __691(5);
Variable varY = __691(6);
if ( varX==null || varY==null )
return false;
float ratio = G.Clamp(__689(0));
int ax = __688(1);
int ay = __688(2);
int bx = __688(3);
int by = __688(4);
if ( ratio==0.0f )
{
varX.m_value = ax.ToString();
varY.m_value = ay.ToString();
return true;
}
if ( ratio==1.0f )
{
varX.m_value = bx.ToString();
varY.m_value = by.ToString();
return true;
}
Vec2 dir;
dir.x = (float)(bx-ax);
dir.y = (float)(by-ay);
float len = dir.__433();
if ( dir.__434(len)==false )
{
varX.m_value = ax.ToString();
varY.m_value = ay.ToString();
return true;
}
len *= ratio;
varX.m_value = (ax + (int)(dir.x*len)).ToString();
varY.m_value = (ay + (int)(dir.y*len)).ToString();
return true;
}
public bool __748()
{
Variable varX = __691(4);
Variable varY = __691(5);
if ( varX==null || varY==null )
return false;
int x = __688(0);
int y = __688(1);
int angle = __688(2);
int dist = __688(3);
float angle2 = G.__142(angle);
varX.m_value = Mathf.RoundToInt(x+Mathf.Sin(angle2)*dist).ToString();
varY.m_value = Mathf.RoundToInt(y+Mathf.Cos(angle2)*dist).ToString();
return true;
}
public bool __749()
{
Variable var = __691(4);
if ( var==null )
return false;
int ax = __688(0);
int ay = __688(1);
int bx = __688(2);
int by = __688(3);
float angle = G.__141(ax, ay, bx, by);
var.m_value = G.__142(angle).ToString();
return true;
}
public bool __750()
{
int px = __688(0);
int py = __688(1);
int left = __688(2);
int top = __688(3);
int right = left + __688(4);
int bottom = top + __688(5);
return px>=left && px<right && py>=top && py<bottom;
}
public bool __751()
{
Variable var = __691(0);
if ( var==null )
return false;
int angle = G.__113(var.m_value);
int angleNorm = G.__144(angle, __687(1));
if ( angle!=angleNorm )
var.m_value = angleNorm.ToString();
return true;
}
public bool __752()
{
int x = __688(0) - __688(2);
int y = __688(1) - __688(3);
int radius = __688(4);
int value = x*x + y*y - radius*radius;
if ( value==0 )
return true;
return value<0;
}
public bool __753()
{
int l1 = __688(0);
int t1 = __688(1);
int r1 = l1 + __688(2);
int b1 = t1 + __688(3);
int l2 = __688(4);
int t2 = __688(5);
int r2 = l2 + __688(6);
int b2 = t2 + __688(7);
if ( l1<r2 && r1>l2 && t1<b2 && b1>t2 )
return true;
return false;
}
public bool __754()
{
if ( G.m_game.m_menuScene )
return false;
G.Success(__688(0));
return true;
}
public bool __755()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.m_game.m_scenario.__521(__688(i)) )
return true;
}
return false;
}
public bool __756()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.m_game.m_scenario.__523(__688(i)) )
return true;
}
return false;
}
public bool __757()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.m_game.m_scenario.__522(__688(i)) )
return true;
}
return false;
}
public bool __758()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.m_game.m_scenario.__524(__688(i)) )
return true;
}
return false;
}
public bool __759()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.m_game.m_scenario.__527(__688(i)) )
return true;
}
return false;
}
public bool __760()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.m_game.m_scenario.__526(__688(i)) )
return true;
}
return false;
}
public bool __761()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.m_game.m_scenario.__525(__688(i)) )
return true;
}
return false;
}
public bool __762()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
Role role = G.m_game.__283(__686(i));
if ( role )
role.Start();
}
return true;
}
public bool __763()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
Role role = G.m_game.__283(__686(i));
if ( role )
role.Stop();
}
return true;
}
public bool __764()
{
if ( m_script.m_customOuts==null )
m_script.m_customOuts = new List<short>();
if ( m_paramCount==0 )
{
if ( m_script.m_customOuts.Contains(0)==false )
m_script.m_customOuts.Add(0);
}
else
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
short value = (short)__688(i);
if ( m_script.m_customOuts.Contains(value)==false )
m_script.m_customOuts.Add(value);
}
}
return true;
}
public bool __765()
{
return m_script.m_enterNotified;
}
public bool __766()
{
return m_script.m_enterNotified==false && m_script.m_exitNotified==false;
}
public bool __767()
{
return m_script.m_exitNotified;
}
public bool __768()
{
if ( m_script.__685()==false )
return false;
return G.m_game.Task(__686(0), __686(1), __686(2));
}
public bool __769()
{
G.m_game.m_brightness = G.Clamp(__688(0), -100, 100)/100.0f;
return true;
}
public bool __770()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
string p0 = __686(0);
string p1 = __686(1);
string p2 = __686(2);
string p3 = __686(3);
scene.m_shakes[0] = G.__113(ref p0)*0.5f;
scene.m_shakes[1] = p1.Length==0 ? 1.0f : G.__113(ref p1)/100.0f;
scene.m_shakes[2] = p2.Length==0 ? scene.m_shakes[0] : G.__113(ref p2)*0.5f;
scene.m_shakes[3] = p3.Length==0 ? scene.m_shakes[1] : G.__113(ref p3)/100.0f;
return true;
}
public bool __771()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
string p0 = __686(0);
string p1 = __686(1);
string p2 = __686(2);
string p3 = __686(3);
scene.m_waves[0] = G.__113(ref p0)*0.5f;
scene.m_waves[1] = p1.Length==0 ? 1.0f : G.__113(ref p1)/100.0f;
scene.m_waves[2] = G.__113(ref p2)*0.5f;
scene.m_waves[3] = p3.Length==0 ? 1.0f : G.__113(ref p3)/100.0f;
return true;
}
public bool __772()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
if ( __688(0)==0 )
{
scene.__569(CAMERA.CURSOR);
}
else
{
float renderScale = scene.m_renderScale;
Cam cam = scene.__568(CAMERA.CURSOR);
cam.m_scale = renderScale;
}
return true;
}
public bool __773()
{
return G.m_game.m_lastEventByUser;
}
public bool __774()
{
string valueObj = "";
string valueSub = "";
string valueLabel = "";
bool found = false;
if ( G.m_game.m_cursor )
{
Scene scene = G.m_game.__291();
if ( scene )
{
SceneEntity entity;
SubObj sub;
scene.__425(out entity, out sub, G.m_game.m_cursorViewX, G.m_game.m_cursorViewY, true, DRAG.ANY);
if ( entity )
{
found = true;
if ( entity.__602() )
{
valueObj = ((SceneObj)entity).m_obj.m_uid;
if ( sub )
valueSub = sub.m_name;
}
else
{
valueLabel = ((SceneLabel)entity).m_name;
}
}
}
}
Variable var = __691(0);
if ( var )
var.m_value = valueObj;
var = __691(1);
if ( var )
var.m_value = valueSub;
var = __691(2);
if ( var )
var.m_value = valueLabel;
return found;
}
public bool __775()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
for ( int i=0 ; i<m_paramCount ; i++ )
{
SceneObj sceneObj = scene.__277(__686(i));
if ( sceneObj )
sceneObj.__645();
}
return true;
}
public bool __776()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__686(0));
SceneObj sceneObjTrg = scene.__277(__686(1));
if ( sceneObj==null )
return false;
if ( sceneObj.m_chaseObj )
sceneObj.Stop();
sceneObj.m_chaseObj = sceneObjTrg;
sceneObj.m_chaseObjDeltaCol = __688(2);
sceneObj.m_chaseObjDeltaRow = __688(3);
return true;
}
public bool __777()
{
SceneObj sceneObj = G.m_game.__307(__686(0));
if ( sceneObj==null )
return false;
int col = __688(1);
int row = __688(2);
sceneObj.Move(sceneObj.__35()+col*G.PATH_GRID_CELLSIZE, sceneObj.__36()+row*G.PATH_GRID_CELLSIZE);
sceneObj.__656();
G.m_game.m_input.__368();
return true;
}
public bool __778()
{
SceneObj sceneObj = G.m_game.__307(__686(0));
if ( sceneObj==null )
return false;
SceneCell cell = sceneObj.__631(__686(1));
if ( cell==null )
return false;
sceneObj.Move(cell.__35(), cell.__36());
sceneObj.__656();
G.m_game.m_input.__368();
return true;
}
public bool __779()
{
SceneObj sceneObj = G.m_game.__307(__686(0));
if ( sceneObj==null )
return false;
float x = G.__147(G.__146(__688(1)));
float y = G.__147(G.__146(__688(2)));
sceneObj.Move(x, y);
sceneObj.__656();
G.m_game.m_input.__368();
return true;
}
public bool __780()
{
SceneObj sceneObj = G.m_game.__307(__686(0));
if ( sceneObj==null )
return false;
sceneObj.Move((float)__688(1), (float)__688(2));
sceneObj.__656();
G.m_game.m_input.__368();
return true;
}
public bool __781()
{
SceneObj sceneObj = G.m_game.__307(__686(0));
if ( sceneObj==null )
return false;
sceneObj.__624((float)__688(1), (float)__688(2));
sceneObj.__656();
G.m_game.m_input.__368();
return true;
}
public bool __782()
{
SceneObj sceneObj = G.m_game.__307(__686(0));
if ( sceneObj==null )
return false;
for ( int i=0 ; i<2 ; i++ )
{
Variable var = __691(i+1);
if ( var==null )
continue;
var.m_value = i==0 ? sceneObj.__620().ToString() : sceneObj.__621().ToString();
}
Variable varName = __691(3);
if ( varName )
{
SceneCell cell = sceneObj.__630(sceneObj.__620(), sceneObj.__621());
if ( cell )
varName.m_value = cell.__392();
else
varName.m_value = "";
}
return true;
}
public bool __783()
{
SceneObj sceneObj = G.m_game.__307(__686(0));
if ( sceneObj==null )
return false;
for ( int i=0 ; i<2 ; i++ )
{
Variable var = __691(i+1);
if ( var==null )
continue;
var.m_value = i==0 ? ((int)sceneObj.__35()).ToString() : ((int)sceneObj.__36()).ToString();
}
return true;
}
public bool __784()
{
SceneObj sceneObj = G.m_game.__307(__686(0));
if ( sceneObj==null )
return false;
for ( int i=0 ; i<2 ; i++ )
{
Variable var = __691(i+1);
if ( var==null )
continue;
var.m_value = i==0 ? ((int)sceneObj.__607()).ToString() : ((int)sceneObj.__608()).ToString();
}
return true;
}
public bool __785()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
if ( G.m_game.m_dragging==false )
return false;
for ( int i=0 ; i<2 ; i++ )
{
Variable var = __691(i);
if ( var==null )
continue;
var.m_value = i==0 ? ((int)G.m_game.m_dropPos.x).ToString() : ((int)G.m_game.m_dropPos.y).ToString();
}
return true;
}
public bool __786()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__686(0));
if ( sceneObj==null || sceneObj.m_visible.cur==false )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneCell cell = sceneObj.__631(__686(i));
if ( cell && sceneObj.__620()==cell.m_col && sceneObj.__621()==cell.m_row )
return true;
}
return false;
}
public bool __787()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__686(0));
if ( sceneObj==null || sceneObj.m_visible.cur==false )
return false;
SceneCell cell = sceneObj.__629(sceneObj.__35(), sceneObj.__36());
if ( cell==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( cell.__593()==G.__113(__686(i)) )
return true;
}
return false;
}
public bool __788()
{
int tolerance = __688(2);
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__686(0));
if ( sceneObj==null || sceneObj.m_visible.cur==false )
return false;
SceneObj sceneObj2 = scene.__277(__686(1));
if ( sceneObj2==null || sceneObj2.m_visible.cur==false )
return false;
if ( Math.Abs(sceneObj.__620()-sceneObj2.__620())>tolerance )
return false;
if ( Math.Abs(sceneObj.__621()-sceneObj2.__621())>tolerance )
return false;
return true;
}
public bool __789()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__686(0));
if ( sceneObj==null || sceneObj.m_message )
return false;
Anim anim = sceneObj.m_obj.__470(__686(1));
if ( anim )
sceneObj.__626(anim);
sceneObj.m_anim.__672(G.__151(__686(2)));
return true;
}
public bool __790()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__686(0));
if ( sceneObj==null )
return false;
Anim anim = sceneObj.m_anim.cur;
if ( anim==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( G.__148(ref anim.m_name, __686(i)) )
return true;
}
return false;
}
public bool __791()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__686(0));
if ( sceneObj==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( sceneObj.m_anim.__679(__688(i)) )
return true;
}
return false;
}
public bool __792()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__686(0));
if ( sceneObj==null || sceneObj.m_message )
return false;
int index = G.__151(__686(1));
if ( index==-1 )
index = AnimDir.RIGHT;
sceneObj.m_anim.__672(index);
return true;
}
public bool __793()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__686(0));
if ( sceneObj==null )
return false;
AnimDir dir = sceneObj.m_anim.__673();
if ( dir==null )
return false;
string name = dir.__392();
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( G.__148(ref name, __686(i)) )
return true;
}
return false;
}
public bool __794()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__686(0));
if ( sceneObj==null || sceneObj.m_message )
return false;
SceneObj sceneObjTrg = scene.__277(__686(1));
if ( sceneObjTrg==null )
return false;
sceneObj.__627(sceneObjTrg);
return true;
}
public bool __795()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__686(0));
if ( sceneObj==null )
return false;
SceneObj sceneObjTrg = scene.__277(__686(1));
if ( sceneObjTrg==null )
return false;
int dir = -1;
if ( m_paramCount>=3 )
{
string p2 = __686(2);
if ( G.__148(ref p2, "LEFT") )
dir = AnimDir.LEFT;
else if ( G.__148(ref p2, "RIGHT") )
dir = AnimDir.RIGHT;
}
return sceneObj.__628(sceneObjTrg, dir);
}
public bool __796()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__686(0));
if ( sceneObj==null )
return false;
sceneObj.Rotate(__688(1));
return true;
}
public bool __797()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__686(0));
if ( sceneObj==null )
return false;
string p1 = __686(1);
int loop = p1.Length==0 ? -2 : G.__113(ref p1);
return sceneObj.__646(-1, "", loop);
}
public bool __798()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__686(0));
if ( sceneObj==null )
return false;
sceneObj.__647();
return true;
}
public bool __799()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__686(0));
if ( sceneObj==null || sceneObj.m_paths==null || sceneObj.m_paths[sceneObj.m_iPath.cur]==null )
return false;
SpotPath path = sceneObj.m_paths[sceneObj.m_iPath.cur];
path.Pause();
return true;
}
public bool __800()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__686(0));
if ( sceneObj==null || sceneObj.m_paths==null || sceneObj.m_paths[sceneObj.m_iPath.cur]==null )
return false;
SpotPath path = sceneObj.m_paths[sceneObj.m_iPath.cur];
path.__666();
return true;
}
public bool __801()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__686(0));
if ( sceneObj==null || sceneObj.m_paths==null || sceneObj.m_paths[sceneObj.m_iPath.cur]==null )
return false;
SpotPath path = sceneObj.m_paths[sceneObj.m_iPath.cur];
path.__667();
return true;
}
public bool __802()
{
if ( m_paramCount==0 )
return false;
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( scene.__48(__686(i)) )
return true;
}
return false;
}
public bool __803()
{
if ( m_paramCount==0 || G.m_game.m_iOldScene==-1 )
return false;
Scene scene = G.m_game.m_scenes[G.m_game.m_iOldScene];
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( scene.__48(__686(i)) )
return true;
}
return false;
}
public bool __804(string scene, float duration)
{
if ( m_script.__685() )
G.m_game.Jump(null, scene, Color.black, duration, 0.0f, duration*0.5f);
else
{
if ( G.m_game.__274(scene)==null )
return false;
G.m_game.m_scenario.m_debugFirstScene = scene;
}
return true;
}
public bool __804()
{
return __804(__686(0), __690(1));
}
public bool __805()
{
if ( G.m_game.m_iOldScene==-1 )
return false;
string scene = G.m_game.m_scenes[G.m_game.m_iOldScene].m_uid;
return __804(scene, __690(0));
}
public bool __806()
{
Scene scene = __291(0);
if ( scene==null )
return false;
if ( m_paramCount<2 )
scene.m_lightAmbient.Set(-1.0f);
else
{
int ambient = __688(1);
scene.m_lightAmbient.Set(ambient==-1 ? -1.0f : G.Clamp(ambient/255.0f));
}
scene.__561();
return true;
}
public bool __807()
{
Scene scene = __291(0);
if ( scene==null )
return false;
return scene.__545(__686(1), true);
}
public bool __808()
{
Scene scene = __291(0);
if ( scene==null )
return false;
return scene.__545(__686(1), false);
}
public bool __809()
{
Scene scene = __291(0);
if ( scene==null )
return false;
string p2 = __686(2);
float opacity = p2.Length==0 ? 1.0f : G.Clamp(G.__113(ref p2)/100.0f);
switch ( __686(1).ToUpper() )
{
case "MA":
if ( scene.m_maskLayers[0] )
{
scene.m_maskLayers[0].m_opacity = opacity;
return true;
}
break;
case "MB":
if ( scene.m_maskLayers[1] )
{
scene.m_maskLayers[1].m_opacity = opacity;
return true;
}
break;
case "MC":
if ( scene.m_maskLayers[2] )
{
scene.m_maskLayers[2].m_opacity = opacity;
return true;
}
break;
case "MD":
if ( scene.m_maskLayers[3] )
{
scene.m_maskLayers[3].m_opacity = opacity;
return true;
}
break;
case "BACK":
scene.m_backLayer.m_opacity = opacity;
return true;
case "FA":
if ( scene.m_farLayers[0] )
{
scene.m_farLayers[0].m_opacity = opacity;
return true;
}
break;
case "FB":
if ( scene.m_farLayers[1] )
{
scene.m_farLayers[1].m_opacity = opacity;
return true;
}
break;
case "FC":
if ( scene.m_farLayers[2] )
{
scene.m_farLayers[2].m_opacity = opacity;
return true;
}
break;
case "FD":
if ( scene.m_farLayers[3] )
{
scene.m_farLayers[3].m_opacity = opacity;
return true;
}
break;
}
return false;
}
public bool __810()
{
Scene scene = __291(0);
if ( scene==null )
return false;
Effect effect = G.m_game.__282(__686(1));
if ( effect==null )
scene.m_effect.Reset();
else
scene.m_effect.Set(effect);
return true;
}
public bool __811()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneStill still = scene.__540(__686(1));
if ( still==null )
return false;
still.m_tags[0] = __686(2);
return true;
}
public bool __812()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneStill still = scene.__540(__686(1));
if ( still==null )
return false;
Variable var = __691(2);
if ( var==null )
return false;
var.m_value = still.m_tags[0];
return true;
}
public bool __813()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneStill still = scene.__540(__686(1));
if ( still==null )
return false;
for ( int i=2 ; i<m_paramCount ; i++ )
{
if ( G.__149(still.m_tags, __692(i)) )
return true;
}
return false;
}
public bool __814()
{
Scene scene = __291(0);
if ( scene==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneStill still = scene.__540(__686(i));
if ( still )
still.m_visible.Set(true);
}
return true;
}
public bool __815()
{
Scene scene = __291(0);
if ( scene==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneStill still = scene.__540(__686(i));
if ( still )
still.m_visible.Set(false);
}
return true;
}
public bool __816()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneLabel label = scene.__536(__686(1));
if ( label==null )
return false;
label.m_parentName.Set(__686(2));
label.m_parent = scene.__535(ref label.m_parentName.cur);
return true;
}
public bool __817()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneLabel label = scene.__536(__686(1));
if ( label==null )
return false;
label.m_tags[0] = __686(2);
return true;
}
public bool __818()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneLabel label = scene.__536(__686(1));
if ( label==null )
return false;
Variable var = __691(2);
if ( var==null )
return false;
var.m_value = label.m_tags[0];
return true;
}
public bool __819()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneLabel label = scene.__536(__686(1));
if ( label==null )
return false;
for ( int i=2 ; i<m_paramCount ; i++ )
{
if ( G.__149(label.m_tags, __692(i)) )
return true;
}
return false;
}
public bool __820()
{
Scene scene = __291(0);
if ( scene==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneLabel label = scene.__536(__686(i));
if ( label )
label.m_enabled.Set(true);
}
return true;
}
public bool __821()
{
Scene scene = __291(0);
if ( scene==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneLabel label = scene.__536(__686(i));
if ( label )
label.m_enabled.Set(false);
}
return true;
}
public bool __822()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneLabel label = scene.__536(__686(1));
if ( label==null )
return false;
label.m_title.m_sub.Set(__688(2));
return true;
}
public bool __823()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneLabel label = scene.__536(__686(1));
if ( label==null )
return false;
Color defColor = G.m_game.m_colorText;
label.m_color = G.__162(__686(2).ToUpper(), (int)(defColor.r*255.0f), (int)(defColor.g*255.0f), (int)(defColor.b*255.0f));
return true;
}
public bool __824()
{
Scene scene = __291(0);
if ( scene==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneLabel label = scene.__536(__686(i));
if ( label )
label.m_visible.Set(true);
}
return true;
}
public bool __825()
{
Scene scene = __291(0);
if ( scene==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneLabel label = scene.__536(__686(i));
if ( label )
label.m_visible.Set(false);
}
return true;
}
public bool __826()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneLabel label = scene.__536(__686(1));
if ( label==null )
return false;
label.m_text.m_sub.Set(__688(2));
label.__618();
return true;
}
public bool __827()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneLabel label = scene.__536(__686(1));
if ( label==null )
return false;
label.m_usertext.Set(__686(2));
label.__618();
return true;
}
public bool __828()
{
Scene scene = __291(0);
if ( scene==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneLabel label = scene.__536(__686(i));
if ( label )
label.m_cheat.Set(true);
}
return true;
}
public bool __829()
{
Scene scene = __291(0);
if ( scene==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneLabel label = scene.__536(__686(i));
if ( label )
label.m_cheat.Set(false);
}
return true;
}
public bool __830()
{
Scene scene = __291(0);
if ( scene==null )
return false;
scene.m_bokehSize.Set(G.__118(__686(1)));
scene.m_bokehMaxZoom.Set(G.__121(__686(2)));
return true;
}
public bool __831()
{
Scene scene = __291(0);
if ( scene==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneBokeh bokeh = scene.__539(__686(i));
if ( bokeh )
bokeh.m_visible.Set(true);
}
return true;
}
public bool __832()
{
Scene scene = __291(0);
if ( scene==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneBokeh bokeh = scene.__539(__686(i));
if ( bokeh )
bokeh.m_visible.Set(false);
}
return true;
}
public bool __833()
{
Scene scene = G.m_game.__291();
for ( int i=0 ; i<m_paramCount ; i++ )
{
string uid = __686(i);
Obj obj = G.m_game.__277(uid);
if ( obj )
{
obj.Reset(true);
if ( scene )
{
SceneObj sceneObj = scene.__277(obj);
if ( sceneObj )
{
sceneObj.__645();
sceneObj.Reset(true);
G.m_game.__306(scene.m_uid, sceneObj.m_obj.m_uid, true);
}
}
}
}
return true;
}
public bool __834()
{
for ( int iObj=0 ; iObj<G.m_game.m_objects.Length ; iObj++ )
{
Obj obj = G.m_game.m_objects[iObj];
obj.Reset(true);
}
Scene scene = G.m_game.__291();
if ( scene )
{
for ( int iObj=0 ; iObj<scene.m_objects.Length ; iObj++ )
{
SceneObj sceneObj = scene.m_objects[iObj];
sceneObj.__645();
sceneObj.Reset(true);
G.m_game.__306(scene.m_uid, sceneObj.m_obj.m_uid, true);
}
}
return true;
}
public bool __835()
{
Asset asset = G.__95(G.m_pathGraphics);
if ( asset==null )
return false;
Scene scene = G.m_game.__291();
for ( int i=0 ; i<m_paramCount ; i++ )
{
string uid = __686(i);
Obj obj = G.m_game.__277(uid);
if ( obj && obj.m_killed.cur )
{
obj.m_killed.Set(false);
if ( scene )
{
SceneObj sceneObj = scene.__277(uid);
if ( sceneObj )
obj.__468(asset, scene);
}
}
}
asset.Close();
return true;
}
public bool __836()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
string uid = __686(i);
Obj obj = G.m_game.__277(uid);
if ( obj && obj.m_killed.cur==false )
{
G.m_game.__306("", uid, false);
obj.End(null);
obj.m_killed.Set(true);
}
}
return true;
}
public bool __837()
{
Obj obj = G.m_game.__277(__686(0));
if ( obj==null )
return false;
Color defColor = G.m_game.m_colorText;
obj.m_speech = G.__162(__686(1).ToUpper(), (int)(defColor.r*255.0f), (int)(defColor.g*255.0f), (int)(defColor.b*255.0f));
return true;
}
public bool __838()
{
Obj obj = G.m_game.__277(__686(0));
if ( obj==null )
return false;
obj.m_title.m_sub.Set(__688(1));
return true;
}
public bool __839()
{
Obj obj = G.m_game.__277(__686(0));
if ( obj==null )
return false;
int index = G.Clamp(__688(1), 0, 3);
if ( index==obj.m_icon.cur )
return false;
if ( obj.__453() )
obj.__453().End();
obj.m_icon.Set(index);
Sprite icon = obj.__453();
if ( icon==null )
return false;
Asset asset = G.__95(G.m_pathGraphics);
if ( asset )
{
icon.__468(asset);
asset.Close();
}
return true;
}
public bool __840()
{
Obj obj = G.m_game.__277(__686(0));
if ( obj==null )
return false;
if ( m_paramCount<2 )
{
obj.m_anchorX.__77();
obj.m_anchorY.__77();
}
else
{
obj.m_anchorX.Set(__688(1));
obj.m_anchorY.Set(__688(2));
}
return true;
}
public bool __841()
{
Obj obj = G.m_game.__277(__686(0));
if ( obj==null )
return false;
if ( m_paramCount<2 )
{
obj.m_speedX.__77();
obj.m_speedY.__77();
}
else
{
obj.m_speedX.Set(__688(1));
obj.m_speedY.Set(__688(2));
}
return true;
}
public bool __842()
{
Obj obj = G.m_game.__277(__686(0));
if ( obj==null )
return false;
obj.m_tint.Set(G.__123(__686(1)));
return true;
}
public bool __843()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
string uid = __686(i);
Obj obj = G.m_game.__277(uid);
if ( obj )
obj.m_enabled.Set(true);
}
return true;
}
public bool __844()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
string uid = __686(i);
Obj obj = G.m_game.__277(uid);
if ( obj )
obj.m_enabled.Set(false);
}
return true;
}
public bool __845()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
string uid = __686(i);
Obj obj = G.m_game.__277(uid);
if ( obj )
obj.m_subEnabled.Set(true);
}
return true;
}
public bool __846()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
string uid = __686(i);
Obj obj = G.m_game.__277(uid);
if ( obj )
obj.m_subEnabled.Set(false);
}
return true;
}
public bool __847()
{
string uidScene = "";
string uidObj = "";
for ( int i=0 ; i<m_paramCount ; i++ )
{
G.__97(__686(i), ref uidScene, ref uidObj);
G.m_game.__306(uidScene, uidObj, true);
}
return true;
}
public bool __848()
{
string uidScene = "";
string uidObj = "";
for ( int i=0 ; i<m_paramCount ; i++ )
{
G.__97(__686(i), ref uidScene, ref uidObj);
G.m_game.__306(uidScene, uidObj, false);
}
return true;
}
public bool __849()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
SceneObj sceneObj = G.m_game.__307(__686(i));
if ( sceneObj==null )
continue;
if ( sceneObj.m_visible.cur )
return true;
}
return false;
}
public bool __850()
{
SceneObj sceneObj = G.m_game.__307(__686(0));
if ( sceneObj==null )
return false;
sceneObj.m_parentName.Set(__686(1));
sceneObj.m_parent = sceneObj.m_scene.__535(ref sceneObj.m_parentName.cur);
return true;
}
public bool __851()
{
string p1 = __686(1);
string p2 = __686(2);
string p3 = __686(3);
string defaultStop = p1.Length==0 ? "STOP" : p1;
string defaultTalk = p2.Length==0 ? "TALK" : p2;
string defaultWalk = p3.Length==0 ? "WALK" : p3;
Scene scene = null;
SceneObj sceneObj = null;
if ( __695(__686(0), ref scene, ref sceneObj)==false )
return false;
string prevDefStopAnim = sceneObj.m_defaultStopAnim.cur;
string prevDefTalkAnim = sceneObj.m_defaultTalkAnim.cur;
string prevDefWalkAnim = sceneObj.m_defaultWalkAnim.cur;
sceneObj.m_defaultStopAnim.Set(defaultStop);
sceneObj.m_defaultTalkAnim.Set(defaultTalk);
sceneObj.m_defaultWalkAnim.Set(defaultWalk);
if ( sceneObj.m_message==null && sceneObj.m_anim.cur )
{
if ( sceneObj.m_anim.cur.m_name==prevDefStopAnim && sceneObj.m_defaultStopAnim.cur!=prevDefStopAnim )
sceneObj.__626(ref sceneObj.m_defaultStopAnim.cur);
if ( sceneObj.m_anim.cur.m_name==prevDefTalkAnim && sceneObj.m_defaultTalkAnim.cur!=prevDefTalkAnim )
sceneObj.__626(ref sceneObj.m_defaultTalkAnim.cur);
if ( sceneObj.m_anim.cur.m_name==prevDefWalkAnim && sceneObj.m_defaultWalkAnim.cur!=prevDefWalkAnim )
sceneObj.__626(ref sceneObj.m_defaultWalkAnim.cur);
}
return true;
}
public bool __852()
{
string p1 = __686(1);
string defaultStop = p1.Length==0 ? "STOP" : p1;
Scene scene = null;
SceneObj sceneObj = null;
if ( __695(__686(0), ref scene, ref sceneObj)==false )
return false;
string prevDefStopAnim = sceneObj.m_defaultStopAnim.cur;
sceneObj.m_defaultStopAnim.Set(defaultStop);
if ( sceneObj.m_message==null && sceneObj.m_anim.cur )
{
if ( sceneObj.m_anim.cur.m_name==prevDefStopAnim && sceneObj.m_defaultStopAnim.cur!=prevDefStopAnim )
sceneObj.__626(ref sceneObj.m_defaultStopAnim.cur);
}
return true;
}
public bool __853()
{
string p1 = __686(1);
string defaultWalk = p1.Length==0 ? "WALK" : p1;
Scene scene = null;
SceneObj sceneObj = null;
if ( __695(__686(0), ref scene, ref sceneObj)==false )
return false;
string prevDefWalkAnim = sceneObj.m_defaultWalkAnim.cur;
sceneObj.m_defaultWalkAnim.Set(defaultWalk);
if ( sceneObj.m_message==null && sceneObj.m_anim.cur )
{
if ( sceneObj.m_anim.cur.m_name==prevDefWalkAnim && sceneObj.m_defaultWalkAnim.cur!=prevDefWalkAnim )
sceneObj.__626(ref sceneObj.m_defaultWalkAnim.cur);
}
return true;
}
public bool __854()
{
string p1 = __686(1);
string defaultTalk = p1.Length==0 ? "TALK" : p1;
Scene scene = null;
SceneObj sceneObj = null;
if ( __695(__686(0), ref scene, ref sceneObj)==false )
return false;
string prevDefTalkAnim = sceneObj.m_defaultTalkAnim.cur;
sceneObj.m_defaultTalkAnim.Set(defaultTalk);
if ( sceneObj.m_message==null && sceneObj.m_anim.cur )
{
if ( sceneObj.m_anim.cur.m_name==prevDefTalkAnim && sceneObj.m_defaultTalkAnim.cur!=prevDefTalkAnim )
sceneObj.__626(ref sceneObj.m_defaultTalkAnim.cur);
}
return true;
}
public bool __855()
{
Scene scene = null;
SceneObj sceneObj = null;
if ( __695(__686(0), ref scene, ref sceneObj)==false )
return false;
Anim anim = sceneObj.m_obj.__470(__686(1));
if ( anim==null )
return false;
anim.__473(__688(2));
return true;
}
public bool __856()
{
Scene scene = null;
SceneObj sceneObj = null;
if ( __695(__686(0), ref scene, ref sceneObj)==false )
return false;
Anim anim = sceneObj.m_obj.__470(__686(1));
if ( anim==null )
return false;
string mode = __686(2);
if ( mode=="BACKWARD" )
anim.__473(-Math.Abs(anim.m_speed.cur));
else if ( mode=="TOGGLE" )
anim.__473(-anim.m_speed.cur);
else
anim.__473(Math.Abs(anim.m_speed.cur));
return true;
}
public bool __857()
{
Scene scene = null;
SceneObj sceneObj = null;
if ( __695(__686(0), ref scene, ref sceneObj)==false )
return false;
sceneObj.m_tags[0] = __686(1);
return true;
}
public bool __858()
{
Scene scene = null;
SceneObj sceneObj = null;
if ( __695(__686(0), ref scene, ref sceneObj)==false )
return false;
Variable var = __691(1);
if ( var==null )
return false;
var.m_value = sceneObj.m_tags[0];
return true;
}
public bool __859()
{
Scene scene = null;
SceneObj sceneObj = null;
if ( __695(__686(0), ref scene, ref sceneObj)==false )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
string tag = __692(i);
if ( G.__149(sceneObj.m_tags, ref tag) )
return true;
if ( G.__149(sceneObj.m_obj.m_tags, ref tag) )
return true;
}
return false;
}
public bool __860()
{
Scene scene = null;
SceneObj sceneObj = null;
if ( __695(__686(0), ref scene, ref sceneObj)==false )
return false;
sceneObj.m_sticker.Set(__686(1));
return true;
}
public bool __861()
{
Scene scene = null;
SceneObj sceneObj = null;
if ( __695(__686(0), ref scene, ref sceneObj)==false )
return false;
Variable var = __691(1);
if ( var==null )
return false;
var.m_value = sceneObj.m_sticker.cur;
return true;
}
public bool __862()
{
Scene scene = null;
SceneObj sceneObj = null;
if ( __695(__686(0), ref scene, ref sceneObj)==false )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( sceneObj.m_sticker.cur==__686(i) )
return true;
}
return false;
}
public bool __863()
{
SceneObj sceneObj = __693(0);
if ( sceneObj==null )
return false;
sceneObj.m_elevator.Set(__688(1));
return true;
}
public bool __864()
{
SceneObj sceneObj = __693(0);
if ( sceneObj==null )
return false;
int iGrid = __688(1);
if ( iGrid<0 || iGrid>=G.PATH_GRID_COUNT )
return false;
sceneObj.m_iGrid.Set(iGrid);
return true;
}
public bool __865()
{
SceneObj sceneObj = __693(0);
if ( sceneObj==null || sceneObj.m_paths==null )
return false;
int iPath = __688(1);
if ( iPath<0 || iPath>=G.PATH_GRID_COUNT || sceneObj.m_paths[iPath]==null )
return false;
sceneObj.m_iPath.Set(iPath);
sceneObj.m_pathStarted.Set(false);
sceneObj.m_pathRoleBox = null;
return true;
}
public bool __866()
{
SceneObj sceneObj = __693(0);
if ( sceneObj==null )
return false;
sceneObj.m_opacity.Set(G.Clamp(__688(1), 0, 100)/100.0f);
return true;
}
public bool __867()
{
SceneObj sceneObj = __693(0);
if ( sceneObj==null )
return false;
sceneObj.m_angle.Set(__688(1));
return true;
}
public bool __868()
{
Variable var = __691(1);
if ( var==null )
return false;
SceneObj sceneObj = __693(0);
if ( sceneObj==null )
{
var.m_value = "0";
return false;
}
var.m_value = G.__144(sceneObj.m_angle.cur).ToString();
return true;
}
public bool __869()
{
SceneObj sceneObj = __693(0);
if ( sceneObj==null )
return false;
int iOld = (int)sceneObj.m_placement.cur;
switch ( __686(1) )
{
case "MA":		sceneObj.m_placement.Set(PLACEMENT.MA); break;
case "MB":		sceneObj.m_placement.Set(PLACEMENT.MB); break;
case "MC":		sceneObj.m_placement.Set(PLACEMENT.MC); break;
case "MD":		sceneObj.m_placement.Set(PLACEMENT.MD); break;
case "OBJECT":	sceneObj.m_placement.Set(PLACEMENT.OBJECT); break;
case "FA":		sceneObj.m_placement.Set(PLACEMENT.FA); break;
case "FB":		sceneObj.m_placement.Set(PLACEMENT.FB); break;
case "FC":		sceneObj.m_placement.Set(PLACEMENT.FC); break;
case "FD":		sceneObj.m_placement.Set(PLACEMENT.FD); break;
case "CLEAR":	sceneObj.m_placement.Set(PLACEMENT.CLEAR); break;
default:		sceneObj.m_placement.Set(PLACEMENT.BACK); break;
}
int iNew = (int)sceneObj.m_placement.cur;
if ( iNew==iOld )
return false;
Scene scene = sceneObj.m_scene;
if ( scene!=G.m_game.__291() )
return true;
if ( scene.m_sortedEntities[iNew]==null )
scene.m_sortedEntities[iNew] = new List<SceneEntity>();
scene.m_sortedEntities[iNew].Add(sceneObj);
scene.m_sortedEntities[iOld].Remove(sceneObj);
if ( scene.m_sortedEntities[iOld].Count==0 )
scene.m_sortedEntities[iOld] = null;
scene.__562();
scene.__564();
return true;
}
public bool __870()
{
SceneObj sceneObj = G.m_game.__307(__686(0));
if ( sceneObj==null )
return false;
if ( m_paramCount==1 )
sceneObj.m_autoZ.Set(true);
else
{
sceneObj.m_autoZ.Set(false);
sceneObj.m_z.Set((float)__688(1));
}
return true;
}
public bool __871()
{
SceneObj sceneObj = G.m_game.__307(__686(0));
if ( sceneObj==null )
return false;
if ( m_paramCount==1 )
sceneObj.m_manualScale.Set(0.0f);
else
sceneObj.m_manualScale.Set(G.__120(__686(1)));
return true;
}
public bool __872()
{
Variable var = __691(1);
if ( var==null )
return false;
SceneObj sceneObj = __693(0);
if ( sceneObj==null )
{
var.m_value = "100";
return false;
}
float scale = sceneObj.__640(sceneObj.__615()) * 100.0f;
var.m_value = G.Clamp((int)scale, 0, 400).ToString();
return true;
}
public bool __873()
{
SceneObj sceneObj = G.m_game.__307(__686(0));
if ( sceneObj==null )
return false;
if ( m_paramCount==1 )
sceneObj.m_manualZoom.Set(0.0f);
else
sceneObj.m_manualZoom.Set(G.__121(__686(1)));
return true;
}
public bool __874()
{
Variable var = __691(1);
if ( var==null )
return false;
SceneObj sceneObj = __693(0);
if ( sceneObj==null )
{
var.m_value = "100";
return false;
}
float zoom = sceneObj.__641(sceneObj.__615()) * 100.0f;
var.m_value = G.Clamp((int)zoom, 100, 400).ToString();
return true;
}
public bool __875()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
SceneObj sceneObj = G.m_game.__307(__686(i));
if ( sceneObj )
sceneObj.m_cheat.Set(true);
}
return true;
}
public bool __876()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
SceneObj sceneObj = G.m_game.__307(__686(i));
if ( sceneObj )
sceneObj.m_cheat.Set(false);
}
return true;
}
public bool __877(bool visible)
{
bool changed = false;
for ( int i=0 ; i<m_paramCount ; i++ )
{
SceneObj sceneObj = G.m_game.__307(__686(i));
if ( sceneObj )
{
if ( sceneObj.__625(visible, false) )
changed = true;
}
}
if ( changed )
{
Scene scene = G.m_game.__291();
if ( scene )
scene.__561();
}
return true;
}
public bool __877()
{
return __877(true);
}
public bool __878()
{
return __877(false);
}
public bool __879()
{
SceneObj sceneObj = __693(0);
if ( sceneObj==null || sceneObj.m_light==false )
return false;
sceneObj.m_lightAmbient.Set(G.Clamp(__688(1)/255.0f));
return true;
}
public bool __880()
{
SceneObj sceneObj = __693(0);
if ( sceneObj==null || sceneObj.m_light==false )
return false;
sceneObj.m_lightDiffuse.Set(G.__123(__686(1)));
return true;
}
public bool __881()
{
SceneObj sceneObj = __693(0);
if ( sceneObj==null || sceneObj.m_light==false )
return false;
sceneObj.m_lightAngle.Set(G.Clamp(__688(1)*G.DEG_TO_RAD, 0.0f, G.RAD_360));
sceneObj.m_lightMeshChanged = true;
return true;
}
public bool __882()
{
SceneObj sceneObj = __693(0);
if ( sceneObj==null || sceneObj.m_light==false )
return false;
sceneObj.m_lightDir.Set(G.__142(__688(1)));
sceneObj.m_lightMeshChanged = true;
return true;
}
public bool __883()
{
SceneObj sceneObj = __693(0);
if ( sceneObj==null || sceneObj.m_light==false )
return false;
float oldDist = sceneObj.m_lightDist.cur;
float newDist = G.__133(__688(1));
sceneObj.m_lightDist.Set(newDist);
sceneObj.m_lightMeshChanged = true;
if ( oldDist==0.0f )
{
if ( newDist!=0.0f )
{
Scene scene = G.m_game.__291();
if ( scene )
scene.__561();
}
}
else
{
if ( newDist==0.0f )
{
Scene scene = G.m_game.__291();
if ( scene )
scene.__561();
}
}
return true;
}
public bool __884()
{
G.m_game.__309();
return true;
}
public bool __885()
{
return G.m_game.m_menuDialog.__38();
}
public bool __886()
{
Dialog dialog = __694(0);
if ( dialog==null )
return false;
Sentence sentence = dialog.m_root.__49(__688(1));
if ( sentence==null )
return false;
return sentence.m_visited;
}
public bool __887()
{
Dialog dialog = __694(0);
if ( dialog==null )
return false;
Sentence sentence = dialog.m_root.__49(__688(1));
if ( sentence==null || sentence.m_choice==false )
return false;
return sentence.m_visible.cur;
}
public bool __888()
{
Dialog dialog = __694(0);
if ( dialog )
dialog.__50(__688(1));
return true;
}
public bool __889()
{
Dialog dialog = __694(0);
if ( dialog )
dialog.__50(__688(1), false);
return true;
}
public bool __890()
{
string name = __686(0);
if ( name.Length==0 )
G.m_game.m_colorVoiceOver = Color.clear;
else
G.m_game.m_colorVoiceOver = G.__162(name);
return true;
}
public bool __891()
{
Variable var = __691(0);
if ( var==null )
return false;
var.m_value = __688(2).ToString();
return true;
}
public bool __892()
{
Variable var = __691(2);
if ( var==null )
return false;
var.m_value = G.m_game.__312(__686(0), __688(1));
return true;
}
public bool __893()
{
Dialog dialog = __694(0);
if ( dialog==null )
return false;
Sentence sentence = dialog.m_root.__49(__688(1));
if ( sentence==null )
return false;
sentence.m_tags[0] = __686(2);
return true;
}
public bool __894()
{
Dialog dialog = __694(0);
if ( dialog==null )
return false;
Sentence sentence = dialog.m_root.__49(__688(1));
if ( sentence==null )
return false;
Variable var = __691(2);
if ( var==null )
return false;
var.m_value = sentence.m_tags[0];
return true;
}
public bool __895()
{
Dialog dialog = __694(0);
if ( dialog==null )
return false;
Sentence sentence = dialog.m_root.__49(__688(1));
if ( sentence==null )
return false;
for ( int i=2 ; i<m_paramCount ; i++ )
{
if ( G.__149(sentence.m_tags, __692(i)) )
return true;
}
return false;
}
public bool __896()
{
return G.m_game.__305(__686(0), __686(1));
}
public bool __897()
{
return G.m_game.__304(__686(0), m_script.__685());
}
public bool __898()
{
if ( G.m_game.m_menuScene )
return false;
if ( m_paramCount==0 )
{
G.m_game.m_layout.__417(new string[0]);
}
else
{
string[] players = new string[m_paramCount];
for ( int i=0 ; i<m_paramCount ; i++ )
players[i] = __686(i);
G.m_game.m_layout.__417(players);
}
return true;
}
public bool __899()
{
Player player = G.m_game.__279(__686(0));
if ( player==null )
return false;
int index = G.Clamp(__688(1), 0, 3);
if ( index==player.m_icon.cur )
return false;
if ( player.__453() )
player.__453().End();
player.m_icon.Set(index);
Sprite icon = player.__453();
if ( icon==null )
return false;
Asset asset = G.__95(G.m_pathGraphics);
if ( asset )
{
icon.__468(asset);
asset.Close();
}
return true;
}
public bool __900()
{
Player player = G.m_game.__293();
if ( player==null )
return false;
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( player.__48(__686(i)) )
return true;
}
return false;
}
public bool __901()
{
if ( G.m_game.m_menuScene )
return false;
Player player = G.m_game.__279(__686(0));
if ( player==null )
return false;
player.__476(__686(1));
return true;
}
public bool __902()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
Player player = G.m_game.__279(__686(0));
if ( player )
player.__481();
}
return true;
}
public bool __903()
{
Player src = G.m_game.__279(__686(0));
Player trg = G.m_game.__279(__686(1));
if ( src==null || trg==null )
return false;
src.__482(trg);
return true;
}
public bool __904()
{
return __904(true);
}
public bool __905()
{
return __904(false);
}
public bool __904(bool withFx)
{
Player player = G.m_game.__279(__686(0));
if ( player==null )
return false;
if ( m_script.__685()==false )
withFx = false;
string uidScene = "";
string uidObj = "";
for ( int i=1 ; i<m_paramCount ; i++ )
{
G.__97(__686(i), ref uidScene, ref uidObj);
G.m_game.__306(uidScene, uidObj, false);
Obj obj = G.m_game.__277(uidObj);
Player objPlayer = obj.__293();
if ( objPlayer )
objPlayer.__480(obj);
player.__479(obj);
if ( withFx && obj.__453() )
G.m_game.__313(obj);
}
return true;
}
public bool __906()
{
return __906(true);
}
public bool __907()
{
return __906(false);
}
public bool __906(bool withFx)
{
Player player = G.m_game.__279(__686(0));
if ( player==null )
return false;
if ( m_script.__685()==false )
withFx = false;
string[] uidScene = new string[2];
string[] uidObj = new string[2];
Obj[] obj = new Obj[2];
Player[] objPlayer = new Player[2];
for ( int i=0 ; i<2 ; i++ )
{
G.__97(__686(i+1), ref uidScene[i], ref uidObj[i]);
G.m_game.__306(uidScene[i], uidObj[i], false);
obj[i] = G.m_game.__277(uidObj[i]);
objPlayer[i] = obj[i].__293();
}
if ( objPlayer[0]==null || objPlayer[0]!=player || obj[1]==null )
return false;
if ( player.__484(obj[0])==-1 )
return false;
if ( objPlayer[1] )
objPlayer[1].__480(obj[1]);
int index = player.__484(obj[0]);
if ( index==-1 )
return false;
objPlayer[0].__480(obj[0]);
G.m_game.__290().__479(obj[0]);
player.__479(obj[1], index);
if ( withFx && obj[1].__453() )
G.m_game.__313(obj[1]);
return true;
}
public bool __908()
{
string uidScene = "";
string uidObj = "";
for ( int i=0 ; i<m_paramCount ; i++ )
{
G.__97(__686(i), ref uidScene, ref uidObj);
G.m_game.__306(uidScene, uidObj, false);
Obj obj = G.m_game.__277(uidObj);
Player objPlayer = obj.__293();
if ( objPlayer )
objPlayer.__480(obj);
G.m_game.__290().__479(obj);
}
return true;
}
public bool __909()
{
Player dump = G.m_game.__290();
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( dump.__483(G.m_game.__277(__686(i))) )
return true;
}
return false;
}
public bool __910()
{
Player player = G.m_game.__279(__686(0));
if ( player==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( player.__483(__686(i)) )
return true;
}
return false;
}
public bool __911()
{
Player player = G.m_game.__279(__686(0));
if ( player==null )
return false;
player.m_hasScroll.Set(true);
Scene scene = G.m_game.__291();
if ( scene )
scene.__567(player);
return true;
}
public bool __912()
{
Player player = G.m_game.__279(__686(0));
if ( player==null )
return false;
player.m_hasScroll.Set(false);
Scene scene = G.m_game.__291();
if ( scene )
scene.__567(player);
return true;
}
public bool __913()
{
Player player = G.m_game.__279(__686(0));
if ( player==null )
return false;
player.m_hasZoom.Set(true);
Scene scene = G.m_game.__291();
if ( scene )
scene.__567(player);
return true;
}
public bool __914()
{
Player player = G.m_game.__279(__686(0));
if ( player==null )
return false;
player.m_hasZoom.Set(false);
Scene scene = G.m_game.__291();
if ( scene )
scene.__567(player);
return true;
}
public bool __915()
{
Effect effect = G.m_game.__282(__686(0));
if ( effect==null )
return false;
float size = G.__118(__686(1));
float maxZoom = G.__121(__686(2));
int count = 0;
for ( int i=0 ; i<effect.__66() ; i++ )
{
if ( effect.m_items[i].m_model==EffectItem.MODEL.BLUR )
{
EffectItem_Blur fx = (EffectItem_Blur)effect.m_items[i];
fx.__76(size, maxZoom);
count++;
}
}
return count>0;
}
public bool __916()
{
Effect effect = G.m_game.__282(__686(0));
if ( effect==null )
return false;
int angle = __688(1);
int scale = __688(2);
int focus = __688(3);
int count = 0;
for ( int i=0 ; i<effect.__66() ; i++ )
{
if ( effect.m_items[i].m_model==EffectItem.MODEL.PARALLAX )
{
EffectItem_Parallax fx = (EffectItem_Parallax)effect.m_items[i];
fx.__76(angle, scale, focus);
count++;
}
}
return count>0;
}
public bool __917()
{
if ( m_script.__685()==false )
return false;
string p1 = __686(1);
float vol = p1.Length==0 ? 1.0f : G.Clamp(G.__113(ref p1)/100.0f);
bool loop = __688(2)!=0;
Sound sound = G.m_game.__271(__686(0));
if ( sound )
{
sound.m_loop = loop;
sound.__982(vol);
}
return true;
}
public bool __918()
{
if ( m_script.__685()==false )
return false;
for ( int i=0 ; i<m_paramCount ; i++ )
{
string name = __686(i);
if ( name.Length==0 )
{
G.m_game.__268();
break;
}
Sound sound = G.m_game.__271(name);
if ( sound )
sound.Stop();
}
return true;
}
public bool __919()
{
if ( m_script.__685()==false )
return false;
for ( int i=0 ; i<m_paramCount ; i++ )
{
Sound sound = G.m_game.__271(__686(i));
if ( sound && sound.__527() )
return true;
}
return false;
}
public bool __920()
{
float volume = G.Clamp(__688(0)/100.0f);
float duration = __690(1);
G.m_game.__263(__688(2), volume, duration);
return true;
}
public bool __921()
{
int p0 = __688(0);
G.m_game.m_locked = true;
G.m_game.m_lockedDuration = p0<=0 ? -1.0f : p0/1000.0f;
G.m_game.m_layout.m_bagOpened = false;
G.m_game.m_cursorObj = null;
return true;
}
public bool __922()
{
G.m_game.m_locked = false;
G.m_game.m_lockedDuration = -1.0f;
G.m_game.m_layout.m_bagOpened = false;
return true;
}
public bool __923()
{
return G.m_game.__256();
}
public bool __924()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
string name = __686(i);
if ( name=="BAG" )
G.m_game.m_gestureBagLocked = true;
else if ( name=="MENU" )
G.m_game.m_gestureMenuLocked = true;
}
return true;
}
public bool __925()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
string name = __686(i);
if ( name=="BAG" )
G.m_game.m_gestureBagLocked = false;
else if ( name=="MENU" )
G.m_game.m_gestureMenuLocked = false;
}
return true;
}
public bool __926()
{
if ( G.__86() )
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
string key = __686(i).ToLower();
if ( key=="shift" )
{
if ( Input.GetKey("left shift") )
return true;
key = "rshift";
}
else if ( key=="ctrl" )
{
if ( Input.GetKey("left ctrl") )
return true;
key = "rctrl";
}
switch ( key )
{
case "lshift":
key = "left shift";
break;
case "rshift":
key = "right shift";
break;
case "lctrl":
key = "left ctrl";
break;
case "rctrl":
key = "right ctrl";
break;
default:
if ( key.Length>2 && key.Substring(0, 3)=="pad" )
key = "[" + key.Substring(3) + "]";
break;
}
if ( Input.GetKey(key) )
return true;
}
}
return false;
}
public bool __927()
{
return G.m_game.m_input.m_isDown;
}
public bool __928()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
int x = G.m_game.m_cursor ? (int)scene.__550(G.m_game.m_cursorViewX) : -1;
int y = G.m_game.m_cursor ? (int)scene.__551(G.m_game.m_cursorViewY) : -1;
for ( int i=0 ; i<2 ; i++ )
{
string value = i==0 ? x.ToString() : y.ToString();
Variable var = __691(i);
if ( var )
var.m_value = value;
}
if ( x==-1 || y==-1 )
return false;
return true;
}
public bool __929()
{
G.m_game.__298(__686(0));
return true;
}
public bool __930()
{
G.m_game.m_cursorVisible = true;
return true;
}
public bool __931()
{
G.m_game.m_cursorVisible = false;
return true;
}
public bool __932()
{
G.m_game.m_useLocked = true;
return true;
}
public bool __933()
{
G.m_game.m_useLocked = false;
return true;
}
public bool __934()
{
G.m_game.m_layout.m_bagLocked = true;
G.m_game.m_layout.m_bagOpened = false;
G.m_game.m_cursorObj = null;
return true;
}
public bool __935()
{
G.m_game.m_layout.m_bagLocked = false;
G.m_game.m_layout.m_bagOpened = false;
return true;
}
public bool __936()
{
G.m_game.m_layout.m_bagForceHidden = false;
G.m_game.m_cursorObj = null;
return true;
}
public bool __937()
{
G.m_game.m_layout.m_bagForceHidden = true;
return true;
}
public bool __938()
{
if ( G.m_game.m_menuScene )
return false;
G.m_game.__257();
return true;
}
public bool __939()
{
if ( G.m_game.m_menuScene==null )
return false;
G.m_game.__258();
return true;
}
public bool __940()
{
if ( G.m_game.m_menuScene==null )
return false;
string uid = __686(0);
if ( G.m_game.__274(uid)==null )
return false;
G.m_game.__302(uid);
return true;
}
public bool __941()
{
G.m_game.__259();
return true;
}
public bool __942()
{
bool ok = false;
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.__187(G.__113(__686(i)), true) )
ok = true;
}
return ok;
}
public bool __943()
{
bool ok = false;
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.__187(G.__113(__686(i)), false) )
ok = true;
}
return ok;
}
public bool __944()
{
G.m_game.m_saveMenuLocked = true;
return true;
}
public bool __945()
{
G.m_game.m_saveMenuLocked = false;
return true;
}
public bool __946()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.__148(__686(i), G.m_game.__210()) )
return true;
}
return false;
}
public bool __947()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.__148(__686(i), G.m_game.__207()) )
return true;
}
return false;
}
public bool __948()
{
int index = G.m_game.__205(__686(0));
if ( index==-1 )
return false;
G.m_game.m_optionLanguageAudio = index;
G.m_game.__248();
return true;
}
public bool __949()
{
int index = G.m_game.__205(__686(0));
if ( index==-1 )
return false;
G.m_game.m_optionLanguageText = index;
G.m_game.__248();
G.m_game.__218();
return true;
}
public bool __950()
{
return G.m_game.m_earlyAccess;
}
public bool __951()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.m_game.__242(__686(i)) )
return true;
}
return false;
}
public bool __952()
{
return G.m_game.m_domainAllowed;
}
public bool __953()
{
Effect effect = G.m_game.__282(__686(0));
if ( effect==null )
G.m_game.m_effect.Reset();
else
G.m_game.m_effect.Set(effect);
return true;
}
public bool __954()
{
Variable var = __691(0);
if ( var==null )
return false;
int start = __688(1);
int time = (int)(G.m_game.m_time*1000.0f);
var.m_value = (time-start).ToString();
return true;
}
public bool __955()
{
Variable var = __691(0);
if ( var==null )
return false;
int time = (int)(G.m_game.m_elapsed*1000.0f);
var.m_value = time.ToString();
return true;
}
public bool __956()
{
string asset = __686(0);
if ( asset.Length==0 )
return false;
switch ( asset[0] )
{
case 'S':
case 's':
{
Scene scene = G.m_game.__274(asset);
if ( scene )
{
scene.m_tags[0] = __686(1);
return true;
}
break;
}
case 'O':
case 'o':
{
Obj obj = G.m_game.__277(asset);
if ( obj )
{
obj.m_tags[0] = __686(1);
return true;
}
break;
}
case 'P':
case 'p':
{
Player player = G.m_game.__279(asset);
if ( player )
{
player.m_tags[0] = __686(1);
return true;
}
break;
}
case 'D':
case 'd':
{
Dialog dialog = G.m_game.__278(asset);
if ( dialog )
{
dialog.m_tags[0] = __686(1);
return true;
}
break;
}
}
return false;
}
public bool __957()
{
string asset = __686(0);
if ( asset.Length==0 )
return false;
Variable var = __691(1);
if ( var==null )
return false;
switch ( asset[0] )
{
case 'S':
case 's':
{
Scene scene = G.m_game.__274(asset);
if ( scene )
{
var.m_value = scene.m_tags[0];
return true;
}
break;
}
case 'O':
case 'o':
{
Obj obj = G.m_game.__277(asset);
if ( obj )
{
var.m_value = obj.m_tags[0];
return true;
}
break;
}
case 'P':
case 'p':
{
Player player = G.m_game.__279(asset);
if ( player )
{
var.m_value = player.m_tags[0];
return true;
}
break;
}
case 'D':
case 'd':
{
Dialog dialog = G.m_game.__278(asset);
if ( dialog )
{
var.m_value = dialog.m_tags[0];
return true;
}
break;
}
}
return false;
}
public bool __958()
{
string asset = __686(0);
if ( asset.Length==0 )
return false;
switch ( asset[0] )
{
case 'S':
case 's':
{
Scene scene = G.m_game.__274(asset);
if ( scene )
{
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( G.__149(scene.m_tags, __692(i)) )
return true;
}
}
break;
}
case 'O':
case 'o':
{
Obj obj = G.m_game.__277(asset);
if ( obj )
{
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( G.__149(obj.m_tags, __692(i)) )
return true;
}
}
break;
}
case 'P':
case 'p':
{
Player player = G.m_game.__279(asset);
if ( player )
{
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( G.__149(player.m_tags, __692(i)) )
return true;
}
}
break;
}
case 'D':
case 'd':
{
Dialog dialog = G.m_game.__278(asset);
if ( dialog )
{
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( G.__149(dialog.m_tags, __692(i)) )
return true;
}
}
break;
}
}
return false;
}
public bool __959()
{
G.m_game.__244();
return true;
}
public bool __960()
{
return G.m_game.__245(__686(0), __686(1));
}
public bool __961()
{
string value = G.m_game.__246(__686(1));
Variable var = __691(0);
if ( var )
var.m_value = value;
return true;
}
public bool __962()
{
if ( G.__190(0)==false )
return false;
return G.m_game.__47(0);
}
public bool __963()
{
if ( G.m_game.m_configDebug )
return false;
G.m_game.m_autoSaveAsap = true;
return true;
}
public bool __964()
{
return G.m_game.__47(__688(0), true);
}
public bool __965()
{
if ( G.m_game.m_menuScene==null )
return false;
G.m_game.__240();
G.m_game.__241();
return true;
}
public bool __966()
{
if ( G.m_game.m_menuScene==null )
return false;
int index = __688(0);
if ( index<0 || index>4 )
return false;
if ( G.__190(index)==false )
return false;
return G.m_game.__47(index);
}
public bool __967()
{
if ( __968()==false )
return false;
int index = __688(0);
if ( index<0 || index>4 )
return false;
G.m_game.__46(index);
return true;
}
public bool __968()
{
return G.m_game.m_configDebug==false && G.m_game.m_menuScene!=null && G.m_game.m_customMenuSaveDisabled==false;
}
public bool __969()
{
AgePlugin.OnAppQuit();
return true;
}
public bool __970()
{
G.OpenUrl(__686(0));
return true;
}
public bool __971()
{
#if UNITY_ANDROID
return true;
#else
return false;
#endif
}
public bool __972()
{
#if UNITY_IOS
return true;
#else
return false;
#endif
}
public bool __973()
{
#if UNITY_WEBGL
return true;
#else
return false;
#endif
}
public bool __974()
{
#if UNITY_STANDALONE_WIN
return true;
#else
return false;
#endif
}
public bool __975()
{
G.Vibrate();
return true;
}
public bool __976()
{
string text = __686(0);
for ( int i=1 ; i<m_paramCount ; i++ )
{
text += "\n";
text += __686(i);
}
G.__198(text);
return true;
}
public bool __977()
{
G.m_game.__251(__686(0));
return true;
}
public bool __978()
{
return G.m_game.m_configDebug;
}
public bool __979()
{
return AgePlugin.Callback(__686(0), __686(1));
}
public bool __980()
{
if ( m_script.__685()==false )
return false;
G.m_game.m_interludeAsap = true;
G.m_game.m_interludeParam = __686(0);
return true;
}
public bool __981()
{
G.m_game.m_interludeAsap = false;
G.m_game.m_interludeParam = "";
return true;
}
}
