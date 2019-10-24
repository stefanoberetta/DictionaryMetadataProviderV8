using System.Web.Mvc;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Services;
using Umbraco.Core.Services.Implement;

namespace FlyingFox.DictionaryMetadataProviderV8.Startup
{
    public class DictionaryMetadataProviderComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            
            composition.Components().Append<DictionaryMetadataProviderComponent>();
        }
    }

    public class DictionaryMetadataProviderComponent : IComponent
    {

        public void Initialize()
        {
            ModelMetadataProviders.Current = new Mvc.DictionaryMetadataProvider();
            LocalizationService.SavedDictionaryItem += LocalizationService_SavedDictionaryItem;
        }
        private void LocalizationService_SavedDictionaryItem(ILocalizationService sender, global::Umbraco.Core.Events.SaveEventArgs<global::Umbraco.Core.Models.IDictionaryItem> e)
        {
            
            if (ModelMetadataProviders.Current is Mvc.DictionaryMetadataProvider provider)
            {
                foreach (var item in e.SavedEntities)
                {
                    provider.UpdateCache(item);
                }
            }
        }
        public void Terminate()
        {
            
        }
    }
}
