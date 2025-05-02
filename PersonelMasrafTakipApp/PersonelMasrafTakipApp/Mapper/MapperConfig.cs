using AutoMapper;
using PersonelMasrafTakipApp.Domain;
using MasrafTakip.Schema;
using MasrafTakip.Base;

namespace PersonelMasrafTakipApp.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<CategoryRequest, Category>();
        CreateMap<Category, CategoryResponse>();

        CreateMap<UserRequest, User>();
        CreateMap<User, UserResponse>().ForMember(dest => dest.Expenses, opt => opt.MapFrom(src => src.Expense)); // yeni eklenen expense user response'a gelmez

        CreateMap<ExpenseRequest, Expense>();
        CreateMap<Expense, ExpenseResponse>()
    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
    .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

    }
}