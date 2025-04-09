using UnityEngine;
public static class AgePlugin
{
public static bool OnAppInit()
{
return true;
}
public static void OnAppQuit()
{
Application.Quit();
}
public static void OnAppUpdate()
{
}
public static bool IsLoadEnabled()
{
return true;
}
public static bool IsSaveEnabled()
{
return true;
}
public static bool OnUserButton(int index, string url)
{
return true;
}
public static bool Callback(string param1, string param2)
{
return true;
}
public static void Interlude(string param)
{
}
public static bool OnInterludeDraw()
{
return false;
}
}
