using UnityEngine;
public class KeyboardBehavior:MonoBehaviour
{
public static KeyboardBehavior m_instance = null;
public delegate void OnClose(bool success, int param, string text);
private TouchScreenKeyboard m_keyboard = null;
private KeyboardBehavior.OnClose m_callback = null;
private int m_param = 0;
private string m_text = "";
void Awake()
{
m_instance = this;
}
void Update()
{
if ( m_keyboard!=null )
{
switch ( m_keyboard.status )
{
case TouchScreenKeyboard.Status.Done:
{
m_text = m_keyboard.text;
m_keyboard = null;
m_callback(true, m_param, m_text);
break;
}
case TouchScreenKeyboard.Status.Canceled:
{
m_text = m_keyboard.text;
m_keyboard = null;
m_callback(false, m_param, m_text);
break;
}
case TouchScreenKeyboard.Status.LostFocus:
{
m_text = m_keyboard.text;
m_keyboard = null;
m_callback(false, m_param, m_text);
break;
}
case TouchScreenKeyboard.Status.Visible:
{
m_text = m_keyboard.text;
break;
}
}
}
}
public bool IsBusy()
{
return m_keyboard!=null;
}
public void OpenEmail(KeyboardBehavior.OnClose callback, int param, string text = "")
{
m_callback = callback;
m_param = param;
m_text = text;
m_keyboard = TouchScreenKeyboard.Open(m_text, TouchScreenKeyboardType.EmailAddress, false, false, false, false, "", 50);
}
public void OpenPassword(KeyboardBehavior.OnClose callback, int param, string text = "")
{
m_callback = callback;
m_param = param;
m_text = text;
m_keyboard = TouchScreenKeyboard.Open(m_text, TouchScreenKeyboardType.ASCIICapable, false, false, true, false, "", 10);
}
}
