using Services;
using Data.Repositories.Abstract;
using System;
using System.Linq;
using Mappers;
using Newtonsoft.Json;
using System.Collections.Generic;
using Domain;
using Xunit;

namespace ConsoleEShopMultilayeredTests
{
    public class RegistredUserServiceTests
    {
        private readonly RegistredUserService _sut;
        private readonly Moq.Mock<IRegistredUsersRepository> _registreduserRepository = new Moq.Mock<IRegistredUsersRepository>();


        public RegistredUserServiceTests()
        {
            _sut = new RegistredUserService(_registreduserRepository.Object);
        }
        private List<RegistredUser> GetTestUsersDomain()
        {
            var users = new List<RegistredUser>()
            {
                new RegistredUser(){ Name = "Dima",Lastname = "Melnyk",Email = "sfsdf@.com",Login = "Admin", Password = "Admin",Orders = null,Status = Domain.Enums.UserStatus.Admin},
                new RegistredUser(){ Name = "Diana",Lastname = "Melnyk",Email = "qwerty@.com",Login = "Abricos", Password = "red",Orders = null,Status = Domain.Enums.UserStatus.Registred},
                new RegistredUser(){ Name = "Dina",Lastname = "Melnyk",Email = "sfsdf@.com",Login = "Abricos", Password = "red",Orders = null,Status = Domain.Enums.UserStatus.Registred},
                new RegistredUser(){ Name = "Dilan",Lastname = "Melnyk",Email = "qwerty@.com",Login = "Admin", Password = "red",Orders = null,Status = Domain.Enums.UserStatus.Registred},
            };
            return users;
        }
        private List<Entities.RegistredUserEntity> ActualUsersRepository()
        {
            var users = new List<Entities.RegistredUserEntity>()
            {
                new Entities.RegistredUserEntity(){ID = 0, Name = "Dima",Lastname = "Melnyk",Email = "sfsdf@.com",Login = "Admin",Password = "Admin", Rights = (int)Domain.Enums.UserStatus.Admin},
            };
            return users;
        }
        private Entities.RegistredUserEntity GetTestUserEntity()
        {
            var user = new Entities.RegistredUserEntity
            {
                ID = 0,
                Name = "Dima",
                Lastname = "Melnyk",
                Email = "sfsdf@.com",
                Login = "Admin",
                Password = "Admin",
                Rights = 2
            };
            return user;
        }
        [Fact]
        public void GetUserByLogin_ShouldReturnUser_WhenUserExists()
        {
            //Arrange
            var userLogin = "Admin";

            _registreduserRepository.Setup(x => x.GetUserByLogin(userLogin));

            //Act
            var customer = _sut.GetUserByLogin(userLogin);

            //Assert
            Assert.Equal(customer.Login, userLogin);
        }
        [Fact]
        public void GetUserByLogin_ShouldReturnUserDomain_WhenUserExists()
        {
            //Arrange
            var userDomain = GetTestUsersDomain()[0];

            _registreduserRepository.Setup(x => x.GetUserByLogin(userDomain.Login));

            //Act
            var customer = _sut.GetUserByLogin(userDomain.Login);
            Assert.Equal(userDomain.Login, customer.Login);
            Assert.Equal(userDomain.Email, customer.Email);
        }
        [Fact]
        public void AddUser_ShouldReturnFalse_IfUserAlreadyExists()
        {
            //Arrange
            var userEntity = GetTestUserEntity();
            _registreduserRepository.Setup(x => x.AddUser(userEntity));

            //Act
            var customer = _sut.AddUser(GetTestUsersDomain()[0]);

            //Assert
            Assert.False(customer);
        }
        [Fact]
        public void AddUser_ShouldReturnFalse_IfUserWithSuchEmailAlreadyExists()
        {
            //Arrange
            var userEntity = GetTestUserEntity();
            _registreduserRepository.Setup(x => x.AddUser(userEntity));

            //Act
            var customer = _sut.AddUser(GetTestUsersDomain()[2]);

            //Assert
            Assert.False(customer);
        }
        [Fact]
        public void AddUser_ShouldReturnFalse_IfUserWithSuchLoginAlreadyExists()
        {
            //Arrange
            var userEntity = GetTestUserEntity();
            _registreduserRepository.Setup(x => x.AddUser(userEntity));

            //Act
            var customer = _sut.AddUser(GetTestUsersDomain()[3]);

            //Assert
            Assert.False(customer);
        }

        [Fact]
        public void AddUser_ShouldReturnTrue_IfUserDontAlreadyExists()
        {
            //Arrange
            var userEntity = GetTestUserEntity();
            _registreduserRepository.Setup(x => x.AddUser(userEntity));

            //Act
            var customer = _sut.AddUser(GetTestUsersDomain()[1]);

            //Assert
            Assert.True(customer);
        }
        [Fact]
        public void GetRegistredUsers_ShouldReturnUsersDomain()
        {
            //Arrange
            var usersRepo = ActualUsersRepository();
            _registreduserRepository.Setup(x => x.GetRegistredUsers());

            //Act
            var usersDomainRepo = usersRepo.Select(userEntity => userEntity.ToDomain()).ToList();
            var users = _sut.GetRegistredUsers();

            var usersDomainRepoStr = JsonConvert.SerializeObject(usersDomainRepo);
            var usersStr = JsonConvert.SerializeObject(users);

            //Assert
            Assert.Equal(usersDomainRepoStr, usersStr);
        }
        [Fact]
        public void UpdateUser_ShouldUpdateUser_WhenUserExists()
        {
            //Arrange
            var usersRepo = ActualUsersRepository();
            var userUpdatedInfo = new RegistredUser() { Login = "Admin" };
            _registreduserRepository.Setup(x => x.UpdateUser(usersRepo[0]));

            //Act
            var usersDomainRepo = usersRepo.Select(userEntity => userEntity.ToDomain()).ToList();
            _sut.UpdateUser(userUpdatedInfo);

            var usersDomainRepoStr = JsonConvert.SerializeObject(usersDomainRepo[0]);
            var userUpdatedInfoStr = JsonConvert.SerializeObject(userUpdatedInfo);

            //Assert
            Assert.NotEqual(usersDomainRepoStr, userUpdatedInfoStr);
        }
        [Fact]
        public void UpdateUser_ShouldNotUpdateUser_WhenUserDoesntExist()
        {
            //Arrange
            var usersRepo = ActualUsersRepository();
            var userUpdatedInfo = new RegistredUser() { Login = "Lordgi" };
            _registreduserRepository.Setup(x => x.UpdateUser(usersRepo[0]));

            //Act
            var usersDomainRepo = usersRepo.Select(userEntity => userEntity.ToDomain()).ToList();
            _sut.UpdateUser(userUpdatedInfo);

            var usersDomainRepoStr = JsonConvert.SerializeObject(usersDomainRepo[0]);
            var userUpdatedInfoStr = JsonConvert.SerializeObject(userUpdatedInfo);

            //Assert
            Assert.NotEqual(usersDomainRepoStr, userUpdatedInfoStr);
        }
    }
    public class OrderServiceTests
    {
        private readonly OrderService _sut;
        private readonly Moq.Mock<IOrdersRepository> _orderRepository = new Moq.Mock<IOrdersRepository>();
        private List<Entities.OrderEntity> ActualOrdersRepository()
        {
            var orders = new List<Entities.OrderEntity>()
            {
                
            };
            return orders;
        }
        private List<Entities.OrderEntity> OrdersEntitiesForTest()
        {
            var orders = new List<Entities.OrderEntity>()
            {
                new Entities.OrderEntity(){ ID = 0, FullCost = 477,
                OrderedProducts = new List<Entities.ProductEntity>()
                    {
                        new Entities.ProductEntity() { ID = 0, Name = "sfd", Category = "sdf", Description = "fsdf", CodeProduct = "AA11", Cost = 477 }
                    },
                Status = Domain.Enums.OrderStatus.New,
                Receiver = "lord" },

                new Entities.OrderEntity(){ ID = 0, FullCost = 477,
                OrderedProducts = new List<Entities.ProductEntity>()
                    {
                        new Entities.ProductEntity() { ID = 0, Name = "sfd", Category = "sdf", Description = "fsdf", CodeProduct = "AA11", Cost = 477 }
                    },
                Status = Domain.Enums.OrderStatus.New,
                Receiver = "fsdf" },

                new Entities.OrderEntity(){ ID = 0, FullCost = 477,
                OrderedProducts = new List<Entities.ProductEntity>()
                    {
                        new Entities.ProductEntity() { ID = 0, Name = "sfd", Category = "sdf", Description = "fsdf", CodeProduct = "AA11", Cost = 477 }
                    },
                Status = Domain.Enums.OrderStatus.New,
                Receiver = "lorder" },

                new Entities.OrderEntity(){ ID = 0, FullCost = 477,
                OrderedProducts = new List<Entities.ProductEntity>()
                    {
                        new Entities.ProductEntity() { ID = 0, Name = "sfd", Category = "sdf", Description = "fsdf", CodeProduct = "AA11", Cost = 477 }
                    },
                Status = Domain.Enums.OrderStatus.Received,
                Receiver = "lorder" },
            };
            return orders;
        }

        public OrderServiceTests()
        {
            _sut = new OrderService(_orderRepository.Object);

        }
        [Fact]
        public void AddOrder_ShouldAddOrderToRepository()
        {
            //Arrange
            var ordersRepo = ActualOrdersRepository();
            var orderDomain = new Order();
            orderDomain.FullCost = 477;
            orderDomain.Status = Domain.Enums.OrderStatus.New;
            orderDomain.Receiver = "fsdf";
            orderDomain.OrderedProducts = new List<Product>() { new Product() { Name = "sfd", Category = "sdf", Description = "fsdf", CodeProduct = "AA11", Cost = 477 } };
            _orderRepository.Setup(x => x.AddOrder(orderDomain.ToEntity()));

            //Act
            _sut.AddOrder(orderDomain);
            var expectedOrder = JsonConvert.SerializeObject(orderDomain.ToEntity());
            var actualOrder = JsonConvert.SerializeObject(_sut.GetOrders()[ActualOrdersRepository().Count].ToEntity());

            //Assert
            Assert.NotEqual(ordersRepo.Count, _sut.GetOrders().Count);
            Assert.Equal(expectedOrder, actualOrder);
        }

        [Fact]
        public void GetOrders_ShouldReturnOrdersList()
        {
            //Arrange
            var ordersRepo = ActualOrdersRepository();
            _orderRepository.Setup(x => x.GetOrders());

            //Act
            var expectedRepo = JsonConvert.SerializeObject(ordersRepo);
            var actualRepo = JsonConvert.SerializeObject(_sut.GetOrders());

            //Arrange
            Assert.Equal(expectedRepo, actualRepo);
        }
        [Fact]
        public void GetOrders_ShouldReturnOrdersList_WhenOrderWasAdded()
        {
            //Arrange
            var orderDomain = new Order();
            orderDomain.FullCost = 477;
            orderDomain.Status = Domain.Enums.OrderStatus.New;
            orderDomain.Receiver = "fsdf";
            orderDomain.OrderedProducts = new List<Product>() { new Product() { Name = "sfd", Category = "sdf", Description = "fsdf", CodeProduct = "AA11", Cost = 477 } };
            var ordersRepo = ActualOrdersRepository();
            ordersRepo.Add(OrdersEntitiesForTest()[1]);
            _orderRepository.Setup(x => x.GetOrders());

            //Act
            _sut.AddOrder(orderDomain);
            var expectedRepo = JsonConvert.SerializeObject(ordersRepo[0]);
            var actualRepo = JsonConvert.SerializeObject(_sut.GetOrders()[0].ToEntity());
            //Arrange
            Assert.Equal(expectedRepo, actualRepo);
        }
        [Fact]
        public void GetOrdersByLogin_ShouldReturnOrdersConnectedWithLogin_WhenUserHasOrders()
        {
            //Arrange
            var login = "lord";
            var ordersRepo = ActualOrdersRepository();
            ordersRepo.Add(OrdersEntitiesForTest()[0]);
            ordersRepo.Add(OrdersEntitiesForTest()[1]);
            _orderRepository.Setup(x => x.GetOrdersByLogin(login));

            //Act
            _sut.AddOrder(ordersRepo[0].ToDomain());
            var usersOrders = _sut.GetOrdersByLogin(login);
            var expectedorders = JsonConvert.SerializeObject(ordersRepo.Where(order => order.Receiver == login).Select(order => order));
            var actualorders = JsonConvert.SerializeObject(usersOrders.Select(order => order.ToEntity()));

            //Assert
            Assert.Equal(expectedorders, actualorders);
        }
        
        [Fact]
        public void UpdateOrderStatus_ShouldUpdateChoosenOrderStatus()
        {
            //Arrange
            var login = "lorder";
            var ordersRepo = ActualOrdersRepository();
            ordersRepo.Add(OrdersEntitiesForTest()[0]);
            ordersRepo.Add(OrdersEntitiesForTest()[1]);
            ordersRepo.Add(OrdersEntitiesForTest()[2]);
            ordersRepo.Add(OrdersEntitiesForTest()[3]);
            _orderRepository.Setup(x => x.UpdateOrderStatusByCustomID(0, login, Domain.Enums.OrderStatus.Received));

            //Act
            _sut.AddOrder(OrdersEntitiesForTest()[2].ToDomain());
            _sut.UpdateOrderStatusByCustomID(0, login, Domain.Enums.OrderStatus.Received);

            //Assert
            Assert.Equal(_sut.GetOrdersByLogin(login)[0].Status, ordersRepo[3].Status);
        }
    }
    public class ProductServiceTests
    {
        private readonly ProductService _sut;
        private readonly Moq.Mock<IProductsRepository> _productRepository = new Moq.Mock<IProductsRepository>();

        public ProductServiceTests()
        {
            _sut = new ProductService(_productRepository.Object);

        }
        private List<Entities.ProductEntity> ActualProductsRepository()
        {
            var products = new List<Entities.ProductEntity>()
            {
                new Entities.ProductEntity(){ID = 0, Name = "Lenovo Legion",Category = "Laptop",Description = "Оч крутой сам, юзаю", Cost = 1000,CodeProduct = "AA11" },
                new Entities.ProductEntity(){ID = 1, Name = "Asus Gaming",Category = "Laptop",Description = "Тоже крутой", Cost = 1200,CodeProduct = "AA12" },
                new Entities.ProductEntity(){ID = 2, Name = "Acer",Category = "Laptop",Description = "Крутой, но чуть хуже", Cost = 900,CodeProduct = "AA13" },
                new Entities.ProductEntity(){ID = 3, Name = "Xiaomi",Category = "Smartphone",Description = "Топ за свої гроші", Cost = 100,CodeProduct = "AA14" },
            };
            return products;
        }

        [Fact]
        public void AddProduct_ShouldAddProduct_WhenProductCodeDoesntAlreadyExist()
        {
            //Arrange
            var product = new Product() { Name = "Xiaomi", Category = "Smartphone", Description = "Топ за свої гроші", Cost = 100, CodeProduct = "AA15" };
            _productRepository.Setup(x => x.AddProduct(product.ToEntity()));

            //Assert
            Assert.True(_sut.AddProduct(product));
        }
        [Fact]
        public void DeleteProduct_ShouldDeleteProductById()
        {
            //Arrange
            int id = 4;
            _productRepository.Setup(x => x.DeleteProductByID(id));

            //Act
            _sut.DeleteProductByID(id);

            //Assert
            Assert.Equal(4, _sut.GetProducts().Count);
        }
        [Fact]
        public void GetBucketProducts_ShouldReturnProductsWithCorrectCodes()
        {
            //Arrange
            var bucket = new List<string>() { "AA11", "AA12", "fsdfsf" };
            var expectedProducts = new List<Product>();
            expectedProducts.Add(ActualProductsRepository()[0].ToDomain());
            expectedProducts.Add(ActualProductsRepository()[1].ToDomain());
            _productRepository.Setup(x => x.GetBucketProducts(bucket));

            //Act
            var expected = JsonConvert.SerializeObject(expectedProducts);
            var actual = JsonConvert.SerializeObject(_sut.GetBucketProducts(bucket));

            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetProductByCode_ShouldReturnProduct_WhenProductCodeExist()
        {
            //Arrange
            string productcode = "AA12";
            _productRepository.Setup(x => x.GetProductByCode(productcode));

            //Act
            var expected = JsonConvert.SerializeObject(ActualProductsRepository()[1].ToDomain());
            var actual = JsonConvert.SerializeObject(_sut.GetProductByCode(productcode));

            //Assert
            Assert.Equal(expected, actual);

        }
        [Fact]
        public void GetProductByCode_ShouldReturnEmptyProduct_WhenProductCodeDoesntExist()
        {
            //Arrange
            string productcode = "FDFds";
            _productRepository.Setup(x => x.GetProductByCode(productcode));

            //Act
            var expected = JsonConvert.SerializeObject(new Product());
            var actual = JsonConvert.SerializeObject(_sut.GetProductByCode(productcode));

            //Assert
            Assert.Equal(expected, actual);

        }
        [Fact]
        public void GetProductByID_ShouldReturnProductById_WhenProductWithIDExist()
        {
            //Arrange
            int id = 3;
            _productRepository.Setup(x => x.GetProductByID(id));

            //Act
            var expected = JsonConvert.SerializeObject(ActualProductsRepository()[3].ToDomain());
            var actual = JsonConvert.SerializeObject(_sut.GetProductByID(id));

            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetProductByID_ShouldReturnNull_WhenProductWithIDDoesntExist()
        {
            //Arrange
            int id = 4234324;
            _productRepository.Setup(x => x.GetProductByID(id));

            //Act
            var actual = _sut.GetProductByID(id);


            //Assert
            Assert.Null(actual);
        }
        [Fact]
        public void GetProducts_ShouldReturnProductList()
        {
            //Arrange
            _productRepository.Setup(x => x.GetProducts());

            //Act
            var expected = JsonConvert.SerializeObject(ActualProductsRepository().Select(product => product.ToDomain()));
            var actual = JsonConvert.SerializeObject(_sut.GetProducts());

            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetProductsByName_ShouldReturnProductsWithInputedName()
        {
            //Arrange
            string productName = "Acer";
            _productRepository.Setup(x => x.GetProductsByName(productName));

            //Act
            var expected = JsonConvert.SerializeObject(ActualProductsRepository().Where(product => productName == product.Name).Select(product => product.ToDomain()));
            var actual = JsonConvert.SerializeObject(_sut.GetProductsByName(productName));

            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void GetProductsByName_ShouldReturnEmptyList_WhenProductsWithInputedNameDoesntExist()
        {
            //Arrange
            string productName = "gdfgsdfgsfdgdsfgfdg";
            _productRepository.Setup(x => x.GetProductsByName(productName));

            //Act
            var expected = JsonConvert.SerializeObject(ActualProductsRepository().Where(product => productName == product.Name).Select(product => product.ToDomain()));
            var actual = JsonConvert.SerializeObject(_sut.GetProductsByName(productName));

            //Assert
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void SearchProductByCode_ShouldReturnTrue_WhenProductWithCodeExists()
        {
            //Arrange
            string productCode = "AA13";
            _productRepository.Setup(x => x.SearchProductByCODE(productCode));

            //Assert
            Assert.True(_sut.SearchProductByCODE(productCode));
        }
        [Fact]
        public void SearchProductByCode_ShouldReturnFalse_WhenProductWithCodeDoesntExists()
        {
            //Arrange
            string productCode = "FSDFewfew";
            _productRepository.Setup(x => x.SearchProductByCODE(productCode));

            //Assert
            Assert.False(_sut.SearchProductByCODE(productCode));
        }
        [Fact]
        public void UpdateProduct_ShouldUpdateProduct_WhenProductExists()
        {
            //Arrange
            var productsRepo = ActualProductsRepository();
            string oldCode = "AA14";
            var productUpdatedInfo = new Product() { Name = "Xiaomi", Category = "Smartphone", Description = "Топ за свої гроші", Cost = 130, CodeProduct = "AA14" };
            _productRepository.Setup(x => x.UpdateProduct(productUpdatedInfo.ToEntity(), oldCode));

            //Act
            var usersDomainRepo = productsRepo.Select(productEntity => productEntity.ToDomain()).ToList();
            _sut.UpdateProduct(productUpdatedInfo, oldCode);

            var expected = JsonConvert.SerializeObject(productUpdatedInfo);
            var actual = JsonConvert.SerializeObject(ActualProductsRepository()[3].ToDomain());

            //Assert
            Assert.NotEqual(expected, actual);
        }
        [Fact]
        public void UpdateProduct_ShouldntUpdateProduct_WhenProductDoesntAlreadyExist()
        {
            //Arrange
            var productsRepo = ActualProductsRepository();
            string oldCode = "ASDFG";
            var productUpdatedInfo = new Product() { Name = "Acer", Category = "Laptop", Description = "Крутой, но чуть хуже", Cost = 1900, CodeProduct = "AA13" };
            _productRepository.Setup(x => x.UpdateProduct(productUpdatedInfo.ToEntity(), oldCode));

            //Act
            var usersDomainRepo = productsRepo.Select(productEntity => productEntity.ToDomain()).ToList();
            _sut.UpdateProduct(productUpdatedInfo, oldCode);

            var expected = JsonConvert.SerializeObject(productUpdatedInfo);
            var actual = JsonConvert.SerializeObject(ActualProductsRepository()[2].ToDomain());

            //Assert
            Assert.NotEqual(expected, actual);
        }
    }
}


