using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace common {
    public static class Extensions {

        //private static UnityEngine.Random random = new UnityEngine.Random();  //UnityEngine.Random((int)DateTime.Now.Ticks & 0x0000FFFF);

        #region Transform Methods
        public static void ResetTransform(this Transform trans) {
            trans.localPosition = Vector3.zero;
            trans.localRotation = Quaternion.identity;
            trans.localScale = Vector3.one;
        }
        public static void ResetLocalPosition(this Transform trans) {
            trans.localPosition = Vector3.zero;
        }
        public static void ResetLocalScale(this Transform trans) {
            trans.localScale = Vector3.one;
        }
        public static void SetLocalY(this Transform trans, float y) {
            trans.localPosition = new Vector2(trans.transform.localPosition.x, y);
        }
        public static float GetLocalY(this Transform trans) {
            return trans.localPosition.y;
        }
        #endregion

        #region GameObject Methods
        public static T GetInterface<T>(this GameObject obj) where T : class {
            T iInterface = obj.GetComponent(typeof(T)) as T;
            return iInterface;
        }
        #endregion

        #region MonoBehaviour Methods
        public static void StartCoroutineWithCallback(this MonoBehaviour monoBehaviour, IEnumerator ie, Action callback) {
            monoBehaviour.StartCoroutine( monoBehaviour.WaitForCoroutine_(ie, callback) );
        }
        public static IEnumerator WaitForCoroutine_(this MonoBehaviour monoBehaviour, IEnumerator ie, Action callback) {
            yield return monoBehaviour.StartCoroutine(ie);
            callback();
        }
        public static IEnumerator Wait(this MonoBehaviour monoBehaviour, float delay, Action callback) {
            yield return new WaitForSeconds(delay);
            callback();
        }
        #endregion

        #region List Methods
        public static void Shuffle<T>(this IList<T> list) {
            //Random rng = new Random((int) DateTime.Now.Ticks & 0x0000FFFF);
            int n = list.Count;
            while (n > 1) {
                n--;
                //UnityEngine.Random.Range(0, n + 1);
                //int k = random.Next(n + 1);
                int k = UnityEngine.Random.Range(0, n + 1);

                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T SelectRandomOne<T>(this IList<T> list, bool remove = true) {
            if (list == null || list.Count <= 0) {
                return default(T);
            }
            
            int k = UnityEngine.Random.Range(0, list.Count);
            T val = list[k];
            if (remove) {
                list.RemoveAt(k);
            }            

            return val;
        }

        public static List<T> SelectRandomList<T>(this IList<T> list, int count) {
            List<T> results = new List<T>(list);

            if (list == null || list.Count <= 0) {
                return results;
            }

            results.Shuffle<T>();
            //Shuffle<T>(results);

            if (results.Count < count) {
                return results;
            } else {
                return results.GetRange(0, count);
            }
        }
        public static List<T> Clone<T>(this List<T> listToClone) where T : ICloneable {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
        public static T GetLast<T>(this List<T> list ) {
            if (list.Count == 0) {
                return default(T);
            } else {
                return list[ list.Count-1 ];
            }
        }
        public static string ToString<T>(this List<T> list, string delemeter) {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach ( T t in list  ) {
                sb.Append(t.ToString()).Append( delemeter );
            }
            return sb.ToString();
        }
        #endregion



    }

}