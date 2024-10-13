namespace CodeBase.ServiceLocatorAPI
{
    public class ServiceRegistration
    {
        public readonly object Instance;
        public bool IsDisposable { get; private set; }

        public ServiceRegistration(object instance)
        {
            Instance = instance;
            IsDisposable = false;
        }

        public ServiceRegistration AsSceneService() 
        {
            IsDisposable = true;
            return this;
        }
    }
}