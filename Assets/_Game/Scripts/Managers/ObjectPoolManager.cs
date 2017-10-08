using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    public EZObjectPool bulletPool;
    public EZObjectPool cratesPool;
    public EZObjectPool enemyPool;
    public EZObjectPool meteorPool;
}
