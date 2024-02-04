using System;
using System.Collections.Generic;
using UnityEngine;

namespace GuardiansDefense.Pool
{
  public class Pool : MonoBehaviour
  {
    [SerializeField] private ObjectPooler _prefab;

    [Space(10)] [SerializeField] private Transform _container;

    [SerializeField, Min(0)] private int _minCapacity;
    [SerializeField] private int _maxCapacity;

    [Space(10)] [SerializeField] bool _autoExpand;

    //--------------------------------------

    private List<ObjectPooler> objectPoolers;

    //======================================

    private void Start()
    {
      CreatePool();
    }

    private void OnValidate()
    {
      if (_autoExpand)
      {
        _maxCapacity = int.MaxValue;
      }
    }

    //======================================

    public bool TryGetElement(out ObjectPooler parElement)
    {
      foreach (var element in objectPoolers)
      {
        if (element.gameObject.activeInHierarchy)
          continue;

        parElement = element;
        element.gameObject.SetActive(true);

        return true;
      }

      parElement = null;
      return false;
    }

    public ObjectPooler GetFreeElement()
    {
      if (TryGetElement(out ObjectPooler parElement))
        return parElement;

      if (_autoExpand)
        return CreateElement(true);

      if (objectPoolers.Count < _maxCapacity)
        return CreateElement(true);

      throw new Exception("Pool is over!");
    }

    public ObjectPooler GetFreeElement(Vector3 parPosition)
    {
      ObjectPooler element = GetFreeElement();
      element.transform.position = parPosition;

      return element;
    }

    public ObjectPooler GetFreeElement(Vector3 parPosition, Quaternion parRotation)
    {
      ObjectPooler element = GetFreeElement(parPosition);
      element.transform.rotation = parRotation;

      return element;
    }

    //======================================

    private void CreatePool()
    {
      objectPoolers = new List<ObjectPooler>(_minCapacity);

      for (int i = 0; i < _minCapacity; i++)
      {
        CreateElement();
      }
    }

    private ObjectPooler CreateElement(bool parIsActiveByDefault = false)
    {
      ObjectPooler createObject = Instantiate(_prefab, _container);
      createObject.gameObject.SetActive(parIsActiveByDefault);

      objectPoolers.Add(createObject);

      return createObject;
    }

    //======================================
  }
}