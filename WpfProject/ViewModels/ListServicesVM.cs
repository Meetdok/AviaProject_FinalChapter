using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WebProject.WebModels;
using WpfProject.Tools;
using WpfProject.Windows;

namespace WpfProject.ViewModels
{
    public class ListServicesVM : BaseTools
    {
        public Service SelectedItem { get; set; }
        public Service service { get; set; }
        private List<Service> services;
        public string Name { get; set; }
        public int Cost { get; set; }


        public List<Service> Service
        {
            get => services;
            set
            {
                services = value;

                Signal();
            }
        }

        public CommandVM EditService { get; set; }
        public CommandVM DeleteService { get; set; }
        public CommandVM Save { get; set; }

        public ListServicesVM()
        {
            Task.Run(async () =>
            {               
                var json2 = await HttpApi.Post("Services", "ListServices", null);
                Service = HttpApi.Deserialize<List<Service>>(json2);
            });


            EditService = new CommandVM(async () =>
            {
                service = SelectedItem;
                new EditService(service).Show();
            });

            Save = new CommandVM(async () =>
            {
                var json = await HttpApi.Post("Services", "save", new Service
                {
                    ServiceType = Name,
                    ServiceCost = Cost
                });
                Service result = HttpApi.Deserialize<Service>(json);

                MessageBox.Show("Сохранилось!");
            });

            DeleteService = new CommandVM(async () =>
            {
                var json = await HttpApi.Post("Services", "delete", SelectedItem.ServiceId);

                Task.Run(async () =>
                {
                    var json2 = await HttpApi.Post("Services", "ListServices", null);
                    Service = HttpApi.Deserialize<List<Service>>(json2);
                });
            });
        }
    }
}
