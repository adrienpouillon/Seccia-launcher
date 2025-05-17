using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
public class FileBehavior : MonoBehaviour
{
public static FileBehavior m_instance = null;
List<FileItem> m_items = new List<FileItem>();
private float m_startTime = 0.0f;
public float m_progress = 0.0f;
void Awake()
{
m_instance = this;
}
void Start()
{
}
public bool IsBusy(float duration = 0.0f)
{
bool busy = m_items.Count!=0;
if ( busy==false || duration==0.0f )
return busy;
return Time.time-m_startTime>duration;
}
public int GetProgress()
{
return (int)(m_progress*100.0f);
}
public void OpenAudioFile(Sound sound, string path)
{
if ( sound==null )
return;
FileItem item = new FileItem();
item.m_sound = sound;
item.m_path = path;
m_items.Add(item);
if ( m_items.Count==1 )
{
m_startTime = Time.time;
StartCoroutine(Streaming());
}
}
public void Cancel(Sound sound)
{
for ( int i=0 ; i<m_items.Count ; i++ )
{
if ( m_items[i].m_sound==sound )
{
m_items[i].m_cancelled = true;
if ( m_items[i].m_www!=null )
m_items[i].m_www.Abort();
}
}
}
IEnumerator Streaming()
{
while ( m_items.Count>0 )
{
FileItem item = m_items[0];
if ( item.m_cancelled )
{
m_items.RemoveAt(0);
}
else
{
if ( item.m_www==null )
{
AudioType audioType = AudioType.WAV;
if ( item.m_sound.m_type!=Sound.TYPE.SFX )
{
if ( G.m_audioExtension==".mp3" )
audioType = AudioType.MPEG;
else
audioType = AudioType.OGGVORBIS;
}
item.m_www = UnityWebRequestMultimedia.GetAudioClip(item.m_path, audioType);
item.m_www.SendWebRequest();
}
if ( item.m_www.isDone )
{
if ( item.m_www.result==UnityWebRequest.Result.Success )
item.m_sound.__983(item.m_www, item.m_path);
m_items.RemoveAt(0);
}
}
m_progress = m_items.Count==0 ? 1.0f : (m_items[0].m_www!=null ? m_items[0].m_www.downloadProgress : 0.0f);
if ( m_items.Count>0 )
yield return null;
}
}
}
public class FileItem
{
public Sound m_sound = null;
public string m_path = "";
public UnityWebRequest m_www = null;
public bool m_cancelled = false;
public float m_progress = 0.0f;
}
