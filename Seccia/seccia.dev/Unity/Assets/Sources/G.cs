using System;
using System.Collections.Generic;
using UnityEngine;
using Ionic.Zlib;
using System.Globalization;
#if UNITY_EDITOR
using System.Threading;
#endif
public enum CONTROLLER
{
MOUSE,
TOUCH,
}
public enum SHADER
{
BCS,
BEACON,
BLUR,
BOKEH,
BRUSH,
COLORIZATION,
CURVE,
ERASE,
FLAME,
FILTER,
FIRE,
GRAIN,
GRAYSCALE,
LIGHTAMBIENT,
LIGHTAMBIENTFINAL,
LIGHTDIFFUSE,
LIGHTFINAL,
LUT,
MONOCHROME,
PARALLAX,
RAIN,
SOFTLIGHT,
TEXT,
TEXTURE24,
TEXTURE32,
COUNT,
}
public enum COMPUTE
{
BLUR,
BOKEH,
COUNT,
}
public enum FXO
{
MA,
MB,
MC,
MD,
BACK,
FA,
FB,
FC,
FD,
SCENE_TEXT,
SCENE_HUD,
SCENE_MA,
SCENE_MB,
SCENE_MC,
SCENE_MD,
SCENE_OBJECT,
SCENE_BACK,
SCENE_FA,
SCENE_FB,
SCENE_FC,
SCENE_FD,
CINEMATIC_TEXT,
CINEMATIC_IMAGE,
MENU_TEXT,
MENU_HUD,
MENU_BACK,
COUNT,
}
public enum SUBTITLE
{
NONE,
SLOW,
FAST,
MANUAL,
}
public enum QUALITY
{
HIGH,
LOW,
}
public enum PROFILE
{
NONE,
NEUTRAL,
ANGRY,
ARROGANT,
CALM,
HYSTERIC,
MODEST,
NERVOUS,
PROUD,
SHY,
}
public enum RANDOM
{
NONE,
ANIM,
LOOP,
FRAME,
};
public enum PACKGROUP
{
OBJ,
SCN,
FLY,
FLY_LOW,
AIR,
AIR_LOW,
}
public enum BLEND
{
DEFAULT,
ADDITION,
SOFTLIGHT,
}
public enum ANIMATION
{
NONE,
FLAME,
}
public enum RATIOLESS
{
SCROLL,
LETTERBOX,
LETTERBOX_TOP,
LETTERBOX_BOTTOM,
CROP,
CROP_LEFT,
CROP_RIGHT,
}
public enum RATIOGREAT
{
LETTERBOX,
LETTERBOX_LEFT,
LETTERBOX_RIGHT,
CROP,
CROP_TOP,
CROP_BOTTOM,
}
public enum POPUP
{
DEFAULT,
QUESTION,
EMAIL,
PASSWORD,
LOGIN,
SIGNUP,
}
public enum POPUPANSWER
{
OK,
YES,
NO,
}
public enum ALIGN
{
LEFT	= 0x00,
CENTER	= 0x01,
RIGHT	= 0x02,
JUSTIFY	= 0x03,
TOP		= 0x00,
MIDDLE	= 0x10,
BOTTOM	= 0x20,
}
public enum READING
{
NEAR,
CENTER,
FAR,
}
public enum PLACEMENT
{
HA,
HB,
MA,
MB,
MC,
MD,
OBJECT,
BACK,
FA,
FB,
FC,
FD,
CLEAR,
COUNT,
}
public enum DRAG
{
NONE,
SOURCE,
TARGET,
BOTH,
ANY,
}
public enum CAMERA
{
OFF,
AUTO,
AUTO_POS,
AUTO_SCALE,
CURSOR,
MANUAL,
TALK,
TRACK,
TIMELINE,
}
public enum FADING
{
NONE,
CLOSING,
CLOSED,
BLACK,
OPENING,
OPENED,
}
public enum LINK
{
ENTER,
SELECT,
LABEL,
USE,
USELABEL,
DETACH,
COUNT,
}
public enum LAYOUT_CTRL
{
AUTOSAVE,
BAG,
DETACH,
ITEMS,
LEGEND,
MAGNIFY,
MENU,
PLAYERS,
SHUTUP,
USER1,
USER2,
USER3,
USER4,
USER5,
USER6,
USER7,
USER8,
COUNT,
}
public enum LAYOUT_ALIGN
{
NEAR,
CENTER,
FAR,
SIZE,
}
public enum LAYOUT_MODE
{
NONE,
ALWAYS,
CLICK,
}
public enum LAYOUT_LINK
{
DISABLED,
ENABLED,
INVERTED,
}
public enum LAYOUT_CELL
{
NONE,
HORZ,
VERT,
GRID,
}
public enum CINEMATICSKIP
{
NONE,
CLICK,
DELAY,
}
public enum ENTITY
{
NONE,
OBJ,
LABEL,
BOKEH,
STILL,
CURSOR,
}
public enum TEXTURESCALE
{
FULL,
HALF,
QUARTER,
EIGHTH,
}
public enum SCENARIO
{
DEBUG_START,
DEBUG_UPDATE,
UPDATE,
}
public class Event
{
public int id = 0;
public string p1;
public string p2;
public string p3;
public string p4;
public bool hasReturnValue = false;
public bool consume = false;
public Variable result = null;
}
public struct Serial<T>
{
public T init;
public T cur;
public bool modified;
public void Init(T val)
{
init = val;
cur = val;
modified = false;
}
public void Reset()
{
cur = init;
modified = false;
}
public void __77()
{
cur = init;
modified = true;
}
public void Set(T val)
{
if ( modified )
cur = val;
else if ( Comparer<T>.Default.Compare(init, val)!=0 )
{
cur = val;
modified = true;
}
}
}
public static class G
{
public const float PI = 3.14159265f;
public const float RAD_0 = 0.0f;
public const float RAD_1 = 0.01745329f;
public const float RAD_30 = 0.52359878f;
public const float RAD_45 = 0.78539816f;
public const float RAD_90 = 1.57079633f;
public const float RAD_135 = 2.35619449f;
public const float RAD_180 = 3.14159265f;
public const float RAD_225 = 3.92699082f;
public const float RAD_270 = 4.71238898f;
public const float RAD_315 = 5.49778714f;
public const float RAD_360 = 6.28318531f;
public const float RAD_120 = 2.09439510f;
public const float RAD_240 = 4.18879020f;
public const float RAD_TO_DEG = 57.29577951f;
public const float DEG_TO_RAD = 0.01745329f;
public const float PATH_GRID_CELLSIZE = 16.0f;
public const float PATH_GRID_CELLSIZE_HALF = 8.0f;
public const int PATH_GRID_COUNT = 8;
public const int BOKEH_DIVIDER = 8;
public const float BOKEH_SIZE = (int)BOKEH_DIVIDER;
public const int BOKEH_MAX = 512;
public const float BOKEH_SHAPE = 256.0f;
public const int SAVEGAME_COUNT = 6;
public const int SAVEGAME_INDEX_AUTO = 0;
public const int SAVEGAME_INDEX_SERVER = 5;
public const int SAVEGAME_VERSION = 1;
public const int SAVEGAME_SERVER_VERSION = 1;
public const int PROFILE_FRAME_COUNT = 7;
public const float HUD_MARGIN = 10.0f;
public const int CHANNEL_COUNT = 4;
public const int ICON_COUNT = 4;
public const int MASK_COUNT = 4;
public const int FAR_COUNT = 4;
public const int CURVE_COUNT = 4;
public const float INVALIDCOORD = 1000000.0f;
public const float ETERNAL = -1.0f;
public const float SUBTITLE_MARGIN = 24.0f;
public const float SUBTITLE_WIDTH = 0.66f;
public const float SKIP_DELAY = 1.2f;
public const float MAGNIFY_DURATION = 0.5f;
public static int[] m_date = new int[3];
public static bool m_mouseCaps = true;
public static bool m_touchCaps = false;
public static CONTROLLER m_controller = CONTROLLER.MOUSE;
public static bool m_androidFailed = false;
public static bool m_isInitialized = false;
public static bool m_isGameLoaded = false;
public static int m_nativeScreenWidth;
public static int m_nativeScreenHeight;
public static bool m_nativeScreenFull;
public static Rect m_rcWindow;
public static float m_windowRatio;
public static Rect m_rcClient;
public static float m_clientRatio;
public static Rect m_rcGame;
public static float m_gameRatio;
public static float m_gameRatioInv;
public static Rect m_rcView;
public static float m_viewRatio;
public static Rect m_rcViewUI;
public static bool m_screenLessGame;
public static int m_bokehWidth;
public static int m_bokehHeight;
public static string m_audioExtension;
public static string m_videoExtension;
public static string m_folderContent;
public static string m_folderContentSongs;
public static string m_folderContentVideos;
public static string m_folderContentVoices;
public static string m_folderSavegame;
public static string m_pathGame;
public static string m_pathGraphics;
public static string m_pathSounds;
public static string m_pathConfig;
public static string m_pathSettings;
public static string m_pathUserLog;
public static string m_pathMd5;
public static string m_webURL;
#if UNITY_WEBGL
public static List<Sound> m_downloadedSongs = new List<Sound>();
#elif UNITY_ANDROID
public static string m_obbPath;
public static int m_obbOffset;
public static Dictionary<string, int> m_obbMap = new Dictionary<string,int>();
public static List<int> m_obbOffsets = new List<int>();
public static List<int> m_obbSizes = new List<int>();
#endif
public static System.Random m_random;
public static Color m_colorClear = new Color(0.0f, 0.0f, 0.0f, 0.0f);
public static Color m_colorBlack = new Color(0.0f, 0.0f, 0.0f, 1.0f);
public static Color m_colorWhite = new Color(1.0f, 1.0f, 1.0f, 1.0f);
public static Color m_colorGray = new Color(0.5f, 0.5f, 0.5f, 1.0f);
public static Sprite m_spriteLoading = null;
public static Sprite m_spriteSkip = null;
public static Sprite m_spriteNoise = null;
public static Sprite m_spriteFlame = null;
public static bool m_webReady = false;
public static RenderTexture m_mainRT = null;
public static GraphicsDevice m_graphics = null;
public static Police m_font = null;
public static Shader[] m_shaders = null;
public static Material m_materialBlur = null;
public static Material m_materialBokeh = null;
public static Material m_materialBrush = null;
public static Material m_materialErase = null;
public static Material m_materialLightAmbient = null;
public static Material m_materialLightAmbientFinal = null;
public static Material m_materialLightDiffuse = null;
public static Material m_materialLightFinal = null;
public static Material m_materialSoftLight = null;
public static Material m_materialTexture24 = null;
public static Material m_materialTexture32 = null;
public static Texture2D m_textureGrain = null;
public static Color[] m_colors = null;
public static ComputeShader[] m_computeShaders = null;
public static int m_kernelBlurHorz;
public static int m_kernelBlurVert;
public static AgeGame m_game = null;
public static byte[] m_yek = new byte[128];
public static void __78(int width, int height, bool fullscreen)
{
m_date[0] = DateTime.Now.Year;
m_date[1] = DateTime.Now.Month;
m_date[2] = DateTime.Now.Day;
Input.simulateMouseWithTouches = false;
m_mouseCaps = Input.mousePresent || __86();
m_touchCaps = Input.touchSupported;
m_controller = CONTROLLER.MOUSE;
Screen.sleepTimeout = SleepTimeout.NeverSleep;
#if UNITY_STANDALONE_WIN
QualitySettings.vSyncCount = 1;
#elif UNITY_IOS
Application.targetFrameRate = (int)Math.Round(Screen.currentResolution.refreshRateRatio.value);
#endif
m_random = new System.Random(DateTime.Now.Millisecond+DateTime.Now.Second*1000+DateTime.Now.Minute*60000+DateTime.Now.Hour*3600000);
m_nativeScreenWidth = width;
m_nativeScreenHeight = height;
m_nativeScreenFull = fullscreen;
m_rcWindow = new Rect(0.0f, 0.0f, width, height);
m_windowRatio = width/(float)height;
m_rcClient = m_rcWindow;
m_clientRatio = m_windowRatio;
m_rcGame = Rect.Zero;
m_gameRatio = 1.0f;
m_rcView = m_rcWindow;
m_viewRatio = m_windowRatio;
m_rcViewUI = m_rcView;
m_screenLessGame = true;
#if UNITY_STANDALONE_WIN
m_audioExtension = ".ogg";
string folder = Application.dataPath + "/../";
#if UNITY_EDITOR
folder = "s:/seccia.dev/src/setup/demo/pieman/BINARIES/play_windows/";
folder = "s:/seccia.dev/src/Test/pieman15 - copy/BINARIES/play_windows/";
#endif
m_folderContent = folder + "Content/";
m_folderContentSongs = "file://" + m_folderContent + "SONGS/";
m_folderContentVideos = "file://" + m_folderContent + "VIDEOS/";
m_folderContentVoices = "file://" + m_folderContent + "VOICES/";
m_folderSavegame = folder;
m_pathGame = m_folderContent + "Game.dat";
m_pathGraphics = m_folderContent + "Graphics.dat";
m_pathSounds = m_folderContent + "Sounds.dat";
m_pathConfig = m_folderContent + "Config.dat";
m_pathSettings = m_folderSavegame + "settings.sav";
m_pathUserLog = m_folderSavegame + "userlog.txt";
#elif UNITY_WEBGL
m_audioExtension = ".mp3";
m_folderContent = m_webURL;
m_folderContentSongs = m_folderContent + "SONGS/";
m_folderContentVideos = m_folderContent + "VIDEOS/";
m_folderContentVoices = m_folderContent + "VOICES/";
m_folderSavegame = Application.persistentDataPath + "/";
m_pathGame = m_folderContent + "Game.dat";
m_pathGraphics = m_folderContent + "Graphics.dat";
m_pathSounds = m_folderContent + "Sounds.dat";
m_pathConfig = m_folderContent + "Config.dat";
m_pathSettings = "";
m_pathUserLog = "";
#elif UNITY_ANDROID
m_audioExtension = ".mp3";
m_folderContent = Application.persistentDataPath + "/";
m_folderContentSongs = "file://" + m_folderContent + "SONGS/";
m_folderContentVideos = "file://" + m_folderContent + "VIDEOS/";
m_folderContentVoices = "file://" + m_folderContent + "VOICES/";
m_folderSavegame = Application.persistentDataPath + "/";
m_pathGame = "Game.dat";
m_pathGraphics = "Graphics.dat";
m_pathSounds = "Sounds.dat";
m_pathConfig = "Config.dat";
m_pathSettings = m_folderSavegame + "settings.sav";
m_pathUserLog = m_folderSavegame + "userlog.txt";
m_pathMd5 = m_folderSavegame + "md5.bin";
#elif UNITY_IOS
m_audioExtension = ".mp3";
m_folderContent = Application.dataPath + "/Content/";
m_folderContentSongs = "file://" + m_folderContent + "SONGS/";
m_folderContentVideos = "file://" + m_folderContent + "VIDEOS/";
m_folderContentVoices = "file://" + m_folderContent + "VOICES/";
m_folderSavegame = Application.persistentDataPath + "/";
m_pathGame = m_folderContent + "Game.dat";
m_pathGraphics = m_folderContent + "Graphics.dat";
m_pathSounds = m_folderContent + "Sounds.dat";
m_pathConfig = m_folderContent + "Config.dat";
m_pathSettings = m_folderSavegame + "settings.sav";
m_pathUserLog = m_folderSavegame + "userlog.txt";
#endif
#if !UNITY_WEBGL
m_computeShaders = new ComputeShader[(int)COMPUTE.COUNT];
m_computeShaders[(int)COMPUTE.BLUR] = Resources.Load<ComputeShader>("Compute/Blur");
m_computeShaders[(int)COMPUTE.BOKEH] = Resources.Load<ComputeShader>("Compute/Bokeh");
m_kernelBlurHorz = __164(COMPUTE.BLUR).FindKernel("HorizontalBlur");
m_kernelBlurVert = __164(COMPUTE.BLUR).FindKernel("VerticalBlur");
#endif
m_shaders = new Shader[(int)SHADER.COUNT];
m_shaders[(int)SHADER.BCS] = Shader.Find("AGE/BCS");
m_shaders[(int)SHADER.BEACON] = Shader.Find("AGE/Beacon");
m_shaders[(int)SHADER.BLUR] = Shader.Find("AGE/Blur");
m_shaders[(int)SHADER.BOKEH] = Shader.Find("AGE/Bokeh");
m_shaders[(int)SHADER.BRUSH] = Shader.Find("AGE/Brush");
m_shaders[(int)SHADER.COLORIZATION] = Shader.Find("AGE/Colorization");
m_shaders[(int)SHADER.CURVE] = Shader.Find("AGE/Curve");
m_shaders[(int)SHADER.ERASE] = Shader.Find("AGE/Erase");
m_shaders[(int)SHADER.FLAME] = Shader.Find("AGE/Flame");
m_shaders[(int)SHADER.FILTER] = Shader.Find("AGE/Filter");
m_shaders[(int)SHADER.FIRE] = Shader.Find("AGE/Fire");
m_shaders[(int)SHADER.GRAIN] = Shader.Find("AGE/Grain");
m_shaders[(int)SHADER.GRAYSCALE] = Shader.Find("AGE/Grayscale");
m_shaders[(int)SHADER.LIGHTAMBIENT] = Shader.Find("AGE/LightAmbient");
m_shaders[(int)SHADER.LIGHTAMBIENTFINAL] = Shader.Find("AGE/LightAmbientFinal");
m_shaders[(int)SHADER.LIGHTDIFFUSE] = Shader.Find("AGE/LightDiffuse");
m_shaders[(int)SHADER.LIGHTFINAL] = Shader.Find("AGE/LightFinal");
m_shaders[(int)SHADER.LUT] = Shader.Find("AGE/LUT");
m_shaders[(int)SHADER.MONOCHROME] = Shader.Find("AGE/Monochrome");
m_shaders[(int)SHADER.PARALLAX] = Shader.Find("AGE/Parallax");
m_shaders[(int)SHADER.RAIN] = Shader.Find("AGE/Rain");
m_shaders[(int)SHADER.SOFTLIGHT] = Shader.Find("AGE/SoftLight");
m_shaders[(int)SHADER.TEXT] = Shader.Find("AGE/Text");
m_shaders[(int)SHADER.TEXTURE24] = Shader.Find("AGE/Texture24");
m_shaders[(int)SHADER.TEXTURE32] = Shader.Find("AGE/Texture32");
m_spriteLoading = new Sprite();
m_spriteLoading.m_texture = (Texture2D)Resources.Load("Textures/loading", typeof(Texture2D));
m_spriteLoading.m_material = __165(SHADER.TEXTURE32, m_spriteLoading.m_texture);
m_spriteSkip = new Sprite();
m_spriteSkip.m_texture = (Texture2D)Resources.Load("Textures/skip", typeof(Texture2D));
m_spriteSkip.m_material = __165(SHADER.TEXTURE32, m_spriteSkip.m_texture);
m_spriteNoise = new Sprite();
m_spriteNoise.m_texture = (Texture2D)Resources.Load("Textures/noise", typeof(Texture2D));
m_spriteFlame = new Sprite();
m_spriteFlame.m_texture = (Texture2D)Resources.Load("Textures/flame", typeof(Texture2D));
#if UNITY_ANDROID
if ( __85()==false )
m_androidFailed = true;
#endif
m_mainRT = __174();
m_graphics = new GraphicsDevice();
m_font = new Police();
m_font.__488(0.6f);
m_materialBlur = __165(SHADER.BLUR);
m_materialBokeh = __165(SHADER.BOKEH);
m_materialBrush = __165(SHADER.BRUSH);
m_materialErase = __165(SHADER.ERASE);
m_materialLightAmbient = __165(SHADER.LIGHTAMBIENT);
m_materialLightAmbientFinal = __165(SHADER.LIGHTAMBIENTFINAL);
m_materialLightDiffuse = __165(SHADER.LIGHTDIFFUSE);
m_materialLightFinal = __165(SHADER.LIGHTFINAL);
m_materialSoftLight = __165(SHADER.SOFTLIGHT);
m_materialTexture24 = __165(SHADER.TEXTURE24);
m_materialTexture32 = __165(SHADER.TEXTURE32);
m_isInitialized = true;
}
#if UNITY_ANDROID
public static int __79(System.IO.Stream stream)
{
return stream.ReadByte();
}
public static ushort __80(System.IO.Stream stream)
{
ushort val = (ushort)stream.ReadByte();
val |= (ushort)(((ushort)stream.ReadByte())<<8);
return val;
}
public static uint __81(System.IO.Stream stream)
{
uint val = (uint)stream.ReadByte();
val |= (uint)(((uint)stream.ReadByte())<<8);
val |= (uint)(((uint)stream.ReadByte())<<16);
val |= (uint)(((uint)stream.ReadByte())<<24);
return val;
}
public static void __82(System.IO.Stream stream, int val)
{
stream.WriteByte((byte)(uint)val);
stream.WriteByte((byte)(((uint)val)>>8));
stream.WriteByte((byte)(((uint)val)>>16));
stream.WriteByte((byte)(((uint)val)>>24));
}
public static string __83(System.IO.Stream stream)
{
string str = "";
while ( true )
{
int c = stream.ReadByte();
if ( c==0 || c==-1 )
break;
str += (char)(byte)c;
}
return str;
}
public static void __84(System.IO.Stream stream, string val)
{
for ( int i=0 ; i<val.Length ; i++ )
stream.WriteByte((byte)val[i]);
stream.WriteByte(0);
}
public static bool __85()
{
m_obbOffset = 0;
m_obbPath = Application.dataPath;
System.IO.Stream fileApk = OpenFile(m_obbPath);
if ( fileApk==null )
return false;
while ( fileApk.Position<fileApk.Length )
{
uint signature = __81(fileApk);
if ( signature==0x08074B50 )
{
fileApk.Position += 12;
continue;
}
if ( signature!=0x04034B50 )
break;
fileApk.Position += 14;
int size = (int)__81(fileApk);
fileApk.Position += 4;
int len = (int)__80(fileApk);
int extralen = (int)__80(fileApk);
string path = "";
for ( int i=0 ; i<len ; i++ )
path += (char)(byte)__79(fileApk);
fileApk.Position += extralen;
if ( path.Substring(path.Length-8)=="/age.obb" )
{
m_obbOffset = (int)fileApk.Position;
break;
}
fileApk.Position += size;
}
fileApk.Close();
if ( m_obbOffset==0 )
return false;
return true;
}
#endif
public static bool __86()
{
return SystemInfo.deviceType==DeviceType.Desktop;
}
public static bool __87()
{
#if UNITY_WEBGL
return true;
#else
return false;
#endif
}
public static bool __88()
{
#if UNITY_EDITOR
return true;
#else
return false;
#endif
}
public static bool __89()
{
#if !UNITY_EDITOR
#if UNITY_STANDALONE_WIN || UNITY_WEBGL
Cursor.visible = false;
#endif
#endif
m_game = new AgeGame();
m_game.__78();
m_isGameLoaded = m_game.__64();
if ( m_isGameLoaded==false )
return false;
if ( m_game.__284() )
{
m_textureGrain = new Texture2D((int)(m_rcGame.width*0.5f), (int)(m_rcGame.height*0.5f), TextureFormat.RGB24, false);
int width = m_textureGrain.width;
int height = m_textureGrain.height;
m_textureGrain.filterMode = FilterMode.Bilinear;
m_textureGrain.wrapMode = TextureWrapMode.Repeat;
Color32 color = new Color32(0, 0, 0, 255);
Color32[] colors = new Color32[width*height];
float xdiv = width / (width * 0.5f);
float ydiv = height / (height * 0.5f);
for ( int y=0 ; y<height ; y++ )
{
for ( int x=0 ; x<width ; x++ )
{
color.r = (byte)(Clamp(Mathf.PerlinNoise(x/xdiv, y/ydiv))*64.0f);
color.g = color.r;
color.b = color.r;
colors[y*width+x] = color;
}
}
m_textureGrain.SetPixels32(colors);
m_textureGrain.Apply();
}
return true;
}
public static bool __90(string path)
{
return System.IO.File.Exists(path);
}
public static int __91(string path)
{
System.IO.FileInfo info = new System.IO.FileInfo(path);
return (int)info.Length;
}
public static void __92(string path)
{
System.IO.File.Delete(path);
}
public static void __93(string path, string newPath)
{
if ( __90(path) )
{
if ( __90(newPath) )
__92(newPath);
System.IO.File.Move(path, newPath);
}
}
public static System.IO.Stream NewFile(string path)
{
System.IO.Stream file = new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
if ( file==null )
return null;
file.SetLength(0);
return file;
}
public static System.IO.Stream OpenFile(string path)
{
if ( __90(path)==false )
return null;
return new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
}
public static void __94(System.IO.Stream stream, string val)
{
for ( int i=0 ; i<val.Length ; i++ )
stream.WriteByte((byte)val[i]);
stream.WriteByte(13);
stream.WriteByte(10);
}
public static Asset __95(string path, bool disableYek = false)
{
#if UNITY_WEBGL
if ( path==m_pathGame )
{
WebForm.m_instance.m_assetGame.__3(0);
return WebForm.m_instance.m_assetGame;
}
else if ( path==m_pathGraphics )
{
WebForm.m_instance.m_assetGraphics.__3(0);
return WebForm.m_instance.m_assetGraphics;
}
else if ( path==m_pathSounds )
{
WebForm.m_instance.m_assetSounds.__3(0);
return WebForm.m_instance.m_assetSounds;
}
else if ( path==m_pathConfig )
{
WebForm.m_instance.m_assetConfig.__3(0);
return WebForm.m_instance.m_assetConfig;
}
#endif
Asset asset = new Asset();
if ( asset.Open(path)==false )
return null;
if ( disableYek )
asset.__0();
return asset;
}
public static bool __10(byte[] bytes, ref int offset)
{
offset++;
return bytes[offset-1]!=0;
}
public static int __12(byte[] bytes, ref int offset)
{
offset++;
return (int)bytes[offset-1];
}
public static ushort __14(byte[] bytes, ref int offset)
{
offset += 2;
ushort val = bytes[offset-2];
val |= (ushort)(((ushort)bytes[offset-1])<<8);
return val;
}
public static uint __16(byte[] bytes, ref int offset)
{
offset += 4;
uint val = bytes[offset-4];
val |= (uint)(((uint)bytes[offset-3])<<8);
val |= (uint)(((uint)bytes[offset-2])<<16);
val |= (uint)(((uint)bytes[offset-1])<<24);
return val;
}
public static string __18(byte[] bytes, ref int offset)
{
string str = "";
while ( offset<bytes.Length )
{
offset++;
int c = bytes[offset-1];
if ( c==0 )
break;
str += (char)(byte)c;
}
return str;
}
public static byte[] StringToBytes(ref string str)
{
if ( str.Length==0 )
return null;
char[] chars = str.ToCharArray();
byte[] bytes = new byte[chars.Length];
for ( int i=0 ; i<chars.Length ; i++ )
bytes[i] = (byte)chars[i];
return bytes;
}
public static byte[] Zip(byte[] buffer)
{
return ZlibStream.CompressBuffer(buffer);
}
public static byte[] Unzip(byte[] buffer)
{
if ( buffer==null )
return null;
return ZlibStream.UncompressBuffer(buffer);
}
public static string __96(ref string str)
{
if ( str.Length==0 )
return "";
if ( str[0]=='\"' )
return str.Substring(1, str.Length-2);
if ( str[0]=='$' )
{
Variable variable;
if ( m_game.m_variables.TryGetValue(str.Substring(1), out variable)==false )
return "";
return variable.m_value;
}
if ( str[0]=='&' )
{
Variable variable;
if ( m_game.m_variables.TryGetValue(str.Substring(1), out variable)==false )
return "";
if ( m_game.m_variables.TryGetValue(variable.m_value, out variable)==false )
return "";
return variable.m_value;
}
return str;
}
public static void __97(string uid, ref string uidScene, ref string uidObj)
{
int index = uid.IndexOf('.');
if ( index==-1 )
{
uidScene = "";
uidObj = uid;
}
else
{
uidScene = uid.Substring(0, index);
uidObj = uid.Substring(index+1);
}
}
public static uint __98(int l, int h)
{
return (uint)( ((ushort)(l)) | (((uint)(ushort)(h))<<16) );
}
public static int __99(uint v)
{
return (int)(short)(ushort)( ((uint)(v)) & 0x0000FFFF );
}
public static int __100(uint v)
{
return (int)(short)(ushort)( ((uint)(v)) >> 16 );
}
public static bool __101(int a, int b)
{
if ( b==0 )
return true;
return (a & b)==b;
}
public static bool __101(uint a, uint b)
{
if ( b==0 )
return true;
return (a & b)==b;
}
public static bool __102(ref string value)
{
int count = 0;
for ( int i=0 ; i<value.Length ; i++ )
{
switch ( value[i] )
{
case '0': case '1': case '2': case '3': case '4': case '5': case '6': case '7': case '8': case '9': break;
case '-':
if ( i!=0 )
return false;
break;
case '.':
count++;
if ( count==2 || i==value.Length-1 )
return false;
break;
default:
return false;
}
}
return false;
}
public static bool __103(char c)
{
if ( c>=48 && c<58 )
return true;
if ( c>=65 && c<91 )
return true;
if ( c>=97 && c<123 )
return true;
if ( c=='_' )
return true;
return false;
}
public static bool __104(ref string name)
{
if ( name.Length==0 )
return false;
for ( int i=0 ; i<name.Length ; i++ )
{
if ( __103(name[i])==false )
return false;
}
return true;
}
public static byte __105(char c)
{
if ( c>=48 && c<58 )
return (byte)(c-48);
if ( c>=65 && c<71 )
return (byte)(c-55);
if ( c>=97 && c<103 )
return (byte)(c-87);
return 0;
}
public static int __106(int value)
{
value = value - ((value >> 1) & 0x55555555);
value = (value & 0x33333333) + ((value >> 2) & 0x33333333);
return (((value + (value >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
}
public static bool __107(int value)
{
int bitcount = 0;
for ( int i=0 ; i<32 ; i++ )
{
if ( (value & (1<<i))!=0 )
bitcount++;
}
if ( bitcount==1 )
return true;
return false;
}
public static int __108(int value, int maxValue)
{
if ( value>maxValue )
return maxValue;
if ( __107(value) )
return value;
for ( int i=1 ; i<=maxValue ; i*=2 )
{
if ( i>value )
return i;
}
return 0;
}
public static uint __109(float value)
{
double d = (double)Clamp(value);
d *= 0xFFFFFFFF;
return (uint)d;
}
public static float __110(uint value)
{
double d = ((double)value)/0xFFFFFFFF;
return (float)d;
}
public static void __111(ref string str, float val)
{
str = val.ToString(CultureInfo.InvariantCulture);
}
public static bool __112(string str)
{
if ( str.Length==0 || str=="1" || __148(ref str, "true") )
return true;
return false;
}
public static bool __112(ref string str)
{
if ( str.Length==0 || str=="1" || __148(ref str, "true") )
return true;
return false;
}
public static int __113(string str)
{
int value;
if ( int.TryParse(str, out value)==false )
return 0;
return value;
}
public static int __113(ref string str)
{
int value;
if ( int.TryParse(str, out value)==false )
return 0;
return value;
}
public static float __114(string str)
{
float value;
if ( float.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out value)==false )
return 0.0f;
return value;
}
public static float __114(ref string str)
{
float value;
if ( float.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out value)==false )
return 0.0f;
return value;
}
public static float __115(string str, bool acceptMinusOne = false)
{
int value;
if ( int.TryParse(str, out value)==false )
return 0.0f;
if ( acceptMinusOne && value==-1 )
return -1.0f;
return Clamp(value/255.0f);
}
public static float __116(ref string str)
{
int value;
if ( int.TryParse(str, out value)==false )
return 0.0f;
return value/1000.0f;
}
public static float __117(string str)
{
int value;
if ( int.TryParse(str, out value)==false )
return 0.0f;
if ( value<=0 )
return 0.0f;
return value/1000.0f;
}
public static float __118(string str)
{
int value;
if ( int.TryParse(str, out value)==false )
value = 0;
value = Clamp(value, 0, 100);
return value/100.0f;
}
public static float __119(string str)
{
int value;
if ( int.TryParse(str, out value)==false )
value = 0;
value = Clamp(value, -100, 100);
return value/100.0f;
}
public static float __120(string str)
{
int value;
if ( int.TryParse(str, out value)==false )
value = 0;
if ( value<1 || value>400 )
return 0.0f;
return value/100.0f;
}
public static float __121(string str)
{
int value;
if ( int.TryParse(str, out value)==false )
value = 0;
if ( value<100 || value>400 )
return 0.0f;
return value/100.0f;
}
public static Color __122(string str)
{
if ( str.IndexOf(',')==-1 )
{
if ( str.Length!=6 && str.Length!=8 )
return Color.black;
return __124(uint.Parse(str, NumberStyles.HexNumber));
}
string[] values = str.Split(',');
byte r = (byte)(values.Length>0 ? G.Clamp(G.__113(ref values[0]), 0, 255) : 0);
byte g = (byte)(values.Length>1 ? G.Clamp(G.__113(ref values[1]), 0, 255) : 0);
byte b = (byte)(values.Length>2 ? G.Clamp(G.__113(ref values[2]), 0, 255) : 0);
return new Color(r/255.0f, g/255.0f, b/255.0f, 1.0f);
}
public static Color __123(string str)
{
if ( str.IndexOf(',')==-1 )
{
if ( str.Length!=6 && str.Length!=8 )
return Color.black;
return __126(uint.Parse(str, NumberStyles.HexNumber));
}
string[] values = str.Split(',');
byte r = (byte)(values.Length>0 ? G.Clamp(G.__113(ref values[0]), 0, 255) : 0);
byte g = (byte)(values.Length>1 ? G.Clamp(G.__113(ref values[1]), 0, 255) : 0);
byte b = (byte)(values.Length>2 ? G.Clamp(G.__113(ref values[2]), 0, 255) : 0);
byte a = (byte)(values.Length>3 ? G.Clamp(G.__113(ref values[3]), 0, 255) : 255);
return new Color(r/255.0f, g/255.0f, b/255.0f, a/255.0f);
}
public static Color __124(uint rgb)
{
byte r = (byte)((rgb & 0xFF000000) >> 24);
byte g = (byte)((rgb & 0xFF0000) >> 16);
byte b = (byte)((rgb & 0xFF00) >> 8);
return new Color(r/255.0f, g/255.0f, b/255.0f);
}
public static Color __125(uint rgb)
{
byte r = (byte)(rgb & 0xFF);
byte g = (byte)((rgb & 0xFF00) >> 8);
byte b = (byte)((rgb & 0xFF0000) >> 16);
return new Color(r/255.0f, g/255.0f, b/255.0f);
}
public static Color __126(uint rgba)
{
byte r = (byte)((rgba & 0xFF000000) >> 24);
byte g = (byte)((rgba & 0xFF0000) >> 16);
byte b = (byte)((rgba & 0xFF00) >> 8);
byte a = (byte)(rgba & 0xFF);
return new Color(r/255.0f, g/255.0f, b/255.0f, a/255.0f);
}
public static uint __127(ref Color rgb)
{
byte r = (byte)(rgb.r*255.0f);
byte g = (byte)(rgb.g*255.0f);
byte b = (byte)(rgb.b*255.0f);
return ((uint)r)<<24 | ((uint)g)<<16 | ((uint)b)<<8 | 255;
}
public static uint __128(ref Color rgba)
{
byte r = (byte)(rgba.r*255.0f);
byte g = (byte)(rgba.g*255.0f);
byte b = (byte)(rgba.b*255.0f);
byte a = (byte)(rgba.a*255.0f);
return ((uint)r)<<24 | ((uint)g)<<16 | ((uint)b)<<8 | a;
}
public static bool __129(double val)
{
return Math.Abs(val)<double.Epsilon;
}
public static bool __129(float val)
{
return Mathf.Abs(val)<float.Epsilon;
}
public static bool __130(float a, float b)
{
return Mathf.Abs(a-b)<float.Epsilon;
}
public static bool __131(float a, float b)
{
return (a-b)>float.Epsilon;
}
public static bool __132(float a, float b)
{
if ( (a-b)>float.Epsilon )
return true;
if ( Mathf.Abs(a-b)<float.Epsilon )
return true;
return false;
}
public static double Clamp(double v, double a, double b)
{
return v<a ? a : (v>b ? b : v);
}
public static float Clamp(float v)
{
return v<0.0f ? 0.0f : (v>1.0f ? 1.0f : v);
}
public static float Clamp(float v, float a, float b)
{
return v<a ? a : (v>b ? b : v);
}
public static float Clamp(float v, float a, float b, float def)
{
return v<a ? def : (v>b ? def : v);
}
public static int Clamp(int v, int a, int b)
{
return v<a ? a : (v>b ? b : v);
}
public static float __133(float v)
{
return v<0.0f ? 0.0f : v;
}
public static int __133(int v)
{
return v<0 ? 0 : v;
}
public static double __134(double s, double a, double b)
{
return a + s*(b-a);
}
public static float __135(float s, float a, float b)
{
return a + s*(b-a);
}
public static int __136(float s, int a, int b)
{
return a + (int)(s*(b-a));
}
public static Color __137(float s, Color a, Color b)
{
return new Color((int)(a.r+s*(b.r-a.r)), (int)(a.g+s*(b.g-a.g)), (int)(a.b+s*(b.b-a.b)), (int)(a.a+s*(b.a-a.a)));
}
public static float __138(float s, float a = 0.0f, float b = 1.0f, bool doIN = true, bool doOUT = true)
{
if ( s<0.5f )
{
if ( doIN )
{
s = Mathf.Sin(s*PI+RAD_270);
s = (s+1.0f)*0.5f;
}
}
else
{
if ( doOUT )
{
s = Mathf.Sin(s*PI+RAD_270);
s = (s+1.0f)*0.5f;
}
}
return (b-a)*s+a;
}
public static float __139(float x1, float y1, float x2, float y2)
{
return Mathf.Sqrt((x1-x2)*(x1-x2)+(y1-y2)*(y1-y2));
}
public static float __140(float x1, float y1, float x2, float y2)
{
return (x1-x2)*(x1-x2)+(y1-y2)*(y1-y2);
}
public static float __141(float x1, float y1, float x2, float y2)
{
if ( x1==x2 && y1==y2 )
return -1.0f;
return Mathf.Atan2(x2-x1, y2-y1);
}
public static float __142(int angleInDegrees)
{
return __144(-angleInDegrees*DEG_TO_RAD+PI);
}
public static int __142(float angle)
{
int result = Mathf.RoundToInt(__144(-angle+PI)*RAD_TO_DEG);
return result==360 ? 0 : result;
}
public static float __143(float a, float b)
{
a = __144(a);
b = __144(b);
float deltaL = b>=a ? b-a : RAD_360-a+b;
float deltaR = b<=a ? a-b : a+(RAD_360-b);
return deltaL<=deltaR ? deltaL : -deltaR;
}
public static float __144(float angle)
{
if ( angle>=0.0f && angle<RAD_360 )
return angle;
float newAngle = Mathf.Acos(Mathf.Cos(angle));
if ( newAngle>RAD_180 )
newAngle = RAD_180;
if ( Mathf.Sin(angle)<0.0f )
{
newAngle = RAD_180-newAngle + RAD_180;
if ( newAngle>=RAD_360 )
newAngle = 0.0f;
return newAngle;
}
return newAngle;
}
public static int __144(int angle, bool positive = true)
{
if ( angle>359 )
return angle % 360;
if ( angle<0 )
{
angle = 360 + angle % 360;
return angle==360 ? 0 : angle;
}
if ( positive==false )
angle -= 180;
return angle;
}
public static int __145(float pixel)
{
if ( pixel<0.0f )
return (int)(pixel/PATH_GRID_CELLSIZE) - 1;
return (int)(pixel/PATH_GRID_CELLSIZE);
}
public static float __146(int cell)
{
return cell*PATH_GRID_CELLSIZE + PATH_GRID_CELLSIZE_HALF;
}
public static float __147(float pixel)
{
return __146(__145(pixel));
}
public static bool __148(string a, string b)
{
return String.Compare(a, b, true)==0;
}
public static bool __148(ref string a, string b)
{
return String.Compare(a, b, true)==0;
}
public static bool __148(string a, ref string b)
{
return String.Compare(a, b, true)==0;
}
public static bool __148(ref string a, ref string b)
{
return String.Compare(a, b, true)==0;
}
public static bool __149(string[] tags, ref string v)
{
if ( tags[0].Length>0 )
{
if ( tags[0].Contains(',') )
{
string[] values = tags[0].Split(',');
for ( int i=0 ; i<values.Length ; i++ )
{
if ( G.__148(ref values[i], ref v) )
return true;
}
}
else
{
if ( G.__148(ref tags[0], ref v) )
return true;
}
}
for ( int i=1 ; i<tags.Length ; i++ )
{
if ( tags[i].Length>0 && G.__148(ref tags[i], ref v) )
return true;
}
return false;
}
public static bool __149(string[] tags, string v)
{
return __149(tags, ref v);
}
public static bool __150(string a, string b)
{
return String.Compare(a, b, true)!=0;
}
public static bool __150(ref string a, ref string b)
{
return String.Compare(a, b, true)!=0;
}
public static bool __150(ref string a, string b)
{
return String.Compare(a, b, true)!=0;
}
public static int __151(string name)
{
if ( __148(ref name, "LEFT") )
return AnimDir.LEFT;
if ( __148(ref name, "RIGHT") )
return AnimDir.RIGHT;
if ( __148(ref name, "FRONT") )
return AnimDir.FRONT;
if ( __148(ref name, "BACK") )
return AnimDir.BACK;
if ( __148(ref name, "FL") )
return AnimDir.FL;
if ( __148(ref name, "FR") )
return AnimDir.FR;
if ( __148(ref name, "BL") )
return AnimDir.BL;
if ( __148(ref name, "BR") )
return AnimDir.BR;
return -1;
}
public static int __151(ref string name)
{
if ( __148(ref name, "LEFT") )
return AnimDir.LEFT;
if ( __148(ref name, "RIGHT") )
return AnimDir.RIGHT;
if ( __148(ref name, "FRONT") )
return AnimDir.FRONT;
if ( __148(ref name, "BACK") )
return AnimDir.BACK;
if ( __148(ref name, "FL") )
return AnimDir.FL;
if ( __148(ref name, "FR") )
return AnimDir.FR;
if ( __148(ref name, "BL") )
return AnimDir.BL;
if ( __148(ref name, "BR") )
return AnimDir.BR;
return -1;
}
public static int __152(float angle, int prevAnimDir)
{
int iAngle = (int)Mathf.Round((angle+PI)/RAD_45);
if ( iAngle>=8 )
iAngle = 0;
switch ( iAngle )
{
case 0:	return AnimDir.BACK;
case 1:	return prevAnimDir==AnimDir.BACK ? AnimDir.BACK : AnimDir.LEFT;
case 2:	return AnimDir.LEFT;
case 3:	return prevAnimDir==AnimDir.FRONT ? AnimDir.FRONT : AnimDir.LEFT;
case 4:	return AnimDir.FRONT;
case 5:	return prevAnimDir==AnimDir.FRONT ? AnimDir.FRONT : AnimDir.RIGHT;
case 6:	return AnimDir.RIGHT;
case 7:	return prevAnimDir==AnimDir.BACK ? AnimDir.BACK : AnimDir.RIGHT;
}
return -1;
}
public static int __153(float angle)
{
int iAngle = (int)Mathf.Round((angle+PI)/RAD_45);
if ( iAngle>=8 )
iAngle = 0;
return __154(iAngle);
}
public static int __154(int index)
{
switch ( index )
{
case 0:	return AnimDir.BACK;
case 1:	return AnimDir.BL;
case 2:	return AnimDir.LEFT;
case 3:	return AnimDir.FL;
case 4:	return AnimDir.FRONT;
case 5:	return AnimDir.FR;
case 6:	return AnimDir.RIGHT;
case 7:	return AnimDir.BR;
}
return -1;
}
public static int __155(int dir)
{
switch ( dir )
{
case AnimDir.BACK:	return 0;
case AnimDir.BL:	return 1;
case AnimDir.LEFT:	return 2;
case AnimDir.FL:	return 3;
case AnimDir.FRONT:	return 4;
case AnimDir.FR:	return 5;
case AnimDir.RIGHT:	return 6;
case AnimDir.BR:	return 7;
}
return -1;
}
public static int __156(int count)
{
if ( count<2 )
return 0;
return m_random.Next(count);
}
public static int __156(int a, int b)
{
int min, max;
if ( a<b )
{
min = a;
max = b;
}
else
{
min = b;
max = a;
}
return m_random.Next(min, max+1);
}
public static float __156(float a, float b)
{
float min, max;
if ( a<b )
{
min = a;
max = b;
}
else
{
min = b;
max = a;
}
return min + (max-min)*(float)m_random.NextDouble();
}
public static int __157(int ax, int ay, int bx, int by, int cx, int cy)
{
return (ax-cx)*(by-cy) - (ay-cy)*(bx-cx);
}
public static int __158(int x, int y, int ax, int ay, int bx, int by)
{
return (bx-ax)*(y-ay) - (by-ay)*(x-ax);
}
public static float __158(float x, float y, float ax, float ay, float bx, float by)
{
return (bx-ax)*(y-ay) - (by-ay)*(x-ax);
}
public static bool __159(int x, int y, int[] tri)
{
if ( __158(x, y, tri[0], tri[1], tri[2], tri[3])<0 )
return false;
if ( __158(x, y, tri[2], tri[3], tri[4], tri[5])<0 )
return false;
if ( __158(x, y, tri[4], tri[5], tri[0], tri[1])<0 )
return false;
return true;
}
public static bool __160(int a1x, int a1y, int b1x, int b1y, int a2x, int a2y, int b2x, int b2y, out Vec2 intersect, out float length)
{
if ( (a1x==a2x && a1y==a2y) || (a1x==b2x && a1y==b2y) )
{
intersect.x = a1x;
intersect.y = a1y;
length = 0.0f;
return true;
}
if ( (b1x==a2x && b1y==a2y) || (b1x==b2x && b1y==b2y) )
{
intersect.x = b1x;
intersect.y = b1y;
length = __139(a1x, a1y, b1x, b1y);
return true;
}
int s1 = __157(a1x, a1y, b1x, b1y, b2x, b2y);
int s2 = __157(a1x, a1y, b1x, b1y, a2x, a2y);
if ( (s1|s2)!=0 && (s1^s2)<0 )
{
int s3 = __157(a2x, a2y, b2x, b2y, a1x, a1y);
int s4 = s3 + s2 - s1;
if ( (s3|s4)!=0 && (s3^s4)<0 )
{
float t = s3/(float)(s3-s4);
intersect.x = a1x + t*(b1x-a1x);
intersect.y = a1y + t*(b1y-a1y);
length = __139((float)a1x, (float)a1y, intersect.x, intersect.y);
return true;
}
}
intersect.x = 0.0f;
intersect.y = 0.0f;
length = 0.0f;
return false;
}
public static void __161(BreakTextInfo info, string text, Police font, float maxRowWidth, float textScale = -1.0f, READING reading = READING.CENTER)
{
info.Clear();
if ( textScale==0.0f )
return;
bool cjk = font.m_cjk;
ALIGN align = ALIGN.CENTER;
if ( font.__489() )
{
if ( reading==READING.NEAR )
align = ALIGN.LEFT;
else if ( reading==READING.FAR )
align = ALIGN.RIGHT;
}
else
{
if ( reading==READING.NEAR )
align = ALIGN.RIGHT;
else if ( reading==READING.FAR )
align = ALIGN.LEFT;
}
Rect rc = Rect.Zero;
string[] paras = text.Split('\n');
for ( int iPara=0 ; iPara<paras.Length ; iPara++ )
{
if ( iPara>0 )
info.Add();
float lineWidth = 0.0f;
float lineHeight = 0.0f;
string line = "";
int wordCount = 0;
string tmp = "";
string[] words = paras[iPara].Split(' ');
if ( cjk )
{
List<string> list = new List<string>();
for ( int i=0 ; i<words.Length ; i++ )
{
if ( i>0 )
list.Add(" ");
int k = 0;
for ( int j=0 ; j<=words[i].Length ; j++ )
{
if ( j==words[i].Length || words[i][j]>=256 )
{
if ( j>k )
{
list.Add(words[i].Substring(k, j-k));
k = j;
}
if ( j<words[i].Length )
list.Add(words[i].Substring(j, 1));
k++;
}
else
{
continue;
}
}
}
words = list.ToArray();
}
for ( int i=0 ; i<words.Length ; i++ )
{
if ( cjk )
{
tmp += words[i];
}
else
{
if ( tmp.Length==0 )
tmp += words[i];
else
tmp += " " + words[i];
}
Vec2 size = font.__491(tmp, textScale);
if ( size.x>maxRowWidth )
{
rc.width = lineWidth;
rc.height = lineHeight;
switch ( align )
{
case ALIGN.CENTER:
rc.x = maxRowWidth*0.5f - rc.width*0.5f;
break;
case ALIGN.LEFT:
rc.x = 0.0f;
break;
case ALIGN.RIGHT:
rc.x = rc.width - lineWidth;
break;
}
rc.y = 0.0f;
info.m_texts.Add(line);
info.m_words.Add(wordCount);
info.m_initLineRects.Add(rc);
info.m_lineRects.Add(rc);
info.m_paraSizes.Add(Vec2.Zero);
tmp = words[i];
line = tmp;
size = font.__491(line, textScale);
lineWidth = size.x;
lineHeight = size.y;
wordCount = words[i].Length>16 ? 2 : 1;
continue;
}
if ( size.x>lineWidth )
lineWidth = size.x;
if ( size.y>lineHeight )
lineHeight = size.y;
line = tmp;
if ( words[i].Length>16 )
wordCount += 2;
else
wordCount++;
}
if ( line.Length>0 )
{
rc.width = lineWidth;
rc.height = lineHeight;
switch ( align )
{
case ALIGN.CENTER:
rc.x = maxRowWidth*0.5f - rc.width*0.5f;
break;
case ALIGN.LEFT:
rc.x = 0.0f;
break;
case ALIGN.RIGHT:
rc.x = rc.width - lineWidth;
break;
}
rc.y = 0.0f;
info.m_texts.Add(line);
info.m_words.Add(wordCount);
info.m_initLineRects.Add(rc);
info.m_lineRects.Add(rc);
info.m_paraSizes.Add(Vec2.Zero);
}
}
info.m_maxRowWidth = maxRowWidth;
info.__203(font);
}
public static Color __162(string name, int defRed = 255, int defGreen = 255, int defBlue = 255)
{
switch ( name )
{
case "Blue":		case "BLUE":			return m_colors[0];
case "Brown":		case "BROWN":			return m_colors[1];
case "Cyan":		case "CYAN":			return m_colors[2];
case "DarkGreen":	case "DARKGREEN":		return m_colors[3];
case "Gold":		case "GOLD":			return m_colors[4];
case "Gray":		case "GRAY":			return m_colors[5];
case "Green":		case "GREEN":			return m_colors[6];
case "Magenta":		case "MAGENTA":			return m_colors[7];
case "Maroon":		case "MAROON":			return m_colors[8];
case "Orange":		case "ORANGE":			return m_colors[9];
case "Pink":		case "PINK":			return m_colors[10];
case "Purple":		case "PURPLE":			return m_colors[11];
case "Red":			case "RED":				return m_colors[12];
case "Silver":		case "SILVER":			return m_colors[13];
case "Yellow":		case "YELLOW":			return m_colors[14];
case "White":		case "WHITE":			return m_colors[15];
}
return new Color(defRed/255.0f, defGreen/255.0f, defBlue/255.0f);
}
public static Shader __163(SHADER type)
{
return m_shaders[(int)type];
}
public static ComputeShader __164(COMPUTE type)
{
#if UNITY_WEBGL
return null;
#else
return m_computeShaders[(int)type];
#endif
}
public static Material __165(SHADER type, Texture texture = null)
{
Material material = new Material(__163(type));
material.mainTexture = texture;
material.color = G.m_colorWhite;
switch ( type )
{
case SHADER.TEXT:
material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
break;
case SHADER.BCS:
case SHADER.BEACON:
case SHADER.BLUR:
case SHADER.BRUSH:
case SHADER.COLORIZATION:
case SHADER.CURVE:
case SHADER.FLAME:
case SHADER.FILTER:
case SHADER.FIRE:
case SHADER.GRAYSCALE:
case SHADER.LIGHTAMBIENT:
case SHADER.LIGHTAMBIENTFINAL:
case SHADER.LIGHTDIFFUSE:
case SHADER.LIGHTFINAL:
case SHADER.LUT:
case SHADER.MONOCHROME:
case SHADER.PARALLAX:
case SHADER.RAIN:
case SHADER.TEXTURE32:
material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
break;
case SHADER.BOKEH:
case SHADER.ERASE:
case SHADER.GRAIN:
case SHADER.SOFTLIGHT:
case SHADER.TEXTURE24:
break;
}
switch ( type )
{
case SHADER.ERASE:
material.color = Color.black;
break;
case SHADER.FLAME:
material.SetTexture("_FlameTex", G.m_spriteFlame.m_texture);
material.SetFloat("_speed", 0.1f);
material.SetFloat("_scale", 1.0f);
break;
}
return material;
}
public static Vector2Int __166(TEXTURESCALE mode = TEXTURESCALE.FULL)
{
float scale;
switch ( mode )
{
case TEXTURESCALE.EIGHTH:
scale = 0.125f;
break;
case TEXTURESCALE.QUARTER:
scale = 0.25f;
break;
case TEXTURESCALE.HALF:
scale = 0.5f;
break;
default:
scale = 1.0f;
break;
}
return new Vector2Int((int)(m_rcView.width*scale), (int)(m_rcView.height*scale));
}
public static RenderTexture __167(int width, int height)
{
RenderTexture rt = new RenderTexture(width, height, 0);
rt.Create();
return rt;
}
public static RenderTexture __167(TEXTURESCALE scale = TEXTURESCALE.FULL)
{
Vector2Int size = __166(scale);
return __167(size.x, size.y);
}
public static RenderTexture __168(int width, int height, bool rw = false)
{
if ( rw )
{
RenderTextureDescriptor desc = new RenderTextureDescriptor(width, height);
desc.enableRandomWrite = true;
return RenderTexture.GetTemporary(desc);
}
return RenderTexture.GetTemporary(width, height);
}
public static RenderTexture __168(TEXTURESCALE scale = TEXTURESCALE.FULL, bool rw = false)
{
Vector2Int size = __166(scale);
return __168(size.x, size.y, rw);
}
public static RenderTexture __169(int width, int height)
{
return __168(width, height, true);
}
public static RenderTexture __169(TEXTURESCALE scale = TEXTURESCALE.FULL)
{
return __168(scale, true);
}
public static void __170(RenderTexture rt)
{
if ( rt )
RenderTexture.ReleaseTemporary(rt);
}
public static void __171()
{
GL.Clear(false, true, Color.black);
}
public static void __171(Color color)
{
GL.Clear(false, true, color);
}
public static void __172()
{
GL.Clear(false, true, m_colorClear);
}
public static RenderTexture __173(RenderTexture rt)
{
RenderTexture old = RenderTexture.active;
Graphics.SetRenderTarget(rt);
return old;
}
public static RenderTexture __174()
{
return RenderTexture.active;
}
public static void __175(Texture2D tex)
{
tex.ReadPixels(new UnityEngine.Rect(0, 0, tex.width, tex.height), 0, 0);
tex.Apply();
}
public static void Dispatch(ComputeShader cs, int kernel = 0)
{
int x = Mathf.CeilToInt(m_rcView.width / 8.0f);
int y = Mathf.CeilToInt(m_rcView.height / 8.0f);
cs.Dispatch(kernel, x, y, 1);
}
public static void Dispatch(ComputeShader cs, int x, int y, int z, int kernel = 0)
{
cs.Dispatch(kernel, x, y, z);
}
public static void Dispatch(ComputeShader cs, int width, int height, int kernel = 0)
{
int threadGroupX = Mathf.CeilToInt(width / 8.0f);
int threadGroupY = Mathf.CeilToInt(height / 8.0f);
cs.Dispatch(kernel, threadGroupX, threadGroupY, 1);
}
public static void __70(Texture input)
{
m_materialTexture32.mainTexture = input;
m_graphics.__353(m_materialTexture32);
m_materialTexture32.mainTexture = null;
}
#if UNITY_WEBGL
public static void __176(float radius, Texture texture = null, TEXTURESCALE scale = TEXTURESCALE.FULL)
{
bool self = false;
if ( texture==null )
{
self = true;
texture = __174();
}
RenderTexture rtBlur = __168(scale);
RenderTexture old = __173(rtBlur);
__172();
m_materialBlur.SetFloat("_size", radius);
m_materialBlur.mainTexture = texture;
m_graphics.__363(m_materialBlur);
m_materialBlur.mainTexture = null;
__173(old);
if ( self )
__172();
__70(rtBlur);
__170(rtBlur);
}
#else
public static void __176(float radius, Texture texture = null, TEXTURESCALE scale = TEXTURESCALE.FULL)
{
float sigma = Clamp(radius*0.5f, 0.5f, 1000.0f);
float data = 1.0f / (Mathf.Sqrt(2.0f * Mathf.PI) * sigma);
float data2 = 2.0f * sigma * sigma;
bool self = false;
if ( texture==null )
{
self = true;
texture = __174();
}
int width = texture.width;
int height = texture.height;
ComputeShader cs = __164(COMPUTE.BLUR);
cs.SetInt("g_width", width);
cs.SetInt("g_height", height);
cs.SetFloat("g_radius", radius);
cs.SetFloat("g_data", data);
cs.SetFloat("g_data2", data2);
RenderTexture rtHorz = __169(width, height);
cs.SetTexture(m_kernelBlurHorz, "g_texture", texture);
cs.SetTexture(m_kernelBlurHorz, "g_output", rtHorz);
G.Dispatch(cs, width, height, m_kernelBlurHorz);
RenderTexture rtVert = __169(width, height);
cs.SetTexture(m_kernelBlurVert, "g_texture", rtHorz);
cs.SetTexture(m_kernelBlurVert, "g_output", rtVert);
Dispatch(cs, width, height, m_kernelBlurVert);
if ( self )
__172();
__70(rtVert);
__170(rtVert);
__170(rtHorz);
}
#endif
public static void FillScreen(Color color)
{
m_materialBrush.color = color;
m_graphics.__354(m_materialBrush, ref m_rcView);
}
public static Rect __177(Rect rc)
{
return new Rect(
Mathf.Clamp(rc.x-rc.width, 0.0f, m_rcView.width),
Mathf.Clamp(rc.y-rc.height, 0.0f, m_rcView.height),
Mathf.Clamp(rc.width*2.0f, 0.0f, m_rcView.width),
Mathf.Clamp(rc.height*2.0f, 0.0f, m_rcView.height));
}
public static void Release(UnityEngine.Object obj)
{
if ( obj )
UnityEngine.Object.DestroyImmediate(obj, true);
}
public static void __178(ref Mesh mesh)
{
Release(mesh);
mesh = new Mesh();
mesh.MarkDynamic();
}
public static void __179()
{
}
public static void __180(string name)
{
if ( name.Length>0 )
PlayerPrefs.SetString(name, "");
}
public static void __181(string name, ref string value)
{
if ( name.Length>0 )
PlayerPrefs.SetString(name, value);
}
public static string __182(string name)
{
if ( name.Length==0 || PlayerPrefs.HasKey(name)==false )
return "";
return PlayerPrefs.GetString(name);
}
public static JsonObj __183(string path)
{
if ( path.Length==0 )
return null;
if ( G.__90(path)==false )
return null;
Asset asset = __95(path, true);
if ( asset==null )
return null;
int size = asset.__7();
JsonObj json = Json.__375(Json.__372(asset.__9(size)));
asset.Close();
return json;
}
public static void __184(JsonObj json, string path)
{
if ( json==null || path.Length==0 )
return;
System.IO.Stream file = NewFile(path);
if ( file==null )
return;
json.__377(file);
file.Close();
#if UNITY_WEBGL
WebForm.JavascriptSyncFiles();
#endif
}
public static string __185()
{
return m_folderSavegame + "game.sav";
}
public static void __186()
{
string path = __185();
if ( __90(path) )
{
__92(path);
#if UNITY_WEBGL
WebForm.JavascriptSyncFiles();
#endif
}
}
public static bool __187(int index, bool visible)
{
index--;
if ( index<0 || index>=m_game.m_menuUserButtonVisibilities.Length )
return false;
if ( visible!=m_game.m_menuUserButtonVisibilities[index] )
{
m_game.m_menuUserButtonVisibilities[index] = visible;
m_game.__248();
return true;
}
return false;
}
public static string __188(int index)
{
return m_folderSavegame + "game" + index.ToString() + ".sav";
}
public static string __189(int index)
{
string path = __188(index);
JsonObj jSystem = null;
if ( __90(path)==false )
return "";
Asset asset = __95(path, true);
if ( asset==null )
return "";
asset.__4(1);
jSystem = Json.__375(Json.__372(asset.__9(1024)));
asset.Close();
if ( m_game.__247(jSystem)==false )
return "";
return jSystem.GetString("name");
}
public static bool __190(int index)
{
if ( index==-1 )
{
for ( int i=0 ; i<SAVEGAME_COUNT ; i++ )
{
if ( __190(i) )
return true;
}
return false;
}
if ( index<0 || index>=SAVEGAME_COUNT )
return false;
return __189(index).Length>0;
}
public static string __191(int index)
{
string title = index.ToString() + ") ";
string name = __189(index);
if ( name.Length==0 )
name = Localization.m_wordMenuEmpty.Get();
title += name;
return title;
}
public static void __192(int index)
{
string path = __188(index);
if ( __90(path) )
{
__92(path);
#if UNITY_WEBGL
WebForm.JavascriptSyncFiles();
#endif
}
}
public static string __193()
{
string lang = "";
switch ( Application.systemLanguage )
{
case SystemLanguage.Chinese:
lang = "ChineseTraditional";
break;
default:
lang = Application.systemLanguage.ToString();
break;
}
return lang;
}
public static Police __194()
{
if ( m_font )
return m_font;
return m_game.__216();
}
/*public static string Md5(string strToEncrypt)
{
System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
byte[] bytes = ue.GetBytes(strToEncrypt);
System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
byte[] hashBytes = md5.ComputeHash(bytes);
string hashString = "";
for ( int i=0 ; i<hashBytes.Length ; i++ )
hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
return hashString.PadLeft(32, '0');
}*/
public static string __195(string text)
{
byte[] bytes = new byte[text.Length];
for ( int i=0 ; i<text.Length ; i++ )
bytes[i] = (byte)text[i];
return Convert.ToBase64String(bytes);
}
public static string __196(string text)
{
byte[] bytes = Convert.FromBase64String(text);
text = "";
for ( int i=0 ; i<bytes.Length ; i++ )
{
if ( bytes[i]==0 )
break;
text += (char)bytes[i];
}
return text;
}
public static string __197(string data)
{
if ( data.Length==0 || data.Length%2!=0 )
return "";
int[] word = { 55, 49, 52, 50, 53, 53, 57, 48 };
char[] result = new char[data.Length/2];
for ( int i=0, j=0 ; i<data.Length ; i+=2, j++ )
{
int a = (int)data[i];
int b = (int)data[i+1];
a = a>=48 && a<58 ? a-48 : a-55;
b = b>=48 && b<58 ? b-48 : b-55;
result[j] = (char)(a*16+b-word[j%8]);
}
return new string(result);
}
public static void __198(string text)
{
m_game.m_alert = text;
m_game.__250(text);
#if UNITY_EDITOR
Debug.Log(text);
#endif
}
public static void Popup(string name, string title, string message, string[] fields = null, POPUP id = POPUP.DEFAULT, bool showMessageAtTop = true, RoleBox roleBox = null)
{
MessageBox.m_instance.Open(name, title, message, fields, id, showMessageAtTop, roleBox);
}
public static void Popup(string message)
{
MessageBox.m_instance.Open("", m_game.m_gameName, message);
}
public static void OpenUrl(string url)
{
if ( url.Length>0 )
{
m_game.__289(ref url);
#if UNITY_WEBGL
Application.ExternalEval("window.open(\"" + url + "\")");
#else
Application.OpenURL(url);
#endif
}
}
public static void InterludeLock(float duration = 0.0f)
{
G.m_game.m_interludeLock = true;
G.m_game.m_interludeLockDuration = duration;
G.m_game.m_interludeLockTime = G.m_game.m_time;
}
public static void InterludeUnlock()
{
G.m_game.m_interludeLock = false;
}
public static void Vibrate()
{
#if UNITY_IOS || UNITY_ANDROID
Handheld.Vibrate();
#endif
}
public static void Success(int puzzle)
{
m_game.m_scenario.Success(puzzle);
}
#if UNITY_EDITOR
static int s_debugCount = 0;
public static void __199(int ms)
{
if ( s_debugCount==10 )
return;
Thread.Sleep(ms);
s_debugCount++;
}
#endif
}
public class BreakTextInfo
{
public float m_maxRowWidth;
public List<string> m_texts = new List<string>();
public List<int> m_words = new List<int>();
public List<Rect> m_initLineRects = new List<Rect>();
public List<Rect> m_lineRects = new List<Rect>();
public List<Vec2> m_paraSizes = new List<Vec2>();
public int __66()
{
return m_texts.Count;
}
public void Clear()
{
m_maxRowWidth = 0.0f;
m_texts.Clear();
m_words.Clear();
m_initLineRects.Clear();
m_lineRects.Clear();
m_paraSizes.Clear();
}
public void __200()
{
if ( m_texts.Count==0 )
Add();
}
public void Add()
{
m_texts.Add("");
m_words.Add(0);
m_initLineRects.Add(Rect.Zero);
m_lineRects.Add(Rect.Zero);
m_paraSizes.Add(Vec2.Zero);
}
public void __201()
{
while ( m_texts.Count>1 )
__202();
}
public void __202()
{
if ( m_texts.Count>0 )
{
m_texts.RemoveAt(m_texts.Count-1);
m_words.RemoveAt(m_texts.Count);
m_initLineRects.RemoveAt(m_texts.Count);
m_lineRects.RemoveAt(m_texts.Count);
m_paraSizes.RemoveAt(m_texts.Count);
}
}
public void __203(Police font)
{
for ( int i=0 ; i<__66() ; i++ )
{
m_lineRects[i] = m_initLineRects[i];
m_paraSizes[i] = Vec2.Zero;
}
int index = 0;
float height = 0.0f;
Rect rc;
for ( int i=0 ; i<=__66() ; i++ )
{
if ( i==__66() || m_texts[i].Length==0 )
{
if ( height>0.0f )
{
for ( int j=index ; j<i ; j++ )
m_paraSizes[j] = new Vec2(m_maxRowWidth, height);
}
height = 0.0f;
continue;
}
if ( height==0.0f )
{
height = m_lineRects[i].height;
index = i;
continue;
}
height += font.m_lineSpacing;
height += m_lineRects[i].height;
rc = m_lineRects[i];
rc.y = m_lineRects[i-1].__437() + font.m_lineSpacing;
m_lineRects[i] = rc;
}
}
}
