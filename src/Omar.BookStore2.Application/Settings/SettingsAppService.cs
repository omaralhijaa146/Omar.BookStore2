using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.SettingManagement;

namespace Omar.BookStore2.Settings
{
    public class SettingsAppService:ApplicationService,ISettingsAppService
    {
        private readonly ISettingManager _settingManager;

        public SettingsAppService(ISettingManager settingManager)
        {
            _settingManager = settingManager;
        }

        public async Task ChangeSettingAsync(CreateCustomSettingsDto settingsDto) {

            Check.NotNull(settingsDto,nameof(settingsDto));
            Check.NotNullOrWhiteSpace(settingsDto.ShowBookAuthor,nameof(settingsDto.ShowBookAuthor));
            await _settingManager.SetGlobalAsync(BookStore2Settings.ShowBookAuthor,settingsDto.ShowBookAuthor);
        }
        public async Task<CustomSettingsDto> GetSettingsAsync()
        {
            var settingsDto= new CustomSettingsDto { 
                ShowBookAuthor= (await _settingManager.GetOrNullGlobalAsync(BookStore2Settings.ShowBookAuthor))
            };
            return settingsDto;
        }
    }
}
