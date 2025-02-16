using AutoMapper;
using DDD_Project.API.Hubs;
using DDD_Project.API.ViewModels;
using DDD_Project.Domain.Entities;
using DDD_Project.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DDD_Project.API.Controllers
{
    public class CountryController(ICountryService countryService, IMapper mapper, IHubContext<DDDHub> dddHub) : Controller
    {
        private readonly ICountryService _countryService = countryService;
        private readonly IMapper _mapper = mapper;
        private readonly IHubContext<DDDHub> _dddHub = dddHub;

        public async Task<IActionResult> Countries()
        {
            List<Country> allCountries = await _countryService.GetAllCountriesAsync();
            return View(allCountries);
        }

        public IActionResult CreateCountry()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCountry(CreateCountryViewModel country)
        {
            if (ModelState.IsValid)
            {
                var addedCountry = await _countryService.AddCountryAsync(_mapper.Map<Country>(country));
                await _dddHub.Clients.All.SendAsync("CreateCountry", new
                {
                    id = addedCountry.Id,
                    name = addedCountry.Name
                });
                return RedirectToAction(nameof(Countries));
            }
            return View(country);
        }

        public async Task<IActionResult> EditCountry(int id)
        {
            var country = await _countryService.GetCountryByIdAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<EditCountryViewModel>(country));
        }

        [HttpPost]
        public async Task<IActionResult> EditCountry(EditCountryViewModel country)
        {
            if (ModelState.IsValid)
            {
                var updatedCountry = await _countryService.UpdateCountryAsync(_mapper.Map<Country>(country));
                await _dddHub.Clients.All.SendAsync("EditCountry", new
                {
                    id = updatedCountry.Id,
                    name = updatedCountry.Name
                });
                return RedirectToAction(nameof(Countries));
            }
            return View(country);
        }

        public async Task<IActionResult> DetailsCountry(int id)
        {
            var country = await _countryService.GetCountryByIdAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countryService.GetCountryByIdAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        [HttpPost, ActionName("DeleteCountry")]
        public async Task<IActionResult> DeleteCountryConfirmed(int id)
        {
            await _countryService.DeleteCountryAsync(id);
            await _dddHub.Clients.All.SendAsync("DeleteCountry", id);
            return RedirectToAction(nameof(Countries));
        }
    }
}
