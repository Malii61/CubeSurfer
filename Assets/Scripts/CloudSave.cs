using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using Newtonsoft.Json;
using System.Linq;

public class CloudSave : MonoBehaviour
{
    private async Task Awake()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        //List<string> keys = await _client.RetrieveAllKeysAsync();

        //for (int i = 0; i < keys.Count; i++)
        //{
        //    Debug.Log(keys[i]);
        //}
    }
    public static async Task Save(string key, object value)
    {
        var data = new Dictionary<string, object> { { key, value } };
        await CloudSaveService.Instance.Data.ForceSaveAsync(data);
        Debug.Log("saving");
    }

    public static async Task Save(params (string key, object value)[] values)
    {
        var data = values.ToDictionary(item => item.key, item => item.value);
        await Call(CloudSaveService.Instance.Data.ForceSaveAsync(data));
    }

    public static async Task<T> Load<T>(string key)
    {
        var query = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { key });
        if (query == null)
            Debug.Log("yok");
        return query.TryGetValue(key, out var value) ? Deserialize<T>(value) : default;
    }
    private static T Deserialize<T>(string input)
    {
        if (typeof(T) == typeof(string)) return (T)(object)input;
        return JsonConvert.DeserializeObject<T>(input);
    }
    private static async Task Call(Task action)
    {
        try
        {
            await action;
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogWarning(e);
        }
    }
    private static async Task<T> Call<T>(Task<T> action)
    {
        try
        {
            return await action;
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogWarning(e);
        }

        return default;
    }
}
