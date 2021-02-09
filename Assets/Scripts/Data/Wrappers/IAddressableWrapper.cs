using System;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;

public interface IAddressableWrapper
{
    AsyncOperationHandle<IList<T>> LoadAssets<T>(string key, Action<T> callback);
    void ReleaseAssets<T>(AsyncOperationHandle<IList<T>> assets);
}
