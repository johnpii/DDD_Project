using AutoMapper;
using DDD_Project.API.Hubs;
using DDD_Project.API.ViewModels;
using DDD_Project.Domain.Entities;
using DDD_Project.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;

namespace DDD_Project.API.Controllers
{
    public class PhoneController(IPhoneService phoneService, ICountryService countryService, IMapper mapper, IHubContext<DDDHub> dddHub) : Controller
    {
        private readonly IPhoneService _phoneService = phoneService;
        private readonly ICountryService _countryService = countryService;
        private readonly IMapper _mapper = mapper;
        private readonly IHubContext<DDDHub> _dddHub = dddHub;

        public async Task<IActionResult> Phones()
        {
            List<Phone> allPhones = await _phoneService.GetAllPhonesAsync();
            return View(allPhones);
        }

        public async Task<IActionResult> CreatePhone()
        {
            var countries = await _countryService.GetAllCountriesAsync();
            ViewBag.Countries = countries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePhone(CreatePhoneViewModel phone)
        {
            if (ModelState.IsValid)
            {
                Phone addedPhone = await _phoneService.AddPhoneAsync(_mapper.Map<Phone>(phone));
                Country country = await _countryService.GetCountryByIdAsync(phone.CountryId);
                addedPhone.Country = country;
                await _dddHub.Clients.All.SendAsync("CreatePhone", new
                {
                    id = addedPhone.Id,
                    name = addedPhone.Name,
                    year = addedPhone.Year,
                    countryName = addedPhone.Country.Name
                });
                return RedirectToAction(nameof(Phones));
            }

            var countries = await _countryService.GetAllCountriesAsync();
            ViewBag.Countries = countries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            return View(phone);
        }

        public async Task<IActionResult> EditPhone(int id)
        {
            var phone = await _phoneService.GetPhoneByIdAsync(id);
            if (phone == null)
            {
                return NotFound();
            }

            var countries = await _countryService.GetAllCountriesAsync();
            ViewBag.Countries = countries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            return View(_mapper.Map<EditPhoneViewModel>(phone));
        }

        [HttpPost]
        public async Task<IActionResult> EditPhone(EditPhoneViewModel phone)
        {
            if (ModelState.IsValid)
            {
                Phone updatedPhone = await _phoneService.UpdatePhoneAsync(_mapper.Map<Phone>(phone));
                var country = await _countryService.GetCountryByIdAsync(phone.CountryId);
                updatedPhone.Country = country;

                await _dddHub.Clients.All.SendAsync("EditPhone", new
                {
                    id = updatedPhone.Id,
                    name = updatedPhone.Name,
                    year = updatedPhone.Year,
                    countryName = updatedPhone.Country.Name
                });

                return RedirectToAction(nameof(Phones));
            }

            var countries = await _countryService.GetAllCountriesAsync();
            ViewBag.Countries = countries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            return View(phone);
        }

        public async Task<IActionResult> DetailsPhone(int id)
        {
            var phone = await _phoneService.GetPhoneByIdAsync(id);
            if (phone == null)
            {
                return NotFound();
            }
            return View(phone);
        }

        public async Task<IActionResult> DeletePhone(int id)
        {
            var phone = await _phoneService.GetPhoneByIdAsync(id);
            if (phone == null)
            {
                return NotFound();
            }
            return View(phone);
        }

        [HttpPost, ActionName("DeletePhone")]
        public async Task<IActionResult> DeletePhoneConfirmed(int id)
        {
            await _phoneService.DeletePhoneAsync(id);
            await _dddHub.Clients.All.SendAsync("DeletePhone", id);
            return RedirectToAction(nameof(Phones));
        }
    }
}
