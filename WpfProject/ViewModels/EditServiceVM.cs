using ModelsLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfProject.Tools;

namespace WpfProject.ViewModels
{
    public class EditServiceVM : BaseTools
    {
        public CommandVM Save { get; set; }

        public string Name { get; set; }
        public int Cost { get; set; }

        public EditServiceVM(Service service)
        {
            Save = new CommandVM(async () =>
            {
                var json = await HttpApi.Post("Services", "put", new Service
                {
                    ServiceId = service.ServiceId,
                    ServiceType = Name,
                    ServiceCost = Cost
                });

                MessageBox.Show("Сохранилось!");
            });
        }
    }
}
