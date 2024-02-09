using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace ECS
{
    public abstract class System
    {
        private static List<GameObject> _entities = new List<GameObject>();

        protected GameData gameData;
        protected List<GameObject> entities = null;
        protected static Action OnChange;

        public System(GameData gameData)
        {
            this.gameData = gameData;

            Init();
            OnChange += OnEntitiesChanged;
        }

        protected virtual void Init()
        {
            OnEntitiesChanged();
        }

        protected virtual bool Filter(GameObject entity) => true;
        protected virtual void OnEntitiesChanged() => entities = _entities.Where(e => Filter(e)).ToList();

        protected virtual List<GameObject> GetEntities(params Type[] componentTypes) => _entities.Where(e => HasComponents(e, componentTypes)).ToList();

        protected virtual void AddEntity(GameObject entity)
        {
            _entities.Add(entity);
            OnChange?.Invoke();
        }

        protected virtual void RemoveEntity(GameObject entity)
        {
            DestroyEntity(entity);
            _entities.Remove(entity);
            OnChange?.Invoke();
        }

        private void DestroyEntity(GameObject entity)
        {
            entity.SetActive(false);

            if (entity.TryGetComponent(out Asteroid a))
                ObjectPool.Release("Asteroid", entity);
            else if (entity.TryGetComponent(out Player s))
                ObjectPool.Release("Ship", entity);
            else if (entity.TryGetComponent(out Bullet b))
                ObjectPool.Release("Bullet", entity);
            else if (entity.TryGetComponent(out Duration d))
                ObjectPool.Release("Laser", entity);
            else
                UnityEngine.Object.Destroy(entity);
        }

        protected bool HasComponents(GameObject gameObject, params Type[] componentTypes)
        {
            foreach (var componentType in componentTypes)
                if (!gameObject.TryGetComponent(componentType, out var component))
                    return false;
            return true;
        }

        protected void ClearScene()
        {
            _entities.ForEach(e => DestroyEntity(e));
            _entities.Clear();

        }


        public virtual void Update()
        {
            //for override
        }

    }
}
