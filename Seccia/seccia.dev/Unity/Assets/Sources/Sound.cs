using UnityEngine;
using UnityEngine.Networking;
using System;
public class Sound
{
public enum TYPE
{
SFX,
VOICE,
MUSIC,
MUSIC_CROSSFADE,
CINEMATIC,
TIMELINE,
};
public TYPE m_type = TYPE.SFX;
public string m_name = "";
public string m_url = "";
public bool m_loop = false;
public AudioSource m_audioSource = null;
public AudioClip m_audioClip = null;
public float m_volume = 1.0f;
public float m_startTime = 0.0f;
public int m_channel = 0;
public int m_offset = 0;
public int m_size = 0;
public int m_hz = 44100;
public bool m_stereo = true;
public bool m_16bits = true;
public int m_frameSize = 4;
public int m_frameSizePerChannel = 2;
public int m_frameCount = 0;
public float[] m_samples = null;
public static implicit operator bool(Sound inst) { return inst!=null; }
public Sound(TYPE type)
{
m_type = type;
}
public bool __42()
{
if ( __1()==false )
return true;
bool isEnd = __528()==false;
if ( isEnd )
{
if ( m_type==TYPE.MUSIC )
{
if ( m_loop )
{
if ( m_audioSource )
m_audioSource.Play();
G.m_game.__331(m_name);
return false;
}
else
{
Stop();
G.m_game.__331(m_name);
return true;
}
}
Stop();
return true;
}
return false;
}
public bool __1()
{
return m_audioSource;
}
public float[] LoadSFX()
{
if ( m_samples!=null )
return m_samples;
Asset asset = G.__96(G.m_pathSounds);
if ( asset==null )
return null;
asset.__3(m_offset);
byte[] buffer = asset.__9(m_size);
asset.Close();
int channels = m_stereo ? 2 : 1;
m_audioClip = AudioClip.Create("", m_frameCount, channels, m_hz, false);
float[] samples = new float[m_frameCount*channels];
if ( m_16bits )
{
for ( int i=0, j=0 ; i<m_size-1 ; i+=2, j++ )
{
int val = (short)buffer[i] | (short)(((int)buffer[i+1])<<8);
samples[j] = ((val+32768)/65535.0f)*2.0f - 1.0f;
}
}
else
{
for ( int i=0 ; i<m_size ; i++ )
samples[i] = (buffer[i]/255.0f)*2.0f - 1.0f;
}
return samples;
}
public bool __989(float volume, int channel = 0, float startTime = 0.0f)
{
Stop();
switch ( m_type )
{
case TYPE.SFX:
{
float[] samples = LoadSFX();
if ( samples==null )
return false;
m_audioClip.SetData(samples, 0);
m_volume = volume;
__990(null, "");
break;
}
case TYPE.VOICE:
{
m_volume = volume;
string path = G.m_folderContentVoices + m_name + G.m_audioExtension;
FileBehavior.m_instance.OpenAudioFile(this, path);
break;
}
case TYPE.MUSIC:
case TYPE.MUSIC_CROSSFADE:
{
m_volume = volume;
m_channel = channel;
string path = G.m_folderContentSongs + m_name + G.m_audioExtension;
FileBehavior.m_instance.OpenAudioFile(this, path);
break;
}
case TYPE.CINEMATIC:
{
m_volume = volume;
string path = G.m_folderContentVideos + m_name + G.m_audioExtension;
FileBehavior.m_instance.OpenAudioFile(this, path);
break;
}
case TYPE.TIMELINE:
{
m_volume = volume;
m_startTime = startTime;
string path = G.m_folderContentSongs + m_name + G.m_audioExtension;
FileBehavior.m_instance.OpenAudioFile(this, path);
break;
}
}
return true;
}
public bool __990(UnityWebRequest www, string path)
{
if ( m_audioSource )
return false;
if ( www!=null )
{
m_audioClip = DownloadHandlerAudioClip.GetContent(www);
m_url = path;
}
m_audioSource = GameObject.Find("Audio").AddComponent<AudioSource>();
m_audioSource.loop = m_loop;
m_audioSource.spatialBlend = 0.0f;
m_audioSource.clip = m_audioClip;
__993(m_volume);
m_audioSource.Play();
switch ( m_type )
{
case TYPE.SFX:
G.m_game.m_playingSounds.Add(this);
break;
case TYPE.VOICE:
G.m_game.m_currentVoice = this;
break;
case TYPE.MUSIC:
G.m_game.m_songs[m_channel].m_current = this;
break;
case TYPE.TIMELINE:
m_audioSource.time = m_startTime;
break;
}
return true;
}
public void Stop()
{
FileBehavior.m_instance.Cancel(this);
switch ( m_type )
{
case TYPE.SFX:
G.m_game.m_playingSounds.Remove(this);
break;
case TYPE.VOICE:
G.m_game.m_currentVoice = null;
break;
case TYPE.MUSIC:
G.m_game.m_songs[m_channel].m_current = null;
break;
}
if ( m_audioSource )
{
m_audioSource.Stop();
G.Release(m_audioSource);
m_audioSource = null;
}
m_volume = 1.0f;
m_startTime = 0.0f;
}
public void __991()
{
if ( m_audioSource && m_audioSource.isPlaying==false )
m_audioSource.Play();
}
public void __992()
{
if ( m_audioSource && m_audioSource.isPlaying )
m_audioSource.Pause();
}
public bool __528()
{
if ( __1()==false )
return false;
return m_audioSource.isPlaying;
}
public void __993(int volume)
{
if ( m_audioSource )
m_audioSource.volume = volume/10.0f;
}
public void __993(float volume)
{
if ( m_audioSource )
{
m_volume = volume;
switch ( m_type )
{
case TYPE.SFX:
m_audioSource.volume = (G.m_game.m_optionSound/10.0f) * volume;
break;
case TYPE.VOICE:
m_audioSource.volume = (G.m_game.m_optionVoice/10.0f) * volume;
break;
case TYPE.MUSIC:
case TYPE.MUSIC_CROSSFADE:
m_audioSource.volume = (G.m_game.m_optionSong/10.0f) * volume;
break;
case TYPE.CINEMATIC:
m_audioSource.volume = (G.m_game.m_optionVoice/10.0f) * volume;
break;
case TYPE.TIMELINE:
m_audioSource.volume = (G.m_game.m_optionVoice/10.0f) * volume;
break;
}
}
}
public float __994()
{
return m_volume;
}
}
public class Song
{
public Sound m_sound = null;
public Sound m_crossfade = null;
public Sound m_current = null;
public string m_lastName = "";
public float m_lastVolume = 1.0f;
public bool m_lastLoop = true;
public int m_fadeMode = 0;
public float m_fadeTime = 0.0f;
public float m_fadeDuration = 0.0f;
public float m_fadeVolBeg = 0.0f;
public float m_fadeVolEnd = 0.0f;
public Song()
{
m_sound = new Sound(Sound.TYPE.MUSIC);
m_sound.m_loop = true;
m_crossfade = new Sound(Sound.TYPE.MUSIC_CROSSFADE);
m_crossfade.m_loop = true;
}
public void __991()
{
if ( m_current )
m_current.__991();
}
public void __992()
{
if ( m_current )
m_current.__992();
}
public void __42()
{
if ( m_current )
m_current.__42();
}
}
