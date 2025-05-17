using UnityEngine;
using System.Collections.Generic;
using System.IO;
public class Dialog
{
public int m_sid;
public string m_uid;
public string[] m_tags;
public Sentence m_root;
public int[] m_entryPointIDs;
public Dictionary<int, Sentence> m_sentencesByID = new Dictionary<int, Sentence>();
public static implicit operator bool(Dialog inst) { return inst!=null; }
public void Reset()
{
m_root.Reset();
}
public void __46(JsonObj json)
{
json.__382("sid", m_sid);
json.__380("uid", m_uid);
json.__380("tag", m_tags[0]);
JsonArray jSentences = json.__389("sentences");
m_root.__46(jSentences);
}
public void __47(JsonObj json)
{
m_tags[0] = json.GetString("tag");
JsonArray jSentences = json.__394("sentences");
if ( jSentences )
{
for ( int i=0 ; i<jSentences.__66() ; i++ )
{
JsonObj jSentence = jSentences.__393(i);
if ( jSentence )
{
Sentence sentence;
if ( m_sentencesByID.TryGetValue(jSentence.GetInt("id"), out sentence) )
sentence.__47(jSentence);
}
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
public Sentence __49(int id)
{
if ( id==0 )
return null;
if ( m_root.m_sid==id )
return m_root;
return m_root.__49(id);
}
public void __50(int idToShow, bool visible = true)
{
Sentence sentence = m_root.__49(idToShow);
if ( sentence==null || sentence.m_choice==false )
return;
if ( visible==false )
{
sentence.m_visible.Set(false);
return;
}
if ( sentence.m_visible.cur )
return;
sentence.m_visible.Set(true);
G.m_game.__332(sentence);
}
public void __51(int idToShow)
{
if ( __52(idToShow) )
{
Sentence sentence = __49(idToShow);
if ( sentence && sentence.m_visited==false )
{
sentence.m_visible.Set(true);
G.m_game.__332(sentence);
}
}
}
public bool __52(int idToShow)
{
return m_root.__52(idToShow);
}
}
public class Sentence
{
public Dialog m_dialog = null;
public Sentence m_parent = null;
public int m_ageIndex = 0;
public int m_sid = 0;
public string[] m_tags;
public bool m_choice = false;
public bool m_hideBranch = false;
public int m_goto = 0;
public int[] m_mutualShows = null;
public bool m_exit = false;
public bool m_task = false;
public string m_taskRole;
public string m_taskName;
public string m_taskArg;
public Serial<bool> m_visible;
public bool m_doNotSay = false;
public bool m_neverHide = false;
public bool m_locked = false;
public Obj m_icon;
public string m_speaker = "";
public int m_iDir = -1;
public string m_lookat = "";
public bool m_keepDir = false;
public string m_anim = "";
public bool m_keepAnim = false;
public bool m_randomly = false;
public bool m_entryPoint = false;
public float m_duration = 0.0f;
public SentenceVoice m_voice = null;
public Term m_text;
public Sentence[] m_subs = null;
public bool m_visited;
public bool m_unlocked;
public static implicit operator bool(Sentence inst) { return inst!=null; }
public void Reset()
{
m_visited = false;
m_unlocked = false;
m_visible.Reset();
if ( m_subs!=null )
{
for ( int i=0 ; i<m_subs.Length ; i++ )
m_subs[i].Reset();
}
}
public void __46(JsonArray json)
{
JsonObj jSentence = json.__388();
jSentence.__381("id", m_sid);
jSentence.__380("tag", m_tags[0]);
if ( m_visible.modified )
jSentence.__384("visible", m_visible.cur);
jSentence.__384("visited", m_visited);
jSentence.__384("unlocked", m_unlocked);
if ( m_subs!=null )
{
for ( int i=0 ; i<m_subs.Length ; i++ )
m_subs[i].__46(json);
}
}
public void __47(JsonObj json)
{
m_tags[0] = json.GetString("tag");
if ( json.__390("visible") )
m_visible.Set(json.__400("visible"));
m_visited = json.__400("visited");
m_unlocked = json.__400("unlocked");
}
public bool __48(ref string idOrTag)
{
if ( idOrTag.Length==0 )
return false;
if ( idOrTag[0]=='@' )
return G.__149(m_tags, idOrTag.Substring(1));
return m_sid==G.__113(ref idOrTag);
}
public bool __53()
{
if ( m_text && m_text.Get().Length==0 )
return true;
return false;
}
public Sentence __49(int id)
{
if ( id==0 || m_subs==null )
return null;
for ( int i=0 ; i<m_subs.Length ; i++ )
{
if ( m_subs[i].m_sid==id )
return m_subs[i];
Sentence sentence = m_subs[i].__49(id);
if ( sentence )
return sentence;
}
return null;
}
public bool __54(int id)
{
if ( id==0 || m_mutualShows==null )
return false;
for ( int i=0 ; i<m_mutualShows.Length ; i++ )
{
if ( m_mutualShows[i]==id )
return true;
}
return false;
}
public SceneObj __55()
{
if ( m_choice || m_parent==null )
return G.m_game.__295();
else
{
if ( m_speaker.Length==0 )
return null;
if ( m_speaker=="VOICE-OVER" )
{
Scene scene = G.m_game.__291();
return scene==null ? null : scene.m_voiceOverObj;
}
if ( m_speaker[0]=='P' || m_speaker[0]=='p' )
{
Player player = G.m_game.__279(m_speaker);
if ( player==null )
return null;
return player.m_sceneObj;
}
return G.m_game.__307("", m_speaker);
}
}
public int __56()
{
int count = 0;
for ( int i=0 ; i<m_subs.Length ; i++ )
{
if ( m_subs[i].m_visible.cur && m_subs[i].m_choice )
count++;
}
return count;
}
public int __57()
{
int count = 0;
for ( int i=0 ; i<m_subs.Length ; i++ )
{
if ( m_subs[i].m_visible.cur && m_subs[i].m_choice )
{
if ( m_subs[i].m_icon==null )
count += m_subs[i].m_text.GetParagraphs().Length;
else
count++;
}
}
return count;
}
public int __58()
{
int count = 0;
if ( m_subs==null || m_subs[0].m_choice==false )
return count;
for ( int i=0 ; i<m_subs.Length ; i++ )
{
if ( m_subs[i].m_visible.cur )
count++;
}
return count;
}
public Sentence __59()
{
if ( m_parent==null )
return null;
if ( m_parent.__58()>0 )
{
for ( int i=0 ; i<m_parent.m_subs.Length ; i++ )
{
Sentence sub = m_parent.m_subs[i];
if ( sub.m_visible.cur && sub.m_entryPoint==false )
return sub;
}
return null;
}
return m_parent.__59();
}
public bool __60(bool skipThis = true)
{
if ( skipThis==false )
{
if ( m_choice && m_visible.cur )
return true;
if ( m_choice==false && m_visible.cur && m_parent && m_parent.m_subs[0].m_choice )
return true;
}
if ( m_subs!=null )
{
for ( int i=0 ; i<m_subs.Length ; i++ )
{
if ( m_subs[i].__60(false) )
return true;
}
}
return false;
}
public void __61()
{
if ( m_neverHide )
return;
if ( __60() )
return;
m_visible.cur = false;
if ( m_parent==null )
return;
m_parent.__61();
}
public bool __52(int idToShow)
{
if ( idToShow==0 )
return false;
if ( m_visited==false && __54(idToShow) )
return false;
if ( m_subs!=null )
{
for ( int i=0 ; i<m_subs.Length ; i++ )
{
if ( m_subs[i].__52(idToShow)==false )
return false;
}
}
return true;
}
}
public class SentenceVoice
{
public List<string>[] m_paths;
public static implicit operator bool(SentenceVoice inst) { return inst!=null; }
public SentenceVoice()
{
m_paths = new List<string>[G.m_game.m_languages.Length];
for ( int i=0 ; i<m_paths.Length ; i++ )
m_paths[i] = new List<string>();
}
public string __62(int index)
{
if ( G.m_game.m_optionLanguageAudio>=m_paths.Length )
return "";
if ( index<0 || index>=m_paths[G.m_game.m_optionLanguageAudio].Count )
return "";
return m_paths[G.m_game.m_optionLanguageAudio][index];
}
}
