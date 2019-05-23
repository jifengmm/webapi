using AutoMapper;
using Project.Core.Utility;
using Project.Model;
using Project.Model.Dtos;

namespace Project.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>();
            CreateMap<AccountDto, Account>();
            CreateMap<Account, LoginUserInfoDto>();

            CreateMap<Word, WordDto>().ForMember(o => o.StatusDesc,
                p => p.MapFrom(q =>
                    Utils.GetEnumDesc<Status>(q.Status)));
            CreateMap<WordDto, Word>();

            CreateMap<Txt, TxtDto>();
            CreateMap<TxtDto, Txt>();

            CreateMap<EmailConfig, EmailConfigDto>();
            CreateMap<EmailConfigDto, EmailConfig>();

            CreateMap<FtpConfig, FtpConfigDto>();
            CreateMap<FtpConfigDto, FtpConfig>();

            CreateMap<Achievement, AchievementDto>();
            CreateMap<AchievementDto, Achievement>();
        }
    }
}