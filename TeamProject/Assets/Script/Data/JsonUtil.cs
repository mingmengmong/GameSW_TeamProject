using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

namespace Script.Data {
    internal class JsonUtil {
        public JsonUtil() {
        }

        public static T ToObject<T>(JsonData data) {
            T child = JsonMapper.ToObject<T>(JsonMapper.ToJson(data));
            return child;
        }

        public static T ToObject<T>(string jsonString) {
            //JsonData data = JsonMapper.ToObject(jsonString);
            //string childJson = JsonMapper.ToJson(data["Child"]);
            T child = JsonMapper.ToObject<T>(jsonString);
            return child;
        }
        public static T ToObject<T>(string jsonString, string subsetKey) {
            string nestedJson = ExtractNestedJsonString(jsonString, subsetKey);
            if (nestedJson != null) {
                return JsonMapper.ToObject<T>(nestedJson);
            } else {
                return default(T);
            }
        }
        public static string ExtractNestedJsonString(string jsonString, string subsetKey) {
            JsonData data = JsonMapper.ToObject(jsonString);
            if (data.Keys.Contains(subsetKey)) {
                return JsonMapper.ToJson(data[subsetKey]);
            } else {
                return null;
            }
        }
        public static JsonData ToJsonData(string jsonString) {
            JsonData data = JsonMapper.ToObject(jsonString);
            return data;
        }

        public static string ToJson(object obj) {
            string jsonString = JsonMapper.ToJson(obj);
            return jsonString;
        }



        public static Dictionary<string, object> ToDictionary(string jsonString) {

            JsonReader reader = new JsonReader(jsonString);

            Dictionary<string, object> wrapper = new Dictionary<string, object>();
            JsonToDictionary(reader, wrapper, "root");
            return (Dictionary<string, object>)wrapper["root"];
        }

        public static Dictionary<string, object> ToDictionary(string jsonString, string subsetKey) {
            JsonData data = JsonMapper.ToObject(jsonString);
            string substring = JsonMapper.ToJson(data[subsetKey]);
            return ToDictionary(substring);
        }


        private static void JsonToDictionary(JsonReader reader, object obj, string key) {

            while (reader.Read()) {
                var token = reader.Token;

                if (token.Equals(LitJson.JsonToken.ObjectStart)) {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    addJsonValue(obj, key, dic);
                    JsonToDictionary(reader, dic, key);
                } else if (token.Equals(LitJson.JsonToken.ArrayStart)) {
                    List<object> list = new List<object>();
                    addJsonValue(obj, key, list);
                    JsonToDictionary(reader, list, key);
                } else if (token.Equals(LitJson.JsonToken.PropertyName)) {
                    key = reader.Value.ToString();
                    JsonToDictionary(reader, obj, key);
                    return;
                } else if (token.Equals(LitJson.JsonToken.ObjectEnd) ||
                          token.Equals(LitJson.JsonToken.ArrayEnd)) {
                    return;
                } else {
                    object value = token.Equals(LitJson.JsonToken.Null) ? "" : reader.Value;
                    addJsonValue(obj, key, value);
                }
            }
        }

        private static void addJsonValue(object obj, string key, object value) {

            if (obj is Dictionary<string, object>) {
                Dictionary<string, object> dic = (Dictionary<string, object>)obj;
                dic.Add(key, value);


            } else if (obj is System.Collections.Generic.List<object>) {
                List<object> list = (List<object>)obj;
                list.Add(value);
            }

        }


        public static string ConvertToJsonString(Dictionary<string, object> dic) {

            return JsonMapper.ToJson(dic);
        }

    }
}