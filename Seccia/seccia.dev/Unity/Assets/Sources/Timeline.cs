using UnityEngine;
public class Timeline
{
public Scene m_scene;
public int m_sid;
public string m_name;
public float m_duration;
public bool m_skip;
public Term m_song;
public TimelineShot[] m_shots;
public TimelineScript[] m_scripts;
public TimelineTitle[] m_titles;
public Sound m_sound = new Sound(Sound.TYPE.TIMELINE);
public bool m_isPlaying;
public bool m_isWaitingAudio;
public RoleBox m_roleBox;
public int m_roleBoxToken;
public float m_time;
public int m_iShot;
public int m_iScript;
public int m_iTitle;
public TimelineTitle m_titleToDraw;
public float m_camX;
public float m_camY;
public float m_camScale;
public float m_camFade;
public static implicit operator bool(Timeline inst) { return inst!=null; }
public void __44(Asset asset)
{
int count;
m_name = asset.__18();
m_sid = asset.__13();
m_skip = asset.__10();
m_song = asset.__21();
count = asset.__13();
m_scripts = new TimelineScript[count];
for ( int i=0 ; i<count ; i++ )
{
float time = asset.__13()/10.0f;
TimelineScript script = new TimelineScript();
script.m_start = time;
script.m_script = asset.__26();
m_scripts[i] = script;
}
count = asset.__13();
m_shots = new TimelineShot[count];
for ( int iShot=0 ; iShot<count ; iShot++ )
{
float time = asset.__13()/10.0f;
TimelineShot shot = new TimelineShot();
shot.m_shot = m_scene.__537(asset.__13());
shot.m_start = time;
shot.m_end = time + asset.__13()/10.0f;
shot.m_shotNext = m_scene.__537(asset.__13());
if ( shot.m_shotNext )
{
shot.m_cutStart = asset.__13()/10.0f;
shot.m_cutEnd = asset.__13()/10.0f;
shot.m_curves = new Curve[G.CURVE_COUNT];
for ( int i=0 ; i<G.CURVE_COUNT ; i++ )
{
shot.m_curves[i] = new Curve();
shot.m_curves[i].__44(asset);
}
}
m_shots[iShot] = shot;
}
count = asset.__13();
m_titles = new TimelineTitle[count];
for ( int i=0 ; i<count ; i++ )
{
float time = asset.__13()/10.0f;
TimelineTitle title = new TimelineTitle();
title.m_start = time;
title.m_end = time + asset.__13()/10.0f;
title.m_text = asset.__21();
m_titles[i] = title;
}
m_duration = asset.__13()/10.0f;
Reset();
}
public void Reset()
{
m_isPlaying = false;
m_isWaitingAudio = false;
m_roleBox = null;
m_roleBoxToken = 0;
m_time = 0.0f;
m_iShot = 0;
m_iScript = 0;
m_iTitle = 0;
m_titleToDraw = null;
m_camX = 0.0f;
m_camY = 0.0f;
m_camScale = 1.0f;
m_camFade = 0.0f;
}
public void Play(RoleBox roleBox, float startTime)
{
Reset();
m_roleBox = roleBox;
m_roleBoxToken = m_roleBox.m_parent.m_token;
m_time = startTime;
string song = m_song.Get();
if ( song=="" )
m_isPlaying = true;
else
{
m_sound.m_name = song;
if ( m_sound.__982(1.0f, 0, startTime) )
m_isWaitingAudio = true;
}
m_scene.__568(CAMERA.TIMELINE);
}
public void Stop()
{
RoleBox roleBox = m_roleBox;
int roleToken = m_roleBoxToken;
m_scene.__569(CAMERA.TIMELINE);
m_scene.__570(true);
m_sound.Stop();
Reset();
if ( roleBox!=null )
roleBox.__457(roleToken);
}
public TimelineShot __992()
{
while ( m_iShot<m_shots.Length )
{
TimelineShot shot = m_shots[m_iShot];
if ( shot.m_shot==null )
return null;
if ( m_time<shot.m_start )
return null;
if ( shot.m_shotNext==null )
{
if ( m_time<shot.m_end )
return shot;
}
else
{
if ( m_time<shot.m_cutEnd )
return shot;
}
m_iShot++;
}
return null;
}
public TimelineTitle __993()
{
while ( m_iTitle<m_titles.Length )
{
TimelineTitle title = m_titles[m_iTitle];
if ( m_time<title.m_start )
return null;
if ( m_time<title.m_end )
return title;
m_iTitle++;
}
return null;
}
public void __42()
{
if ( m_isWaitingAudio )
{
if ( m_sound.m_audioClip==null )
return;
m_isWaitingAudio = false;
m_isPlaying = true;
}
if ( m_isPlaying==false )
return;
if ( m_sound.m_audioSource==null || m_sound.__527()==false )
m_time += G.m_game.m_elapsed;
else
m_time = m_sound.m_audioSource.time;
if ( m_time>=m_duration || (m_skip && G.m_game.m_input.__367()) )
{
__994();
G.m_game.__273();
return;
}
TimelineShot shot = __992();
if ( shot )
shot.__996(this);
m_titleToDraw = __993();
__994();
}
public void __994()
{
while ( m_iScript<m_scripts.Length )
{
TimelineScript script = m_scripts[m_iScript];
if ( m_time<script.m_start )
break;
m_iScript++;
if ( script.m_script )
script.m_script.__684();
}
}
public void __995()
{
if ( G.m_game.m_optionSubtitle==SUBTITLE.NONE )
return;
if ( m_isPlaying==false )
return;
if ( m_titleToDraw )
G.m_game.__215().__492(m_titleToDraw.m_text.Get(), ref G.m_colorWhite, G.m_rcViewUI.width*G.SUBTITLE_WIDTH, G.m_game.m_subtitleMargin);
}
}
public class TimelineShot
{
public SceneShot m_shot;
public SceneShot m_shotNext;
public float m_start;
public float m_end;
public bool m_cut;
public float m_cutStart;
public float m_cutEnd;
public Curve[] m_curves;
public static implicit operator bool(TimelineShot inst) { return inst!=null; }
public void __996(Timeline timeline)
{
if ( m_shot==null )
{
timeline.m_camX = 0.0f;
timeline.m_camY = 0.0f;
timeline.m_camScale = 1.0f;
timeline.m_camFade = 0.0f;
return;
}
if ( m_shotNext==null )
{
timeline.m_camX = m_shot.m_x;
timeline.m_camY = m_shot.m_y;
timeline.m_camScale = m_shot.m_scale;
timeline.m_camFade = 1.0f;
return;
}
if ( timeline.m_time<m_cutStart )
{
timeline.m_camX = m_shot.m_x;
timeline.m_camY = m_shot.m_y;
timeline.m_camScale = m_shot.m_scale;
timeline.m_camFade = 1.0f;
return;
}
if ( timeline.m_time>=m_cutEnd )
{
timeline.m_camX = m_shotNext.m_x;
timeline.m_camY = m_shotNext.m_y;
timeline.m_camScale = m_shotNext.m_scale;
timeline.m_camFade = 1.0f;
return;
}
float ratio = G.Clamp((timeline.m_time-m_cutStart)/(m_cutEnd-m_cutStart));
float ratioX = m_curves[0].GetValue(ratio);
float ratioY = m_curves[1].GetValue(ratio);
float ratioScale = m_curves[2].GetValue(ratio);
float ratioFade = m_curves[3].GetValue(ratio);
timeline.m_camX = m_shot.m_x + ratioX*(m_shotNext.m_x-m_shot.m_x);
timeline.m_camY = m_shot.m_y + ratioY*(m_shotNext.m_y-m_shot.m_y);
timeline.m_camScale = m_shot.m_scale + ratioScale*(m_shotNext.m_scale-m_shot.m_scale);
timeline.m_camFade = 1.0f - ratioFade;
}
}
public class TimelineScript
{
public float m_start;
public Script m_script;
public static implicit operator bool(TimelineScript inst) { return inst!=null; }
}
public class TimelineTitle
{
public float m_start;
public float m_end;
public Term m_text;
public static implicit operator bool(TimelineTitle inst) { return inst!=null; }
}
