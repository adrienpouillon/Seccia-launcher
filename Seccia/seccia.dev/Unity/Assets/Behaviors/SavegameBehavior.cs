using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
public class SavegameBehavior : MonoBehaviour
{
private const int ERROR_UNKNOWN = -1;
private const int ERROR_NONE = 0;
private const int ERROR_LOGIN = 1;
private const int ERROR_TAKEN = 2;
public static SavegameBehavior m_instance = null;
private float m_startTime = 0.0f;
private bool m_busy = false;
void Awake()
{
m_instance = this;
}
void Update()
{
}
public bool IsBusy(float duration = 0.0f)
{
if ( G.m_game.m_savegameServerEnabled==false )
return false;
if ( m_busy==false || duration==0.0f )
return m_busy;
return Time.time-m_startTime>duration;
}
public void Download()
{
if ( IsBusy() )
return;
if ( G.m_game.__225()==false )
{
OnDownload(1, null);
return;
}
m_startTime = Time.time;
m_busy = true;
StartCoroutine(DownloadProc());
}
public void Upload(int index)
{
if ( IsBusy() || G.__190(index)==false )
return;
if ( G.m_game.__225()==false )
{
OnUpload(ERROR_LOGIN);
return;
}
string path = G.__188(index);
string text = System.IO.File.ReadAllText(path);
byte[] zip = G.Zip(G.StringToBytes(ref text));
if ( zip==null || zip.Length>100000 )
{
OnUpload(ERROR_UNKNOWN);
return;
}
m_startTime = Time.time;
m_busy = true;
StartCoroutine(UploadProc(zip));
}
public void Connect(string user, string pass)
{
if ( IsBusy() )
return;
m_startTime = Time.time;
m_busy = true;
StartCoroutine(ConnectProc(user, pass));
}
public void Signup(string user)
{
if ( IsBusy() )
return;
m_startTime = Time.time;
m_busy = true;
StartCoroutine(SignupProc(user));
}
public void ResetPassword(string user)
{
if ( IsBusy() )
return;
m_startTime = Time.time;
m_busy = true;
StartCoroutine(ResetPasswordProc(user));
}
public void DeleteAccount(string user, string pass)
{
if ( IsBusy() )
return;
m_startTime = Time.time;
m_busy = true;
StartCoroutine(DeleteAccountProc(user, pass));
}
bool CheckServerVersion(int version)
{
return version==1;
}
void OnDownload(int error, byte[] zip)
{
switch ( error )
{
case ERROR_NONE:
{
byte[] data = G.Unzip(zip);
if ( data!=null )
{
int index = G.m_game.m_savegameEnabled ? G.SAVEGAME_INDEX_SERVER : G.SAVEGAME_INDEX_AUTO;
string path = G.__188(index);
System.IO.Stream file = G.NewFile(path);
if ( file!=null )
{
file.Write(data, 0, data.Length);
file.Close();
}
if ( G.m_game.m_savegameEnabled )
G.Popup("play", Localization.m_wordMenuAccountDownload.Get(), Localization.m_wordMenuAccountPlay.Get(), null, POPUP.QUESTION);
else
{
G.m_game.__47(G.SAVEGAME_INDEX_AUTO);
G.m_game.m_menuGame.__460(MenuGame.ID_MAIN);
}
return;
}
G.Popup("", Localization.m_wordMenuAccountDownload.Get(), Localization.m_wordMenuError.Get());
break;
}
case ERROR_LOGIN:
{
G.Popup("", Localization.m_wordMenuAccountDownload.Get(), Localization.m_wordMenuAccountLoginFailed.Get());
break;
}
default:
{
G.Popup("", Localization.m_wordMenuAccountDownload.Get(), Localization.m_wordMenuError.Get());
break;
}
}
}
void OnUpload(int error)
{
switch ( error )
{
case ERROR_NONE:
{
G.m_game.m_menuGame.__460(MenuGame.ID_ACCOUNT);
break;
}
case ERROR_LOGIN:
{
G.Popup("", Localization.m_wordMenuAccountUpload.Get(), Localization.m_wordMenuAccountLoginFailed.Get());
break;
}
default:
{
G.Popup("", Localization.m_wordMenuAccountUpload.Get(), Localization.m_wordMenuError.Get());
break;
}
}
}
void OnConnect(int error, string user, string pass)
{
switch ( error )
{
case ERROR_NONE:
{
G.m_game.m_savegameServerUser = user;
G.m_game.m_savegameServerPass = pass;
G.m_game.__248();
G.m_game.m_menuGame.__459();
break;
}
case ERROR_LOGIN:
{
G.Popup("", Localization.m_wordMenuAccountConnect.Get(), Localization.m_wordMenuAccountLoginFailed.Get());
break;
}
default:
{
G.Popup("", Localization.m_wordMenuAccountConnect.Get(), Localization.m_wordMenuError.Get());
break;
}
}
}
void OnSignup(int error, string user)
{
switch ( error )
{
case ERROR_NONE:
{
G.m_game.m_menuGame.__459();
G.Popup("", Localization.m_wordMenuAccountCreate.Get(), Localization.m_wordMenuAccountEmailSent.Get());
break;
}
case ERROR_LOGIN:
{
G.Popup("", Localization.m_wordMenuAccountCreate.Get(), Localization.m_wordMenuAccountLoginFailed.Get());
break;
}
case ERROR_TAKEN:
{
G.Popup("", Localization.m_wordMenuAccountCreate.Get(), Localization.m_wordMenuAccountTaken.Get());
break;
}
default:
{
G.Popup("", Localization.m_wordMenuAccountCreate.Get(), Localization.m_wordMenuError.Get());
break;
}
}
}
void OnResetPassword(int error, string user)
{
switch ( error )
{
case ERROR_NONE:
{
G.Popup("", Localization.m_wordMenuAccountPassword.Get(), Localization.m_wordMenuAccountEmailSent.Get());
break;
}
case ERROR_LOGIN:
{
G.Popup("", Localization.m_wordMenuAccountPassword.Get(), Localization.m_wordMenuAccountLoginFailed.Get());
break;
}
default:
{
G.Popup("", Localization.m_wordMenuAccountPassword.Get(), Localization.m_wordMenuError.Get());
break;
}
}
}
void OnDeleteAccount(int error, string user)
{
switch ( error )
{
case ERROR_NONE:
{
G.m_game.m_savegameServerUser = "";
G.m_game.m_savegameServerPass = "";
G.m_game.__248();
G.m_game.m_menuGame.__459();
G.Popup("", Localization.m_wordMenuAccountDelete.Get(), Localization.m_wordMenuAccountEmailSent.Get());
break;
}
case ERROR_LOGIN:
{
G.Popup("", Localization.m_wordMenuAccountDelete.Get(), Localization.m_wordMenuAccountLoginFailed.Get());
break;
}
default:
{
G.Popup("", Localization.m_wordMenuAccountDelete.Get(), Localization.m_wordMenuError.Get());
break;
}
}
}
IEnumerator DownloadProc()
{
string serverUrl = G.m_game.m_savegameServerUrl + "age/";
WWWForm form = new WWWForm();
form.AddField("version", G.SAVEGAME_SERVER_VERSION.ToString());
form.AddField("action", "download");
form.AddField("user", G.m_game.m_savegameServerUser.ToLower());
form.AddField("pass", G.m_game.m_savegameServerPass.ToLower());
form.AddField("game", G.m_game.m_savegameServerGame);
UnityWebRequest www = UnityWebRequest.Post(serverUrl+"app.php", form);
yield return www.SendWebRequest();
if ( www.result!=UnityWebRequest.Result.Success )
{
m_busy = false;
OnDownload(ERROR_UNKNOWN, null);
yield break;
}
JsonObj json = Json.__376(Json.__373(www.downloadHandler.data));
if ( json==null )
{
OnDownload(ERROR_UNKNOWN, null);
yield break;
}
if ( json.GetInt("error")!=ERROR_NONE || CheckServerVersion(json.GetInt("version"))==false )
{
m_busy = false;
OnDownload(json.GetInt("error"), null);
yield break;
}
string url = serverUrl + json.GetString("param");
url += "?x=" + G.__156(1000000).ToString() + DateTime.Now.Millisecond.ToString();
www = UnityWebRequest.Get(url);
yield return www.SendWebRequest();
if ( www.result!=UnityWebRequest.Result.Success )
{
m_busy = false;
OnDownload(ERROR_UNKNOWN, null);
yield break;
}
m_busy = false;
OnDownload(ERROR_NONE, www.downloadHandler.data);
}
IEnumerator UploadProc(byte[] zip)
{
string serverUrl = G.m_game.m_savegameServerUrl + "age/";
WWWForm form = new WWWForm();
form.AddField("version", G.SAVEGAME_SERVER_VERSION.ToString());
form.AddField("action", "upload");
form.AddField("user", G.m_game.m_savegameServerUser.ToLower());
form.AddField("pass", G.m_game.m_savegameServerPass.ToLower());
form.AddField("game", G.m_game.m_savegameServerGame);
form.AddField("data", Convert.ToBase64String(zip));
UnityWebRequest www = UnityWebRequest.Post(serverUrl+"app.php", form);
yield return www.SendWebRequest();
if ( www.result!=UnityWebRequest.Result.Success )
{
m_busy = false;
OnUpload(ERROR_UNKNOWN);
yield break;
}
JsonObj json = Json.__376(Json.__373(www.downloadHandler.data));
if ( json==null )
{
m_busy = false;
OnUpload(ERROR_UNKNOWN);
yield break;
}
if ( json.GetInt("error")!=ERROR_NONE || CheckServerVersion(json.GetInt("version"))==false )
{
m_busy = false;
OnUpload(json.GetInt("error"));
yield break;
}
m_busy = false;
OnUpload(ERROR_NONE);
}
IEnumerator ConnectProc(string user, string pass)
{
string serverUrl = G.m_game.m_savegameServerUrl;
WWWForm form = new WWWForm();
form.AddField("version", G.SAVEGAME_SERVER_VERSION.ToString());
form.AddField("action", "connect");
form.AddField("user", user.ToLower());
form.AddField("pass", pass.ToLower());
UnityWebRequest www = UnityWebRequest.Post(serverUrl+"app.php", form);
yield return www.SendWebRequest();
if ( www.result!=UnityWebRequest.Result.Success )
{
m_busy = false;
OnConnect(ERROR_UNKNOWN, user, pass);
yield break;
}
JsonObj json = Json.__376(Json.__373(www.downloadHandler.data));
if ( json==null )
{
m_busy = false;
OnConnect(ERROR_UNKNOWN, user, pass);
yield break;
}
if ( json.GetInt("error")!=ERROR_NONE || CheckServerVersion(json.GetInt("version"))==false )
{
m_busy = false;
OnConnect(json.GetInt("error"), user, pass);
yield break;
}
m_busy = false;
OnConnect(ERROR_NONE, user, pass);
}
IEnumerator SignupProc(string user)
{
string serverUrl = G.m_game.m_savegameServerUrl;
WWWForm form = new WWWForm();
form.AddField("site", serverUrl);
form.AddField("version", G.SAVEGAME_SERVER_VERSION.ToString());
form.AddField("action", "signup");
form.AddField("user", user.ToLower());
form.AddField("pass", "0000000000");
form.AddField("app", G.m_game.m_gameNameForFile);
form.AddField("os", G.m_game.__204());
UnityWebRequest www = UnityWebRequest.Post(serverUrl+"app.php", form);
yield return www.SendWebRequest();
if ( www.result!=UnityWebRequest.Result.Success )
{
m_busy = false;
OnSignup(ERROR_UNKNOWN, user);
yield break;
}
JsonObj json = Json.__376(Json.__373(www.downloadHandler.data));
if ( json==null )
{
m_busy = false;
OnSignup(ERROR_UNKNOWN, user);
yield break;
}
if ( json.GetInt("error")!=ERROR_NONE || CheckServerVersion(json.GetInt("version"))==false )
{
m_busy = false;
OnSignup(json.GetInt("error"), user);
yield break;
}
m_busy = false;
OnSignup(ERROR_NONE, user);
}
IEnumerator ResetPasswordProc(string user)
{
string serverUrl = G.m_game.m_savegameServerUrl;
WWWForm form = new WWWForm();
form.AddField("site", serverUrl);
form.AddField("version", G.SAVEGAME_SERVER_VERSION.ToString());
form.AddField("action", "reset");
form.AddField("user", user.ToLower());
form.AddField("pass", "0000000000");
UnityWebRequest www = UnityWebRequest.Post(serverUrl+"app.php", form);
yield return www.SendWebRequest();
if ( www.result!=UnityWebRequest.Result.Success )
{
m_busy = false;
OnResetPassword(ERROR_UNKNOWN, user);
yield break;
}
JsonObj json = Json.__376(Json.__373(www.downloadHandler.data));
if ( json==null )
{
m_busy = false;
OnResetPassword(ERROR_UNKNOWN, user);
yield break;
}
if ( json.GetInt("error")!=ERROR_NONE || CheckServerVersion(json.GetInt("version"))==false )
{
m_busy = false;
OnResetPassword(json.GetInt("error"), user);
yield break;
}
m_busy = false;
OnResetPassword(ERROR_NONE, user);
}
IEnumerator DeleteAccountProc(string user, string pass)
{
string serverUrl = G.m_game.m_savegameServerUrl;
WWWForm form = new WWWForm();
form.AddField("site", serverUrl);
form.AddField("version", G.SAVEGAME_SERVER_VERSION.ToString());
form.AddField("action", "delete");
form.AddField("user", user.ToLower());
form.AddField("pass", pass.ToLower());
UnityWebRequest www = UnityWebRequest.Post(serverUrl+"app.php", form);
yield return www.SendWebRequest();
if ( www.result!=UnityWebRequest.Result.Success )
{
m_busy = false;
OnDeleteAccount(ERROR_UNKNOWN, user);
yield break;
}
JsonObj json = Json.__376(Json.__373(www.downloadHandler.data));
if ( json==null )
{
m_busy = false;
OnDeleteAccount(ERROR_UNKNOWN, user);
yield break;
}
if ( json.GetInt("error")!=ERROR_NONE || CheckServerVersion(json.GetInt("version"))==false )
{
m_busy = false;
OnDeleteAccount(json.GetInt("error"), user);
yield break;
}
m_busy = false;
OnDeleteAccount(ERROR_NONE, user);
}
}
