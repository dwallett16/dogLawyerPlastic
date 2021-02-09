using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableWrapper : IAddressableWrapper
{
    public AsyncOperationHandle<IList<T>> LoadAssets<T>(string key, Action<T> callback)
    {
        return Addressables.LoadAssetsAsync<T>(key, callback);
    }

    public void ReleaseAssets<T>(AsyncOperationHandle<IList<T>> assets)
    {
        assets.Completed += c => Addressables.Release(assets);
    }
}
