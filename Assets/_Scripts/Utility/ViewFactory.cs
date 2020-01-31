using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace _Scripts.Utility
{
    public interface IViewFactory<out T>
    {
        void Create(params object[] args);
        void Create(Vector3 position, params object[] args);
        void Create(Vector3 position, Quaternion rotation, params object[] args);
    }
    
    public class ViewFactory<T> : IViewFactory<T> where T : Component
    {
        private readonly DiContainer _container;
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly Stack<GameObject> _availableInstances = new Stack<GameObject>();
        
        public ViewFactory(DiContainer container, T prefab)
        {
            _container = container;
            _prefab = prefab;

            var projectContext = ProjectContext.Instance.transform;
            _parent = new GameObject(typeof(T).Name).transform;
            _parent.parent = projectContext.transform;
        }
        
        public void Create(object[] args)
        {
            Create(Vector3.zero, args);
        }

        public void Create(Vector3 position, object[] args)
        {
            Create(position, Quaternion.identity, args);
        }

        public void Create(Vector3 position, Quaternion rotation, object[] args)
        {
            var subContext = _container.CreateSubContainer();
            var view = SpawnView(position, rotation);
            subContext.Bind<GameObject>().FromInstance(view);
            
            var disposer = new CompositeDisposable();
            
            subContext.Bind<ISelfDestruction>().FromInstance(new SelfDestruction(disposer.Dispose));
            var despawn = SetupDespawnView(view);
            
            var installer = view.GetComponent<View>();
            installer.ResetView();
            
            subContext.Inject(installer, args);
            installer.InstallBindings();
           
            SetupInitializableManager(subContext);
            SetupDisposableManager(subContext, disposer);
            SetupTickableManager(subContext, disposer);
            
            disposer.Add(despawn);
        }

        private GameObject SpawnView(Vector3 position, Quaternion rotation)
        {
            if (_availableInstances.Count <= 0)
                return Object.Instantiate(_prefab, position, rotation, _parent).gameObject;

            var view = _availableInstances.Pop();
            view.transform.position = position;
            view.transform.rotation = rotation;
            view.SetActive(true);
            
            return view;
        }

        private IDisposable SetupDespawnView(GameObject view)
        {
            return Disposable.Create(() =>
            {
                view.transform.parent = _parent;
                view.SetActive(false);
                _availableInstances.Push(view);
            });
        }

        private void SetupTickableManager(DiContainer subContext, CompositeDisposable disposer)
        {
            var manager = subContext.Instantiate<TickableManager>();
            Observable.EveryUpdate().Subscribe(manager.Update).AddTo(disposer);
            Observable.EveryLateUpdate().Subscribe(manager.LateUpdate).AddTo(disposer);
            Observable.EveryFixedUpdate().Subscribe(manager.FixedUpdate).AddTo(disposer);
        }

        private void SetupDisposableManager(DiContainer subContext, CompositeDisposable disposer)
        {
            var manager = subContext.Instantiate<DisposableManager>();
            disposer.Add(Disposable.Create(() =>
            {
                manager.Dispose();
                manager.LateDispose();
            }));
        }

        private void SetupInitializableManager(DiContainer subContext)
        {
            var manager = subContext.Instantiate<InitializableManager>();
            manager.Initialize();
        }
    }
}