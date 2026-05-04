using Hotel.Contract;
using Hotel.Data;
using Hotel.DTOs.Country;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Collections;



namespace Hotel.Services;



public class CountryServices(Context context) : ICountryInterface
{
    public async Task<IEnumerable<GetCountriesDTO>> GetCountries()
    {
        var countries = await context.countries.Select(a => new GetCountriesDTO(
            a.CountryId,
            a.Name,
            a.Shortname
            )).ToListAsync();
        return countries;
    }
    public async Task<GetCountryDTO> GetCountry(int id)
    {
        var country = await context.countries.Where(c => c.CountryId == id).Select(q => new GetCountryDTO(
            q.CountryId,
            q.Name,
            q.Shortname,
            q.Hotels.Select(z => z.Name).ToList()))
            .SingleOrDefaultAsync();
        if (country == null)
            throw new KeyNotFoundException("");
        return country;
    }
    
    public async Task<GetCountryDTO> CreateCountry(CreateCountryDTO newCountry)
    {
        var country = await context.countries.AnyAsync(a => a.Name == newCountry.Name);

        if (!country)
        {
            var addingCountry = new Counrty
            {
                Name = newCountry.Name,
                Shortname = newCountry.Shortname,
            };
            context.countries.AddAsync(addingCountry);
            context.SaveChangesAsync();
            var countryDto = new GetCountryDTO(
                addingCountry.CountryId,
                addingCountry.Name,
                addingCountry.Shortname,
                []
                );
            return countryDto;

        }
        return null;
    }
    public async Task<CreateCountryDTO> UpdateCountry(int id, UpdateCountryDTO UpdatinCountry)
    {
        var updatedCountry = await context.countries.FirstOrDefaultAsync(a => a.CountryId == id);
        if (updatedCountry != null)
        {
            updatedCountry.Name = UpdatinCountry.Name;
            updatedCountry.Shortname = UpdatinCountry.Shortname;
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
            Name = UpdatinCountry.Name,
            Shortname = UpdatinCountry.Shortname,
        };
    }
    public async Task DeleteCountry(int id)
    {
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
