﻿using Microsoft.EntityFrameworkCore;
using Outsider.DTO;
using Outsider.TabelaGeralAPI.Repository;

namespace Outsider.TabelaGeralAPI.Services
{
    public class TabelaGeralService : ITabelaGeralService
    {
        private readonly ITabelaGeralRepository _tabelaGeralRepository;
        private readonly ITabelaGeralItemRepository _tabelaGeralItemRepository;

        public TabelaGeralService(ITabelaGeralRepository tabelaGeralRepository, ITabelaGeralItemRepository tabelaGeralItemRepository)
        {
            _tabelaGeralRepository = tabelaGeralRepository;
            _tabelaGeralItemRepository = tabelaGeralItemRepository;
        }

        public async Task<TabelaGeralDTO> AddAsync(TabelaGeralDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Descricao)) throw new ArgumentNullException("Descrição da tabela geral não pode estar vazia");
            if (string.IsNullOrEmpty(dto.Nome)) throw new ArgumentNullException("Nome da tabela geral não pode estar vazia");
            var tg = await _tabelaGeralRepository.GetByNomeAsync(dto.Nome);
            if (tg != null)
                throw new ArgumentException("Já existe uma tabela geral com esse nome");

            dto.Id = Guid.NewGuid();
            dto.DataInclusao = DateTime.Now;
            return await _tabelaGeralRepository.AddAsync(dto);
        }

        public async Task DeleteAsync(Guid id)
        {
            var tabelaGeralItens = await _tabelaGeralItemRepository.GetAllItemsAsync(id);
            if (tabelaGeralItens == null || tabelaGeralItens.Any() == false)
                throw new KeyNotFoundException();

            foreach (var item in tabelaGeralItens)
                await _tabelaGeralItemRepository.DeleteAsync(item.Id.Value);

            await _tabelaGeralRepository.DeleteAsync(id);


        }

        public async Task<IEnumerable<TabelaGeralDTO>> GetAllAsync()
        {
            return await _tabelaGeralRepository.GetAllAsync();
        }

        public async Task<TabelaGeralDTO> GetByIdAsync(Guid id)
        {
            return await _tabelaGeralRepository.GetByIdAsync(id);
        }

        public async Task<TabelaGeralDTO> GetByNomeAsync(string nome)
        {
            return await _tabelaGeralRepository.GetByNomeAsync(nome);
        }

        public async Task UpdateAsync(TabelaGeralDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Descricao)) throw new ArgumentNullException("Descrição da tabela geral não pode estar vazia");
            if (string.IsNullOrEmpty(dto.Nome)) throw new ArgumentNullException("Nome da tabela geral não pode estar vazia");
            if (dto.Id == Guid.Empty) throw new ArgumentNullException("Id da tabela geral não pode estar vazio");
            var tg = await _tabelaGeralRepository.GetByNomeAsync(dto.Nome);
            if (tg != null)
                throw new ArgumentException("Já existe uma tabela geral com esse nome");

            dto.DataAlteracao = DateTime.Now;
            await _tabelaGeralRepository.UpdateAsync(dto);
        }
    }
}
