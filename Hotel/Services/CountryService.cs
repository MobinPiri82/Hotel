using AutoMapper;
using AutoMapper.QueryableExtensions;
using Hotel.Contract;
using Hotel.Data;
using Hotel.DTOs.Country;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Collections;



namespace Hotel.Services;



public class CountryServices(Context context, IMapper mapper) : ICountryInterface
{
    public async Task<IEnumerable<GetCountriesDTO>> GetCountries()
    {
        var countries = await context.countries.ProjectTo<GetCountriesDTO>(mapper.ConfigurationProvider).ToListAsync();
        return countries;
    }
    public async Task<GetCountryDTO> GetCountry(int id)
    {
        var country = await context.countries.Where(c => c.CountryId == id).Include(a=>a.Hotels).
             ProjectTo<GetCountryDTO>(mapper.ConfigurationProvider).
             SingleOrDefaultAsync();
        if (country == null)
            throw new KeyNotFoundException("");
        return country;
    }
    
    public async Task<GetCountryDTO> CreateCountry(CreateCountryDTO newCountry)
    {
        var country = await context.countries.AnyAsync(a => a.Name == newCountry.Name);

        if (!country)
        {
            var addingCountry = mapper.Map<Counrty>(newCountry);
            context.countries.AddAsync(addingCountry);
            context.SaveChangesAsync();
            var countryDto = mapper.Map<GetCountryDTO>(addingCountry);
            return countryDto;

        }
        return null;
    }
    public async Task<CreateCountryDTO> UpdateCountry(int id, UpdateCountryDTO UpdatingCountry)
    {
        var updatedCountry = await context.countries.FirstOrDefaultAsync(a => a.CountryId == id);
        if (updatedCountry != null)
        {
            mapper.Map(UpdatingCountry,updatedCountry);           
        }
        context.Entry(updatedCountry).State = EntityState.Modified;
        try
        {
            await context.SaveChangesAsync();

        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await CountryExistsAsync(id))
            {
                return null;
            }
            else throw;
        }
        return new CreateCountryDTO
        {
            Name = UpdatingCountry.Name,
            Shortname = UpdatingCountry.Shortname,
        };
    }
    public async Task DeleteCountry(int id)
    {
        var dddd = await context.countries.Where(a=>a.CountryId == id).ExecuteDeleteAsync();
        var delete = await context.countries.FirstOrDefaultAsync(a => a.CountryId == id);
        if (delete != null)
        {
            context.countries.Remove(delete);
            await context.SaveChangesAsync();

        }
        else  throw new KeyNotFoundException();
            
    }
    private async Task<bool> CountryExistsAsync(int id)
    {
        return await context.countries.AnyAsync(c => c.CountryId == id);
    }
}
