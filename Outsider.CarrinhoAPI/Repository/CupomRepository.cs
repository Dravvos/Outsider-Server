using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Outsider.CarrinhoAPI.Model;
using Outsider.CarrinhoAPI.Model.Context;
using Outsider.DTO;
using System.Net.Http.Headers;
using System.Net;
using System.Text.Json;

namespace Outsider.CarrinhoAPI.Repository
{
    public class CupomRepository : ICupomRepository
    {
        private readonly HttpClient _client;

        public CupomRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<CupomDTO> GetCupom(string codigoCupom, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"/api/cupom/{codigoCupom}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode != HttpStatusCode.OK) return new CupomDTO();
            return JsonSerializer.Deserialize<CupomDTO>(content,
                new JsonSerializerOptions
                { WriteIndented = true, PropertyNameCaseInsensitive = true });
        }
    }
}
