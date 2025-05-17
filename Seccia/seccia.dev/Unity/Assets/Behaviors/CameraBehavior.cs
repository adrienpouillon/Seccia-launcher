using UnityEngine;
using System;
public class CameraBehavior : MonoBehaviour
{
static public uint s_iRender = 1;
bool m_drawReady;
void Start()
{
}
bool PrepareRender()
{
RenderTexture rt = G.__168();
if ( rt==null )
return false;
G.__173(rt);
G.__171();
return true;
}
void DrawLoadingLogo(int percent = -1, string message = "")
{
const int count = 12;
const float dotSize = 16.0f;
const float circleRadius = 64.0f;
const float textScale = 0.8f;
float x = G.m_rcView.width*0.5f;
float y = G.m_rcView.height*0.5f;
float angle = Time.time*2.0f;
float opacity = (Mathf.Cos(Time.time*1.5f)+1.0f)/2.0f*0.8f + 0.1f;
G.m_spriteLoading.m_material.color = new Color(1.0f, 1.0f, 1.0f, opacity);
Rect rc = new Rect(0.0f, 0.0f, dotSize, dotSize);
for ( int i=0 ; i<count ; i++ )
{
float angleNext = angle + i*G.PI/6.0f;
rc.x = x + Mathf.Cos(angleNext)*circleRadius - dotSize*0.5f;
rc.y = y + Mathf.Sin(angleNext)*circleRadius - dotSize*0.5f;
G.m_graphics.__354(G.m_spriteLoading.m_material, ref rc);
}
G.m_spriteLoading.m_material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
Police font = G.__194();
if ( percent!=-1 )
{
string text = percent.ToString();
Vec2 size = font.__491(text, textScale);
font.__70(ref text, x-size.x*0.5f, y-size.y*0.5f, ref G.m_colorGray, textScale);
}
if ( message.Length>0 )
{
Vec2 size = font.__491(message, textScale);
font.__70(ref message, x-size.x*0.5f, y+circleRadius*2.0f+dotSize, ref G.m_colorGray, textScale);
}
}
void Update()
{
if ( G.m_isGameLoaded==false )
{
m_drawReady = true;
return;
}
if ( FileBehavior.m_instance.IsBusy() )
{
m_drawReady = true;
return;
}
if ( SavegameBehavior.m_instance.IsBusy() )
{
m_drawReady = true;
return;
}
m_drawReady = G.m_game.__42();
}
void OnPostRender()
{
if ( Camera.main==null )
return;
if ( m_drawReady==false )
return;
m_drawReady = false;
s_iRender++;
if ( s_iRender==0 )
s_iRender = 1;
if ( G.m_isGameLoaded )
{
if ( WebForm.m_instance.IsBusy() )
{
if ( PrepareRender()==false )
return;
DrawLoadingLogo(WebForm.m_instance.GetProgress(), WebForm.m_instance.m_hasError ? "ERROR" : "");
G.m_graphics.__351();
}
else if ( FileBehavior.m_instance.IsBusy(2.0f) )
{
if ( PrepareRender()==false )
return;
DrawLoadingLogo(FileBehavior.m_instance.GetProgress());
G.m_graphics.__351();
}
else if ( SavegameBehavior.m_instance.IsBusy(1.0f) )
{
if ( PrepareRender()==false )
return;
DrawLoadingLogo();
G.m_graphics.__351();
}
else
{
G.m_game.__335();
}
}
else if ( G.m_isInitialized )
{
G.__171();
if ( G.m_androidFailed )
{
G.__194().__492("Please download the app again and accept permissions", ref G.m_colorWhite, G.m_rcWindow.width);
}
else if ( WebForm.m_instance.IsBusy() )
{
DrawLoadingLogo(WebForm.m_instance.GetProgress(), WebForm.m_instance.m_hasError ? "ERROR" : "");
}
else
{
DrawLoadingLogo();
}
}
}
}
