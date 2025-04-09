using UnityEngine;
using System;
using System.Collections.Generic;
public class MenuDialog : Menu
{
public enum MODE
{
POINT_AND_CLICK,
VISUAL_NOVEL,
}
const float TEXT_OFFSET_X		= 20.0f;
const float TEXT_OFFSET_Y		= 20.0f;
public MODE m_mode = MODE.POINT_AND_CLICK;
public float m_frameSize;
public float m_frameIconSize;
public float m_frameSpaceWidth;
public float m_frameSpaceHeight;
public float m_frameTextWidth;
public float m_frameTextHeight;
public int m_callCount = 0;
public Dialog m_dialog = null;
public SceneObj m_speaker = null;
public Sentence m_sentence = null;
public bool m_isWaitingUser = false;
public bool m_locked = false;
public int m_iScroll = -1;
public float m_scroll = 0.0f;
public float m_timeLastAction = 0.0f;
public int m_indexParagraph = 0;
public List<ChoiceRectangle> m_choices = new List<ChoiceRectangle>();
public List<Sprite> m_icons = new List<Sprite>();
public float m_timeIcon;
public uint m_iRenderIcon;
public double m_avatarTime = 0.0f;
public Anim m_avatarAnim = null;
public int m_avatarIndex = 0;
public int m_avatarLoopRange = 0;
public int[] m_avatarProfileFrames = new int[G.PROFILE_FRAME_COUNT];
public int m_avatarProfileIndex = -1;
public List<DialogRole> m_roles = new List<DialogRole>();
public static implicit operator bool(MenuDialog inst) { return inst!=null; }
public MenuDialog()
{
m_avatarProfileFrames[0] = -1;
}
public void Init()
{
if ( G.m_game.m_choiceFrame )
{
m_mode = MODE.VISUAL_NOVEL;
m_frameSize = G.m_game.m_choiceFrameSize;
m_frameIconSize = G.m_game.m_choiceFrameIconSize;
m_frameSpaceWidth = G.m_game.m_choiceFrameSpace[0];
m_frameSpaceHeight = G.m_game.m_choiceFrameSpace[1];
m_frameTextWidth = m_frameSize - G.m_game.m_choiceFrameMargin.__446();
m_frameTextHeight = m_frameSize - G.m_game.m_choiceFrameMargin.__447();
}
}
public void Reset()
{
Quit();
m_callCount = 0;
m_roles.Clear();
}
public void __450(Dialog dialog, RoleBox box, int sentence)
{
if ( dialog==null || box==null )
return;
DialogRole dr = new DialogRole();
dr.m_dialog = dialog;
dr.m_box = box;
dr.m_sentence = sentence;
dr.m_token = box.m_parent.m_token;
m_roles.Add(dr);
}
public void __451(int sentence)
{
for ( int iRole=0 ; iRole<m_roles.Count ; iRole++ )
{
DialogRole role = m_roles[iRole];
if ( role.m_dialog!=m_dialog )
continue;
if ( sentence!=role.m_sentence )
continue;
role.m_box.__458(role.m_token);
m_roles.RemoveAt(iRole);
iRole--;
}
}
public bool __452()
{
for ( int i=0 ; i<m_roles.Count ; i++ )
{
DialogRole role = m_roles[i];
if ( role.m_dialog==m_dialog && role.m_box.__256() )
return true;
}
return false;
}
public void Quit()
{
if ( __38()==false )
return;
if ( m_speaker )
m_speaker.__654();
G.m_game.__323(RoleBoxEventExitDialog.ID, m_dialog.m_uid);
__451(0);
__455();
m_dialog = null;
m_speaker = null;
m_sentence = null;
m_isWaitingUser = false;
m_locked = false;
m_iScroll = -1;
m_scroll = 0.0f;
m_timeLastAction = 0.0f;
m_indexParagraph = 0;
m_avatarTime = 0.0f;
m_avatarAnim = null;
m_avatarIndex = 0;
m_avatarLoopRange = 0;
m_avatarProfileFrames[0] = -1;
m_avatarProfileIndex = -1;
Scene scene = G.m_game.__291();
if ( scene )
scene.__573(CAMERA.TALK);
}
public bool __38()
{
return m_dialog;
}
public bool __453(string uid)
{
if ( m_dialog==null )
return false;
return G.__148(ref m_dialog.m_uid, ref uid);
}
public Sprite __454(Sentence sentence)
{
Sprite sprite = sentence.m_icon.__454();
if ( sprite.__995() )
return sprite;
Asset asset = G.__96(G.m_pathGraphics);
if ( asset==null )
return null;
sprite.__469(asset);
asset.Close();
m_icons.Add(sprite);
return sprite;
}
public void __455()
{
for ( int i=0 ; i<m_icons.Count ; i++ )
m_icons[i].End();
m_icons.Clear();
m_timeIcon = 0.0f;
m_iRenderIcon = 0;
}
public void Next(Sentence sentence, bool byUser, bool init)
{
if ( __38()==false )
return;
m_timeLastAction = G.m_game.m_time;
if ( sentence && sentence.m_speaker!=m_sentence.m_speaker )
{
m_avatarTime = 0.0f;
m_avatarAnim = null;
m_avatarIndex = 0;
m_avatarLoopRange = 0;
m_avatarProfileFrames[0] = -1;
m_avatarProfileIndex = -1;
}
__455();
int callCount = m_callCount;
if ( init==false )
{
bool visited = m_sentence.m_visited;
if ( m_sentence.m_sid!=0 )
G.m_game.__332(m_sentence, m_indexParagraph);
if ( visited==false )
{
m_sentence.m_visited = true;
if ( m_sentence.m_sid!=0 )
G.Success(m_sentence.m_onSay);
}
if ( m_sentence.m_mutualShows!=null )
{
for ( int i=0 ; i<m_sentence.m_mutualShows.Length ; i++ )
m_sentence.m_dialog.__51(m_sentence.m_mutualShows[i]);
}
if ( m_sentence.m_task )
G.m_game.Task(m_sentence.m_taskRole, m_sentence.m_taskName, m_sentence.m_taskArg);
}
if ( m_sentence.m_sid!=0 )
__451(m_sentence.m_sid);
if ( __38()==false || callCount!=m_callCount )
return;
if ( init==false && m_sentence.m_hideBranch )
{
Sentence nextSentence = m_sentence;
while ( nextSentence )
{
if ( nextSentence.m_choice )
nextSentence.m_visible.Set(false);
nextSentence = nextSentence.m_parent;
}
}
if ( init==false && m_sentence.m_exit )
{
Quit();
return;
}
if ( init==false && m_sentence.m_goto!=0 )
{
if ( m_sentence.m_goto==-1 )
sentence = m_dialog.m_root;
else
sentence = m_dialog.__49(m_sentence.m_goto);
}
if ( init==false && m_sentence.m_hideBranch && m_sentence.m_goto==0 )
{
sentence = m_dialog.m_root;
byUser = true;
}
Sentence oldSentence = m_sentence;
Sentence defaultSentence = sentence;
if ( sentence==null && m_sentence.m_subs!=null )
sentence = m_sentence.m_subs[0];
if ( sentence==null )
sentence = m_sentence.__59();
else if ( sentence.m_choice && sentence.m_parent.__58()==0 )
sentence = sentence.__59();
if ( sentence==null )
{
Quit();
return;
}
m_sentence = sentence;
m_locked = m_sentence.m_locked;
if ( m_locked==false )
m_locked = __452();
m_speaker = m_sentence.__55();
if ( m_speaker==null )
{
Quit();
return;
}
if ( byUser )
{
if ( m_sentence.m_choice && m_sentence.m_neverHide==false )
m_sentence.__61();
m_isWaitingUser = false;
if ( m_sentence.m_doNotSay==false )
m_indexParagraph = m_speaker.Say(m_sentence, m_indexParagraph);
else
Next(null, false, false);
}
else
{
if ( m_sentence.m_choice && m_sentence.m_visible.cur==false && m_sentence.m_parent )
{
for ( int i=0 ; i<m_sentence.m_parent.m_subs.Length ; i++ )
{
Sentence sub = m_sentence.m_parent.m_subs[i];
if ( sub.m_choice )
{
if ( sub.m_visible.cur )
break;
if ( sub.__53() )
{
if ( i+1==m_sentence.m_parent.m_subs.Length )
{
Quit();
return;
}
m_sentence = m_sentence.m_parent.m_subs[i+1];
m_speaker = m_sentence.__55();
if ( m_speaker==null )
{
Quit();
return;
}
}
}
else
{
if ( sub.m_visible.cur )
{
if ( sub.m_entryPoint )
{
Quit();
return;
}
m_sentence = sub;
m_speaker = m_sentence.__55();
if ( m_speaker==null )
{
Quit();
return;
}
break;
}
}
}
}
if ( m_sentence.m_choice )
{
if ( m_sentence.__53() )
{
Next(m_sentence, true, false);
return;
}
else
{
m_isWaitingUser = true;
Sentence curSentence = m_sentence;
m_sentence = m_sentence.m_parent;
__457();
bool autoSelectEnabled = false;
if ( autoSelectEnabled && curSentence.m_visited==false && m_sentence.__57()==1 )
{
Next(curSentence, true, false);
return;
}
}
}
else
{
if ( defaultSentence==null && m_sentence==oldSentence )
{
Quit();
return;
}
if ( m_sentence.m_parent==null )
{
Next(m_sentence, true, false);
return;
}
m_isWaitingUser = false;
m_indexParagraph = m_speaker.Say(m_sentence, m_indexParagraph);
}
}
}
public void __456()
{
if ( __38()==false )
return;
if ( m_sentence==null )
{
Quit();
return;
}
m_speaker = m_sentence.__55();
if ( m_speaker==null )
{
Quit();
return;
}
m_indexParagraph = m_speaker.Say(m_sentence, m_indexParagraph, true);
}
public void __457()
{
m_choices.Clear();
if ( m_isWaitingUser==false )
return;
Police font = G.m_game.__215();
float y = G.m_rcView.__438();
int maxChoiceCount = 32;
if ( m_mode==MODE.VISUAL_NOVEL )
{
maxChoiceCount = (int)((G.m_rcView.width+m_frameSpaceWidth)/(m_frameSize+m_frameSpaceWidth));
y -= m_frameSpaceHeight;
}
else
y -= TEXT_OFFSET_Y;
for ( int iSub=m_sentence.m_subs.Length-1 ; iSub>=0 ; iSub-- )
{
if ( m_sentence.m_subs[iSub].m_visible.cur==false || m_sentence.m_subs[iSub].m_choice==false )
continue;
if ( m_sentence.m_subs[iSub].m_icon==null )
{
bool randomly = m_sentence.m_subs[iSub].m_randomly;
string[] paragraphs = m_sentence.m_subs[iSub].m_text.GetParagraphs();
for ( int iPar=paragraphs.Length-1 ; iPar>=0 ; iPar-- )
{
if ( randomly )
iPar = G.__156(paragraphs.Length);
ChoiceRectangle choice = new ChoiceRectangle();
choice.m_iSub = iSub;
choice.m_index = iPar;
choice.m_text = paragraphs[iPar];
if ( m_mode==MODE.POINT_AND_CLICK )
{
choice.m_textSize = font.__492(choice.m_text);
choice.m_rc.width = choice.m_textSize.x;
choice.m_rc.height = choice.m_textSize.y;
choice.m_rc.x = TEXT_OFFSET_X;
choice.m_rc.y = y - choice.m_rc.height;
choice.m_rcInput = choice.m_rc;
m_choices.Insert(0, choice);
y = choice.m_rc.y;
}
else
{
choice.m_rc.x = 0.0f;
choice.m_rc.y = y - m_frameSize;
choice.m_rc.width = m_frameSize;
choice.m_rc.height = m_frameSize;
choice.m_info = new BreakTextInfo();
G.__161(choice.m_info, choice.m_text, font, m_frameTextWidth);
if ( choice.m_info.m_paraSizes.Count>0 && choice.m_info.m_paraSizes[0].y>m_frameTextHeight )
{
for ( int i=0 ; i<choice.m_info.__67() ; i++ )
{
if ( choice.m_info.m_lineRects[i].__438()>m_frameTextHeight )
{
while ( choice.m_info.__67()>i )
choice.m_info.__202();
choice.m_info.__203(font);
break;
}
}
}
m_choices.Insert(0, choice);
}
if ( randomly )
break;
}
}
else
{
ChoiceRectangle choice = new ChoiceRectangle();
choice.m_iSub = iSub;
choice.m_index = 0;
choice.m_icon = m_sentence.m_subs[iSub].m_icon;
choice.m_rc.x = 0.0f;
choice.m_rc.y = y - m_frameSize;
choice.m_rc.width = m_frameSize;
choice.m_rc.height = m_frameSize;
m_choices.Insert(0, choice);
}
}
while ( m_choices.Count>maxChoiceCount )
m_choices.RemoveAt(m_choices.Count-1);
if ( m_mode==MODE.VISUAL_NOVEL )
{
float x = m_choices.Count*m_frameSize;
if ( m_choices.Count>1 )
x += (m_choices.Count-1)*m_frameSpaceWidth;
x = G.m_rcView.width*0.5f - x*0.5f;
for ( int i=0 ; i<m_choices.Count ; i++ )
{
ChoiceRectangle choice = m_choices[i];
choice.m_rc.x = x;
x += m_frameSize + m_frameSpaceWidth;
choice.m_rcInput = choice.m_rc;
choice.m_rcInput.x += G.m_game.m_choiceFrameMargin.left;
choice.m_rcInput.y += G.m_game.m_choiceFrameMargin.top;
choice.m_rcInput.width -= G.m_game.m_choiceFrameMargin.__446();
choice.m_rcInput.height -= G.m_game.m_choiceFrameMargin.__447();
choice.m_rcIcon.x = choice.m_rcInput.__440() - m_frameIconSize*0.5f;
choice.m_rcIcon.y = choice.m_rcInput.__441() - m_frameIconSize*0.5f;
choice.m_rcIcon.width = m_frameIconSize;
choice.m_rcIcon.height = m_frameIconSize;
}
}
}
public void __458(float x, float y)
{
if ( __38()==false )
return;
float duration = 0.5f;
if ( G.m_game.m_time-m_timeLastAction>duration )
{
if ( m_isWaitingUser )
{
bool outside = true;
for ( int i=0 ; i<m_choices.Count ; i++ )
{
if ( m_choices[i].m_rcInput.Contains(x, y) )
{
outside = false;
m_indexParagraph = m_choices[i].m_index;
Next(m_sentence.m_subs[m_choices[i].m_iSub], true, false);
m_iScroll = -1;
break;
}
}
if ( outside )
{
if ( m_locked )
return;
if ( m_choices.Count==0 )
{
Quit();
return;
}
else
{
if ( G.m_game.m_layout.Get(LAYOUT_CTRL.SHUTUP).m_active )
{
if ( G.m_game.m_layout.__426(LAYOUT_CTRL.SHUTUP, x, y) )
{
Quit();
return;
}
}
else
{
Quit();
return;
}
}
}
}
else
{
if ( m_speaker && m_speaker.m_timeSay>=m_speaker.m_timeSayDurationForced )
{
m_speaker.__655();
m_iScroll = -1;
}
}
}
}
public override void __42()
{
if ( G.m_game.__291()==null )
return;
if ( __38() )
{
if ( m_isWaitingUser )
{
if ( m_mode==MODE.POINT_AND_CLICK )
{
int index = -1;
if ( G.m_game.m_cursor )
{
for ( int i=0 ; i<m_choices.Count ; i++ )
{
if ( m_choices[i].m_rcInput.Contains(G.m_game.m_cursorViewX, G.m_game.m_cursorViewY) )
{
index = i;
break;
}
}
}
float textWidth = 0.0f;
if ( index!=-1 )
textWidth = m_choices[index].m_textSize.x;
if ( index!=m_iScroll )
{
m_scroll = 0.0f;
m_iScroll = textWidth<=G.m_rcView.width-TEXT_OFFSET_X ? -1 : index;
}
if ( m_iScroll!=-1 )
{
float speed = (G.m_game.__215().__447()+G.m_game.__215().__491())/G.m_rcView.height * 2000.0f;
m_scroll += G.m_game.m_elapsed*speed;
float maxScroll = (float)(textWidth-(G.m_rcView.width-TEXT_OFFSET_X));
if ( m_scroll>maxScroll )
m_scroll = maxScroll;
}
}
}
else
{
if ( m_speaker && m_speaker.__656()==false )
{
Next(null, false, false);
m_iScroll = -1;
}
}
}
base.__42();
}
public override void __43()
{
if ( __38() && m_isWaitingUser )
{
Police font = G.m_game.__215();
bool leftToRight = font.__490();
if ( m_mode==MODE.POINT_AND_CLICK )
{
float y = m_choices.Count==0 ? 0.0f : m_choices[0].m_rc.y - 8.0f;
Rect rc = G.m_rcView;
rc.y = y;
rc.height -= y;
G.m_graphics.__355(G.m_game.m_uiDialogBack.m_material, ref rc);
}
for ( int iChoice=0 ; iChoice<m_choices.Count ; iChoice++ )
{
ChoiceRectangle choice = m_choices[iChoice];
Sentence sentence = m_sentence.m_subs[choice.m_iSub];
if ( sentence.m_visible.cur && sentence.m_choice )
{
if ( sentence.m_icon==null )
{
Color color = G.m_game.m_cursor && choice.m_rcInput.Contains(G.m_game.m_cursorViewX, G.m_game.m_cursorViewY) ? G.m_game.m_colorTextHighlight : G.m_game.m_colorText;
if ( m_mode==MODE.POINT_AND_CLICK )
{
int scroll = iChoice==m_iScroll ? (int)m_scroll : 0;
float x = leftToRight ? choice.m_rc.x-scroll : choice.m_rc.__437()+scroll;
font.__71(ref choice.m_text, x, choice.m_rc.y, ref color);
}
else
{
float x = leftToRight ? choice.m_rc.x + G.m_game.m_choiceFrameMargin.left : choice.m_rc.__437() - G.m_game.m_choiceFrameMargin.right;
float y = choice.m_rc.y + G.m_game.m_choiceFrameMargin.top;
if ( G.m_game.m_uiChoice )
G.m_graphics.__355(G.m_game.m_uiChoice.m_material, ref choice.m_rc);
BreakTextInfo info = choice.m_info;
for ( int i=0 ; i<info.__67() ; i++ )
{
float dy = (m_frameTextHeight - info.m_paraSizes[i].y)*0.5f;
float offset = leftToRight ? x+info.m_lineRects[i].x : x-(info.m_paraSizes[i].x-info.m_lineRects[i].__437());
font.__71(info.m_texts[i], offset, y+info.m_lineRects[i].y+dy, ref color);
}
}
}
else
{
if ( G.m_game.m_uiChoice )
G.m_graphics.__355(G.m_game.m_uiChoice.m_material, ref choice.m_rc);
/*
Anim anim = obj.__471("STOP");
if ( anim && anim.__475(AnimDir.RIGHT) && anim.m_dirs[AnimDir.RIGHT].__476()>0 )
{
float time = G.m_game.m_time * anim.m_fps;
int index = (int)(time % anim.m_dirs[AnimDir.RIGHT].__476());
Frame frame = anim.m_dirs[AnimDir.RIGHT].m_frames[index];
if ( frame && frame.m_sprite )
{
if ( frame.m_sprite.__995()==false )
{
Asset asset = G.__96(G.m_pathGraphics);
if ( asset )
{
frame.m_sprite.__469(asset);
asset.Close();
m_loadedSprites.Add(frame.m_sprite);
}
}
rc.width = obj.m_imgWidth;
rc.height = obj.m_imgHeight;
rc.x = G.m_rcViewUI.__440() - rc.width*0.5f;
rc.y = y;
G.m_graphics.__360(frame, ref rc);
y += rc.height;
}
}
*/
Sprite sprite = __454(sentence);
if ( sprite )
{
Rect rc = choice.m_rcIcon;
if ( G.m_game.m_cursor && rc.Contains(G.m_game.m_cursorViewX, G.m_game.m_cursorViewY) )
rc.__442(ref m_timeIcon, ref m_iRenderIcon);
G.m_graphics.__355(sprite.m_material, ref rc);
}
}
}
}
}
base.__43();
}
}
public class ChoiceRectangle
{
public Rect m_rc = Rect.Zero;
public Rect m_rcInput = Rect.Zero;
public int m_iSub;
public int m_index;
public string m_text;
public Obj m_icon;
public Vec2 m_textSize;
public BreakTextInfo m_info;
public Rect m_rcIcon = Rect.Zero;
}
public class DialogRole
{
public Dialog m_dialog;
public RoleBox m_box;
public int m_sentence;
public int m_token;
}
