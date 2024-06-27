using CommunityToolkit.Mvvm.ComponentModel;

namespace MyFriends.App.ViewModels
{
    public abstract partial class ViewModelBase : ObservableObject, IQueryAttributable
    {
        public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
        {

        }

        public void Refresh()
        {
            _ = InitializeAsync();
        }

        protected abstract Task InitializeAsync();
    }
}
