using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using AlternateLife.RageMP.Net.Helpers;
using AlternateLife.RageMP.Net.Interfaces;
using AlternateLife.RageMP.Net.Native;

namespace AlternateLife.RageMP.Net.Elements.Pools
{
    internal abstract class PoolBase<T> : IPool<T>, IInternalPool where T : IEntity
    {
        protected readonly IntPtr _nativePointer;
        protected readonly Plugin _plugin;

        private readonly ConcurrentDictionary<IntPtr, T> _entities = new ConcurrentDictionary<IntPtr, T>();

        public int Count => _entities.Count;

        public T this[int index] => _entities.Values.FirstOrDefault(x => x.Id == index);
        public T this[uint index] => this[(int) index];

        internal T this[IntPtr index]
        {
            get
            {
                if (_entities.TryGetValue(index, out T entity) == false)
                {
                    return default(T);
                }

                return entity;
            }
        }

        internal PoolBase(IntPtr nativePointer, Plugin plugin)
        {
            _nativePointer = nativePointer;
            _plugin = plugin;
        }

        protected abstract T BuildEntity(IntPtr entityPointer);

        public T GetAt(int index)
        {
            return GetAt((uint) index);
        }

        public T GetAt(uint index)
        {
            var pointer = Rage.Pool.Pool_GetAt(_nativePointer, index);

            if (_entities.TryGetValue(pointer, out T entity) == false)
            {
                return default(T);
            }

            return entity;
        }

        public async Task<IReadOnlyCollection<T>> GetInRangeAsync(Vector3 position, float range, uint dimension)
        {
            IntPtr entityPointers = IntPtr.Zero;
            ulong size = 0;

            await _plugin
                .Schedule(() => Rage.Pool.Pool_GetInRange(_nativePointer, position, range, dimension, out entityPointers, out size))
                .ConfigureAwait(false);

            return ArrayHelper.ConvertFromIntPtr(entityPointers, size, x => this[x]);
        }

        public async Task<IReadOnlyCollection<T>> GetInDimensionAsync(uint dimension)
        {
            IntPtr entityPointers = IntPtr.Zero;
            ulong size = 0;

            await _plugin
                .Schedule(() => Rage.Pool.Pool_GetInDimension(_nativePointer, dimension, out entityPointers, out size))
                .ConfigureAwait(false);

            return ArrayHelper.ConvertFromIntPtr(entityPointers, size, x => this[x]);
        }

        public IEntity GetEntity(IntPtr entity)
        {
            return this[entity];
        }

        public bool RemoveEntity(IntPtr entityPointer, Action<IEntity> preRemoveCallback)
        {
            if (_entities.TryGetValue(entityPointer, out T entity) == false)
            {
                return false;
            }

            preRemoveCallback?.Invoke(entity);

            if (entity is IInternalEntity internalEntity)
            {
                internalEntity.Exists = false;
            }

            return _entities.TryRemove(entityPointer, out _);
        }

        public bool CreateAndSaveEntity(IntPtr entityPointer, out IEntity entity)
        {
            entity = CreateAndSaveEntity(entityPointer);

            if (entity == null)
            {
                entity = default(IEntity);
                return false;
            }

            return true;
        }

        protected T CreateAndSaveEntity(IntPtr entityPointer)
        {
            if (_entities.TryGetValue(entityPointer, out var entity))
            {
                return entity;
            }

            entity = BuildEntity(entityPointer);

            if (_entities.TryAdd(entityPointer, entity) == false)
            {
                return default(T);
            }

            return entity;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _entities.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
