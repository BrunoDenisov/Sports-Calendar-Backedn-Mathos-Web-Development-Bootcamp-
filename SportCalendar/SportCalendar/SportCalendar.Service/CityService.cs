﻿using SportCalendar.Model;
using SportCalendar.ModelCommon;
using SportCalendar.RepositoryCommon;
using SportCalendar.ServiceCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCalendar.Service
{
    public class CityService : ICityService
    {
        private ICityRepository CityRepository;

        public CityService(ICityRepository repository)
        {
            CityRepository = repository;
        }
        public async Task<List<City>> GetAll()
        {
            try
            {
                var result = await CityRepository.GetAll();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<City>> GetById(Guid id)
        {
            try
            {
                var result = await CityRepository.GetById(id);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<City> Post(City city)
        {
            try
            {
                var result = await CityRepository.Post(city);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<City> Put(Guid id, City updatedCity)
        {
            try
            {
                var result = await CityRepository.Put(id, updatedCity);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
