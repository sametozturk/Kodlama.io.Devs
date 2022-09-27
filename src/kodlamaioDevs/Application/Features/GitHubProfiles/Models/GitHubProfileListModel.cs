using Application.Features.GitHubProfiles.Dtos;
using Application.Features.Technologies.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GitHubProfiles.Models
{
    public class GitHubProfileListModel: BasePageableModel
    {
        public IList<GitHubProfileListDto> Items { get; set; }
    }
}
