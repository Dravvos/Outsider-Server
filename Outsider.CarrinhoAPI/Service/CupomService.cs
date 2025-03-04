using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Outsider.CarrinhoAPI.Model;
using Outsider.CarrinhoAPI.Model.Context;
using Outsider.DTO;
using System.Net.Http.Headers;
using System.Net;
using System.Text.Json;
using Outsider.CarrinhoAPI.Repository;

namespace Outsider.CarrinhoAPI.Service
{
    public class CupomService : ICupomService
    {
        private readonly ICupomRepository _repository;

        public CupomService(ICupomRepository repository)
        {
            _repository = repository;
        }

        public async Task<CupomDTO> GetCupom(string codigoCupom, string token)
        {
           return await _repository.GetCupom(codigoCupom, token);
        }
    }
}
