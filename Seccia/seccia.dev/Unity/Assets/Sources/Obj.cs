using UnityEngine;
using System;
using System.Collections.Generic;
public class Obj
{
public int m_sid;
public string m_uid;
public string[] m_tags;
public string m_uidClone;
public Obj m_clone = null;
public Serial<float> m_anchorX;
public Serial<float> m_anchorY;
public float m_imgWidth;
public float m_imgHeight;
public int m_tolerance;
public Serial<float> m_speedX;
public Serial<float> m_speedY;
public ANIMATION m_animation = ANIMATION.NONE;
public float m_animationSpeed = 1.0f;
public float m_animationScale = 1.0f;
public bool m_monochrome = false;
public Serial<Color> m_tint;
public Color m_speech;
public string m_uidAvatar;
public Obj m_avatar = null;
public Rect m_avatarImage;
public Rect m_avatarText;
public int m_avatarTextAlign;
public float m_avatarOpacity;
public bool m_speechMovie;
public Term m_title;
public Serial<bool> m_enabled;
public Serial<bool> m_subEnabled;
public bool m_bboxDetection;
public Serial<int> m_icon;
public Sprite[] m_icons = null;
public List<Turn> m_turns = null;
public Sprite[] m_sprites = null;
public int m_maxAnimFrameCount;
public Anim[] m_anims = null;
public SubObj[] m_subObjs = null;
public bool m_opened = false;
public Serial<bool> m_killed;
public static implicit operator bool(Obj inst) { return inst!=null; }
public void Reset(bool fromScript = false)
{
if ( fromScript==false )
{
End(null);
m_killed.Reset();
}
m_icon.Reset();
m_title.m_sub.Reset();
m_enabled.Reset();
m_subEnabled.Reset();
m_anchorX.Reset();
m_anchorY.Reset();
m_speedX.Reset();
m_speedY.Reset();
for ( int i=0 ; i<m_anims.Length ; i++ )
m_anims[i].Reset();
}
public void __46(JsonObj json)
{
json.__382("sid", m_sid);
json.__380("uid", m_uid);
json.__380("tag", m_tags[0]);
if ( m_icon.modified )
json.__381("icon", m_icon.cur);
if ( m_title.m_sub.modified )
json.__381("title", m_title.m_sub.cur);
if ( m_killed.modified )
json.__384("killed", m_killed.cur);
if ( m_enabled.modified )
json.__384("enabled", m_enabled.cur);
if ( m_subEnabled.modified )
json.__384("subEnabled", m_subEnabled.cur);
if ( m_anchorX.modified )
json.__381("x", (int)m_anchorX.cur);
if ( m_anchorY.modified )
json.__381("y", (int)m_anchorY.cur);
if ( m_speedX.modified )
json.__381("speedX", (int)m_speedX.cur);
if ( m_speedY.modified )
json.__381("speedY", (int)m_speedY.cur);
if ( m_tint.modified )
json.__381("tint", (int)G.__128(ref m_tint.cur));
JsonArray jAnims = json.__389("anims");
for ( int i=0 ; i<m_anims.Length ; i++ )
{
JsonObj jAnim = jAnims.__388();
m_anims[i].__46(jAnim);
}
}
public void __47(JsonObj json)
{
m_tags[0] = json.GetString("tag");
if ( json.__390("icon") )
m_icon.Set(json.GetInt("icon"));
if ( json.__390("title") )
m_title.m_sub.Set(json.GetInt("title"));
if ( json.__390("killed") )
m_killed.Set(json.__400("killed"));
if ( json.__390("enabled") )
m_enabled.Set(json.__400("enabled"));
if ( json.__390("subEnabled") )
m_subEnabled.Set(json.__400("subEnabled"));
if ( json.__390("x") )
m_anchorX.Set(json.GetInt("x"));
if ( json.__390("y") )
m_anchorY.Set(json.GetInt("y"));
if ( json.__390("speedX") )
m_speedX.Set(json.GetInt("speedX"));
if ( json.__390("speedY") )
m_speedY.Set(json.GetInt("speedY"));
if ( json.__390("tint") )
m_tint.Set(G.__126((uint)json.GetInt("tint")));
JsonArray jAnims = json.__394("anims");
if ( jAnims )
{
for ( int i=0 ; i<jAnims.__66() ; i++ )
{
JsonObj jAnim = jAnims.__393(i);
if ( jAnim )
{
Anim anim = __470(jAnim.GetString("name"));
if ( anim )
anim.__47(jAnim);
}
}
}
}
public void __468(Asset asset, Scene scene)
{
if ( m_avatar )
m_avatar.__468(asset, scene);
if ( m_killed.cur )
return;
if ( m_opened )
{
if ( m_clone==null )
{
for ( int i=0 ; i<m_sprites.Length ; i++ )
{
if ( m_sprites[i] )
{
if ( m_sprites[i].m_packGroupType==PACKGROUP.SCN && m_sprites[i].m_packGroupScene==scene.m_uid )
{
if ( m_sprites[i].__988()==false )
m_sprites[i].__468(asset);
}
}
}
}
}
else
{
if ( m_clone==null )
{
for ( int i=0 ; i<m_sprites.Length ; i++ )
{
if ( m_sprites[i] )
{
if ( m_sprites[i].m_packGroupType==PACKGROUP.OBJ || (m_sprites[i].m_packGroupType==PACKGROUP.SCN && m_sprites[i].m_packGroupScene==scene.m_uid) )
{
if ( m_sprites[i].__988()==false )
m_sprites[i].__468(asset);
}
}
}
for ( int i=0 ; i<m_anims.Length ; i++ )
{
for ( int j=0 ; j<m_anims[i].m_dirs.Length ; j++ )
{
for ( int k=0 ; k<m_anims[i].m_dirs[j].__475() ; k++ )
{
if ( m_anims[i].m_dirs[j].m_frames[k].m_mask && m_anims[i].m_dirs[j].m_frames[k].m_maskCloned==false )
m_anims[i].m_dirs[j].m_frames[k].m_mask.__468(asset);
}
}
}
}
m_opened = true;
}
}
public void End(Scene scene, Scene nextScene = null)
{
if ( m_avatar )
m_avatar.End(scene, nextScene);
if ( m_killed.cur )
return;
if ( nextScene && nextScene==scene && nextScene.__277(this) )
{
if ( m_clone==null )
{
for ( int i=0 ; i<m_sprites.Length ; i++ )
{
if ( m_sprites[i] )
{
if ( m_sprites[i].m_packGroupType==PACKGROUP.SCN && m_sprites[i].m_packGroupScene==scene.m_uid )
m_sprites[i].End();
}
}
}
return;
}
if ( m_clone==null )
{
for ( int i=0 ; i<m_sprites.Length ; i++ )
{
if ( m_sprites[i] )
m_sprites[i].End();
}
for ( int i=0 ; i<m_anims.Length ; i++ )
{
for ( int j=0 ; j<m_anims[i].m_dirs.Length ; j++ )
{
for ( int k=0 ; k<m_anims[i].m_dirs[j].__475() ; k++ )
{
if ( m_anims[i].m_dirs[j].m_frames[k].m_mask && m_anims[i].m_dirs[j].m_frames[k].m_maskCloned==false )
m_anims[i].m_dirs[j].m_frames[k].m_mask.End();
}
}
}
}
m_opened = false;
}
public bool __48(ref string nameOrTag)
{
if ( nameOrTag.Length==0 )
return false;
if ( nameOrTag[0]=='@' )
return G.__149(m_tags, nameOrTag.Substring(1));
return G.__148(ref m_uid, ref nameOrTag);
}
public SubObj __469(ref string name)
{
if ( name.Length==0 )
return null;
for ( int i=0 ; i<m_subObjs.Length ; i++ )
{
if ( G.__148(ref m_subObjs[i].m_name, ref name) )
return m_subObjs[i];
}
return null;
}
public Anim __470(ref string name)
{
if ( name.Length==0 )
return null;
for ( int i=0 ; i<m_anims.Length ; i++ )
{
if ( G.__148(ref m_anims[i].m_name, ref name) )
return m_anims[i];
}
return null;
}
public Anim __470(string name)
{
return __470(ref name);
}
public Player __293()
{
for ( int i=0 ; i<G.m_game.m_players.Length ; i++ )
{
for ( int j=0 ; j<G.m_game.m_players[i].m_items.Count ; j++ )
{
if ( G.m_game.m_players[i].m_items[j]==this )
return G.m_game.m_players[i];
}
}
return null;
}
public Sprite __453()
{
return m_icons[m_icon.cur];
}
public Sprite __453(int index)
{
if ( index<0 || index>=G.ICON_COUNT )
return null;
return m_icons[index];
}
public Turn __471(int dir, string stop)
{
if ( m_turns!=null )
{
for ( int i=0 ; i<m_turns.Count ; i++ )
{
Turn turn = m_turns[i];
if ( turn.m_dir==dir )
{
if ( turn.m_stop.Length==0 || turn.m_stop==stop )
return turn;
}
}
}
return null;
}
}
public class SubObj
{
public Obj m_obj;
public string m_name;
public Term m_title;
public static implicit operator bool(SubObj inst) { return inst!=null; }
public bool __472()
{
return m_title.Get().Length>0;
}
}
public class Anim
{
public string m_name;
public Serial<int> m_speed;
public float m_fps;
public float m_fpsInv;
public bool m_reversed;
public int m_actionFrame;
public int m_loopCount;
public int m_loopRangeMin;
public int m_loopRangeMax;
public int m_loopRangeCount;
public RANDOM m_random;
public PROFILE m_profile;
public int m_maxFrameCount;
public AnimDir[] m_dirs;
public AnimDir m_defaultDir;
public List<Sprite> m_sprites = null;
public static implicit operator bool(Anim inst) { return inst!=null; }
public void Reset()
{
m_speed.Reset();
__473((int)m_speed.init);
}
public void __46(JsonObj json)
{
json.__380("name", m_name);
if ( m_speed.modified )
json.__381("fps", (int)m_speed.cur);
}
public void __47(JsonObj json)
{
if ( json.__390("fps") )
__473(json.GetInt("fps"));
}
public void __473(int speed)
{
speed = G.Clamp(speed, -127, 127);
m_speed.Set(speed);
m_reversed = m_random==RANDOM.NONE && m_profile==PROFILE.NONE && speed<0;
speed = Math.Abs(speed);
m_fps = (float)speed;
m_fpsInv = speed==0 ? 0.0f : 1.0f/m_fps;
}
public bool __474(int index)
{
if ( index==-1 )
return false;
return m_dirs[index].__475()>0;
}
}
public class AnimDir
{
public const int COUNT = 8;
public const int LEFT = 0;
public const int RIGHT = 1;
public const int FRONT = 2;
public const int BACK = 3;
public const int FL = 4;
public const int FR = 5;
public const int BL = 6;
public const int BR = 7;
public int m_id;
public Frame[] m_frames;
public static implicit operator bool(AnimDir inst) { return inst!=null; }
public int __475()
{
return m_frames.Length;
}
public string __392()
{
switch ( m_id )
{
case LEFT:	return "LEFT";
case RIGHT:	return "RIGHT";
case FRONT:	return "FRONT";
case BACK:	return "BACK";
case FL:	return "FL";
case FR:	return "FR";
case BL:	return "BL";
case BR:	return "BR";
}
return "";
}
}
public class Frame
{
public int m_index;
public int m_index2;
public Rect m_rcTrim;
public bool m_maskCloned;
public Mask m_mask;
public Sprite m_sprite = null;
public int m_layer = -1;
public Rect m_source;
public bool m_rotated = false;
public bool m_flipped = false;
public Rect[] m_subObjRects;
public static implicit operator bool(Frame inst) { return inst!=null; }
}
public class Turn
{
public int m_dir;
public string m_stop;
public Anim m_anim;
public static implicit operator bool(Turn inst) { return inst!=null; }
}
public class Mask
{
public int m_offset = 0;
public int m_size = 0;
public byte[] m_buffer;
public static implicit operator bool(Mask inst) { return inst!=null; }
public void __468(Asset asset)
{
m_buffer = null;
if ( m_offset==0 )
return;
asset.__3(m_offset);
m_buffer = asset.__9(m_size);
}
public void End()
{
m_buffer = null;
}
}
