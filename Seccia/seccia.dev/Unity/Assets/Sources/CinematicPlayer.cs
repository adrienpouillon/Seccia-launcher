using UnityEngine;
using UnityEngine.Video;
using System;
using System.IO;
public class CinematicPlayer
{
public GameObject m_go;
public VideoPlayer m_player;
public bool m_prepareCompleted;
public Cinematic m_cinematic = null;
public RoleBox m_roleBox = null;
public int m_roleBoxToken = 0;
public Sound m_sound = new Sound(Sound.TYPE.CINEMATIC);
public int m_width;
public int m_height;
public float m_ratio;
public int m_frameCount;
public float m_duration;
public Rect m_rc;
public bool m_isWaitingVideo = false;
public bool m_isWaitingAudio = false;
public bool m_isPlaying = false;
public float m_time = 0.0f;
public int m_iFrame;
public int m_iLastSub;
public int m_syncLimit;
public static implicit operator bool(CinematicPlayer inst) { return inst!=null; }
void ErrorReceived(VideoPlayer player, string message)
{
__41();
}
public void Init()
{
m_go = GameObject.Find("Video");
m_player = m_go.GetComponent<VideoPlayer>();
m_player.errorReceived += ErrorReceived;
m_go.SetActive(false);
}
public bool __38()
{
return m_isPlaying || m_isWaitingAudio || m_isWaitingVideo;
}
public bool __39(string uid, RoleBox roleBox = null)
{
Stop();
m_sound.Stop();
Cinematic cinematic = G.m_game.__281(uid);
if ( cinematic==null )
return false;
if ( G.m_game.m_hasVideos==false )
return false;
string videoName = cinematic.m_descs[G.m_game.m_optionLanguageAudio].m_video;
if ( videoName.Length==0 )
return false;
m_width = cinematic.m_descs[G.m_game.m_optionLanguageAudio].m_width;
m_height = cinematic.m_descs[G.m_game.m_optionLanguageAudio].m_height;
m_ratio = m_width / (float)m_height;
m_frameCount = cinematic.m_descs[G.m_game.m_optionLanguageAudio].m_frameCount;
m_syncLimit = G.Clamp(cinematic.m_descs[G.m_game.m_optionLanguageAudio].m_fps/4, 2, 1000);
m_rc = G.m_rcView;
if ( G.__129(m_ratio)==false )
{
m_rc.height = m_rc.width / m_ratio;
m_rc.y = (G.m_rcView.height - m_rc.height)*0.5f;
}
m_cinematic = cinematic;
m_roleBox = roleBox;
m_roleBoxToken = roleBox==null ? 0 : roleBox.m_parent.m_token;
m_isWaitingVideo = true;
m_go.SetActive(true);
m_player.url = G.m_folderContentVideos + videoName;
m_player.Prepare();
return true;
}
public void __40()
{
string audioName = m_cinematic.m_descs[G.m_game.m_optionLanguageAudio].m_audio;
m_sound.m_name = audioName;
if ( m_sound.m_name.Length>0 )
{
if ( m_sound.__982(1.0f) )
m_isWaitingAudio = true;
}
}
public void Stop()
{
m_player.Stop();
m_go.SetActive(false);
m_prepareCompleted = false;
m_cinematic = null;
m_roleBox = null;
m_roleBoxToken = 0;
m_sound.Stop();
m_isWaitingVideo = false;
m_isWaitingAudio = false;
m_isPlaying = false;
m_time = 0.0f;
m_iFrame = 0;
m_iLastSub = 0;
}
public void __41()
{
RoleBox roleBox = m_roleBox;
int roleBoxToken = m_roleBoxToken;
Stop();
G.m_game.m_input.Reset();
if ( roleBox )
roleBox.__457(roleBoxToken);
}
public void __42()
{
if ( m_isWaitingVideo )
{
#if UNITY_ANDROID
if ( m_player.isPrepared==false && m_player.texture==null )
return;
#else
if ( m_player.isPrepared==false )
return;
#endif
m_duration = (float)m_player.length;
m_isWaitingVideo = false;
__40();
if ( m_isWaitingAudio==false )
{
m_isPlaying = true;
m_player.Play();
}
}
if ( m_isWaitingAudio )
{
if ( m_sound.m_audioClip==null )
return;
m_isWaitingAudio = false;
m_isPlaying = true;
m_player.Play();
}
if ( m_isPlaying==false )
return;
if ( m_sound.m_audioSource==null || m_sound.__527()==false )
m_time += G.m_game.m_elapsed;
else
m_time = m_sound.m_audioSource.time;
int iFrame = (int)(m_time/m_duration * m_frameCount);
if ( iFrame!=m_iFrame )
{
m_iFrame = iFrame;
if ( m_iFrame>=m_frameCount )
__41();
else
{
if ( Math.Abs(m_player.frame-m_iFrame)>m_syncLimit )
m_player.frame = m_iFrame;
}
}
}
public void __43()
{
if ( m_isPlaying==false )
{
if ( m_cinematic.m_skip==CINEMATICSKIP.DELAY && G.m_game.m_input.m_isDown )
G.m_graphics.__366();
return;
}
G.m_materialTexture24.mainTexture = m_player.texture;
G.m_graphics.__354(G.m_materialTexture24, ref m_rc);
G.m_materialTexture24.mainTexture = null;
G.m_graphics.__352(FXO.CINEMATIC_IMAGE);
if ( G.m_game.m_optionSubtitle!=SUBTITLE.NONE )
{
CinematicText sub = m_cinematic.__37(m_time, m_iLastSub);
if ( sub )
{
m_iLastSub = sub.m_index;
G.m_game.__215().__492(sub.m_text.Get(), ref G.m_colorWhite, G.m_rcViewUI.width*G.SUBTITLE_WIDTH, G.m_game.m_subtitleMargin);
}
}
G.m_graphics.__352(FXO.CINEMATIC_TEXT);
if ( m_cinematic.m_skip==CINEMATICSKIP.DELAY && G.m_game.m_input.m_isDown )
G.m_graphics.__366();
}
}
