using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShop2022.Areas.Admin;
using OnlineShop2022.Controllers;
using OnlineShop2022.Data;
using OnlineShop2022.Helpers;
using OnlineShop2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class ProductControllerTests
    {
        //needs to be given a mock database - dont want to effect real data.
        //private ILogger<ProductController> _logger;
        private AppDbContext _db;
        private Images _images;
        private IWebHostEnvironment _webHostEnvironment;






        private void CreateMockDB()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            //create database
            _db = new AppDbContext(options);

            //ensure creation and return
            _db.Database.EnsureCreated();

        }

        [Fact]
        public async void ProductUpdateIdNotEqual()
        {

            //Arrange
            //needs images, iweb and database.
            CreateMockDB();
            _images = new Images(_webHostEnvironment);
            ProductController controller = new ProductController(_db, _webHostEnvironment, _images);

            //Act 

            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.Product = new ProductModel();
            productViewModel.Product.Description = "Shirt";
            productViewModel.Product.Id = 1;


            //giving different id than declared above. cast result.
            var result = await controller.Update(2, productViewModel) as NotFoundResult;

            //Assert
            //check if null and return error code
            Assert.NotNull(result);
            Assert.Equal("404", result.StatusCode.ToString());

        }


        [Fact]
        
        public async void ProductUpdateIdNotNull()
        {
            CreateMockDB();
            //Arrange
            //needs images, iweb and database.
            CreateMockDB();
            _images = new Images(_webHostEnvironment);
            ProductController controller = new ProductController(_db, _webHostEnvironment, _images);

            //Act 

            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.Product = new ProductModel();
            productViewModel.Product.Description = "Shirt";
            productViewModel.Product.Id = 1;


            //checking for null
            var result = await controller.Update(null) as NotFoundResult;

            //Assert
            //check if null and return error code
            Assert.NotNull(result);
            Assert.Equal("404", result.StatusCode.ToString());



        }
        


    }
}
