﻿using ModelsLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfProject.Tools;
using WpfProject.Windows;

namespace WpfProject.ViewModels
{
    public class ListUserVM : BaseTools
    {
        public User SelectedItem { get; set; }
        private List<User> user;
        private List<Post> post;
        public User userere { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Patron { get; set; }
        public int Phone { get; set; }
        public string Mail { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }

        private Post posts;

        public Post SelectedPost
        {
            get => posts;
            set
            {
                posts = value;
                Signal();
            }
        }



        public List<User> User
        {
            get => user;
            set
            {
                user = value;

                Signal();
            }
        }

        public List<Post> Post
        {
            get => post;
            set
            {
                post = value;

                Signal();
            }
        }

        public CommandVM DeleteUser { get; set; }
        public CommandVM Edit { get; set; }
        public CommandVM Save { get; set; }

        public ListUserVM()
        {
            Task.Run(async () =>
            {
                var json = await HttpApi.Post("Users", "ListUsers", null);
                User = HttpApi.Deserialize<List<User>>(json);

                var json2 = await HttpApi.Post("Posts", "list", null);
                Post = HttpApi.Deserialize<List<Post>>(json2);

            });

            Edit = new CommandVM(async () =>
            {
                userere = SelectedItem;
                new EditUsers(userere).Show();
            });


            Save = new CommandVM(async () =>
            {
                var json = await HttpApi.Post("Users", "SaveUser", new User
                {
                    FirstName = Name,
                    LastName = LastName,
                    PatronomycName = Patron,
                    PhoneNumber = Phone,
                    Mail = Mail,
                    Login = Login,
                    Password = Pass,
                    PostId = SelectedPost.PostId
                });
                User result = HttpApi.Deserialize<User>(json);

                MessageBox.Show("Сохранилось!");

            });

            DeleteUser = new CommandVM(async () =>
            {
                var json = await HttpApi.Post("Users", "delete", SelectedItem.UserId);

                Task.Run(async () =>
                {
                    var json = await HttpApi.Post("Users", "ListUsers", null);
                    User = HttpApi.Deserialize<List<User>>(json);

                    var json2 = await HttpApi.Post("Posts", "list", null);
                    Post = HttpApi.Deserialize<List<Post>>(json2);

                });

            });

        }
    }
}
