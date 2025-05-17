using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Data;
public class AgeGame
{
public List<string> m_resolutionList = new List<string>();
public InputManager m_input;
public bool m_userCursorVisible = true;
public bool m_userSubtitleVisible = true;
public bool m_cursor;
public float m_cursorViewX;
public float m_cursorViewY;
public Rect m_rcCursor;
public Texture2D m_logoTexture;
public Material m_logoMaterial;
public Serial<Sprite> m_uiCursor;
public Serial<Sprite> m_uiCursorObj;
public Serial<Sprite> m_uiCursorDoor;
public Serial<Sprite> m_uiCursorBusy;
public Sprite m_uiChoice;
public Sprite m_uiBalloon;
public Sprite m_uiWay;
public Sprite m_uiCheat;
public Sprite m_uiCheatDoor;
public Sprite m_uiCheatLabel;
public Sprite m_uiCheatObject;
public bool m_cheatUsed;
public string m_menuUserButtonPos;
public float m_menuUserButtonSize;
public Sprite[] m_menuUserButtonSprites = new Sprite[6];
public string[] m_menuUserButtonUrls = new string[6];
public bool[] m_menuUserButtonVisibilities = new bool[6];
public Sprite[] m_menuSplashSprites = new Sprite[2];
public float[] m_menuSplashDurations = new float[2];
public bool m_licenseWarning = false;
public Sprite m_menuHelpSprite;
public Sprite m_menuBack;
public Sprite m_uiDialogBack;
public bool m_portrait;
public bool m_forceNativeFullscreen;
public RATIOLESS m_ratioLess;
public RATIOGREAT m_ratioGreat;
public bool m_pixelPerfect;
public bool m_png256;
public string m_gamePackage;
public string m_gameName;
public string m_gameNameForFile;
public string m_gameVersion;
public string m_gameAuthor;
public string m_gameCopyright;
public bool m_gameSignatureAuto;
public int m_gameSignatureValue;
public Layout m_layout;
public Color m_colorText = new Color(1.0f, 1.0f, 1.0f);
public Color m_colorTextHighlight;
public Color m_colorTextDoor;
public Color m_colorTextMenu1;
public Color m_colorTextMenu2;
public Color m_colorTextMenu3;
public Color m_colorTextMenu4;
public Color m_colorTextCredits;
public Color m_colorTextCredits2;
public Color m_colorTextBack;
public Texture2D m_lutTexture;
public Dictionary<string, int> m_lutNames;
public float m_fontScale = 0.0f;
public Dictionary<string, string> m_symbols = new Dictionary<string, string>();
public Serial<Effect> m_menuEffect;
public Serial<Effect> m_effect;
public string m_privacyPolicy;
public bool m_domainAllowed = true;
public bool m_lightBaked;
public float m_lightAmbient;
public float m_lightBlur;
public bool m_lightLow;
public float m_bokehShapeWidth;
public float m_bokehShapeHeight;
public bool m_configLive;
public bool m_configDebug;
public bool m_configRun;
public string m_liveScene = "";
public bool m_earlyAccess;
public string m_customMenu;
public bool m_forceLowQuality;
public int m_forceLowQualityMemory;
public Language[] m_languages;
public Police m_font;
public bool m_randomizeDialogChoices;
public float m_subtitleMargin;
public bool m_typewriter;
public bool m_vibration;
public bool m_balloon;
public Margin m_balloonImageMargin;
public Margin m_balloonImageMargin01;
public Margin m_balloonTextMargin;
public bool m_choiceFrame;
public float m_choiceFrameSize;
public float m_choiceFrameIconSize;
public float[] m_choiceFrameSpace = new float[2];
public Margin m_choiceFrameMargin;
public bool m_options;
public bool m_mergeAudioText;
public string m_audioLevel;
public bool m_hasMenuMusic;
public bool m_subtitleOption;
public bool m_fontSizeOption;
public int m_defaultFontSize = 0;
public float m_cursorSize;
public float m_cursorHalfSize;
public string m_storeUrl;
public string m_rateUrl;
public bool m_noLowQuality;
public bool m_hasSongs;
public bool m_hasVideos;
public bool m_hasVoices;
public bool m_savegameEnabled;
public bool m_savegameServerEnabled;
public string m_savegameServerUrl;
public string m_savegameServerGame;
public string m_savegameServerUser = "";
public string m_savegameServerPass = "";
public bool m_autoSavePuzzles;
public Dictionary<int, (int,int)> m_chapters = null;
public Script m_initScript;
public Scenario m_scenario;
public Scene[] m_scenes;
public Dictionary<int, Scene> m_scenesBySID;
public Dictionary<string, Scene> m_scenesByUID;
public Obj[] m_objects;
public Dictionary<int, Obj> m_objectsBySID;
public Dictionary<string, Obj> m_objectsByUID;
public Dialog[] m_dialogs;
public Dictionary<int, Dialog> m_dialogsBySID;
public Dictionary<string, Dialog> m_dialogsByUID;
public Player[] m_players;
public Dictionary<int, Player> m_playersBySID;
public Dictionary<string, Player> m_playersByUID;
public Cinematic[] m_cinematics;
public Dictionary<int, Cinematic> m_cinematicsBySID;
public Dictionary<string, Cinematic> m_cinematicsByUID;
public Effect[] m_effects;
public Dictionary<int, Effect> m_effectsBySID;
public Dictionary<string, Effect> m_effectsByUID;
public Role[] m_roles;
public Dictionary<int, Role> m_rolesBySID;
public Dictionary<string, Role> m_rolesByUID;
public Dictionary<string, Sound> m_sounds = new Dictionary<string, Sound>(StringComparer.OrdinalIgnoreCase);
public Dictionary<string, string> m_gameValues;
public Dictionary<string, Variable> m_variables;
public Variable m_sysVarArg;
public Variable m_sysVarChoice;
public Variable m_sysVarLabel;
public Variable m_sysVarObj;
public Variable m_sysVarObj2;
public Variable m_sysVarOr;
public Variable m_sysVarPlayer;
public Variable m_sysVarPlayer2;
public Variable m_sysVarRes;
public Variable m_sysVarSub;
public MenuGame m_menuGame = new MenuGame();
public MenuDialog m_menuDialog = new MenuDialog();
public CinematicPlayer m_cinematicPlayer = new CinematicPlayer();
public SceneCursor m_entityCursor = new SceneCursor();
public int m_optionLanguageDef = 0;
public int m_optionLanguageText = 0;
public int m_optionLanguageAudio = 0;
public SUBTITLE m_optionSubtitle = SUBTITLE.FAST;
public int m_optionSound = 8;
public int m_optionVoice = 8;
public int m_optionSong = 8;
public int m_optionFont = 3;
public QUALITY m_optionQuality = QUALITY.HIGH;
public int m_optionResolution = -1;
public bool m_optionFullscreen = false;
public bool m_started = false;
public bool m_isGameOver = false;
public Scene m_menuScene = null;
public bool m_customMenuSaveDisabled = false;
public int m_iScene = -1;
public int m_iOldScene = -1;
public int m_iPlayer = -1;
public float m_cursorObjTime;
public Obj m_cursorObj = null;
public bool m_cursorVisible;
public bool m_gestureBagLocked;
public bool m_gestureMenuLocked;
public bool m_locked = false;
public float m_lockedDuration = -1.0f;
public bool m_saveMenuLocked = false;
public bool m_useLocked = false;
public int m_lockedByAnim = 0;
public List<Taker> m_takers = new List<Taker>();
public float m_brightness;
public FADING m_fade = FADING.NONE;
public bool m_fadeLock = false;
public Color m_fadeColor = Color.black;
public float m_fadeOutDuration = 0.0f;
public float m_fadeWaitDuration = 0.0f;
public float m_fadeInDuration = 0.0f;
public float m_fadeTime = 0.0f;
public bool m_fadeJump = false;
public bool m_fadeJumpEnterExitEvents = true;
public bool m_fadeJumpSwitchEvents = false;
public bool m_fadeJumpPlayer = false;
public string m_fadeJumpScene = "";
public RoleBox m_fadeRoleBox = null;
public int m_fadeRoleBoxToken = 0;
public FADING m_fadeEvent = FADING.CLOSED;
public float m_legendTime = -1.0f;
public string m_legendLastText = "";
public Color m_legendLastColor;
public float m_timeAltIcon;
public uint m_iRenderAltIcon;
public float m_timeDragIcon;
public uint m_iRenderDragIcon;
public Song[] m_songs = new Song[G.CHANNEL_COUNT];
public Sound m_voice = null;
public List<Sound> m_playingSounds = new List<Sound>();
public Sound m_currentVoice = null;
public SubObj m_lastEventSubObj = null;
public bool m_lastEventByUser = true;
public string m_alert = "";
public bool m_hasShowPathPlayer = false;
public bool m_autoSaveAsap = false;
public float m_autoSaveIconStartTime = 0.0f;
public float m_autoSaveIconRatio = 0.0f;
public bool m_interludeLock = false;
public float m_interludeLockTime = 0.0f;
public float m_interludeLockDuration = 0.0f;
public bool m_interludeAsap = false;
public string m_interludeParam = "";
public Frame m_examine = null;
public bool m_examineToBeFree = false;
public float m_examineScale = 1.0f;
public Color m_examineColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
public RoleBox m_examineRoleBox = null;
public int m_examineRoleBoxToken = 0;
public bool m_dragging;
public SceneObj m_dragObj = null;
public Vec2 m_dropPos;
public Color m_colorVoiceOver;
public string m_cursorCustom = "";
public Timeline m_timeline = null;
public float m_time;
public float m_elapsed;
private float m_internalElapsed;
public static implicit operator bool(AgeGame inst) { return inst!=null; }
public void __78()
{
m_optionResolution = -1;
m_optionFullscreen = G.m_nativeScreenFull;
m_resolutionList.Clear();
if ( G.__86() && G.__87()==false )
{
Resolution[] resolutions = Screen.resolutions;
for ( int i=0 ; i<resolutions.Length ; i++ )
{
if ( resolutions[i].height<720 )
continue;
string name = resolutions[i].width.ToString() + "x" + resolutions[i].height.ToString();
if ( m_resolutionList.IndexOf(name)==-1 )
{
m_resolutionList.Add(name);
if ( resolutions[i].width==G.m_nativeScreenWidth && resolutions[i].height==G.m_nativeScreenHeight )
m_optionResolution = m_resolutionList.Count - 1;
}
}
}
else
{
m_resolutionList.Add(G.m_nativeScreenWidth.ToString()+"x"+G.m_nativeScreenHeight.ToString());
m_optionResolution = 0;
m_optionFullscreen = true;
}
m_time = 0.0f;
m_elapsed = 0.0f;
m_internalElapsed = 0.0f;
m_cinematicPlayer.Init();
m_input = new InputManager();
m_voice = new Sound(Sound.TYPE.VOICE);
for ( int i=0 ; i<G.CHANNEL_COUNT ; i++ )
m_songs[i] = new Song();
MessageBox messageBox = new MessageBox();
messageBox.Init();
m_logoTexture = (Texture2D)Resources.Load("Textures/logo", typeof(Texture2D));
m_logoMaterial = G.__165(SHADER.TEXTURE32);
m_logoMaterial.mainTexture = m_logoTexture;
}
public bool __64()
{
m_lutTexture = (Texture2D)Resources.Load("Textures/Lut", typeof(Texture2D));
TextAsset file = (TextAsset)Resources.Load("Texts/Lut", typeof(TextAsset));
string[] seps = new string[1];
seps[0] = "\r\n";
string[] result = file.text.Split(seps, StringSplitOptions.None);
m_lutNames = new Dictionary<string, int>();
m_lutNames.Add("Grayscale", 0);
for ( int i=0 ; i<result.Length ; i++ )
m_lutNames.Add(result[i], i+1);
if ( __89()==false )
return false;
__240();
if ( m_portrait )
{
#if UNITY_ANDROID || UNITY_IOS
Screen.autorotateToLandscapeLeft = false;
Screen.autorotateToLandscapeRight = false;
Screen.orientation = ScreenOrientation.Portrait;
float old = G.m_rcWindow.width;
G.m_rcWindow.width = G.m_rcWindow.height;
G.m_rcWindow.height = old;
#endif
}
__219(true);
return true;
}
public void __65()
{
m_lutTexture = null;
}
public string __204()
{
#if UNITY_STANDALONE_WIN
return "windows";
#elif UNITY_WEBGL
return "web";
#elif UNITY_ANDROID
return "android";
#elif UNITY_IOS
return "ios";
#else
return "";
#endif
}
public int __205(string name)
{
if ( name.Length==0 )
return -1;
for ( int i=0 ; i<m_languages.Length ; i++ )
{
if ( G.__148(ref m_languages[i].m_name, ref name) )
return i;
}
return -1;
}
public Language __206()
{
return m_languages[m_optionLanguageText];
}
public string __207()
{
return m_languages[m_optionLanguageText].m_name;
}
public string __208()
{
return Localization.m_wordMenuLanguageName.m_values[m_optionLanguageText];
}
public Language __209()
{
return m_languages[m_optionLanguageAudio];
}
public string __210()
{
return m_languages[m_optionLanguageAudio].m_name;
}
public string __211()
{
return Localization.m_wordMenuLanguageName.m_values[m_optionLanguageAudio];
}
public string __212()
{
switch ( m_optionSubtitle )
{
case SUBTITLE.NONE:		return Localization.m_wordMenuNone.Get();
case SUBTITLE.SLOW:		return Localization.m_wordMenuSlow.Get();
case SUBTITLE.FAST:		return Localization.m_wordMenuFast.Get();
case SUBTITLE.MANUAL:	return Localization.m_wordMenuManual.Get();
}
return "";
}
public string __213()
{
switch ( m_optionQuality )
{
case QUALITY.HIGH:		return Localization.m_wordMenuHigh.Get();
case QUALITY.LOW:		return Localization.m_wordMenuLow.Get();
}
return "";
}
public bool __214()
{
return m_optionQuality==QUALITY.HIGH;
}
public Police __215()
{
if ( m_languages[m_optionLanguageText].m_cjkFont )
return m_languages[m_optionLanguageText].m_cjkFont;
return m_font;
}
public Police __216()
{
return m_font;
}
public void __217()
{
switch ( m_optionFont )
{
case 0:
m_fontScale = 0.7f;	break;
case 1:
m_fontScale = 0.8f;	break;
case 2:
m_fontScale = 0.9f;	break;
default:
case 3:
m_fontScale = 1.0f;	break;
case 4:
m_fontScale = 1.1f;	break;
case 5:
m_fontScale = 1.2f;	break;
case 6:
m_fontScale = 1.3f;	break;
case 7:
m_fontScale = 1.4f;	break;
case 8:
m_fontScale = 1.5f;	break;
case 9:
m_fontScale = 1.6f;	break;
}
if ( m_font )
m_font.m_scale = m_fontScale;
for ( int i=0 ; i<m_languages.Length ; i++ )
{
if ( m_languages[i].m_cjkFont )
m_languages[i].m_cjkFont.m_scale = m_fontScale;
}
}
public void __218(Asset assetGraphics = null)
{
for ( int i=0 ; i<m_languages.Length ; i++ )
{
if ( m_languages[i].m_cjkFont )
m_languages[i].m_cjkFont.End();
}
Language lang = __206();
if ( lang.m_cjk==false )
return;
bool opened = false;
if ( assetGraphics==null )
{
opened = true;
assetGraphics = G.__95(G.m_pathGraphics);
}
if ( lang.m_cjkFont )
lang.m_cjkFont.__468(assetGraphics);
if ( opened )
assetGraphics.Close();
}
public void __219(bool atStart = false)
{
m_menuGame.m_resolutionChanged = false;
float width = G.m_rcWindow.width;
float height = G.m_rcWindow.height;
if ( G.__86() && G.__87()==false )
{
if ( m_optionResolution!=-1 )
{
string resolution = m_resolutionList[m_optionResolution];
string[] values = resolution.Split('x');
width = G.__113(ref values[0]);
height = G.__113(ref values[1]);
if ( atStart==false || m_forceNativeFullscreen )
Screen.SetResolution((int)width, (int)height, m_optionFullscreen);
}
}
__220(width, height);
m_layout.__219();
for ( int i=0 ; i<m_scenes.Length ; i++ )
m_scenes[i].__219();
}
public void __220(float width, float height)
{
G.m_rcWindow = new Rect(0.0f, 0.0f, width, height);
G.m_windowRatio = width/height;
G.m_screenLessGame = G.m_windowRatio<=G.m_gameRatio;
G.m_rcView.width = G.m_rcGame.width;
G.m_rcView.height = G.m_rcGame.height;
G.m_viewRatio = G.m_gameRatio;
G.m_rcViewUI = G.m_rcView;
float margin;
if ( G.m_screenLessGame )
{
RATIOLESS ratioLess = m_ratioLess;
switch ( ratioLess )
{
case RATIOLESS.SCROLL:
G.m_clientRatio = G.m_windowRatio;
G.m_rcClient = G.m_rcWindow;
G.m_rcView.width = G.m_rcGame.height*G.m_windowRatio;
G.m_rcView.height = G.m_rcGame.height;
G.m_viewRatio = G.m_rcView.width/G.m_rcView.height;
if ( G.m_viewRatio>G.m_gameRatio )
{
G.m_rcView.width = G.m_rcGame.width;
G.m_viewRatio = G.m_gameRatio;
G.m_rcClient.width = G.m_rcClient.height*G.m_viewRatio;
G.m_rcClient.x = (G.m_rcWindow.width-G.m_rcClient.width)*0.5f;
G.m_clientRatio = G.m_viewRatio;
}
G.m_rcViewUI = G.m_rcView;
break;
case RATIOLESS.LETTERBOX:
G.m_clientRatio = G.m_gameRatio;
G.m_rcClient.width = G.m_rcWindow.width;
G.m_rcClient.height = G.m_rcClient.width/G.m_clientRatio;
G.m_rcClient.x = 0.0f;
G.m_rcClient.y = (G.m_rcWindow.height-G.m_rcClient.height)*0.5f;
break;
case RATIOLESS.LETTERBOX_TOP:
G.m_clientRatio = G.m_gameRatio;
G.m_rcClient.width = G.m_rcWindow.width;
G.m_rcClient.height = G.m_rcClient.width/G.m_clientRatio;
G.m_rcClient.x = 0.0f;
G.m_rcClient.y = G.m_rcWindow.height-G.m_rcClient.height;
break;
case RATIOLESS.LETTERBOX_BOTTOM:
G.m_clientRatio = G.m_gameRatio;
G.m_rcClient.width = G.m_rcWindow.width;
G.m_rcClient.height = G.m_rcClient.width/G.m_clientRatio;
G.m_rcClient.x = 0.0f;
G.m_rcClient.y = 0.0f;
break;
case RATIOLESS.CROP:
G.m_clientRatio = G.m_gameRatio;
G.m_rcClient = G.m_rcWindow;
G.m_rcClient.width = G.m_rcClient.height*G.m_clientRatio;
G.m_rcClient.x = (G.m_rcWindow.width - G.m_rcClient.width)*0.5f;
margin = (G.m_rcGame.width - G.m_rcGame.height*G.m_windowRatio)*0.5f;
G.m_rcViewUI.width -= margin*2.0f;
G.m_rcViewUI.x += margin;
break;
case RATIOLESS.CROP_LEFT:
G.m_clientRatio = G.m_gameRatio;
G.m_rcClient = G.m_rcWindow;
G.m_rcClient.width = G.m_rcClient.height*G.m_clientRatio;
G.m_rcClient.x = G.m_rcWindow.width - G.m_rcClient.width;
margin = G.m_rcGame.width - G.m_rcGame.height*G.m_windowRatio;
G.m_rcViewUI.width -= margin;
G.m_rcViewUI.x += margin;
break;
case RATIOLESS.CROP_RIGHT:
G.m_clientRatio = G.m_gameRatio;
G.m_rcClient = G.m_rcWindow;
G.m_rcClient.width = G.m_rcClient.height*G.m_clientRatio;
margin = G.m_rcGame.width - G.m_rcGame.height*G.m_windowRatio;
G.m_rcViewUI.width -= margin;
break;
}
}
else
{
RATIOGREAT ratioGreat = m_ratioGreat;
switch ( ratioGreat )
{
case RATIOGREAT.LETTERBOX:
G.m_clientRatio = G.m_gameRatio;
G.m_rcClient.height = G.m_rcWindow.height;
G.m_rcClient.width = G.m_rcClient.height*G.m_clientRatio;
G.m_rcClient.width += G.m_rcClient.width % 2;
G.m_rcClient.x = (G.m_rcWindow.width-G.m_rcClient.width)*0.5f;
G.m_rcClient.y = 0.0f;
break;
case RATIOGREAT.LETTERBOX_LEFT:
G.m_clientRatio = G.m_gameRatio;
G.m_rcClient.height = G.m_rcWindow.height;
G.m_rcClient.width = G.m_rcClient.height*G.m_clientRatio;
G.m_rcClient.width += G.m_rcClient.width % 2;
G.m_rcClient.x = G.m_rcWindow.width-G.m_rcClient.width;
G.m_rcClient.y = 0.0f;
break;
case RATIOGREAT.LETTERBOX_RIGHT:
G.m_clientRatio = G.m_gameRatio;
G.m_rcClient.height = G.m_rcWindow.height;
G.m_rcClient.width = G.m_rcClient.height*G.m_clientRatio;
G.m_rcClient.width += G.m_rcClient.width % 2;
G.m_rcClient.x = 0.0f;
G.m_rcClient.y = 0.0f;
break;
case RATIOGREAT.CROP:
G.m_clientRatio = G.m_gameRatio;
G.m_rcClient = G.m_rcWindow;
G.m_rcClient.height = G.m_rcClient.width/G.m_clientRatio;
G.m_rcClient.y = (G.m_rcWindow.height - G.m_rcClient.height)*0.5f;
margin = (G.m_rcGame.height - G.m_rcGame.width/G.m_windowRatio)*0.5f;
G.m_rcViewUI.height -= margin*2.0f;
G.m_rcViewUI.y += margin;
break;
case RATIOGREAT.CROP_TOP:
G.m_clientRatio = G.m_gameRatio;
G.m_rcClient = G.m_rcWindow;
G.m_rcClient.height = G.m_rcClient.width/G.m_clientRatio;
G.m_rcClient.y = G.m_rcWindow.height - G.m_rcClient.height;
margin = G.m_rcGame.height - G.m_rcGame.width/G.m_windowRatio;
G.m_rcViewUI.height -= margin;
G.m_rcViewUI.y += margin;
break;
case RATIOGREAT.CROP_BOTTOM:
G.m_clientRatio = G.m_gameRatio;
G.m_rcClient = G.m_rcWindow;
G.m_rcClient.height = G.m_rcClient.width/G.m_clientRatio;
margin = G.m_rcGame.height - G.m_rcGame.width/G.m_windowRatio;
G.m_rcViewUI.height -= margin;
break;
}
}
}
public void __221(Asset assetGraphics = null)
{
Asset asset = assetGraphics==null ? G.__95(G.m_pathGraphics) : assetGraphics;
if ( asset==null )
return;
for ( int i=0 ; i<m_menuSplashSprites.Length ; i++ )
__237(asset, m_menuSplashSprites[i]);
__237(asset, m_menuHelpSprite);
m_layout.__416(asset);
__237(asset, m_uiDialogBack);
__237(asset, m_menuBack);
__237(asset, m_uiCursor.init);
__237(asset, m_uiCursorObj.init);
__237(asset, m_uiCursorDoor.init);
__237(asset, m_uiCursorBusy.init);
__237(asset, m_uiCursor.cur);
__237(asset, m_uiCursorObj.cur);
__237(asset, m_uiCursorDoor.cur);
__237(asset, m_uiCursorBusy.cur);
__237(asset, m_uiChoice);
__237(asset, m_uiBalloon);
__237(asset, m_uiWay);
__237(asset, m_uiCheat);
__237(asset, m_uiCheatDoor);
__237(asset, m_uiCheatLabel);
__237(asset, m_uiCheatObject);
for ( int i=0 ; i<m_menuUserButtonSprites.Length ; i++ )
__237(asset, m_menuUserButtonSprites[i]);
for ( int i=0 ; i<m_players.Length ; i++ )
{
Player player = m_players[i];
if ( player==null )
continue;
__237(asset, player.__453());
}
Scene scene = __291();
if ( scene )
{
scene.m_backLayer.__468(asset);
for ( int i=0 ; i<G.MASK_COUNT ; i++ )
{
if ( scene.m_maskLayers[i] )
scene.m_maskLayers[i].__468(asset);
}
for ( int i=0 ; i<G.FAR_COUNT ; i++ )
{
if ( scene.m_farLayers[i] )
scene.m_farLayers[i].__468(asset);
}
for ( int iObj=0 ; iObj<scene.m_objects.Length ; iObj++ )
{
Obj obj = scene.m_objects[iObj].m_obj;
if ( obj==null )
continue;
__237(asset, obj.__453());
for ( int i=0 ; i<obj.m_sprites.Length ; i++ )
__237(asset, obj.m_sprites[i]);
}
}
if ( assetGraphics==null )
asset.Close();
}
public bool Examine(RoleBox roleBox, string uid, string animName = "", int dirIndex = -1, float scale = 0.0f, float opacity = 0.0f)
{
__223();
Scene scene = __291();
if ( scene==null )
return false;
Obj obj = __277(uid);
if ( obj==null )
return false;
if ( animName.Length==0 )
animName = "STOP";
Anim anim = obj.__470(ref animName);
if ( anim==null )
return false;
AnimDir dir = anim.m_defaultDir;
if ( dirIndex!=-1 )
{
if ( anim.__474(dirIndex) )
dir = anim.m_dirs[dirIndex];
}
if ( dir==null || dir.__475()==0 )
return false;
return Examine(roleBox, dir.m_frames[0], scale, opacity);
}
public bool Examine(RoleBox roleBox, Frame frame, float scale = 0.0f, float opacity = 0.0f)
{
__223();
if ( frame.m_sprite.__988()==false )
{
Asset asset = G.__95(G.m_pathGraphics);
if ( asset==null )
return false;
frame.m_sprite.__468(asset);
asset.Close();
m_examineToBeFree = true;
}
m_examine = frame;
m_examineScale = scale==0.0f ? 0.75f : scale;
m_examineColor.a = opacity==0.0f ? 1.0f : opacity;
m_examineRoleBox = roleBox;
m_examineRoleBoxToken = roleBox==null ? 0 : roleBox.m_parent.m_token;
return true;
}
public bool __222()
{
return m_examine;
}
public bool __223(bool callEvent = false)
{
if ( callEvent )
{
if ( m_examineRoleBox )
m_examineRoleBox.__457(m_examineRoleBoxToken);
}
__314();
if ( m_examine==null )
return false;
m_examineRoleBox = null;
m_examineRoleBoxToken = 0;
if ( m_examineToBeFree )
m_examine.m_sprite.End();
m_examineToBeFree = false;
m_examine = null;
return true;
}
public bool __224()
{
if ( m_savegameServerUrl.Length==0 || m_savegameServerGame.Length==0 )
return false;
return true;
}
public bool __225()
{
if ( __224()==false )
return false;
if ( m_savegameServerUser.Length==0 || m_savegameServerPass.Length==0 )
return false;
return true;
}
public bool __89()
{
Asset asset = G.__95(G.m_pathGame);
if ( asset==null )
return false;
Asset assetConfig = G.__95(G.m_pathConfig, true);
if ( assetConfig==null )
{
asset.Close();
return false;
}
Asset assetGraphics = G.__95(G.m_pathGraphics);
if ( assetGraphics==null )
{
assetConfig.Close();
asset.Close();
return false;
}
asset.__8();
asset.__18();
m_gamePackage = asset.__18();
m_gameName = asset.__18();
m_gameNameForFile = asset.__18();
m_gameVersion = asset.__18();
m_gameAuthor = asset.__18();
m_gameCopyright = asset.__18();
m_gameSignatureAuto = asset.__10();
m_gameSignatureValue = asset.__15();
int licenseWarning = asset.__12();
#if UNITY_STANDALONE_WIN
m_licenseWarning = G.__101(licenseWarning, 0x01);
#elif UNITY_ANDROID
m_licenseWarning = G.__101(licenseWarning, 0x02);
#elif UNITY_WEBGL
m_licenseWarning = G.__101(licenseWarning, 0x04);
#endif
m_configLive = asset.__12()!=0;
m_configDebug = asset.__12()!=0;
m_configRun = asset.__12()!=0;
m_liveScene = asset.__18();
#if UNITY_STANDALONE_WIN
IDE.m_hWnd = asset.__17();
#else
asset.__17();
#endif
m_earlyAccess = false;
int year = asset.__13();
if ( year!=0 )
{
m_earlyAccess = true;
int month = asset.__12();
int day = asset.__12();
if ( G.m_date[0]>year )
m_earlyAccess = false;
else if ( G.m_date[0]==year )
{
if ( G.m_date[1]>month )
m_earlyAccess = false;
else if ( G.m_date[1]==month )
{
if ( G.m_date[2]>=day )
m_earlyAccess = false;
}
}
}
string[] symbols = asset.__18().Split(' ');
for ( int i=0 ; i<symbols.Length ; i++ )
{
if ( symbols[i].Length>0 )
m_symbols.Add(symbols[i], symbols[i]);
}
G.m_colors = new Color[16];
for ( int i=0 ; i<16 ; i++ )
G.m_colors[i] = new Color(asset.__12()/255.0f, asset.__12()/255.0f, asset.__12()/255.0f);
m_portrait = asset.__10();
m_forceNativeFullscreen = asset.__10();
if ( m_forceNativeFullscreen && (G.__86()==false || G.__87() || G.__88()) )
m_forceNativeFullscreen = false;
if ( m_forceNativeFullscreen )
{
m_optionResolution = m_resolutionList.Count - 1;
m_optionFullscreen = true;
}
m_pixelPerfect = asset.__10();
m_png256 = asset.__10();
switch ( asset.__18() )
{
case "None":	m_optionSubtitle = SUBTITLE.NONE;	break;
case "Manual":	m_optionSubtitle = SUBTITLE.MANUAL;	break;
case "Slow":	m_optionSubtitle = SUBTITLE.SLOW;	break;
}
m_subtitleMargin = asset.__13();
m_privacyPolicy = asset.__18();
m_customMenu = asset.__18();
m_noLowQuality = asset.__10();
m_fontSizeOption = asset.__10();
m_storeUrl = asset.__18();
m_menuUserButtonPos = asset.__18();
m_menuUserButtonSize = G.__114(asset.__18());
m_forceLowQuality = assetConfig.__10();
m_forceLowQualityMemory = assetConfig.__13();
m_defaultFontSize = assetConfig.__12();
m_cursorSize = assetConfig.__13();
m_cursorHalfSize = m_cursorSize*0.5f;
string storeUrl = assetConfig.__18();
m_rateUrl = assetConfig.__18();
float menuUserButtonSize = G.__114(assetConfig.__18());
m_hasSongs = assetConfig.__10();
m_hasVideos = assetConfig.__10();
m_hasVoices = assetConfig.__10();
if ( storeUrl.Length>0 )
m_storeUrl = storeUrl;
if ( menuUserButtonSize!=0.0f )
m_menuUserButtonSize = menuUserButtonSize;
if ( m_forceLowQuality && m_forceLowQualityMemory>0 && m_forceLowQualityMemory<SystemInfo.systemMemorySize )
m_forceLowQuality = false;
if ( m_defaultFontSize>0 )
m_optionFont = m_defaultFontSize - 1;
string defLang = asset.__18();
bool osIfPossible = asset.__10();
m_languages = new Language[asset.__12()];
for ( int i=0 ; i<m_languages.Length ; i++ )
{
Language lang = new Language();
m_languages[i] = lang;
lang.m_name = asset.__18();
lang.m_cjk = asset.__10();
lang.m_leftToRight = asset.__10();
lang.m_fast = asset.__13()/1000.0f;
lang.m_slow = asset.__13()/1000.0f;
if ( lang.m_fast==0.0f || lang.m_slow==0.0f )
{
if ( lang.m_cjk )
{
lang.m_fast = 0.38f;
lang.m_slow = 0.42f;
}
else
{
switch ( lang.m_name.ToLower() )
{
case "french":
lang.m_fast = 0.38f;
lang.m_slow = 0.42f;
break;
case "german":
lang.m_fast = 0.42f;
lang.m_slow = 0.46f;
break;
case "russian":
lang.m_fast = 0.46f;
lang.m_slow = 0.50f;
break;
default:
lang.m_fast = 0.40f;
lang.m_slow = 0.44f;
break;
}
}
}
if ( lang.m_name==defLang )
{
m_optionLanguageDef = i;
m_optionLanguageText = i;
m_optionLanguageAudio = i;
}
}
if ( osIfPossible )
{
string os = G.__193();
for ( int i=0 ; i<m_languages.Length ; i++ )
{
if ( m_languages[i].m_name==os )
{
m_optionLanguageDef = i;
m_optionLanguageText = i;
m_optionLanguageAudio = i;
break;
}
}
}
Localization.__64(asset);
if ( m_forceLowQuality && m_noLowQuality==false )
m_optionQuality = QUALITY.LOW;
for ( int iLang=0 ; iLang<m_languages.Length ; iLang++ )
{
int iLine = 0;
while ( true )
{
string line = asset.__19();
if ( line.Length==0 )
break;
Term term;
if ( iLine<m_menuGame.m_credits.Count )
term = m_menuGame.m_credits[iLine];
else
{
term = new Term();
m_menuGame.m_credits.Add(term);
}
term.m_values[iLang] = line;
iLine++;
}
}
G.m_rcGame = new Rect(0.0f, 0.0f, asset.__13(), asset.__13());
G.m_gameRatio = G.m_rcGame.width/G.m_rcGame.height;
G.m_gameRatioInv = G.m_rcGame.height/G.m_rcGame.width;
G.m_rcView = G.m_rcGame;
G.m_viewRatio = G.m_gameRatio;
m_ratioLess = (RATIOLESS)asset.__12();
m_ratioGreat = (RATIOGREAT)asset.__12();
G.m_materialBlur.SetFloat("_aspect", G.m_gameRatioInv);
G.m_bokehWidth = (int)G.m_rcGame.width/G.BOKEH_DIVIDER;
G.m_bokehHeight = (int)G.m_rcGame.height/G.BOKEH_DIVIDER;
if ( G.m_rcGame.width%G.BOKEH_DIVIDER!=0 )
G.m_bokehWidth++;
if ( G.m_rcGame.height%G.BOKEH_DIVIDER!=0 )
G.m_bokehHeight++;
m_font = new Police();
m_font.__64(asset, m_fontScale);
m_font.__468(assetGraphics);
m_colorText = asset.__23(255, 255, 255);
m_colorTextHighlight = asset.__23(87, 230, 87);
m_colorTextDoor = asset.__23(87, 230, 87);
m_colorTextMenu1 = asset.__23(255, 255, 255);
m_colorTextMenu2 = asset.__23(87, 230, 87);
m_colorTextMenu3 = asset.__23(255, 255, 96);
m_colorTextMenu4 = asset.__23(255, 255, 255);
m_colorTextCredits = asset.__23(255, 255, 255);
m_colorTextCredits2 = asset.__23(87, 230, 87);
m_colorTextBack = asset.__25();
m_randomizeDialogChoices = asset.__10();
m_typewriter = asset.__10();
m_vibration = asset.__10();
m_balloon = asset.__10();
float balloonImageWidth = (float)asset.__13();
float balloonImageHeight = (float)asset.__13();
m_balloonImageMargin = asset.__32();
m_balloonImageMargin01 = m_balloonImageMargin.__448(balloonImageWidth, balloonImageHeight);
m_balloonTextMargin = asset.__32();
m_choiceFrame = asset.__10();
m_choiceFrameSize = G.Clamp((float)asset.__13(), 1.0f, 10000.0f, 300.0f);
m_choiceFrameIconSize = G.Clamp((float)asset.__13(), 1.0f, 10000.0f, 256.0f);
m_choiceFrameSpace[0] = (float)asset.__12();
m_choiceFrameSpace[1] = (float)asset.__12();
m_choiceFrameMargin = asset.__32();
m_mergeAudioText = asset.__10();
m_audioLevel = asset.__18();
m_hasMenuMusic = asset.__10();
m_options = asset.__10();
m_subtitleOption = asset.__10();
m_layout = new Layout();
m_layout.__64(asset);
m_input.m_layout = m_layout;
m_lightBaked = asset.__10();
m_lightAmbient = asset.__12()/255;
m_lightBlur = G.Clamp(asset.__12()/100.0f);
m_lightLow = asset.__10();
m_bokehShapeWidth = (float)asset.__14();
m_bokehShapeHeight = (float)asset.__14();
if ( m_bokehShapeWidth==0.0f )
m_bokehShapeWidth = G.BOKEH_SHAPE;
if ( m_bokehShapeHeight==0.0f )
m_bokehShapeHeight = G.BOKEH_SHAPE;
m_savegameEnabled = asset.__10();
m_savegameServerUrl = asset.__18();
m_savegameServerGame = asset.__18();
m_autoSavePuzzles = asset.__10();
m_savegameServerEnabled = m_savegameEnabled && m_savegameServerUrl.Length>0;
if ( m_savegameServerEnabled==false )
GameObject.Find("Savegame").SetActive(false);
int chapterCount = asset.__12();
if ( chapterCount>0 )
{
m_chapters = new Dictionary<int, (int,int)>();
for ( int i=0 ; i<chapterCount ; i++ )
{
int index = asset.__12();
if ( index!=0 )
{
int size = asset.__15();
int offset = asset.__5();
asset.__4(size);
m_chapters.Add(index, (size,offset));
}
}
}
#if UNITY_WEBGL
string webStore = assetConfig.__18();
bool checkDomainAtStart = assetConfig.__10();
string[] domains = assetConfig.__18().Split(' ');
#if !UNITY_EDITOR
string domain = WebForm.JavascriptGetHostName().ToLower();
if ( domain.IndexOf("://")!=-1 )
{
domain = domain.Substring(domain.IndexOf("://")+3);
if ( domain.IndexOf('/')!=-1 )
domain = domain.Substring(0, domain.IndexOf('/'));
if ( domain.IndexOf(':')!=-1 )
domain = domain.Substring(0, domain.IndexOf(':'));
if ( domain.Substring(0, 4)=="www." )
domain = domain.Substring(4);
}
m_domainAllowed = __238(domain, domains);
#endif
#elif UNITY_ANDROID
System.IO.Stream fileObb = G.OpenFile(G.m_obbPath);
if ( fileObb!=null )
{
int count = assetConfig.__15();
Dictionary<string, string> medias = new Dictionary<string, string>();
System.IO.Stream fileMd5 = G.OpenFile(G.m_pathMd5);
if ( fileMd5!=null )
{
int mediaCount = (int)G.__81(fileMd5);
for ( int i=0 ; i<mediaCount ; i++ )
medias.Add(G.__83(fileMd5), G.__83(fileMd5));
fileMd5.Close();
}
fileMd5 = G.NewFile(G.m_pathMd5);
if ( fileMd5!=null )
{
fileMd5.WriteByte((byte)(uint)count);
fileMd5.WriteByte((byte)(((uint)count)>>8));
fileMd5.WriteByte((byte)(((uint)count)>>16));
fileMd5.WriteByte((byte)(((uint)count)>>24));
}
for ( int i=0 ; i<count ; i++ )
{
bool ok = true;
string name = assetConfig.__18();
string md5 = assetConfig.__18();
string cur;
string path = G.m_folderContent + name;
if ( medias.TryGetValue(name, out cur)==false || cur!=md5 || G.__90(path)==false )
{
ok = false;
int index;
if ( G.m_obbMap.TryGetValue(name, out index) )
{
System.IO.Stream tmp = G.NewFile(path);
if ( tmp!=null )
{
int size = G.m_obbSizes[index];
byte[] buf = new byte[size];
fileObb.Position = G.m_obbOffsets[index];
fileObb.Read(buf, 0, size);
tmp.Write(buf, 0, size);
tmp.Close();
ok = true;
}
}
}
if ( ok && fileMd5!=null )
{
G.__84(fileMd5, name);
G.__84(fileMd5, md5);
}
}
if ( fileMd5!=null )
fileMd5.Close();
fileObb.Close();
}
#endif
#if !UNITY_WEBGL
WebForm.m_instance.Disable();
#endif
m_initScript = asset.__26();
m_variables = new Dictionary<string, Variable>(StringComparer.OrdinalIgnoreCase);
m_scenario = new Scenario();
m_scenario.__64(asset);
__226(asset);
while ( true )
{
int iLang = __205(asset.__18());
if ( iLang==-1 )
break;
m_languages[iLang].m_cjkFont = new Police();
m_languages[iLang].m_cjkFont.__64(asset, m_fontScale);
}
for ( int i=0 ; i<m_menuSplashSprites.Length ; i++ )
{
m_menuSplashSprites[i] = asset.__27();
m_menuSplashDurations[i] = (float)asset.__12();
if ( m_menuSplashSprites[i] )
m_menuSplashSprites[i].__468(assetGraphics);
}
m_menuHelpSprite = asset.__27();
__236(asset, assetGraphics, ref m_uiDialogBack);
__236(asset, assetGraphics, ref m_menuBack);
__236(asset, assetGraphics, ref m_uiCursor.init);
__236(asset, assetGraphics, ref m_uiCursorObj.init);
__236(asset, assetGraphics, ref m_uiCursorDoor.init);
__236(asset, assetGraphics, ref m_uiCursorBusy.init);
__236(asset, assetGraphics, ref m_uiChoice);
__236(asset, assetGraphics, ref m_uiBalloon);
__236(asset, assetGraphics, ref m_uiWay);
__236(asset, assetGraphics, ref m_uiCheat);
__236(asset, assetGraphics, ref m_uiCheatDoor);
__236(asset, assetGraphics, ref m_uiCheatLabel);
__236(asset, assetGraphics, ref m_uiCheatObject);
__236(asset, assetGraphics, ref m_layout.m_spriteAutoSave);
__236(asset, assetGraphics, ref m_layout.m_spriteBag);
__236(asset, assetGraphics, ref m_layout.m_spriteDetach);
__236(asset, assetGraphics, ref m_layout.m_spriteMagnify);
__236(asset, assetGraphics, ref m_layout.m_spriteMenu);
__236(asset, assetGraphics, ref m_layout.m_spriteObject);
__236(asset, assetGraphics, ref m_layout.m_spriteObjectPrev);
__236(asset, assetGraphics, ref m_layout.m_spriteObjectNext);
__236(asset, assetGraphics, ref m_layout.m_spritePlayer);
__236(asset, assetGraphics, ref m_layout.m_spritePlayerPrev);
__236(asset, assetGraphics, ref m_layout.m_spritePlayerNext);
__236(asset, assetGraphics, ref m_layout.m_spriteShutup);
__236(asset, assetGraphics, ref m_layout.m_spriteUser1);
__236(asset, assetGraphics, ref m_layout.m_spriteUser2);
__236(asset, assetGraphics, ref m_layout.m_spriteUser3);
__236(asset, assetGraphics, ref m_layout.m_spriteUser4);
__236(asset, assetGraphics, ref m_layout.m_spriteUser5);
__236(asset, assetGraphics, ref m_layout.m_spriteUser6);
__236(asset, assetGraphics, ref m_layout.m_spriteUser7);
__236(asset, assetGraphics, ref m_layout.m_spriteUser8);
m_uiCursor.Reset();
m_uiCursorObj.Reset();
m_uiCursorDoor.Reset();
m_uiCursorBusy.Reset();
m_cheatUsed = m_uiCheat || m_uiCheatDoor || m_uiCheatLabel || m_uiCheatObject;
for ( int i=0 ; i<m_menuUserButtonSprites.Length ; i++ )
{
m_menuUserButtonSprites[i] = asset.__27();
m_menuUserButtonUrls[i] = asset.__18();
m_menuUserButtonVisibilities[i] = asset.__10();
if ( m_menuUserButtonSprites[i] )
m_menuUserButtonSprites[i].__468(assetGraphics);
}
bool preloadSounds = asset.__12()==1;
#if UNITY_WEBGL
preloadSounds = true;
#endif
int soundCount = asset.__13();
for ( int i=0 ; i<soundCount ; i++ )
{
Sound snd = asset.__30(Sound.TYPE.SFX);
m_sounds.Add(snd.m_name, snd);
if ( preloadSounds )
snd.m_samples = snd.LoadSFX();
}
m_menuEffect.Init(__282(asset.__18()));
m_effect.Init(__282(asset.__18()));
if ( G.m_font )
{
G.m_font.__65();
G.m_font = null;
}
__249(assetGraphics);
__217();
__218(assetGraphics);
m_menuDialog.Init();
asset.Close();
assetConfig.Close();
assetGraphics.Close();
m_gameValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
__243();
#if UNITY_WEBGL
if ( checkDomainAtStart && m_domainAllowed==false )
return false;
#endif
if ( m_configRun )
Application.runInBackground = true;
bool result = AgePlugin.OnAppInit();
if ( m_configLive==false )
{
if ( result )
{
if ( m_configDebug==false )
m_menuGame.Open(MenuGame.ID_LOGO, true);
else if ( m_menuSplashSprites[0] )
m_menuGame.Open(MenuGame.ID_SPLASH1, true);
else if ( m_menuSplashSprites[1] )
m_menuGame.Open(MenuGame.ID_SPLASH2, true);
else if ( m_customMenu.Length==0 )
m_menuGame.Open(MenuGame.ID_MAIN, true);
else
__257();
}
else
m_menuGame.Open(MenuGame.ID_START, true);
}
return true;
}
public void __226(Asset asset)
{
m_cinematics = new Cinematic[asset.__13()];
m_cinematicsBySID = new Dictionary<int, Cinematic>(m_cinematics.Length);
m_cinematicsByUID = new Dictionary<string, Cinematic>(m_cinematics.Length, StringComparer.OrdinalIgnoreCase);
m_dialogs = new Dialog[asset.__13()];
m_dialogsBySID = new Dictionary<int, Dialog>(m_dialogs.Length);
m_dialogsByUID = new Dictionary<string, Dialog>(m_dialogs.Length, StringComparer.OrdinalIgnoreCase);
m_effects = new Effect[asset.__13()];
m_effectsBySID = new Dictionary<int, Effect>(m_effects.Length);
m_effectsByUID = new Dictionary<string, Effect>(m_effects.Length, StringComparer.OrdinalIgnoreCase);
m_objects = new Obj[asset.__13()];
m_objectsBySID = new Dictionary<int, Obj>(m_objects.Length);
m_objectsByUID = new Dictionary<string, Obj>(m_objects.Length, StringComparer.OrdinalIgnoreCase);
m_players = new Player[asset.__13()+1];
m_playersBySID = new Dictionary<int, Player>(m_players.Length);
m_playersByUID = new Dictionary<string, Player>(m_players.Length, StringComparer.OrdinalIgnoreCase);
m_roles = new Role[asset.__13()];
m_rolesBySID = new Dictionary<int, Role>(m_roles.Length);
m_rolesByUID = new Dictionary<string, Role>(m_roles.Length, StringComparer.OrdinalIgnoreCase);
m_scenes = new Scene[asset.__13()];
m_scenesBySID = new Dictionary<int, Scene>(m_scenes.Length);
m_scenesByUID = new Dictionary<string, Scene>(m_scenes.Length, StringComparer.OrdinalIgnoreCase);
m_players[0] = new Player();
m_players[0].m_dump = true;
m_players[0].m_tags = new string[1];
m_players[0].m_tags[0] = "";
int iCinematic = 0;
int iDialog = 0;
int iEffect = 0;
int iObject = 0;
int iPlayer = 1;
int iRole = 0;
int iScene = 0;
string uid;
while ( (uid=asset.__18()).Length>0 )
{
#if UNITY_EDITOR
#endif
int sid = asset.__13();
switch ( uid[0] )
{
case 'C': case 'c':
m_cinematics[iCinematic++] = __233(asset, sid, uid);
m_cinematicsBySID.Add(sid, m_cinematics[iCinematic-1]);
m_cinematicsByUID.Add(uid, m_cinematics[iCinematic-1]);
break;
case 'D': case 'd':
m_dialogs[iDialog++] = __229(asset, sid, uid);
m_dialogsBySID.Add(sid, m_dialogs[iDialog-1]);
m_dialogsByUID.Add(uid, m_dialogs[iDialog-1]);
break;
case 'E': case 'e':
m_effects[iEffect++] = __234(asset, sid, uid);
m_effectsBySID.Add(sid, m_effects[iEffect-1]);
m_effectsByUID.Add(uid, m_effects[iEffect-1]);
break;
case 'O': case 'o':
m_objects[iObject++] = __228(asset, sid, uid);
m_objectsBySID.Add(sid, m_objects[iObject-1]);
m_objectsByUID.Add(uid, m_objects[iObject-1]);
break;
case 'P': case 'p':
m_players[iPlayer++] = __232(asset, sid, uid);
m_playersBySID.Add(sid, m_players[iPlayer-1]);
m_playersByUID.Add(uid, m_players[iPlayer-1]);
break;
case 'R': case 'r':
m_roles[iRole++] = __235(asset, sid, uid);
m_rolesBySID.Add(sid, m_roles[iRole-1]);
m_rolesByUID.Add(uid, m_roles[iRole-1]);
break;
case 'S': case 's':
m_scenes[iScene++] = __227(asset, sid, uid);
m_scenesBySID.Add(sid, m_scenes[iScene-1]);
m_scenesByUID.Add(uid, m_scenes[iScene-1]);
break;
}
}
Role[] roles = m_roles;
m_roles = new Role[roles.Length];
for ( int i=0 ; i<m_roles.Length ; i++ )
{
int index = asset.__13();
m_roles[i] = roles[index];
}
for ( int iObj=0 ; iObj<m_objects.Length ; iObj++ )
{
Obj obj = m_objects[iObj];
if ( obj.m_uidAvatar.Length>0 )
obj.m_avatar = __277(obj.m_uidAvatar);
if ( obj.m_uidClone.Length==0 )
continue;
obj.m_clone = __277(obj.m_uidClone);
if ( obj.m_clone==null )
continue;
obj.m_icons = obj.m_clone.m_icons;
obj.m_sprites = obj.m_clone.m_sprites;
obj.m_anims = obj.m_clone.m_anims;
obj.m_subObjs = obj.m_clone.m_subObjs;
obj.m_turns = obj.m_clone.m_turns;
obj.m_animation = obj.m_clone.m_animation;
obj.m_monochrome = obj.m_clone.m_monochrome;
obj.m_tint = obj.m_clone.m_tint;
}
if ( __274(m_customMenu)==null )
m_customMenu = "";
}
public Scene __227(Asset asset, int sid, string uid)
{
Scene scene = new Scene();
scene.m_sid = sid;
scene.m_uid = uid;
scene.m_tags = asset.ReadTags();
scene.m_role = __283(asset.__18());
scene.m_effect.Init(__282(asset.__18()));
int light = asset.__11();
scene.m_lightBaked = light==-1 ? m_lightBaked : light==1;
light = asset.__13();
scene.m_lightAmbient.Init(light==-1 ? -1.0f : light/255.0f);
light = asset.__11();
scene.m_lightBlur = light==-1 ? -1.0f : G.Clamp(light/100.0f);
light = asset.__11();
scene.m_lightLow = light==-1 ? m_lightLow : light==1;
scene.m_bokehShapeWidth = (float)asset.__14();
scene.m_bokehShapeHeight = (float)asset.__14();
scene.m_bokehSize.Init(G.Clamp(asset.__12()/100.0f));
scene.m_bokehMaxZoom.Init(G.Clamp(asset.__14()/100.0f, 1.0f, 4.0f));
if ( scene.m_bokehShapeWidth==0.0f )
scene.m_bokehShapeWidth = m_bokehShapeWidth;
if ( scene.m_bokehShapeHeight==0.0f )
scene.m_bokehShapeHeight = m_bokehShapeHeight;
scene.m_width = asset.__13();
scene.m_height = asset.__13();
scene.m_backLayer = asset.__28();
if ( scene.m_backLayer )
{
scene.m_backLayer.m_depth = asset.__27();
scene.m_backLayer.m_isBack = true;
scene.m_backLayer.m_mask = asset.__29();
scene.m_backLayer.m_blend = asset.__10() ? BLEND.ADDITION : BLEND.DEFAULT;
scene.m_backLayer.m_minOpacity = asset.__12()/100.0f;
scene.m_backLayer.m_maxOpacity = asset.__12()/100.0f;
scene.m_backLayer.m_speedOpacity = asset.__13()/100.0f;
}
for ( int i=0 ; i<G.FAR_COUNT ; i++ )
{
scene.m_farLayers[i] = asset.__28();
if ( scene.m_farLayers[i] )
{
scene.m_farLayers[i].m_depth = asset.__27();
scene.m_farLayers[i].m_mask = asset.__29();
scene.m_farLayers[i].m_blend = asset.__10() ? BLEND.ADDITION : BLEND.DEFAULT;
scene.m_farLayers[i].m_visible = asset.__10();
scene.m_farLayers[i].m_tileX = asset.__10();
scene.m_farLayers[i].m_tileY = asset.__10();
scene.m_farLayers[i].m_offsetX = asset.__13();
scene.m_farLayers[i].m_offsetY = asset.__13();
scene.m_farLayers[i].m_speedX = asset.__14()/100.0f;
scene.m_farLayers[i].m_speedY = asset.__14()/100.0f;
scene.m_farLayers[i].m_parallax = asset.__14()/100.0f;
scene.m_farLayers[i].m_minOpacity = asset.__12()/100.0f;
scene.m_farLayers[i].m_maxOpacity = asset.__12()/100.0f;
scene.m_farLayers[i].m_speedOpacity = asset.__13()/100.0f;
}
}
for ( int i=0 ; i<G.MASK_COUNT ; i++ )
{
scene.m_maskLayers[i] = asset.__28();
if ( scene.m_maskLayers[i] )
{
scene.m_maskLayers[i].m_depth = asset.__27();
scene.m_maskLayers[i].m_mask = asset.__29();
scene.m_maskLayers[i].m_blend = asset.__10() ? BLEND.ADDITION : BLEND.DEFAULT;
scene.m_maskLayers[i].m_visible = asset.__10();
scene.m_maskLayers[i].m_tileX = asset.__10();
scene.m_maskLayers[i].m_tileY = asset.__10();
scene.m_maskLayers[i].m_offsetX = asset.__13();
scene.m_maskLayers[i].m_offsetY = asset.__13();
scene.m_maskLayers[i].m_speedX = asset.__13()/100.0f;
scene.m_maskLayers[i].m_speedY = asset.__13()/100.0f;
scene.m_maskLayers[i].m_parallax = asset.__14()/100.0f;
scene.m_maskLayers[i].m_minOpacity = asset.__12()/100.0f;
scene.m_maskLayers[i].m_maxOpacity = asset.__12()/100.0f;
scene.m_maskLayers[i].m_speedOpacity = asset.__13()/100.0f;
}
}
scene.m_tempPlacementCounts = new int[(int)PLACEMENT.COUNT];
scene.m_sortedEntities = new List<SceneEntity>[(int)PLACEMENT.COUNT];
scene.m_sortedLabels = new List<SceneLabel>[(int)PLACEMENT.COUNT];
scene.m_voiceOverObj = new SceneObj();
scene.m_voiceOverObj.m_scene = scene;
scene.m_voiceOverObj.m_obj = null;
scene.m_objects = new SceneObj[asset.__13()];
scene.m_objectsByUID = new Dictionary<string, SceneObj>(scene.m_objects.Length, StringComparer.OrdinalIgnoreCase);
scene.m_objectsByRef = new Dictionary<Obj, SceneObj>(scene.m_objects.Length);
scene.m_objectsByID = new Dictionary<int, SceneObj>(scene.m_objects.Length);
scene.m_lightCount = 0;
for ( int iObj=0 ; iObj<scene.m_objects.Length ; iObj++ )
{
SceneObj sceneObj = new SceneObj();
scene.m_objects[iObj] = sceneObj;
sceneObj.m_scene = scene;
sceneObj.m_obj = __277(asset.__18());
sceneObj.m_sid = asset.__13();
sceneObj.m_tags = asset.ReadTags();
sceneObj.m_defaultStopAnim.Init("STOP");
sceneObj.m_defaultWalkAnim.Init("WALK");
sceneObj.m_defaultTalkAnim.Init("TALK");
sceneObj.m_visibleInThisScene = asset.__11();
sceneObj.m_sticker.Init("");
sceneObj.m_visible.Init(false);
sceneObj.m_local.Init(new Vec2((float)asset.__13(), (float)asset.__13()));
sceneObj.m_angle.Init(asset.__14());
sceneObj.m_speedX = (float)asset.__13();
if ( sceneObj.m_speedX==0.0f )
sceneObj.m_speedX = sceneObj.m_obj.m_speedX.cur;
sceneObj.m_speedY = (float)asset.__13();
if ( sceneObj.m_speedY==0.0f )
sceneObj.m_speedY = sceneObj.m_obj.m_speedY.cur;
sceneObj.m_elevator.Init((float)asset.__13());
sceneObj.m_elevatorScaling = asset.__10();
sceneObj.m_elevatorRotation = asset.__10();
sceneObj.m_z.Init(0.0f);
sceneObj.m_autoZ.Init(true);
sceneObj.m_manualScale.Init(0.0f);
sceneObj.m_manualZoom.Init(0.0f);
sceneObj.m_parallax = asset.__14()/100.0f;
sceneObj.m_blend = (BLEND)asset.__12();
sceneObj.m_hud = asset.__10();
sceneObj.m_placement.Init((PLACEMENT)asset.__12());
sceneObj.m_opacity.Init(1.0f);
switch ( asset.__12() )
{
case 0: sceneObj.m_drag = DRAG.NONE; break;
case 83: sceneObj.m_drag = DRAG.SOURCE; break;
case 84: sceneObj.m_drag = DRAG.TARGET; break;
case 66: sceneObj.m_drag = DRAG.BOTH; break;
}
sceneObj.m_cheat.Init(asset.__10());
sceneObj.m_door = asset.__10();
sceneObj.m_skip = asset.__10();
sceneObj.m_light = asset.__10();
if ( sceneObj.m_light )
{
scene.m_lightCount++;
sceneObj.m_lightVisible.Init(true);
sceneObj.m_lightAmbient.Init(asset.__12()/255.0f);
sceneObj.m_lightDiffuse.Init(asset.__25());
sceneObj.m_lightAngle.Init(asset.__14() * G.DEG_TO_RAD);
sceneObj.m_lightDir.Init(G.__142(asset.__14()));
sceneObj.m_lightDist.Init((float)asset.__13());
sceneObj.m_lightAttn.Init(asset.__14()/100.0f);
}
scene.m_objectsByUID.Add(sceneObj.m_obj.m_uid, sceneObj);
scene.m_objectsByRef.Add(sceneObj.m_obj, sceneObj);
scene.m_objectsByID.Add(sceneObj.m_sid, sceneObj);
sceneObj.m_anim.passedFrames = new bool[sceneObj.m_obj.m_maxAnimFrameCount];
int maxFrameCount = asset.__12();
if ( maxFrameCount>0 )
sceneObj.m_anim.randomFrames = new int[maxFrameCount];
if ( asset.__10() )
sceneObj.m_anim.profileFrames = new int[G.PROFILE_FRAME_COUNT];
bool hasPaths = asset.__10();
if ( hasPaths )
{
sceneObj.m_paths = new SpotPath[G.PATH_GRID_COUNT];
for ( int iPath=0 ; iPath<G.PATH_GRID_COUNT ; iPath++ )
{
int spotCount = asset.__13();
if ( spotCount==0 )
sceneObj.m_paths[iPath] = null;
else
{
SpotPath path = new SpotPath();
sceneObj.m_paths[iPath] = path;
path.m_spots = new Spot[spotCount];
path.m_spline = new Spline(asset.__12());
path.m_initLoop = asset.__12();
if ( path.m_initLoop==255 )
path.m_initLoop = -1;
for ( int iSpot=0 ; iSpot<path.m_spots.Length ; iSpot++ )
{
path.m_spots[iSpot] = new Spot();
path.m_spots[iSpot].m_index = iSpot;
path.m_spots[iSpot].m_name = asset.__18();
path.m_spots[iSpot].m_x = (float)asset.__13();
path.m_spots[iSpot].m_y = (float)asset.__13();
path.m_spots[iSpot].m_speed = (float)asset.__13();
path.m_spots[iSpot].m_pause = (float)asset.__13();
if ( path.m_spots[iSpot].m_pause!=-1.0f )
path.m_spots[iSpot].m_pause /= 1000.0f;
path.m_spots[iSpot].m_zoom = asset.__13()/100.0f;
}
path.Stop();
}
}
}
bool hasGrids = asset.__10();
if ( hasGrids )
{
sceneObj.m_grids = new Grid[G.PATH_GRID_COUNT];
for ( int iGrid=0 ; iGrid<G.PATH_GRID_COUNT ; iGrid++ )
{
if ( asset.__10()==false )
sceneObj.m_grids[iGrid] = null;
else
{
int colMin = asset.__13();
int colMax = asset.__13();
int rowMin = asset.__13();
int rowMax = asset.__13();
int cellCount = asset.__15();
sceneObj.m_grids[iGrid] = new Grid();
Grid grid = sceneObj.m_grids[iGrid];
grid.m_cells = new SceneCell[cellCount];
grid.m_mapCellByIndex = new Dictionary<uint, SceneCell>();
grid.m_mapCellByName = new Dictionary<string, SceneCell>(StringComparer.OrdinalIgnoreCase);
grid.m_mapCellByEnterFromLink = new Dictionary<string, SceneCell>();
grid.m_mapCellByEnterToLink = new Dictionary<string, SceneCell>();
grid.m_mapCellBySelectLink = new Dictionary<string, SceneCell>();
grid.m_mapCellByLabelLink = new Dictionary<string, SceneCell>();
grid.m_mapCellByUseLink = new Dictionary<string, SceneCell>();
grid.m_mapCellByUseLabelLink = new Dictionary<string, SceneCell>();
grid.m_mapCellByDetachLink = new Dictionary<string, SceneCell>();
grid.m_mapEventCellByIndex = new Dictionary<uint, SceneCell>();
grid.m_router = new Router();
grid.m_router.Init(colMin, colMax, rowMin, rowMax);
if ( m_hasShowPathPlayer )
{
grid.m_routerHud = new Router();
grid.m_routerHud.Init(colMin, colMax, rowMin, rowMax);
}
List<uint> bridgeBackSrcs = new List<uint>();
List<uint> bridgeBackTrgs = new List<uint>();
for ( int iCell=0 ; iCell<grid.m_cells.Length ; iCell++ )
{
SceneCell cell = new SceneCell();
grid.m_cells[iCell] = cell;
int header = asset.__14();
if ( G.__101(header, 0x0001) )
{
cell.m_col = asset.__12();
cell.m_row = asset.__12();
}
else
{
cell.m_col = asset.__13();
cell.m_row = asset.__13();
}
uint index = G.__98(cell.m_col, cell.m_row);
int indexRouter = grid.m_router.__391(cell.m_col, cell.m_row);
if ( grid.m_mapCellByIndex.ContainsKey(index)==false )
grid.m_mapCellByIndex.Add(index, cell);
bool walkable = true;
int bridgeCount = 0;
if ( header>0x0001 )
{
cell.m_data = new SceneCellData();
cell.m_data.m_event = G.__101(header, 0x0002);
if ( cell.m_data.m_event )
{
if ( grid.m_mapEventCellByIndex.ContainsKey(index)==false )
grid.m_mapEventCellByIndex.Add(index, cell);
}
cell.m_data.m_magnet = G.__101(header, 0x0004);
if ( G.__101(header, 0x0008) )
walkable = false;
cell.m_data.m_walkable = walkable;
if ( G.__101(header, 0x0010) )
cell.m_data.m_name = asset.__18();
if ( G.__101(header, 0x0020) )
cell.m_data.m_flag = (sbyte)asset.__12();
else
cell.m_data.m_flag = -1;
if ( G.__101(header, 0x0040) )
{
cell.m_data.m_speedFactorX = asset.__13()/100.0f;
cell.m_data.m_speedFactorY = asset.__13()/100.0f;
cell.m_data.m_scaleFactor = asset.__13()/100.0f;
}
else
{
cell.m_data.m_speedFactorX = 1.0f;
cell.m_data.m_speedFactorY = 1.0f;
cell.m_data.m_scaleFactor = 1.0f;
}
if ( cell.m_data.m_name!=null && cell.m_data.m_name.Length>0 && grid.m_mapCellByName.ContainsKey(cell.m_data.m_name)==false )
grid.m_mapCellByName.Add(cell.m_data.m_name, cell);
if ( G.__101(header, 0x0080) )
{
bridgeCount = asset.__12();
if ( bridgeCount>0 )
{
cell.m_data.m_bridges = new List<uint>();
for ( int i=0 ; i<bridgeCount ; i++ )
{
uint bridge = asset.__16();
bool isDirectional = asset.__10();
cell.m_data.m_bridges.Add(bridge);
if ( isDirectional==false )
{
bridgeBackSrcs.Add(bridge);
bridgeBackTrgs.Add(index);
}
}
}
}
if ( G.__101(header, 0x0100) )
{
cell.m_data.m_walkAnim = asset.__18();
string walkAnimDirs = asset.__18();
if ( walkAnimDirs.Length==8 )
{
cell.m_data.m_walkAnimDirs = new bool[AnimDir.COUNT];
cell.m_data.m_walkAnimDirs[AnimDir.LEFT] = walkAnimDirs[AnimDir.LEFT]=='1';
cell.m_data.m_walkAnimDirs[AnimDir.RIGHT] = walkAnimDirs[AnimDir.RIGHT]=='1';
cell.m_data.m_walkAnimDirs[AnimDir.FRONT] = walkAnimDirs[AnimDir.FRONT]=='1';
cell.m_data.m_walkAnimDirs[AnimDir.BACK] = walkAnimDirs[AnimDir.BACK]=='1';
cell.m_data.m_walkAnimDirs[AnimDir.FL] = walkAnimDirs[AnimDir.FL]=='1';
cell.m_data.m_walkAnimDirs[AnimDir.FR] = walkAnimDirs[AnimDir.FR]=='1';
cell.m_data.m_walkAnimDirs[AnimDir.BL] = walkAnimDirs[AnimDir.BL]=='1';
cell.m_data.m_walkAnimDirs[AnimDir.BR] = walkAnimDirs[AnimDir.BR]=='1';
}
}
}
if ( bridgeCount==0 )
{
if ( grid.m_router.__512(indexRouter) )
grid.m_router.m_mapWalkable[indexRouter] = walkable;
if ( grid.m_routerHud && grid.m_routerHud.__512(indexRouter) )
grid.m_routerHud.m_mapWalkable[indexRouter] = walkable;
}
else
{
if ( grid.m_router.__512(indexRouter) )
{
grid.m_router.m_mapWalkable[indexRouter] = walkable;
if ( cell.m_data && cell.m_data.m_bridges!=null )
{
Router.Bridge bridge = new Router.Bridge();
grid.m_router.m_mapBridge[indexRouter] = bridge;
for ( int i=0 ; i<cell.m_data.m_bridges.Count ; i++ )
bridge.m_cells.Add(cell.m_data.m_bridges[i]);
}
}
if ( grid.m_routerHud && grid.m_routerHud.__512(indexRouter) )
{
grid.m_routerHud.m_mapWalkable[indexRouter] = walkable;
if ( cell.m_data && cell.m_data.m_bridges!=null )
{
Router.Bridge bridge = new Router.Bridge();
grid.m_routerHud.m_mapBridge[indexRouter] = bridge;
for ( int i=0 ; i<cell.m_data.m_bridges.Count ; i++ )
bridge.m_cells.Add(cell.m_data.m_bridges[i]);
}
}
}
header = asset.__12();
if ( header!=0 )
{
cell.m_data.m_links = new SceneCellLink[(int)LINK.COUNT];
if ( G.__101(header, 0x01) )
{
SceneCellLink link = new SceneCellLink();
cell.m_data.m_links[(int)LINK.ENTER] = link;
bool from = asset.__10();
int prevSceneCount = asset.__12();
for ( int i=0 ; i<prevSceneCount ; i++ )
{
string prevScene = asset.__18();
if ( from && grid.m_mapCellByEnterFromLink.ContainsKey(prevScene)==false )
grid.m_mapCellByEnterFromLink.Add(prevScene, cell);
if ( from==false && grid.m_mapCellByEnterToLink.ContainsKey(prevScene)==false )
grid.m_mapCellByEnterToLink.Add(prevScene, cell);
}
if ( from==false )
{
link.m_anim = asset.__18();
link.m_dir = (sbyte)asset.__11();
}
}
if ( G.__101(header, 0x02) )
{
SceneCellLink link = new SceneCellLink();
cell.m_data.m_links[(int)LINK.SELECT] = link;
string name = asset.__18();
link.m_dist = asset.__20();
if ( link.m_dist==0.0f )
{
link.m_anim = asset.__18();
link.m_dir = (sbyte)asset.__11();
}
if ( grid.m_mapCellBySelectLink.ContainsKey(name)==false )
grid.m_mapCellBySelectLink.Add(name, cell);
}
if ( G.__101(header, 0x04) )
{
SceneCellLink link = new SceneCellLink();
cell.m_data.m_links[(int)LINK.LABEL] = link;
string name = asset.__18();
link.m_dist = asset.__20();
if ( link.m_dist==0.0f )
{
link.m_anim = asset.__18();
link.m_dir = (sbyte)asset.__11();
}
if ( grid.m_mapCellByLabelLink.ContainsKey(name)==false )
grid.m_mapCellByLabelLink.Add(name, cell);
}
if ( G.__101(header, 0x08) )
{
SceneCellLink link = new SceneCellLink();
cell.m_data.m_links[(int)LINK.USE] = link;
string name = asset.__18();
string name2 = asset.__18();
link.m_dist = asset.__20();
if ( link.m_dist==0.0f )
{
link.m_anim = asset.__18();
link.m_dir = (sbyte)asset.__11();
}
if ( grid.m_mapCellByUseLink.ContainsKey(name+"+"+name2)==false )
grid.m_mapCellByUseLink.Add(name+"+"+name2, cell);
}
if ( G.__101(header, 0x10) )
{
SceneCellLink link = new SceneCellLink();
cell.m_data.m_links[(int)LINK.USELABEL] = link;
string name = asset.__18();
string name2 = asset.__18();
link.m_dist = asset.__20();
if ( link.m_dist==0.0f )
{
link.m_anim = asset.__18();
link.m_dir = (sbyte)asset.__11();
}
if ( grid.m_mapCellByUseLabelLink.ContainsKey(name+"+"+name2)==false )
grid.m_mapCellByUseLabelLink.Add(name+"+"+name2, cell);
}
if ( G.__101(header, 0x20) )
{
SceneCellLink link = new SceneCellLink();
cell.m_data.m_links[(int)LINK.DETACH] = link;
string name = asset.__18();
link.m_dist = asset.__20();
if ( link.m_dist==0.0f )
{
link.m_anim = asset.__18();
link.m_dir = (sbyte)asset.__11();
}
if ( grid.m_mapCellByDetachLink.ContainsKey(name)==false )
grid.m_mapCellByDetachLink.Add(name, cell);
}
}
}
for ( int i=0 ; i<bridgeBackSrcs.Count ; i++ )
{
int indexRouter = grid.m_router.__391(G.__99(bridgeBackSrcs[i]), G.__100(bridgeBackSrcs[i]));
if ( grid.m_router.m_mapBridge[indexRouter]==null )
grid.m_router.m_mapBridge[indexRouter] = new Router.Bridge();
grid.m_router.m_mapBridge[indexRouter].m_cells.Add(bridgeBackTrgs[i]);
if ( grid.m_routerHud )
{
if ( grid.m_routerHud.m_mapBridge[indexRouter]==null )
grid.m_routerHud.m_mapBridge[indexRouter] = new Router.Bridge();
grid.m_routerHud.m_mapBridge[indexRouter].m_cells.Add(bridgeBackTrgs[i]);
}
}
}
}
}
sceneObj.m_depthScaleCount = asset.__12();
if ( sceneObj.m_depthScaleCount>0 )
{
sceneObj.m_depthScaleYs = new float[sceneObj.m_depthScaleCount];
sceneObj.m_depthScaleValues = new float[sceneObj.m_depthScaleCount];
for ( int i=0 ; i<sceneObj.m_depthScaleCount ; i++ )
{
sceneObj.m_depthScaleYs[i] = (float)asset.__13();
sceneObj.m_depthScaleValues[i] = asset.__13()/100.0f;
}
}
sceneObj.m_depthZoomCount = asset.__12();
if ( sceneObj.m_depthZoomCount>0 )
{
sceneObj.m_depthZoomYs = new float[sceneObj.m_depthZoomCount];
sceneObj.m_depthZoomValues = new float[sceneObj.m_depthZoomCount];
for ( int i=0 ; i<sceneObj.m_depthZoomCount ; i++ )
{
sceneObj.m_depthZoomYs[i] = (float)asset.__13();
sceneObj.m_depthZoomValues[i] = asset.__13()/100.0f;
}
}
}
if ( scene.m_lightCount>0 )
{
int count = (int)PLACEMENT.COUNT;
scene.m_lights = new List<SceneObj>[count];
scene.m_lightAmbients = new bool[count];
scene.m_lightDiffuses = new bool[count];
scene.m_lightChanges = new bool[count];
if ( scene.m_lightBaked )
scene.m_lightRTs = new RenderTexture[count];
}
scene.m_labels = new SceneLabel[asset.__13()];
scene.m_labelsByName = new Dictionary<string, SceneLabel>(scene.m_labels.Length);
scene.m_labelsByID = new Dictionary<int, SceneLabel>(scene.m_labels.Length);
for ( int i=0 ; i<scene.m_labels.Length ; i++ )
{
SceneLabel item = new SceneLabel();
scene.m_labels[i] = item;
item.m_scene = scene;
item.m_name = asset.__18();
item.m_sid = asset.__13();
item.m_tags = asset.ReadTags();
item.m_local.Init(new Vec2((float)asset.__13(), (float)asset.__13()));
item.m_width = (float)asset.__13();
item.m_height = (float)asset.__13();
item.m_title = asset.__21();
item.m_text = asset.__21();
item.m_usertext.Init("");
item.m_hud = asset.__10();
item.m_placement.Init((PLACEMENT)asset.__12());
item.m_size = (float)asset.__12();
item.m_color = asset.__23(m_colorText);
item.m_align = asset.__12();
item.m_visible.Init(asset.__10());
item.m_enabled.Init(asset.__10());
switch ( asset.__12() )
{
case 0: item.m_drag = DRAG.NONE; break;
case 83: item.m_drag = DRAG.SOURCE; break;
case 84: item.m_drag = DRAG.TARGET; break;
case 66: item.m_drag = DRAG.BOTH; break;
}
item.m_cheat.Init(asset.__10());
item.m_door = asset.__10();
item.m_skip = asset.__10();
if ( scene.m_labelsByName.ContainsKey(item.m_name)==false )
scene.m_labelsByName.Add(item.m_name, item);
scene.m_labelsByID.Add(item.m_sid, item);
int iPlacement = (int)item.m_placement.cur;
if ( scene.m_sortedLabels[iPlacement]==null )
scene.m_sortedLabels[iPlacement] = new List<SceneLabel>();
scene.m_sortedLabels[iPlacement].Add(item);
}
scene.m_shots = new SceneShot[asset.__13()];
scene.m_shotsByName = new Dictionary<string, SceneShot>(scene.m_shots.Length);
scene.m_shotsByID = new Dictionary<int, SceneShot>(scene.m_shots.Length);
for ( int i=0 ; i<scene.m_shots.Length ; i++ )
{
SceneShot item = new SceneShot();
scene.m_shots[i] = item;
item.m_scene = scene;
item.m_name = asset.__18();
item.m_sid = asset.__13();
item.m_x = (float)asset.__13();
item.m_y = (float)asset.__13();
item.m_width = (float)asset.__13();
item.m_scale = item.m_width==0.0f ? 1.0f : G.m_rcGame.width/item.m_width;
if ( scene.m_shotsByName.ContainsKey(item.m_name)==false )
scene.m_shotsByName.Add(item.m_name, item);
scene.m_shotsByID.Add(item.m_sid, item);
}
scene.m_walls = new SceneWall[asset.__13()];
scene.m_wallsByID = new Dictionary<int, SceneWall>(scene.m_walls.Length);
for ( int i=0 ; i<scene.m_walls.Length ; i++ )
{
SceneWall item = new SceneWall();
scene.m_walls[i] = item;
item.m_scene = scene;
item.m_sid = asset.__13();
item.m_ax = (float)asset.__13();
item.m_ay = (float)asset.__13();
item.m_bx = (float)asset.__13();
item.m_by = (float)asset.__13();
scene.m_wallsByID.Add(item.m_sid, item);
}
scene.m_bokehs = new SceneBokeh[asset.__13()];
scene.m_bokehsByName = new Dictionary<string, SceneBokeh>(scene.m_bokehs.Length);
scene.m_bokehsByID = new Dictionary<int, SceneBokeh>(scene.m_bokehs.Length);
for ( int i=0 ; i<scene.m_bokehs.Length ; i++ )
{
SceneBokeh item = new SceneBokeh();
scene.m_bokehs[i] = item;
item.m_scene = scene;
scene.m_bokehs[i].m_name = asset.__18();
item.m_sid = asset.__13();
item.m_local.Init(new Vec2((float)asset.__13(), (float)asset.__13()));
item.m_hud = asset.__10();
item.m_placement.Init((PLACEMENT)asset.__12());
item.m_visible.Init(asset.__10());
if ( scene.m_bokehsByName.ContainsKey(item.m_name)==false )
scene.m_bokehsByName.Add(item.m_name, item);
scene.m_bokehsByID.Add(item.m_sid, item);
}
scene.m_stillSprites = new Sprite[asset.__13()];
for ( int i=0 ; i<scene.m_stillSprites.Length ; i++ )
scene.m_stillSprites[i] = asset.__27();
scene.m_stills = new SceneStill[asset.__13()];
scene.m_stillsByName = new Dictionary<string, SceneStill>(scene.m_stills.Length);
scene.m_stillsByID = new Dictionary<int, SceneStill>(scene.m_stills.Length);
for ( int i=0 ; i<scene.m_stills.Length ; i++ )
{
SceneStill item = new SceneStill();
scene.m_stills[i] = item;
item.m_scene = scene;
item.m_name = asset.__18();
item.m_sid = asset.__13();
item.m_tags = asset.ReadTags();
item.m_placement.Init(PLACEMENT.BACK);
item.m_visible.Init(asset.__10());
item.m_rcTrg.x = (float)asset.__13();
item.m_rcTrg.y = (float)asset.__13();
item.m_rcTrg.width = (float)asset.__13();
item.m_rcTrg.height = (float)asset.__13();
item.m_z = item.m_rcTrg.y + item.m_rcTrg.height + (float)asset.__13();
item.m_rcSrc.x = (float)asset.__13();
item.m_rcSrc.y = (float)asset.__13();
item.m_rcSrc.width = (float)asset.__13();
item.m_rcSrc.height = (float)asset.__13();
item.m_rotated = asset.__10();
int pack = asset.__14();
item.m_sprite = scene.m_stillSprites[pack];
if ( scene.m_stillsByName.ContainsKey(item.m_name)==false )
scene.m_stillsByName.Add(item.m_name, item);
scene.m_stillsByID.Add(item.m_sid, item);
}
for ( int iObj=0 ; iObj<scene.m_objects.Length ; iObj++ )
{
string parent = asset.__18();
scene.m_objects[iObj].m_parentName.Init(parent);
}
for ( int iLabel=0 ; iLabel<scene.m_labels.Length ; iLabel++ )
{
string parent = asset.__18();
scene.m_labels[iLabel].m_parentName.Init(parent);
}
for ( int iBokeh=0 ; iBokeh<scene.m_bokehs.Length ; iBokeh++ )
{
string parent = asset.__18();
scene.m_bokehs[iBokeh].m_parentName.Init(parent);
}
scene.m_timelines = new Timeline[asset.__12()];
scene.m_timelinesByName = new Dictionary<string, Timeline>(scene.m_timelines.Length);
scene.m_timelinesByID = new Dictionary<int, Timeline>(scene.m_timelines.Length);
for ( int i=0 ; i<scene.m_timelines.Length ; i++ )
{
Timeline timeline = new Timeline();
scene.m_timelines[i] = timeline;
timeline.m_scene = scene;
timeline.__44(asset);
if ( scene.m_timelinesByName.ContainsKey(timeline.m_name)==false )
scene.m_timelinesByName.Add(timeline.m_name, timeline);
scene.m_timelinesByID.Add(timeline.m_sid, timeline);
}
return scene;
}
public Obj __228(Asset asset, int sid, string uid)
{
Obj obj = new Obj();
obj.m_sid = sid;
obj.m_uid = uid;
obj.m_tags = asset.ReadTags();
obj.m_uidClone = asset.__18();
obj.m_anchorX.Init((float)asset.__13());
obj.m_anchorY.Init((float)asset.__13());
obj.m_imgWidth = (float)asset.__13();
obj.m_imgHeight = (float)asset.__13();
obj.m_speech = asset.__23(m_colorText);
obj.m_uidAvatar = asset.__18();
obj.m_avatarImage = asset.__31();
obj.m_avatarText = asset.__31();
obj.m_avatarTextAlign = asset.__12();
obj.m_avatarOpacity = G.Clamp(asset.__12(), 0, 100)/100.0f;
obj.m_speechMovie = asset.__10();
obj.m_title = asset.__21();
obj.m_enabled.Init(asset.__10());
obj.m_subEnabled.Init(asset.__10());
obj.m_bboxDetection = asset.__10();
obj.m_tolerance = asset.__12();
obj.m_speedX.Init((float)asset.__13());
obj.m_speedY.Init((float)asset.__13());
if ( obj.m_uidClone.Length>0 )
return obj;
obj.m_animation = (ANIMATION)asset.__12();
if ( obj.m_animation!=ANIMATION.NONE )
{
obj.m_animationSpeed = asset.__13()/100.0f * 0.1f;
obj.m_animationScale = asset.__13()/100.0f;
}
obj.m_tint.Init(new Color(asset.__12()/255.0f, asset.__12()/255.0f, asset.__12()/255.0f, asset.__12()/255.0f));
obj.m_monochrome = asset.__10();
bool turning = asset.__10();
if ( turning )
obj.m_turns = new List<Turn>();
obj.m_icons = new Sprite[G.ICON_COUNT];
for ( int i=0 ; i<obj.m_icons.Length ; i++ )
obj.m_icons[i] = asset.__27();
obj.m_subObjs = new SubObj[asset.__12()];
for ( int i=0 ; i<obj.m_subObjs.Length ; i++ )
{
obj.m_subObjs[i] = new SubObj();
obj.m_subObjs[i].m_obj = obj;
obj.m_subObjs[i].m_name = asset.__18();
obj.m_subObjs[i].m_title = asset.__21();
}
List<Frame> frames = new List<Frame>();
obj.m_anims = new Anim[asset.__13()];
for ( int iAnim=0 ; iAnim<obj.m_anims.Length ; iAnim++ )
{
Anim anim = new Anim();
obj.m_anims[iAnim] = anim;
anim.m_name = asset.__18();
anim.m_speed.Init(asset.__11());
anim.m_actionFrame = asset.__11();
anim.m_loopCount = asset.__13();
anim.m_loopRangeMin = asset.__11();
anim.m_loopRangeMax = asset.__11();
anim.m_loopRangeCount = asset.__13();
anim.m_random = (RANDOM)asset.__12();
anim.m_profile = (PROFILE)asset.__12();
if ( turning )
{
int turnDir = asset.__11();
if ( turnDir!=-1 )
{
Turn turn = new Turn();
turn.m_dir = turnDir;
turn.m_stop = asset.__18();
turn.m_anim = anim;
obj.m_turns.Add(turn);
}
}
anim.m_maxFrameCount = 0;
anim.m_dirs = new AnimDir[AnimDir.COUNT];
for ( int iDir=0 ; iDir<AnimDir.COUNT ; iDir++ )
{
int iDir2 = AnimDir.RIGHT;
switch ( iDir )
{
case AnimDir.LEFT: iDir2 = AnimDir.RIGHT; break;
case AnimDir.RIGHT: iDir2 = AnimDir.LEFT; break;
case AnimDir.FRONT: iDir2 = AnimDir.FRONT; break;
case AnimDir.BACK: iDir2 = AnimDir.BACK; break;
case AnimDir.FL: iDir2 = AnimDir.FR; break;
case AnimDir.FR: iDir2 = AnimDir.FL; break;
case AnimDir.BL: iDir2 = AnimDir.BR; break;
case AnimDir.BR: iDir2 = AnimDir.BL; break;
}
AnimDir dir = new AnimDir();
anim.m_dirs[iDir2] = dir;
dir.m_id = iDir2;
int frameCount = asset.__12();
if ( frameCount>anim.m_maxFrameCount )
anim.m_maxFrameCount = frameCount;
dir.m_frames = new Frame[frameCount];
for ( int iFrame=0 ; iFrame<frameCount ; iFrame++ )
{
Frame frame = new Frame();
dir.m_frames[iFrame] = frame;
frame.m_index = iFrame;
frame.m_index2 = -1;
frame.m_rcTrim.x = (float)asset.__13();
frame.m_rcTrim.y = (float)asset.__13();
frame.m_rcTrim.width = (float)asset.__13();
frame.m_rcTrim.height = (float)asset.__13();
int cloneFrame = -1;
int cloneDir = -1;
int cloneAnim = asset.__12();
if ( cloneAnim!=255 )
{
cloneDir = asset.__12();
cloneFrame = asset.__13();
}
if ( cloneAnim==255 )
{
frame.m_maskCloned = false;
frame.m_mask = asset.__29();
}
else
{
frame.m_maskCloned = true;
frame.m_mask = obj.m_anims[cloneAnim].m_dirs[cloneDir].m_frames[cloneFrame].m_mask;
asset.__15();
}
frame.m_subObjRects = new Rect[obj.m_subObjs.Length];
for ( int i=0 ; i<obj.m_subObjs.Length ; i++ )
{
frame.m_subObjRects[i] = new Rect();
frame.m_subObjRects[i].x = (float)asset.__13();
frame.m_subObjRects[i].y = (float)asset.__13();
frame.m_subObjRects[i].width = (float)asset.__13();
frame.m_subObjRects[i].height = (float)asset.__13();
}
frames.Add(frame);
}
}
anim.m_defaultDir = anim.m_dirs[asset.__12()];
if ( anim.m_maxFrameCount>obj.m_maxAnimFrameCount )
obj.m_maxAnimFrameCount = anim.m_maxFrameCount;
}
obj.m_sprites = new Sprite[asset.__12()];
for ( int iSprite=0 ; iSprite<obj.m_sprites.Length ; iSprite++ )
{
Sprite sprite = asset.__27();
obj.m_sprites[iSprite] = sprite;
sprite.m_obj = obj;
sprite.m_packGroupType = (PACKGROUP)asset.__12();
if ( sprite.m_packGroupType==PACKGROUP.SCN )
sprite.m_packGroupScene = asset.__18();
else if ( sprite.m_packGroupType==PACKGROUP.FLY || sprite.m_packGroupType==PACKGROUP.FLY_LOW )
{
string name = asset.__18();
Anim anim = obj.__470(ref name);
if ( anim )
{
if ( anim.m_sprites==null )
anim.m_sprites = new List<Sprite>();
anim.m_sprites.Add(sprite);
}
}
}
for ( int iFrame=0 ; iFrame<frames.Count ; iFrame++ )
{
int index = asset.__13();
int index2 = asset.__13();
frames[index].m_flipped = asset.__10();
if ( index2==-1 )
{
int iPack = asset.__12();
int layer = asset.__12();
frames[index].m_sprite = obj.m_sprites[iPack];
frames[index].m_layer = layer==255 ? -1 : layer;
frames[index].m_source.x = (float)asset.__13();
frames[index].m_source.y = (float)asset.__13();
frames[index].m_source.width = (float)asset.__13();
frames[index].m_source.height = (float)asset.__13();
frames[index].m_rotated = asset.__10();
}
else
frames[index].m_index2 = index2;
}
for ( int iFrame=0 ; iFrame<frames.Count ; iFrame++ )
{
if ( frames[iFrame].m_index2!=-1 )
{
int j = frames[iFrame].m_index2;
frames[iFrame].m_sprite = frames[j].m_sprite;
frames[iFrame].m_layer = frames[j].m_layer;
frames[iFrame].m_source = frames[j].m_source;
frames[iFrame].m_rotated = frames[j].m_rotated;
}
}
return obj;
}
public Dialog __229(Asset asset, int sid, string uid)
{
Dialog dlg = new Dialog();
dlg.m_sid = sid;
dlg.m_uid = uid;
dlg.m_tags = asset.ReadTags();
dlg.m_root = new Sentence();
dlg.m_sentencesByID.Add(dlg.m_root.m_sid, dlg.m_root);
dlg.m_root.m_dialog = dlg;
int count = asset.__13();
if ( count>0 )
{
dlg.m_root.m_subs = new Sentence[count];
__231(asset, dlg.m_root);
}
dlg.m_entryPointIDs = new int[asset.__12()];
for ( int i=0 ; i<dlg.m_entryPointIDs.Length ; i++ )
dlg.m_entryPointIDs[i] = asset.__13();
return dlg;
}
public void __230(Asset asset, Sentence sentence)
{
sentence.m_sid = asset.__13();
sentence.m_tags = asset.ReadTags();
sentence.m_dialog.m_sentencesByID.Add(sentence.m_sid, sentence);
sentence.m_choice = asset.__10();
sentence.m_randomly = asset.__10();
sentence.m_hideBranch = asset.__10();
sentence.m_goto = asset.__13();
sentence.m_exit = asset.__10();
sentence.m_iDir = asset.__11();
sentence.m_lookat = asset.__18();
sentence.m_keepDir = asset.__10();
sentence.m_anim = asset.__18();
sentence.m_keepAnim = asset.__10();
sentence.m_task = asset.__10();
if ( sentence.m_task )
{
sentence.m_taskRole = asset.__18();
sentence.m_taskName = asset.__18();
sentence.m_taskArg = asset.__18();
}
int mutualShowCount = asset.__12();
if ( mutualShowCount!=0 )
{
sentence.m_mutualShows = new int[mutualShowCount];
for ( int i=0 ; i<mutualShowCount ; i++ )
sentence.m_mutualShows[i] = asset.__13();
}
if ( sentence.m_choice )
{
sentence.m_visible.Init(asset.__10());
sentence.m_doNotSay = asset.__10();
sentence.m_neverHide = asset.__10();
sentence.m_locked = asset.__10();
sentence.m_icon = __277(asset.__18());
if ( sentence.m_icon==null )
sentence.m_text = asset.__21();
}
else
{
sentence.m_entryPoint = asset.__10();
sentence.m_duration = (float)asset.__12();
sentence.m_speaker = asset.__18();
sentence.m_text = asset.__21();
for ( int i=0 ; i<sentence.m_text.m_values.Length ; i++ )
{
if ( sentence.m_text.m_values[i].Length==0 )
sentence.m_text.m_values[i] += (char)(byte)1;
}
}
if ( asset.__10() )
{
sentence.m_voice = new SentenceVoice();
for ( int i=0 ; i<m_languages.Length ; i++ )
{
int paragraphCount = asset.__12();
for ( int j=0 ; j<paragraphCount ; j++ )
sentence.m_voice.m_paths[i].Add(asset.__18());
}
}
if ( m_hasVoices==false )
sentence.m_voice = null;
int count = asset.__13();
if ( count>0 )
{
sentence.m_subs = new Sentence[count];
__231(asset, sentence);
}
}
public void __231(Asset asset, Sentence parent)
{
if ( m_randomizeDialogChoices )
{
bool doNotRandomize = false;
List<Sentence> redSubs = new List<Sentence>();
List<Sentence> blueSubs = new List<Sentence>();
for ( int i=0 ; i<parent.m_subs.Length ; i++ )
{
Sentence sentence = new Sentence();
sentence.m_ageIndex = i;
sentence.m_dialog = parent.m_dialog;
sentence.m_parent = parent;
__230(asset, sentence);
if ( sentence.m_choice )
{
redSubs.Add(sentence);
if ( doNotRandomize==false )
{
if ( sentence.m_text && sentence.m_text.m_values[m_optionLanguageDef].Length==0 )
doNotRandomize = true;
}
}
else
blueSubs.Add(sentence);
}
int redCount = redSubs.Count;
int iSub = 0;
while ( iSub<redCount )
{
int index = doNotRandomize ? iSub : G.__156(redSubs.Count);
parent.m_subs[iSub++] = redSubs[index];
if ( doNotRandomize==false )
redSubs.RemoveAt(index);
}
for ( int i=0 ; i<blueSubs.Count ; i++ )
parent.m_subs[iSub++] = blueSubs[i];
}
else
{
for ( int i=0 ; i<parent.m_subs.Length ; i++ )
{
parent.m_subs[i] = new Sentence();
parent.m_subs[i].m_ageIndex = i;
parent.m_subs[i].m_dialog = parent.m_dialog;
parent.m_subs[i].m_parent = parent;
__230(asset, parent.m_subs[i]);
}
}
}
public Player __232(Asset asset, int sid, string uid)
{
Player player = new Player();
player.m_sid = sid;
player.m_uid = uid;
player.m_tags = asset.ReadTags();
player.m_interaction = asset.__10();
player.m_hasScroll.Init(asset.__10());
player.m_scrollSmooth.Init(asset.__12()/100.0f);
player.m_hasZoom.Init(asset.__10());
player.m_zoomSmooth.Init(asset.__12()/100.0f);
player.m_hasShowPath = asset.__10();
player.m_pathColor = asset.__25();
if ( player.m_hasShowPath )
m_hasShowPathPlayer = true;
player.m_icons = new Sprite[G.ICON_COUNT];
for ( int i=0 ; i<G.ICON_COUNT ; i++ )
player.m_icons[i] = asset.__27();
return player;
}
public Cinematic __233(Asset asset, int sid, string uid)
{
Cinematic cine = new Cinematic();
cine.m_sid = sid;
cine.m_uid = uid;
cine.m_descs = new CinematicDesc[m_languages.Length];
for ( int i=0 ; i<m_languages.Length ; i++ )
cine.m_descs[i] = new CinematicDesc();
for ( int i=0 ; i<m_languages.Length ; i++ )
{
cine.m_descs[i].m_video = asset.__18();
if ( cine.m_descs[i].m_video.Length==0 )
continue;
cine.m_descs[i].m_width = asset.__13();
cine.m_descs[i].m_height = asset.__13();
cine.m_descs[i].m_fps = asset.__12();
cine.m_descs[i].m_frameCount = asset.__15();
}
for ( int i=0 ; i<m_languages.Length ; i++ )
cine.m_descs[i].m_audio = asset.__18();
cine.m_subs = new List<CinematicText>[m_languages.Length];
for ( int i=0 ; i<m_languages.Length ; i++ )
{
cine.m_subs[i] = new List<CinematicText>();
while ( true )
{
int start = asset.__15();
if ( start==0 )
break;
CinematicText sub = new CinematicText();
sub.m_index = cine.m_subs[i].Count;
sub.m_start = (start-1)/1000.0f;
sub.m_end = asset.__15()/1000.0f;
sub.m_text = asset.__22(i);
cine.m_subs[i].Add(sub);
}
}
cine.m_skip = (CINEMATICSKIP)asset.__12();
cine.m_effect.Init(__282(asset.__18()));
return cine;
}
public Effect __234(Asset asset, int sid, string uid)
{
Effect effect = new Effect();
effect.m_sid = sid;
effect.m_uid = uid;
effect.__64(asset);
return effect;
}
public Role __235(Asset asset, int sid, string uid)
{
Role role = new Role();
role.m_sid = sid;
role.m_uid = uid;
role.__64(asset);
return role;
}
public void __236(Asset asset, Asset assetGraphics, ref Sprite sprite, SHADER shaderDef = SHADER.TEXTURE32)
{
sprite = asset.__27();
if ( sprite )
sprite.__468(assetGraphics, shaderDef);
}
public void __237(Asset asset, Sprite sprite, SHADER shaderDef = SHADER.TEXTURE32)
{
if ( sprite && sprite.__988() )
sprite.__468(asset, shaderDef);
}
public bool __238(string domain, string[] domains)
{
if ( domains.Length==0 || (domains.Length==1 && domains[0].Length==0) )
return true;
int len = domain.Length;
for ( int i=0 ; i<domains.Length ; i++ )
{
int len2 = domains[i].Length;
if ( len2==0 )
continue;
if ( len>=len2 && domain.Substring(len-len2)==domains[i] )
{
if ( len==len2 || domain.Substring(len-len2-1, 1)=="." )
return true;
}
}
return false;
}
public void __239()
{
for ( int i=0 ; i<G.CHANNEL_COUNT ; i++ )
{
m_songs[i].m_lastName = "";
m_songs[i].m_lastVolume = 1.0f;
m_songs[i].m_lastLoop = true;
}
}
public void __240()
{
__223();
m_cinematicPlayer.Stop();
__266();
for ( int i=0 ; i<G.CHANNEL_COUNT ; i++ )
__262(i);
__268();
__239();
m_cursor = false;
m_cursorViewX = 0.0f;
m_cursorViewY = 0.0f;
m_rcCursor = Rect.Zero;
__297();
for ( int i=0 ; i<m_scenes.Length ; i++ )
m_scenes[i].Reset();
for ( int i=0 ; i<m_objects.Length ; i++ )
m_objects[i].Reset();
for ( int i=0 ; i<m_dialogs.Length ; i++ )
m_dialogs[i].Reset();
for ( int i=0 ; i<m_players.Length ; i++ )
m_players[i].Reset();
for ( int i=0 ; i<m_effects.Length ; i++ )
m_effects[i].Reset();
for ( int i=0 ; i<m_roles.Length ; i++ )
m_roles[i].Reset(false);
for ( int i=0 ; i<m_cinematics.Length ; i++ )
m_cinematics[i].Reset();
KeyValuePair<string, Variable>[] variables = m_variables.ToArray();
for ( int i=0 ; i<variables.Length ; i++ )
variables[i].Value.Reset();
variables = null;
m_variables.Clear();
m_sysVarArg = __288("_arg");
m_sysVarChoice = __288("_choice");
m_sysVarLabel = __288("_label");
m_sysVarObj = __288("_obj");
m_sysVarObj2 = __288("_obj2");
m_sysVarOr = __288("_or");
m_sysVarPlayer = __288("_player");
m_sysVarPlayer2 = __288("_player2");
m_sysVarRes = __288("_res");
m_sysVarSub = __288("_sub");
m_menuDialog.Reset();
m_scenario.Reset();
m_layout.Reset();
m_layout.__417(new string[0]);
m_started = false;
m_isGameOver = false;
m_menuScene = null;
m_iScene = -1;
m_iOldScene = -1;
m_iPlayer = -1;
m_cursorObjTime = 0.0f;
m_cursorObj = null;
m_cursorVisible = true;
m_gestureBagLocked = false;
m_gestureMenuLocked = false;
m_locked = false;
m_lockedDuration = -1.0f;
m_saveMenuLocked = false;
m_useLocked = false;
m_lockedByAnim = 0;
m_takers.Clear();
m_brightness = 1.0f;
m_fade = FADING.NONE;
m_legendTime = -1.0f;
m_timeAltIcon = 0.0f;
m_iRenderAltIcon = 0;
m_timeDragIcon = 0.0f;
m_iRenderDragIcon = 0;
m_lastEventSubObj = null;
m_lastEventByUser = true;
m_autoSaveAsap = false;
m_autoSaveIconStartTime = 0.0f;
m_autoSaveIconRatio = 0.0f;
m_interludeLock = false;
m_interludeLockTime = 0.0f;
m_interludeLockDuration = 0.0f;
m_interludeAsap = false;
m_interludeParam = "";
m_colorVoiceOver = Color.clear;
m_menuEffect.Reset();
m_effect.Reset();
#if UNITY_STANDALONE_WIN
if ( m_configDebug )
{
string path = G.m_folderSavegame + "debug.txt";
System.IO.Stream file = G.NewFile(path);
if ( file!=null )
{
G.__94(file, "New Game");
file.Close();
}
}
if ( m_configRun )
IDE.Post(IDE_MSG.RESET);
#endif
}
public void __241(bool newgame = true)
{
if ( newgame )
m_scenario.__517();
Player player = __293();
if ( player )
player.__468();
if ( m_initScript )
m_initScript.__684();
if ( newgame )
{
for ( int i=0 ; i<m_roles.Length ; i++ )
{
if ( m_roles[i].m_autoStart )
m_roles[i].Start();
}
}
if ( m_configDebug && m_scenario.__518().m_debugResolved )
{
m_scenario.__42(SCENARIO.DEBUG_START);
m_scenario.__42(SCENARIO.DEBUG_UPDATE);
}
m_started = true;
if ( m_liveScene.Length==0 && m_configDebug && m_scenario.m_debugFirstScene.Length>0 )
__301(m_scenario.m_debugFirstScene);
}
public bool __242(string name)
{
return m_symbols.ContainsKey(name);
}
public bool __243()
{
m_gameValues.Clear();
JsonObj json = G.__183(G.__185());
if ( json==null )
return false;
JsonArray jsonValues = json.__394("values");
if ( jsonValues==null )
return false;
for ( int i=0 ; i<jsonValues.__66() ; i++ )
{
JsonObj jsonItem = jsonValues.__393(i);
if ( jsonItem )
m_gameValues.Add(jsonItem.GetString("k"), jsonItem.GetString("v"));
}
return true;
}
public void __244()
{
m_gameValues.Clear();
G.__186();
}
public bool __245(string name, string value)
{
if ( G.__104(ref name)==false )
return false;
if ( m_gameValues.ContainsKey(name) )
m_gameValues[name] = value;
else
m_gameValues.Add(name, value);
JsonObj json = Json.__373();
JsonArray jsonValues = json.__389("values");
foreach ( var val in m_gameValues )
{
JsonObj jsonItem = jsonValues.__388();
jsonItem.__380("k", val.Key);
jsonItem.__380("v", val.Value);
}
G.__184(json, G.__185());
return true;
}
public string __246(string name)
{
if ( G.__104(ref name)==false )
return "";
string value;
if ( m_gameValues.TryGetValue(name, out value)==false )
return "";
return value;
}
public void __46(int index)
{
JsonObj json = Json.__373();
JsonObj jSystem = json.__388("system");
jSystem.__381("version", G.SAVEGAME_VERSION);
jSystem.__380("game_version", m_gameVersion);
jSystem.__384("game_sign_auto", m_gameSignatureAuto);
jSystem.__381("game_sign_value", m_gameSignatureValue);
string stamp = DateTime.Now.ToString();
jSystem.__380("name", stamp);
JsonArray jVars = json.__389("vars");
foreach ( var variable in m_variables )
{
JsonObj jVar = jVars.__388();
jVar.__380("k", variable.Value.m_name);
jVar.__380("v", variable.Value.m_value);
if ( variable.Value.m_list!=null )
{
JsonArray jList = jVar.__389("l");
for ( int i=0 ; i<variable.Value.m_list.Count ; i++ )
jList.__380(variable.Value.m_list[i]);
}
}
JsonObj jGame = json.__388("game");
jGame.__383("brightness", m_brightness);
jGame.__382("scene", m_iScene==-1 ? 0 : m_scenes[m_iScene].m_sid);
jGame.__382("old_scene", m_iOldScene==-1 ? 0 : m_scenes[m_iOldScene].m_sid);
jGame.__382("player", m_iPlayer==-1 ? 0 : m_players[m_iPlayer].m_sid);
JsonArray jGamePlayers = jGame.__389("item_players");
for ( int i=0 ; i<m_layout.m_players.Length ; i++ )
jGamePlayers.__382(m_layout.m_players[i].m_sid);
jGame.__384("gesture_bag_locked", m_gestureBagLocked);
jGame.__384("gesture_menu_locked", m_gestureMenuLocked);
jGame.__384("use_locked", m_useLocked);
jGame.__384("bag_locked", m_layout.m_bagLocked);
jGame.__384("bag_hidden", m_layout.m_bagForceHidden);
if ( m_menuEffect.modified )
json.__382("menu_effect", m_menuEffect.cur.m_sid);
if ( m_effect.modified )
json.__382("effect", m_effect.cur.m_sid);
jGame.__380("cursor", m_cursorCustom);
JsonObj jTimeline = json.__388("timeline");
if ( m_timeline )
{
jTimeline.__382("id", m_timeline.m_sid);
jTimeline.__383("time", m_timeline.m_time);
jTimeline.__382("role", m_timeline.m_roleBox.m_parent.m_sid);
jTimeline.__381("box", m_timeline.m_roleBox.m_id);
}
JsonArray jPlayers = json.__389("players");
for ( int i=0 ; i<m_players.Length ; i++ )
{
JsonObj jPlayer = jPlayers.__388();
m_players[i].__46(jPlayer);
}
JsonArray jObjects = json.__389("objects");
for ( int i=0 ; i<m_objects.Length ; i++ )
{
JsonObj jObject = jObjects.__388();
m_objects[i].__46(jObject);
}
JsonArray jScenes = json.__389("scenes");
for ( int i=0 ; i<m_scenes.Length ; i++ )
{
JsonObj jScene = jScenes.__388();
m_scenes[i].__46(jScene);
}
JsonArray jDialogs = json.__389("dialogs");
for ( int i=0 ; i<m_dialogs.Length ; i++ )
{
JsonObj jDialog = jDialogs.__388();
m_dialogs[i].__46(jDialog);
}
JsonArray jRoles = json.__389("roles");
for ( int i=0 ; i<m_roles.Length ; i++ )
{
JsonObj jRole = jRoles.__388();
m_roles[i].__46(jRole);
}
JsonObj jScenario = json.__388("scenario");
m_scenario.__46(jScenario);
for ( int i=0 ; i<G.CHANNEL_COUNT ; i++ )
{
JsonObj jSong = json.__388(i==0 ? "song" : "song_"+(i+1).ToString());
if ( m_songs[i].m_current==null )
{
jSong.__380("name", "");
jSong.__381("volume", 0);
}
else
{
jSong.__380("name", m_songs[i].m_current.m_name);
jSong.__381("volume", (int)(m_songs[i].m_current.m_volume*100.0f));
}
jSong = json.__388(i==0 ? "last_song" : "last_song_"+(i+1).ToString());
jSong.__380("name", m_songs[i].m_lastName);
jSong.__381("volume", (int)(m_songs[i].m_lastVolume*100.0f));
jSong.__384("loop", m_songs[i].m_lastLoop);
}
G.__184(json, G.__188(index));
}
public bool __247(JsonObj js)
{
if ( js==null )
return false;
int version = js.GetInt("version");
if ( version<1 )
return false;
if ( js.__400("game_sign_auto")!=m_gameSignatureAuto )
return false;
if ( js.GetInt("game_sign_value")!=m_gameSignatureValue )
return false;
return true;
}
public bool __47(int index, bool isChapter = false)
{
__240();
JsonObj jAsset = null;
if ( isChapter )
{
(int, int) pair;
if ( m_chapters.TryGetValue(index, out pair)==false )
return false;
Asset asset = G.__95(G.m_pathGame);
if ( asset==null )
return false;
asset.__3(pair.Item2);
jAsset = Json.__375(Json.__372(asset.__9(pair.Item1)));
asset.Close();
}
else
{
jAsset = G.__183(G.__188(index));
}
if ( jAsset==null )
return false;
JsonObj jSystem = jAsset.__393("system");
if ( jSystem==null )
return false;
if ( __247(jSystem)==false )
return false;
if ( __47(jAsset)==false )
return false;
Timeline timeline = null;
RoleBox timelineBox = null;
float timelineTime = 0.0f;
Scene timelineScene = __291();
JsonObj jTimeline = jAsset.__393("timeline");
if ( jTimeline && timelineScene )
{
timeline = timelineScene.__541(jTimeline.GetInt("id"));
if ( timeline )
{
Role role = __283(jTimeline.GetInt("role"));
if ( role )
{
timelineBox = role.__496(jTimeline.GetInt("box"));
timelineTime = jTimeline.GetFloat("time");
}
}
}
jAsset = null;
__241(false);
Scene scene = __291();
if ( scene )
{
scene.__508("LOAD");
if ( timelineBox )
__272(timeline, timelineBox, timelineTime);
}
return true;
}
public bool __47(JsonObj jAsset)
{
JsonArray jVars = jAsset.__394("vars");
if ( jVars )
{
for ( int i=0 ; i<jVars.__66() ; i++ )
{
JsonObj jVar = jVars.__393(i);
if ( jVar )
{
Variable variable = new Variable();
variable.m_name = jVar.GetString("k");
variable.m_value = jVar.GetString("v");
JsonArray jList = jVar.__394("l");
if ( jList!=null )
{
variable.m_list = new List<string>();
for ( int j=0 ; j<jList.__66() ; j++ )
variable.m_list.Add(jList.GetString(j));
}
if ( m_variables.ContainsKey(variable.m_name)==false )
m_variables.Add(variable.m_name, variable);
}
}
}
string[] players = null;
JsonObj jGame = jAsset.__393("game");
if ( jGame )
{
m_brightness = G.Clamp(jGame.GetFloat("brightness"), -1.0f, 1.0f);
m_iScene = __275(jGame.GetInt("scene"));
m_iOldScene = __275(jGame.GetInt("old_scene"));
m_iPlayer = __280(jGame.GetInt("player"));
JsonArray jGamePlayers = jGame.__394("item_players");
if ( jGamePlayers )
{
players = new string[jGamePlayers.__66()];
for ( int i=0 ; i<jGamePlayers.__66() ; i++ )
{
Player player = __279(jGamePlayers.GetInt(i));
players[i] = player==null ? "" : player.m_uid;
}
}
m_gestureBagLocked = jGame.__400("gesture_bag_locked");
m_gestureMenuLocked = jGame.__400("gesture_menu_locked");
m_useLocked = jGame.__400("use_locked");
m_layout.m_bagLocked = jGame.__400("bag_locked");
m_layout.m_bagForceHidden = jGame.__400("bag_hidden");
if ( jGame.__390("menu_effect") )
m_menuEffect.Set(__282(jGame.GetInt("menu_effect")));
if ( jGame.__390("effect") )
m_effect.Set(__282(jGame.GetInt("effect")));
__298(jGame.GetString("cursor"));
}
JsonArray jPlayers = jAsset.__394("players");
if ( jPlayers )
{
for ( int i=0 ; i<jPlayers.__66() ; i++ )
{
JsonObj jPlayer = jPlayers.__393(i);
int sid = jPlayer==null ? 0 : jPlayer.GetInt("sid");
Player player = __279(sid);
if ( player )
player.__47(jPlayer);
}
}
JsonArray jObjects = jAsset.__394("objects");
if ( jObjects )
{
for ( int i=0 ; i<jObjects.__66() ; i++ )
{
JsonObj jObject = jObjects.__393(i);
int sid = jObject==null ? 0 : jObject.GetInt("sid");
Obj obj = __277(sid);
if ( obj )
obj.__47(jObject);
}
}
JsonArray jScenes = jAsset.__394("scenes");
if ( jScenes )
{
for ( int i=0 ; i<jScenes.__66() ; i++ )
{
JsonObj jScene = jScenes.__393(i);
int sid = jScene==null ? 0 : jScene.GetInt("sid");
Scene scene = __274(sid);
if ( scene )
scene.__47(jScene);
}
}
JsonArray jDialogs = jAsset.__394("dialogs");
if ( jDialogs )
{
for ( int i=0 ; i<jDialogs.__66() ; i++ )
{
JsonObj jDialog = jDialogs.__393(i);
int sid = jDialog==null ? 0 : jDialog.GetInt("sid");
Dialog dialog = __278(sid);
if ( dialog )
dialog.__47(jDialog);
}
}
JsonArray jRoles = jAsset.__394("roles");
if ( jRoles )
{
for ( int i=0 ; i<jRoles.__66() ; i++ )
{
JsonObj jRole = jRoles.__393(i);
int sid = jRole==null ? 0 : jRole.GetInt("sid");
Role role = __283(sid);
if ( role )
{
if ( role.__47(jRole)==false )
return false;
}
}
}
JsonObj jScenario = jAsset.__393("scenario");
if ( jScenario )
{
if ( m_scenario.__47(jScenario)==false )
return false;
}
m_layout.__417(players);
for ( int i=0 ; i<G.CHANNEL_COUNT ; i++ )
{
JsonObj jSong = jAsset.__393(i==0 ? "song" : "song_"+(i+1).ToString());
if ( jSong )
{
string name = jSong.GetString("name");
float volume = jSong.GetInt("volume")/100.0f;
if ( name.Length>0 )
__264(i, name, volume);
}
jSong = jAsset.__393(i==0 ? "last_song" : "last_song_"+(i+1).ToString());
if ( jSong )
{
m_songs[i].m_lastName = jSong.GetString("name");
m_songs[i].m_lastVolume = jSong.GetInt("volume")/100.0f;
m_songs[i].m_lastLoop = jSong.__400("loop");
}
}
return true;
}
public void __248()
{
if ( G.m_pathSettings.Length==0 )
return;
JsonObj json = new JsonObj();
json.SetInt("version", 1);
JsonObj jConfig = json.__388("config");
jConfig.SetString("audio_lang", m_languages[m_optionLanguageAudio].m_name);
jConfig.SetString("text_lang", m_languages[m_optionLanguageText].m_name);
jConfig.SetInt("subtitle", (int)m_optionSubtitle);
jConfig.SetInt("sound", (int)m_optionSound);
jConfig.SetInt("voice", (int)m_optionVoice);
jConfig.SetInt("song", (int)m_optionSong);
jConfig.SetInt("font", (int)m_optionFont);
jConfig.SetInt("quality", (int)m_optionQuality);
jConfig.SetString("user", m_savegameServerUser);
jConfig.SetString("pass", G.__195(m_savegameServerPass));
string userButtons = "";
for ( int i=0 ; i<m_menuUserButtonVisibilities.Length ; i++ )
userButtons += m_menuUserButtonVisibilities[i] ? "1" : "0";
jConfig.SetString("user_buttons", userButtons);
G.__184(json, G.m_pathSettings);
}
public void __249(Asset assetGraphics = null)
{
JsonObj json = G.__183(G.m_pathSettings);
if ( json==null )
return;
string value;
JsonObj jConfig = json.__393("config");
if ( jConfig==null )
return;
value = jConfig.GetString("audio_lang");
m_optionLanguageAudio = 0;
for ( int i=0 ; i<m_languages.Length ; i++ )
{
if ( G.__148(ref m_languages[i].m_name, ref value) )
{
m_optionLanguageAudio = i;
break;
}
}
value = jConfig.GetString("text_lang");
m_optionLanguageText = 0;
for ( int i=0 ; i<m_languages.Length ; i++ )
{
if ( G.__148(ref m_languages[i].m_name, ref value) )
{
m_optionLanguageText = i;
break;
}
}
m_optionSubtitle = (SUBTITLE)jConfig.GetInt("subtitle");
m_optionSound = jConfig.GetInt("sound");
m_optionVoice = jConfig.GetInt("voice");
m_optionSong = jConfig.GetInt("song");
if ( m_fontSizeOption )
m_optionFont = jConfig.GetInt("font");
QUALITY quality = m_optionQuality;
if ( m_noLowQuality )
m_optionQuality = QUALITY.HIGH;
else
m_optionQuality = (QUALITY)jConfig.GetInt("quality");
m_savegameServerUser = jConfig.GetString("user");
m_savegameServerPass = G.__196(jConfig.GetString("pass"));
value = jConfig.GetString("user_buttons");
for ( int i=0 ; i<m_menuUserButtonVisibilities.Length ; i++ )
m_menuUserButtonVisibilities[i] = true;
for ( int i=0 ; i<value.Length ; i++ )
{
if ( value[i]=='0' )
m_menuUserButtonVisibilities[i] = false;
}
if ( quality!=m_optionQuality )
__221(assetGraphics);
}
public void __250(string msg)
{
#if UNITY_STANDALONE_WIN
if ( m_configDebug )
{
#if UNITY_EDITOR
#endif
string path = G.m_folderSavegame + "debug.txt";
System.IO.Stream stream = new System.IO.FileStream(path, System.IO.FileMode.Append, System.IO.FileAccess.Write);
if ( stream!=null )
{
G.__94(stream, msg);
stream.Close();
}
}
#endif
}
public void __251(string msg)
{
if ( G.m_pathUserLog.Length==0 )
return;
System.IO.FileStream stream = new System.IO.FileStream(G.m_pathUserLog, System.IO.FileMode.Append, System.IO.FileAccess.Write);
if ( stream!=null )
{
string text = DateTime.Now.ToString("{yyyy-MM-dd} {HH:mm:ss.fff} ");
text += "{" + m_cursor + ":" + m_cursorViewX + "," + m_cursorViewY + "} ";
Scene scene = __291();
if ( scene==null )
text += "{,0,0} ";
else
text += "{" + scene.m_uid + "," + scene.m_width + "," + scene.m_height + "} ";
text += "{";
text += msg;
text += "}";
G.__94(stream, text);
stream.Close();
}
}
public void __252(bool escapeKey)
{
if ( MessageBox.m_instance.__38() )
return;
if ( m_cinematicPlayer.__38() )
return;
if ( m_menuGame.m_id==MenuGame.ID_SPLASH1 || m_menuGame.m_id==MenuGame.ID_SPLASH2 || m_menuGame.m_id==MenuGame.ID_START )
return;
if ( escapeKey )
{
if ( m_menuGame.__38() )
{
if ( m_menuGame.m_id==MenuGame.ID_MAIN )
{
if ( m_menuGame.m_intro==false )
m_menuGame.Close();
}
else
m_menuGame.Open(MenuGame.ID_MAIN, m_menuGame.m_intro, m_menuGame.m_disableSave);
}
else
{
__257();
}
}
else
{
if ( m_menuGame.__38()==false )
__257();
}
}
public bool __253()
{
if ( MessageBox.m_instance.__38() )
return false;
if ( m_cinematicPlayer.__38() )
return false;
if ( m_menuGame.__38() )
return false;
if ( m_menuDialog.__38() )
return false;
if ( __256() )
return false;
if ( m_examine )
return false;
if ( m_fade!=FADING.NONE && m_fadeLock )
return false;
for ( int i=0 ; i<G.CHANNEL_COUNT ; i++ )
{
if ( m_songs[i].m_fadeMode!=0 )
return false;
}
SceneObj playerSceneObj = __295();
if ( playerSceneObj==null )
return false;
if ( playerSceneObj.m_pathStarted.cur )
return false;
if ( playerSceneObj.m_routeStatus==Router.FOUND )
{
if ( playerSceneObj.m_routeLocked || playerSceneObj.m_routeTurn )
return false;
}
if ( playerSceneObj.m_message )
{
if ( playerSceneObj.m_message.m_state==Message.S_ANIM )
return false;
if ( playerSceneObj.m_message.m_state==Message.S_MOVE && playerSceneObj.m_message.m_enter )
return false;
}
if ( playerSceneObj.m_anim.cur )
{
string name = playerSceneObj.m_anim.cur.m_name;
if ( name!=playerSceneObj.m_defaultStopAnim.cur && name!=playerSceneObj.m_defaultWalkAnim.cur )
return false;
}
for ( int i=0 ; i<m_roles.Length ; i++ )
{
if ( m_roles[i].__256() )
return false;
}
return true;
}
public bool __254()
{
if ( MessageBox.m_instance.__38() )
return false;
if ( m_cinematicPlayer.__38() )
return false;
if ( m_menuGame.__38() )
return false;
if ( m_menuDialog.__38() )
return false;
if ( m_locked )
return false;
if ( m_examine )
return false;
if ( m_fade!=FADING.NONE && m_fadeLock )
return false;
if ( m_layout.m_bagLocked==false && m_layout.m_bagOpened )
return false;
return true;
}
public bool __255()
{
return m_timeline && m_timeline.m_roleBox.__256();
}
public bool __256()
{
return m_locked || m_lockedByAnim>0 || m_interludeLock;
}
public void __257()
{
m_cursorObj = null;
m_layout.m_bagOpened = false;
bool disableSave = __253()==false || m_layout.m_bagLocked || m_saveMenuLocked;
if ( m_customMenu.Length==0 )
m_menuGame.Open(m_savegameEnabled ? MenuGame.ID_PLAY : MenuGame.ID_MAIN, false, disableSave);
else
{
m_customMenuSaveDisabled = disableSave;
__302(m_customMenu);
}
}
public void __258()
{
if ( m_menuScene )
__302("");
else if ( m_menuGame.__38() )
m_menuGame.Close();
}
public void __259()
{
m_cursorObj = null;
m_layout.m_bagOpened = false;
m_menuGame.Open(MenuGame.ID_CREDITS, false, true, false, true);
}
public bool __260()
{
if ( m_hasSongs==false )
return false;
for ( int i=0 ; i<G.CHANNEL_COUNT ; i++ )
{
if ( m_songs[i].m_current )
return true;
}
return false;
}
public void __261(float fadeDuration = 0.0f)
{
for ( int i=0 ; i<G.CHANNEL_COUNT ; i++ )
__262(i, fadeDuration);
}
public void __262(int channel, float fadeDuration = 0.0f)
{
if ( m_hasSongs==false )
return;
if ( channel<-1 || channel>=G.CHANNEL_COUNT )
return;
if ( channel==-1 )
{
__261(fadeDuration);
return;
}
Song song = m_songs[channel];
if ( song.m_current==null )
return;
song.m_lastName = song.m_current.m_name;
song.m_lastVolume = song.m_current.m_volume;
song.m_lastLoop = song.m_current.m_loop;
song.m_crossfade.Stop();
if ( fadeDuration>0.0f )
{
song.m_fadeMode = 1;
song.m_fadeTime = 0.0f;
song.m_fadeDuration = fadeDuration;
song.m_fadeVolBeg = song.m_current.__987();
song.m_fadeVolEnd = 0.0f;
}
else
{
song.m_fadeMode = 0;
song.m_current.Stop();
}
}
public void __263(int channel, float volume, float fadeDuration)
{
if ( m_hasSongs==false )
return;
if ( channel<0 || channel>=G.CHANNEL_COUNT )
return;
Song song = m_songs[channel];
if ( song.m_current==null )
return;
if ( fadeDuration>0.0f )
{
song.m_fadeMode = 2;
song.m_fadeTime = 0.0f;
song.m_fadeDuration = fadeDuration;
song.m_fadeVolBeg = song.m_current.__987();
song.m_fadeVolEnd = volume;
}
else
{
song.m_current.__986(volume);
}
}
public bool __264(int channel, string name, float volume = 1.0f, bool loop = true)
{
if ( m_hasSongs==false )
return false;
if ( channel<0 || channel>=G.CHANNEL_COUNT )
return false;
Song song = m_songs[channel];
song.m_crossfade.Stop();
__262(channel);
if ( name.Length>0 )
{
#if UNITY_WEBGL
string url = G.m_folderContentSongs + name + G.m_audioExtension;
for ( int i=0 ; i<G.m_downloadedSongs.Count ; i++ )
{
if ( G.__148(ref G.m_downloadedSongs[i].m_url, ref url) )
return G.m_downloadedSongs[i].__983(null, "");
}
Sound sound = new Sound(Sound.TYPE.MUSIC);
G.m_downloadedSongs.Add(sound);
sound.m_name = name;
sound.m_loop = loop;
return sound.__982(volume, channel);
#else
song.m_sound.m_name = name;
song.m_sound.m_loop = loop;
return song.m_sound.__982(volume, channel);
#endif
}
return false;
}
public bool __265(int channel, string name, float duration = 2.0f, float volume = 1.0f, bool loop = true)
{
if ( m_hasSongs==false )
return false;
if ( channel<0 || channel>=G.CHANNEL_COUNT )
return false;
Song song = m_songs[channel];
bool hasOld = false;
bool hasNew = false;
song.m_crossfade.Stop();
if ( song.m_current && song.m_current.__1() )
{
song.m_crossfade.m_name = song.m_current.m_name;
song.m_crossfade.m_url = song.m_current.m_url;
song.m_crossfade.m_audioSource = song.m_current.m_audioSource;
song.m_crossfade.m_audioClip = song.m_current.m_audioClip;
song.m_crossfade.m_volume = song.m_current.m_volume;
song.m_current.m_audioSource = null;
song.m_current.m_audioClip = null;
song.m_current.Stop();
hasOld = true;
}
#if UNITY_EDITOR
return hasOld && hasNew;
#else
song.m_fadeMode = -1;
song.m_fadeTime = 0.0f;
song.m_fadeDuration = duration;
song.m_fadeVolBeg = song.m_crossfade.m_volume;
song.m_fadeVolEnd = volume;
if ( name.Length>0 )
{
#if UNITY_WEBGL
string url = G.m_folderContentSongs + name + G.m_audioExtension;
for ( int i=0 ; i<G.m_downloadedSongs.Count ; i++ )
{
if ( G.__148(ref G.m_downloadedSongs[i].m_url, ref url) )
return G.m_downloadedSongs[i].__983(null, "");
}
Sound sound = new Sound(Sound.TYPE.MUSIC);
G.m_downloadedSongs.Add(sound);
sound.m_name = name;
sound.m_loop = true;
return sound.__982(volume, channel);
#else
song.m_sound.m_name = name;
song.m_sound.m_loop = loop;
hasNew = song.m_sound.__982(0.0f, channel);
#endif
}
if ( hasOld==false && hasNew==false )
{
song.m_fadeMode = 0;
return false;
}
return true;
#endif
}
public void __266()
{
if ( m_currentVoice )
m_currentVoice.Stop();
}
public bool __267(string name)
{
__266();
if ( name.Length>0 )
{
m_voice.m_name = name;
return m_voice.__982(1.0f);
}
return false;
}
public void __268()
{
while ( m_playingSounds.Count>0 )
m_playingSounds[m_playingSounds.Count-1].Stop();
}
public void __269()
{
for ( int i=0 ; i<m_playingSounds.Count ; i++ )
m_playingSounds[i].__985();
}
public void __270()
{
for ( int i=0 ; i<m_playingSounds.Count ; i++ )
m_playingSounds[i].__984();
}
public Sound __271(string name)
{
if ( name.Length==0 )
return null;
Sound sound;
if ( m_sounds.TryGetValue(name, out sound)==false )
return null;
return sound;
}
public bool __272(Timeline timeline, RoleBox roleBox, float startTime)
{
if ( timeline==null )
return false;
__273();
m_timeline = timeline;
m_timeline.Play(roleBox, startTime);
return true;
}
public void __273()
{
if ( m_timeline )
{
m_timeline.Stop();
m_timeline = null;
}
}
public Scene __274(int sid)
{
if ( sid==0 )
return null;
Scene scene;
if ( m_scenesBySID.TryGetValue(sid, out scene)==false )
return null;
return scene;
}
public Scene __274(string uid)
{
if ( uid.Length==0 )
return null;
Scene scene;
if ( m_scenesByUID.TryGetValue(uid, out scene)==false )
return null;
return scene;
}
public int __275(int sid)
{
if ( sid==0 )
return -1;
for ( int i=0 ; i<m_scenes.Length ; i++ )
{
if ( m_scenes[i].m_sid==sid )
return i;
}
return -1;
}
public int __275(string uid)
{
if ( uid.Length==0 )
return -1;
for ( int i=0 ; i<m_scenes.Length ; i++ )
{
if ( G.__148(ref m_scenes[i].m_uid, ref uid) )
return i;
}
return -1;
}
public Scene __276(ref string input)
{
Scene currentScene = __291();
if ( input.Length==0 )
return currentScene;
if ( input[0]!='@' )
{
Scene scene = G.m_game.__274(input);
if ( scene==null )
return currentScene;
return scene;
}
if ( currentScene==null )
return null;
if ( currentScene.__48(ref input)==false )
return null;
return currentScene;
}
public Obj __277(int sid)
{
if ( sid==0 )
return null;
Obj obj;
if ( m_objectsBySID.TryGetValue(sid, out obj)==false )
{
Player player = __279(sid);
if ( player==null )
return null;
return player.m_obj;
}
return obj;
}
public Obj __277(string uid)
{
if ( uid.Length==0 )
return null;
if ( uid[0]=='O' || uid[0]=='o' )
{
Obj obj;
if ( m_objectsByUID.TryGetValue(uid, out obj)==false )
return null;
return obj;
}
Player player = __279(uid);
if ( player==null )
return null;
return player.m_obj;
}
public Dialog __278(int sid)
{
if ( sid==0 )
return null;
Dialog dialog;
if ( m_dialogsBySID.TryGetValue(sid, out dialog)==false )
return null;
return dialog;
}
public Dialog __278(string uid)
{
if ( uid.Length==0 )
return null;
Dialog dialog;
if ( m_dialogsByUID.TryGetValue(uid, out dialog)==false )
return null;
return dialog;
}
public Sentence __49(string uid, int sid)
{
if ( uid.Length==0 || sid==0 )
return null;
Dialog dialog;
if ( m_dialogsByUID.TryGetValue(uid, out dialog)==false )
return null;
return dialog.__49(sid);
}
public Player __279(int sid)
{
if ( sid==0 )
return null;
Player player;
if ( m_playersBySID.TryGetValue(sid, out player)==false )
return null;
return player;
}
public Player __279(string uid)
{
if ( uid.Length==0 )
return null;
Player player;
if ( m_playersByUID.TryGetValue(uid, out player)==false )
return null;
return player;
}
public int __280(int sid)
{
if ( sid==0 )
return -1;
for ( int i=1 ; i<m_players.Length ; i++ )
{
if ( m_players[i].m_sid==sid )
return i;
}
return -1;
}
public int __280(string uid)
{
if ( uid.Length==0 )
return -1;
for ( int i=1 ; i<m_players.Length ; i++ )
{
if ( G.__148(ref m_players[i].m_uid, ref uid) )
return i;
}
return -1;
}
public Cinematic __281(int sid)
{
if ( sid==0 )
return null;
Cinematic cinematic;
if ( m_cinematicsBySID.TryGetValue(sid, out cinematic)==false )
return null;
return cinematic;
}
public Cinematic __281(string uid)
{
if ( uid.Length==0 )
return null;
Cinematic cinematic;
if ( m_cinematicsByUID.TryGetValue(uid, out cinematic)==false )
return null;
return cinematic;
}
public Effect __282(int sid)
{
if ( sid==0 )
return null;
Effect fx;
if ( m_effectsBySID.TryGetValue(sid, out fx)==false )
return null;
return fx;
}
public Effect __282(string uid)
{
if ( uid.Length==0 )
return null;
Effect fx;
if ( m_effectsByUID.TryGetValue(uid, out fx)==false )
return null;
return fx;
}
public Role __283(int sid)
{
if ( sid==0 )
return null;
Role role;
if ( m_rolesBySID.TryGetValue(sid, out role)==false )
return null;
return role;
}
public Role __283(string uid)
{
if ( uid.Length==0 )
return null;
Role role;
if ( m_rolesByUID.TryGetValue(uid, out role)==false )
return null;
return role;
}
public bool __284()
{
if ( m_effects!=null )
{
for ( int i=0 ; i<m_effects.Length ; i++ )
{
if ( m_effects[i].__66()>0 )
{
for ( int j=0 ; j<m_effects[i].m_items.Length ; j++ )
{
if ( m_effects[i].m_items[j].m_model==EffectItem.MODEL.GRAIN )
return true;
}
}
}
}
return false;
}
public Effect __285()
{
if ( m_menuGame.__38() )
return m_menuEffect.cur;
if ( m_cinematicPlayer.__38() )
return m_cinematicPlayer.m_cinematic.m_effect.cur;
Scene scene = __291();
if ( scene && scene.m_effect.cur )
return scene.m_effect.cur;
return m_effect.cur;
}
public Effect __285(FXO fxo)
{
Effect effect = __285();
if ( effect && effect.__67(fxo)>0 )
return effect;
return null;
}
public Variable __286(ref string eval)
{
string name = G.__96(ref eval);
Variable var = __287(ref name);
if ( var )
return var;
if ( G.__104(ref name) )
{
Variable newVar = new Variable();
newVar.m_name = name;
newVar.m_value = "";
G.m_game.m_variables.Add(name, newVar);
return newVar;
}
return null;
}
public Variable __287(ref string name)
{
if ( name.Length==0 )
return null;
Variable variable;
if ( m_variables.TryGetValue(name, out variable)==false )
return null;
return variable;
}
public Variable __288(ref string name)
{
Variable var = __287(ref name);
if ( var )
return var;
if ( G.__104(ref name) )
{
Variable newVar = new Variable();
newVar.m_name = name;
newVar.m_value = "";
G.m_game.m_variables.Add(name, newVar);
return newVar;
}
return null;
}
public Variable __288(string name)
{
return __288(ref name);
}
public void __289(ref string str)
{
if ( str.Length==0 )
return;
if ( str.Contains("{LANG}") )
str = str.Replace("{LANG}", __207());
if ( str.Contains("{OS}") )
str = str.Replace("{OS}", __204());
if ( str.Contains("{TITLE}") )
str = str.Replace("{TITLE}", m_gameName);
if ( str.Contains("{NAME}") )
str = str.Replace("{NAME}", m_gameNameForFile);
if ( str.Contains("{VERSION}") )
str = str.Replace("{VERSION}", m_gameVersion);
if ( str.Contains("{AUTHOR}") )
str = str.Replace("{AUTHOR}", m_gameAuthor);
if ( str.Contains("{COPYRIGHT}") )
str = str.Replace("{COPYRIGHT}", m_gameCopyright);
}
public Player __290()
{
return m_players[0];
}
public Scene __291()
{
if ( m_menuScene )
return m_menuScene;
return m_iScene==-1 ? null : m_scenes[m_iScene];
}
public bool __292(ref string uid)
{
Scene scene = __291();
if ( scene==null )
return false;
return G.__148(ref scene.m_uid, ref uid);
}
public Player __293()
{
if ( m_menuScene )
return null;
return m_iPlayer==-1 ? null : m_players[m_iPlayer];
}
public bool __294(ref string uid)
{
if ( m_iPlayer==-1 )
return false;
return G.__148(ref m_players[m_iPlayer].m_uid, ref uid);
}
public SceneObj __295()
{
Player player = m_iPlayer==-1 ? null : m_players[m_iPlayer];
if ( player )
return player.m_sceneObj;
return null;
}
public Router __296()
{
if ( m_hasShowPathPlayer==false )
return null;
Player player = __293();
if ( player==null || player.m_sceneObj==null )
return null;
return player.__296();
}
public void __297()
{
if ( m_uiCursor.cur && m_uiCursor.cur!=m_uiCursor.init )
m_uiCursor.cur.End();
if ( m_uiCursorObj.cur && m_uiCursorObj.cur!=m_uiCursorObj.init )
m_uiCursorObj.cur.End();
if ( m_uiCursorDoor.cur && m_uiCursorDoor.cur!=m_uiCursorDoor.init )
m_uiCursorDoor.cur.End();
if ( m_uiCursorBusy.cur && m_uiCursorBusy.cur!=m_uiCursorBusy.init )
m_uiCursorBusy.cur.End();
m_uiCursor.__77();
m_uiCursorObj.__77();
m_uiCursorDoor.__77();
m_uiCursorBusy.__77();
m_cursorCustom = "";
}
public void __298(string uid)
{
Obj obj = __277(uid);
if ( obj==null )
{
__297();
return;
}
bool ok = false;
Asset asset = G.__95(G.m_pathGraphics);
if ( asset==null )
{
__297();
return;
}
Sprite sprite = obj.__453(0);
if ( sprite )
{
m_uiCursor.cur = sprite;
if ( sprite!=m_uiCursor.init )
{
sprite.__468(asset);
ok = true;
}
}
sprite = obj.__453(1);
if ( sprite )
{
m_uiCursorObj.cur = sprite;
if ( sprite!=m_uiCursorObj.init )
{
sprite.__468(asset);
ok = true;
}
}
sprite = obj.__453(2);
if ( sprite )
{
m_uiCursorDoor.cur = sprite;
if ( sprite!=m_uiCursorDoor.init )
{
sprite.__468(asset);
ok = true;
}
}
sprite = obj.__453(3);
if ( sprite )
{
m_uiCursorBusy.cur = sprite;
if ( sprite!=m_uiCursorBusy.init )
{
sprite.__468(asset);
ok = true;
}
}
asset.Close();
if ( ok==false )
{
__297();
return;
}
m_cursorCustom = obj.m_uid;
}
public Vec2 __299()
{
Vec2 pt = new Vec2(-1.0f, -1.0f);
if ( m_cursor==false )
return pt;
Scene scene = __291();
if ( scene==null )
return pt;
pt.x = m_cursorViewX;
pt.y = m_cursorViewY;
scene.__553(ref pt);
return pt;
}
public void __300()
{
m_fade = FADING.NONE;
m_fadeLock = false;
m_fadeColor = Color.black;
m_fadeOutDuration = 0.0f;
m_fadeWaitDuration = 0.0f;
m_fadeInDuration = 0.0f;
m_fadeTime = 0.0f;
m_fadeJump = false;
m_fadeJumpEnterExitEvents = true;
m_fadeJumpSwitchEvents = false;
m_fadeJumpPlayer = false;
m_fadeJumpScene = "";
m_fadeRoleBox = null;
m_fadeRoleBoxToken = 0;
m_fadeEvent = FADING.CLOSED;
}
public bool Jump(RoleBox roleBox, string scene, Color color, float durationOut = 0.0f, float durationWait = 0.0f, float durationIn = 0.0f)
{
if ( m_liveScene.Length>0 )
return false;
if ( m_menuScene )
return false;
if ( scene=="BACK" )
{
if ( m_iOldScene==-1 )
return false;
scene = m_scenes[m_iOldScene].m_uid;
}
if ( __274(scene)==null )
return false;
if ( m_fadeJump )
{
switch ( m_fade )
{
case FADING.CLOSING:
case FADING.CLOSED:
return false;
}
}
bool black = m_fade==FADING.BLACK;
__300();
m_fadeLock = true;
m_fadeJump = true;
m_fadeJumpScene = scene;
m_fadeRoleBox = roleBox;
m_fadeRoleBoxToken = roleBox==null ? 0 : roleBox.m_parent.m_token;
m_fadeOutDuration = G.__133(durationOut);
m_fadeWaitDuration = G.__133(durationWait);
m_fadeInDuration = G.__133(durationIn);
m_fadeColor = color;
m_fadeColor.a = black ? 1.0f : 0.0f;
m_fade = black ? FADING.CLOSED : FADING.CLOSING;
return true;
}
public void __301(string uid, bool callEnterExitEvents = true, bool callSwitchEvent = false)
{
if ( m_liveScene.Length>0 && m_liveScene!=uid )
return;
int index = __275(uid);
if ( m_iScene!=-1 )
{
Scene nextScene = index==-1 ? null : m_scenes[index];
if ( callEnterExitEvents )
m_scenes[m_iScene].__520();
m_scenes[m_iScene].End(nextScene);
}
m_brightness = 1.0f;
m_iOldScene = m_iScene;
m_iScene = index;
Scene scene = null;
if ( m_iScene!=-1 )
{
scene = m_scenes[m_iScene];
scene.__468();
}
Player player = __293();
if ( player )
{
if ( m_iScene==-1 )
player.m_sceneObj = null;
else
player.m_sceneObj = scene.__277(player.m_obj);
}
if ( scene && player && player.m_sceneObj )
__306(scene.m_uid, player.m_sceneObj.m_obj.m_uid, true);
if ( scene )
{
scene.__570();
if ( scene.m_role )
scene.m_role.Start();
}
G.m_game.m_menuDialog.__455();
if ( scene )
{
if ( callEnterExitEvents )
{
scene.__508("JUMP");
if ( m_iOldScene!=-1 && player.m_sceneObj )
{
Scene oldScene = m_scenes[m_iOldScene];
SceneCell cellTo = player.m_sceneObj.__635(oldScene);
if ( cellTo==null )
{
SceneCell cellFrom = player.m_sceneObj.__634(oldScene);
if ( cellFrom )
player.m_sceneObj.Move(cellFrom.__35(), cellFrom.__36());
}
else
{
SceneCellLink link = cellTo.__600(LINK.ENTER);
SceneCell cellFrom = player.m_sceneObj.__634(oldScene);
if ( cellFrom )
player.m_sceneObj.Move(cellFrom.__35(), cellFrom.__36());
Message msg = new Message();
msg.m_type = Message.WALK;
msg.m_anim = player.m_sceneObj.m_obj.__470(ref link.m_anim);
msg.m_dir = link.m_dir;
msg.m_state = Message.S_MOVE;
msg.m_enter = true;
msg.m_enterCell = cellTo.__392();
player.m_sceneObj.__643(cellTo.__35(), cellTo.__36(), msg, true);
}
}
}
if ( callSwitchEvent )
scene.__508("SWITCH");
}
if ( scene )
{
Cam cam = scene.__478();
if ( cam && cam.__33() )
{
cam.m_scrollSmoothInitialized = false;
cam.m_zoomSmoothInitialized = false;
}
scene.__571(true);
}
if ( player && player.m_sceneObj )
player.m_sceneObj.__601();
}
public void __302(string uid)
{
if ( m_customMenu.Length==0 )
return;
Scene scene = __274(uid);
if ( scene==m_menuScene )
return;
if ( m_menuScene )
{
m_menuScene.__520();
m_menuScene.End(scene);
}
m_menuScene = scene;
if ( m_menuScene==null )
return;
m_menuScene.__468();
m_menuScene.__571(true);
m_menuScene.__508("JUMP");
}
public void __303()
{
string liveScene = m_liveScene;
m_liveScene = "";
m_locked = false;
m_lockedDuration = -1.0f;
m_layout.m_bagLocked = false;
m_cursorVisible = true;
__301(liveScene);
}
public bool __304(string uid, bool callEvent = true)
{
if ( G.m_game.m_menuScene )
return false;
int index = __280(uid);
if ( index==-1 )
return false;
Scene scene = __291();
Scene lastScene = m_players[index].m_lastScene;
Obj obj = m_players[index].m_obj;
SceneObj sceneObj = null;
if ( lastScene )
sceneObj = lastScene.__277(obj);
else if ( scene )
sceneObj = scene.__277(obj);
Player prevPlayer = null;
if ( m_iPlayer!=-1 )
{
prevPlayer = m_players[m_iPlayer];
m_players[m_iPlayer].End();
m_players[m_iPlayer].m_lastScene = scene;
if ( m_players[m_iPlayer].m_sceneObj==null )
{
m_players[m_iPlayer].m_lastX = G.INVALIDCOORD;
m_players[m_iPlayer].m_lastY = G.INVALIDCOORD;
}
else
{
if ( m_players[m_iPlayer].m_sceneObj.__651() )
m_players[m_iPlayer].m_sceneObj.__645();
m_players[m_iPlayer].m_lastX = m_players[m_iPlayer].m_sceneObj.__35();
m_players[m_iPlayer].m_lastY = m_players[m_iPlayer].m_sceneObj.__36();
}
m_players[m_iPlayer].m_sceneObj = null;
}
m_iPlayer = index;
m_layout.m_bagOpened = false;
m_players[m_iPlayer].__468();
m_players[m_iPlayer].m_sceneObj = sceneObj;
if ( callEvent )
__329(prevPlayer);
if ( callEvent && sceneObj && sceneObj.m_scene!=scene )
{
__300();
m_fadeLock = true;
m_fadeJump = true;
m_fadeJumpEnterExitEvents = false;
m_fadeJumpSwitchEvents = true;
m_fadeJumpPlayer = true;
m_fadeJumpScene = sceneObj.m_scene.m_uid;
m_fadeOutDuration = 0.3f;
m_fadeInDuration = m_fadeOutDuration*0.5f;
m_fade = FADING.CLOSING;
}
else
{
if ( scene )
scene.__567(m_players[m_iPlayer]);
}
m_layout.__418();
return true;
}
public bool __305(string uidPlayer, string uidObj)
{
if ( m_menuScene )
return false;
SceneObj playerSceneObj = __295();
if ( playerSceneObj )
playerSceneObj.__645();
Player player = __279(uidPlayer);
if ( player==null )
return false;
player.m_obj = __277(uidObj);
player.m_sceneObj = null;
Scene scene = __291();
if ( scene && player==__293() && player.m_obj )
{
player.m_sceneObj = scene.__277(player.m_obj);
player.m_sceneObj.__645();
scene.__567(player);
}
return true;
}
public void __306(string uidScene, string uidObj, bool visible)
{
Obj obj = __277(uidObj);
if ( obj==null )
return;
if ( visible==false && m_cursorObj==obj )
m_cursorObj = null;
for ( int i=0 ; i<m_scenes.Length ; i++ )
{
SceneObj so = m_scenes[i].__277(obj);
if ( so )
so.m_visible.cur = false;
}
Scene scene = uidScene.Length==0 ? __291() : __274(uidScene);
if ( scene==null )
return;
SceneObj sceneObj = scene.__277(uidObj);
if ( sceneObj==null )
return;
if ( visible==false )
{
sceneObj.m_visible.modified = true;
return;
}
if ( obj.m_killed.cur )
return;
if ( obj.__293() && obj.__293().__477()==false )
return;
sceneObj.m_visible.Set(true);
if ( obj.__293() )
obj.__293().__480(obj);
}
public SceneObj __307(string uid)
{
string uidScene = "";
string uidObj = "";
G.__97(uid, ref uidScene, ref uidObj);
return __307(uidScene, uidObj);
}
public SceneObj __307(string uidScene, string uidObj)
{
if ( uidObj.Length==0 )
return null;
Scene scene = uidScene.Length==0 ? __291() : __274(uidScene);
if ( scene==null )
return null;
return scene.__277(uidObj);
}
public bool __308(string uid, int idSentence, RoleBox roleBox = null, int roleBoxSentence = 0)
{
__309();
Dialog dlg = __278(uid);
if ( dlg==null )
return false;
if ( roleBox )
m_menuDialog.__449(dlg, roleBox, roleBoxSentence);
m_menuDialog.m_sentence = dlg.__49(idSentence);
if ( m_menuDialog.m_sentence==null )
m_menuDialog.m_sentence = dlg.m_root;
if ( m_menuDialog.m_sentence==dlg.m_root && dlg.m_root.__56()==0 )
{
if ( dlg.m_entryPointIDs.Length>0 )
{
int id = dlg.m_entryPointIDs[G.__156(dlg.m_entryPointIDs.Length)];
m_menuDialog.m_sentence = dlg.__49(id);
}
}
m_menuDialog.m_callCount++;
m_menuDialog.m_dialog = dlg;
G.m_game.__322(RoleBoxEventEnterDialog.ID, dlg.m_uid);
m_menuDialog.Next(m_menuDialog.m_sentence, true, true);
return true;
}
public void __309()
{
m_cursorObj = null;
m_layout.m_bagOpened = false;
m_menuDialog.Quit();
}
public bool __310()
{
return m_menuDialog.__38();
}
public bool __311()
{
return m_menuDialog.__38() && m_menuDialog.m_isWaitingUser;
}
public string __312(string uid, int idSentence)
{
Dialog dlg = __278(uid);
if ( dlg==null )
return "";
Sentence sentence = dlg.__49(idSentence);
if ( sentence==null )
return "";
return sentence.m_text.Get();
}
public void __313(Obj obj)
{
Scene scene = __291();
if ( scene==null )
return;
SceneObj sceneObj = scene.__277(obj);
if ( sceneObj==null )
{
sceneObj = __295();
if ( sceneObj==null )
return;
}
Vec2 pt = sceneObj.__659();
Taker taker = new Taker();
taker.m_obj = obj;
taker.m_x = pt.x;
taker.m_y = pt.y;
m_takers.Add(taker);
}
public void __314()
{
m_dragging = false;
m_dragObj = null;
}
public bool __315(ref string uid, ref string nameOrTag)
{
Dialog dialog = __278(uid);
if ( dialog==null )
return false;
return dialog.__48(ref nameOrTag);
}
public bool __316(ref string uid, ref string nameOrTag)
{
Scene scene = __274(uid);
if ( scene==null )
return false;
return scene.__48(ref nameOrTag);
}
public bool __316(ref string nameOrTag)
{
if ( nameOrTag.Length==0 || m_iScene==-1 )
return false;
if ( nameOrTag[0]=='@' )
return G.__149(m_scenes[m_iScene].m_tags, nameOrTag.Substring(1));
return G.__148(ref m_scenes[m_iScene].m_uid, ref nameOrTag);
}
public bool __317(ref string nameOrTag)
{
if ( nameOrTag.Length==0 || m_iOldScene==-1 )
return false;
if ( nameOrTag[0]=='@' )
return G.__149(m_scenes[m_iOldScene].m_tags, nameOrTag.Substring(1));
return G.__148(ref m_scenes[m_iOldScene].m_uid, ref nameOrTag);
}
public bool __318(ref string uid, ref string nameOrTag)
{
Player player = __279(uid);
if ( player==null )
return false;
return player.__48(ref nameOrTag);
}
public bool __318(ref string nameOrTag)
{
if ( nameOrTag.Length==0 || m_iPlayer==-1 )
return false;
if ( nameOrTag[0]=='@' )
return G.__149(m_players[m_iPlayer].m_tags, nameOrTag.Substring(1));
return G.__148(ref m_players[m_iPlayer].m_uid, ref nameOrTag);
}
public bool __319(ref string uid, ref string nameOrTag)
{
if ( nameOrTag.Length==0 )
return false;
if ( nameOrTag[0]=='@' )
{
Scene scene = __291();
if ( scene==null )
return false;
SceneObj sceneObj = scene.__277(uid);
if ( sceneObj==null )
return false;
return sceneObj.__48(ref nameOrTag);
}
return G.__148(ref uid, ref nameOrTag);
}
public bool __320(ref string name, ref string nameOrTag)
{
if ( nameOrTag.Length==0 )
return false;
if ( nameOrTag[0]=='@' )
{
Scene scene = __291();
if ( scene==null )
return false;
SceneLabel label = scene.__536(name);
if ( label==null )
return false;
return G.__149(label.m_tags, nameOrTag.Substring(1));
}
return G.__148(ref name, ref nameOrTag);
}
public bool __321(ref string sub)
{
if ( sub=="ANY" )
{
if ( m_lastEventSubObj==null )
return false;
}
else if ( sub=="NONE" )
{
if ( m_lastEventSubObj )
return false;
}
else
{
if ( m_lastEventSubObj==null || G.__150(ref m_lastEventSubObj.m_name, ref sub) )
return false;
}
return true;
}
public int __322(int id, Variable result = null)
{
Event evt = new Event();
evt.id = id;
evt.result = result;
return __322(evt);
}
public int __322(int id, string p1, Variable result = null)
{
Event evt = new Event();
evt.id = id;
evt.p1 = p1;
evt.result = result;
return __322(evt);
}
public int __322(int id, string p1, string p2, Variable result = null)
{
Event evt = new Event();
evt.id = id;
evt.p1 = p1;
evt.p2 = p2;
evt.result = result;
return __322(evt);
}
public int __322(int id, string p1, string p2, string p3, Variable result = null)
{
Event evt = new Event();
evt.id = id;
evt.p1 = p1;
evt.p2 = p2;
evt.p3 = p3;
evt.result = result;
return __322(evt);
}
public int __322(int id, string p1, string p2, string p3, string p4, Variable result = null)
{
Event evt = new Event();
evt.id = id;
evt.p1 = p1;
evt.p2 = p2;
evt.p3 = p3;
evt.p4 = p4;
evt.result = result;
return __322(evt);
}
public int __322(Event evt)
{
if ( evt.result )
evt.result.m_value = "0";
evt.hasReturnValue = false;
evt.consume = false;
int count = 0;
for ( int i=0 ; i<m_roles.Length ; i++ )
{
if ( m_roles[i].m_running )
{
count += m_roles[i].__322(evt);
if ( evt.consume )
break;
}
}
return count;
}
public bool Task(string uid, string name, string value)
{
bool result = false;
if ( uid=="" )
{
for ( int i=0 ; i<m_roles.Length ; i++ )
{
if ( m_roles[i].m_running )
{
if ( m_roles[i].Task(ref name, ref value, ref result) )
break;
}
}
}
else
{
Role role = __283(uid);
if ( role && role.m_running )
role.Task(ref name, ref value, ref result);
}
return result;
}
public void __323(bool alt = false)
{
__322(RoleBoxEventInput.ID, alt ? "ALT" : "MAIN");
}
public void __324(Obj obj, SubObj subObj, bool byUser = true, bool skip = false)
{
m_sysVarSub.m_value = subObj==null ? "" : subObj.m_name;
m_lastEventSubObj = subObj;
m_lastEventByUser = byUser;
if ( skip )
__322(RoleBoxEventSelect.ID, obj.m_uid, "SKIP");
else
__322(RoleBoxEventSelect.ID, obj.m_uid, "WALK");
m_lastEventByUser = true;
m_lastEventSubObj = null;
}
public void __325(Obj objA, Obj objB, SubObj subObj, bool bothFromInventory)
{
m_sysVarSub.m_value = subObj==null ? "" : subObj.m_name;
m_lastEventSubObj = subObj;
__322(RoleBoxEventUse.ID, objA.m_uid, objB.m_uid, bothFromInventory ? "1" : "");
m_lastEventSubObj = null;
}
public void __326(Obj obj)
{
__322(RoleBoxEventDetach.ID, obj.m_uid);
}
public void __327(SceneObj sceneObj, SubObj subObj)
{
Variable var = new Variable();
m_sysVarSub.m_value = subObj==null ? "" : subObj.m_name;
m_lastEventSubObj = subObj;
__322(RoleBoxEventDrag.ID, sceneObj.m_obj.m_uid, var);
m_lastEventSubObj = null;
if ( var.m_value=="0" )
{
m_dragging = true;
m_dragObj = sceneObj;
}
else
__324(sceneObj.m_obj, subObj);
}
public void __328(Obj obj)
{
__322(RoleBoxEventDrop.ID, obj.m_uid);
}
public void __329(Player prevPlayer)
{
if ( prevPlayer==null )
__322(RoleBoxEventSwitch.ID, __293().m_uid);
else
__322(RoleBoxEventSwitch.ID, __293().m_uid, prevPlayer.m_uid);
}
public void __330(string name)
{
__322(RoleBoxEventSong.ID, name);
}
public void __331(Sentence sentence, int iPara)
{
m_sysVarChoice.m_value = sentence.m_choice ? "" : iPara.ToString();
__322(RoleBoxEventSay.ID, sentence.m_dialog.m_uid, sentence.m_sid.ToString(), sentence.m_visited.ToString(), m_sysVarChoice.m_value);
}
public void __332(Sentence sentence)
{
if ( sentence.m_unlocked==false )
{
sentence.m_unlocked = true;
__322(RoleBoxEventChoice.ID, sentence.m_dialog.m_uid, sentence.m_sid.ToString());
}
}
public void __333(string name, POPUPANSWER answer, string[] values)
{
switch ( name )
{
case "download":
{
if ( answer==POPUPANSWER.YES )
SavegameBehavior.m_instance.Download();
break;
}
case "play":
{
if ( answer==POPUPANSWER.YES )
{
__47(G.SAVEGAME_INDEX_SERVER);
m_menuGame.Close();
}
break;
}
case "upload0":
case "upload1":
case "upload2":
case "upload3":
case "upload4":
case "upload5":
{
if ( answer==POPUPANSWER.YES )
{
int index = G.__113(name.Substring(name.Length-1));
SavegameBehavior.m_instance.Upload(index);
}
break;
}
case "connect":
{
if ( answer==POPUPANSWER.YES )
SavegameBehavior.m_instance.Connect(values[0], values[1]);
break;
}
case "disconnect":
{
if ( answer==POPUPANSWER.YES )
{
m_savegameServerUser = "";
m_savegameServerPass = "";
__248();
m_menuGame.__458();
}
break;
}
case "signup":
{
if ( answer==POPUPANSWER.YES )
SavegameBehavior.m_instance.Signup(values[0]);
break;
}
case "reset":
{
if ( answer==POPUPANSWER.YES )
SavegameBehavior.m_instance.ResetPassword(values[0]);
break;
}
case "delete":
{
if ( answer==POPUPANSWER.YES && G.__148(ref values[0], ref m_savegameServerPass) )
SavegameBehavior.m_instance.DeleteAccount(m_savegameServerUser, m_savegameServerPass);
break;
}
}
}
public bool __42()
{
m_internalElapsed += Time.deltaTime;
if ( m_internalElapsed<0.008f )
{
m_elapsed = 0.0f;
return false;
}
m_elapsed = m_internalElapsed;
m_internalElapsed = 0.0f;
if ( m_elapsed>0.035f )
m_elapsed = 0.035f;
if ( m_configDebug )
{
if ( Input.GetKey(KeyCode.B) )
m_elapsed = 0.001f;
}
m_time += m_elapsed;
bool checkResolution = false;
#if UNITY_EDITOR
checkResolution = true;
#elif UNITY_STANDALONE_WIN
checkResolution = m_configRun;
#endif
if ( checkResolution )
{
if ( Screen.width!=(int)G.m_rcWindow.width || Screen.height!=(int)G.m_rcWindow.height )
{
G.m_rcWindow.width = Screen.width;
G.m_rcWindow.height = Screen.height;
G.m_windowRatio = G.m_rcWindow.width / G.m_rcWindow.height;
__219();
}
}
#if UNITY_STANDALONE_WIN
if ( G.m_game.m_configRun && G.m_game.m_configDebug )
{
if ( Input.GetKeyDown(KeyCode.R) )
{
Scene scene = __291();
if ( scene && scene.m_role )
IDE.Post(IDE_MSG.ROLE_OPEN, scene.m_role.m_sid);
}
}
#endif
AgePlugin.OnAppUpdate();
if ( m_locked && m_lockedDuration!=-1.0f )
{
m_lockedDuration -= m_elapsed;
if ( m_lockedDuration<=0.0f )
{
m_lockedDuration = -1.0f;
m_locked = false;
}
}
if ( m_autoSaveIconStartTime!=0.0f )
{
float duration = 2.0f;
m_autoSaveIconRatio = G.__138(G.Clamp((m_time-m_autoSaveIconStartTime)/duration));
if ( m_autoSaveIconRatio==1.0f )
m_autoSaveIconStartTime = 0.0f;
}
if ( m_autoSaveAsap )
{
if ( __253() )
{
__46(G.SAVEGAME_INDEX_AUTO);
m_autoSaveAsap = false;
if ( m_layout.m_spriteAutoSave && m_layout.Get(LAYOUT_CTRL.AUTOSAVE).m_active )
{
m_autoSaveIconStartTime = m_time;
m_autoSaveIconRatio = 0.0f;
}
}
}
if ( m_interludeLock )
{
if ( m_interludeLockDuration==0.0f || m_time-m_interludeLockTime<m_interludeLockDuration )
return true;
m_interludeLock = false;
}
if ( m_interludeAsap )
{
if ( __253() )
{
string param = m_interludeParam;
m_interludeAsap = false;
m_interludeParam = "";
AgePlugin.Interlude(param);
}
}
if ( m_configDebug && MessageBox.m_instance.__38()==false )
{
if ( Input.GetKeyDown(KeyCode.C) )
m_userCursorVisible = !m_userCursorVisible;
if ( Input.GetKeyDown(KeyCode.V) )
m_userSubtitleVisible = !m_userSubtitleVisible;
}
if ( Input.GetKeyDown(KeyCode.Escape) && MessageBox.m_instance.__38()==false )
__252(true);
for ( int i=0 ; i<G.CHANNEL_COUNT ; i++ )
m_songs[i].__42();
if ( m_currentVoice )
m_currentVoice.__42();
for ( int i=0 ; i<m_playingSounds.Count ; i++ )
{
if ( m_playingSounds[i].__42() )
i--;
}
__334();
m_input.__42();
if ( MessageBox.m_instance.__38() )
MessageBox.m_instance.__42();
else if ( m_menuGame.__38() )
m_menuGame.__42();
else if ( m_menuScene )
m_menuScene.__42();
else if ( m_cinematicPlayer.__38() )
m_cinematicPlayer.__42();
else if ( m_started )
{
m_scenario.__42(SCENARIO.UPDATE);
if ( m_liveScene.Length>0 )
__303();
m_menuDialog.__42();
if ( m_iScene!=-1 )
m_scenes[m_iScene].__42();
int repeatEventCount = 0;
for ( int i=0 ; i<m_roles.Length ; i++ )
repeatEventCount += m_roles[i].__501();
if ( repeatEventCount>0 )
__322(RoleBoxEventRepeat.ID);
for ( int i=0 ; i<m_roles.Length ; i++ )
m_roles[i].__42();
if ( m_iScene!=-1 )
m_scenes[m_iScene].__579();
}
return true;
}
public void __334()
{
Scene scene = __291();
Player player = __293();
switch ( m_fade )
{
case FADING.CLOSING:
{
m_fadeTime += m_elapsed;
if ( m_fadeTime>=m_fadeOutDuration )
m_fadeTime = m_fadeOutDuration;
if ( m_fadeOutDuration!=0.0f )
m_fadeColor.a = G.__138(m_fadeTime/m_fadeOutDuration);
else
m_fadeColor.a = 1.0f;
if ( m_fadeTime==m_fadeOutDuration )
{
m_fade++;
m_fadeTime = 0.0f;
}
break;
}
case FADING.CLOSED:
{
if ( m_fadeJump )
{
__301(m_fadeJumpScene, m_fadeJumpEnterExitEvents, m_fadeJumpSwitchEvents);
if ( m_fadeJumpPlayer && player )
{
if ( player.m_sceneObj )
{
if ( player.m_lastX!=G.INVALIDCOORD )
{
player.m_sceneObj.__613(player.m_lastX);
player.m_sceneObj.m_lightMeshChanged = true;
}
if ( player.m_lastY!=G.INVALIDCOORD )
{
player.m_sceneObj.__614(player.m_lastY);
player.m_sceneObj.m_lightMeshChanged = true;
}
}
if ( scene )
scene.__567(player);
}
}
if ( m_fadeEvent==m_fade )
{
if ( m_fadeRoleBox )
m_fadeRoleBox.__457(m_fadeRoleBoxToken);
m_fadeRoleBox = null;
m_fadeRoleBoxToken = 0;
}
m_fadeColor.a = 1.0f;
m_fade++;
m_fadeTime = 0.0f;
break;
}
case FADING.BLACK:
{
m_fadeTime += m_elapsed;
if ( m_fadeTime>=m_fadeWaitDuration )
m_fadeTime = m_fadeWaitDuration;
m_fadeColor.a = 1.0f;
if ( m_fadeTime==m_fadeWaitDuration )
{
m_fade++;
m_fadeTime = 0.0f;
}
break;
}
case FADING.OPENING:
{
m_fadeTime += m_elapsed;
if ( m_fadeTime>=m_fadeInDuration )
m_fade++;
else
m_fadeColor.a = 1.0f - G.__138(m_fadeTime/m_fadeInDuration);
break;
}
case FADING.OPENED:
{
if ( m_fadeEvent==m_fade )
{
if ( m_fadeRoleBox )
m_fadeRoleBox.__457(m_fadeRoleBoxToken);
m_fadeRoleBox = null;
m_fadeRoleBoxToken = 0;
}
m_fade = FADING.NONE;
break;
}
}
}
public void __335()
{
RenderTexture rt = G.__168();
if ( rt==null )
return;
G.__173(rt);
G.__171();
__43();
if ( m_licenseWarning )
{
float size = Mathf.Min(G.m_rcView.width, G.m_rcView.height)*0.1f;
Rect rc = new Rect(G.m_rcView.x, G.m_rcView.__437()-size, size, size);
G.m_graphics.__354(m_logoMaterial, ref rc);
}
if ( m_alert.Length>0 )
{
Police font = G.__194();
string[] lines = m_alert.Split('\n');
Vec2[] positions = new Vec2[lines.Length];
for ( int i=0 ; i<positions.Length ; i++ )
{
positions[i].x = G.m_rcViewUI.x + G.HUD_MARGIN;
positions[i].y = G.m_rcViewUI.y + G.HUD_MARGIN + i*font.__446();
}
FontDrawInfo info = new FontDrawInfo(lines, positions, ref G.m_colorWhite);
font.__70(ref info);
}
G.m_graphics.__351();
}
public void __43()
{
if ( m_menuGame.__38() )
{
m_menuGame.__43();
if ( MessageBox.m_instance.__38() )
MessageBox.m_instance.__43();
if ( m_menuGame.__461() && m_uiCursor.cur.m_material && G.m_controller!=CONTROLLER.TOUCH )
G.m_graphics.__354(m_uiCursor.cur.m_material, ref m_rcCursor);
return;
}
if ( m_interludeLock )
{
if ( AgePlugin.OnInterludeDraw() )
return;
}
if ( m_cinematicPlayer.__38() )
{
m_cinematicPlayer.__43();
return;
}
Scene scene = __291();
if ( scene )
scene.__43();
else
{
if ( m_configDebug )
G.__194().__492("No active scene", ref G.m_colorWhite, G.m_rcWindow.width);
}
if ( m_examine )
{
if ( m_examineColor.a!=1.0f )
G.FillScreen(m_examineColor);
float examineHeight = G.m_rcViewUI.height * m_examineScale;
float examineWidth = examineHeight * m_examine.m_rcTrim.width / m_examine.m_rcTrim.height;
Rect rcExamine = new Rect(G.m_rcViewUI.__439()-examineWidth*0.5f, G.m_rcViewUI.__440()-examineHeight*0.5f, examineWidth, examineHeight);
G.m_graphics.__354(m_examine.m_sprite.m_material, ref rcExamine);
}
if ( m_brightness!=1.0f && m_brightness>=0.0f )
G.FillScreen(new Color(0.0f, 0.0f, 0.0f, 1.0f-m_brightness));
bool isPlayable = __253();
m_layout.__43(isPlayable);
Player player = __293();
if ( player )
player.__43();
__336();
string legend = "";
bool hasLegend = false;
SceneLabel labelLegend = null;
Obj objLegend = null;
SubObj subObjLegend = null;
SceneObj sceneObjLegend = null;
Player playerLegend = null;
bool isDetach = false;
if ( m_cursor && __256()==false && m_dragObj==null )
{
bool active = scene && m_menuDialog.__38()==false;
do
{
if ( active )
{
SceneEntity entity;
SubObj sub;
if ( scene.__425(out entity, out sub, m_cursorViewX, m_cursorViewY, m_cursorObj==null, DRAG.ANY)==false )
break;
if ( entity==null )
{
if ( player )
{
bool empty;
objLegend = player.__431(m_cursorViewX, m_cursorViewY, out empty);
if ( objLegend )
{
legend = objLegend.m_title.Get();
hasLegend = true;
break;
}
}
}
else
{
if ( entity.__602() )
{
sceneObjLegend = (SceneObj)entity;
objLegend = sceneObjLegend.m_obj;
subObjLegend = sub;
legend = subObjLegend==null ? objLegend.m_title.Get() : subObjLegend.m_title.Get();
}
else
{
labelLegend = (SceneLabel)entity;
legend = labelLegend.m_title.Get();
}
hasLegend = true;
break;
}
}
if ( active )
{
playerLegend = m_layout.__426(m_cursorViewX, m_cursorViewY);
if ( playerLegend && playerLegend.m_obj )
{
legend = playerLegend.m_obj.m_title.Get();
hasLegend = true;
break;
}
}
if ( m_layout.__425(LAYOUT_CTRL.DETACH, m_cursorViewX, m_cursorViewY) )
{
isDetach = true;
break;
}
} while ( false );
}
Material cursorMaterial = null;
Rect cursorRectangle = m_rcCursor;
if ( m_cursorObj==null )
{
if ( G.m_controller==CONTROLLER.TOUCH )
cursorMaterial = null;
else if ( isPlayable==false && m_menuDialog.__38()==false )
{
if ( m_examine==null )
cursorMaterial = m_uiCursorBusy.cur ? m_uiCursorBusy.cur.m_material : m_uiCursor.cur.m_material;
else
cursorMaterial = m_uiCursor.cur.m_material;
}
else if ( m_dragObj )
cursorMaterial = null;
else if ( labelLegend )
{
if ( labelLegend.m_door )
cursorMaterial = m_uiCursorDoor.cur ? m_uiCursorDoor.cur.m_material : m_uiCursor.cur.m_material;
else
cursorMaterial = m_uiCursorObj.cur ? m_uiCursorObj.cur.m_material : m_uiCursor.cur.m_material;
}
else if ( objLegend )
{
if ( sceneObjLegend && sceneObjLegend.m_door )
cursorMaterial = m_uiCursorDoor.cur ? m_uiCursorDoor.cur.m_material : m_uiCursor.cur.m_material;
else
cursorMaterial = m_uiCursorObj.cur ? m_uiCursorObj.cur.m_material : m_uiCursor.cur.m_material;
}
else
cursorMaterial = m_uiCursor.cur.m_material;
}
else if ( m_cursor )
{
bool useAltIcon = false;
if ( hasLegend==false )
legend = Localization.m_wordUse.Get() + " \xAB\x01" + m_cursorObj.m_title.Get() + "\x01\xBB " + Localization.m_wordWith.Get();
else if ( objLegend && objLegend!=m_cursorObj )
{
useAltIcon = true;
legend = Localization.m_wordUse.Get() + " \xAB\x01" + m_cursorObj.m_title.Get() + "\x01\xBB " + Localization.m_wordWith.Get() + " \xAB\x01" + legend + "\x01\xBB";
}
else if ( labelLegend )
{
useAltIcon = true;
legend = Localization.m_wordUse.Get() + " \xAB\x01" + m_cursorObj.m_title.Get() + "\x01\xBB " + Localization.m_wordWith.Get() + " \xAB\x01" + legend + "\x01\xBB";
}
else if ( playerLegend )
{
useAltIcon = true;
legend = Localization.m_wordUse.Get() + " \xAB\x01" + m_cursorObj.m_title.Get() + "\x01\xBB " + Localization.m_wordWith.Get() + " \xAB\x01" + legend + "\x01\xBB";
}
else if ( isDetach )
{
useAltIcon = true;
legend = Localization.m_wordDetach.Get() + " \xAB\x01" + m_cursorObj.m_title.Get() + "\x01\xBB";
}
else
legend = Localization.m_wordUse.Get() + " \xAB\x01" + m_cursorObj.m_title.Get() + "\x01\xBB " + Localization.m_wordWith.Get();
Sprite icon = m_cursorObj.__453();
cursorMaterial = icon.m_material;
cursorRectangle = icon.m_rc;
if ( useAltIcon )
cursorRectangle.__441(ref m_timeAltIcon, ref m_iRenderAltIcon);
}
if ( m_userCursorVisible && m_cursorVisible && cursorMaterial && m_cursorObj )
G.m_graphics.__354(cursorMaterial, ref cursorRectangle);
G.m_graphics.__352(FXO.SCENE_HUD);
if ( m_layout.Get(LAYOUT_CTRL.LEGEND).__427() )
{
const float legendDuration = 0.4f;
bool drawLegend = false;
Color legendColor;
if ( (labelLegend && labelLegend.m_door) || (sceneObjLegend && sceneObjLegend.m_door) )
legendColor = m_colorTextDoor;
else
legendColor = m_colorText;
if ( isPlayable==false )
{
m_legendTime = -1.0f;
}
else if ( legend.Length>0 )
{
drawLegend = true;
if ( G.m_controller==CONTROLLER.TOUCH )
{
m_legendTime = 0.0f;
m_legendLastText = legend;
m_legendLastColor = legendColor;
}
}
else if ( m_legendTime!=-1.0f )
{
drawLegend = true;
legend = m_legendLastText;
legendColor = m_legendLastColor;
m_legendTime += m_elapsed;
if ( G.__135(G.Clamp(m_legendTime/legendDuration), 1.0f, 0.0f)==0.0f )
m_legendTime = -1.0f;
}
if ( drawLegend )
__215().__493(legend, legendColor);
}
if ( scene )
scene.__585();
m_menuDialog.__43();
G.m_graphics.__352(FXO.SCENE_TEXT);
if ( m_timeline )
{
float fade = m_timeline.m_camFade;
if ( fade!=1.0f )
G.FillScreen(new Color(0.0f, 0.0f, 0.0f, fade));
}
if ( m_brightness!=-1.0f && m_brightness<=0.0f )
G.FillScreen(new Color(0.0f, 0.0f, 0.0f, 1.0f+m_brightness));
if ( m_fade!=FADING.NONE )
G.FillScreen(m_fadeColor);
for ( int i=0 ; i<G.CHANNEL_COUNT ; i++ )
{
Song song = m_songs[i];
if ( song.m_fadeMode!=0 )
{
if ( song.m_fadeMode==-1 )
{
song.m_fadeTime += m_elapsed;
if ( song.m_fadeTime>=song.m_fadeDuration )
song.m_fadeTime = song.m_fadeDuration;
float scale = song.m_fadeTime/song.m_fadeDuration;
song.m_sound.__986(song.m_fadeVolEnd*scale);
song.m_crossfade.__986(song.m_fadeVolBeg*(1.0f-scale));
if ( song.m_fadeTime==song.m_fadeDuration )
{
song.m_crossfade.Stop();
song.m_fadeMode = 0;
}
}
else
{
if ( song.m_current==null )
song.m_fadeMode = 0;
else
{
song.m_fadeTime += m_elapsed;
if ( song.m_fadeTime>=song.m_fadeDuration )
song.m_fadeTime = song.m_fadeDuration;
float scale = song.m_fadeTime/song.m_fadeDuration;
float vol = song.m_fadeVolBeg + (song.m_fadeVolEnd-song.m_fadeVolBeg)*scale;
song.m_current.__986(vol);
if ( song.m_fadeTime==song.m_fadeDuration )
{
if ( song.m_fadeMode==1 )
{
song.m_fadeMode = 0;
song.m_current.Stop();
}
else
song.m_fadeMode = 0;
}
}
}
}
}
if ( m_autoSaveIconStartTime!=0.0f )
{
float opacity = 1.0f;
if ( m_autoSaveIconRatio<0.1f )
opacity = m_autoSaveIconRatio/0.1f;
else if ( m_autoSaveIconRatio>0.9f )
opacity = 1.0f - (m_autoSaveIconRatio-0.9f)/0.1f;
G.m_graphics.__354(m_layout.m_spriteAutoSave.m_material, ref m_layout.Get(LAYOUT_CTRL.AUTOSAVE).m_rcView, opacity);
}
if ( MessageBox.m_instance.__38() )
{
MessageBox.m_instance.__43();
if ( m_uiCursor.cur.m_material && G.m_controller!=CONTROLLER.TOUCH )
G.m_graphics.__354(m_uiCursor.cur.m_material, ref m_rcCursor);
return;
}
if ( __255() && m_timeline.m_skip && G.m_game.m_input.m_isDown )
G.m_graphics.__366();
if ( m_userCursorVisible && m_cursorVisible && cursorMaterial && m_cursorObj==null && __255()==false )
G.m_graphics.__354(cursorMaterial, ref cursorRectangle);
__337();
}
public void __336()
{
for ( int iTaker=0 ; iTaker<m_takers.Count ; iTaker++ )
{
bool toRemove = false;
Taker taker = m_takers[iTaker];
Sprite icon = taker.m_obj.__453();
if ( icon.m_texture==null )
{
toRemove = true;
}
else if ( taker.m_time>0.0f )
{
const float duration = 2.0f;
float ratio = G.__138(G.Clamp((m_time-taker.m_time)/duration));
float scale = 0.5f + Mathf.Sin(ratio*G.RAD_180)*0.75f;
float opacity = 0.9f - ratio*0.9f;
float x = taker.m_x + (m_layout.m_takerX-taker.m_x)*ratio;
float y = taker.m_y + (m_layout.m_takerY-taker.m_y)*ratio;
float width = m_cursorSize*2.0f*scale;
float height = m_cursorSize*2.0f*scale;
Rect rc = new Rect(x-width*0.5f, y-height*0.5f, width, height);
G.m_graphics.__354(icon.m_material, ref rc, opacity);
if ( ratio==1.0f )
toRemove = true;
}
else
{
if ( iTaker==0 )
{
if ( taker.m_time==0.0f )
taker.m_time = -m_time;
else if ( m_time-(-taker.m_time)>0.1f )
taker.m_time = m_time;
}
else
{
if ( (m_time-m_takers[iTaker-1].m_time)>(iTaker*0.5f) )
taker.m_time = m_time;
}
}
if ( toRemove )
{
m_takers.RemoveAt(iTaker);
iTaker--;
}
}
}
public void __337()
{
if ( m_input.m_hasMagnify==false )
return;
LayoutCtrl magnify = m_layout.Get(LAYOUT_CTRL.MAGNIFY);
Rect rcSrc;
rcSrc.width = magnify.m_rcView.width * magnify.m_invZoom;
rcSrc.height = magnify.m_rcView.height * magnify.m_invZoom;
rcSrc.x = G.m_game.m_cursorViewX - rcSrc.width*0.5f;
rcSrc.y = G.m_game.m_cursorViewY - rcSrc.height*0.5f;
RenderTexture rt = G.__168((int)magnify.m_rcView.width, (int)magnify.m_rcView.height);
RenderTexture old = G.__173(rt);
G.__172();
G.m_graphics.__355(old, ref rcSrc);
G.__173(old);
Rect rcTrg = magnify.m_rcView;
G.m_materialTexture32.mainTexture = rt;
G.m_graphics.__354(G.m_materialTexture32, ref rcTrg);
G.m_materialTexture32.mainTexture = null;
G.__170(rt);
if ( G.m_game.m_layout.m_spriteMagnify )
{
rcTrg.__442(1.02f);
G.m_graphics.__354(G.m_game.m_layout.m_spriteMagnify.m_material, ref rcTrg);
}
}
/*public void __337()
{
if ( G.m_game.m_input.m_hasMagnify==false )
return;
float texSize = G.m_game.m_portrait ? G.m_rcView.height*G.MAGNIFY_SIZE : G.m_rcView.width*G.MAGNIFY_SIZE;
float size = texSize * G.MAGNIFY_ZOOM;
Rect rcSrc;
rcSrc.width = size;
rcSrc.height = size;
rcSrc.x = G.m_game.m_cursorViewX - size*0.5f;
rcSrc.y = G.m_game.m_cursorViewY - size*0.5f;
RenderTexture rt = G.__168((int)texSize, (int)texSize);
RenderTexture old = G.__173(rt);
G.__172();
G.m_graphics.__355(old, ref rcSrc);
G.__173(old);
Rect rcTrg;
rcTrg.x = 0.0f;
rcTrg.y = 0.0f;
rcTrg.width = texSize;
rcTrg.height = texSize;
G.m_materialTexture32.mainTexture = rt;
G.m_graphics.__354(G.m_materialTexture32, ref rcTrg);
G.m_materialTexture32.mainTexture = null;
G.__170(rt);
if ( G.m_game.m_layout.m_spriteMagnify )
{
rcTrg.__442(1.02f);
G.m_graphics.__354(G.m_game.m_layout.m_spriteMagnify.m_material, ref rcTrg);
}
}*/
public void __338(bool pause)
{
if ( pause )
G.__179();
}
public void __339()
{
#if UNITY_STANDALONE_WIN
if ( m_configRun )
IDE.Post(IDE_MSG.RESET);
#endif
}
}
