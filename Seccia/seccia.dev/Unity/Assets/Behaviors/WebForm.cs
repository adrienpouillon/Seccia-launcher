using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Runtime.InteropServices;
public class WebForm:MonoBehaviour
{
public static WebForm m_instance = null;
public bool m_hasError = false;
private float m_startTime = 0.0f;
#if UNITY_WEBGL
[DllImport("__Internal")]
public static extern void JavascriptSyncFiles();
[DllImport("__Internal")]
public static extern string JavascriptGetHostName();
[DllImport("__Internal")]
public static extern void JavascriptOpenUrl(string url);
private int m_state = 0;
private int m_gameSize = 0;
private int m_graphicsSize = 0;
private int m_soundsSize = 0;
private int m_configSize = 0;
private float m_progressGame = 0.0f;
private float m_progressGraphics = 0.0f;
private float m_progressSounds = 0.0f;
private float m_progressConfig = 0.0f;
public Asset m_assetGame = null;
public Asset m_assetGraphics = null;
public Asset m_assetSounds = null;
public Asset m_assetConfig = null;
#endif
void Awake()
{
m_instance = this;
m_startTime = Time.time;
}
public void Disable()
{
GameObject.Find("WebForm").SetActive(false);
}
#if UNITY_WEBGL
void Start()
{
m_startTime = Time.time;
}
void Update()
{
if ( G.m_webReady==false )
{
#if !UNITY_EDITOR
if ( m_startTime!=-1.0f && Time.time-m_startTime>0.3f )
{
m_startTime = -1.0f;
Application.ExternalCall("LetsGo");
}
#endif
}
else if ( G.m_isGameLoaded==false )
{
switch ( m_state )
{
case 0:
m_state++;
StartCoroutine(DownloadAssetSizes());
break;
case 2:
m_state++;
StartCoroutine(DownloadGameAsset());
StartCoroutine(DownloadGraphicsAsset());
StartCoroutine(DownloadSoundsAsset());
StartCoroutine(DownloadConfigAsset());
break;
case 3:
if ( m_assetGame && m_assetGraphics && m_assetSounds && m_assetConfig )
{
m_state++;
G.__89();
m_state++;
}
break;
}
}
}
#else
void Start()
{
}
#endif
public bool IsBusy()
{
#if UNITY_WEBGL
if ( G.m_webReady==false )
return true;
if ( m_hasError )
return true;
if ( m_state<5 )
return true;
#endif
return false;
}
public int GetProgress()
{
#if UNITY_WEBGL
if ( m_state<5 )
{
int total = m_gameSize + m_graphicsSize + m_soundsSize;
if ( total<=0 )
return 0;
float value = (m_progressGame*m_gameSize + m_progressGraphics*m_graphicsSize + m_progressSounds*m_soundsSize + m_progressConfig*m_configSize) / total;
return (int)(Mathf.Clamp01(value)*100.0f);
}
return 0;
#else
return 0;
#endif
}
#if UNITY_WEBGL
public void LetsGo(string url)
{
if ( url.Length==0 )
G.m_webURL = Application.dataPath + "/Content/";
else
G.m_webURL = url;
G.__78(Screen.width, Screen.height, false);
G.m_webReady = true;
}
IEnumerator DownloadAssetSizes()
{
UnityWebRequest web;
web = UnityWebRequest.Head(G.m_pathGame);
yield return web.SendWebRequest();
m_gameSize = G.__113(web.GetResponseHeader("Content-Length"));
web = UnityWebRequest.Head(G.m_pathGraphics);
yield return web.SendWebRequest();
m_graphicsSize = G.__113(web.GetResponseHeader("Content-Length"));
web = UnityWebRequest.Head(G.m_pathSounds);
yield return web.SendWebRequest();
m_soundsSize = G.__113(web.GetResponseHeader("Content-Length"));
web = UnityWebRequest.Head(G.m_pathConfig);
yield return web.SendWebRequest();
m_configSize = G.__113(web.GetResponseHeader("Content-Length"));
m_state++;
}
IEnumerator DownloadGameAsset()
{
m_progressGame = 0.0f;
UnityWebRequest www = UnityWebRequest.Get(G.m_pathGame);
www.SendWebRequest();
while ( www.isDone==false )
{
m_progressGame = www.downloadProgress;
yield return null;
}
m_progressGame = 1.0f;
if ( www.result!=UnityWebRequest.Result.Success )
m_hasError = true;
else
{
Asset asset = new Asset(false);
asset.Open(www.downloadHandler.data);
m_assetGame = asset;
}
}
IEnumerator DownloadGraphicsAsset()
{
m_progressGraphics = 0.0f;
UnityWebRequest www = UnityWebRequest.Get(G.m_pathGraphics);
www.SendWebRequest();
while ( www.isDone==false )
{
m_progressGraphics = www.downloadProgress;
yield return null;
}
m_progressGraphics = 1.0f;
if ( www.result!=UnityWebRequest.Result.Success )
m_hasError = true;
else
{
Asset asset = new Asset(false);
asset.Open(www.downloadHandler.data);
m_assetGraphics = asset;
}
}
IEnumerator DownloadSoundsAsset()
{
m_progressSounds = 0.0f;
UnityWebRequest www = UnityWebRequest.Get(G.m_pathSounds);
www.SendWebRequest();
while ( www.isDone==false )
{
m_progressSounds = www.downloadProgress;
yield return null;
}
m_progressSounds = 1.0f;
if ( www.result!=UnityWebRequest.Result.Success )
m_hasError = true;
else
{
Asset asset = new Asset(false);
asset.Open(www.downloadHandler.data);
m_assetSounds = asset;
}
}
IEnumerator DownloadConfigAsset()
{
m_progressConfig = 0.0f;
UnityWebRequest www = UnityWebRequest.Get(G.m_pathConfig);
www.SendWebRequest();
while ( www.isDone==false )
{
m_progressConfig = www.downloadProgress;
yield return null;
}
m_progressConfig = 1.0f;
if ( www.result!=UnityWebRequest.Result.Success )
m_hasError = true;
else
{
Asset asset = new Asset(false);
asset.Open(www.downloadHandler.data);
asset.__0();
m_assetConfig = asset;
}
}
#endif
}
