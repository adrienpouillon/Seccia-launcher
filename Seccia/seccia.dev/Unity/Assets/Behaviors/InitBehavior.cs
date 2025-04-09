using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class InitBehavior:MonoBehaviour
{
float m_startTime = 0.0f;
int m_state = 0;
void Awake()
{
#if UNITY_WEBGL
#else
G.__79(Screen.width, Screen.height, Screen.fullScreen);
m_startTime = Time.time;
#endif
}
void Start()
{
#if UNITY_WEBGL
#if UNITY_EDITOR
#endif
#else
m_state++;
#endif
}
private void OnDestroy()
{
Scene.__533();
}
void Update()
{
switch ( m_state )
{
case 1:
{
if ( Time.time-m_startTime>0.2f )
{
#if UNITY_ANDROID
m_state++;
StartCoroutine(OnReadObb());
#else
m_state += 2;
#endif
}
break;
}
case 3:
{
m_state++;
if ( G.__90() )
{
m_startTime = Time.time;
m_state++;
}
break;
}
case 5:
{
if ( Time.time-m_startTime>0.2f )
{
#if UNITY_EDITOR
G.m_game.__241();
#else
if ( G.m_game.m_configDebug )
G.m_game.__241();
#endif
m_state++;
}
break;
}
}
}
#if UNITY_ANDROID
IEnumerator OnReadObb()
{
G.m_obbMap.Clear();
System.IO.Stream file = G.OpenFile(G.m_obbPath);
if ( file!=null )
{
if ( G.m_obbOffset>0 )
file.Seek(G.m_obbOffset, System.IO.SeekOrigin.Begin);
int index = 0;
while ( G.__82(file)==0x04034B50 )
{
file.Position += 18;
uint size = G.__82(file);
ushort len = G.__81(file);
file.Position += 2;
string name = "";
for ( ushort i=0 ; i<len ; i++ )
name += (char)(byte)file.ReadByte();
G.m_obbMap.Add(name, index++);
G.m_obbOffsets.Add((int)file.Position);
G.m_obbSizes.Add((int)size);
int iSlash = name.LastIndexOf('/');
if ( iSlash!=-1 )
{
string ext = name.Substring(name.Length-4, 4);
if ( ext==".mp3" || ext==".mp4" )
{
string dir = G.m_folderContent + name.Substring(0, iSlash+1);
if ( System.IO.Directory.Exists(dir)==false )
System.IO.Directory.CreateDirectory(dir);
}
}
file.Position += size;
yield return null;
}
file.Close();
}
m_state++;
}
#endif
void OnApplicationPause(bool pause)
{
if ( G.m_game )
G.m_game.__339(pause);
}
void OnApplicationQuit()
{
if ( G.m_game )
G.m_game.__340();
}
}
