using UnityEngine;
using System.Collections.Generic;
public class MenuGame : Menu
{
public const int ID_START		= 1;
public const int ID_MAIN		= 2;
public const int ID_PLAY		= 3;
public const int ID_LOADGAME	= 4;
public const int ID_SAVEGAME	= 5;
public const int ID_ACCOUNT		= 6;
public const int ID_UPLOAD		= 7;
public const int ID_OPTIONS		= 8;
public const int ID_HELP		= 9;
public const int ID_CREDITS		= 10;
public const int ID_LOGO		= 11;
public const int ID_SPLASH1		= 12;
public const int ID_SPLASH2		= 13;
public bool m_menuMusicPlayed = false;
public float m_startTime = 0.0f;
public int m_id = 0;
public List<string> m_items = new List<string>();
public List<string> m_itemsAlt = new List<string>();
public List<Term> m_credits = new List<Term>();
public bool m_intro = false;
public bool m_disableSave = false;
public bool m_onlyOnePageNavigation = false;
public float m_scrollCredits = 0.0f;
public bool m_resolutionChanged = false;
public int m_newgameOffset = 0;
public List<Sprite> m_loadedSprites = new List<Sprite>();
public static implicit operator bool(MenuGame inst) { return inst!=null; }
public void Open(int id, bool intro = false, bool disableSave = false, bool force = false, bool onlyOnePageNavigation = false)
{
if ( id==m_id && force==false )
return;
Close(false);
G.m_game.__269();
m_startTime = G.m_game.m_time;
m_intro = intro;
m_disableSave = disableSave;
m_onlyOnePageNavigation = onlyOnePageNavigation;
m_resolutionChanged = false;
m_id = id;
switch ( m_id )
{
case ID_MAIN:
{
if ( G.m_game.m_savegameEnabled==false && m_intro==false && G.m_game.m_isGameOver==false )
m_items.Add(Localization.m_wordMenuContinue.Get());
else
m_items.Add(Localization.m_wordMenuPlay.Get());
if ( G.m_game.__224() )
m_items.Add(Localization.m_wordMenuAccount.Get());
else
m_items.Add("");
if ( G.m_game.m_options )
m_items.Add(Localization.m_wordMenuOptions.Get());
else
m_items.Add("");
if ( G.m_game.m_menuHelpSprite==null )
m_items.Add("");
else
m_items.Add(Localization.m_wordMenuHelp.Get());
m_items.Add(Localization.m_wordMenuCredits.Get());
if ( G.m_game.m_storeUrl.Length==0 )
m_items.Add("");
else
m_items.Add(Localization.m_wordMenuStore.Get());
if ( G.m_game.m_rateUrl.Length==0 )
m_items.Add("");
else
m_items.Add(Localization.m_wordMenuRate.Get());
if ( G.__87()==false )
m_items.Add(Localization.m_wordMenuQuit.Get());
break;
}
case ID_PLAY:
{
bool separator = false;
if ( m_intro==false && G.m_game.m_isGameOver==false )
{
m_items.Add(Localization.m_wordMenuContinue.Get());
separator = true;
}
else
m_items.Add("");
if ( m_disableSave==false && m_intro==false && G.m_game.m_isGameOver==false && AgePlugin.IsSaveEnabled() )
{
m_items.Add(Localization.m_wordMenuSaveGame.Get());
separator = true;
}
else
m_items.Add("");
if ( separator )
m_items.Add("+");
else
m_items.Add("");
m_items.Add(Localization.m_wordMenuNewGame.Get());
if ( G.__190(-1) && AgePlugin.IsLoadEnabled() )
m_items.Add(Localization.m_wordMenuLoadGame.Get());
else
m_items.Add("");
m_items.Add("+");
m_items.Add(Localization.m_wordMenuBackMainMenu.Get());
break;
}
case ID_LOADGAME:
{
if ( G.__190(G.SAVEGAME_INDEX_AUTO) )
{
m_items.Add(G.__191(G.SAVEGAME_INDEX_AUTO));
m_items.Add("+");
}
else
{
G.__192(0);
m_items.Add("");
m_items.Add("");
}
bool hasAtLeastOne = false;
for ( int i=1 ; i<5 ; i++ )
{
if ( G.__190(i)==false )
{
G.__192(i);
m_items.Add("");
}
else
{
hasAtLeastOne = true;
m_items.Add(G.__191(i));
}
}
if ( hasAtLeastOne )
m_items.Add("+");
else
m_items.Add("");
if ( G.__190(G.SAVEGAME_INDEX_SERVER) )
{
m_items.Add(G.__191(G.SAVEGAME_INDEX_SERVER));
m_items.Add("+");
}
else
{
G.__192(G.SAVEGAME_INDEX_SERVER);
m_items.Add("");
m_items.Add("");
}
m_items.Add(Localization.m_wordMenuBack.Get());
if ( intro==false )
m_items.Add(Localization.m_wordMenuBackGame.Get());
break;
}
case ID_SAVEGAME:
{
for ( int i=1 ; i<5 ; i++ )
{
if ( G.__190(i)==false )
G.__192(i);
m_items.Add(G.__191(i));
}
m_items.Add("+");
m_items.Add(Localization.m_wordMenuBack.Get());
m_items.Add(Localization.m_wordMenuBackGame.Get());
break;
}
case ID_ACCOUNT:
{
if ( G.m_game.__225() )
{
m_items.Add("");
m_items.Add("");
m_items.Add("");
m_items.Add("@"+G.m_game.m_savegameServerUser);
m_items.Add("+");
m_items.Add(Localization.m_wordMenuAccountDownload.Get());
m_items.Add(G.__190(-1) ? Localization.m_wordMenuAccountUpload.Get() : "");
m_items.Add(Localization.m_wordMenuAccountDisconnect.Get());
m_items.Add("+");
m_items.Add(Localization.m_wordMenuAccountDelete.Get());
}
else
{
m_items.Add(Localization.m_wordMenuAccountCreate.Get());
m_items.Add(Localization.m_wordMenuAccountConnect.Get());
m_items.Add(Localization.m_wordMenuAccountPassword.Get());
m_items.Add("");
m_items.Add("");
m_items.Add("");
m_items.Add("");
m_items.Add("");
m_items.Add("");
m_items.Add("");
}
m_items.Add("+");
m_items.Add(Localization.m_wordMenuBackMainMenu.Get());
break;
}
case ID_UPLOAD:
{
for ( int i=0 ; i<G.SAVEGAME_COUNT ; i++ )
{
if ( G.__190(i) )
m_items.Add(G.__191(i));
else
m_items.Add("");
}
m_items.Add("+");
m_items.Add(Localization.m_wordMenuBack.Get());
break;
}
case ID_OPTIONS:
{
if ( G.m_game.m_languages.Length==1 )
{
m_items.Add("");
m_itemsAlt.Add("");
m_items.Add("");
m_itemsAlt.Add("");
}
else
{
if ( G.m_game.m_mergeAudioText )
{
m_items.Add(Localization.m_wordMenuLanguage.Get());
m_itemsAlt.Add(G.m_game.__211());
m_items.Add("");
m_itemsAlt.Add("");
}
else
{
m_items.Add(Localization.m_wordMenuLanguageAudio.Get());
m_itemsAlt.Add(G.m_game.__211());
m_items.Add(Localization.m_wordMenuLanguageText.Get());
m_itemsAlt.Add(G.m_game.__208());
}
}
if ( G.m_game.m_subtitleOption )
{
m_items.Add(Localization.m_wordMenuSubtitle.Get());
m_itemsAlt.Add(G.m_game.__212());
}
else
{
m_items.Add("");
m_itemsAlt.Add("");
}
if ( G.m_game.m_audioLevel=="Global" )
{
m_items.Add(Localization.m_wordMenuGlobalVolume.Get());
m_itemsAlt.Add(G.m_game.m_optionSound==0 ? Localization.m_wordMenuOff.Get() : G.m_game.m_optionSound.ToString());
m_items.Add("");
m_itemsAlt.Add("");
m_items.Add("");
m_itemsAlt.Add("");
}
else if ( G.m_game.m_audioLevel=="Separate" )
{
m_items.Add(Localization.m_wordMenuSoundVolume.Get());
m_itemsAlt.Add(G.m_game.m_optionSound==0 ? Localization.m_wordMenuOff.Get() : G.m_game.m_optionSound.ToString());
m_items.Add(Localization.m_wordMenuMusicVolume.Get());
m_itemsAlt.Add(G.m_game.m_optionSong==0 ? Localization.m_wordMenuOff.Get() : G.m_game.m_optionSong.ToString());
m_items.Add(Localization.m_wordMenuVoiceVolume.Get());
m_itemsAlt.Add(G.m_game.m_optionVoice==0 ? Localization.m_wordMenuOff.Get() : G.m_game.m_optionVoice.ToString());
}
else
{
m_items.Add("");
m_itemsAlt.Add("");
m_items.Add("");
m_itemsAlt.Add("");
m_items.Add("");
m_itemsAlt.Add("");
}
if ( G.m_game.m_fontSizeOption )
{
m_items.Add(Localization.m_wordMenuFont.Get());
m_itemsAlt.Add((G.m_game.m_optionFont+1).ToString());
}
else
{
m_items.Add("");
m_itemsAlt.Add("");
}
if ( G.m_game.m_noLowQuality )
{
m_items.Add("");
m_itemsAlt.Add("");
}
else
{
m_items.Add(Localization.m_wordMenuQuality.Get());
m_itemsAlt.Add(G.m_game.__213());
}
if ( G.__86() && G.__87()==false && G.m_game.m_forceNativeFullscreen==false )
{
m_items.Add(Localization.m_wordMenuResolution.Get());
m_itemsAlt.Add(G.m_game.m_resolutionList[G.m_game.m_optionResolution==-1 ? 0 : G.m_game.m_optionResolution]);
m_items.Add(Localization.m_wordMenuFullscreen.Get());
m_itemsAlt.Add(G.m_game.m_optionFullscreen ? Localization.m_wordMenuOn.Get() : Localization.m_wordMenuOff.Get());
}
else
{
m_items.Add("");
m_itemsAlt.Add("");
m_items.Add("");
m_itemsAlt.Add("");
}
if ( G.m_game.m_privacyPolicy.Length==0 )
m_items.Add("");
else
m_items.Add(Localization.m_wordMenuPrivacyPolicy.Get());
m_items.Add("+");
m_items.Add(Localization.m_wordMenuBackMainMenu.Get());
break;
}
case ID_HELP:
{
Asset asset = G.__95(G.m_pathGraphics);
if ( asset )
{
G.m_game.m_menuHelpSprite.__468(asset);
asset.Close();
}
break;
}
case ID_CREDITS:
{
m_scrollCredits = 0.0f;
break;
}
case ID_LOGO:
{
break;
}
}
}
public void __457(float x, float y)
{
if ( __38() )
{
if ( G.m_game.m_time-m_startTime<0.25f )
return;
List<Rect> rects = __462();
for ( int iRect=0 ; iRect<rects.Count ; iRect++ )
{
if ( rects[iRect].Contains(x, y) )
{
switch ( m_id )
{
case ID_MAIN:
{
switch ( iRect )
{
case 0:
{
if ( G.m_game.m_savegameEnabled )
__459(ID_PLAY);
else
{
if ( m_intro || G.m_game.m_isGameOver )
__466();
else
Close();
}
break;
}
case 1:
{
__459(ID_ACCOUNT);
break;
}
case 2:
{
__459(ID_OPTIONS);
break;
}
case 3:
{
__459(ID_HELP);
break;
}
case 4:
{
__459(ID_CREDITS);
break;
}
case 5:
{
G.OpenUrl(G.m_game.m_storeUrl);
break;
}
case 6:
{
G.OpenUrl(G.m_game.m_rateUrl);
break;
}
case 7:
{
AgePlugin.OnAppQuit();
break;
}
}
break;
}
case ID_PLAY:
{
switch ( iRect )
{
case 0:
{
Close();
break;
}
case 1:
{
__459(ID_SAVEGAME);
break;
}
case 3:
{
__466();
break;
}
case 4:
{
__459(ID_LOADGAME);
break;
}
case 6:
{
__459(ID_MAIN);
break;
}
}
break;
}
case ID_LOADGAME:
{
switch ( iRect )
{
case 0:
{
if ( G.__190(G.SAVEGAME_INDEX_AUTO) )
{
G.m_game.__47(G.SAVEGAME_INDEX_AUTO);
Close(false);
}
break;
}
case 2:
case 3:
case 4:
case 5:
{
int iGame = iRect - 1;
if ( G.__190(iGame) )
{
G.m_game.__47(iGame);
Close(false);
}
break;
}
case 7:
{
if ( G.__190(G.SAVEGAME_INDEX_SERVER) )
{
G.m_game.__47(G.SAVEGAME_INDEX_SERVER);
Close(false);
}
break;
}
case 9:
{
__459(ID_PLAY);
break;
}
case 10:
{
Close();
break;
}
}
break;
}
case ID_SAVEGAME:
{
switch ( iRect )
{
case 0:
case 1:
case 2:
case 3:
{
G.m_game.__46(iRect+1);
Close();
break;
}
case 5:
{
__459(ID_PLAY);
break;
}
case 6:
{
Close();
break;
}
}
break;
}
case ID_ACCOUNT:
{
switch ( iRect )
{
case 0:
{
string[] fields = new string[6];
fields[0] = "name@domain.com";
fields[1] = "";
fields[2] = "account";
fields[3] = Localization.m_wordMenuAccountCheckboxAccount.Get();
string legal = Localization.m_wordMenuAccountLegal1.Get();
G.Popup("signup", Localization.m_wordMenuAccountCreate.Get(), legal, fields, POPUP.SIGNUP);
break;
}
case 1:
{
string[] fields = new string[4];
fields[0] = "name@domain.com";
fields[1] = G.m_game.m_savegameServerUser;
fields[2] = "password";
fields[3] = G.m_game.m_savegameServerPass;
G.Popup("connect", Localization.m_wordMenuAccountConnect.Get(), "", fields, POPUP.LOGIN);
break;
}
case 2:
{
string[] fields = new string[2];
fields[0] = "name@domain.com";
fields[1] = "";
G.Popup("reset", Localization.m_wordMenuAccountPassword.Get(), "", fields, POPUP.EMAIL);
break;
}
case 5:
{
G.Popup("download", Localization.m_wordMenuAccountDownload.Get(), Localization.m_wordMenuConfirmation.Get(), null, POPUP.QUESTION);
break;
}
case 6:
{
if ( G.m_game.m_savegameEnabled )
__459(ID_UPLOAD);
else
G.Popup("upload0", Localization.m_wordMenuAccountUpload.Get(), Localization.m_wordMenuConfirmation.Get(), null, POPUP.QUESTION);
break;
}
case 7:
{
G.Popup("disconnect", Localization.m_wordMenuAccountDisconnect.Get(), Localization.m_wordMenuConfirmation.Get(), null, POPUP.QUESTION);
break;
}
case 9:
{
string[] fields = new string[2];
fields[0] = "password";
fields[1] = "";
G.Popup("delete", Localization.m_wordMenuAccountDelete.Get(), "", fields, POPUP.PASSWORD);
break;
}
case 11:
{
__459(ID_MAIN);
break;
}
}
break;
}
case ID_UPLOAD:
{
switch ( iRect )
{
case 0:
case 1:
case 2:
case 3:
case 4:
case 5:
{
G.Popup("upload"+iRect.ToString(), Localization.m_wordMenuAccountUpload.Get(), Localization.m_wordMenuConfirmation.Get(), null, POPUP.QUESTION);
break;
}
case 7:
{
__459(ID_ACCOUNT);
break;
}
}
break;
}
case ID_OPTIONS:
{
switch ( iRect )
{
case 0:
{
G.m_game.m_optionLanguageAudio++;
if ( G.m_game.m_optionLanguageAudio>=G.m_game.m_languages.Length )
G.m_game.m_optionLanguageAudio = 0;
if ( G.m_game.m_mergeAudioText )
G.m_game.m_optionLanguageText = G.m_game.m_optionLanguageAudio;
m_itemsAlt[iRect] = G.m_game.m_languages[G.m_game.m_optionLanguageAudio].m_name;
G.m_game.__248();
if ( G.m_game.m_mergeAudioText )
{
G.m_game.__218();
bool old = m_resolutionChanged;
__458();
m_resolutionChanged = old;
}
break;
}
case 1:
{
G.m_game.m_optionLanguageText++;
if ( G.m_game.m_optionLanguageText>=G.m_game.m_languages.Length )
G.m_game.m_optionLanguageText = 0;
m_itemsAlt[iRect] = G.m_game.m_languages[G.m_game.m_optionLanguageText].m_name;
G.m_game.__248();
G.m_game.__218();
bool old = m_resolutionChanged;
__458();
m_resolutionChanged = old;
break;
}
case 2:
{
G.m_game.m_optionSubtitle++;
if ( G.m_game.m_optionSubtitle>SUBTITLE.MANUAL )
G.m_game.m_optionSubtitle = SUBTITLE.NONE;
m_itemsAlt[iRect] = G.m_game.__212();
G.m_game.__248();
break;
}
case 3:
{
G.m_game.m_optionSound++;
if ( G.m_game.m_optionSound>=11 )
G.m_game.m_optionSound = 0;
m_itemsAlt[iRect] = G.m_game.m_optionSound==0 ? Localization.m_wordMenuOff.Get() : G.m_game.m_optionSound.ToString();
if ( G.m_game.m_audioLevel=="Global" )
{
G.m_game.m_optionSong = G.m_game.m_optionSound;
G.m_game.m_optionVoice = G.m_game.m_optionSound;
for ( int i=0 ; i<2 ; i++ )
{
if ( G.m_game.m_songs[i].m_current )
G.m_game.m_songs[i].m_current.__986(G.m_game.m_optionSong);
}
}
G.m_game.__248();
break;
}
case 4:
{
G.m_game.m_optionSong++;
if ( G.m_game.m_optionSong>=11 )
G.m_game.m_optionSong = 0;
m_itemsAlt[iRect] = G.m_game.m_optionSong==0 ? Localization.m_wordMenuOff.Get() : G.m_game.m_optionSong.ToString();
G.m_game.__248();
for ( int i=0 ; i<2 ; i++ )
{
if ( G.m_game.m_songs[i].m_current )
G.m_game.m_songs[i].m_current.__986(G.m_game.m_optionSong);
}
break;
}
case 5:
{
G.m_game.m_optionVoice++;
if ( G.m_game.m_optionVoice>=11 )
G.m_game.m_optionVoice = 0;
m_itemsAlt[iRect] = G.m_game.m_optionVoice==0 ? Localization.m_wordMenuOff.Get() : G.m_game.m_optionVoice.ToString();
G.m_game.__248();
break;
}
case 6:
{
G.m_game.m_optionFont++;
if ( G.m_game.m_optionFont>9 )
G.m_game.m_optionFont = 0;
m_itemsAlt[iRect] = (G.m_game.m_optionFont+1).ToString();
G.m_game.__217();
G.m_game.__219();
G.m_game.__248();
break;
}
case 7:
{
G.m_game.m_optionQuality++;
if ( G.m_game.m_optionQuality>QUALITY.LOW )
G.m_game.m_optionQuality = QUALITY.HIGH;
m_itemsAlt[iRect] = G.m_game.__213();
G.m_game.__248();
G.m_game.__221();
break;
}
case 8:
{
if ( G.m_game.m_optionResolution==-1 )
G.m_game.m_optionResolution = 0;
G.m_game.m_optionResolution++;
if ( G.m_game.m_optionResolution>=G.m_game.m_resolutionList.Count )
G.m_game.m_optionResolution = 0;
m_itemsAlt[iRect] = G.m_game.m_resolutionList[G.m_game.m_optionResolution];
G.m_game.__248();
m_resolutionChanged = true;
break;
}
case 9:
{
G.m_game.m_optionFullscreen = !G.m_game.m_optionFullscreen;
m_itemsAlt[iRect] = G.m_game.m_optionFullscreen ? Localization.m_wordMenuOn.Get() : Localization.m_wordMenuOff.Get();
G.m_game.__248();
m_resolutionChanged = true;
break;
}
case 10:
{
G.OpenUrl(G.m_game.m_privacyPolicy);
break;
}
case 12:
{
if ( m_resolutionChanged )
G.m_game.__219();
__459(ID_MAIN);
break;
}
}
break;
}
}
return;
}
}
switch ( m_id )
{
case ID_MAIN:
{
for ( int i=0 ; i<G.m_game.m_menuUserButtonSprites.Length ; i++ )
{
if ( __464(i).Contains(x, y) )
{
string url = __465(i);
if ( AgePlugin.OnUserButton(i+1, url) )
G.OpenUrl(url);
return;
}
}
break;
}
case ID_HELP:
{
__459(ID_MAIN);
break;
}
case ID_CREDITS:
{
if ( m_onlyOnePageNavigation )
Close();
else if ( G.m_game.m_customMenu.Length==0 )
__459(ID_MAIN);
else
G.m_game.__257();
break;
}
case ID_SPLASH1:
{
if ( G.m_game.m_menuSplashDurations[0]==0 )
__460();
break;
}
case ID_SPLASH2:
{
if ( G.m_game.m_menuSplashDurations[1]==0 )
__460();
break;
}
}
}
}
public void Close(bool resumeSounds = true)
{
switch ( m_id )
{
case ID_HELP:
{
G.m_game.m_menuHelpSprite.End();
break;
}
case ID_SPLASH1:
{
G.m_game.m_menuSplashSprites[0].End();
break;
}
case ID_SPLASH2:
{
G.m_game.m_menuSplashSprites[1].End();
break;
}
}
m_id = 0;
m_items.Clear();
m_itemsAlt.Clear();
for ( int i=0 ; i<m_loadedSprites.Count ; i++ )
m_loadedSprites[i].End();
m_loadedSprites.Clear();
if ( resumeSounds )
G.m_game.__270();
}
public void __458()
{
if ( __38() )
Open(m_id, m_intro, m_disableSave, true);
}
public void __459(int id)
{
if ( __38() )
Open(id, m_intro, m_disableSave);
}
public void __460()
{
if ( m_id==ID_LOGO )
{
if ( G.m_game.m_menuSplashSprites[0]==null )
{
if ( G.m_game.m_customMenu.Length==0 )
__459(ID_MAIN);
else
{
Close();
G.m_game.__302(G.m_game.m_customMenu);
}
}
else
__459(ID_SPLASH1);
}
else if ( m_id==ID_SPLASH1 )
{
if ( G.m_game.m_menuSplashSprites[1]==null )
{
if ( G.m_game.m_customMenu.Length==0 )
__459(ID_MAIN);
else
{
Close();
G.m_game.__302(G.m_game.m_customMenu);
}
}
else
__459(ID_SPLASH2);
}
else if ( m_id==ID_SPLASH2 )
{
if ( G.m_game.m_customMenu.Length==0 )
__459(ID_MAIN);
else
{
Close();
G.m_game.__302(G.m_game.m_customMenu);
}
}
}
public bool __38()
{
return m_id!=0;
}
public bool __461()
{
switch ( m_id )
{
case ID_MAIN:
case ID_PLAY:
case ID_LOADGAME:
case ID_SAVEGAME:
case ID_ACCOUNT:
case ID_UPLOAD:
case ID_OPTIONS:
return true;
}
return false;
}
public List<Rect> __462()
{
float width = G.m_rcViewUI.width;
float height = G.m_rcViewUI.height;
Police font = G.m_game.__215();
float fontHeight = font.__446();
float lineSpacing = font.__490();
float extraLineSpacing = fontHeight*0.2f;
float separatorLineSpacing = fontHeight*0.5f;
List<Rect> rects = new List<Rect>();
Rect rc = new Rect();
float y = 0.0f;
for ( int i=0 ; i<m_items.Count ; i++ )
{
string text = m_items[i];
if ( text=="+" )
{
rc.width = 0;
rc.height = 0;
rects.Add(rc);
y += separatorLineSpacing;
continue;
}
if ( text.Length>0 && text[0]=='@' )
text = text.Substring(1);
Vec2 size = font.__491(text);
rc.width = size.x;
rc.height = size.y;
rc.y = y;
if ( m_id==ID_OPTIONS && i<m_itemsAlt.Count )
rc.x = width*0.5f - rc.width - 8.0f;
else
rc.x = width*0.5f - rc.width*0.5f;
rects.Add(rc);
if ( rc.height>0.0f )
{
y += rc.height;
if ( i<m_items.Count-1 )
{
y += lineSpacing;
y += extraLineSpacing;
}
}
}
y = height*0.5f - y*0.5f;
y += G.m_rcViewUI.y;
for ( int i=0 ; i<m_items.Count ; i++ )
{
rc = rects[i];
rc.x += G.m_rcViewUI.x;
rc.y += y;
rects[i] = rc;
}
return rects;
}
public List<Rect> __463(List<Rect> mainRects)
{
float width = G.m_rcViewUI.width;
Police font = G.m_game.__215();
List<Rect> rects = new List<Rect>();
Rect rc = new Rect();
for ( int i=0 ; i<m_itemsAlt.Count ; i++ )
{
Vec2 size = font.__491(m_itemsAlt[i]);
rc.width = size.x;
rc.height = size.y;
rc.x = G.m_rcViewUI.x + width*0.5f + 8.0f;
rc.y = mainRects[i].y;
rects.Add(rc);
}
return rects;
}
public Rect __464(int index)
{
Rect rc = Rect.Zero;
if ( index<0 || index>=G.m_game.m_menuUserButtonSprites.Length || G.m_game.m_menuUserButtonSprites[index]==null || G.m_game.m_menuUserButtonUrls[index].Length==0 || G.m_game.m_menuUserButtonVisibilities[index]==false )
return rc;
int buttonCount = 0;
for ( int i=0 ; i<G.m_game.m_menuUserButtonSprites.Length ; i++ )
{
if ( G.m_game.m_menuUserButtonSprites[i] && G.m_game.m_menuUserButtonUrls[i].Length>0 && G.m_game.m_menuUserButtonVisibilities[i] )
buttonCount++;
}
float size = 96.0f * G.m_game.m_menuUserButtonSize;
float space = 48.0f;
float length = buttonCount*size + (buttonCount-1)*space;
switch ( G.m_game.m_menuUserButtonPos )
{
case "Left":
rc.x = G.m_rcViewUI.x + space;
rc.y = G.m_rcViewUI.__438().y - length*0.5f + index*(size+space);
break;
case "Right":
rc.x = G.m_rcViewUI.__436() - size - space;
rc.y = G.m_rcViewUI.__438().y - length*0.5f + index*(size+space);
break;
case "Top":
rc.x = G.m_rcViewUI.__438().x - length*0.5f + index*(size+space);
rc.y = G.m_rcViewUI.y + space;
break;
case "Top Left":
rc.x = G.m_rcViewUI.x + space + index*(size+space);
rc.y = G.m_rcViewUI.y + space;
break;
case "Top Right":
rc.x = G.m_rcViewUI.__436() - length - space + index*(size+space);
rc.y = G.m_rcViewUI.y + space;
break;
case "Bottom Left":
rc.x = G.m_rcViewUI.x + space + index*(size+space);
rc.y = G.m_rcViewUI.__437() - size - space;
break;
case "Bottom Right":
rc.x = G.m_rcViewUI.__436() - length - space + index*(size+space);
rc.y = G.m_rcViewUI.__437() - size - space;
break;
default:
rc.x = G.m_rcViewUI.__438().x - length/2 + index*(size+space);
rc.y = G.m_rcViewUI.__437() - size - space;
break;
}
rc.width = size;
rc.height = size;
return rc;
}
public string __465(int index)
{
if ( index<0 || index>=G.m_game.m_menuUserButtonSprites.Length )
return "";
return G.m_game.m_menuUserButtonUrls[index];
}
public void __466()
{
if ( m_intro )
{
G.m_game.__261();
G.m_game.__239();
G.m_game.__241();
Close(false);
}
else
{
G.m_game.__240();
G.m_game.__241();
Close(false);
}
}
public override void __42()
{
if ( __38() )
{
if ( m_intro && m_menuMusicPlayed==false && m_id!=ID_LOGO && m_id!=ID_SPLASH1 && m_id!=ID_SPLASH2 && m_id!=ID_START )
{
m_menuMusicPlayed = true;
if ( G.m_game.m_hasMenuMusic )
{
if ( G.m_game.__260()==false )
G.m_game.__264(0, "MENU");
}
}
if ( m_id==ID_CREDITS )
{
float speed = G.m_rcViewUI.height / 15.0f;
m_scrollCredits += G.m_game.m_elapsed * speed;
}
if ( m_id==ID_LOGO )
{
float duration = G.m_game.m_licenseWarning ? 5.0f : 2.0f;
if ( G.m_game.m_time-m_startTime>duration )
__460();
}
else if ( m_id==ID_SPLASH1 )
{
if ( G.m_game.m_menuSplashDurations[0]!=0 && (G.m_game.m_time-m_startTime)>=G.m_game.m_menuSplashDurations[0] )
__460();
}
else if ( m_id==ID_SPLASH2 )
{
if ( G.m_game.m_menuSplashDurations[1]!=0 && (G.m_game.m_time-m_startTime)>=G.m_game.m_menuSplashDurations[1] )
__460();
}
}
base.__42();
}
public override void __43()
{
if ( __38() )
{
Police font = G.m_game.__215();
bool leftToRight = font.__489();
float playtime = G.m_game.m_time - m_startTime;
bool isReady = playtime>0.25f;
if ( MessageBox.m_instance.__38() )
isReady = false;
Rect rc;
rc.x = G.m_rcView.__438().x - G.m_rcGame.width*0.5f;
rc.y = 0.0f;
rc.width = G.m_rcGame.width;
rc.height = G.m_rcView.height;
G.__171();
switch ( m_id )
{
case ID_START:
{
break;
}
case ID_HELP:
{
G.m_graphics.__354(G.m_game.m_menuHelpSprite.m_material, ref rc);
break;
}
case ID_LOGO:
{
float view = Mathf.Min(G.m_rcView.width, G.m_rcView.height);
float size = view*0.2f;
rc.x = G.m_rcView.__438().x - size*0.5f;
rc.y = G.m_rcView.__438().y - size*0.5f;
rc.width = size;
rc.height = size;
G.m_graphics.__354(G.m_game.m_logoMaterial, ref rc);
if ( G.m_game.m_licenseWarning && Mathf.Repeat(playtime, 0.5f)<0.4f )
font.__492("Please do not forget to build the Unity project before publishing your game. This build is only intended to debug your game.", ref G.m_colorWhite, G.m_rcView.width);
break;
}
case ID_SPLASH1:
{
G.m_graphics.__354(G.m_game.m_menuSplashSprites[0].m_material, ref rc);
break;
}
case ID_SPLASH2:
{
G.m_graphics.__354(G.m_game.m_menuSplashSprites[1].m_material, ref rc);
break;
}
default:
{
if ( G.m_game.m_menuBack )
G.m_graphics.__354(G.m_game.m_menuBack.m_material, ref rc);
break;
}
}
G.m_graphics.__352(FXO.MENU_BACK);
if ( m_id==ID_MAIN )
{
for ( int i=0 ; i<G.m_game.m_menuUserButtonSprites.Length ; i++ )
{
rc = __464(i);
if ( rc.width>0 )
G.m_graphics.__354(G.m_game.m_menuUserButtonSprites[i].m_material, ref rc);
}
}
G.m_graphics.__352(FXO.MENU_HUD);
if ( m_id==ID_CREDITS )
{
float fontHeight = font.__446();
float lineSpacing = font.__490();
Vec2 size = Vec2.Zero;
float y = G.m_rcViewUI.__437() - m_scrollCredits;
for ( int i=0 ; i<m_credits.Count ; i++ )
{
string line = m_credits[i].Get();
if ( line.Length==0 )
line = " ";
Color color = line[0]=='@' ? G.m_game.m_colorTextCredits2 : G.m_game.m_colorTextCredits;
Obj obj = null;
if ( line[0]=='@' )
{
line = line.Substring(1);
obj = G.m_game.__277(line);
}
if ( obj==null )
{
size = font.__491(line);
if ( size.y==0 )
{
y += fontHeight;
y += lineSpacing;
continue;
}
float x = G.m_rcViewUI.x + G.m_rcViewUI.width*0.5f;
x = leftToRight ? x-size.x*0.5f : x+size.x*0.5f;
font.__70(ref line, x, y, ref color);
y += size.y;
y += lineSpacing;
}
else
{
Anim anim = obj.__470("STOP");
if ( anim && anim.__474(AnimDir.RIGHT) && anim.m_dirs[AnimDir.RIGHT].__475()>0 )
{
float time = G.m_game.m_time * anim.m_fps;
int index = (int)(time % anim.m_dirs[AnimDir.RIGHT].__475());
Frame frame = anim.m_dirs[AnimDir.RIGHT].m_frames[index];
if ( frame && frame.m_sprite )
{
if ( frame.m_sprite.__988()==false )
{
Asset asset = G.__95(G.m_pathGraphics);
if ( asset )
{
frame.m_sprite.__468(asset);
asset.Close();
m_loadedSprites.Add(frame.m_sprite);
}
}
rc.width = obj.m_imgWidth;
rc.height = obj.m_imgHeight;
rc.x = G.m_rcViewUI.__439() - rc.width*0.5f;
rc.y = y;
G.m_graphics.__359(frame, ref rc);
y += rc.height;
}
}
}
}
if ( y<-size.y )
m_scrollCredits = 0.0f;
}
else
{
List<Rect> rects = __462();
for ( int i=0 ; i<rects.Count ; i++ )
{
if ( rects[i].width==0 )
continue;
string text = m_items[i];
Color color;
if ( text.Length>0 && text[0]=='@' )
{
text = text.Substring(1);
color = G.m_game.m_colorTextMenu3;
}
else if ( isReady && G.m_game.m_cursor && rects[i].Contains(G.m_game.m_cursorViewX, G.m_game.m_cursorViewY) )
color = G.m_game.m_colorTextMenu2;
else
{
if ( m_id==ID_MAIN && i==6 )
color = G.m_game.m_colorTextMenu4;
else
color = G.m_game.m_colorTextMenu1;
}
font.__70(ref text, leftToRight ? rects[i].x : rects[i].__436(), rects[i].y, ref color);
}
rects = __463(rects);
for ( int i=0 ; i<rects.Count ; i++ )
font.__70(m_itemsAlt[i], leftToRight ? rects[i].x : rects[i].__436(), rects[i].y, ref G.m_game.m_colorTextMenu3);
}
G.m_graphics.__352(FXO.MENU_TEXT);
}
base.__43();
}
}
