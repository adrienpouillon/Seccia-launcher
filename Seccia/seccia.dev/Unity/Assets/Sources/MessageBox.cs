using UnityEngine;
using System.Collections.Generic;
public class MessageBox
{
enum FIELD
{
EDIT,
CHECKBOX,
};
public static MessageBox m_instance = null;
public const float ICONSIZE = 96.0f;
private bool m_isActive = false;
private POPUP m_id = POPUP.DEFAULT;
private string m_name = "";
private string m_title = "";
private string m_message = "";
bool m_showMessageAtTop = true;
RoleBox m_roleBox;
int m_roleBoxToken;
private List<FIELD> m_inputTypes = new List<FIELD>();
private List<string> m_inputNames = new List<string>();
private List<string> m_inputValues = new List<string>();
private List<bool> m_inputPasswords = new List<bool>();
private List<bool> m_inputChecks = new List<bool>();
private List<Rect> m_inputRects = new List<Rect>();
private Material m_material = null;
private Material m_materialIcon = null;
private Texture2D m_textureCheck = null;
private Texture2D m_textureCross = null;
private Rect m_rcCheck = new Rect();
private Rect m_rcCross = new Rect();
private float m_checkboxSize = 0.0f;
private int m_iInput = -1;
public static implicit operator bool(MessageBox inst) { return inst!=null; }
public void Init()
{
m_instance = this;
m_material = G.__165(SHADER.BRUSH);
m_materialIcon = G.__165(SHADER.TEXT);
m_textureCheck = (Texture2D)Resources.Load("Textures/icon_check", typeof(Texture2D));
m_textureCross = (Texture2D)Resources.Load("Textures/icon_cross", typeof(Texture2D));
m_rcCheck.width = ICONSIZE;
m_rcCheck.height = ICONSIZE;
m_rcCross.width = ICONSIZE;
m_rcCross.height = ICONSIZE;
}
public bool Open(string name, string title, string message, string[] fields = null, POPUP id = POPUP.DEFAULT, bool showMessageAtTop = true, RoleBox roleBox = null)
{
if ( __38() )
return false;
m_id = id;
m_name = name;
m_title = title;
if ( m_title.Length==0 )
m_title = G.m_game.m_gameName;
m_message = message;
m_showMessageAtTop = showMessageAtTop;
m_roleBox = roleBox;
m_roleBoxToken = roleBox==null ? 0 : roleBox.m_parent.m_token;
m_iInput = -1;
switch ( m_id )
{
case POPUP.SIGNUP:
m_inputTypes.Add(FIELD.EDIT);
m_inputTypes.Add(FIELD.CHECKBOX);
m_inputChecks.Add(false);
m_inputChecks.Add(false);
for ( int i=0 ; i<4 ; i+=2 )
{
m_inputNames.Add(fields[i]);
m_inputValues.Add(fields[i+1]);
m_inputPasswords.Add(false);
m_inputRects.Add(Rect.Zero);
}
break;
case POPUP.EMAIL:
m_inputTypes.Add(FIELD.EDIT);
m_inputNames.Add(fields[0]);
m_inputValues.Add(fields[1]);
m_inputPasswords.Add(false);
m_inputChecks.Add(false);
m_inputRects.Add(Rect.Zero);
break;
case POPUP.PASSWORD:
m_inputTypes.Add(FIELD.EDIT);
m_inputNames.Add(fields[0]);
m_inputValues.Add(fields[1]);
m_inputPasswords.Add(true);
m_inputChecks.Add(false);
m_inputRects.Add(Rect.Zero);
break;
case POPUP.LOGIN:
m_inputTypes.Add(FIELD.EDIT);
m_inputNames.Add(fields[0]);
m_inputValues.Add(fields[1]);
m_inputPasswords.Add(false);
m_inputChecks.Add(false);
m_inputRects.Add(Rect.Zero);
m_inputTypes.Add(FIELD.EDIT);
m_inputNames.Add(fields[2]);
m_inputValues.Add(fields[3]);
m_inputPasswords.Add(true);
m_inputChecks.Add(false);
m_inputRects.Add(Rect.Zero);
break;
}
m_isActive = true;
return true;
}
public void Close()
{
if ( __38()==false )
return;
m_isActive = false;
m_id = POPUP.DEFAULT;
m_name = "";
m_title = "";
m_message = "";
m_roleBox = null;
m_roleBoxToken = 0;
m_inputTypes.Clear();
m_inputNames.Clear();
m_inputValues.Clear();
m_inputPasswords.Clear();
m_inputChecks.Clear();
m_inputRects.Clear();
m_iInput = -1;
}
public bool __38()
{
return m_isActive;
}
private bool HasDefaultInputText(int index)
{
return m_inputValues[index].Length==0 && m_inputNames[index].Length>0 && m_iInput!=index;
}
private string GetInputText(int index)
{
if ( m_inputValues[index].Length==0 )
{
if ( m_iInput==index )
return  "«  »";
return m_inputNames[index];
}
if ( m_inputPasswords[index]==false )
{
if ( m_iInput==index )
return  "« " + m_inputValues[index] + " »";
return m_inputValues[index];
}
int len = m_inputValues[index].Length;
string text = "";
for ( int i=0 ; i<len ; i++ )
text += (char)149;
if ( m_iInput==index )
return  "« " + text + " »";
return text;
}
public void __467(POPUPANSWER answer)
{
m_iInput = -1;
string name = m_name;
string[] values = m_inputValues.ToArray();
RoleBox roleBox = m_roleBox;
int roleBoxToken = m_roleBoxToken;
Close();
if ( name.Length==0 )
{
if ( roleBox )
{
RoleBoxActionPopup popup = (RoleBoxActionPopup)roleBox;
if ( popup.__457(roleBoxToken) )
popup.m_answer = answer;
}
return;
}
G.m_game.__333(name, answer, values);
}
public void __457(float x, float y)
{
if ( KeyboardBehavior.m_instance.IsBusy() )
{
return;
}
for ( int i=0 ; i<m_inputRects.Count ; i++ )
{
if ( m_inputRects[i].Contains(x, y) )
{
switch ( m_inputTypes[i] )
{
case FIELD.EDIT:
{
if ( G.__86() )
m_iInput = i;
else
{
if ( m_inputPasswords[i] )
KeyboardBehavior.m_instance.OpenPassword(new KeyboardBehavior.OnClose(Callback_Keyboard), i, m_inputValues[i]);
else
KeyboardBehavior.m_instance.OpenEmail(new KeyboardBehavior.OnClose(Callback_Keyboard), i, m_inputValues[i]);
}
break;
}
case FIELD.CHECKBOX:
{
m_inputChecks[i] = !m_inputChecks[i];
break;
}
}
return;
}
}
switch ( m_id )
{
case POPUP.DEFAULT:
{
if ( m_rcCross.Contains(x, y) )
__467(POPUPANSWER.OK);
break;
}
case POPUP.QUESTION:
{
if ( m_rcCheck.Contains(x, y) )
__467(POPUPANSWER.YES);
else if ( m_rcCross.Contains(x, y) )
__467(POPUPANSWER.NO);
break;
}
case POPUP.EMAIL:
case POPUP.PASSWORD:
case POPUP.LOGIN:
{
if ( m_rcCheck.Contains(x, y) )
{
if ( m_inputValues[0].Length>0 )
__467(POPUPANSWER.YES);
}
else if ( m_rcCross.Contains(x, y) )
__467(POPUPANSWER.NO);
break;
}
case POPUP.SIGNUP:
{
if ( m_rcCheck.Contains(x, y) )
{
if ( m_inputValues[0].Length>0 && m_inputChecks[1] )
__467(POPUPANSWER.YES);
}
else if ( m_rcCross.Contains(x, y) )
__467(POPUPANSWER.NO);
break;
}
}
}
public void Callback_Keyboard(bool success, int param, string text)
{
if ( success )
{
m_inputValues[param] = text;
}
}
public void __42()
{
if ( G.__86() )
{
if ( m_iInput==-1 )
return;
if ( Input.GetKeyDown(KeyCode.Tab) )
{
m_iInput++;
if ( m_iInput>=m_inputValues.Count )
m_iInput = -1;
return;
}
string input = Input.inputString.ToLower();
if ( input.Length>0 )
{
foreach ( char c in input )
{
switch ( c )
{
case '\b':
m_inputValues[m_iInput] = m_inputValues[m_iInput].Substring(0, m_inputValues[m_iInput].Length-1);
break;
case '\r':
case '\n':
m_iInput = -1;
break;
case '`':
m_inputValues[m_iInput] += "@";
break;
case '.':
case '-':
case '_':
case '@':
m_inputValues[m_iInput] += c;
break;
default:
if ( c>=48 && c<58 )
m_inputValues[m_iInput] += c;
else if ( c>=97 && c<123 )
m_inputValues[m_iInput] += c;
break;
}
if ( m_iInput==-1 )
break;
}
}
}
}
public void __43()
{
const int space = 4;
Police font = G.m_game.__215();
bool leftToRight = font.__489();
int maxWidth = (int)(G.m_rcViewUI.width*0.8);
Vec2 center = G.m_rcViewUI.__438();
m_checkboxSize = font.__491("O").y;
Color okColor = new Color(1.0f, 1.0f, 1.0f);
Color yesColor = new Color(1.0f, 1.0f, 1.0f);
Color noColor = new Color(1.0f, 0.0f, 0.0f);
Color disableColor = new Color(0.25f, 0.25f, 0.25f);
Color viewColor = new Color(0.0f, 0.0f, 0.0f, 0.8f);
Color borderColor = new Color(0.8f, 0.8f, 0.8f);
Color titleBackColor = new Color(0.05f, 0.05f, 0.05f);
Color titleTextColor = new Color(1.0f, 1.0f, 1.0f);
Color bodyBackColor = new Color(0.2f, 0.2f, 0.2f);
Color messageTextColor = new Color(1.0f, 1.0f, 1.0f);
Color inputBackColor = new Color(0.4f, 0.4f, 0.4f);
Color inputTextColor = new Color(1.0f, 1.0f, 1.0f);
Color inputTextAltColor = new Color(0.6f, 0.6f, 0.6f);
BreakTextInfo btiTitle = new BreakTextInfo();
G.__161(btiTitle, m_title, font, maxWidth);
float titleWidth = btiTitle.m_paraSizes[0].x;
float titleHeight = btiTitle.m_paraSizes[0].y + space*2.0f;
float bodyWidth = titleWidth;
float bodyHeight = 0.0f;
float msgScale = m_showMessageAtTop ? -1.0f : font.__34()*0.8f;
BreakTextInfo btiMessage = new BreakTextInfo();
G.__161(btiMessage, m_message, font, maxWidth, msgScale);
float messageWidth = 0.0f;
float messageHeight = 0.0f;
for ( int i=0 ; i<btiMessage.__66() ; i++ )
{
if ( btiMessage.m_paraSizes[i].x>messageWidth )
messageWidth = btiMessage.m_paraSizes[i].x;
if ( btiMessage.m_paraSizes[i].y>messageHeight )
messageHeight = btiMessage.m_paraSizes[i].y;
}
messageHeight = space*2 + messageHeight + space*4;
if ( messageWidth>bodyWidth )
bodyWidth = messageWidth;
bodyHeight += messageHeight;
float inputWidth = maxWidth - 100.0f;
if ( inputWidth<500.0f )
inputWidth = 500.0f;
float inputHeight = titleHeight;
BreakTextInfo[] btiInputs = new BreakTextInfo[m_inputValues.Count];
if ( m_inputValues.Count>0 )
{
for ( int i=0 ; i<m_inputValues.Count ; i++ )
{
bool isEdit = m_inputTypes[i]!=FIELD.CHECKBOX;
btiInputs[i] = new BreakTextInfo();
float maxLineWidth = isEdit ? inputWidth : inputWidth-m_checkboxSize-space*2.0f;
G.__161(btiInputs[i], GetInputText(i), font, maxLineWidth, -1.0f, isEdit ? READING.CENTER : READING.NEAR);
btiInputs[i].__200();
if ( isEdit )
{
btiInputs[i].__201();
btiInputs[i].__203(font);
}
m_inputRects[i] = new Rect(0.0f, 0.0f, inputWidth, inputHeight*btiInputs[i].__66());
bodyHeight += inputHeight*btiInputs[i].__66() + space*2.0f;
}
if ( inputWidth>bodyWidth )
bodyWidth = inputWidth;
bodyHeight += space*2.0f;
}
bodyHeight += ICONSIZE + space*2.0f;
float borderWidth = titleWidth>bodyWidth ? titleWidth : bodyWidth;
borderWidth += space * 2.0f;
float borderHeight = space + titleHeight + bodyHeight + space;
if ( m_showMessageAtTop==false )
{
bodyHeight += space*2.0f;
borderHeight += space*2.0f;
}
Rect rcBorder = new Rect(center.x-borderWidth*0.5f, center.y-borderHeight*0.5f, borderWidth, borderHeight);
Rect rcTitle = new Rect(rcBorder.x+space, rcBorder.y+space, rcBorder.width-space*2.0f, titleHeight);
Rect rcMessage = new Rect(rcTitle.x, rcTitle.__437(), rcTitle.width, messageHeight);
for ( int i=0 ; i<m_inputValues.Count ; i++ )
{
Rect rc = m_inputRects[i];
rc.x = center.x - inputWidth*0.5f;
if ( i==0 )
rc.y = m_showMessageAtTop ? rcMessage.__437() : rcTitle.__437()+space*2.0f;
else
rc.y = m_inputRects[i-1].__437() + space*2.0f;
m_inputRects[i] = rc;
}
if ( m_showMessageAtTop==false && m_inputValues.Count>0 )
rcMessage.y = m_inputRects[m_inputValues.Count-1].__437();
Rect rcBody = new Rect(rcTitle.x, rcTitle.__437(), bodyWidth, bodyHeight);
m_material.color = viewColor;
G.m_graphics.__354(m_material, ref G.m_rcViewUI);
m_material.color = borderColor;
G.m_graphics.__354(m_material, ref rcBorder);
m_material.color = titleBackColor;
G.m_graphics.__354(m_material, ref rcTitle);
m_material.color = bodyBackColor;
G.m_graphics.__354(m_material, ref rcBody);
for ( int i=0 ; i<m_inputValues.Count ; i++ )
{
m_material.color = inputBackColor;
Rect rc = m_inputRects[i];
G.m_graphics.__354(m_material, ref rc);
}
if ( leftToRight )
font.__70(btiTitle.m_texts[0], center.x-btiTitle.m_lineRects[0].width*0.5f, rcTitle.y+space, ref titleTextColor);
else
font.__70(btiTitle.m_texts[0], center.x+btiTitle.m_lineRects[0].width*0.5f, rcTitle.y+space, ref titleTextColor);
for ( int i=0 ; i<btiMessage.__66() ; i++ )
{
float x = leftToRight ? center.x-btiMessage.m_lineRects[i].width*0.5f : center.x+btiMessage.m_lineRects[i].width*0.5f;
float y = rcMessage.y + space*2.0f + btiMessage.m_lineRects[i].y;
font.__70(btiMessage.m_texts[i], x, y, ref messageTextColor, msgScale);
}
for ( int i=0 ; i<btiInputs.Length ; i++ )
{
Color color = HasDefaultInputText(i) ? inputTextAltColor : inputTextColor;
BreakTextInfo btiInput = btiInputs[i];
for ( int j=0 ; j<btiInput.__66() ; j++ )
{
float x = center.x - inputWidth*0.5f + btiInput.m_lineRects[j].x;
if ( leftToRight==false )
x = center.x + inputWidth*0.5f - (inputWidth-btiInput.m_lineRects[j].__436());
if ( m_inputTypes[i]==FIELD.CHECKBOX )
x += m_checkboxSize + space*2.0f;
float y = m_inputRects[i].y + btiInput.m_lineRects[j].y;
font.__70(btiInput.m_texts[j], x, y, ref color);
}
}
for ( int i=0 ; i<btiInputs.Length ; i++ )
{
if ( m_inputTypes[i]!=FIELD.CHECKBOX )
continue;
Rect rc = m_inputRects[i];
rc.x += space;
rc.width = m_checkboxSize;
rc.y += space;
rc.height = m_checkboxSize;
m_material.color = bodyBackColor;
G.m_graphics.__354(m_material, ref rc);
if ( m_inputChecks[i] )
{
m_materialIcon.color = inputTextColor;
m_materialIcon.mainTexture = m_textureCheck;
G.m_graphics.__354(m_materialIcon, ref rc);
}
}
float iconsY = rcBody.__437() - ICONSIZE - space*2.0f;
switch ( m_id )
{
case POPUP.DEFAULT:
{
m_rcCross.x = center.x - ICONSIZE*0.5f;
m_rcCross.y = iconsY;
m_materialIcon.color = okColor;
m_materialIcon.mainTexture = m_textureCross;
G.m_graphics.__354(m_materialIcon, ref m_rcCross);
break;
}
case POPUP.QUESTION:
case POPUP.EMAIL:
case POPUP.PASSWORD:
case POPUP.LOGIN:
case POPUP.SIGNUP:
{
#if UNITY_STANDALONE_WIN || UNITY_WEBGL
m_rcCheck.x = center.x - ICONSIZE - ICONSIZE*0.5f;
m_rcCheck.y = iconsY;
m_rcCross.x = center.x + ICONSIZE*0.5f;
m_rcCross.y = iconsY;
#else
m_rcCross.x = center.x - ICONSIZE - ICONSIZE*0.5f;
m_rcCross.y = iconsY;
m_rcCheck.x = center.x + ICONSIZE*0.5f;
m_rcCheck.y = iconsY;
#endif
bool showYes = false;
switch ( m_id )
{
case POPUP.EMAIL:
case POPUP.PASSWORD:
case POPUP.LOGIN:
if ( m_inputValues[0].Length>0 )
showYes = true;
break;
case POPUP.SIGNUP:
if ( m_inputValues[0].Length>0 && m_inputChecks[1] )
showYes = true;
break;
default:
showYes = true;
break;
}
m_materialIcon.color = showYes ? yesColor : disableColor;
m_materialIcon.mainTexture = m_textureCheck;
G.m_graphics.__354(m_materialIcon, ref m_rcCheck);
m_materialIcon.color = noColor;
m_materialIcon.mainTexture = m_textureCross;
G.m_graphics.__354(m_materialIcon, ref m_rcCross);
break;
}
}
}
}
