using Microsoft.EntityFrameworkCore;
using ModelsLib.Models;
using NuGet.Protocol.Plugins;
using System.Windows;
using WebProject.Database;
using WpfProject;
using WpfProject.ViewModels;
using WpfProject.Windows;
using WpfProject.Tools;

namespace AviaTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Delete_User_Test()
        {
            using (var context = new user05_1Context())
            {
                var user = new User() { Login = "����", Password = "�����" };
                context.Users.Add(user);
                context.SaveChanges();
                context.Users.Remove(user);
                context.SaveChanges();
                var result = context.Users.FirstOrDefault(u => u.Login == user.Login && u.Password == user.Password);
                Assert.IsNull(result);
            }
        }

        [Test]
        public void Update_User_Test()
        {
            using (var context = new user05_1Context())
            {
                var user = new User() { LastName = "�����", PhoneNumber = 8964738573 };
                context.Add(user);
                context.SaveChanges();
                user.PhoneNumber = 8957384835;
                context.SaveChanges();
                var result = context.Users.FirstOrDefault(s => s.LastName == "�����");
                Assert.IsNotNull(result);
                Assert.AreEqual(user.PhoneNumber, result.PhoneNumber);
            }
        }

        [Test]
        public void Save_User_Test()
        {
            using (var context = new user05_1Context())
            {
                var user = new User() {FirstName = "�������", LastName = "������", PatronomycName = "��������������" };
                user05_1Context.GetInstance().Users.Add(user);
                user05_1Context.GetInstance().SaveChanges();
                var result = context.Users.FirstOrDefault(s => s.LastName == user.LastName && s.FirstName ==  
                user.FirstName && s.PatronomycName == user.PatronomycName );
                Assert.IsNotNull(result);
            }
        }
        [Test]
        public void Update_User2_Test()
        {
            using (var context = new user05_1Context())
            {
                var user = new User() { FirstName = "����", LastName = "�������", PatronomycName = "���������" };
                user05_1Context.GetInstance().Users.Add(user);
                user05_1Context.GetInstance().SaveChanges();
                var lastUser = user05_1Context.GetInstance().Users.FirstOrDefault(s => s.LastName == "�������" && s.FirstName == "����" && s.PatronomycName == "���������");
                user05_1Context.GetInstance().Users.Update(lastUser);
                user05_1Context.GetInstance().SaveChanges();
                var updateUser = user05_1Context.GetInstance().Users.FirstOrDefault(s => s.LastName == "�������" && s.FirstName == "����" && s.PatronomycName == "���������");
                Assert.IsNotNull(updateUser);
            }
        }

        [Test]
        public void User_Creation_Correct_Test()
        {
            var user = new MainMenuGuestVM();
            Assert.IsNotNull (user);
        }

        [Test]
        public async Task Command_Save_Check()
        {
            var vm = new ListUserVM();
            Assert.IsNotNull(vm.Save);
        }

        [Test]
        public async Task Command_Delete_Check()
        {
            var vm = new ListUserVM();
            Assert.IsNotNull(vm.DeleteUser);
        }

        //[Test]
        //public void LastTest()
        //{
        //    var listAirplane = new ListUserVM();
        //    var expectedClass = new User();
        //    listAirplane.User = expectedClass;
        //    var actualList = listAirplane.User;
        //    Assert.AreEqual(expectedClass, actualList);
        //}
    }
}