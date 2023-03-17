using UnityEngine;
namespace Symbiotic.Core
{

    public class LogService : ILog
    {
        public bool b;
        public void Log(string msg)
        {
#if UNITY_EDITOR
            Debug.Log(msg);
#endif
        }
    }
}