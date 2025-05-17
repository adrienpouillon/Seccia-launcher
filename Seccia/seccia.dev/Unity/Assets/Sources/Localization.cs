using System;
public class Language
{
public string m_name;
public bool m_leftToRight;
public bool m_cjk;
public Police m_cjkFont;
public float m_fast;
public float m_slow;
public static implicit operator bool(Language inst) { return inst!=null; }
}
public static class Localization
{
private static bool m_loaded = false;
public static bool[] m_cjk;
public static Term m_wordMenuLanguageName;
public static Term m_wordMenuNewGame;
public static Term m_wordMenuLoadGame;
public static Term m_wordMenuSaveGame;
public static Term m_wordMenuGame;
public static Term m_wordMenuEmpty;
public static Term m_wordMenuContinue;
public static Term m_wordMenuOptions;
public static Term m_wordMenuHelp;
public static Term m_wordMenuCredits;
public static Term m_wordMenuQuit;
public static Term m_wordMenuBackMainMenu;
public static Term m_wordMenuBackGame;
public static Term m_wordMenuLanguage;
public static Term m_wordMenuLanguageAudio;
public static Term m_wordMenuLanguageText;
public static Term m_wordMenuSubtitle;
public static Term m_wordMenuGlobalVolume;
public static Term m_wordMenuSoundVolume;
public static Term m_wordMenuMusicVolume;
public static Term m_wordMenuVoiceVolume;
public static Term m_wordMenuFont;
public static Term m_wordMenuIcon;
public static Term m_wordMenuQuality;
public static Term m_wordMenuResolution;
public static Term m_wordMenuFullscreen;
public static Term m_wordMenuOn;
public static Term m_wordMenuOff;
public static Term m_wordMenuNone;
public static Term m_wordMenuSlow;
public static Term m_wordMenuFast;
public static Term m_wordMenuManual;
public static Term m_wordMenuHigh;
public static Term m_wordMenuLow;
public static Term m_wordUse;
public static Term m_wordWith;
public static Term m_wordMenuPlay;
public static Term m_wordMenuStore;
public static Term m_wordMenuRate;
public static Term m_wordMenuBack;
public static Term m_wordMenuConfirmation;
public static Term m_wordMenuError;
public static Term m_wordMenuAccount;
public static Term m_wordMenuAccountCreate;
public static Term m_wordMenuAccountConnect;
public static Term m_wordMenuAccountDisconnect;
public static Term m_wordMenuAccountPassword;
public static Term m_wordMenuAccountDownload;
public static Term m_wordMenuAccountUpload;
public static Term m_wordMenuAccountEmailSent;
public static Term m_wordMenuAccountLoginFailed;
public static Term m_wordMenuAccountTaken;
public static Term m_wordMenuAccountPlay;
public static Term m_wordMenuPrivacyPolicy;
public static Term m_wordDetach;
public static Term m_wordMenuAccountDelete;
public static Term m_wordMenuAccountCheckboxAccount;
public static Term m_wordMenuAccountCheckboxNewsletter;
public static Term m_wordMenuAccountLegal1;
public static Term m_wordMenuAccountLegal2;
public static void __64(Asset asset)
{
if ( m_loaded )
return;
m_loaded = true;
m_wordMenuLanguageName = asset.__21();
m_wordMenuNewGame = asset.__21();
m_wordMenuLoadGame = asset.__21();
m_wordMenuSaveGame = asset.__21();
m_wordMenuGame = asset.__21();
m_wordMenuEmpty = asset.__21();
m_wordMenuContinue = asset.__21();
m_wordMenuOptions = asset.__21();
m_wordMenuHelp = asset.__21();
m_wordMenuCredits = asset.__21();
m_wordMenuQuit = asset.__21();
m_wordMenuBackMainMenu = asset.__21();
m_wordMenuBackGame = asset.__21();
m_wordMenuLanguage = asset.__21();
m_wordMenuLanguageAudio = asset.__21();
m_wordMenuLanguageText = asset.__21();
m_wordMenuSubtitle = asset.__21();
m_wordMenuGlobalVolume = asset.__21();
m_wordMenuSoundVolume = asset.__21();
m_wordMenuMusicVolume = asset.__21();
m_wordMenuVoiceVolume = asset.__21();
m_wordMenuFont = asset.__21();
m_wordMenuIcon = asset.__21();
m_wordMenuQuality = asset.__21();
m_wordMenuResolution = asset.__21();
m_wordMenuFullscreen = asset.__21();
m_wordMenuOn = asset.__21();
m_wordMenuOff = asset.__21();
m_wordMenuNone = asset.__21();
m_wordMenuSlow = asset.__21();
m_wordMenuFast = asset.__21();
m_wordMenuManual = asset.__21();
m_wordMenuHigh = asset.__21();
m_wordMenuLow = asset.__21();
m_wordUse = asset.__21();
m_wordWith = asset.__21();
m_wordMenuPlay = asset.__21();
m_wordMenuStore = asset.__21();
m_wordMenuRate = asset.__21();
m_wordMenuBack = asset.__21();
m_wordMenuConfirmation = asset.__21();
m_wordMenuError = asset.__21();
m_wordMenuAccount = asset.__21();
m_wordMenuAccountCreate = asset.__21();
m_wordMenuAccountConnect = asset.__21();
m_wordMenuAccountDisconnect = asset.__21();
m_wordMenuAccountPassword = asset.__21();
m_wordMenuAccountDownload = asset.__21();
m_wordMenuAccountUpload = asset.__21();
m_wordMenuAccountEmailSent = asset.__21();
m_wordMenuAccountLoginFailed = asset.__21();
m_wordMenuAccountTaken = asset.__21();
m_wordMenuAccountPlay = asset.__21();
m_wordMenuPrivacyPolicy = asset.__21();
m_wordDetach = asset.__21();
m_wordMenuAccountDelete = asset.__21();
m_wordMenuAccountCheckboxAccount = asset.__21();
m_wordMenuAccountCheckboxNewsletter = asset.__21();
m_wordMenuAccountLegal1 = asset.__21();
m_wordMenuAccountLegal2 = asset.__21();
}
public static float __432(int wordCount)
{
float duration = 0.0f;
SUBTITLE mode = G.m_game.m_optionSubtitle;
switch ( mode )
{
case SUBTITLE.SLOW:
duration = wordCount * G.m_game.__206().m_slow;
break;
case SUBTITLE.FAST:
duration = wordCount * G.m_game.__206().m_fast;
break;
case SUBTITLE.MANUAL:
duration = 0.0f;
break;
case SUBTITLE.NONE:
duration = 0.5f;
break;
}
if ( mode!=SUBTITLE.NONE && duration<2.0f )
duration = 2.0f;
if ( mode==SUBTITLE.MANUAL )
duration = -1.0f;
return duration;
}
}
