﻿using ModelsLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfProject.ViewModels;

namespace WpfProject.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditService.xaml
    /// </summary>
    public partial class EditService : Window
    {
        public EditService(Service service)
        {
            InitializeComponent();
            DataContext = new EditServiceVM(service);
        }
    }
}
