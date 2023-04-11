using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Unity.Services.CloudSave;
using Newtonsoft.Json;
using System.Linq;

public class CloudSave : MonoBehaviour
{
    public static async Task Save(string key, object value)
    {
        var data = new Dictionary<string, object> { { key, value } };
        await CloudSaveService.Instance.Data.ForceSaveAsync(data);
        List<string> keys = await CloudSaveService.Instance.Data.RetrieveAllKeysAsync();
    }

    public static async Task Save(params (string key, object value)[] values)
    {
        var data = values.ToDictionary(item => item.key, item => item.value);
        await Call(CloudSaveService.Instance.Data.ForceSaveAsync(data));
    }

    public static async Task<T> Load<T>(string key)
    {
        var query = await Call(CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { key }));
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
