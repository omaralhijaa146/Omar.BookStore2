using Volo.Abp.Settings;

namespace Omar.BookStore2.Settings;

public class BookStore2SettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(BookStore2Settings.MySetting1));
    }
}
