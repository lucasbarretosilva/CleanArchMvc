using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _ICategoryRepository;
        private readonly IMapper _Mapper;

        public CategoryService(ICategoryRepository iCategoryRepository, IMapper mapper)
        {
            _ICategoryRepository = iCategoryRepository;
            _Mapper = mapper;
        }

        public async Task Add(CategoryDTO categoryDTO)
        {
            var categoryEntity = _Mapper.Map<Category>(categoryDTO);
            await _ICategoryRepository.Create(categoryEntity);
        }

        public async Task<CategoryDTO> GetById(int? id)
        {
            var categoryEntity = await _ICategoryRepository.GetById(id);
            return _Mapper.Map<CategoryDTO>(categoryEntity);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categoriesEntity = await _ICategoryRepository.GetCategories();
            return _Mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
        }

        public async Task Remove(int? id)
        {
            var categoryEntity = await _ICategoryRepository.GetById(id);
            await _ICategoryRepository.Remove(categoryEntity);
        }

        public async Task Update(CategoryDTO categoryDTO)
        {
            var categoryEntity = _Mapper.Map<Category>(categoryDTO);
            await _ICategoryRepository.Update(categoryEntity);
        }
    }
}
