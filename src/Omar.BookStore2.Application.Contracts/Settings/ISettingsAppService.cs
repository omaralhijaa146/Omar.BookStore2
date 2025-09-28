using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Omar.BookStore2.Settings
{
    public interface ISettingsAppService:IApplicationService
    {

        public Task ChangeSettingAsync(CreateCustomSettingsDto settingsDto);
        public  Task<CustomSettingsDto> GetSettingsAsync();


        }
}
