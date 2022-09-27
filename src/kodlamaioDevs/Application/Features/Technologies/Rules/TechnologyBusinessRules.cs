using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Features.Technologies.Rules
{
    public class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository _technologyRepository;
        private readonly ILanguageRepository _languageRepository;
        public TechnologyBusinessRules(ITechnologyRepository technologyRepository, ILanguageRepository languageRepository)
        {
            _technologyRepository = technologyRepository;
            _languageRepository = languageRepository;
        }

        public async Task TechnologyNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<Technology> result = await _technologyRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any()) throw new BusinessException("Technology name exists.");
        }
        public void TechnologyShouldExistWhenRequested(Technology technology)
        {
            if (technology == null) throw new BusinessException("Requested technology does not exist.");
        }
        public async Task LanguageShouldExistWhenTechnologyRequested(Technology technology)
        {
            Language result= await _languageRepository.GetAsync(b => b.Id==technology.LanguageId);
            if (result==null) throw new BusinessException("Language does not exists.");
        }
    }
}
