using CarBook.Dto.PricingTypeDtos;
using CarBook.WebUI.Utilities.Settings;

namespace CarBook.WebUI.Services.PricingTypeServices
{
    public interface IPricingTypeService
    {
        Task<UIServiceApiResponseSetting<ResultPricingTypeDto>> GetPricingTypeAsync();
        Task<UIServiceApiResponseSetting<ResultPricingTypeDto>> GetPricingTypeByIdAsync(int id);
        Task<HttpResponseMessage> CreatePricingTypeAsync(CreatePricingTypeDto createPricingTypeDto);
        Task<HttpResponseMessage> UpdatePricingTypeAsync(UpdatePricingTypeDto updatePricingTypeDto);
        Task<HttpResponseMessage> DeletePricingTypeAsync(int id);
    }
}
