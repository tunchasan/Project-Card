using System.Diagnostics;

namespace ProjectCard.Game.Debug
{
    public static class ConditionalDebug
    {
        [Conditional( "UNITY_EDITOR" )] public static void Log( object message ) { UnityEngine.Debug.Log( message ); }
        [Conditional( "UNITY_EDITOR" )] public static void LogWarning( object message ) { UnityEngine.Debug.LogWarning( message ); }
        [Conditional( "UNITY_EDITOR" )] public static void LogError( object message ) { UnityEngine.Debug.LogError( message ); }
        [Conditional( "UNITY_EDITOR" )] public static void LogException( System.Exception exception ) { UnityEngine.Debug.LogException( exception ); }
    }
}