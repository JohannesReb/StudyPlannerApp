using App.BLL;
using App.Contracts.BLL;
using App.Contracts.DAL;
using App.DAL.EF;
using App.Domain.Identity;
using App.DTO.v1_0.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using WebApp.ApiControllers;

namespace App.Test;

public class ApiControllerCurriculumsTest
{
    private AppDbContext _ctx;

    private IAppBLL _bll;

    private IAppUnitOfWork _uow;

    private UserManager<User> _userManager;

    // sut
    private ApiCurriculumsController _controller;

    public ApiControllerCurriculumsTest()
    {
        // set up mock database - inmemory
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

        _ctx = new AppDbContext(optionsBuilder.Options);

        var configUow =
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<App.Domain.DbEntities.Curriculum, DAL.DTO.Entities.Curriculum>().ReverseMap();
                cfg.CreateMap<App.Domain.ManyToMany.UserCurriculum, DAL.DTO.ManyToMany.UserCurriculum>().ReverseMap();
            });
        var mapperUow = configUow.CreateMapper();


        _uow = new AppUoW(_ctx, mapperUow);

        var configBll = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<DAL.DTO.Entities.Curriculum, BLL.DTO.Entities.Curriculum>().ReverseMap();
            cfg.CreateMap<DAL.DTO.ManyToMany.UserCurriculum, BLL.DTO.ManyToMany.UserCurriculum>().ReverseMap();
        });
        var mapperBll = configBll.CreateMapper();
        _bll = new AppBLL(_uow, mapperBll);

        var configWeb = new MapperConfiguration(cfg => cfg.CreateMap<BLL.DTO.Entities.Curriculum, DTO.v1_0.Entities.Curriculum>().ReverseMap());
        var mapperWeb = configWeb.CreateMapper();


        var storeStub = Substitute.For<IUserStore<User>>();
        var optionsStub = Substitute.For<IOptions<IdentityOptions>>();
        var hasherStub = Substitute.For<IPasswordHasher<User>>();

        var validatorStub = Substitute.For<IEnumerable<IUserValidator<User>>>();
        var passwordStub = Substitute.For<IEnumerable<IPasswordValidator<User>>>();
        var lookupStub = Substitute.For<ILookupNormalizer>();
        var errorStub = Substitute.For<IdentityErrorDescriber>();
        var serviceStub = Substitute.For<IServiceProvider>();
        var loggerStub = Substitute.For<ILogger<UserManager<User>>>();

        _userManager = Substitute.For<UserManager<User>>(
            storeStub, 
            optionsStub, 
            hasherStub,
            validatorStub, passwordStub, lookupStub, errorStub, serviceStub, loggerStub
        );

        
        _controller = new ApiCurriculumsController(_ctx, _bll, _userManager, mapperWeb);
        _userManager.GetUserId(_controller.User).Returns(Guid.NewGuid().ToString());

    }

    // [Fact]
    // public async Task GetAllTest()
    // {
    //     var result = await _controller.GetCurriculums();
    //     var okRes = result.Result as OkObjectResult;
    //     var val = okRes!.Value as List<App.DTO.v1_0.Entities.Curriculum>;
    //     Assert.Empty(val);
    // }
    //
    // [Fact]
    // public async Task PostTest()
    // {
    //     // Post
    //     var curriculum = new Curriculum()
    //     {
    //         Code = "a",
    //         Label = "a",
    //         Manager = "a",
    //         From = DateTime.Now,
    //         Until = DateTime.Now + TimeSpan.Parse("365.00:00:00"),
    //         Semesters = 6
    //     };
    //     
    //     var result = await _controller.PostCurriculum(curriculum);
    //     var okRes = result.Result as CreatedAtActionResult;
    //     var val1 = okRes!.Value as Curriculum;
    //     Assert.Equal(val1, curriculum);
    // }
    //
    // [Fact]
    // public async Task GetTest()
    // {
    //     // Get
    //     var curriculum = new Curriculum()
    //     {
    //         Id = Id,
    //         Code = "a",
    //         Label = "a",
    //         Manager = "a",
    //         From = DateTime.Now,
    //         Until = DateTime.Now + TimeSpan.Parse("365.00:00:00"),
    //         Semesters = 6
    //     };
    //     var result = await _controller.GetCurriculum(Id);
    //     var okRes = result.Result as OkObjectResult;
    //     var val = okRes!.Value as Curriculum;
    //     Assert.Equivalent(val, curriculum);
    // }
    //
    // [Fact]
    // public async Task UpdateTest()
    // {
    //     // update
    //     var curriculum = new Curriculum()
    //     {
    //         Code = "a",
    //         Label = "b",
    //         Manager = "a",
    //         From = DateTime.Now,
    //         Until = DateTime.Now + TimeSpan.Parse("365.00:00:00"),
    //         Semesters = 6
    //     };
    //     var result = await _controller.UpdateCurriculum(curriculum);
    //     var okRes = result.Result as OkObjectResult;
    //     var val = okRes!.Value as Curriculum;
    //     Assert.Equal(curriculum, val);
    // }
}