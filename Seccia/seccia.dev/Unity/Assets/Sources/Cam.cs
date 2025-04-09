using UnityEngine;
using System;
using System.IO;
public class Cam
{
public CAMERA m_type;
public Scene m_scene;
public float m_fromScale;
public float m_toScale;
public float m_fromX;
public float m_toX;
public float m_fromY;
public float m_toY;
public RoleBox m_roleBox;
public int m_roleBoxToken;
public float m_scale;
public float m_x;
public float m_y;
public float m_duration;
public float m_time;
public float m_ratio;
public bool m_scrollSmoothInitialized;
public bool m_zoomSmoothInitialized;
public static implicit operator bool(Cam inst) { return inst!=null; }
public Cam(CAMERA type, Scene scene)
{
m_type = type;
m_scene = scene;
Reset();
}
public void Reset()
{
m_fromScale = 0.0f;
m_toScale = 0.0f;
m_fromX = 0.0f;
m_toX = 0.0f;
m_fromY = 0.0f;
m_toY = 0.0f;
m_scale = 0.0f;
m_x = 0.0f;
m_y = 0.0f;
m_roleBox = null;
m_roleBoxToken = 0;
m_duration = 0.0f;
m_time = 0.0f;
m_ratio = 0.0f;
m_scrollSmoothInitialized = false;
m_zoomSmoothInitialized = false;
}
public bool __33()
{
return m_type==CAMERA.AUTO || m_type==CAMERA.AUTO_POS || m_type==CAMERA.AUTO_SCALE;
}
public float __34()
{
switch ( m_type )
{
case CAMERA.AUTO:
case CAMERA.AUTO_SCALE:
{
SceneObj playerSceneObj = G.m_game.__295();
if ( playerSceneObj==null )
return 1.0f;
return playerSceneObj.__628();
}
case CAMERA.AUTO_POS:
{
return 1.0f;
}
case CAMERA.CURSOR:
{
return m_scale;
}
case CAMERA.MANUAL:
{
return m_scale;
}
case CAMERA.TALK:
{
return m_scale;
}
case CAMERA.TRACK:
{
return m_fromScale + (m_toScale-m_fromScale)*m_ratio;
}
case CAMERA.TIMELINE:
{
if ( G.m_game.m_timeline==null )
return 1.0f;
return G.m_game.m_timeline.m_camScale;
}
}
return 1.0f;
}
public float __35()
{
switch ( m_type )
{
case CAMERA.AUTO:
case CAMERA.AUTO_POS:
{
SceneObj playerSceneObj = G.m_game.__295();
if ( playerSceneObj==null )
return G.m_rcGame.width*0.5f;
return playerSceneObj.__35();
}
case CAMERA.AUTO_SCALE:
{
return G.m_rcGame.width*0.5f;
}
case CAMERA.CURSOR:
{
if ( G.m_game.m_cursor )
m_x = G.m_game.m_cursorViewX/m_scene.m_rc.width * m_scene.m_width;
return m_x;
}
case CAMERA.MANUAL:
{
return m_x;
}
case CAMERA.TALK:
{
return m_toX;
}
case CAMERA.TRACK:
{
return m_fromX + (m_toX-m_fromX)*m_ratio;
}
case CAMERA.TIMELINE:
{
if ( G.m_game.m_timeline==null )
return 0.0f;
return G.m_game.m_timeline.m_camX;
}
}
return 0.0f;
}
public float __36()
{
switch ( m_type )
{
case CAMERA.AUTO:
case CAMERA.AUTO_POS:
{
SceneObj playerSceneObj = G.m_game.__295();
if ( playerSceneObj==null )
return G.m_rcGame.height*0.5f;
return playerSceneObj.__36();
}
case CAMERA.AUTO_SCALE:
{
return G.m_rcGame.height*0.5f;
}
case CAMERA.CURSOR:
{
if ( G.m_game.m_cursor )
m_y = G.m_game.m_cursorViewY/m_scene.m_rc.height * m_scene.m_height;
return m_y;
}
case CAMERA.MANUAL:
{
return m_y;
}
case CAMERA.TALK:
{
return m_toY;
}
case CAMERA.TRACK:
{
return m_fromY + (m_toY-m_fromY)*m_ratio;
}
case CAMERA.TIMELINE:
{
if ( G.m_game.m_timeline )
return G.m_game.m_timeline.m_camY;
break;
}
}
return 0.0f;
}
}
