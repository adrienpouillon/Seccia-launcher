using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
public class Json
{
public static implicit operator bool(Json inst) { return inst!=null; }
public static void Write(Stream stream, string val)
{
for ( int i=0 ; i<val.Length ; i++ )
stream.WriteByte((byte)val[i]);
}
public static JsonBuffer __370(Stream stream)
{
int count = (int)(stream.Length - stream.Position);
byte[] buffer = new byte[count];
stream.Read(buffer, 0, count);
JsonBuffer buf = new JsonBuffer(count);
for ( int i=0 ; i<count ; i++ )
buf.data[i] = (char)buffer[i];
return buf;
}
public static JsonBuffer __370(Asset asset)
{
int size = asset.__7();
JsonBuffer buf = new JsonBuffer(size);
if ( size>0 )
{
byte[] buffer = new byte[size];
asset.__9(buffer, size);
for ( int i=0 ; i<size ; i++ )
buf.data[i] = (char)buffer[i];
}
return buf;
}
public static string __371(string src)
{
if ( src.Length==0 )
return "";
int count = 0;
for ( int i=0 ; i<src.Length ; i++ )
{
switch ( src[i] )
{
case '\"': case '\\': case '\b': case '\f': case '\n': case '\r': case '\t':
count++;
break;
}
}
char[] buffer = new char[src.Length+count];
for ( int i=0,j=0 ; i<src.Length ; i++,j++ )
{
switch ( src[i] )
{
case '\"':
buffer[j++] = '\\';
buffer[j] = '\"';
break;
case '\\':
buffer[j++] = '\\';
buffer[j] = '\\';
break;
case '\b':
buffer[j++] = '\\';
buffer[j] = 'b';
break;
case '\f':
buffer[j++] = '\\';
buffer[j] = 'f';
break;
case '\n':
buffer[j++] = '\\';
buffer[j] = 'n';
break;
case '\r':
buffer[j++] = '\\';
buffer[j] = 'r';
break;
case '\t':
buffer[j++] = '\\';
buffer[j] = 't';
break;
default:
buffer[j] = src[i];
break;
}
}
return new string(buffer);
}
public static string __372(string src)
{
if ( src.Length==0 )
return "";
int count = 0;
for ( int i=0 ; i<src.Length ; i++ )
{
if ( src[i]=='\\' )
{
switch ( src[i+1] )
{
case '\"': case '\\': case '\b': case '\f': case '\n': case '\r': case '\t':
count++;
i++;
break;
}
}
}
char[] buffer = new char[src.Length-count];
for ( int i=0,j=0 ; i<src.Length ; i++,j++ )
{
if ( src[i]=='\\' )
{
switch ( src[i+1] )
{
case '\"': case '\\': case '\b': case '\f': case '\n': case '\r': case '\t':
buffer[j] = src[i+1];
i++;
break;
default:
buffer[j] = src[i];
break;
}
}
else
buffer[j] = src[i];
}
return new string(buffer);
}
public static JsonValue __373(ref string data)
{
JsonBuffer buf = new JsonBuffer(data.Length);
for ( int i=0 ; i<data.Length ; i++ )
buf.data[i] = data[i];
return __373(buf);
}
public static JsonValue __373(byte[] data)
{
if ( data==null )
return null;
JsonBuffer buf = new JsonBuffer(data.Length);
for ( int i=0 ; i<data.Length ; i++ )
buf.data[i] = (char)data[i];
return __373(buf);
}
public static JsonValue __373(Stream stream)
{
if ( stream==null )
return null;
return __373(__370(stream));
}
public static JsonValue __373(JsonBuffer content)
{
JsonBuffer buf = content;
while ( buf.__416()==false )
{
if ( buf.__415()=='{' )
{
buf.Add(1);
JsonObj obj = new JsonObj();
if ( JsonValue.__410(buf, obj)==-1 )
return null;
return obj;
}
if ( buf.__415()=='[' )
{
buf.Add(1);
JsonArray array = new JsonArray();
if ( JsonValue.__411(buf, array, "", true)==-1 )
return null;
return array;
}
buf.Add(1);
}
return null;
}
public static JsonObj __374()
{
return new JsonObj();
}
public static JsonArray __375()
{
return new JsonArray();
}
public static JsonObj __376(JsonValue json)
{
if ( json==null )
return null;
return json.__376();
}
public static JsonArray __377(JsonValue json)
{
if ( json==null )
return null;
return json.__377();
}
}
public class JsonObj : JsonValue
{
List<string> m_names;
List<JsonValue> m_values;
public static implicit operator bool(JsonObj inst) { return inst!=null; }
public JsonObj()
{
m_valueType = TYPE.OBJ;
m_names = new List<string>();
m_values = new List<JsonValue>();
}
public JsonObj(string name, JsonValue value)
{
m_valueType = TYPE.OBJ;
m_names = new List<string>();
m_values = new List<JsonValue>();
Add(name, value);
}
public override void __378(ref string result)
{
result += "{";
int count = __67();
for ( int i=0 ; i<count ; i++ )
{
result += "\"";
result += Json.__371(m_names[i]);
result += "\":";
m_values[i].__378(ref result);
if ( i+1<count )
result += ",";
}
result += "}";
}
public override void __378(Stream stream)
{
if ( stream==null )
return;
int count = __67();
if ( count==0 )
{
Json.Write(stream, "{}");
return;
}
Json.Write(stream, "{");
for ( int i=0 ; i<count ; i++ )
{
Json.Write(stream, "\"");
Json.Write(stream, Json.__371(m_names[i]));
Json.Write(stream, "\":");
m_values[i].__378(stream);
if ( i+1<count )
Json.Write(stream, ",");
}
Json.Write(stream, "}");
}
public override bool __373(JsonBuffer src)
{
Clear();
if ( src.__415()!='{' )
return false;
src.Add(1);
return __410(src, this)!=-1;
}
public override bool __373(Stream stream)
{
if ( stream==null )
return false;
return __373(Json.__370(stream));
}
public override JsonValue __379()
{
JsonObj value = new JsonObj();
for ( int i=0 ; i<__67() ; i++ )
value.Add(m_names[i], m_values[i].__379());
return value;
}
public override string GetValue()
{
return "";
}
public void Clear()
{
m_names.Clear();
m_values.Clear();
}
public int __67()
{
return m_names.Count;
}
public void __380(JsonObj trg)
{
if ( trg==null )
return;
trg.Clear();
for ( int i=0 ; i<__67() ; i++ )
{
trg.m_names.Add(m_names[i]);
trg.m_values.Add(m_values[i]);
}
m_names.Clear();
m_values.Clear();
}
public JsonValue Add(string name, JsonValue value)
{
if ( value==null )
return null;
m_names.Add(name);
m_values.Add(value);
return value;
}
public JsonString __381(string name, string value)
{
return (JsonString)Add(name, new JsonString(value));
}
public JsonNumber __382(string name, int value)
{
return (JsonNumber)Add(name, new JsonNumber(value));
}
public void __383(string name, int sid)
{
if ( sid>0 )
Add(name, new JsonNumber(sid));
}
public JsonNumber __384(string name, float value)
{
return (JsonNumber)Add(name, new JsonNumber(value));
}
public JsonConst __385(string name, bool value)
{
return (JsonConst)Add(name, new JsonConst(value ? SUBTYPE.TRUE : SUBTYPE.FALSE));
}
public JsonConst __386(string name)
{
return (JsonConst)Add(name, new JsonConst(SUBTYPE.TRUE));
}
public JsonConst __387(string name)
{
return (JsonConst)Add(name, new JsonConst(SUBTYPE.FALSE));
}
public JsonConst __388(string name)
{
return (JsonConst)Add(name, new JsonConst(SUBTYPE.NULL));
}
public JsonObj __389(string name)
{
return (JsonObj)Add(name, new JsonObj());
}
public JsonArray __390(string name)
{
return (JsonArray)Add(name, new JsonArray());
}
public void Remove(string name)
{
Remove(__392(name));
}
public void Remove(int index)
{
m_names.RemoveAt(index);
m_values.RemoveAt(index);
}
public bool __391(string name)
{
return Get(name);
}
public int __392(string name)
{
for ( int i=0 ; i<m_values.Count ; i++ )
{
if ( m_names[i]==name )
return i;
}
return -1;
}
public int __392(JsonValue value)
{
for ( int i=0 ; i<m_values.Count ; i++ )
{
if ( m_values[i]==value )
return i;
}
return -1;
}
public string __393(int index)
{
return m_names[index];
}
public JsonValue Get(string name)
{
for ( int i=0 ; i<m_names.Count ; i++ )
{
if ( m_names[i]==name )
return m_values[i];
}
return null;
}
public JsonValue Get(int index)
{
return m_values[index];
}
public JsonObj __394(string name, bool create = false)
{
JsonValue value = Get(name);
if ( value==null )
{
if ( create )
return __389(name);
return null;
}
if ( value.Type()!=TYPE.OBJ )
return null;
return (JsonObj)value;
}
public JsonObj __394(int index)
{
if ( m_values[index].Type()!=TYPE.OBJ )
return null;
return (JsonObj)m_values[index];
}
public JsonArray __395(string name, bool create = false)
{
JsonValue value = Get(name);
if ( value==null )
{
if ( create )
return __390(name);
return null;
}
if ( value.Type()!=TYPE.ARRAY )
return null;
return (JsonArray)value;
}
public JsonArray __395(int index)
{
if ( m_values[index].Type()!=TYPE.ARRAY )
return null;
return (JsonArray)m_values[index];
}
public JsonString SetString(string name, string val)
{
JsonValue value = Get(name);
if ( value==null )
return __381(name, val);
if ( value.Type()!=TYPE.STRING )
{
int index = __392(value);
value = new JsonString();
m_values[index] = value;
}
((JsonString)value).m_value = val;
return (JsonString)value;
}
public JsonString SetString(int index, string val)
{
JsonValue value = Get(index);
if ( value==null )
return null;
if ( value.Type()!=TYPE.STRING )
{
value = new JsonString();
m_values[index] = value;
}
((JsonString)value).m_value = val;
return (JsonString)value;
}
public string GetString(string name)
{
JsonValue value = Get(name);
if ( value==null )
return "";
if ( value.Type()!=TYPE.STRING )
return "";
return ((JsonString)value).m_value;
}
public string GetString(int index)
{
if ( m_values[index].Type()!=TYPE.STRING )
return "";
return ((JsonString)m_values[index]).m_value;
}
public JsonNumber SetInt(string name, int val)
{
JsonValue value = Get(name);
if ( value==null )
return __382(name, val);
if ( value.Type()!=TYPE.NUMBER )
{
int index = __392(value);
value = new JsonNumber();
m_values[index] = value;
}
((JsonNumber)value).SetInt(val);
return (JsonNumber)value;
}
public JsonNumber SetInt(int index, int val)
{
JsonValue value = Get(index);
if ( value==null )
return null;
if ( value.Type()!=TYPE.NUMBER )
{
value = new JsonNumber();
m_values[index] = value;
}
((JsonNumber)value).SetInt(val);
return (JsonNumber)value;
}
public int GetInt(string name)
{
JsonValue value = Get(name);
if ( value==null )
return 0;
if ( value.Type()!=TYPE.NUMBER )
return 0;
return ((JsonNumber)value).GetInt();
}
public int GetInt(int index)
{
if ( m_values[index].Type()!=TYPE.NUMBER )
return 0;
return ((JsonNumber)m_values[index]).GetInt();
}
public bool __396(string name)
{
JsonValue value = Get(name);
if ( value==null )
return false;
if ( value.Type()!=TYPE.NUMBER )
return false;
return ((JsonNumber)value).m_type==SUBTYPE.INTEGER;
}
public JsonNumber SetFloat(string name, float val)
{
JsonValue value = Get(name);
if ( value==null )
return __384(name, val);
if ( value.Type()!=TYPE.NUMBER )
{
int index = __392(value);
value = new JsonNumber();
m_values[index] = value;
}
((JsonNumber)value).SetFloat(val);
return (JsonNumber)value;
}
public JsonNumber SetFloat(int index, float val)
{
JsonValue value = Get(index);
if ( value==null )
return null;
if ( value.Type()!=TYPE.NUMBER )
{
value = new JsonNumber();
m_values[index] = value;
}
((JsonNumber)value).SetFloat(val);
return (JsonNumber)value;
}
public float GetFloat(string name)
{
JsonValue value = Get(name);
if ( value==null )
return 0.0f;
if ( value.Type()!=TYPE.NUMBER )
return 0.0f;
return ((JsonNumber)value).GetFloat();
}
public float GetFloat(int index)
{
if ( m_values[index].Type()!=TYPE.NUMBER )
return 0.0f;
return ((JsonNumber)m_values[index]).GetFloat();
}
public bool __397(string name)
{
JsonValue value = Get(name);
if ( value==null )
return false;
if ( value.Type()!=TYPE.NUMBER )
return false;
return ((JsonNumber)value).m_type==SUBTYPE.FLOAT;
}
public JsonConst __398(string name, SUBTYPE val)
{
JsonValue value = Get(name);
if ( value==null )
return (JsonConst)Add(name, new JsonConst(val));
if ( value.Type()!=TYPE.CONST )
{
int index = __392(value);
value = new JsonConst();
m_values[index] = value;
}
((JsonConst)value).m_value = val;
return (JsonConst)value;
}
public JsonConst __398(int index, SUBTYPE val)
{
JsonValue value = Get(index);
if ( value==null )
return null;
if ( value.Type()!=TYPE.CONST )
{
value = new JsonConst();
m_values[index] = value;
}
((JsonConst)value).m_value = val;
return (JsonConst)value;
}
public SUBTYPE __399(string name)
{
JsonValue value = Get(name);
if ( value==null )
return 0;
if ( value.Type()!=TYPE.CONST )
return 0;
return ((JsonConst)value).m_value;
}
public SUBTYPE __399(int index)
{
if ( m_values[index].Type()!=TYPE.CONST )
return SUBTYPE.ERROR;
return ((JsonConst)m_values[index]).m_value;
}
public JsonConst __400(string name, bool value)
{
return __398(name, value ? SUBTYPE.TRUE : SUBTYPE.FALSE);
}
public JsonConst __400(int index, bool value)
{
return __398(index, value ? SUBTYPE.TRUE : SUBTYPE.FALSE);
}
public bool __401(string name)
{
JsonValue value = Get(name);
if ( value==null )
return false;
return value.__405();
}
public bool __401(int index)
{
return m_values[index].__405();
}
public bool __402(string name)
{
JsonValue value = Get(name);
if ( value==null || value.Type()!=TYPE.CONST )
return false;
return ((JsonConst)value).__404()==false;
}
public JsonConst __403(string name)
{
return __398(name, SUBTYPE.NULL);
}
public JsonConst __403(int index)
{
return __398(index, SUBTYPE.NULL);
}
public bool __404(string name)
{
JsonValue value = Get(name);
if ( value==null )
return false;
return value.__404();
}
public bool __404(int index)
{
return m_values[index].__404();
}
public bool __405(string name)
{
JsonValue value = Get(name);
if ( value==null )
return false;
return value.__405();
}
public bool __405(int index)
{
return m_values[index].__405();
}
public bool __406(string name)
{
JsonValue value = Get(name);
if ( value==null )
return false;
return value.__406();
}
public bool __406(int index)
{
return m_values[index].__406();
}
}
public class JsonArray : JsonValue
{
public List<JsonValue> m_items;
public static implicit operator bool(JsonArray inst) { return inst!=null; }
public JsonArray()
{
m_valueType = TYPE.ARRAY;
m_items = new List<JsonValue>();
}
public JsonArray(JsonValue value)
{
m_valueType = TYPE.ARRAY;
m_items = new List<JsonValue>();
Add(value);
}
public override void __378(Stream stream)
{
if ( stream==null )
return;
Json.Write(stream, "[");
int count = __67();
for ( int i=0 ; i<count ; i++ )
{
m_items[i].__378(stream);
if ( i+1<count )
Json.Write(stream, ",");
}
Json.Write(stream, "]");
}
public override void __378(ref string result)
{
result += "[";
int count = __67();
for ( int i=0 ; i<count ; i++ )
{
m_items[i].__378(ref result);
if ( i+1<count )
result += ",";
}
result += "]";
}
public override bool __373(JsonBuffer src)
{
Clear();
if ( src.__415()!='[' )
return false;
src.Add(1);
return __411(src, this, "", true)!=-1;
}
public override bool __373(Stream stream)
{
if ( stream==null )
return false;
return __373(Json.__370(stream));
}
public override JsonValue __379()
{
JsonArray value = new JsonArray();
for ( int i=0 ; i<__67() ; i++ )
value.Add(m_items[i].__379());
return value;
}
public override string GetValue()
{
return "";
}
public void Clear()
{
m_items.Clear();
}
public int __67()
{
return m_items.Count;
}
public void __380(JsonArray trg)
{
if ( trg==null )
return;
trg.Clear();
for ( int i=0 ; i<__67() ; i++ )
trg.m_items.Add(m_items[i]);
m_items.Clear();
}
public JsonValue Add(JsonValue value)
{
if ( value==null )
return null;
m_items.Add(value);
return value;
}
public JsonObj __389()
{
return (JsonObj)Add(new JsonObj());
}
public JsonString __381(string value)
{
return (JsonString)Add(new JsonString(value));
}
public JsonNumber __382(int value)
{
return (JsonNumber)Add(new JsonNumber(value));
}
public void __383(int sid)
{
Add(new JsonNumber(sid));
}
public JsonNumber __384(float value)
{
return (JsonNumber)Add(new JsonNumber(value));
}
public JsonConst __385(bool value)
{
return (JsonConst)Add(new JsonConst(value ? SUBTYPE.TRUE : SUBTYPE.FALSE));
}
public JsonConst __386()
{
return (JsonConst)Add(new JsonConst(SUBTYPE.TRUE));
}
public JsonConst __387()
{
return (JsonConst)Add(new JsonConst(SUBTYPE.FALSE));
}
public JsonConst __388()
{
return (JsonConst)Add(new JsonConst(SUBTYPE.NULL));
}
public JsonArray __390()
{
return (JsonArray)Add(new JsonArray());
}
public void Remove(int index)
{
m_items.RemoveAt(index);
}
public int __392(JsonValue value)
{
for ( int i=0 ; i<m_items.Count ; i++ )
{
if ( m_items[i]==value )
return i;
}
return -1;
}
public JsonObj __407(string subName, string value)
{
for ( int i=0 ; i<m_items.Count ; i++ )
{
if ( m_items[i].Type()==TYPE.OBJ )
{
if ( __394(i).GetString(subName)==value )
return __394(i);
}
}
return null;
}
public JsonObj __407(string subName, int value)
{
for ( int i=0 ; i<m_items.Count ; i++ )
{
if ( m_items[i].Type()==TYPE.OBJ )
{
if ( __394(i).GetInt(subName)==value )
return __394(i);
}
}
return null;
}
public JsonValue Get(int index)
{
return m_items[index];
}
public JsonObj __394(int index)
{
if ( m_items[index].Type()!=TYPE.OBJ )
return null;
return (JsonObj)m_items[index];
}
public JsonArray __395(int index)
{
if ( m_items[index].Type()!=TYPE.ARRAY )
return null;
return (JsonArray)m_items[index];
}
public JsonString SetString(int index, string value)
{
if ( m_items[index].Type()!=TYPE.STRING )
m_items[index] = new JsonString();
((JsonString)m_items[index]).m_value = value;
return (JsonString)m_items[index];
}
public string GetString(int index)
{
if ( m_items[index].Type()!=TYPE.STRING )
return "";
return ((JsonString)m_items[index]).m_value;
}
public JsonNumber SetInt(int index, int value)
{
if ( m_items[index].Type()!=TYPE.NUMBER )
m_items[index] = new JsonNumber();
((JsonNumber)m_items[index]).SetInt(value);
return (JsonNumber)m_items[index];
}
public int GetInt(int index)
{
if ( m_items[index].Type()!=TYPE.NUMBER )
return 0;
return ((JsonNumber)m_items[index]).GetInt();
}
public JsonNumber SetFloat(int index, float value)
{
if ( m_items[index].Type()!=TYPE.NUMBER )
m_items[index] = new JsonNumber();
((JsonNumber)m_items[index]).SetFloat(value);
return (JsonNumber)m_items[index];
}
public float GetFloat(int index)
{
if ( m_items[index].Type()!=TYPE.NUMBER )
return 0.0f;
return ((JsonNumber)m_items[index]).GetFloat();
}
public JsonConst __398(int index, SUBTYPE val)
{
if ( m_items[index].Type()!=TYPE.CONST )
m_items[index] = new JsonConst();
((JsonConst)m_items[index]).m_value = val;
return (JsonConst)m_items[index];
}
public SUBTYPE __399(int index)
{
if ( m_items[index].Type()!=TYPE.CONST )
return 0;
return ((JsonConst)m_items[index]).m_value;
}
public JsonConst __400(int index, bool value)
{
return __398(index, value ? SUBTYPE.TRUE : SUBTYPE.FALSE);
}
public bool __401(int index)
{
return m_items[index].__405();
}
public JsonConst __403(int index)
{
return __398(index, SUBTYPE.NULL);
}
public bool __404(int index)
{
return m_items[index].__404();
}
public bool __405(int index)
{
return m_items[index].__405();
}
public bool __406(int index)
{
return m_items[index].__406();
}
}
public class JsonString : JsonValue
{
public string m_value;
public static implicit operator bool(JsonString inst) { return inst!=null; }
public JsonString()
{
m_valueType = TYPE.STRING;
m_value = "";
}
public JsonString(string value)
{
m_valueType = TYPE.STRING;
m_value = value;
}
public override void __378(Stream stream)
{
if ( stream==null )
return;
Json.Write(stream, "\"");
Json.Write(stream, Json.__371(m_value));
Json.Write(stream, "\"");
}
public override void __378(ref string result)
{
result += "\"";
result += Json.__371(m_value);
result += "\"";
}
public override bool __373(JsonBuffer src)
{
return false;
}
public override bool __373(Stream stream)
{
if ( stream==null )
return false;
return __373(Json.__370(stream));
}
public override JsonValue __379()
{
JsonString value = new JsonString();
value.m_value = m_value;
return value;
}
public override string GetValue()
{
return m_value;
}
}
public class JsonNumber : JsonValue
{
public SUBTYPE m_type;
int m_int;
float m_float;
public static implicit operator bool(JsonNumber inst) { return inst!=null; }
public JsonNumber()
{
m_valueType = TYPE.NUMBER;
m_type = SUBTYPE.INTEGER;
m_int = 0;
m_float = 0.0f;
}
public JsonNumber(int value)
{
m_valueType = TYPE.NUMBER;
SetInt(value);
}
public JsonNumber(float value)
{
m_valueType = TYPE.NUMBER;
SetFloat(value);
}
public override void __378(Stream stream)
{
if ( stream==null )
return;
Json.Write(stream, GetValue());
}
public override void __378(ref string result)
{
result += GetValue();
}
public override bool __373(JsonBuffer src)
{
return false;
}
public override bool __373(Stream stream)
{
if ( stream==null )
return false;
return __373(Json.__370(stream));
}
public override JsonValue __379()
{
JsonNumber value = new JsonNumber();
value.m_type = m_type;
value.m_int = m_int;
value.m_float = m_float;
return value;
}
public override string GetValue()
{
switch ( m_type )
{
case SUBTYPE.INTEGER:	return m_int.ToString();
case SUBTYPE.FLOAT:		return m_float.ToString(CultureInfo.InvariantCulture);
}
return "";
}
public void SetInt(int value)
{
m_int = value;
m_type = SUBTYPE.INTEGER;
}
public int GetInt()
{
switch ( m_type )
{
case SUBTYPE.INTEGER:	return (int)m_int;
case SUBTYPE.FLOAT:		return (int)m_float;
}
return 0;
}
public void SetFloat(float value)
{
m_float = value;
m_type = SUBTYPE.FLOAT;
}
public float GetFloat()
{
switch ( m_type )
{
case SUBTYPE.INTEGER:	return (float)m_int;
case SUBTYPE.FLOAT:		return m_float;
}
return 0.0f;
}
}
public class JsonConst : JsonValue
{
public SUBTYPE m_value;
public static implicit operator bool(JsonConst inst) { return inst!=null; }
public JsonConst()
{
m_valueType = TYPE.CONST;
m_value = SUBTYPE.NULL;
}
public JsonConst(SUBTYPE value)
{
m_valueType = TYPE.CONST;
m_value = value;
}
public override void __378(Stream stream)
{
if ( stream==null )
return;
Json.Write(stream, GetValue());
}
public override void __378(ref string result)
{
result += GetValue();
}
public override bool __373(JsonBuffer src)
{
return false;
}
public override bool __373(Stream stream)
{
if ( stream==null )
return false;
return __373(Json.__370(stream));
}
public override JsonValue __379()
{
return new JsonConst(m_value);
}
public override string GetValue()
{
switch ( m_value )
{
case SUBTYPE.FALSE:	return "false";
case SUBTYPE.TRUE:	return "true";
case SUBTYPE.NULL:	return "null";
}
return "";
}
public override bool __404()
{
return m_value==SUBTYPE.NULL;
}
public override bool __405()
{
return m_value==SUBTYPE.TRUE;
}
public override bool __406()
{
return m_value==SUBTYPE.FALSE;
}
public void __408()
{
m_value = SUBTYPE.TRUE;
}
public void __409()
{
m_value = SUBTYPE.FALSE;
}
public void __403()
{
m_value = SUBTYPE.NULL;
}
}
public abstract class JsonValue
{
public enum TYPE
{
NONE,
OBJ,
ARRAY,
STRING,
NUMBER,
CONST,
};
public enum SUBTYPE
{
ERROR,
TRUE,
FALSE,
NULL,
INTEGER,
FLOAT,
};
protected TYPE m_valueType = TYPE.NONE;
public static implicit operator bool(JsonValue inst) { return inst!=null; }
public abstract void __378(ref string result);
public abstract void __378(Stream file);
public abstract bool __373(JsonBuffer src);
public abstract bool __373(Stream stream);
public abstract JsonValue __379();
public abstract string GetValue();
public virtual bool __404() { return false; }
public virtual bool __405() { return false; }
public virtual bool __406() { return false; }
public TYPE Type() { return m_valueType; }
public JsonObj __376()
{
if ( Type()!=TYPE.OBJ )
return null;
return (JsonObj)this;
}
public JsonArray __377()
{
if ( Type()!=TYPE.ARRAY )
return null;
return (JsonArray)this;
}
public static int __410(JsonBuffer src, JsonValue parent)
{
JsonBuffer buf = src;
string name = "";
bool hasName = false;
bool hasNameSep = false;
bool hasQuote = false;
while ( buf.__416()==false )
{
char c = buf.__415();
if ( hasQuote )
{
switch ( c )
{
case '\"':
{
hasQuote = false;
hasName = true;
break;
}
case '\\':
{
if ( hasQuote==false )
{
return -1;
}
switch ( buf.__415(1) )
{
case '\"':	name += '\"'; break;
case '\\':	name += '\\'; break;
case 'b':	name += '\b'; break;
case 'f':	name += '\f'; break;
case 'n':	name += '\n'; break;
case 'r':	name += '\r'; break;
case 't':	name += '\t'; break;
default:
return -1;
}
buf.Add(1);
break;
}
default:
{
name += c;
break;
}
}
}
else
{
switch ( c )
{
case '\"':
{
if ( hasName )
return -1;
hasQuote = true;
break;
}
case ':':
{
if ( hasName==false )
return -1;
hasNameSep = true;
buf.Add(1);
int res = __411(buf, parent, name, false);
if ( res==-1 )
return -1;
buf.Set(res);
continue;
}
case ',':
{
if ( hasName==false || hasNameSep==false )
return -1;
hasName = false;
hasNameSep = false;
name = "";
break;
}
case '}':
{
if ( hasName && hasNameSep==false )
return -1;
return buf.Get()+1;
}
case ']':
{
if ( hasName && hasNameSep==false )
return -1;
return buf.Get()+1;
}
case ' ': case '\t': case '\r': case '\n':
{
break;
}
default:
{
return -1;
}
}
}
buf.Add(1);
}
return -1;
}
public static int __411(JsonBuffer src, JsonValue parent, string name, bool noName)
{
JsonBuffer buf = src;
TYPE type = TYPE.NONE;
while ( buf.__416()==false )
{
char c = buf.__415();
switch ( c )
{
case '{':
{
if ( type!=TYPE.NONE )
return -1;
type = TYPE.OBJ;
JsonValue pNew = null;
if ( noName )
{
JsonArray array = (JsonArray)parent;
pNew = array.Add(new JsonObj());
}
else
{
JsonObj obj = (JsonObj)parent;
pNew = obj.Add(name, new JsonObj());
}
buf.Add(1);
int res = __410(buf, pNew);
if ( res==-1 )
return -1;
buf.Set(res);
continue;
}
case '[':
{
if ( type!=TYPE.NONE )
return -1;
type = TYPE.ARRAY;
JsonValue pNew = null;
if ( noName )
{
JsonArray array = (JsonArray)parent;
pNew = array.Add(new JsonArray());
}
else
{
JsonObj obj = (JsonObj)parent;
pNew = obj.Add(name, new JsonArray());
}
buf.Add(1);
int res = __411(buf, pNew, "", true);
if ( res==-1 )
return -1;
buf.Set(res);
break;
}
case '\"':
{
if ( type!=TYPE.NONE )
return -1;
type = TYPE.STRING;
buf.Add(1);
int res = __413(buf, parent, name, noName);
if ( res==-1 )
return -1;
buf.Set(res);
continue;
}
case '0': case '1': case '2': case '3': case '4': case '5': case '6': case '7': case '8': case '9': case '-':
{
if ( type!=TYPE.NONE )
return -1;
type = TYPE.NUMBER;
int res = __412(buf, parent, name, noName);
if ( res==-1 )
return -1;
buf.Set(res);
continue;
}
case 'n': case 't': case 'f':
{
if ( type!=TYPE.NONE )
return -1;
type = TYPE.CONST;
int res = __414(buf, parent, name, noName);
if ( res==-1 )
return -1;
buf.Set(res);
continue;
}
case ',':
{
if ( type==TYPE.NONE )
return -1;
if ( noName )
type = TYPE.NONE;
else
return buf.Get();
break;
}
case '}':
case ']':
{
return buf.Get();
}
case ' ': case '\t': case '\r': case '\n':
{
break;
}
default:
{
return -1;
}
}
buf.Add(1);
}
return -1;
}
public static int __412(JsonBuffer src, JsonValue parent, string name, bool noName)
{
JsonBuffer buf = src;
string value = "";
while ( buf.__416()==false )
{
char c = buf.__415();
switch ( c )
{
case '0': case '1': case '2': case '3': case '4': case '5': case '6': case '7': case '8': case '9': case '-': case '+': case '.': case 'e': case 'E':
{
value += c;
break;
}
case ',':
case '}':
case ']':
{
float dbl = G.__115(ref value);
if ( noName )
{
JsonArray array = (JsonArray)parent;
if ( value.IndexOf('.')==-1 )
{
int integer;
if ( int.TryParse(value, out integer)==false )
return -1;
array.Add(new JsonNumber(integer));
}
else
array.Add(new JsonNumber(dbl));
}
else
{
JsonObj obj = (JsonObj)parent;
if ( value.IndexOf('.')==-1 )
{
int integer;
if ( int.TryParse(value, out integer)==false )
return -1;
obj.Add(name, new JsonNumber(integer));
}
else
obj.Add(name, new JsonNumber(dbl));
}
return buf.Get();
}
case ' ': case '\t': case '\r': case '\n':
{
break;
}
default:
{
return -1;
}
}
buf.Add(1);
}
return -1;
}
public static int __413(JsonBuffer src, JsonValue parent, string name, bool noName)
{
JsonBuffer buf = src;
string value = "";
bool hasQuote = true;
while ( buf.__416()==false )
{
char c = buf.__415();
switch ( c )
{
case '\"':
{
if ( hasQuote==false )
return -1;
hasQuote = false;
break;
}
case '\\':
{
if ( hasQuote==false )
return -1;
switch ( buf.__415(1) )
{
case '\"':	value += '\"'; break;
case '\\':	value += '\\'; break;
case '/':	value += '/'; break;
case 'b':	value += '\b'; break;
case 'f':	value += '\f'; break;
case 'n':	value += '\n'; break;
case 'r':	value += '\r'; break;
case 't':	value += '\t'; break;
default:
return -1;
}
buf.Add(1);
break;
}
case ',':
case '}':
case ']':
{
if ( hasQuote )
value += c;
else
{
if ( noName )
{
JsonArray array = (JsonArray)parent;
array.Add(new JsonString(value));
}
else
{
JsonObj obj = (JsonObj)parent;
obj.Add(name, new JsonString(value));
}
return buf.Get();
}
break;
}
case ' ':
{
if ( hasQuote )
value += c;
break;
}
case '\t': case '\r': case '\n':
{
if ( hasQuote )
return -1;
break;
}
default:
{
if ( hasQuote )
value += c;
break;
}
}
buf.Add(1);
}
return -1;
}
public static int __414(JsonBuffer src, JsonValue parent, string name, bool noName)
{
JsonBuffer buf = src;
SUBTYPE type = SUBTYPE.ERROR;
switch ( buf.__415() )
{
case 'n':
{
if ( buf.__415(1)!='u' || buf.__415(2)!='l' || buf.__415(3)!='l' )
return -1;
type = SUBTYPE.NULL;
buf.Add(4);
break;
}
case 't':
{
if ( buf.__415(1)!='r' || buf.__415(2)!='u' || buf.__415(3)!='e' )
return -1;
type = SUBTYPE.TRUE;
buf.Add(4);
break;
}
case 'f':
{
if ( buf.__415(1)!='a' || buf.__415(2)!='l' || buf.__415(3)!='s' || buf.__415(4)!='e' )
return -1;
type = SUBTYPE.FALSE;
buf.Add(5);
break;
}
}
if ( type==SUBTYPE.ERROR )
return -1;
if ( noName )
{
JsonArray array = (JsonArray)parent;
array.Add(new JsonConst(type));
}
else
{
JsonObj obj = (JsonObj)parent;
obj.Add(name, new JsonConst(type));
}
while ( buf.__416()==false )
{
switch ( buf.__415() )
{
case ',':
case '}':
case ']':
return buf.Get();
}
buf.Add(1);
}
return -1;
}
}
public struct JsonBuffer
{
public char[] data;
int offset;
public JsonBuffer(int size)
{
data = new char[size];
offset = 0;
}
public char __415(int index = 0)
{
if ( offset+index>=data.Length )
return '\0';
return data[offset+index];
}
public bool __416()
{
if ( offset>=data.Length )
return true;
return false;
}
public void Set(int index)
{
offset = index;
}
public int Get()
{
return offset;
}
public void Add(int count)
{
offset += count;
}
}
