using UnityEngine;

namespace MWP.Misc
{
    public class AutoDestroy : MonoBehaviour
    {
        public void DestroyThis()
        {
            Destroy(gameObject);
        }
    }
}