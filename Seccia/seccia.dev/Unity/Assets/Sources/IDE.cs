using UnityEngine;
using System.Runtime.InteropServices;
#if UNITY_STANDALONE_WIN
public enum IDE_MSG
{
NONE,
RESET,
SCENARIO_BOX_ENTER,
SCENARIO_BOX_EXIT,
SCENARIO_BOX_MISSED,
SCENARIO_SIGNAL_PASSED,
SCENARIO_SIGNAL_MISSED,
ROLE_RESET,
ROLE_ENTER,
ROLE_EXIT,
ROLE_SIGNAL,
ROLE_OPEN,
}
public static class IDE
{
public static float m_time = 0;
public static ulong m_hWnd = 0;
[DllImport("user32.dll", CharSet = CharSet.Ansi)]
public static extern int PostMessageA(ulong hWnd, uint msg, ulong wParam, long lParam);
[DllImport("user32.dll", CharSet = CharSet.Ansi)]
public static extern ulong GetWindowLongPtrA(ulong hWnd, int index);
public static void Post(IDE_MSG msg, int asset = 0, int id = 0, int p1 = 0, int p2 = 0, int p3 = 0)
{
if ( G.m_game.m_configRun==false )
return;
if ( m_hWnd==0 )
return;
ulong wParam = (((ulong)msg))<<56 | (((ulong)asset)<<32) | (((ulong)id)<<8) | ((ulong)p1>>16);
ulong lParam = (((ulong)p1&0xFFFF)<<48) | ((ulong)p2<<24) | (uint)p3;
PostMessageA(m_hWnd, 33268, wParam, (long)lParam);
}
}
#endif
