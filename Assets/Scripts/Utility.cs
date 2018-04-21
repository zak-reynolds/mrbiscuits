using UnityEngine;

namespace Assets.Scripts
{
    public static class Utility
    {
        public static void PhysicalDestroy(GameObject gob)
        {
            gob.transform.position = new Vector3(0, -1000, 0);
            GameObject.Destroy(gob, 0.1f);
        }
    }
}
