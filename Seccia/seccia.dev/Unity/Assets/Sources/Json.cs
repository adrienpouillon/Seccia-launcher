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
public static JsonBuffer __369(Stream stream)
{
int count = (int)(stream.Length - stream.Position);
byte[] buffer = new byte[count];
stream.Read(buffer, 0, count);
JsonBuffer buf = new JsonBuffer(count);
for ( int i=0 ; i<count ; i++ )
buf.data[i] = (char)buffer[i];
return buf;
}
public static JsonBuffer __369(Asset asset)
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
public static string __370(string src)
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
public static string __371(string src)
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
public static JsonValue __372(ref string data)
{
JsonBuffer buf = new JsonBuffer(data.Length);
for ( int i=0 ; i<data.Length ; i++ )
buf.data[i] = data[i];
return __372(buf);
}
public static JsonValue __372(byte[] data)
{
if ( data==null )
return null;
JsonBuffer buf = new JsonBuffer(data.Length);
for ( int i=0 ; i<data.Length ; i++ )
buf.data[i] = (char)data[i];
return __372(buf);
}
public static JsonValue __372(Stream stream)
{
if ( stream==null )
return null;
return __372(__369(stream));
}
public static JsonValue __372(JsonBuffer content)
{
JsonBuffer buf = content;
while ( buf.__415()==false )
{
if ( buf.__414()=='{' )
{
buf.Add(1);
JsonObj obj = new JsonObj();
if ( JsonValue.__409(buf, obj)==-1 )
return null;
return obj;
}
if ( buf.__414()=='[' )
{
buf.Add(1);
JsonArray array = new JsonArray();
if ( JsonValue.__410(buf, array, "", true)==-1 )
return null;
return array;
}
buf.Add(1);
}
return null;
}
public static JsonObj __373()
{
return new JsonObj();
}
public static JsonArray __374()
{
return new JsonArray();
}
public static JsonObj __375(JsonValue json)
{
if ( json==null )
return null;
return json.__375();
}
public static JsonArray __376(JsonValue json)
{
if ( json==null )
return null;
return json.__376();
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
public override void __377(ref string result)
{
result += "{";
int count = __66();
for ( int i=0 ; i<count ; i++ )
{
result += "\"";
result += Json.__370(m_names[i]);
result += "\":";
m_values[i].__377(ref result);
if ( i+1<count )
result += ",";
}
result += "}";
}
public override void __377(Stream stream)
{
if ( stream==null )
return;
int count = __66();
if ( count==0 )
{
Json.Write(stream, "{}");
return;
}
Json.Write(stream, "{");
for ( int i=0 ; i<count ; i++ )
{
Json.Write(stream, "\"");
Json.Write(stream, Json.__370(m_names[i]));
Json.Write(stream, "\":");
m_values[i].__377(stream);
if ( i+1<count )
Json.Write(stream, ",");
}
Json.Write(stream, "}");
}
public override bool __372(JsonBuffer src)
{
Clear();
if ( src.__414()!='{' )
return false;
src.Add(1);
return __409(src, this)!=-1;
}
public override bool __372(Stream stream)
{
if ( stream==null )
return false;
return __372(Json.__369(stream));
}
public override JsonValue __378()
{
JsonObj value = new JsonObj();
for ( int i=0 ; i<__66() ; i++ )
value.Add(m_names[i], m_values[i].__378());
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
public int __66()
{
return m_names.Count;
}
public void __379(JsonObj trg)
{
if ( trg==null )
return;
trg.Clear();
for ( int i=0 ; i<__66() ; i++ )
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
public JsonString __380(string name, string value)
{
return (JsonString)Add(name, new JsonString(value));
}
public JsonNumber __381(string name, int value)
{
return (JsonNumber)Add(name, new JsonNumber(value));
}
public void __382(string name, int sid)
{
if ( sid>0 )
Add(name, new JsonNumber(sid));
}
public JsonNumber __383(string name, float value)
{
return (JsonNumber)Add(name, new JsonNumber(value));
}
public JsonConst __384(string name, bool value)
{
return (JsonConst)Add(name, new JsonConst(value ? SUBTYPE.TRUE : SUBTYPE.FALSE));
}
public JsonConst __385(string name)
{
return (JsonConst)Add(name, new JsonConst(SUBTYPE.TRUE));
}
public JsonConst __386(string name)
{
return (JsonConst)Add(name, new JsonConst(SUBTYPE.FALSE));
}
public JsonConst __387(string name)
{
return (JsonConst)Add(name, new JsonConst(SUBTYPE.NULL));
}
public JsonObj __388(string name)
{
return (JsonObj)Add(name, new JsonObj());
}
public JsonArray __389(string name)
{
return (JsonArray)Add(name, new JsonArray());
}
public void Remove(string name)
{
Remove(__391(name));
}
public void Remove(int index)
{
m_names.RemoveAt(index);
m_values.RemoveAt(index);
}
public bool __390(string name)
{
return Get(name);
}
public int __391(string name)
{
for ( int i=0 ; i<m_values.Count ; i++ )
{
if ( m_names[i]==name )
return i;
}
return -1;
}
public int __391(JsonValue value)
{
for ( int i=0 ; i<m_values.Count ; i++ )
{
if ( m_values[i]==value )
return i;
}
return -1;
}
public string __392(int index)
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
public JsonObj __393(string name, bool create = false)
{
JsonValue value = Get(name);
if ( value==null )
{
if ( create )
return __388(name);
return null;
}
if ( value.Type()!=TYPE.OBJ )
return null;
return (JsonObj)value;
}
public JsonObj __393(int index)
{
if ( m_values[index].Type()!=TYPE.OBJ )
return null;
return (JsonObj)m_values[index];
}
public JsonArray __394(string name, bool create = false)
{
JsonValue value = Get(name);
if ( value==null )
{
if ( create )
return __389(name);
return null;
}
if ( value.Type()!=TYPE.ARRAY )
return null;
return (JsonArray)value;
}
public JsonArray __394(int index)
{
if ( m_values[index].Type()!=TYPE.ARRAY )
return null;
return (JsonArray)m_values[index];
}
public JsonString SetString(string name, string val)
{
JsonValue value = Get(name);
if ( value==null )
return __380(name, val);
if ( value.Type()!=TYPE.STRING )
{
int index = __391(value);
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
return __381(name, val);
if ( value.Type()!=TYPE.NUMBER )
{
int index = __391(value);
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
public bool __395(string name)
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
return __383(name, val);
if ( value.Type()!=TYPE.NUMBER )
{
int index = __391(value);
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
public bool __396(string name)
{
JsonValue value = Get(name);
if ( value==null )
return false;
if ( value.Type()!=TYPE.NUMBER )
return false;
return ((JsonNumber)value).m_type==SUBTYPE.FLOAT;
}
public JsonConst __397(string name, SUBTYPE val)
{
JsonValue value = Get(name);
if ( value==null )
return (JsonConst)Add(name, new JsonConst(val));
if ( value.Type()!=TYPE.CONST )
{
int index = __391(value);
value = new JsonConst();
m_values[index] = value;
}
((JsonConst)value).m_value = val;
return (JsonConst)value;
}
public JsonConst __397(int index, SUBTYPE val)
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
public SUBTYPE __398(string name)
{
JsonValue value = Get(name);
if ( value==null )
return 0;
if ( value.Type()!=TYPE.CONST )
return 0;
return ((JsonConst)value).m_value;
}
public SUBTYPE __398(int index)
{
if ( m_values[index].Type()!=TYPE.CONST )
return SUBTYPE.ERROR;
return ((JsonConst)m_values[index]).m_value;
}
public JsonConst __399(string name, bool value)
{
return __397(name, value ? SUBTYPE.TRUE : SUBTYPE.FALSE);
}
public JsonConst __399(int index, bool value)
{
return __397(index, value ? SUBTYPE.TRUE : SUBTYPE.FALSE);
}
public bool __400(string name)
{
JsonValue value = Get(name);
if ( value==null )
return false;
return value.__404();
}
public bool __400(int index)
{
return m_values[index].__404();
}
public bool __401(string name)
{
JsonValue value = Get(name);
if ( value==null || value.Type()!=TYPE.CONST )
return false;
return ((JsonConst)value).__403()==false;
}
public JsonConst __402(string name)
{
return __397(name, SUBTYPE.NULL);
}
public JsonConst __402(int index)
{
return __397(index, SUBTYPE.NULL);
}
public bool __403(string name)
{
JsonValue value = Get(name);
if ( value==null )
return false;
return value.__403();
}
public bool __403(int index)
{
return m_values[index].__403();
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
public override void __377(Stream stream)
{
if ( stream==null )
return;
Json.Write(stream, "[");
int count = __66();
for ( int i=0 ; i<count ; i++ )
{
m_items[i].__377(stream);
if ( i+1<count )
Json.Write(stream, ",");
}
Json.Write(stream, "]");
}
public override void __377(ref string result)
{
result += "[";
int count = __66();
for ( int i=0 ; i<count ; i++ )
{
m_items[i].__377(ref result);
if ( i+1<count )
result += ",";
}
result += "]";
}
public override bool __372(JsonBuffer src)
{
Clear();
if ( src.__414()!='[' )
return false;
src.Add(1);
return __410(src, this, "", true)!=-1;
}
public override bool __372(Stream stream)
{
if ( stream==null )
return false;
return __372(Json.__369(stream));
}
public override JsonValue __378()
{
JsonArray value = new JsonArray();
for ( int i=0 ; i<__66() ; i++ )
value.Add(m_items[i].__378());
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
public int __66()
{
return m_items.Count;
}
public void __379(JsonArray trg)
{
if ( trg==null )
return;
trg.Clear();
for ( int i=0 ; i<__66() ; i++ )
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
public JsonObj __388()
{
return (JsonObj)Add(new JsonObj());
}
public JsonString __380(string value)
{
return (JsonString)Add(new JsonString(value));
}
public JsonNumber __381(int value)
{
return (JsonNumber)Add(new JsonNumber(value));
}
public void __382(int sid)
{
Add(new JsonNumber(sid));
}
public JsonNumber __383(float value)
{
return (JsonNumber)Add(new JsonNumber(value));
}
public JsonConst __384(bool value)
{
return (JsonConst)Add(new JsonConst(value ? SUBTYPE.TRUE : SUBTYPE.FALSE));
}
public JsonConst __385()
{
return (JsonConst)Add(new JsonConst(SUBTYPE.TRUE));
}
public JsonConst __386()
{
return (JsonConst)Add(new JsonConst(SUBTYPE.FALSE));
}
public JsonConst __387()
{
return (JsonConst)Add(new JsonConst(SUBTYPE.NULL));
}
public JsonArray __389()
{
return (JsonArray)Add(new JsonArray());
}
public void Remove(int index)
{
m_items.RemoveAt(index);
}
public int __391(JsonValue value)
{
for ( int i=0 ; i<m_items.Count ; i++ )
{
if ( m_items[i]==value )
return i;
}
return -1;
}
public JsonObj __406(string subName, string value)
{
for ( int i=0 ; i<m_items.Count ; i++ )
{
if ( m_items[i].Type()==TYPE.OBJ )
{
if ( __393(i).GetString(subName)==value )
return __393(i);
}
}
return null;
}
public JsonObj __406(string subName, int value)
{
for ( int i=0 ; i<m_items.Count ; i++ )
{
if ( m_items[i].Type()==TYPE.OBJ )
{
if ( __393(i).GetInt(subName)==value )
return __393(i);
}
}
return null;
}
public JsonValue Get(int index)
{
return m_items[index];
}
public JsonObj __393(int index)
{
if ( m_items[index].Type()!=TYPE.OBJ )
return null;
return (JsonObj)m_items[index];
}
public JsonArray __394(int index)
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
public JsonConst __397(int index, SUBTYPE val)
{
if ( m_items[index].Type()!=TYPE.CONST )
m_items[index] = new JsonConst();
((JsonConst)m_items[index]).m_value = val;
return (JsonConst)m_items[index];
}
public SUBTYPE __398(int index)
{
if ( m_items[index].Type()!=TYPE.CONST )
return 0;
return ((JsonConst)m_items[index]).m_value;
}
public JsonConst __399(int index, bool value)
{
return __397(index, value ? SUBTYPE.TRUE : SUBTYPE.FALSE);
}
public bool __400(int index)
{
return m_items[index].__404();
}
public JsonConst __402(int index)
{
return __397(index, SUBTYPE.NULL);
}
public bool __403(int index)
{
return m_items[index].__403();
}
public bool __404(int index)
{
return m_items[index].__404();
}
public bool __405(int index)
{
return m_items[index].__405();
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
public override void __377(Stream stream)
{
if ( stream==null )
return;
Json.Write(stream, "\"");
Json.Write(stream, Json.__370(m_value));
Json.Write(stream, "\"");
}
public override void __377(ref string result)
{
result += "\"";
result += Json.__370(m_value);
result += "\"";
}
public override bool __372(JsonBuffer src)
{
return false;
}
public override bool __372(Stream stream)
{
if ( stream==null )
return false;
return __372(Json.__369(stream));
}
public override JsonValue __378()
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
public override void __377(Stream stream)
{
if ( stream==null )
return;
Json.Write(stream, GetValue());
}
public override void __377(ref string result)
{
result += GetValue();
}
public override bool __372(JsonBuffer src)
{
return false;
}
public override bool __372(Stream stream)
{
if ( stream==null )
return false;
return __372(Json.__369(stream));
}
public override JsonValue __378()
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
public override void __377(Stream stream)
{
if ( stream==null )
return;
Json.Write(stream, GetValue());
}
public override void __377(ref string result)
{
result += GetValue();
}
public override bool __372(JsonBuffer src)
{
return false;
}
public override bool __372(Stream stream)
{
if ( stream==null )
return false;
return __372(Json.__369(stream));
}
public override JsonValue __378()
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
public override bool __403()
{
return m_value==SUBTYPE.NULL;
}
public override bool __404()
{
return m_value==SUBTYPE.TRUE;
}
public override bool __405()
{
return m_value==SUBTYPE.FALSE;
}
public void __407()
{
m_value = SUBTYPE.TRUE;
}
public void __408()
{
m_value = SUBTYPE.FALSE;
}
public void __402()
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
public abstract void __377(ref string result);
public abstract void __377(Stream file);
public abstract bool __372(JsonBuffer src);
public abstract bool __372(Stream stream);
public abstract JsonValue __378();
public abstract string GetValue();
public virtual bool __403() { return false; }
public virtual bool __404() { return false; }
public virtual bool __405() { return false; }
public TYPE Type() { return m_valueType; }
public JsonObj __375()
{
if ( Type()!=TYPE.OBJ )
return null;
return (JsonObj)this;
}
public JsonArray __376()
{
if ( Type()!=TYPE.ARRAY )
return null;
return (JsonArray)this;
}
public static int __409(JsonBuffer src, JsonValue parent)
{
JsonBuffer buf = src;
string name = "";
bool hasName = false;
bool hasNameSep = false;
bool hasQuote = false;
while ( buf.__415()==false )
{
char c = buf.__414();
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
switch ( buf.__414(1) )
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
int res = __410(buf, parent, name, false);
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
public static int __410(JsonBuffer src, JsonValue parent, string name, bool noName)
{
JsonBuffer buf = src;
TYPE type = TYPE.NONE;
while ( buf.__415()==false )
{
char c = buf.__414();
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
int res = __409(buf, pNew);
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
int res = __410(buf, pNew, "", true);
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
int res = __412(buf, parent, name, noName);
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
int res = __411(buf, parent, name, noName);
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
int res = __413(buf, parent, name, noName);
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
public static int __411(JsonBuffer src, JsonValue parent, string name, bool noName)
{
JsonBuffer buf = src;
string value = "";
while ( buf.__415()==false )
{
char c = buf.__414();
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
float dbl = G.__114(ref value);
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
public static int __412(JsonBuffer src, JsonValue parent, string name, bool noName)
{
JsonBuffer buf = src;
string value = "";
bool hasQuote = true;
while ( buf.__415()==false )
{
char c = buf.__414();
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
switch ( buf.__414(1) )
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
public static int __413(JsonBuffer src, JsonValue parent, string name, bool noName)
{
JsonBuffer buf = src;
SUBTYPE type = SUBTYPE.ERROR;
switch ( buf.__414() )
{
case 'n':
{
if ( buf.__414(1)!='u' || buf.__414(2)!='l' || buf.__414(3)!='l' )
return -1;
type = SUBTYPE.NULL;
buf.Add(4);
break;
}
case 't':
{
if ( buf.__414(1)!='r' || buf.__414(2)!='u' || buf.__414(3)!='e' )
return -1;
type = SUBTYPE.TRUE;
buf.Add(4);
break;
}
case 'f':
{
if ( buf.__414(1)!='a' || buf.__414(2)!='l' || buf.__414(3)!='s' || buf.__414(4)!='e' )
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
while ( buf.__415()==false )
{
switch ( buf.__414() )
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
public char __414(int index = 0)
{
if ( offset+index>=data.Length )
return '\0';
return data[offset+index];
}
public bool __415()
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
