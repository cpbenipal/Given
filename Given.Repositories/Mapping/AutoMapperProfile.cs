using AutoMapper;
using Given.DataContext.Entities;
using Given.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Given.Repositories.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>()
                .ForSourceMember(x => x.Company, y => y.DoNotValidate())
               .ForSourceMember(x => x.UserRole, y => y.DoNotValidate());
            CreateMap<UserModel, User>()
                .ForMember(x => x.Company, y => y.Ignore())
               .ForMember(x => x.UserRole, y => y.Ignore());


            CreateMap<Company, CompanyModel>()
                .ForSourceMember(x => x.CompanySize, y => y.DoNotValidate())
               .ForSourceMember(x => x.User, y => y.DoNotValidate());
            CreateMap<CompanyModel, Company>()
             .ForMember(x => x.CompanySize, y => y.Ignore())
            .ForMember(x => x.User, y => y.Ignore());

            CreateMap<Company, UpdateCompanyModel>().ReverseMap();
            CreateMap<UpdateCompanyModel, Company>().ReverseMap();


            CreateMap<CompanySize, CompanySizeModel>()
                .ForSourceMember(x => x.Company, y => y.DoNotValidate()).ReverseMap();
            CreateMap<CompanySizeModel, CompanySize>()
                .ForMember(x => x.Company, y => y.Ignore()).ReverseMap();


            CreateMap<User, UpdateModel>()
             .ForSourceMember(x => x.Company, y => y.DoNotValidate())
             .ForSourceMember(x => x.UserRole, y => y.DoNotValidate()).ReverseMap();
            CreateMap<UpdateModel, User>()
               .ForMember(x => x.Company, y => y.Ignore())
               .ForMember(x => x.UserRole, y => y.Ignore()).ReverseMap();


            CreateMap<Role, RoleModel>()
               .ForSourceMember(x => x.UserRole, y => y.DoNotValidate()).ReverseMap();
            CreateMap<RoleModel, Role>()
                .ForMember(x => x.UserRole, y => y.Ignore()).ReverseMap();


            CreateMap<User, RegisterModel>()
              .ForSourceMember(x => x.Company, y => y.DoNotValidate())
              .ForSourceMember(x => x.UserRole, y => y.DoNotValidate()).ReverseMap();
            CreateMap<RegisterModel, User>()
                .ForMember(x => x.UserRole, y => y.Ignore())
                .ForMember(x => x.Company, y => y.Ignore()).ReverseMap();

            CreateMap<User, ListUserModel>()
              .ForSourceMember(x => x.Company, y => y.DoNotValidate())
              .ForSourceMember(x => x.UserRole, y => y.DoNotValidate()).ReverseMap();
            CreateMap<ListUserModel, User>()
              .ForMember(x => x.UserRole, y => y.Ignore())
              .ForMember(x => x.Company, y => y.Ignore()).ReverseMap();

            CreateMap<Contacts, ContactModel>()
               .ForSourceMember(x => x.User, y => y.DoNotValidate());

            CreateMap<ContactModel, Contacts>()
                .ForMember(x => x.User, y => y.Ignore());

            CreateMap<Category, CategoryModel>()
               .ForSourceMember(x => x.Designation, y => y.DoNotValidate()).ReverseMap();
            CreateMap<CategoryModel, Category>()
               .ForMember(x => x.Designation, y => y.Ignore());

            CreateMap<Designation, DesignationModel>()
              .ForSourceMember(x => x.Category, y => y.DoNotValidate())
              .ForSourceMember(x => x.User, y => y.DoNotValidate())  
              .ForSourceMember(x => x.Gift, y => y.DoNotValidate()).ReverseMap();

            CreateMap<DesignationModel, Designation>()
               .ForMember(x => x.Category, y => y.Ignore())
                 .ForMember(x => x.User, y => y.Ignore())
               .ForMember(x => x.Gift, y => y.Ignore());


            CreateMap<Gift, GiftModel>()
              .ForSourceMember(x => x.Designation, y => y.DoNotValidate())
              .ForSourceMember(x => x.Contact, y => y.DoNotValidate())
              .ForSourceMember(x => x.User, y => y.DoNotValidate()).ReverseMap();

            CreateMap<GiftModel, Gift>()
               .ForMember(x => x.Designation, y => y.Ignore())
                 .ForMember(x => x.Contact, y => y.Ignore())
               .ForMember(x => x.User, y => y.Ignore());
        }
    }
}
