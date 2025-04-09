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
public void __65(Asset asset, int count)
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
public int __690(bool enter = false, bool exit = false)
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
index = instruction.__690() ? instruction.m_firstGoto : instruction.m_secondGoto;
break;
case Instruction.COMMAND.IFNOT:
case Instruction.COMMAND.ELSEIFNOT:
case Instruction.COMMAND.WHILENOT:
index = instruction.__690() ? instruction.m_secondGoto : instruction.m_firstGoto;
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
return G.__114(G.__97(ref instruction.m_commandParam));
}
else
{
return instruction.__690() ? 1 : 0;
}
case Instruction.COMMAND.BREAK:
case Instruction.COMMAND.CONTINUE:
case Instruction.COMMAND.GOTO:
index = instruction.m_firstGoto;
break;
default:
instruction.__690();
index++;
break;
}
}
return 0;
}
public bool __691()
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
public bool __690()
{
switch ( m_function )
{
case 001: return __704();
case 002: return __983();
case 003: return __959();
case 004: return __978();
case 005: return __787();
case 006: return __788();
case 007: return __965();
case 008: return __953();
case 009: return __729();
case 010: return __730();
case 011: return __771();
case 012: return __986();
case 013: return __988();
case 014: return __720();
case 015: return __784();
case 016: return __894();
case 017: return __710();
case 018: return __966();
case 019: return __799();
case 020: return __903();
case 021: return __785();
case 022: return __985();
case 023: return __791();
case 024: return __849();
case 025: return __881();
case 026: return __821();
case 027: return __827();
case 028: return __835();
case 029: return __919();
case 030: return __851();
case 031: return __921();
case 032: return __707();
case 033: return __816();
case 034: return __783();
case 035: return __915();
case 036: return __916();
case 037: return __957();
case 038: return __909();
case 039: return __848();
case 040: return __880();
case 041: return __820();
case 042: return __826();
case 043: return __834();
case 044: return __918();
case 045: return __850();
case 046: return __920();
case 047: return __758();
case 048: return __763();
case 049: return __724();
case 050: return __764();
case 051: return __765();
case 052: return __712();
case 053: return __716();
case 054: return __715();
case 055: return __717();
case 056: return __714();
case 057: return __789();
case 058: return __718();
case 059: return __713();
case 060: return __922();
case 061: return __923();
case 062: return __873();
case 063: return __964();
case 064: return __935();
case 065: return __962();
case 066: return __815();
case 067: return __824();
case 068: return __877();
case 069: return __901();
case 070: return __899();
case 071: return __866();
case 072: return __810();
case 073: return __863();
case 074: return __961();
case 075: return __879();
case 076: return __727();
case 077: return __728();
case 078: return __917();
case 079: return __853();
case 080: return __944();
case 081: return __838();
case 082: return __896();
case 083: return __938();
case 084: return __818();
case 085: return __831();
case 086: return __806();
case 087: return __885();
case 088: return __946();
case 089: return __813();
case 090: return __950();
case 091: return __772();
case 092: return __987();
case 093: return __979();
case 094: return __802();
case 095: return __803();
case 096: return __947();
case 097: return __933();
case 098: return __842();
case 099: return __825();
case 100: return __722();
case 101: return __711();
case 102: return __725();
case 103: return __726();
case 104: return __734();
case 105: return __731();
case 106: return __735();
case 107: return __738();
case 108: return __736();
case 109: return __732();
case 110: return __733();
case 111: return __739();
case 112: return __737();
case 113: return __969();
case 114: return __971();
case 115: return __973();
case 116: return __928();
case 117: return __941();
case 118: return __931();
case 119: return __951();
case 120: return __939();
case 121: return __930();
case 122: return __984();
case 123: return __792();
case 124: return __793();
case 125: return __741();
case 126: return __747();
case 127: return __743();
case 128: return __740();
case 129: return __744();
case 130: return __746();
case 131: return __749();
case 132: return __750();
case 133: return __748();
case 134: return __745();
case 135: return __751();
case 136: return __742();
case 137: return __708();
case 138: return __934();
case 139: return __776();
case 140: return __777();
case 141: return __779();
case 142: return __778();
case 143: return __706();
case 144: return __972();
case 145: return __801();
case 146: return __977();
case 147: return __762();
case 148: return __797();
case 149: return __924();
case 150: return __907();
case 151: return __757();
case 152: return __780();
case 153: return __782();
case 154: return __781();
case 155: return __709();
case 156: return __775();
case 157: return __976();
case 158: return __723();
case 159: return __968();
case 160: return __913();
case 161: return __833();
case 162: return __839();
case 163: return __840();
case 164: return __753();
case 165: return __754();
case 166: return __883();
case 167: return __798();
case 168: return __841();
case 169: return __794();
case 170: return __719();
case 171: return __893();
case 172: return __786();
case 173: return __970();
case 174: return __974();
case 175: return __975();
case 176: return __800();
case 177: return __902();
case 178: return __702();
case 179: return __804();
case 180: return __846();
case 181: return __872();
case 182: return __963();
case 183: return __955();
case 184: return __836();
case 185: return __767();
case 186: return __774();
case 187: return __843();
case 188: return __936();
case 189: return __856();
case 190: return __857();
case 191: return __859();
case 192: return __858();
case 193: return __814();
case 194: return __819();
case 195: return __960();
case 196: return __868();
case 197: return __770();
case 198: return __860();
case 199: return __869();
case 200: return __845();
case 201: return __829();
case 202: return __822();
case 203: return __823();
case 204: return __832();
case 205: return __828();
case 206: return __807();
case 207: return __886();
case 208: return __888();
case 209: return __887();
case 210: return __889();
case 211: return __890();
case 212: return __871();
case 213: return __855();
case 214: return __870();
case 215: return __874();
case 216: return __861();
case 217: return __906();
case 218: return __905();
case 219: return __908();
case 220: return __876();
case 221: return __808();
case 222: return __898();
case 223: return __900();
case 224: return __768();
case 225: return __927();
case 226: return __865();
case 227: return __809();
case 228: return __862();
case 229: return __956();
case 230: return __847();
case 231: return __844();
case 232: return __897();
case 233: return __769();
case 234: return __875();
case 235: return __878();
case 236: return __852();
case 237: return __943();
case 238: return __837();
case 239: return __895();
case 240: return __948();
case 241: return __937();
case 242: return __817();
case 243: return __830();
case 244: return __805();
case 245: return __884();
case 246: return __945();
case 247: return __812();
case 248: return __949();
case 249: return __891();
case 250: return __914();
case 251: return __912();
case 252: return __926();
case 253: return __795();
case 254: return __760();
case 255: return __756();
case 256: return __867();
case 257: return __811();
case 258: return __773();
case 259: return __796();
case 260: return __761();
case 261: return __925();
case 262: return __882();
case 263: return __705();
case 264: return __721();
case 265: return __752();
case 266: return __904();
case 267: return __958();
case 268: return __864();
case 269: return __911();
case 270: return __892();
case 271: return __766();
case 272: return __954();
case 273: return __910();
case 274: return __790();
case 275: return __929();
case 276: return __942();
case 277: return __932();
case 278: return __952();
case 279: return __940();
case 280: return __755();
case 281: return __759();
case 282: return __703();
case 283: return __982();
case 284: return __854();
case 285: return __980();
case 286: return __981();
case 287: return __967();
}
return true;
}
public string __692(int index)
{
if ( index<0 || index>=m_paramCount )
return "";
string item = m_params[index];
return G.__97(ref item);
}
public bool __693(int index)
{
return G.__113(__692(index));
}
public int __694(int index)
{
return G.__114(__692(index));
}
public float __695(int index)
{
return G.__115(__692(index));
}
public float __696(int index)
{
return G.__114(__692(index))/1000.0f;
}
public Variable __697(int index)
{
string name = __692(index);
return G.m_game.__288(ref name);
}
public string __698(int index)
{
if ( index<0 || index>=m_paramCount )
return "";
string item = m_params[index];
item = G.__97(ref item);
if ( item.Length==0 || item[0]!='@' )
return "";
return item.Substring(1);
}
public Scene __291(int index)
{
string uid = __692(index);
if ( uid.Length==0 )
return G.m_game.__291();
return G.m_game.__274(uid);
}
public SceneObj __699(int index)
{
Scene scene = null;
SceneObj sceneObj = null;
if ( __701(__692(index), ref scene, ref sceneObj)==false )
return null;
return sceneObj;
}
public Dialog __700(int index)
{
string uid = __692(index);
if ( uid.Length==0 )
return null;
return G.m_game.__278(uid);
}
public bool __701(string uid, ref Scene scene, ref SceneObj sceneObj)
{
string uidScene = "";
string uidObj = "";
G.__98(uid, ref uidScene, ref uidObj);
scene = uidScene.Length==0 ? G.m_game.__291() : G.m_game.__274(uidScene);
if ( scene==null )
return false;
sceneObj = scene.__277(uidObj);
if ( sceneObj==null )
return false;
return true;
}
public bool __702()
{
Variable var = __697(0);
if ( var==null )
return false;
string value = __692(1);
for ( int i=2 ; i<m_paramCount ; i++ )
value += __692(i);
var.m_value = value;
return true;
}
public bool __703()
{
Variable var = __697(0);
if ( var==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( __692(i)==var.m_value )
return true;
}
return false;
}
public bool __704()
{
Variable var = __697(0);
if ( var==null )
return false;
var.m_value = (G.__114(ref var.m_value) + __694(1)).ToString();
return true;
}
public bool __705()
{
Variable var = __697(0);
if ( var==null )
return false;
var.m_value = (G.__114(ref var.m_value) - __694(1)).ToString();
return true;
}
public bool __706()
{
Variable var = __697(0);
if ( var==null )
return false;
var.m_value = (G.__114(ref var.m_value) * __694(1)).ToString();
return true;
}
public bool __707()
{
Variable var = __697(0);
if ( var==null )
return false;
int divider = __694(1);
var.m_value = divider==0 ? "0" : (G.__114(ref var.m_value)/divider).ToString();
return true;
}
public bool __708()
{
Variable var = __697(0);
if ( var==null )
return false;
int divider = __694(1);
if ( divider<=0 )
var.m_value = "0";
else
{
int value = G.__114(ref var.m_value) % divider;
var.m_value = value.ToString();
}
return true;
}
public bool __709()
{
Variable var = __697(0);
if ( var==null )
return false;
int power = __694(1);
int result = (int)Mathf.Pow((float)G.__114(var.m_value), (float)power);
var.m_value = result.ToString();
return true;
}
public bool __710()
{
Variable var = __697(0);
if ( var==null )
return false;
int val = G.__114(var.m_value);
int min = __694(1);
int max = __694(2);
if ( val<min )
var.m_value = min.ToString();
else if ( val>max )
var.m_value = max.ToString();
return true;
}
public bool __711()
{
Variable var = __697(0);
if ( var==null )
return false;
float ratio = G.Clamp(__695(1));
float min = (float)__694(2);
float max = (float)__694(3);
string ease = __692(4);
bool easeIn = G.__148(ref ease, "IN");
bool easeOut = G.__148(ref ease, "OUT");
float result = G.__138(ratio, min, max, easeIn, easeOut);
var.m_value = ((int)result).ToString();
return true;
}
public bool __712()
{
Variable var = __697(0);
if ( var==null )
return false;
float res = G.__115(ref var.m_value) + __695(1);
G.__112(ref var.m_value, res);
return true;
}
public bool __713()
{
Variable var = __697(0);
if ( var==null )
return false;
float res = G.__115(ref var.m_value) - __695(1);
G.__112(ref var.m_value, res);
return true;
}
public bool __714()
{
Variable var = __697(0);
if ( var==null )
return false;
float res = G.__115(ref var.m_value) * __695(1);
G.__112(ref var.m_value, res);
return true;
}
public bool __715()
{
Variable var = __697(0);
if ( var==null )
return false;
float divider = __695(1);
if ( G.__129(divider) )
var.m_value = "0";
else
{
float res = G.__115(ref var.m_value)/divider;
G.__112(ref var.m_value, res);
}
return true;
}
public bool __716()
{
Variable var = __697(0);
if ( var==null )
return false;
float val = G.__115(var.m_value);
float min = m_paramCount<2 ? 0.0f : __695(1);
float max = m_paramCount<3 ? 1.0f : __695(2);
if ( val<min )
G.__112(ref var.m_value, min);
else if ( val>max )
G.__112(ref var.m_value, max);
return true;
}
public bool __717()
{
Variable var = __697(0);
if ( var==null )
return false;
float ratio = G.Clamp(__695(1));
float min = m_paramCount<3 ? 0.0f : __695(2);
float max = m_paramCount<4 ? 1.0f : __695(3);
string ease = __692(4);
bool easeIn = G.__148(ref ease, "IN");
bool easeOut = G.__148(ref ease, "OUT");
float result = G.__138(ratio, min, max, easeIn, easeOut);
G.__112(ref var.m_value, result);
return true;
}
public bool __718()
{
Variable var = __697(0);
if ( var==null )
return false;
float result = Mathf.Sqrt(G.__115(var.m_value));
G.__112(ref var.m_value, result);
return true;
}
public bool __719()
{
Variable var = __697(0);
if ( var==null )
return false;
var.m_value = Mathf.RoundToInt(G.__115(ref var.m_value)).ToString();
return true;
}
public bool __720()
{
Variable var = __697(0);
if ( var==null )
return false;
var.m_value += __692(1);
return true;
}
public bool __721()
{
Variable var = __697(0);
if ( var==null )
return false;
int index = __694(1);
if ( index<0 )
index = 0;
else if ( index>var.m_value.Length )
index = var.m_value.Length;
int count = m_paramCount<3 ? -1 : __694(2);
if ( count==0 )
var.m_value = "";
else if ( count<0 || index+count>var.m_value.Length )
var.m_value = var.m_value.Substring(index);
else
var.m_value = var.m_value.Substring(index, count);
return true;
}
public bool __722()
{
Variable var = __697(0);
if ( var==null )
return false;
var.m_value = __692(1).Length.ToString();
return true;
}
public bool __723()
{
Variable var = __697(0);
if ( var==null )
return false;
var.m_value = G.__156(__694(1), __694(2)).ToString();
return true;
}
public bool __724()
{
string value = __692(0);
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( __692(i)==value )
return true;
}
return false;
}
public bool __725()
{
int value = __694(0);
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( value<__694(i) )
return true;
}
return false;
}
public bool __726()
{
int value = __694(0);
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( value<=__694(i) )
return true;
}
return false;
}
public bool __727()
{
int value = __694(0);
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( value>__694(i) )
return true;
}
return false;
}
public bool __728()
{
int value = __694(0);
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( value>=__694(i) )
return true;
}
return false;
}
public bool __729()
{
Variable var = __697(0);
if ( var==null )
return false;
var.m_value = (G.__114(ref var.m_value) | __694(1)).ToString();
return true;
}
public bool __730()
{
Variable var = __697(0);
if ( var==null )
return false;
int a = G.__114(ref var.m_value);
int b = __694(1);
a ^= a & b;
var.m_value = a.ToString();
return true;
}
public bool __731()
{
Variable var = __697(0);
if ( var==null )
return false;
int count = 1;
foreach ( char c in var.m_value )
{
if ( c==',' )
count++;
}
var = __697(1);
if ( var==null )
return false;
var.m_value = count.ToString();
return true;
}
public bool __732()
{
Variable var = __697(0);
if ( var==null )
return false;
List<string> items = var.ToList();
List<string> list = new List<string>();
while ( items.Count>0 )
{
int index = G.__156(items.Count);
list.Add(items[index]);
items.RemoveAt(index);
}
var.m_value = String.Join(",", list);
return true;
}
public bool __733()
{
Variable var = __697(0);
if ( var==null )
return false;
string[] items = var.ToArray();
int index = m_paramCount<3 ? -1 : __694(2);
if ( index<0 )
{
string value = __692(1);
for ( int i=items.Length ; i<index ; i++ )
items[i] = value;
var.m_value = String.Join(",", items);
return true;
}
if ( index>=items.Length )
{
for ( int i=items.Length ; i<index ; i++ )
var.m_value += ",";
var.m_value += ",";
var.m_value += __692(1);
return true;
}
items[index] = __692(1);
var.m_value = String.Join(",", items);
return true;
}
public bool __734()
{
Variable var = __697(0);
if ( var==null )
return false;
int index = m_paramCount<3 ? -1 : __694(2);
if ( index<0 )
{
if ( var.m_value.Length>0 )
var.m_value += ",";
var.m_value += __692(1);
return true;
}
List<string> items = var.ToList();
if ( index>=items.Count )
{
for ( int i=items.Count ; i<index ; i++ )
var.m_value += ",";
var.m_value += ",";
var.m_value += __692(1);
return true;
}
items.Insert(index, __692(1));
var.m_value = String.Join(",", items);
return true;
}
public bool __735()
{
Variable var = __697(0);
if ( var==null )
return false;
List<string> items = var.ToList();
int index = __694(1);
if ( index<0 || index>=items.Count )
return false;
items.RemoveAt(index);
var.m_value = String.Join(",", items);
return true;
}
public bool __736()
{
Variable list = __697(0);
Variable var = __697(2);
if ( list==null || var==null )
return false;
string[] items = list.ToArray();
int index = __694(1);
if ( index<0 || index>=items.Length )
return false;
var.m_value = items[index];
return true;
}
public bool __737()
{
Variable var = __697(0);
if ( var==null )
return false;
string[] items = var.ToArray();
for ( int i=1 ; i<m_paramCount ; i++ )
{
string value = __692(i);
for ( int j=0 ; j<items.Length ; j++ )
{
if ( items[j]==value )
return true;
}
}
return false;
}
public bool __738()
{
Variable list = __697(0);
Variable res = __697(2);
if ( list==null || res==null )
return false;
string[] items = list.ToArray();
string value = __692(1);
for ( int i=0 ; i<items.Length ; i++ )
{
if ( items[i]==value )
{
res.m_value = i.ToString();
return true;
}
}
res.m_value = "-1";
return false;
}
public bool __739()
{
Variable list = __697(0);
if ( list==null )
return false;
List<string> items = list.ToList();
items.Sort();
list.m_value = String.Join(",", items);
return false;
}
public bool __740()
{
Variable var = __697(1);
if ( var==null )
return false;
float angle = G.__142(__694(0));
var.m_value = Mathf.Cos(angle).ToString();
return true;
}
public bool __741()
{
Variable var = __697(1);
if ( var==null )
return false;
int angle = G.__142(Mathf.Acos(__695(0)));
var.m_value = angle.ToString();
return true;
}
public bool __742()
{
Variable var = __697(1);
if ( var==null )
return false;
float angle = G.__142(__694(0));
var.m_value = Mathf.Sin(angle).ToString();
return true;
}
public bool __743()
{
Variable var = __697(1);
if ( var==null )
return false;
int angle = G.__142(Mathf.Asin(__695(0)));
var.m_value = angle.ToString();
return true;
}
public bool __744()
{
Variable var = __697(4);
if ( var==null )
return false;
int x = __694(0)-__694(2);
int y = __694(1)-__694(3);
int dist = Mathf.RoundToInt(Mathf.Sqrt(x*x + y*y));
var.m_value = dist.ToString();
return true;
}
public bool __745()
{
Variable varX = __697(5);
Variable varY = __697(6);
if ( varX==null || varY==null )
return false;
float ratio = G.Clamp(__695(0));
int ax = __694(1);
int ay = __694(2);
int bx = __694(3);
int by = __694(4);
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
float len = dir.__434();
if ( dir.__435(len)==false )
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
public bool __746()
{
Variable varX = __697(4);
Variable varY = __697(5);
if ( varX==null || varY==null )
return false;
int x = __694(0);
int y = __694(1);
int angle = __694(2);
int dist = __694(3);
float angle2 = G.__142(angle);
varX.m_value = Mathf.RoundToInt(x+Mathf.Sin(angle2)*dist).ToString();
varY.m_value = Mathf.RoundToInt(y+Mathf.Cos(angle2)*dist).ToString();
return true;
}
public bool __747()
{
Variable var = __697(4);
if ( var==null )
return false;
int ax = __694(0);
int ay = __694(1);
int bx = __694(2);
int by = __694(3);
float angle = G.__141(ax, ay, bx, by);
var.m_value = G.__142(angle).ToString();
return true;
}
public bool __748()
{
int px = __694(0);
int py = __694(1);
int left = __694(2);
int top = __694(3);
int right = left + __694(4);
int bottom = top + __694(5);
return px>=left && px<right && py>=top && py<bottom;
}
public bool __749()
{
Variable var = __697(0);
if ( var==null )
return false;
int angle = G.__114(var.m_value);
int angleNorm = G.__144(angle, __693(1));
if ( angle!=angleNorm )
var.m_value = angleNorm.ToString();
return true;
}
public bool __750()
{
int x = __694(0) - __694(2);
int y = __694(1) - __694(3);
int radius = __694(4);
int value = x*x + y*y - radius*radius;
if ( value==0 )
return true;
return value<0;
}
public bool __751()
{
int l1 = __694(0);
int t1 = __694(1);
int r1 = l1 + __694(2);
int b1 = t1 + __694(3);
int l2 = __694(4);
int t2 = __694(5);
int r2 = l2 + __694(6);
int b2 = t2 + __694(7);
if ( l1<r2 && r1>l2 && t1<b2 && b1>t2 )
return true;
return false;
}
public bool __752()
{
if ( G.m_game.m_menuScene )
return false;
G.Success(__694(0));
return true;
}
public bool __753()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.m_game.m_scenario.__522(__694(i)) )
return true;
}
return false;
}
public bool __754()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.m_game.m_scenario.__524(__694(i)) )
return true;
}
return false;
}
public bool __755()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.m_game.m_scenario.__523(__694(i)) )
return true;
}
return false;
}
public bool __756()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.m_game.m_scenario.__525(__694(i)) )
return true;
}
return false;
}
public bool __757()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.m_game.m_scenario.__528(__694(i)) )
return true;
}
return false;
}
public bool __758()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.m_game.m_scenario.__527(__694(i)) )
return true;
}
return false;
}
public bool __759()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.m_game.m_scenario.__526(__694(i)) )
return true;
}
return false;
}
public bool __760()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
Role role = G.m_game.__283(__692(i));
if ( role )
role.Start();
}
return true;
}
public bool __761()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
Role role = G.m_game.__283(__692(i));
if ( role )
role.Stop();
}
return true;
}
public bool __762()
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
short value = (short)__694(i);
if ( m_script.m_customOuts.Contains(value)==false )
m_script.m_customOuts.Add(value);
}
}
return true;
}
public bool __763()
{
return m_script.m_enterNotified;
}
public bool __764()
{
return m_script.m_enterNotified==false && m_script.m_exitNotified==false;
}
public bool __765()
{
return m_script.m_exitNotified;
}
public bool __766()
{
if ( m_script.__691()==false )
return false;
return G.m_game.Task(__692(0), __692(1), __692(2));
}
public bool __767()
{
G.m_game.m_brightness = G.Clamp(__694(0)/100.0f);
return true;
}
public bool __768()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
string p0 = __692(0);
string p1 = __692(1);
string p2 = __692(2);
string p3 = __692(3);
scene.m_shakes[0] = G.__114(ref p0)*0.5f;
scene.m_shakes[1] = p1.Length==0 ? 1.0f : G.__114(ref p1)/100.0f;
scene.m_shakes[2] = p2.Length==0 ? scene.m_shakes[0] : G.__114(ref p2)*0.5f;
scene.m_shakes[3] = p3.Length==0 ? scene.m_shakes[1] : G.__114(ref p3)/100.0f;
return true;
}
public bool __769()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
string p0 = __692(0);
string p1 = __692(1);
string p2 = __692(2);
string p3 = __692(3);
scene.m_waves[0] = G.__114(ref p0)*0.5f;
scene.m_waves[1] = p1.Length==0 ? 1.0f : G.__114(ref p1)/100.0f;
scene.m_waves[2] = G.__114(ref p2)*0.5f;
scene.m_waves[3] = p3.Length==0 ? 1.0f : G.__114(ref p3)/100.0f;
return true;
}
public bool __770()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
if ( __694(0)==0 )
{
scene.__573(CAMERA.CURSOR);
}
else
{
float renderScale = scene.m_renderScale;
Cam cam = scene.__572(CAMERA.CURSOR);
cam.m_scale = renderScale;
}
return true;
}
public bool __771()
{
return G.m_game.m_lastEventByUser;
}
public bool __772()
{
string valueObj = "";
string valueSub = "";
string valueLabel = "";
string valueDoor = "";
bool found = false;
if ( G.m_game.m_cursor )
{
Scene scene = G.m_game.__291();
if ( scene )
{
SceneEntity entity;
SubObj sub;
scene.__426(out entity, out sub, G.m_game.m_cursorViewX, G.m_game.m_cursorViewY, true, DRAG.ANY);
if ( entity )
{
found = true;
if ( entity.__606() )
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
if ( found==false )
{
bool abort = false;
SceneDoor door = scene.__546(ref abort, G.m_game.m_cursorViewX, G.m_game.m_cursorViewY);
if ( door )
{
found = true;
valueDoor = door.m_name;
}
}
}
}
Variable var = __697(0);
if ( var )
var.m_value = valueObj;
var = __697(1);
if ( var )
var.m_value = valueSub;
var = __697(2);
if ( var )
var.m_value = valueLabel;
var = __697(3);
if ( var )
var.m_value = valueDoor;
return found;
}
public bool __773()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
for ( int i=0 ; i<m_paramCount ; i++ )
{
SceneObj sceneObj = scene.__277(__692(i));
if ( sceneObj )
sceneObj.__651();
}
return true;
}
public bool __774()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__692(0));
SceneObj sceneObjTrg = scene.__277(__692(1));
if ( sceneObj==null )
return false;
if ( sceneObj.m_chaseObj )
sceneObj.Stop();
sceneObj.m_chaseObj = sceneObjTrg;
sceneObj.m_chaseObjDeltaCol = __694(2);
sceneObj.m_chaseObjDeltaRow = __694(3);
return true;
}
public bool __775()
{
SceneObj sceneObj = G.m_game.__307(__692(0));
if ( sceneObj==null )
return false;
int col = __694(1);
int row = __694(2);
sceneObj.Move(sceneObj.__35()+col*G.PATH_GRID_CELLSIZE, sceneObj.__36()+row*G.PATH_GRID_CELLSIZE);
sceneObj.__662();
G.m_game.m_input.__369();
return true;
}
public bool __776()
{
SceneObj sceneObj = G.m_game.__307(__692(0));
if ( sceneObj==null )
return false;
SceneCell cell = sceneObj.__637(__692(1));
if ( cell==null )
return false;
sceneObj.Move(cell.__35(), cell.__36());
sceneObj.__662();
G.m_game.m_input.__369();
return true;
}
public bool __777()
{
SceneObj sceneObj = G.m_game.__307(__692(0));
if ( sceneObj==null )
return false;
float x = G.__147(G.__146(__694(1)));
float y = G.__147(G.__146(__694(2)));
sceneObj.Move(x, y);
sceneObj.__662();
G.m_game.m_input.__369();
return true;
}
public bool __778()
{
SceneObj sceneObj = G.m_game.__307(__692(0));
if ( sceneObj==null )
return false;
sceneObj.Move((float)__694(1), (float)__694(2));
sceneObj.__662();
G.m_game.m_input.__369();
return true;
}
public bool __779()
{
SceneObj sceneObj = G.m_game.__307(__692(0));
if ( sceneObj==null )
return false;
sceneObj.__630((float)__694(1), (float)__694(2));
sceneObj.__662();
G.m_game.m_input.__369();
return true;
}
public bool __780()
{
SceneObj sceneObj = G.m_game.__307(__692(0));
if ( sceneObj==null )
return false;
for ( int i=0 ; i<2 ; i++ )
{
Variable var = __697(i+1);
if ( var==null )
continue;
var.m_value = i==0 ? sceneObj.__626().ToString() : sceneObj.__627().ToString();
}
return true;
}
public bool __781()
{
SceneObj sceneObj = G.m_game.__307(__692(0));
if ( sceneObj==null )
return false;
for ( int i=0 ; i<2 ; i++ )
{
Variable var = __697(i+1);
if ( var==null )
continue;
var.m_value = i==0 ? ((int)sceneObj.__35()).ToString() : ((int)sceneObj.__36()).ToString();
}
return true;
}
public bool __782()
{
SceneObj sceneObj = G.m_game.__307(__692(0));
if ( sceneObj==null )
return false;
for ( int i=0 ; i<2 ; i++ )
{
Variable var = __697(i+1);
if ( var==null )
continue;
var.m_value = i==0 ? ((int)sceneObj.__611()).ToString() : ((int)sceneObj.__612()).ToString();
}
return true;
}
public bool __783()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
if ( G.m_game.m_dragging==false )
return false;
for ( int i=0 ; i<2 ; i++ )
{
Variable var = __697(i);
if ( var==null )
continue;
var.m_value = i==0 ? ((int)G.m_game.m_dropPos.x).ToString() : ((int)G.m_game.m_dropPos.y).ToString();
}
return true;
}
public bool __784()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__692(0));
if ( sceneObj==null || sceneObj.m_visible.cur==false )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneCell cell = sceneObj.__637(__692(i));
if ( cell && sceneObj.__626()==cell.m_col && sceneObj.__627()==cell.m_row )
return true;
}
return false;
}
public bool __785()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__692(0));
if ( sceneObj==null || sceneObj.m_visible.cur==false )
return false;
SceneCell cell = sceneObj.__635(sceneObj.__35(), sceneObj.__36());
if ( cell==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( cell.__597()==G.__114(__692(i)) )
return true;
}
return false;
}
public bool __786()
{
int tolerance = __694(2);
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__692(0));
if ( sceneObj==null || sceneObj.m_visible.cur==false )
return false;
SceneObj sceneObj2 = scene.__277(__692(1));
if ( sceneObj2==null || sceneObj2.m_visible.cur==false )
return false;
if ( Math.Abs(sceneObj.__626()-sceneObj2.__626())>tolerance )
return false;
if ( Math.Abs(sceneObj.__627()-sceneObj2.__627())>tolerance )
return false;
return true;
}
public bool __787()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__692(0));
if ( sceneObj==null || sceneObj.m_message )
return false;
Anim anim = sceneObj.m_obj.__471(__692(1));
if ( anim )
sceneObj.__632(anim);
sceneObj.m_anim.__678(G.__151(__692(2)));
return true;
}
public bool __788()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__692(0));
if ( sceneObj==null )
return false;
Anim anim = sceneObj.m_anim.cur;
if ( anim==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( G.__148(ref anim.m_name, __692(i)) )
return true;
}
return false;
}
public bool __789()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__692(0));
if ( sceneObj==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( sceneObj.m_anim.__685(__694(i)) )
return true;
}
return false;
}
public bool __790()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__692(0));
if ( sceneObj==null || sceneObj.m_message )
return false;
int index = G.__151(__692(1));
if ( index==-1 )
index = AnimDir.RIGHT;
sceneObj.m_anim.__678(index);
return true;
}
public bool __791()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__692(0));
if ( sceneObj==null )
return false;
AnimDir dir = sceneObj.m_anim.__679();
if ( dir==null )
return false;
string name = dir.__393();
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( G.__148(ref name, __692(i)) )
return true;
}
return false;
}
public bool __792()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__692(0));
if ( sceneObj==null || sceneObj.m_message )
return false;
SceneObj sceneObjTrg = scene.__277(__692(1));
if ( sceneObjTrg==null )
return false;
sceneObj.__633(sceneObjTrg);
return true;
}
public bool __793()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__692(0));
if ( sceneObj==null )
return false;
SceneObj sceneObjTrg = scene.__277(__692(1));
if ( sceneObjTrg==null )
return false;
int dir = -1;
if ( m_paramCount>=3 )
{
string p2 = __692(2);
if ( G.__148(ref p2, "LEFT") )
dir = AnimDir.LEFT;
else if ( G.__148(ref p2, "RIGHT") )
dir = AnimDir.RIGHT;
}
return sceneObj.__634(sceneObjTrg, dir);
}
public bool __794()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__692(0));
if ( sceneObj==null )
return false;
sceneObj.Rotate(__694(1));
return true;
}
public bool __795()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__692(0));
if ( sceneObj==null )
return false;
string p1 = __692(1);
int loop = p1.Length==0 ? -2 : G.__114(ref p1);
return sceneObj.__652(-1, "", loop);
}
public bool __796()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__692(0));
if ( sceneObj==null )
return false;
sceneObj.__653();
return true;
}
public bool __797()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__692(0));
if ( sceneObj==null || sceneObj.m_paths==null || sceneObj.m_paths[sceneObj.m_iPath.cur]==null )
return false;
SpotPath path = sceneObj.m_paths[sceneObj.m_iPath.cur];
path.Pause();
return true;
}
public bool __798()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__692(0));
if ( sceneObj==null || sceneObj.m_paths==null || sceneObj.m_paths[sceneObj.m_iPath.cur]==null )
return false;
SpotPath path = sceneObj.m_paths[sceneObj.m_iPath.cur];
path.__672();
return true;
}
public bool __799()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(__692(0));
if ( sceneObj==null || sceneObj.m_paths==null || sceneObj.m_paths[sceneObj.m_iPath.cur]==null )
return false;
SpotPath path = sceneObj.m_paths[sceneObj.m_iPath.cur];
path.__673();
return true;
}
public bool __800()
{
if ( m_paramCount==0 )
return false;
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( scene.__48(__692(i)) )
return true;
}
return false;
}
public bool __801()
{
if ( m_paramCount==0 || G.m_game.m_iOldScene==-1 )
return false;
Scene scene = G.m_game.m_scenes[G.m_game.m_iOldScene];
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( scene.__48(__692(i)) )
return true;
}
return false;
}
public bool __802(string scene, float duration)
{
if ( m_script.__691() )
G.m_game.Jump(null, scene, duration, 0.0f, duration*0.5f);
else
{
if ( G.m_game.__274(scene)==null )
return false;
G.m_game.m_scenario.m_debugFirstScene = scene;
}
return true;
}
public bool __802()
{
return __802(__692(0), __696(1));
}
public bool __803()
{
if ( G.m_game.m_iOldScene==-1 )
return false;
string scene = G.m_game.m_scenes[G.m_game.m_iOldScene].m_uid;
return __802(scene, __696(0));
}
public bool __804()
{
Scene scene = __291(0);
if ( scene==null )
return false;
if ( m_paramCount<2 )
scene.m_lightAmbient.Set(-1.0f);
else
{
int ambient = __694(1);
scene.m_lightAmbient.Set(ambient==-1 ? -1.0f : G.Clamp(ambient/255.0f));
}
scene.__565();
return true;
}
public bool __805()
{
Scene scene = __291(0);
if ( scene==null )
return false;
return scene.__548(__692(1), true);
}
public bool __806()
{
Scene scene = __291(0);
if ( scene==null )
return false;
return scene.__548(__692(1), false);
}
public bool __807()
{
Scene scene = __291(0);
if ( scene==null )
return false;
string p2 = __692(2);
float opacity = p2.Length==0 ? 1.0f : G.Clamp(G.__114(ref p2)/100.0f);
switch ( __692(1).ToUpper() )
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
public bool __808()
{
Scene scene = __291(0);
if ( scene==null )
return false;
Effect effect = G.m_game.__282(__692(1));
if ( effect==null )
scene.m_effect.Reset();
else
scene.m_effect.Set(effect);
return true;
}
public bool __809()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneStill still = scene.__542(__692(1));
if ( still==null )
return false;
still.m_tags[0] = __692(2);
return true;
}
public bool __810()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneStill still = scene.__542(__692(1));
if ( still==null )
return false;
Variable var = __697(2);
if ( var==null )
return false;
var.m_value = still.m_tags[0];
return true;
}
public bool __811()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneStill still = scene.__542(__692(1));
if ( still==null )
return false;
for ( int i=2 ; i<m_paramCount ; i++ )
{
if ( G.__149(still.m_tags, __698(i)) )
return true;
}
return false;
}
public bool __812()
{
Scene scene = __291(0);
if ( scene==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneStill still = scene.__542(__692(i));
if ( still )
still.m_visible.Set(true);
}
return true;
}
public bool __813()
{
Scene scene = __291(0);
if ( scene==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneStill still = scene.__542(__692(i));
if ( still )
still.m_visible.Set(false);
}
return true;
}
public bool __814()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneDoor door = scene.__537(__692(1));
if ( door==null )
return false;
door.m_tags[0] = __692(2);
return true;
}
public bool __815()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneDoor door = scene.__537(__692(1));
if ( door==null )
return false;
Variable var = __697(2);
if ( var==null )
return false;
var.m_value = door.m_tags[0];
return true;
}
public bool __816()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneDoor door = scene.__537(__692(1));
if ( door==null )
return false;
for ( int i=2 ; i<m_paramCount ; i++ )
{
if ( G.__149(door.m_tags, __698(i)) )
return true;
}
return false;
}
public bool __817()
{
Scene scene = __291(0);
if ( scene==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneDoor door = scene.__537(__692(i));
if ( door )
door.m_visible.Set(true);
}
return true;
}
public bool __818()
{
Scene scene = __291(0);
if ( scene==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneDoor door = scene.__537(__692(i));
if ( door )
door.m_visible.Set(false);
}
return true;
}
public bool __819()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneDoor door = scene.__537(__692(1));
if ( door==null )
return false;
door.m_title.m_sub.Set(__694(2));
return true;
}
public bool __820()
{
Scene scene = __291(0);
if ( scene==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneDoor door = scene.__537(__692(i));
if ( door )
door.m_cheat.Set(true);
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
SceneDoor door = scene.__537(__692(i));
if ( door )
door.m_cheat.Set(false);
}
return true;
}
public bool __822()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneLabel label = scene.__538(__692(1));
if ( label==null )
return false;
label.m_parentName.Set(__692(2));
label.m_parent = scene.__536(ref label.m_parentName.cur);
return true;
}
public bool __823()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneLabel label = scene.__538(__692(1));
if ( label==null )
return false;
label.m_tags[0] = __692(2);
return true;
}
public bool __824()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneLabel label = scene.__538(__692(1));
if ( label==null )
return false;
Variable var = __697(2);
if ( var==null )
return false;
var.m_value = label.m_tags[0];
return true;
}
public bool __825()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneLabel label = scene.__538(__692(1));
if ( label==null )
return false;
for ( int i=2 ; i<m_paramCount ; i++ )
{
if ( G.__149(label.m_tags, __698(i)) )
return true;
}
return false;
}
public bool __826()
{
Scene scene = __291(0);
if ( scene==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneLabel label = scene.__538(__692(i));
if ( label )
label.m_enabled.Set(true);
}
return true;
}
public bool __827()
{
Scene scene = __291(0);
if ( scene==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneLabel label = scene.__538(__692(i));
if ( label )
label.m_enabled.Set(false);
}
return true;
}
public bool __828()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneLabel label = scene.__538(__692(1));
if ( label==null )
return false;
label.m_title.m_sub.Set(__694(2));
return true;
}
public bool __829()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneLabel label = scene.__538(__692(1));
if ( label==null )
return false;
Color defColor = G.m_game.m_colorText;
label.m_color = G.__162(__692(2).ToUpper(), (int)(defColor.r*255.0f), (int)(defColor.g*255.0f), (int)(defColor.b*255.0f));
return true;
}
public bool __830()
{
Scene scene = __291(0);
if ( scene==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneLabel label = scene.__538(__692(i));
if ( label )
label.m_visible.Set(true);
}
return true;
}
public bool __831()
{
Scene scene = __291(0);
if ( scene==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneLabel label = scene.__538(__692(i));
if ( label )
label.m_visible.Set(false);
}
return true;
}
public bool __832()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneLabel label = scene.__538(__692(1));
if ( label==null )
return false;
label.m_text.m_sub.Set(__694(2));
label.__622();
return true;
}
public bool __833()
{
Scene scene = __291(0);
if ( scene==null )
return false;
SceneLabel label = scene.__538(__692(1));
if ( label==null )
return false;
label.m_usertext.Set(__692(2));
label.__622();
return true;
}
public bool __834()
{
Scene scene = __291(0);
if ( scene==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneLabel label = scene.__538(__692(i));
if ( label )
label.m_cheat.Set(true);
}
return true;
}
public bool __835()
{
Scene scene = __291(0);
if ( scene==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneLabel label = scene.__538(__692(i));
if ( label )
label.m_cheat.Set(false);
}
return true;
}
public bool __836()
{
Scene scene = __291(0);
if ( scene==null )
return false;
scene.m_bokehSize.Set(G.__119(__692(1)));
scene.m_bokehMaxZoom.Set(G.__121(__692(2)));
return true;
}
public bool __837()
{
Scene scene = __291(0);
if ( scene==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneBokeh bokeh = scene.__541(__692(i));
if ( bokeh )
bokeh.m_visible.Set(true);
}
return true;
}
public bool __838()
{
Scene scene = __291(0);
if ( scene==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
SceneBokeh bokeh = scene.__541(__692(i));
if ( bokeh )
bokeh.m_visible.Set(false);
}
return true;
}
public bool __839()
{
Scene scene = G.m_game.__291();
for ( int i=0 ; i<m_paramCount ; i++ )
{
string uid = __692(i);
Obj obj = G.m_game.__277(uid);
if ( obj )
{
obj.Reset(true);
if ( scene )
{
SceneObj sceneObj = scene.__277(obj);
if ( sceneObj )
{
sceneObj.__651();
sceneObj.Reset(true);
G.m_game.__306(scene.m_uid, sceneObj.m_obj.m_uid, true);
}
}
}
}
return true;
}
public bool __840()
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
sceneObj.__651();
sceneObj.Reset(true);
G.m_game.__306(scene.m_uid, sceneObj.m_obj.m_uid, true);
}
}
return true;
}
public bool __841()
{
Asset asset = G.__96(G.m_pathGraphics);
if ( asset==null )
return false;
Scene scene = G.m_game.__291();
for ( int i=0 ; i<m_paramCount ; i++ )
{
string uid = __692(i);
Obj obj = G.m_game.__277(uid);
if ( obj && obj.m_killed.cur )
{
obj.m_killed.Set(false);
if ( scene )
{
SceneObj sceneObj = scene.__277(uid);
if ( sceneObj )
obj.__469(asset, scene);
}
}
}
asset.Close();
return true;
}
public bool __842()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
string uid = __692(i);
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
public bool __843()
{
Obj obj = G.m_game.__277(__692(0));
if ( obj==null )
return false;
Color defColor = G.m_game.m_colorText;
obj.m_speech = G.__162(__692(1).ToUpper(), (int)(defColor.r*255.0f), (int)(defColor.g*255.0f), (int)(defColor.b*255.0f));
return true;
}
public bool __844()
{
Obj obj = G.m_game.__277(__692(0));
if ( obj==null )
return false;
obj.m_title.m_sub.Set(__694(1));
return true;
}
public bool __845()
{
Obj obj = G.m_game.__277(__692(0));
if ( obj==null )
return false;
int index = G.Clamp(__694(1), 0, 3);
if ( index==obj.m_icon.cur )
return false;
if ( obj.__454() )
obj.__454().End();
obj.m_icon.Set(index);
Sprite icon = obj.__454();
if ( icon==null )
return false;
Asset asset = G.__96(G.m_pathGraphics);
if ( asset )
{
icon.__469(asset);
asset.Close();
}
return true;
}
public bool __846()
{
Obj obj = G.m_game.__277(__692(0));
if ( obj==null )
return false;
if ( m_paramCount<2 )
{
obj.m_anchorX.__78();
obj.m_anchorY.__78();
}
else
{
obj.m_anchorX.Set(__694(1));
obj.m_anchorY.Set(__694(2));
}
return true;
}
public bool __847()
{
Obj obj = G.m_game.__277(__692(0));
if ( obj==null )
return false;
obj.m_tint.Set(G.__123(__692(1)));
return true;
}
public bool __848()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
string uid = __692(i);
Obj obj = G.m_game.__277(uid);
if ( obj )
obj.m_enabled.Set(true);
}
return true;
}
public bool __849()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
string uid = __692(i);
Obj obj = G.m_game.__277(uid);
if ( obj )
obj.m_enabled.Set(false);
}
return true;
}
public bool __850()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
string uid = __692(i);
Obj obj = G.m_game.__277(uid);
if ( obj )
obj.m_subEnabled.Set(true);
}
return true;
}
public bool __851()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
string uid = __692(i);
Obj obj = G.m_game.__277(uid);
if ( obj )
obj.m_subEnabled.Set(false);
}
return true;
}
public bool __852()
{
string uidScene = "";
string uidObj = "";
for ( int i=0 ; i<m_paramCount ; i++ )
{
G.__98(__692(i), ref uidScene, ref uidObj);
G.m_game.__306(uidScene, uidObj, true);
}
return true;
}
public bool __853()
{
string uidScene = "";
string uidObj = "";
for ( int i=0 ; i<m_paramCount ; i++ )
{
G.__98(__692(i), ref uidScene, ref uidObj);
G.m_game.__306(uidScene, uidObj, false);
}
return true;
}
public bool __854()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
SceneObj sceneObj = G.m_game.__307(__692(i));
if ( sceneObj==null )
continue;
if ( sceneObj.m_visible.cur )
return true;
}
return false;
}
public bool __855()
{
SceneObj sceneObj = G.m_game.__307(__692(0));
if ( sceneObj==null )
return false;
sceneObj.m_parentName.Set(__692(1));
sceneObj.m_parent = sceneObj.m_scene.__536(ref sceneObj.m_parentName.cur);
return true;
}
public bool __856()
{
string p1 = __692(1);
string p2 = __692(2);
string p3 = __692(3);
string defaultStop = p1.Length==0 ? "STOP" : p1;
string defaultTalk = p2.Length==0 ? "TALK" : p2;
string defaultWalk = p3.Length==0 ? "WALK" : p3;
Scene scene = null;
SceneObj sceneObj = null;
if ( __701(__692(0), ref scene, ref sceneObj)==false )
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
sceneObj.__632(ref sceneObj.m_defaultStopAnim.cur);
if ( sceneObj.m_anim.cur.m_name==prevDefTalkAnim && sceneObj.m_defaultTalkAnim.cur!=prevDefTalkAnim )
sceneObj.__632(ref sceneObj.m_defaultTalkAnim.cur);
if ( sceneObj.m_anim.cur.m_name==prevDefWalkAnim && sceneObj.m_defaultWalkAnim.cur!=prevDefWalkAnim )
sceneObj.__632(ref sceneObj.m_defaultWalkAnim.cur);
}
return true;
}
public bool __857()
{
string p1 = __692(1);
string defaultStop = p1.Length==0 ? "STOP" : p1;
Scene scene = null;
SceneObj sceneObj = null;
if ( __701(__692(0), ref scene, ref sceneObj)==false )
return false;
string prevDefStopAnim = sceneObj.m_defaultStopAnim.cur;
sceneObj.m_defaultStopAnim.Set(defaultStop);
if ( sceneObj.m_message==null && sceneObj.m_anim.cur )
{
if ( sceneObj.m_anim.cur.m_name==prevDefStopAnim && sceneObj.m_defaultStopAnim.cur!=prevDefStopAnim )
sceneObj.__632(ref sceneObj.m_defaultStopAnim.cur);
}
return true;
}
public bool __858()
{
string p1 = __692(1);
string defaultWalk = p1.Length==0 ? "WALK" : p1;
Scene scene = null;
SceneObj sceneObj = null;
if ( __701(__692(0), ref scene, ref sceneObj)==false )
return false;
string prevDefWalkAnim = sceneObj.m_defaultWalkAnim.cur;
sceneObj.m_defaultWalkAnim.Set(defaultWalk);
if ( sceneObj.m_message==null && sceneObj.m_anim.cur )
{
if ( sceneObj.m_anim.cur.m_name==prevDefWalkAnim && sceneObj.m_defaultWalkAnim.cur!=prevDefWalkAnim )
sceneObj.__632(ref sceneObj.m_defaultWalkAnim.cur);
}
return true;
}
public bool __859()
{
string p1 = __692(1);
string defaultTalk = p1.Length==0 ? "TALK" : p1;
Scene scene = null;
SceneObj sceneObj = null;
if ( __701(__692(0), ref scene, ref sceneObj)==false )
return false;
string prevDefTalkAnim = sceneObj.m_defaultTalkAnim.cur;
sceneObj.m_defaultTalkAnim.Set(defaultTalk);
if ( sceneObj.m_message==null && sceneObj.m_anim.cur )
{
if ( sceneObj.m_anim.cur.m_name==prevDefTalkAnim && sceneObj.m_defaultTalkAnim.cur!=prevDefTalkAnim )
sceneObj.__632(ref sceneObj.m_defaultTalkAnim.cur);
}
return true;
}
public bool __860()
{
Scene scene = null;
SceneObj sceneObj = null;
if ( __701(__692(0), ref scene, ref sceneObj)==false )
return false;
Anim anim = sceneObj.m_obj.__471(__692(1));
if ( anim==null )
return false;
anim.__474(__694(2));
return true;
}
public bool __861()
{
Scene scene = null;
SceneObj sceneObj = null;
if ( __701(__692(0), ref scene, ref sceneObj)==false )
return false;
Anim anim = sceneObj.m_obj.__471(__692(1));
if ( anim==null )
return false;
string mode = __692(2);
if ( mode=="BACKWARD" )
anim.__474(-Math.Abs(anim.m_speed.cur));
else if ( mode=="TOGGLE" )
anim.__474(-anim.m_speed.cur);
else
anim.__474(Math.Abs(anim.m_speed.cur));
return true;
}
public bool __862()
{
Scene scene = null;
SceneObj sceneObj = null;
if ( __701(__692(0), ref scene, ref sceneObj)==false )
return false;
sceneObj.m_tags[0] = __692(1);
return true;
}
public bool __863()
{
Scene scene = null;
SceneObj sceneObj = null;
if ( __701(__692(0), ref scene, ref sceneObj)==false )
return false;
Variable var = __697(1);
if ( var==null )
return false;
var.m_value = sceneObj.m_tags[0];
return true;
}
public bool __864()
{
Scene scene = null;
SceneObj sceneObj = null;
if ( __701(__692(0), ref scene, ref sceneObj)==false )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
string tag = __698(i);
if ( G.__149(sceneObj.m_tags, ref tag) )
return true;
if ( G.__149(sceneObj.m_obj.m_tags, ref tag) )
return true;
}
return false;
}
public bool __865()
{
Scene scene = null;
SceneObj sceneObj = null;
if ( __701(__692(0), ref scene, ref sceneObj)==false )
return false;
sceneObj.m_sticker.Set(__692(1));
return true;
}
public bool __866()
{
Scene scene = null;
SceneObj sceneObj = null;
if ( __701(__692(0), ref scene, ref sceneObj)==false )
return false;
Variable var = __697(1);
if ( var==null )
return false;
var.m_value = sceneObj.m_sticker.cur;
return true;
}
public bool __867()
{
Scene scene = null;
SceneObj sceneObj = null;
if ( __701(__692(0), ref scene, ref sceneObj)==false )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( sceneObj.m_sticker.cur==__692(i) )
return true;
}
return false;
}
public bool __868()
{
SceneObj sceneObj = __699(0);
if ( sceneObj==null )
return false;
sceneObj.m_elevator.Set(__694(1));
return true;
}
public bool __869()
{
SceneObj sceneObj = __699(0);
if ( sceneObj==null )
return false;
int iGrid = __694(1);
if ( iGrid<0 || iGrid>=G.PATH_GRID_COUNT )
return false;
sceneObj.m_iGrid.Set(iGrid);
return true;
}
public bool __870()
{
SceneObj sceneObj = __699(0);
if ( sceneObj==null || sceneObj.m_paths==null )
return false;
int iPath = __694(1);
if ( iPath<0 || iPath>=G.PATH_GRID_COUNT || sceneObj.m_paths[iPath]==null )
return false;
sceneObj.m_iPath.Set(iPath);
sceneObj.m_pathStarted.Set(false);
sceneObj.m_pathRoleBox = null;
return true;
}
public bool __871()
{
SceneObj sceneObj = __699(0);
if ( sceneObj==null )
return false;
sceneObj.m_opacity.Set(G.Clamp(__694(1), 0, 100)/100.0f);
return true;
}
public bool __872()
{
SceneObj sceneObj = __699(0);
if ( sceneObj==null )
return false;
sceneObj.m_angle.Set(__694(1));
return true;
}
public bool __873()
{
Variable var = __697(1);
if ( var==null )
return false;
SceneObj sceneObj = __699(0);
if ( sceneObj==null )
{
var.m_value = "0";
return false;
}
var.m_value = G.__144(sceneObj.m_angle.cur).ToString();
return true;
}
public bool __874()
{
SceneObj sceneObj = __699(0);
if ( sceneObj==null )
return false;
int iOld = (int)sceneObj.m_placement.cur;
switch ( __692(1) )
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
scene.__566();
scene.__568();
return true;
}
public bool __875()
{
SceneObj sceneObj = G.m_game.__307(__692(0));
if ( sceneObj==null )
return false;
if ( m_paramCount==1 )
sceneObj.m_autoZ.Set(true);
else
{
sceneObj.m_autoZ.Set(false);
sceneObj.m_z.Set((float)__694(1));
}
return true;
}
public bool __876()
{
SceneObj sceneObj = G.m_game.__307(__692(0));
if ( sceneObj==null )
return false;
if ( m_paramCount==1 )
sceneObj.m_manualScale.Set(0.0f);
else
sceneObj.m_manualScale.Set(G.__120(__692(1)));
return true;
}
public bool __877()
{
Variable var = __697(1);
if ( var==null )
return false;
SceneObj sceneObj = __699(0);
if ( sceneObj==null )
{
var.m_value = "100";
return false;
}
float scale = sceneObj.__646(sceneObj.__619()) * 100.0f;
var.m_value = G.Clamp((int)scale, 0, 400).ToString();
return true;
}
public bool __878()
{
SceneObj sceneObj = G.m_game.__307(__692(0));
if ( sceneObj==null )
return false;
if ( m_paramCount==1 )
sceneObj.m_manualZoom.Set(0.0f);
else
sceneObj.m_manualZoom.Set(G.__121(__692(1)));
return true;
}
public bool __879()
{
Variable var = __697(1);
if ( var==null )
return false;
SceneObj sceneObj = __699(0);
if ( sceneObj==null )
{
var.m_value = "100";
return false;
}
float zoom = sceneObj.__647(sceneObj.__619()) * 100.0f;
var.m_value = G.Clamp((int)zoom, 100, 400).ToString();
return true;
}
public bool __880()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
SceneObj sceneObj = G.m_game.__307(__692(i));
if ( sceneObj )
sceneObj.m_cheat.Set(true);
}
return true;
}
public bool __881()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
SceneObj sceneObj = G.m_game.__307(__692(i));
if ( sceneObj )
sceneObj.m_cheat.Set(false);
}
return true;
}
public bool __882()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
SceneObj sceneObj = G.m_game.__307(__692(i));
if ( sceneObj )
sceneObj.__624();
}
return true;
}
public bool __883()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
SceneObj sceneObj = G.m_game.__307(__692(i));
if ( sceneObj )
sceneObj.__625();
}
return true;
}
public bool __884(bool visible)
{
bool changed = false;
for ( int i=0 ; i<m_paramCount ; i++ )
{
SceneObj sceneObj = G.m_game.__307(__692(i));
if ( sceneObj )
{
if ( sceneObj.__631(visible, false) )
changed = true;
}
}
if ( changed )
{
Scene scene = G.m_game.__291();
if ( scene )
scene.__565();
}
return true;
}
public bool __884()
{
return __884(true);
}
public bool __885()
{
return __884(false);
}
public bool __886()
{
SceneObj sceneObj = __699(0);
if ( sceneObj==null || sceneObj.m_light==false )
return false;
sceneObj.m_lightAmbient.Set(G.Clamp(__694(1)/255.0f));
return true;
}
public bool __887()
{
SceneObj sceneObj = __699(0);
if ( sceneObj==null || sceneObj.m_light==false )
return false;
sceneObj.m_lightDiffuse.Set(G.__123(__692(1)));
return true;
}
public bool __888()
{
SceneObj sceneObj = __699(0);
if ( sceneObj==null || sceneObj.m_light==false )
return false;
sceneObj.m_lightAngle.Set(G.Clamp(__694(1)*G.DEG_TO_RAD, 0.0f, G.RAD_360));
sceneObj.m_lightMeshChanged = true;
return true;
}
public bool __889()
{
SceneObj sceneObj = __699(0);
if ( sceneObj==null || sceneObj.m_light==false )
return false;
sceneObj.m_lightDir.Set(G.__142(__694(1)));
sceneObj.m_lightMeshChanged = true;
return true;
}
public bool __890()
{
SceneObj sceneObj = __699(0);
if ( sceneObj==null || sceneObj.m_light==false )
return false;
float oldDist = sceneObj.m_lightDist.cur;
float newDist = G.__133(__694(1));
sceneObj.m_lightDist.Set(newDist);
sceneObj.m_lightMeshChanged = true;
if ( oldDist==0.0f )
{
if ( newDist!=0.0f )
{
Scene scene = G.m_game.__291();
if ( scene )
scene.__565();
}
}
else
{
if ( newDist==0.0f )
{
Scene scene = G.m_game.__291();
if ( scene )
scene.__565();
}
}
return true;
}
public bool __891()
{
G.m_game.__309();
return true;
}
public bool __892()
{
return G.m_game.m_menuDialog.__38();
}
public bool __893()
{
Dialog dialog = __700(0);
if ( dialog==null )
return false;
Sentence sentence = dialog.m_root.__49(__694(1));
if ( sentence==null )
return false;
return sentence.m_visited;
}
public bool __894()
{
Dialog dialog = __700(0);
if ( dialog==null )
return false;
Sentence sentence = dialog.m_root.__49(__694(1));
if ( sentence==null || sentence.m_choice==false )
return false;
return sentence.m_visible.cur;
}
public bool __895()
{
Dialog dialog = __700(0);
if ( dialog )
dialog.__50(__694(1));
return true;
}
public bool __896()
{
Dialog dialog = __700(0);
if ( dialog )
dialog.__50(__694(1), false);
return true;
}
public bool __897()
{
string name = __692(0);
if ( name.Length==0 )
G.m_game.m_colorVoiceOver = Color.clear;
else
G.m_game.m_colorVoiceOver = G.__162(name);
return true;
}
public bool __898()
{
Variable var = __697(0);
if ( var==null )
return false;
var.m_value = __694(2).ToString();
return true;
}
public bool __899()
{
Variable var = __697(2);
if ( var==null )
return false;
var.m_value = G.m_game.__312(__692(0), __694(1));
return true;
}
public bool __900()
{
Dialog dialog = __700(0);
if ( dialog==null )
return false;
Sentence sentence = dialog.m_root.__49(__694(1));
if ( sentence==null )
return false;
sentence.m_tags[0] = __692(2);
return true;
}
public bool __901()
{
Dialog dialog = __700(0);
if ( dialog==null )
return false;
Sentence sentence = dialog.m_root.__49(__694(1));
if ( sentence==null )
return false;
Variable var = __697(2);
if ( var==null )
return false;
var.m_value = sentence.m_tags[0];
return true;
}
public bool __902()
{
Dialog dialog = __700(0);
if ( dialog==null )
return false;
Sentence sentence = dialog.m_root.__49(__694(1));
if ( sentence==null )
return false;
for ( int i=2 ; i<m_paramCount ; i++ )
{
if ( G.__149(sentence.m_tags, __698(i)) )
return true;
}
return false;
}
public bool __903()
{
return G.m_game.__305(__692(0), __692(1));
}
public bool __904()
{
return G.m_game.__304(__692(0), m_script.__691());
}
public bool __905()
{
if ( G.m_game.m_menuScene )
return false;
if ( m_paramCount==0 )
{
G.m_game.m_layout.__418(new string[0]);
}
else
{
string[] players = new string[m_paramCount];
for ( int i=0 ; i<m_paramCount ; i++ )
players[i] = __692(i);
G.m_game.m_layout.__418(players);
}
return true;
}
public bool __906()
{
Player player = G.m_game.__279(__692(0));
if ( player==null )
return false;
int index = G.Clamp(__694(1), 0, 3);
if ( index==player.m_icon.cur )
return false;
if ( player.__454() )
player.__454().End();
player.m_icon.Set(index);
Sprite icon = player.__454();
if ( icon==null )
return false;
Asset asset = G.__96(G.m_pathGraphics);
if ( asset )
{
icon.__469(asset);
asset.Close();
}
return true;
}
public bool __907()
{
Player player = G.m_game.__293();
if ( player==null )
return false;
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( player.__48(__692(i)) )
return true;
}
return false;
}
public bool __908()
{
if ( G.m_game.m_menuScene )
return false;
Player player = G.m_game.__279(__692(0));
if ( player==null )
return false;
player.__477(__692(1));
return true;
}
public bool __909()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
Player player = G.m_game.__279(__692(0));
if ( player )
player.__482();
}
return true;
}
public bool __910()
{
Player src = G.m_game.__279(__692(0));
Player trg = G.m_game.__279(__692(1));
if ( src==null || trg==null )
return false;
src.__483(trg);
return true;
}
public bool __911()
{
return __911(true);
}
public bool __912()
{
return __911(false);
}
public bool __911(bool withFx)
{
Player player = G.m_game.__279(__692(0));
if ( player==null )
return false;
if ( m_script.__691()==false )
withFx = false;
string uidScene = "";
string uidObj = "";
for ( int i=1 ; i<m_paramCount ; i++ )
{
G.__98(__692(i), ref uidScene, ref uidObj);
G.m_game.__306(uidScene, uidObj, false);
Obj obj = G.m_game.__277(uidObj);
Player objPlayer = obj.__293();
if ( objPlayer )
objPlayer.__481(obj);
player.__480(obj);
if ( withFx && obj.__454() )
G.m_game.__313(obj);
}
return true;
}
public bool __913()
{
return __913(true);
}
public bool __914()
{
return __913(false);
}
public bool __913(bool withFx)
{
Player player = G.m_game.__279(__692(0));
if ( player==null )
return false;
if ( m_script.__691()==false )
withFx = false;
string[] uidScene = new string[2];
string[] uidObj = new string[2];
Obj[] obj = new Obj[2];
Player[] objPlayer = new Player[2];
for ( int i=0 ; i<2 ; i++ )
{
G.__98(__692(i+1), ref uidScene[i], ref uidObj[i]);
G.m_game.__306(uidScene[i], uidObj[i], false);
obj[i] = G.m_game.__277(uidObj[i]);
objPlayer[i] = obj[i].__293();
}
if ( objPlayer[0]==null || objPlayer[0]!=player || obj[1]==null )
return false;
if ( player.__485(obj[0])==-1 )
return false;
if ( objPlayer[1] )
objPlayer[1].__481(obj[1]);
int index = player.__485(obj[0]);
if ( index==-1 )
return false;
objPlayer[0].__481(obj[0]);
G.m_game.__290().__480(obj[0]);
player.__480(obj[1], index);
if ( withFx && obj[1].__454() )
G.m_game.__313(obj[1]);
return true;
}
public bool __915()
{
string uidScene = "";
string uidObj = "";
for ( int i=0 ; i<m_paramCount ; i++ )
{
G.__98(__692(i), ref uidScene, ref uidObj);
G.m_game.__306(uidScene, uidObj, false);
Obj obj = G.m_game.__277(uidObj);
Player objPlayer = obj.__293();
if ( objPlayer )
objPlayer.__481(obj);
G.m_game.__290().__480(obj);
}
return true;
}
public bool __916()
{
Player dump = G.m_game.__290();
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( dump.__484(G.m_game.__277(__692(i))) )
return true;
}
return false;
}
public bool __917()
{
Player player = G.m_game.__279(__692(0));
if ( player==null )
return false;
for ( int i=1 ; i<m_paramCount ; i++ )
{
if ( player.__484(__692(i)) )
return true;
}
return false;
}
public bool __918()
{
Player player = G.m_game.__279(__692(0));
if ( player==null )
return false;
player.m_hasScroll.Set(true);
Scene scene = G.m_game.__291();
if ( scene )
scene.__571(player);
return true;
}
public bool __919()
{
Player player = G.m_game.__279(__692(0));
if ( player==null )
return false;
player.m_hasScroll.Set(false);
Scene scene = G.m_game.__291();
if ( scene )
scene.__571(player);
return true;
}
public bool __920()
{
Player player = G.m_game.__279(__692(0));
if ( player==null )
return false;
player.m_hasZoom.Set(true);
Scene scene = G.m_game.__291();
if ( scene )
scene.__571(player);
return true;
}
public bool __921()
{
Player player = G.m_game.__279(__692(0));
if ( player==null )
return false;
player.m_hasZoom.Set(false);
Scene scene = G.m_game.__291();
if ( scene )
scene.__571(player);
return true;
}
public bool __922()
{
Effect effect = G.m_game.__282(__692(0));
if ( effect==null )
return false;
float size = G.__119(__692(1));
float maxZoom = G.__121(__692(2));
int count = 0;
for ( int i=0 ; i<effect.__67() ; i++ )
{
if ( effect.m_items[i].m_model==EffectItem.MODEL.BLUR )
{
EffectItem_Blur fx = (EffectItem_Blur)effect.m_items[i];
fx.__77(size, maxZoom);
count++;
}
}
return count>0;
}
public bool __923()
{
Effect effect = G.m_game.__282(__692(0));
if ( effect==null )
return false;
int angle = __694(1);
int scale = __694(2);
int focus = __694(3);
int count = 0;
for ( int i=0 ; i<effect.__67() ; i++ )
{
if ( effect.m_items[i].m_model==EffectItem.MODEL.PARALLAX )
{
EffectItem_Parallax fx = (EffectItem_Parallax)effect.m_items[i];
fx.__77(angle, scale, focus);
count++;
}
}
return count>0;
}
public bool __924()
{
if ( m_script.__691()==false )
return false;
string p1 = __692(1);
float vol = p1.Length==0 ? 1.0f : G.Clamp(G.__114(ref p1)/100.0f);
bool loop = __694(2)!=0;
Sound sound = G.m_game.__271(__692(0));
if ( sound )
{
sound.m_loop = loop;
sound.__989(vol);
}
return true;
}
public bool __925()
{
if ( m_script.__691()==false )
return false;
for ( int i=0 ; i<m_paramCount ; i++ )
{
string name = __692(i);
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
public bool __926()
{
if ( m_script.__691()==false )
return false;
for ( int i=0 ; i<m_paramCount ; i++ )
{
Sound sound = G.m_game.__271(__692(i));
if ( sound && sound.__528() )
return true;
}
return false;
}
public bool __927()
{
float volume = G.Clamp(__694(0)/100.0f);
float duration = __696(1);
G.m_game.__263(__694(2), volume, duration);
return true;
}
public bool __928()
{
int p0 = __694(0);
G.m_game.m_locked = true;
G.m_game.m_lockedDuration = p0<=0 ? -1.0f : p0/1000.0f;
G.m_game.m_layout.m_bagOpened = false;
G.m_game.m_cursorObj = null;
return true;
}
public bool __929()
{
G.m_game.m_locked = false;
G.m_game.m_lockedDuration = -1.0f;
G.m_game.m_layout.m_bagOpened = false;
return true;
}
public bool __930()
{
return G.m_game.__256();
}
public bool __931()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
string name = __692(i);
if ( name=="BAG" )
G.m_game.m_gestureBagLocked = true;
else if ( name=="MENU" )
G.m_game.m_gestureMenuLocked = true;
}
return true;
}
public bool __932()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
string name = __692(i);
if ( name=="BAG" )
G.m_game.m_gestureBagLocked = false;
else if ( name=="MENU" )
G.m_game.m_gestureMenuLocked = false;
}
return true;
}
public bool __933()
{
if ( G.__87() )
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
string key = __692(i).ToLower();
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
public bool __934()
{
return G.m_game.m_input.m_isDown;
}
public bool __935()
{
Scene scene = G.m_game.__291();
if ( scene==null )
return false;
int x = G.m_game.m_cursor ? (int)scene.__553(G.m_game.m_cursorViewX) : -1;
int y = G.m_game.m_cursor ? (int)scene.__554(G.m_game.m_cursorViewY) : -1;
for ( int i=0 ; i<2 ; i++ )
{
string value = i==0 ? x.ToString() : y.ToString();
Variable var = __697(i);
if ( var )
var.m_value = value;
}
if ( x==-1 || y==-1 )
return false;
return true;
}
public bool __936()
{
G.m_game.__298(__692(0));
return true;
}
public bool __937()
{
G.m_game.m_cursorVisible = true;
return true;
}
public bool __938()
{
G.m_game.m_cursorVisible = false;
return true;
}
public bool __939()
{
G.m_game.m_useLocked = true;
return true;
}
public bool __940()
{
G.m_game.m_useLocked = false;
return true;
}
public bool __941()
{
G.m_game.m_layout.m_bagLocked = true;
G.m_game.m_layout.m_bagOpened = false;
G.m_game.m_cursorObj = null;
return true;
}
public bool __942()
{
G.m_game.m_layout.m_bagLocked = false;
G.m_game.m_layout.m_bagOpened = false;
return true;
}
public bool __943()
{
G.m_game.m_layout.m_bagForceHidden = false;
G.m_game.m_cursorObj = null;
return true;
}
public bool __944()
{
G.m_game.m_layout.m_bagForceHidden = true;
return true;
}
public bool __945()
{
if ( G.m_game.m_menuScene )
return false;
G.m_game.__257();
return true;
}
public bool __946()
{
if ( G.m_game.m_menuScene==null )
return false;
G.m_game.__258();
return true;
}
public bool __947()
{
if ( G.m_game.m_menuScene==null )
return false;
string uid = __692(0);
if ( G.m_game.__274(uid)==null )
return false;
G.m_game.__302(uid);
return true;
}
public bool __948()
{
G.m_game.__259();
return true;
}
public bool __949()
{
bool ok = false;
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.__187(G.__114(__692(i)), true) )
ok = true;
}
return ok;
}
public bool __950()
{
bool ok = false;
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.__187(G.__114(__692(i)), false) )
ok = true;
}
return ok;
}
public bool __951()
{
G.m_game.m_saveMenuLocked = true;
return true;
}
public bool __952()
{
G.m_game.m_saveMenuLocked = false;
return true;
}
public bool __953()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.__148(__692(i), G.m_game.__210()) )
return true;
}
return false;
}
public bool __954()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.__148(__692(i), G.m_game.__207()) )
return true;
}
return false;
}
public bool __955()
{
int index = G.m_game.__205(__692(0));
if ( index==-1 )
return false;
G.m_game.m_optionLanguageAudio = index;
G.m_game.__248();
return true;
}
public bool __956()
{
int index = G.m_game.__205(__692(0));
if ( index==-1 )
return false;
G.m_game.m_optionLanguageText = index;
G.m_game.__248();
G.m_game.__218();
return true;
}
public bool __957()
{
return G.m_game.m_earlyAccess;
}
public bool __958()
{
for ( int i=0 ; i<m_paramCount ; i++ )
{
if ( G.m_game.__242(__692(i)) )
return true;
}
return false;
}
public bool __959()
{
return G.m_game.m_domainAllowed;
}
public bool __960()
{
Effect effect = G.m_game.__282(__692(0));
if ( effect==null )
G.m_game.m_effect.Reset();
else
G.m_game.m_effect.Set(effect);
return true;
}
public bool __961()
{
Variable var = __697(0);
if ( var==null )
return false;
int start = __694(1);
int time = (int)(G.m_game.m_time*1000.0f);
var.m_value = (time-start).ToString();
return true;
}
public bool __962()
{
Variable var = __697(0);
if ( var==null )
return false;
int time = (int)(G.m_game.m_elapsed*1000.0f);
var.m_value = time.ToString();
return true;
}
public bool __963()
{
string asset = __692(0);
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
scene.m_tags[0] = __692(1);
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
obj.m_tags[0] = __692(1);
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
player.m_tags[0] = __692(1);
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
dialog.m_tags[0] = __692(1);
return true;
}
break;
}
}
return false;
}
public bool __964()
{
string asset = __692(0);
if ( asset.Length==0 )
return false;
Variable var = __697(1);
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
public bool __965()
{
string asset = __692(0);
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
if ( G.__149(scene.m_tags, __698(i)) )
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
if ( G.__149(obj.m_tags, __698(i)) )
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
if ( G.__149(player.m_tags, __698(i)) )
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
if ( G.__149(dialog.m_tags, __698(i)) )
return true;
}
}
break;
}
}
return false;
}
public bool __966()
{
G.m_game.__244();
return true;
}
public bool __967()
{
return G.m_game.__245(__692(0), __692(1));
}
public bool __968()
{
string value = G.m_game.__246(__692(1));
Variable var = __697(0);
if ( var )
var.m_value = value;
return true;
}
public bool __969()
{
if ( G.__190(0)==false )
return false;
return G.m_game.__47(0);
}
public bool __970()
{
if ( G.m_game.m_configDebug )
return false;
G.m_game.m_autoSaveAsap = true;
return true;
}
public bool __971()
{
return G.m_game.__47(__694(0), true);
}
public bool __972()
{
if ( G.m_game.m_menuScene==null )
return false;
G.m_game.__240();
G.m_game.__241();
return true;
}
public bool __973()
{
if ( G.m_game.m_menuScene==null )
return false;
int index = __694(0);
if ( index<0 || index>4 )
return false;
if ( G.__190(index)==false )
return false;
return G.m_game.__47(index);
}
public bool __974()
{
if ( __975()==false )
return false;
int index = __694(0);
if ( index<0 || index>4 )
return false;
G.m_game.__46(index);
return true;
}
public bool __975()
{
return G.m_game.m_configDebug==false && G.m_game.m_menuScene!=null && G.m_game.m_customMenuSaveDisabled==false;
}
public bool __976()
{
AgePlugin.OnAppQuit();
return true;
}
public bool __977()
{
G.OpenUrl(__692(0));
return true;
}
public bool __978()
{
#if UNITY_ANDROID
return true;
#else
return false;
#endif
}
public bool __979()
{
#if UNITY_IOS
return true;
#else
return false;
#endif
}
public bool __980()
{
#if UNITY_WEBGL
return true;
#else
return false;
#endif
}
public bool __981()
{
#if UNITY_STANDALONE_WIN
return true;
#else
return false;
#endif
}
public bool __982()
{
G.Vibrate();
return true;
}
public bool __983()
{
string text = __692(0);
for ( int i=1 ; i<m_paramCount ; i++ )
{
text += "\n";
text += __692(i);
}
G.__198(text);
return true;
}
public bool __984()
{
G.m_game.__251(__692(0));
return true;
}
public bool __985()
{
return G.m_game.m_configDebug;
}
public bool __986()
{
return AgePlugin.Callback(__692(0), __692(1));
}
public bool __987()
{
if ( m_script.__691()==false )
return false;
G.m_game.m_interludeAsap = true;
G.m_game.m_interludeParam = __692(0);
return true;
}
public bool __988()
{
G.m_game.m_interludeAsap = false;
G.m_game.m_interludeParam = "";
return true;
}
}
