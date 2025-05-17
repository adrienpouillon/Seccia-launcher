using UnityEngine;
using System;
public class Term
{
public bool m_single;
public string[] m_values;
public Serial<int> m_sub;
public static implicit operator bool(Term inst) { return inst!=null; }
public Term(bool single = false)
{
m_single = single;
if ( m_single )
m_values = new string[1];
else
m_values = new string[G.m_game.m_languages.Length];
}
public string Get()
{
string text = m_single ? m_values[0] : m_values[G.m_game.m_optionLanguageText];
if ( text==null || text.Length==0 )
return "";
G.m_game.__289(ref text);
int index = 0;
while ( index<=text.Length && (index=text.IndexOf('[', index))!=-1 )
{
int index2 = text.IndexOf(']', index);
if ( index2==-1 )
break;
string item = text.Substring(index+1, index2-index-1);
string name = G.__96(ref item);
Obj obj = G.m_game.__277(name);
if ( obj )
{
string title = obj.m_title.Get();
text = text.Substring(0, index) + title + text.Substring(index2+1);
index = title.Length + 1;
}
else
{
Variable var = G.m_game.__287(ref name);
if ( var )
{
text = text.Substring(0, index) + var.m_value + text.Substring(index2+1);
index = var.m_value.Length + 1;
}
else
index = index2 + 1;
}
}
string[] subs = text.Split('|');
if ( m_sub.cur<0 || m_sub.cur>=subs.Length )
return text;
return subs[m_sub.cur];
}
public string[] GetParagraphs()
{
string text = Get();
string[] subs = text.Split('|');
if ( m_sub.cur>=0 && m_sub.cur<subs.Length )
text = subs[m_sub.cur];
if ( text.Length==0 )
{
string[] result = new string[1];
result[0] = "";
return result;
}
int count = 0;
for ( int i=0 ; i<text.Length ; i++ )
{
if ( text[i]==2 )
count++;
}
if ( count==0 )
{
string[] result = new string[1];
result[0] = text;
return result;
}
string[] paragraphs = new string[count+1];
int iPrev = 0;
int iCur = 0;
int index = 0;
while ( (iCur=text.IndexOf((char)2, iCur))!=-1 )
{
paragraphs[index] = text.Substring(iPrev, iCur-iPrev);
iCur++;
iPrev = iCur;
index++;
}
paragraphs[index] = text.Substring(iPrev);
return paragraphs;
}
}
