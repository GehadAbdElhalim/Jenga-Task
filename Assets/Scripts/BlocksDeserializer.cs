using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace JengaTask
{
    public class BlocksDeserializer : MonoBehaviour
    {
        private const string API_URL = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";
        private Action<List<Block>> callback;

        public void GetBlocksFromAPI(Action<List<Block>> callback)
        {
            StartCoroutine(FetchDataFromAPI());
            this.callback = callback;
        }

        private IEnumerator FetchDataFromAPI()
        {
            UnityWebRequest request = UnityWebRequest.Get(API_URL);

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
                callback?.Invoke(new List<Block>());
            }
            else
            {
                string jsonResult = request.downloadHandler.text;
                callback?.Invoke(JsonConvert.DeserializeObject<List<Block>>(jsonResult));
            }
        }
    }
}